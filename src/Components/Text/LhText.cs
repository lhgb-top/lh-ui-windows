using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Text;

/// <summary>
/// 文本控件：按级别/字重/色调/对齐渲染单段文字（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/text.json</c> 逐字对齐；行为遵循 <c>cases/text.json</c>
/// 的 5 条黄金用例（已翻译为 xUnit 测试，见 tests/LhTextTests.cs）。
///
/// 主题：订阅 <see cref="LhTheme.ThemeChanged"/>，切明暗时自动重算文字色（主题热重绘）。
/// </summary>
public sealed class LhText : UserControl
{
    private readonly TextBlock _text;

    /// <summary>字号级别，映射到 typography.fontSize.* 令牌</summary>
    public static readonly DependencyProperty LevelProperty = DependencyProperty.Register(
        nameof(Level), typeof(LhTextLevel), typeof(LhText),
        new PropertyMetadata(LhTextLevel.Base, OnAppearanceChanged));

    /// <summary>字重，映射到 typography.fontWeight.* 令牌</summary>
    public static readonly DependencyProperty WeightProperty = DependencyProperty.Register(
        nameof(Weight), typeof(LhTextWeight), typeof(LhText),
        new PropertyMetadata(LhTextWeight.Primary, OnAppearanceChanged));

    /// <summary>文字色调，映射到 color.text.* / color.selected</summary>
    public static readonly DependencyProperty ToneProperty = DependencyProperty.Register(
        nameof(Tone), typeof(LhTextTone), typeof(LhText),
        new PropertyMetadata(LhTextTone.Regular, OnAppearanceChanged));

    /// <summary>水平对齐（非设计令牌，映射到框架枚举）</summary>
    public static readonly DependencyProperty AlignProperty = DependencyProperty.Register(
        nameof(Align), typeof(LhTextAlign), typeof(LhText),
        new PropertyMetadata(LhTextAlign.Start, OnAppearanceChanged));

    /// <summary>文字内容（spec 的 default 插槽）</summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(LhText),
        new PropertyMetadata(string.Empty, OnTextChanged));

    public LhTextLevel Level { get => (LhTextLevel)GetValue(LevelProperty); set => SetValue(LevelProperty, value); }
    public LhTextWeight Weight { get => (LhTextWeight)GetValue(WeightProperty); set => SetValue(WeightProperty, value); }
    public LhTextTone Tone { get => (LhTextTone)GetValue(ToneProperty); set => SetValue(ToneProperty, value); }
    public LhTextAlign Align { get => (LhTextAlign)GetValue(AlignProperty); set => SetValue(AlignProperty, value); }
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

    public LhText()
    {
        _text = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Top,
            TextWrapping = TextWrapping.Wrap,
        };
        Content = _text;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhTextAppearance ResolveAppearance() =>
        LhTextTokens.Resolve(Level, Weight, Tone, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        _text.Text = Text;
        _text.FontFamily = new FontFamily(LhTokens.Typography.FontFamily);
        _text.FontSize = LhEpx.Px(a.FontSize);
        _text.FontWeight = LhFontWeight.FromToken(a.FontWeight);
        _text.Foreground = new SolidColorBrush(a.FontColor);
        _text.TextAlignment = LhTextTokens.AlignFor(Align);
        _text.LineHeight = LhEpx.Px(a.FontSize * a.LineHeight);
        _text.IsColorFontEnabled = false;
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhText)d).ApplyAppearance();

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhText)d;
        self._text.Text = e.NewValue as string ?? string.Empty;
    }
}
