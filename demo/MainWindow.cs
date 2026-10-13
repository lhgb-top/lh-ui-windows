using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Button;
using Top.Lhgb.Ui.Components.Navbar;
using Top.Lhgb.Ui.Components.Dialog;
using Top.Lhgb.Ui.Components.Picker;
using Top.Lhgb.Ui.Components.Toast;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Demo;

/// <summary>
/// 演示应用主窗口：左侧组件分栏导航 + 右侧滚动演示区 + 浮层宿主。
///
/// 浮层（Dialog / Picker / Toast）作为根 Grid 的最后子节点挂载，跨行铺满；
/// Toast 在最后，渲染层级最高（zIndex.toast &gt; zIndex.modal）。指令式组件的隐藏
/// 由宿主在 Close 事件里完成（组件只负责触发 onClose，见各组件的 OnConfirmClick）。
/// </summary>
public sealed class MainWindow : Window
{
    private readonly Grid _root;
    private readonly ScrollViewer _contentHost;
    private readonly LhDialog _dialog;
    private readonly LhPicker _picker;
    private readonly LhToast _toast;

    private Action<int>? _pickerConfirm;

    public MainWindow()
    {
        Title = "lh-ui-windows 演示";

        _root = new Grid();
        _root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        _root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        var navbar = new LhNavbar
        {
            Title = "lh-ui-windows 演示",
            RightText = "切换主题",
            Bordered = true,
        };
        navbar.RightClick += OnToggleTheme;
        Grid.SetRow(navbar, 0);
        _root.Children.Add(navbar);

        var body = new Grid();
        Grid.SetRow(body, 1);
        body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(LhEpx.Px(180)) });
        body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        var nav = new ListView
        {
            ItemsSource = DemoSections.Names,
            SelectionMode = ListViewSelectionMode.Single,
            IsItemClickEnabled = false,
        };
        nav.SelectionChanged += OnSelectionChanged;
        Grid.SetColumn(nav, 0);
        body.Children.Add(nav);

        _contentHost = new ScrollViewer
        {
            Padding = new Thickness(LhEpx.Px(LhTokens.Spacing.Lg)),
        };
        Grid.SetColumn(_contentHost, 1);
        body.Children.Add(_contentHost);
        _root.Children.Add(body);

        // —— 浮层宿主：行 0 起、跨两行铺满 ——
        _dialog = new LhDialog { Closable = true };
        _dialog.Confirm += () => ShowToast("已确认", LhToastType.Success);
        _dialog.Close += () => _dialog.Visible = false;
        Grid.SetRow(_dialog, 0);
        Grid.SetRowSpan(_dialog, 2);
        _root.Children.Add(_dialog);

        _picker = new LhPicker { Title = "请选择" };
        _picker.Confirm += index => _pickerConfirm?.Invoke(index);
        _picker.Close += () => _picker.Visible = false;
        Grid.SetRow(_picker, 0);
        Grid.SetRowSpan(_picker, 2);
        _root.Children.Add(_picker);

        _toast = new LhToast();
        Grid.SetRow(_toast, 0);
        Grid.SetRowSpan(_toast, 2);
        _root.Children.Add(_toast);

        Content = _root;

        // Window 没有 Loaded 事件（那是 FrameworkElement 的）；主题监听在此直接挂上，
        // 页面底色同步见 OnThemeChanged。组件自身订阅 ThemeChanged 热重绘。
        LhTheme.ThemeChanged += OnThemeChanged;
        ApplyPageBackground();

        nav.SelectedIndex = 0;
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyPageBackground();

    private void ApplyPageBackground()
    {
        _root.Background = new SolidColorBrush(LhTheme.Current.BackgroundPage);
    }

    private void OnToggleTheme()
    {
        LhTheme.Apply(!LhTheme.IsDark);
        ShowToast(LhTheme.IsDark ? "已切换暗色" : "已切换明色", LhToastType.Info);
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListView nav || nav.SelectedIndex < 0) return;

        var ctx = new DemoContext
        {
            ShowToast = ShowToast,
            ShowDialog = ShowDialog,
            ShowPicker = ShowPicker,
        };
        _contentHost.Content = DemoSections.Build(nav.SelectedIndex, ctx);
    }

    private void ShowToast(string text, LhToastType type)
    {
        _toast.Type = type;
        _toast.Show(text);
    }

    private void ShowDialog(string title, string content, LhDialogType type)
    {
        _dialog.Title = title;
        _dialog.Content = content;
        _dialog.Type = type;
        _dialog.Visible = true;
    }

    private void ShowPicker(string[] options, Action<int> onConfirm)
    {
        _picker.Options = options;
        _picker.Value = 0;
        _pickerConfirm = onConfirm;
        _picker.Visible = true;
    }
}
