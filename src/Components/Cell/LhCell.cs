using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Cell;

/// <summary>
/// 单元格控件：列表行单元，左侧图标 + 标题（可带必填红星）+ 说明，右侧值与指示箭头，底部可选分隔线。
///
/// 属性与 <c>lh-ui-core/specs/cell.json</c> 逐字对齐；行为遵循 <c>cases/cell.json</c>
/// 的 6 条黄金用例（已翻译为 xUnit 测试，见 tests/LhCellTests.cs）。
///
/// 主题：订阅 <see cref="LhTheme.ThemeChanged"/>，切明暗时自动重算文字色与分隔线色（主题热重绘）。
/// </summary>
public sealed class LhCell : UserControl
{
    private readonly Grid _root;
    private readonly TextBlock _requiredMark;
    private readonly LhIcon _icon;
    private readonly TextBlock _title;
    private readonly TextBlock _label;
    private readonly TextBlock _value;
    private readonly TextBlock _arrow;
    private readonly Rectangle _divider;

    /// <summary>点击回调；仅当 <see cref="Clickable"/> 为 true 时触发</summary>
    public event Action? Click;

    /// <summary>标题（spec 的 title 插槽）</summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title), typeof(string), typeof(LhCell),
        new PropertyMetadata(string.Empty, OnTitleChanged));

    /// <summary>标题下说明文字（label 插槽）</summary>
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
        nameof(Label), typeof(string), typeof(LhCell),
        new PropertyMetadata(string.Empty, OnLabelChanged));

    /// <summary>右侧值文本（value 插槽）</summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(string), typeof(LhCell),
        new PropertyMetadata(string.Empty, OnValueChanged));

    /// <summary>左侧图标名（本地图标体系，见 LhIconTokens.GlyphFor）</summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon), typeof(string), typeof(LhCell),
        new PropertyMetadata(null, OnIconChanged));

    /// <summary>是否在标题前绘制必填红星（danger 色）</summary>
    public static readonly DependencyProperty RequiredProperty = DependencyProperty.Register(
        nameof(Required), typeof(bool), typeof(LhCell),
        new PropertyMetadata(false, OnAppearanceChanged));

    /// <summary>是否可点击（决定右侧箭头与 Click 放行）</summary>
    public static readonly DependencyProperty ClickableProperty = DependencyProperty.Register(
        nameof(Clickable), typeof(bool), typeof(LhCell),
        new PropertyMetadata(false, OnAppearanceChanged));

    /// <summary>是否绘制底部分隔线（color.border.lighter）</summary>
    public static readonly DependencyProperty BorderedProperty = DependencyProperty.Register(
        nameof(Bordered), typeof(bool), typeof(LhCell),
        new PropertyMetadata(true, OnAppearanceChanged));

    public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
    public string Value { get => (string)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
    public string Icon { get => (string)GetValue(IconProperty); set => SetValue(IconProperty, value); }
    public bool Required { get => (bool)GetValue(RequiredProperty); set => SetValue(RequiredProperty, value); }
    public bool Clickable { get => (bool)GetValue(ClickableProperty); set => SetValue(ClickableProperty, value); }
    public bool Bordered { get => (bool)GetValue(BorderedProperty); set => SetValue(BorderedProperty, value); }

    public LhCell()
    {
        _requiredMark = new TextBlock { VerticalAlignment = VerticalAlignment.Top, Text = "✱" };
        _icon = new LhIcon
        {
            VerticalAlignment = VerticalAlignment.Center,
            Size = LhIconSize.Medium,
            Tone = LhIconTone.Regular,
            Visibility = Visibility.Collapsed,
        };
        _title = new TextBlock { VerticalAlignment = VerticalAlignment.Center, TextWrapping = TextWrapping.NoWrap };
        _label = new TextBlock { VerticalAlignment = VerticalAlignment.Center, TextWrapping = TextWrapping.Wrap };
        _value = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Right,
            TextWrapping = TextWrapping.NoWrap,
        };
        _arrow = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            Text = "›",
            Visibility = Visibility.Collapsed,
        };
        _divider = new Rectangle { IsHitTestVisible = false, Visibility = Visibility.Collapsed };

        // 左列：红星 + 图标 + （标题 / 说明）
        var left = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            Spacing = LhEpx.Px(LhTokens.Spacing.Xs),
        };
        left.Children.Add(_requiredMark);
        left.Children.Add(_icon);
        var textColumn = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Spacing = LhEpx.Px(LhTokens.Cell.Gap),
        };
        textColumn.Children.Add(_title);
        textColumn.Children.Add(_label);
        left.Children.Add(textColumn);

        var right = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Spacing = LhEpx.Px(LhTokens.Spacing.Xs),
        };
        right.Children.Add(_value);
        right.Children.Add(_arrow);

        _root = new Grid();
        _root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        _root.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        _root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        _root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        Grid.SetColumn(left, 0);
        Grid.SetColumn(right, 1);
        Grid.SetColumnSpan(_divider, 2);
        Grid.SetRow(_divider, 1);
        _root.Children.Add(left);
        _root.Children.Add(right);
        _root.Children.Add(_divider);
        Content = _root;

        Tapped += OnTapped;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhCellAppearance ResolveAppearance() =>
        LhCellTokens.Resolve(Bordered, Clickable, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        Padding = new Thickness(
            LhEpx.Px(a.PaddingHorizontal),
            LhEpx.Px(a.PaddingVertical),
            LhEpx.Px(a.PaddingHorizontal),
            LhEpx.Px(a.PaddingVertical));

        var font = new FontFamily(LhTokens.Typography.FontFamily);

        _requiredMark.Visibility = Required ? Visibility.Visible : Visibility.Collapsed;
        _requiredMark.FontSize = LhEpx.Px(a.FontSizeTitle);
        _requiredMark.Foreground = new SolidColorBrush(a.RequiredColor);

        var hasIcon = !string.IsNullOrEmpty(Icon);
        _icon.Visibility = hasIcon ? Visibility.Visible : Visibility.Collapsed;
        _icon.Name = Icon ?? string.Empty;
        _icon.Foreground = new SolidColorBrush(a.IconColor);
        _icon.Width = LhEpx.Px(a.IconSize);
        _icon.Height = LhEpx.Px(a.IconSize);
        _icon.FontSize = LhEpx.Px(a.IconSize);

        _title.Text = Title;
        _title.FontFamily = font;
        _title.FontSize = LhEpx.Px(a.FontSizeTitle);
        _title.Foreground = new SolidColorBrush(a.TitleColor);

        var hasLabel = !string.IsNullOrEmpty(Label);
        _label.Visibility = hasLabel ? Visibility.Visible : Visibility.Collapsed;
        _label.Text = Label;
        _label.FontFamily = font;
        _label.FontSize = LhEpx.Px(a.FontSizeLabel);
        _label.Foreground = new SolidColorBrush(a.LabelColor);

        var hasValue = !string.IsNullOrEmpty(Value);
        _value.Visibility = hasValue ? Visibility.Visible : Visibility.Collapsed;
        _value.Text = Value;
        _value.FontFamily = font;
        _value.FontSize = LhEpx.Px(a.FontSizeValue);
        _value.Foreground = new SolidColorBrush(a.ValueColor);

        _arrow.Visibility = Clickable ? Visibility.Visible : Visibility.Collapsed;
        _arrow.FontFamily = font;
        _arrow.FontSize = LhEpx.Px(a.FontSizeTitle);
        _arrow.Foreground = new SolidColorBrush(a.ArrowColor);

        if (a.DividerColor is { } dividerColor)
        {
            _divider.Fill = new SolidColorBrush(dividerColor);
            _divider.Height = 1;
            _divider.Visibility = Visibility.Visible;
        }
        else
        {
            _divider.Visibility = Visibility.Collapsed;
        }
    }

    private void OnTapped(object sender, TappedRoutedEventArgs e)
    {
        if (Clickable)
        {
            Click?.Invoke();
        }
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhCell)d).ApplyAppearance();

    private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhCell)d;
        self._title.Text = e.NewValue as string ?? string.Empty;
    }

    private static void OnLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhCell)d;
        self._label.Text = e.NewValue as string ?? string.Empty;
        self._label.Visibility = string.IsNullOrEmpty(self._label.Text) ? Visibility.Collapsed : Visibility.Visible;
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhCell)d;
        self._value.Text = e.NewValue as string ?? string.Empty;
        self._value.Visibility = string.IsNullOrEmpty(self._value.Text) ? Visibility.Collapsed : Visibility.Visible;
    }

    private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhCell)d;
        var name = e.NewValue as string;
        self._icon.Name = name ?? string.Empty;
        self._icon.Visibility = string.IsNullOrEmpty(name) ? Visibility.Collapsed : Visibility.Visible;
    }
}
