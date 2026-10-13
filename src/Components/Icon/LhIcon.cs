using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Icon;

/// <summary>
/// 图标控件：把 <see cref="LhIconTokens"/> 解析出的外观（尺寸/色调/字形）画成一个字形文本。
///
/// 属性与 <c>lh-ui-core/specs/icon.json</c> 逐字对齐；行为遵循 <c>cases/icon.json</c>
/// 的 5 条黄金用例（已翻译为 xUnit 测试，见 tests/LhIconTests.cs）。
///
/// 主题：订阅 <see cref="LhTheme.ThemeChanged"/>，切明暗时自动重算色调（主题热重绘）。
/// </summary>
public sealed class LhIcon : UserControl
{
    private readonly TextBlock _glyph;

    /// <summary>图标尺寸</summary>
    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size), typeof(LhIconSize), typeof(LhIcon),
        new PropertyMetadata(LhIconSize.Medium, OnVisualPropertyChanged));

    /// <summary>图标色调（决定文字色）</summary>
    public static readonly DependencyProperty ToneProperty = DependencyProperty.Register(
        nameof(Tone), typeof(LhIconTone), typeof(LhIcon),
        new PropertyMetadata(LhIconTone.Regular, OnVisualPropertyChanged));

    /// <summary>图标名（本地图标表解析；未登记名回落通用字形）</summary>
    // new：契约 prop 名与 FrameworkElement.Name 同名，按家族事件/属性命名约定（去 on、PascalCase）刻意隐藏基类成员
    public static new readonly DependencyProperty NameProperty = DependencyProperty.Register(
        nameof(Name), typeof(string), typeof(LhIcon),
        new PropertyMetadata(string.Empty, OnVisualPropertyChanged));

    public LhIconSize Size { get => (LhIconSize)GetValue(SizeProperty); set => SetValue(SizeProperty, value); }
    public LhIconTone Tone { get => (LhIconTone)GetValue(ToneProperty); set => SetValue(ToneProperty, value); }
    public new string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }

    public LhIcon()
    {
        _glyph = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontFamily = new FontFamily(LhTokens.Typography.FontFamily),
        };
        Content = _glyph;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhIconAppearance ResolveAppearance() =>
        LhIconTokens.Resolve(Size, Tone, Name, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();
        Width = LhEpx.Px(a.Size);
        Height = LhEpx.Px(a.Size);
        _glyph.FontSize = LhEpx.Px(a.Size);
        _glyph.Foreground = new SolidColorBrush(a.Color);
        _glyph.Text = a.Glyph;
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnVisualPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhIcon)d).ApplyAppearance();
}
