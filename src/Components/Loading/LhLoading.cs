using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Loading;

/// <summary>
/// 加载指示器控件：环形旋转指示器（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/loading.json</c> 逐字对齐；行为遵循 <c>cases/loading.json</c>
/// 的 5 条黄金用例（已翻译为 xUnit 测试，见 tests/LhLoadingTests.cs）。
///
/// 旋转由 WinUI 的 ProgressRing 平台动画承担（朴素调性：只发几何，动画由平台补齐）。
/// </summary>
public sealed class LhLoading : UserControl
{
    private readonly ProgressRing _ring;

    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size), typeof(LhLoadingSize), typeof(LhLoading),
        new PropertyMetadata(LhLoadingSize.Medium, OnAppearanceChanged));

    public static readonly DependencyProperty ToneProperty = DependencyProperty.Register(
        nameof(Tone), typeof(LhLoadingTone), typeof(LhLoading),
        new PropertyMetadata(LhLoadingTone.Primary, OnAppearanceChanged));

    public LhLoadingSize Size { get => (LhLoadingSize)GetValue(SizeProperty); set => SetValue(SizeProperty, value); }
    public LhLoadingTone Tone { get => (LhLoadingTone)GetValue(ToneProperty); set => SetValue(ToneProperty, value); }

    public LhLoading()
    {
        _ring = new ProgressRing
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            IsActive = true,
        };
        Content = _ring;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhLoadingAppearance ResolveAppearance() =>
        LhLoadingTokens.Resolve(Size, Tone, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();
        // draw_ring：半径为尺寸一半，描边宽 loading.strokeWidth
        Width = LhEpx.Px(a.Size);
        Height = LhEpx.Px(a.Size);
        _ring.Width = LhEpx.Px(a.Size);
        _ring.Height = LhEpx.Px(a.Size);
        _ring.Foreground = new SolidColorBrush(a.Color);
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhLoading)d).ApplyAppearance();
}
