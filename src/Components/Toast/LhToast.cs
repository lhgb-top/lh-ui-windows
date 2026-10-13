using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Toast;

/// <summary>
/// 宿主调用句柄：组件在 Loaded 时把自身的 Show/Hide 注册到控制器上，宿主即可
/// 在不持有组件实例的情况下触发显示/隐藏。WinUI 的 XAML 树不提供子组件的便利引用，
/// 这是指令式组件在 XAML 中被宿主驱动的推荐接法；不传入时组件仍可独立使用。
/// </summary>
public sealed class LhToastController
{
    /// <summary>显示提示文字并重置自动隐藏计时</summary>
    public Action<string>? Show { get; set; }

    /// <summary>立即隐藏但不触发 OnClose</summary>
    public Action? Hide { get; set; }
}

/// <summary>
/// 轻提示控件：自持可见性与计时的墨色胶囊浮层（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/toast.json</c> 逐字对齐；行为遵循 <c>cases/toast.json</c>
/// 的 6 条黄金用例（已翻译为 xUnit 测试，见 tests/LhToastTests.cs）。
///
/// 指令式接口（spec 的 show / hide / tick）在端侧落点为方法 + 内部计时：
/// - Show(text) 使其可见并重置计时
/// - Hide() 立即隐藏但不触发 OnClose
/// - 计时由 DispatcherTimer 承载（等价于每帧 tick 推进），到期自动隐藏并触发 OnClose
///
/// 宿主须将其作为页面 Grid 的最后子节点挂载（层级 zIndex.toast，命中事件穿透）。
/// </summary>
public sealed class LhToast : UserControl
{
    private readonly Grid _root;
    private readonly Border _capsule;
    private readonly Ellipse _dot;
    private readonly TextBlock _label;
    private readonly DispatcherTimer _timer;

    /// <summary>计时到期自动隐藏时触发；Hide() 主动隐藏不触发</summary>
    public event Action? Close;

    /// <summary>语义类型；非 default 时在文字前绘制对应语义色圆点</summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type), typeof(LhToastType), typeof(LhToast),
        new PropertyMetadata(LhToastType.Default, OnAppearanceChanged));

    /// <summary>自动隐藏时长（毫秒）</summary>
    public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
        nameof(Duration), typeof(int), typeof(LhToast),
        new PropertyMetadata(2000));

    /// <summary>在可用区域内的定位；上下两端留 spacing.xl 边距</summary>
    public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
        nameof(Position), typeof(LhToastPosition), typeof(LhToast),
        new PropertyMetadata(LhToastPosition.Center, OnPositionChanged));

    /// <summary>宿主控制器（可选）</summary>
    public static readonly DependencyProperty ControllerProperty = DependencyProperty.Register(
        nameof(Controller), typeof(LhToastController), typeof(LhToast),
        new PropertyMetadata(null, OnControllerChanged));

    public LhToastType Type { get => (LhToastType)GetValue(TypeProperty); set => SetValue(TypeProperty, value); }
    public int Duration { get => (int)GetValue(DurationProperty); set => SetValue(DurationProperty, value); }
    public LhToastPosition Position { get => (LhToastPosition)GetValue(PositionProperty); set => SetValue(PositionProperty, value); }
    public LhToastController Controller { get => (LhToastController)GetValue(ControllerProperty); set => SetValue(ControllerProperty, value); }

    /// <summary>内部可见态（由 Show/Hide 维护，不可见时不渲染任何内容）</summary>
    private bool _visible;

    public LhToast()
    {
        _dot = new Ellipse { Visibility = Visibility.Collapsed };
        _label = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            TextWrapping = TextWrapping.NoWrap,
            MaxLines = 2,
        };

        var content = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };
        content.Children.Add(_dot);
        content.Children.Add(_label);

        _capsule = new Border { Child = content };
        _root = new Grid();
        _root.Children.Add(_capsule);
        // 命中事件穿透：浮层本身不拦截点击
        IsHitTestVisible = false;
        Content = _root;

        _timer = new DispatcherTimer();
        _timer.Tick += OnTimerTick;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
        UpdateVisibility();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhToastAppearance ResolveAppearance() =>
        LhToastTokens.Resolve(Type, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        // 墨色胶囊：底取 color.ink，文字取 color.onPrimary，圆角取 toast.borderRadius
        _capsule.Background = new SolidColorBrush(a.ContainerColor);
        _capsule.CornerRadius = new CornerRadius(a.BorderRadius);
        _capsule.Padding = new Thickness(
            LhEpx.Px(a.PaddingHorizontal),
            LhEpx.Px(a.PaddingVertical),
            LhEpx.Px(a.PaddingHorizontal),
            LhEpx.Px(a.PaddingVertical));
        _capsule.MaxWidth = ActualWidth * a.MaxWidthPercent / 100;

        var font = new FontFamily(LhTokens.Typography.FontFamily);
        _label.FontFamily = font;
        _label.FontSize = LhEpx.Px(a.FontSize);
        _label.Foreground = new SolidColorBrush(a.ContentColor);

        // type 非 default 时在文字前绘制直径为字号的语义色圆点
        if (a.DotColor is { } dotColor)
        {
            _dot.Fill = new SolidColorBrush(dotColor);
            _dot.Width = LhEpx.Px(a.DotSize);
            _dot.Height = LhEpx.Px(a.DotSize);
            _dot.Visibility = Visibility.Visible;
            _dot.Margin = new Thickness(0, 0, LhEpx.Px(a.Gap), 0);
        }
        else
        {
            _dot.Visibility = Visibility.Collapsed;
        }

        Canvas.SetZIndex(_root, a.ZIndex);
        ApplyPosition(a);
    }

    /// <summary>position → 在可用区域内的定位；上下两端留 spacing.xl 边距（center 时无留白）</summary>
    private void ApplyPosition(LhToastAppearance a)
    {
        var margin = LhEpx.Px(a.EdgeMargin);
        _capsule.HorizontalAlignment = HorizontalAlignment.Center;
        _capsule.VerticalAlignment = Position switch
        {
            LhToastPosition.Top => VerticalAlignment.Top,
            LhToastPosition.Bottom => VerticalAlignment.Bottom,
            _ => VerticalAlignment.Center,
        };
        _root.Padding = Position switch
        {
            LhToastPosition.Top => new Thickness(0, margin, 0, 0),
            LhToastPosition.Bottom => new Thickness(0, 0, 0, margin),
            _ => new Thickness(0),
        };
    }

    /// <summary>不可见时不渲染任何内容（黄金用例 hidden-emits-no-commands）</summary>
    private void UpdateVisibility()
    {
        Visibility = _visible ? Visibility.Visible : Visibility.Collapsed;
    }

    /// <summary>Show(text)：使其可见并重置计时（黄金用例 show-makes-visible）</summary>
    public void Show(string text)
    {
        _label.Text = text;
        _visible = true;
        UpdateVisibility();

        _timer.Stop();
        _timer.Interval = TimeSpan.FromMilliseconds(Duration);
        _timer.Start();
    }

    /// <summary>Hide()：立即隐藏但不触发 Close（黄金用例 hide-is-silent）</summary>
    public void Hide()
    {
        _timer.Stop();
        _visible = false;
        UpdateVisibility();
    }

    private void OnTimerTick(object? sender, object e)
    {
        // 计时到期：自动隐藏并触发 Close（黄金用例 auto-hides-after-duration）
        _timer.Stop();
        _visible = false;
        UpdateVisibility();
        Close?.Invoke();
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        LhTheme.ThemeChanged += OnThemeChanged;
        RegisterController();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        LhTheme.ThemeChanged -= OnThemeChanged;
        _timer.Stop();
    }

    /// <summary>把自身的 Show/Hide 注册到宿主控制器（Loaded 后生效）</summary>
    private void RegisterController()
    {
        if (Controller is { } controller)
        {
            controller.Show = Show;
            controller.Hide = Hide;
        }
    }

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhToast)d).ApplyAppearance();

    private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhToast)d).ApplyAppearance();

    private static void OnControllerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhToast)d;
        if (self.IsLoaded)
        {
            self.RegisterController();
        }
    }
}
