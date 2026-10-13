using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Tag;

/// <summary>
/// 标签控件：小体积状态标记，支持语义类型、尺寸、描边/浅底形态、圆角胶囊与可关闭。
///
/// 属性与 <c>lh-ui-core/specs/tag.json</c> 逐字对齐；行为遵循 <c>cases/tag.json</c>
/// 的 6 条黄金用例（已翻译为 xUnit 测试，见 tests/LhTagTests.cs）。
///
/// 主题：订阅 <see cref="LhTheme.ThemeChanged"/>，切明暗时自动重算类型色（主题热重绘）。
/// </summary>
public sealed class LhTag : UserControl
{
    private readonly Border _container;
    private readonly StackPanel _content;
    private readonly TextBlock _label;
    private readonly TextBlock _closeMark;

    /// <summary>点击关闭符时触发（仅 <see cref="Closable"/> 为 true 时显示关闭符）</summary>
    public event Action? Close;

    /// <summary>标签类型，决定底色与文字色</summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type), typeof(LhTagType), typeof(LhTag),
        new PropertyMetadata(LhTagType.Default, OnAppearanceChanged));

    /// <summary>标签尺寸，映射到 tag.height / paddingHorizontal / fontSize 令牌</summary>
    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size), typeof(LhTagSize), typeof(LhTag),
        new PropertyMetadata(LhTagSize.Medium, OnAppearanceChanged));

    /// <summary>是否为描边（plain）样式：语义浅底 + 语义字 + 语义描边</summary>
    public static readonly DependencyProperty PlainProperty = DependencyProperty.Register(
        nameof(Plain), typeof(bool), typeof(LhTag),
        new PropertyMetadata(false, OnAppearanceChanged));

    /// <summary>是否为圆角胶囊（round）样式：圆角取 height / 2</summary>
    public static readonly DependencyProperty RoundProperty = DependencyProperty.Register(
        nameof(Round), typeof(bool), typeof(LhTag),
        new PropertyMetadata(false, OnAppearanceChanged));

    /// <summary>是否显示关闭符；点击关闭符触发 <see cref="Close"/></summary>
    public static readonly DependencyProperty ClosableProperty = DependencyProperty.Register(
        nameof(Closable), typeof(bool), typeof(LhTag),
        new PropertyMetadata(false, OnAppearanceChanged));

    /// <summary>标签文字内容（spec 的 default 插槽）</summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(LhTag),
        new PropertyMetadata(string.Empty, OnTextChanged));

    public LhTagType Type { get => (LhTagType)GetValue(TypeProperty); set => SetValue(TypeProperty, value); }
    public LhTagSize Size { get => (LhTagSize)GetValue(SizeProperty); set => SetValue(SizeProperty, value); }
    public bool Plain { get => (bool)GetValue(PlainProperty); set => SetValue(PlainProperty, value); }
    public bool Round { get => (bool)GetValue(RoundProperty); set => SetValue(RoundProperty, value); }
    public bool Closable { get => (bool)GetValue(ClosableProperty); set => SetValue(ClosableProperty, value); }
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

    public LhTag()
    {
        _label = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            TextWrapping = TextWrapping.NoWrap,
        };
        // 关闭符单独命中：自身的 Tapped 拦截冒泡（e.Handled），不触发标签整体点击
        _closeMark = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            Text = "✕",
            Visibility = Visibility.Collapsed,
            IsHitTestVisible = true,
        };
        _closeMark.Tapped += OnCloseTapped;

        _content = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };
        _content.Children.Add(_label);
        _content.Children.Add(_closeMark);
        _container = new Border { Child = _content };
        Content = _container;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhTagAppearance ResolveAppearance() =>
        LhTagTokens.Resolve(Type, Size, Plain, Round, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        Height = LhEpx.Px(a.Height);
        MinHeight = LhEpx.Px(a.Height);
        Padding = new Thickness(LhEpx.Px(a.PaddingHorizontal), 0, LhEpx.Px(a.PaddingHorizontal), 0);
        _container.Background = new SolidColorBrush(a.ContainerColor);
        _container.BorderBrush = new SolidColorBrush(a.BorderColor);
        _container.BorderThickness = new Thickness(a.BorderWidth);
        _container.CornerRadius = new CornerRadius(a.BorderRadius);
        _content.Spacing = LhEpx.Px(a.CloseGap);

        _label.Text = Text;
        _label.FontFamily = new FontFamily(LhTokens.Typography.FontFamily);
        _label.FontSize = LhEpx.Px(a.FontSize);
        _label.Foreground = new SolidColorBrush(a.ContentColor);

        _closeMark.Visibility = Closable ? Visibility.Visible : Visibility.Collapsed;
        _closeMark.FontFamily = new FontFamily(LhTokens.Typography.FontFamily);
        _closeMark.FontSize = LhEpx.Px(a.FontSize);
        _closeMark.Foreground = new SolidColorBrush(a.ContentColor);
    }

    /// <summary>关闭符点击：触发 Close 并拦截冒泡，避免命中标签本体</summary>
    private void OnCloseTapped(object sender, TappedRoutedEventArgs e)
    {
        e.Handled = true;
        Close?.Invoke();
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhTag)d).ApplyAppearance();

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhTag)d;
        self._label.Text = e.NewValue as string ?? string.Empty;
    }
}
