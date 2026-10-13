using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Navbar;
using Top.Lhgb.Ui.Components.Toast;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Widgets;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Docs;

/// <summary>
/// 文档宿主：左侧分组导航 + 右侧文档区 + 顶部 <see cref="LhNavbar"/>（dogfood）+ 全局 <see cref="LhToast"/>。
///
/// 照 Android <c>:docsui</c> 范式的四支柱：
/// ① 令牌先行——外观全部取 <c>LhTokens.Docs.*</c>（经 <see cref="DocPalette"/> 按明暗解析），零字面量；
/// ② 内容与预览分离——页面正文由 <c>scripts/gen-doc-pages.mjs</c> 生成（禁手改），
///    真交互预览手写在 <c>src/Docs/previews/</c>，经 <see cref="DocPreviewScope"/> 注入宿主能力；
/// ③ 宿主零依赖——返回栈手写（<c>Stack&lt;string&gt;</c>），顶栏 dogfood 自家 <see cref="LhNavbar"/>，
///    全局提示 dogfood 自家 <see cref="LhToast"/>，不依赖框架级导航/弹窗；
/// ④ 双形态分发——本宿主是 UserControl，既可由 <see cref="LhDocs"/> 开窗口承载，也可直接嵌入任意窗口。
///
/// 明暗切换：顶栏右侧按钮调 <see cref="LhTheme.Apply"/>，所有 Lh 组件热重绘；
/// 宿主自身订阅 <c>ThemeChanged</c> 同步页面底色与侧栏描边色。
/// </summary>
public sealed class LhDocsHost : UserControl
{
    /// <summary>分组顺序与 <c>docs/.vitepress/config.ts</c> 侧边栏一致（真源在 DocRegistry）</summary>
    private static readonly DocGroup[] Groups =
    {
        DocGroup.Guide,
        DocGroup.Basic,
        DocGroup.Form,
        DocGroup.Feedback,
        DocGroup.Navigation,
    };

    private readonly Grid _root;
    private readonly LhNavbar _navbar;
    private readonly ScrollViewer _sidebar;
    private readonly ScrollViewer _contentHost;
    private readonly LhToast _toast;

    /// <summary>侧栏每组一个 ListView，连同其分组一起记录（跨组单选与反查都要它）</summary>
    private readonly List<(DocGroup Group, ListView List)> _groupLists = new();

    /// <summary>手写返回栈：栈顶是当前页 id</summary>
    private readonly Stack<string> _backStack = new();
    private string _currentId = string.Empty;
    private LhToastPosition _toastPosition = LhToastPosition.Center;

    /// <summary>
    /// 预览作用域：预览只通过它与宿主通信（显示全局 Toast / 切换 Toast 定位），
    /// 不自持浮层，保持「宿主拖入即用」。
    /// </summary>
    private DocPreviewScope Scope { get; }

    public LhDocsHost()
    {
        _toast = new LhToast();
        Scope = new DocPreviewScope(ShowToast, position => _toastPosition = position);

        _root = new Grid();
        _root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        _root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        // —— 顶栏：dogfood 自家 LhNavbar ——
        _navbar = new LhNavbar
        {
            Title = "lh-ui-windows 文档",
            LeftIcon = "back",
            RightText = "切换主题",
            Bordered = true,
        };
        _navbar.LeftClick += OnBack;
        _navbar.RightClick += OnToggleTheme;
        Grid.SetRow(_navbar, 0);
        _root.Children.Add(_navbar);

        // —— 主体：左侧分组导航 + 右侧文档区 ——
        var body = new Grid();
        Grid.SetRow(body, 1);
        body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SidebarWidth) });
        body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        _sidebar = BuildSidebar();
        Grid.SetColumn(_sidebar, 0);
        body.Children.Add(_sidebar);

        _contentHost = new ScrollViewer
        {
            Padding = new Thickness(LhEpx.Px(LhTokens.Spacing.Lg)),
            HorizontalContentAlignment = HorizontalAlignment.Left,
        };
        Grid.SetColumn(_contentHost, 1);
        body.Children.Add(_contentHost);

        _root.Children.Add(body);

        // —— 全局 Toast：行 0 起、跨行铺满，挂末层（渲染层级最高）——
        Grid.SetRow(_toast, 0);
        Grid.SetRowSpan(_toast, 2);
        _root.Children.Add(_toast);

        Content = _root;

        // Window 没有 Loaded 事件（那是 FrameworkElement 的）；主题监听直接挂上，
        // 组件自身订阅 ThemeChanged 热重绘。宿主卸载时解绑（见 Unloaded）。
        LhTheme.ThemeChanged += OnThemeChanged;
        Unloaded += OnUnloaded;
        ApplyPageBackground();

        Navigate(DocRegistry.All[0].Id, push: false);
        UpdateNavbar();
    }

    /// <summary>侧栏宽度（spacing 令牌的整数倍，与正文留白同源）</summary>
    private static double SidebarWidth => LhTokens.Spacing.Xxl * 6.0;

    /// <summary>导航到指定 id 的条目；push 为 true 时压入返回栈</summary>
    public void Navigate(string id, bool push = true)
    {
        var entry = DocRegistry.ById(id);
        if (entry is null) return;

        if (push && (_backStack.Count == 0 || _backStack.Peek() != id))
        {
            _backStack.Push(id);
        }
        _currentId = id;

        _contentHost.Content = BuildPage(entry);
        _navbar.Title = entry.Title;
        UpdateNavbar();
        SelectSidebar(id);
    }

    /// <summary>返回栈出栈；栈深 ≤ 1 时无操作（顶栏返回箭头按栈深显隐）</summary>
    public bool GoBack()
    {
        if (_backStack.Count <= 1) return false;

        _backStack.Pop();
        var id = _backStack.Peek();
        var entry = DocRegistry.ById(id);
        if (entry is null) return false;

        _currentId = id;
        _contentHost.Content = BuildPage(entry);
        _navbar.Title = entry.Title;
        UpdateNavbar();
        SelectSidebar(id);
        return true;
    }

    /// <summary>渲染一篇文档：标题 → 导语 → 契约 → 分节（散文 / 预览框 / 代码 / 表格）</summary>
    private StackPanel BuildPage(DocEntry entry)
    {
        var page = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Spacing = LhEpx.Px(LhTokens.Spacing.Md),
            MaxWidth = LhEpx.Px(LhTokens.Spacing.Xxl * 32.0),
        };

        page.Children.Add(DocWidgets.Heading(entry.Title, 1));

        foreach (var line in entry.Lede)
        {
            page.Children.Add(DocWidgets.Prose(line));
        }

        if (entry.Contract.Count > 0)
        {
            foreach (var line in entry.Contract)
            {
                page.Children.Add(DocWidgets.Prose(line, secondary: true));
            }
        }

        page.Children.Add(DocWidgets.Divider());

        foreach (var section in entry.Sections)
        {
            page.Children.Add(BuildSection(section));
        }

        return page;
    }

    /// <summary>渲染一节：小标题 → 散文 → 预览框（真交互）→ 代码块 → 表格</summary>
    private StackPanel BuildSection(DocSection section)
    {
        var panel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Spacing = LhEpx.Px(LhTokens.Spacing.Sm),
        };

        panel.Children.Add(DocWidgets.Heading(section.Title, 2));

        foreach (var line in section.Prose)
        {
            panel.Children.Add(DocWidgets.Prose(line));
        }

        if (section.Preview is { } preview)
        {
            var frame = new DocPreviewFrame
            {
                Scene = section.Title,
                Preview = preview(Scope),
            };
            panel.Children.Add(DocWidgets.Card(frame));
        }

        if (section.Code.Count > 0)
        {
            panel.Children.Add(DocWidgets.CodeBlock(section.Code));
        }

        foreach (var table in section.Tables)
        {
            panel.Children.Add(DocWidgets.Table(table));
        }

        return panel;
    }

    /// <summary>左侧分组导航：分组标签 + 每组一个 ListView，顺序对齐 config.ts 侧边栏</summary>
    private ScrollViewer BuildSidebar()
    {
        var stack = new StackPanel { Orientation = Orientation.Vertical };

        foreach (var group in Groups)
        {
            var label = new TextBlock
            {
                Text = DocGroupLabels.Of(group),
                FontSize = DocPalette.ProseFontSize,
                FontWeight = Microsoft.UI.Text.FontWeights.Bold,
                Foreground = new SolidColorBrush(DocPalette.HeadingColor),
                Padding = new Thickness(
                    LhEpx.Px(LhTokens.Spacing.Md),
                    LhEpx.Px(LhTokens.Spacing.Sm),
                    LhEpx.Px(LhTokens.Spacing.Md),
                    LhEpx.Px(LhTokens.Spacing.Xs)),
            };
            stack.Children.Add(label);

            var list = new ListView
            {
                ItemsSource = TitlesOf(group),
                SelectionMode = ListViewSelectionMode.Single,
                IsItemClickEnabled = false,
            };
            list.SelectionChanged += OnSidebarChanged;
            _groupLists.Add((group, list));
            stack.Children.Add(list);
        }

        return new ScrollViewer
        {
            Content = stack,
            HorizontalContentAlignment = HorizontalAlignment.Stretch,
            BorderBrush = new SolidColorBrush(DocPalette.CardBorder),
            BorderThickness = new Thickness(0, 0, DocPalette.CardBorderWidth, 0),
        };
    }

    /// <summary>取某分组的条目标题列表（侧栏只显示标题，导航走 id，顺序即 DocRegistry 顺序）</summary>
    private static IReadOnlyList<string> TitlesOf(DocGroup group)
    {
        var titles = new List<string>();
        foreach (var entry in DocRegistry.All)
        {
            if (entry.Group == group) titles.Add(entry.Title);
        }
        return titles;
    }

    /// <summary>取某分组内第 index 个条目（与 TitlesOf 同序）</summary>
    private static DocEntry? EntryAt(DocGroup group, int index)
    {
        var i = 0;
        foreach (var entry in DocRegistry.All)
        {
            if (entry.Group != group) continue;
            if (i == index) return entry;
            i++;
        }
        return null;
    }

    /// <summary>侧栏点击：标题 → id 导航（压入返回栈）</summary>
    private void OnSidebarChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListView list || list.SelectedIndex < 0) return;

        // 跨组单选：清掉其它组的选中态，避免两处高亮
        var group = DocGroup.Guide;
        foreach (var (g, other) in _groupLists)
        {
            if (other == list) group = g;
            else other.SelectedIndex = -1;
        }

        var entry = EntryAt(group, list.SelectedIndex);
        if (entry is { } resolved && resolved.Id != _currentId)
        {
            Navigate(resolved.Id);
        }
    }

    /// <summary>同步侧栏选中态到当前 id（导航时调用）</summary>
    private void SelectSidebar(string id)
    {
        var entry = DocRegistry.ById(id);
        if (entry is null) return;

        var groupIndex = Array.IndexOf(Groups, entry.Group);
        var indexInGroup = 0;
        var target = -1;
        foreach (var e in DocRegistry.All)
        {
            if (e.Group != entry.Group) continue;
            if (e.Id == id) target = indexInGroup;
            indexInGroup++;
        }

        for (var i = 0; i < _groupLists.Count; i++)
        {
            var (_, list) = _groupLists[i];
            // 选中态变化会触发 SelectionChanged，OnSidebarChanged 里用 _currentId 判重避免重复导航
            list.SelectedIndex = i == groupIndex ? target : -1;
        }
    }

    /// <summary>顶栏返回：出栈；栈深 ≤ 1 时无操作</summary>
    private void OnBack()
    {
        if (GoBack())
        {
            ShowToast("已返回上一页");
        }
    }

    private void OnToggleTheme()
    {
        LhTheme.Apply(!LhTheme.IsDark);
        ShowToast(LhTheme.IsDark ? "已切换暗色" : "已切换明色");
    }

    /// <summary>全局 Toast：预览与宿主共用同一条指令式通道</summary>
    private void ShowToast(string text)
    {
        _toast.Position = _toastPosition;
        _toast.Show(text);
    }

    /// <summary>顶栏返回箭头按返回栈深度显隐（栈深 ≤ 1 时无返回页）</summary>
    private void UpdateNavbar()
    {
        var canBack = _backStack.Count > 1;
        // 空字符串与 null 等价（HasLeftContent 判 IsNullOrEmpty）；用空串避免向非空属性赋 null
        _navbar.LeftIcon = canBack ? "back" : string.Empty;
        _navbar.LeftText = canBack ? "返回" : string.Empty;
    }

    private void OnThemeChanged(object? sender, EventArgs e)
    {
        ApplyPageBackground();
        _sidebar.BorderBrush = new SolidColorBrush(DocPalette.CardBorder);
    }

    private void ApplyPageBackground()
    {
        _root.Background = new SolidColorBrush(LhTheme.Current.BackgroundPage);
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        LhTheme.ThemeChanged -= OnThemeChanged;
    }
}
