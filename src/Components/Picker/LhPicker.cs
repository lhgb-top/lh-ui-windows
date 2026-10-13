using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Picker;

/// <summary>
/// 选择器控件：底部弹出的单列滚轮（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/picker.json</c> 逐字对齐；行为遵循 <c>cases/picker.json</c>
/// 的 8 条黄金用例（已翻译为 xUnit 测试，见 tests/LhPickerTests.cs）。
///
/// 单列滚轮由 ScrollViewer + 等高条目承担（P0 只做单列，多列留待后续）：
/// 滚动停止时按偏移量吸附到最近条目并触发 <see cref="ValueChange"/>。
/// 声明式可见性：<see cref="Visible"/> 为 false 时不渲染任何内容；确认/取消后触发
/// <see cref="Close"/>，宿主据此置 Visible 为 false。组件须作为页面 Grid 的最后子节点挂载。
/// </summary>
public sealed class LhPicker : UserControl
{
    private readonly Rectangle _overlay;
    private readonly Border _panel;
    private readonly TextBlock _cancelLabel;
    private readonly TextBlock _titleLabel;
    private readonly TextBlock _confirmLabel;
    private readonly ScrollViewer _wheel;
    private readonly StackPanel _items;
    private readonly Rectangle _indicatorTop;
    private readonly Rectangle _indicatorBottom;

    /// <summary>滚轮停止且选中项变化时触发</summary>
    public event Action<int>? ValueChange;

    /// <summary>点击确认按钮时触发，携带当前选中索引</summary>
    public event Action<int>? Confirm;

    /// <summary>点击取消按钮时触发</summary>
    public event Action? Cancel;

    /// <summary>确认/取消后触发，宿主据此置 Visible 为 false</summary>
    public event Action? Close;

    public static readonly DependencyProperty VisibleProperty = DependencyProperty.Register(
        nameof(Visible), typeof(bool), typeof(LhPicker),
        new PropertyMetadata(false, OnVisibleChanged));

    public static readonly DependencyProperty OptionsProperty = DependencyProperty.Register(
        nameof(Options), typeof(string[]), typeof(LhPicker),
        new PropertyMetadata(null, OnOptionsChanged));

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(int), typeof(LhPicker),
        new PropertyMetadata(0, OnValueChanged));

    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title), typeof(string), typeof(LhPicker),
        new PropertyMetadata(string.Empty, OnTitleChanged));

    public static readonly DependencyProperty ConfirmTextProperty = DependencyProperty.Register(
        nameof(ConfirmText), typeof(string), typeof(LhPicker),
        new PropertyMetadata("确认", OnConfirmTextChanged));

    public static readonly DependencyProperty CancelTextProperty = DependencyProperty.Register(
        nameof(CancelText), typeof(string), typeof(LhPicker),
        new PropertyMetadata("取消", OnCancelTextChanged));

    public bool Visible { get => (bool)GetValue(VisibleProperty); set => SetValue(VisibleProperty, value); }
    public string[] Options { get => (string[])GetValue(OptionsProperty); set => SetValue(OptionsProperty, value); }
    public int Value { get => (int)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
    public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    public string ConfirmText { get => (string)GetValue(ConfirmTextProperty); set => SetValue(ConfirmTextProperty, value); }
    public string CancelText { get => (string)GetValue(CancelTextProperty); set => SetValue(CancelTextProperty, value); }

    /// <summary>滚轮当前选中索引（内部态，确认时上报）</summary>
    private int _selectedIndex;

    public LhPicker()
    {
        _cancelLabel = new TextBlock { VerticalAlignment = VerticalAlignment.Center };
        _cancelLabel.Tapped += OnCancelTapped;
        _titleLabel = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            TextAlignment = TextAlignment.Center,
            MaxLines = 1,
        };
        _confirmLabel = new TextBlock { VerticalAlignment = VerticalAlignment.Center };
        _confirmLabel.Tapped += OnConfirmTapped;

        var toolbar = new Grid { Height = LhEpx.Px(LhTokens.Picker.ToolbarHeight) };
        toolbar.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        toolbar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        toolbar.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        toolbar.Padding = new Thickness(
            LhEpx.Px(LhTokens.Picker.ItemHeight), 0, LhEpx.Px(LhTokens.Picker.ItemHeight), 0);
        Grid.SetColumn(_cancelLabel, 0);
        Grid.SetColumn(_titleLabel, 1);
        Grid.SetColumn(_confirmLabel, 2);
        toolbar.Children.Add(_cancelLabel);
        toolbar.Children.Add(_titleLabel);
        toolbar.Children.Add(_confirmLabel);

        _items = new StackPanel { Orientation = Orientation.Vertical };
        _wheel = new ScrollViewer
        {
            Content = _items,
            VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
            VerticalAlignment = VerticalAlignment.Top,
        };
        _wheel.ViewChanged += OnWheelViewChanged;

        // 中央指示器：两条水平线（color.border.lighter），间距 = picker.itemHeight
        _indicatorTop = new Rectangle { IsHitTestVisible = false };
        _indicatorBottom = new Rectangle { IsHitTestVisible = false };
        var wheelHost = new Grid();
        wheelHost.Children.Add(_wheel);
        wheelHost.Children.Add(_indicatorTop);
        wheelHost.Children.Add(_indicatorBottom);

        var panelContent = new StackPanel { Orientation = Orientation.Vertical };
        panelContent.Children.Add(toolbar);
        panelContent.Children.Add(wheelHost);

        _panel = new Border { Child = panelContent };
        _overlay = new Rectangle();
        _overlay.Tapped += OnOverlayTapped;

        var root = new Grid();
        root.Children.Add(_overlay);
        root.Children.Add(_panel);
        Content = root;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
        RebuildItems();
        UpdateVisibility();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhPickerAppearance ResolveAppearance() =>
        LhPickerTokens.Resolve(Options?.Length ?? 0, Value, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        _overlay.Fill = new SolidColorBrush(a.OverlayColor);
        Canvas.SetZIndex(_overlay, a.ZIndexBackdrop);

        // 底部面板：顶部圆角、底色 background.default
        _panel.Height = LhEpx.Px(a.Height);
        _panel.Background = new SolidColorBrush(a.ContainerColor);
        _panel.CornerRadius = new CornerRadius(a.BorderRadius, a.BorderRadius, 0, 0);
        _panel.HorizontalAlignment = HorizontalAlignment.Stretch;
        _panel.VerticalAlignment = VerticalAlignment.Bottom;
        Canvas.SetZIndex(_panel, a.ZIndexModal);

        var font = new FontFamily(LhTokens.Typography.FontFamily);
        _cancelLabel.Text = CancelText;
        _cancelLabel.FontFamily = font;
        _cancelLabel.FontSize = LhEpx.Px(a.ItemFontSize);
        _cancelLabel.Foreground = new SolidColorBrush(a.CancelColor);

        _titleLabel.Text = Title;
        _titleLabel.FontFamily = font;
        _titleLabel.FontSize = LhEpx.Px(a.ItemFontSize);
        _titleLabel.Foreground = new SolidColorBrush(a.TitleColor);

        _confirmLabel.Text = ConfirmText;
        _confirmLabel.FontFamily = font;
        _confirmLabel.FontSize = LhEpx.Px(a.ItemFontSize);
        _confirmLabel.Foreground = new SolidColorBrush(a.ConfirmColor);

        _wheel.Height = LhEpx.Px(a.WheelHeight);

        // 中央指示器：两条线分别贴合中央条目的上下沿
        var indicatorY = (a.WheelHeight - a.ItemHeight) / 2;
        _indicatorTop.Fill = new SolidColorBrush(a.IndicatorColor);
        _indicatorTop.Height = 1;
        _indicatorTop.VerticalAlignment = VerticalAlignment.Top;
        _indicatorTop.Margin = new Thickness(0, LhEpx.Px(indicatorY), 0, 0);
        _indicatorBottom.Fill = new SolidColorBrush(a.IndicatorColor);
        _indicatorBottom.Height = 1;
        _indicatorBottom.VerticalAlignment = VerticalAlignment.Top;
        _indicatorBottom.Margin = new Thickness(0, LhEpx.Px(indicatorY + a.ItemHeight), 0, 0);

        _selectedIndex = a.SelectedIndex;
    }

    /// <summary>重建候选项条目（上下各补一轮空位，使首/末项能滚到中央）</summary>
    private void RebuildItems()
    {
        var a = ResolveAppearance();
        _items.Children.Clear();
        var options = Options ?? Array.Empty<string>();
        var font = new FontFamily(LhTokens.Typography.FontFamily);

        // 顶部空位：让第一项能滚到中央指示器
        _items.Children.Add(new Border { Height = LhEpx.Px(a.WheelHeight / 2 - a.ItemHeight / 2) });

        foreach (var option in options)
        {
            var label = new TextBlock
            {
                Text = option,
                FontFamily = font,
                FontSize = LhEpx.Px(a.ItemFontSize),
                Foreground = new SolidColorBrush(a.TitleColor),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = LhEpx.Px(a.ItemHeight),
            };
            _items.Children.Add(label);
        }

        // 底部空位
        _items.Children.Add(new Border { Height = LhEpx.Px(a.WheelHeight / 2 - a.ItemHeight / 2) });

        // 滚到钳制后的选中项
        _selectedIndex = a.SelectedIndex;
        _wheel.ChangeView(null, LhEpx.Px(_selectedIndex * a.ItemHeight), null, true);
    }

    /// <summary>滚动停止时吸附到最近条目并触发 ValueChange</summary>
    private void OnWheelViewChanged(object? sender, ScrollViewerViewChangedEventArgs e)
    {
        if (e.IsIntermediate)
        {
            return;
        }
        var itemHeight = LhEpx.Px(LhTokens.Picker.ItemHeight);
        var index = (int)Math.Round(_wheel.VerticalOffset / itemHeight);
        var options = Options ?? Array.Empty<string>();
        index = LhPickerTokens.ClampIndex(index, options.Length);
        if (index != _selectedIndex)
        {
            _selectedIndex = index;
            // 滚轮停止后，选中项变化时触发 onValueChange
            ValueChange?.Invoke(index);
        }
        // 吸附到条目边界
        _wheel.ChangeView(null, LhEpx.Px(index * LhTokens.Picker.ItemHeight), null, true);
    }

    private void OnConfirmTapped(object sender, TappedRoutedEventArgs e)
    {
        e.Handled = true;
        // 确认携带当前选中索引，同时触发 onClose（宿主据此隐藏）
        Confirm?.Invoke(_selectedIndex);
        Close?.Invoke();
    }

    private void OnCancelTapped(object sender, TappedRoutedEventArgs e)
    {
        e.Handled = true;
        Cancel?.Invoke();
        Close?.Invoke();
    }

    /// <summary>遮罩点击不关闭（确认/取消是唯一关闭路径），吞掉事件</summary>
    private void OnOverlayTapped(object sender, TappedRoutedEventArgs e) => e.Handled = true;

    /// <summary>visible 为 false 时不渲染任何内容（黄金用例 hidden-not-rendered）</summary>
    private void UpdateVisibility()
    {
        Visibility = Visible ? Visibility.Visible : Visibility.Collapsed;
    }

    private void OnThemeChanged(object? sender, EventArgs e)
    {
        ApplyAppearance();
        RebuildItems();
    }

    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhPicker)d).UpdateVisibility();

    private static void OnOptionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhPicker)d;
        self.ApplyAppearance();
        self.RebuildItems();
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhPicker)d;
        self.ApplyAppearance();
        var a = self.ResolveAppearance();
        self._selectedIndex = a.SelectedIndex;
        self._wheel.ChangeView(null, LhEpx.Px(a.SelectedIndex * LhTokens.Picker.ItemHeight), null, true);
    }

    private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhPicker)d;
        self._titleLabel.Text = e.NewValue as string ?? string.Empty;
    }

    private static void OnConfirmTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhPicker)d;
        self._confirmLabel.Text = e.NewValue as string ?? string.Empty;
    }

    private static void OnCancelTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhPicker)d;
        self._cancelLabel.Text = e.NewValue as string ?? string.Empty;
    }
}
