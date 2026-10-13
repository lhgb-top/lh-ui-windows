using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Switch;

/// <summary>
/// 开关控件：胶囊轨道 + 圆形滑块，点击切换（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/switch.json</c> 逐字对齐；行为遵循 <c>cases/switch.json</c>
/// 的 6 条黄金用例（已翻译为 xUnit 测试，见 tests/LhSwitchTests.cs）。
/// </summary>
public sealed class LhSwitch : UserControl
{
    private readonly Border _track;
    private readonly Ellipse _thumb;
    private readonly ProgressRing _loadingRing;

    /// <summary>开关态变化回调；disabled 或 loading 时不触发</summary>
    public event Action<bool>? Change;

    public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register(
        nameof(Checked), typeof(bool), typeof(LhSwitch),
        new PropertyMetadata(false, OnAppearanceChanged));

    public static readonly DependencyProperty DisabledProperty = DependencyProperty.Register(
        nameof(Disabled), typeof(bool), typeof(LhSwitch),
        new PropertyMetadata(false, OnAppearanceChanged));

    public static readonly DependencyProperty LoadingProperty = DependencyProperty.Register(
        nameof(Loading), typeof(bool), typeof(LhSwitch),
        new PropertyMetadata(false, OnAppearanceChanged));

    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size), typeof(LhSwitchSize), typeof(LhSwitch),
        new PropertyMetadata(LhSwitchSize.Medium, OnAppearanceChanged));

    public bool Checked { get => (bool)GetValue(CheckedProperty); set => SetValue(CheckedProperty, value); }
    public bool Disabled { get => (bool)GetValue(DisabledProperty); set => SetValue(DisabledProperty, value); }
    // new：契约 prop 名与 FrameworkElement.Loading 同名，刻意隐藏基类事件
    public new bool Loading { get => (bool)GetValue(LoadingProperty); set => SetValue(LoadingProperty, value); }
    public LhSwitchSize Size { get => (LhSwitchSize)GetValue(SizeProperty); set => SetValue(SizeProperty, value); }

    public LhSwitch()
    {
        _thumb = new Ellipse { IsHitTestVisible = false };
        _loadingRing = new ProgressRing { IsHitTestVisible = false };
        _track = new Border();
        _track.Child = CreateThumbHost();

        Content = _track;

        Tapped += OnTapped;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
    }

    /// <summary>滑块/加载环的承载层：绝对定位在轨道内</summary>
    private Grid CreateThumbHost()
    {
        var host = new Grid();
        host.Children.Add(_thumb);
        host.Children.Add(_loadingRing);
        return host;
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhSwitchAppearance ResolveAppearance() =>
        LhSwitchTokens.Resolve(Checked, Disabled, Loading, Size, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        Width = LhEpx.Px(a.Width);
        Height = LhEpx.Px(a.Height);
        _track.Width = LhEpx.Px(a.Width);
        _track.Height = LhEpx.Px(a.Height);
        _track.Background = new SolidColorBrush(a.TrackColor);
        _track.CornerRadius = new CornerRadius(a.BorderRadius);
        _track.Padding = new Thickness(0);

        // 滑块贴左/贴右（水平偏移由令牌算出）
        _thumb.Width = LhEpx.Px(a.ThumbSize);
        _thumb.Height = LhEpx.Px(a.ThumbSize);
        _thumb.Fill = new SolidColorBrush(a.ThumbColor);
        _thumb.HorizontalAlignment = HorizontalAlignment.Left;
        _thumb.VerticalAlignment = VerticalAlignment.Center;
        _thumb.Margin = new Thickness(LhEpx.Px(a.ThumbOffset), 0, 0, 0);
        _thumb.Visibility = Loading ? Visibility.Collapsed : Visibility.Visible;

        // loading 时滑块位置改画加载环（主色）
        _loadingRing.Width = LhEpx.Px(a.ThumbSize);
        _loadingRing.Height = LhEpx.Px(a.ThumbSize);
        _loadingRing.Foreground = new SolidColorBrush(a.LoadingColor);
        _loadingRing.IsActive = Loading;
        _loadingRing.HorizontalAlignment = HorizontalAlignment.Left;
        _loadingRing.VerticalAlignment = VerticalAlignment.Center;
        _loadingRing.Margin = new Thickness(LhEpx.Px(a.ThumbOffset), 0, 0, 0);
        _loadingRing.Visibility = Loading ? Visibility.Visible : Visibility.Collapsed;

        IsEnabled = a.Enabled;
    }

    private void OnTapped(object sender, TappedRoutedEventArgs e)
    {
        if (!ResolveAppearance().Enabled)
        {
            return;
        }
        Checked = !Checked;
        Change?.Invoke(Checked);
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhSwitch)d).ApplyAppearance();
}
