using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Checkbox;

/// <summary>
/// 复选框控件：选中画勾、不确定画横杠，点击命中整行（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/checkbox.json</c> 逐字对齐；行为遵循 <c>cases/checkbox.json</c>
/// 的 6 条黄金用例（已翻译为 xUnit 测试，见 tests/LhCheckboxTests.cs）。
/// </summary>
public sealed class LhCheckbox : UserControl
{
    private readonly Border _box;
    private readonly TextBlock _mark;
    private readonly TextBlock _label;

    /// <summary>选中态变化回调；disabled 时不触发</summary>
    public event Action<bool>? Change;

    public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register(
        nameof(Checked), typeof(bool), typeof(LhCheckbox),
        new PropertyMetadata(false, OnAppearanceChanged));

    public static readonly DependencyProperty IndeterminateProperty = DependencyProperty.Register(
        nameof(Indeterminate), typeof(bool), typeof(LhCheckbox),
        new PropertyMetadata(false, OnAppearanceChanged));

    public static readonly DependencyProperty DisabledProperty = DependencyProperty.Register(
        nameof(Disabled), typeof(bool), typeof(LhCheckbox),
        new PropertyMetadata(false, OnAppearanceChanged));

    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
        nameof(Label), typeof(string), typeof(LhCheckbox),
        new PropertyMetadata(string.Empty, OnLabelChanged));

    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size), typeof(LhCheckboxSize), typeof(LhCheckbox),
        new PropertyMetadata(LhCheckboxSize.Medium, OnAppearanceChanged));

    public bool Checked { get => (bool)GetValue(CheckedProperty); set => SetValue(CheckedProperty, value); }
    public bool Indeterminate { get => (bool)GetValue(IndeterminateProperty); set => SetValue(IndeterminateProperty, value); }
    public bool Disabled { get => (bool)GetValue(DisabledProperty); set => SetValue(DisabledProperty, value); }
    public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
    public LhCheckboxSize Size { get => (LhCheckboxSize)GetValue(SizeProperty); set => SetValue(SizeProperty, value); }

    public LhCheckbox()
    {
        _mark = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            IsHitTestVisible = false,
        };
        _box = new Border { Child = _mark };
        _label = new TextBlock { VerticalAlignment = VerticalAlignment.Center };

        var panel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
        };
        panel.Children.Add(_box);
        panel.Children.Add(_label);
        Content = panel;

        Tapped += OnTapped;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhCheckboxAppearance ResolveAppearance() =>
        LhCheckboxTokens.Resolve(Checked, Indeterminate, Disabled, Size, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        _box.Width = LhEpx.Px(a.Size);
        _box.Height = LhEpx.Px(a.Size);
        _box.CornerRadius = new CornerRadius(a.BorderRadius);
        _box.Background = new SolidColorBrush(a.ContainerColor);
        _box.BorderBrush = new SolidColorBrush(a.BorderColor);
        _box.BorderThickness = new Thickness(a.BorderWidth);
        _box.Margin = new Thickness(0, 0, LhEpx.Px(a.Gap), 0);

        // 指示符形态：选中 ✓、不确定 ─、未选中不绘制
        _mark.Text = a.Mark switch
        {
            LhCheckboxMark.Check => "✓",
            LhCheckboxMark.Dash => "─",
            _ => string.Empty,
        };
        // 指示符字号随方框内径缩放（内径 = 尺寸 − 两边描边）
        _mark.FontSize = LhEpx.Px(a.Size - a.BorderWidth * 2);
        _mark.FontFamily = new FontFamily(LhTokens.Typography.FontFamily);
        _mark.Foreground = new SolidColorBrush(a.MarkColor);

        _label.Text = Label;
        _label.FontFamily = new FontFamily(LhTokens.Typography.FontFamily);
        _label.FontSize = LhEpx.Px(LhTokens.Typography.FontSize.Base);
        _label.Foreground = new SolidColorBrush(a.LabelColor);

        IsEnabled = a.Enabled;
    }

    /// <summary>点击命中整行：indeterminate 变为选中，否则翻转选中态</summary>
    private void OnTapped(object sender, TappedRoutedEventArgs e)
    {
        if (!ResolveAppearance().Enabled)
        {
            return;
        }
        // 点击后 indeterminate 归零（状态机入口见 LhCheckboxTokens.NextChecked）
        var next = LhCheckboxTokens.NextChecked(Checked, Indeterminate);
        if (Indeterminate)
        {
            Indeterminate = false;
        }
        Checked = next;
        Change?.Invoke(next);
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhCheckbox)d).ApplyAppearance();

    private static void OnLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhCheckbox)d;
        self._label.Text = e.NewValue as string ?? string.Empty;
    }
}
