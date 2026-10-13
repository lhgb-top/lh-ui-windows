using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Radio;

/// <summary>
/// 单选框控件：圆环 + 内圆点，点击命中整行（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/radio.json</c> 逐字对齐；行为遵循 <c>cases/radio.json</c>
/// 的 6 条黄金用例（已翻译为 xUnit 测试，见 tests/LhRadioTests.cs）。
///
/// 组件本身不含组逻辑：宿主维护单选组，监听 <see cref="Change"/> 后把同组其它项置为未选中。
/// </summary>
public sealed class LhRadio : UserControl
{
    private readonly Ellipse _ring;
    private readonly Ellipse _dot;
    private readonly TextBlock _label;

    /// <summary>选中态变化回调；仅未选中且未禁用时触发（payload 恒为 true）</summary>
    public event Action<bool>? Change;

    public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register(
        nameof(Checked), typeof(bool), typeof(LhRadio),
        new PropertyMetadata(false, OnAppearanceChanged));

    public static readonly DependencyProperty DisabledProperty = DependencyProperty.Register(
        nameof(Disabled), typeof(bool), typeof(LhRadio),
        new PropertyMetadata(false, OnAppearanceChanged));

    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
        nameof(Label), typeof(string), typeof(LhRadio),
        new PropertyMetadata(string.Empty, OnLabelChanged));

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(string), typeof(LhRadio),
        new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size), typeof(LhRadioSize), typeof(LhRadio),
        new PropertyMetadata(LhRadioSize.Medium, OnAppearanceChanged));

    public bool Checked { get => (bool)GetValue(CheckedProperty); set => SetValue(CheckedProperty, value); }
    public bool Disabled { get => (bool)GetValue(DisabledProperty); set => SetValue(DisabledProperty, value); }
    public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
    public string Value { get => (string)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
    public LhRadioSize Size { get => (LhRadioSize)GetValue(SizeProperty); set => SetValue(SizeProperty, value); }

    public LhRadio()
    {
        _ring = new Ellipse { IsHitTestVisible = false };
        _dot = new Ellipse
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            IsHitTestVisible = false,
        };
        _label = new TextBlock { VerticalAlignment = VerticalAlignment.Center };

        var ringHost = new Grid();
        ringHost.Children.Add(_ring);
        ringHost.Children.Add(_dot);

        var panel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
        };
        panel.Children.Add(ringHost);
        panel.Children.Add(_label);
        Content = panel;

        Tapped += OnTapped;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhRadioAppearance ResolveAppearance() =>
        LhRadioTokens.Resolve(Checked, Disabled, Size, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        _ring.Width = LhEpx.Px(a.Size);
        _ring.Height = LhEpx.Px(a.Size);
        _ring.Stroke = new SolidColorBrush(a.RingColor);
        _ring.StrokeThickness = a.RingWidth;
        _ring.Margin = new Thickness(0, 0, LhEpx.Px(a.Gap), 0);

        // 选中时内部画实心圆点，直径 = 尺寸 × dotScale
        _dot.Width = LhEpx.Px(a.DotSize);
        _dot.Height = LhEpx.Px(a.DotSize);
        _dot.Fill = new SolidColorBrush(a.DotColor);
        _dot.Visibility = a.ShowDot ? Visibility.Visible : Visibility.Collapsed;

        _label.Text = Label;
        _label.FontFamily = new FontFamily(LhTokens.Typography.FontFamily);
        _label.FontSize = LhEpx.Px(LhTokens.Typography.FontSize.Base);
        _label.Foreground = new SolidColorBrush(a.LabelColor);

        IsEnabled = a.Enabled;
    }

    /// <summary>点击命中整行：仅当未选中且未禁用时变为选中并触发 onChange(checked=true)</summary>
    private void OnTapped(object sender, TappedRoutedEventArgs e)
    {
        var a = ResolveAppearance();
        if (!a.Enabled || Checked)
        {
            return;
        }
        Checked = true;
        Change?.Invoke(true);
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhRadio)d).ApplyAppearance();

    private static void OnLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhRadio)d;
        self._label.Text = e.NewValue as string ?? string.Empty;
    }
}
