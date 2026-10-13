using System;
using Microsoft.UI;
using Windows.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;
using Windows.Foundation;

namespace Top.Lhgb.Ui.Components.Button;

/// <summary>
/// 按钮组件：触发即时操作（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/button.json</c> 逐字对齐；行为遵循 <c>cases/button.json</c>
/// 的 8 条黄金用例（已翻译为 xUnit 测试，见 tests/LhButtonTests.cs）。
///
/// 主题：应用入口调 <see cref="LhTheme.Apply(bool)"/> 注入色板；本组件订阅
/// <see cref="LhTheme.ThemeChanged"/>，切换明暗时自动重算外观（主题热重绘，无需重建页面）。
/// </summary>
public sealed class LhButton : UserControl
{
    private readonly Grid _root;
    private readonly Rectangle _pattern;
    private readonly StackPanel _content;
    private readonly ProgressRing _spinner;
    private readonly TextBlock _iconText;
    private readonly TextBlock _label;

    /// <summary>点击回调；disabled 或 loading 为 true 时不触发</summary>
    public event Action? Click;

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type), typeof(LhButtonType), typeof(LhButton),
        new PropertyMetadata(LhButtonType.Default, OnAppearanceChanged));

    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size), typeof(LhButtonSize), typeof(LhButton),
        new PropertyMetadata(LhButtonSize.Medium, OnAppearanceChanged));

    public static readonly DependencyProperty DisabledProperty = DependencyProperty.Register(
        nameof(Disabled), typeof(bool), typeof(LhButton),
        new PropertyMetadata(false, OnAppearanceChanged));

    public static readonly DependencyProperty LoadingProperty = DependencyProperty.Register(
        nameof(Loading), typeof(bool), typeof(LhButton),
        new PropertyMetadata(false, OnAppearanceChanged));

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon), typeof(string), typeof(LhButton),
        new PropertyMetadata(null, OnAppearanceChanged));

    public static readonly DependencyProperty BgPatternProperty = DependencyProperty.Register(
        nameof(BgPattern), typeof(bool), typeof(LhButton),
        new PropertyMetadata(false, OnAppearanceChanged));

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(LhButton),
        new PropertyMetadata(string.Empty, OnTextChanged));

    /// <summary>按钮类型，决定底色与文字色</summary>
    public LhButtonType Type { get => (LhButtonType)GetValue(TypeProperty); set => SetValue(TypeProperty, value); }

    /// <summary>按钮尺寸，映射到 button.height / paddingHorizontal / fontSize / iconSize 令牌</summary>
    public LhButtonSize Size { get => (LhButtonSize)GetValue(SizeProperty); set => SetValue(SizeProperty, value); }

    /// <summary>是否禁用；禁用时不响应点击并呈现禁用样式（disabled.* 令牌）</summary>
    public bool Disabled { get => (bool)GetValue(DisabledProperty); set => SetValue(DisabledProperty, value); }

    /// <summary>是否加载中；加载中显示指示器且点击不触发 Click</summary>
    // new：契约 prop 名与 FrameworkElement.Loading 同名，刻意隐藏基类事件
    public new bool Loading { get => (bool)GetValue(LoadingProperty); set => SetValue(LoadingProperty, value); }

    /// <summary>图标名（本地图标体系，见 LhButtonTokens.IconFor），按钮内左侧绘制</summary>
    public string Icon { get => (string)GetValue(IconProperty); set => SetValue(IconProperty, value); }

    /// <summary>是否绘制国风花纹底纹（消费 color.decoration 令牌）</summary>
    public bool BgPattern { get => (bool)GetValue(BgPatternProperty); set => SetValue(BgPatternProperty, value); }

    /// <summary>按钮文字内容（spec 的 default 插槽）</summary>
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

    public LhButton()
    {
        _pattern = new Rectangle
        {
            IsHitTestVisible = false,
            Visibility = Visibility.Collapsed,
        };
        _spinner = new ProgressRing { IsActive = false, Visibility = Visibility.Collapsed };
        _iconText = new TextBlock { Visibility = Visibility.Collapsed };
        _label = new TextBlock { TextWrapping = TextWrapping.NoWrap, VerticalAlignment = VerticalAlignment.Center };
        _content = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Spacing = LhEpx.Px(LhTokens.Spacing.Xs),
        };
        _content.Children.Add(_spinner);
        _content.Children.Add(_iconText);
        _content.Children.Add(_label);
        _root = new Grid();
        _root.Children.Add(_pattern);
        _root.Children.Add(_content);
        Content = _root;

        Tapped += OnTapped;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;

        ApplyAppearance();
    }

    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhButton)d).ApplyAppearance();

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhButton)d;
        self._label.Text = e.NewValue as string ?? string.Empty;
    }

    private void OnTapped(object sender, TappedRoutedEventArgs e)
    {
        // 状态 gate：disabled 与 loading 均不触发（IsEnabled 已拦截，此处双保险）
        if (ResolveAppearance().Enabled)
        {
            Click?.Invoke();
        }
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhButtonAppearance ResolveAppearance() =>
        LhButtonTokens.Resolve(Type, Size, Disabled, Loading, BgPattern, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        Height = LhEpx.Px(a.Height);
        MinHeight = LhEpx.Px(a.Height);
        Padding = new Thickness(LhEpx.Px(a.PaddingHorizontal), 0, LhEpx.Px(a.PaddingHorizontal), 0);
        Background = new SolidColorBrush(a.ContainerColor);
        BorderBrush = new SolidColorBrush(a.BorderColor);
        BorderThickness = new Thickness(a.BorderWidth);
        CornerRadius = new CornerRadius(a.BorderRadius);
        IsEnabled = a.Enabled;

        var font = new FontFamily(LhTokens.Typography.FontFamily);
        var contentBrush = new SolidColorBrush(a.ContentColor);

        _label.FontFamily = font;
        _label.FontSize = a.FontSize;
        _label.Foreground = contentBrush;
        _label.FontWeight = LhFontWeight.FromToken(LhTokens.Button.FontWeight);

        _spinner.Width = LhEpx.Px(a.IconSize);
        _spinner.Height = LhEpx.Px(a.IconSize);
        _spinner.Foreground = contentBrush;
        _spinner.IsActive = Loading;
        _spinner.Visibility = Loading ? Visibility.Visible : Visibility.Collapsed;

        var glyph = LhButtonTokens.IconFor(Icon);
        if (glyph is not null)
        {
            _iconText.Text = glyph;
            _iconText.FontFamily = font;
            _iconText.FontSize = a.IconSize;
            _iconText.Foreground = contentBrush;
            _iconText.VerticalAlignment = VerticalAlignment.Center;
            _iconText.Visibility = Loading ? Visibility.Collapsed : Visibility.Visible;
        }
        else
        {
            _iconText.Visibility = Visibility.Collapsed;
        }

        if (a.PatternColor is { } patternColor)
        {
            _pattern.Fill = CreatePatternBrush(patternColor);
            _pattern.Visibility = Visibility.Visible;
        }
        else
        {
            _pattern.Visibility = Visibility.Collapsed;
        }
    }

    /// <summary>国风花纹底纹：45° 斜纹叠加（消费 color.decoration 令牌），与 web 血脉方案对齐</summary>
    private static LinearGradientBrush CreatePatternBrush(Color color)
    {
        var brush = new LinearGradientBrush
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(0.2, 0.2),
            MappingMode = BrushMappingMode.RelativeToBoundingBox,
            SpreadMethod = GradientSpreadMethod.Repeat,
        };
        brush.GradientStops.Add(new GradientStop { Offset = 0.0, Color = color });
        brush.GradientStops.Add(new GradientStop { Offset = 0.5, Color = LhColor.Transparent });
        return brush;
    }
}
