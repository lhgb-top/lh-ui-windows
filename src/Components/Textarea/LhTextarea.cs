using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Textarea;

/// <summary>
/// 多行输入框控件：按 rows 给出初始可见行数，内容超出自动增高（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/textarea.json</c> 逐字对齐；行为遵循 <c>cases/textarea.json</c>
/// 的 6 条黄金用例（已翻译为 xUnit 测试，见 tests/LhTextareaTests.cs）。
///
/// 受控语义：宿主监听 <see cref="ValueChange"/> 并把新值回写 <see cref="Value"/>。
/// </summary>
public sealed class LhTextarea : UserControl
{
    private readonly Border _container;
    private readonly TextBox _field;
    private readonly TextBlock _count;
    private bool _focused;
    private bool _suppressChange;

    /// <summary>值变化回调（受控）；disabled / readonly 时不触发</summary>
    public event Action<string>? ValueChange;

    /// <summary>获得焦点时触发</summary>
    // new：契约事件 onFocus 按本库命名约定（去 on、PascalCase）即 Focus，与 UIElement.Focus(FocusState) 同名，刻意隐藏
    public new event Action? Focus;

    /// <summary>失去焦点时触发</summary>
    public event Action? Blur;

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(string), typeof(LhTextarea),
        new PropertyMetadata(string.Empty, OnValueChanged));

    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
        nameof(Placeholder), typeof(string), typeof(LhTextarea),
        new PropertyMetadata(string.Empty, OnPlaceholderChanged));

    public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
        nameof(Rows), typeof(int), typeof(LhTextarea),
        new PropertyMetadata(3, OnRowsChanged));

    public static readonly DependencyProperty DisabledProperty = DependencyProperty.Register(
        nameof(Disabled), typeof(bool), typeof(LhTextarea),
        new PropertyMetadata(false, OnDisabledChanged));

    public static readonly DependencyProperty ReadonlyProperty = DependencyProperty.Register(
        nameof(Readonly), typeof(bool), typeof(LhTextarea),
        new PropertyMetadata(false, OnReadonlyChanged));

    public static readonly DependencyProperty ShowCountProperty = DependencyProperty.Register(
        nameof(ShowCount), typeof(bool), typeof(LhTextarea),
        new PropertyMetadata(false, OnShowCountChanged));

    public static readonly DependencyProperty MaxlengthProperty = DependencyProperty.Register(
        nameof(Maxlength), typeof(int?), typeof(LhTextarea),
        new PropertyMetadata(null, OnMaxlengthChanged));

    public string Value { get => (string)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
    public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }
    public int Rows { get => (int)GetValue(RowsProperty); set => SetValue(RowsProperty, value); }
    public bool Disabled { get => (bool)GetValue(DisabledProperty); set => SetValue(DisabledProperty, value); }
    public bool Readonly { get => (bool)GetValue(ReadonlyProperty); set => SetValue(ReadonlyProperty, value); }
    public bool ShowCount { get => (bool)GetValue(ShowCountProperty); set => SetValue(ShowCountProperty, value); }
    public int? Maxlength { get => (int?)GetValue(MaxlengthProperty); set => SetValue(MaxlengthProperty, value); }

    public LhTextarea()
    {
        _field = new TextBox
        {
            BorderThickness = new Thickness(0),
            Background = new SolidColorBrush(LhColor.Transparent),
            Padding = new Thickness(0),
            AcceptsReturn = true,
            TextWrapping = TextWrapping.Wrap,
        };
        // 附加属性不能写在对象初始化器里（编译器把它当成非静态属性赋值），须用静态 setter
        ScrollViewer.SetVerticalScrollBarVisibility(_field, ScrollBarVisibility.Auto);
        _field.GotFocus += OnGotFocus;
        _field.LostFocus += OnLostFocus;
        _field.TextChanged += OnTextChanged;

        _count = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Bottom,
            Visibility = Visibility.Collapsed,
        };

        _container = new Border { Child = _field };
        Content = _container;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhTextareaAppearance ResolveAppearance() =>
        LhTextareaTokens.Resolve(Rows, Disabled, _focused, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        Padding = new Thickness(
            LhEpx.Px(a.PaddingHorizontal),
            LhEpx.Px(a.PaddingVertical),
            LhEpx.Px(a.PaddingHorizontal),
            LhEpx.Px(a.PaddingVertical));
        MinHeight = LhEpx.Px(LhTextareaTokens.HeightForRows(Rows, a));
        _container.Background = new SolidColorBrush(a.ContainerColor);
        _container.BorderBrush = new SolidColorBrush(a.BorderColor);
        _container.BorderThickness = new Thickness(a.BorderWidth);
        _container.CornerRadius = new CornerRadius(a.BorderRadius);

        _field.FontFamily = new FontFamily(LhTokens.Typography.FontFamily);
        _field.FontSize = LhEpx.Px(a.FontSize);
        _field.Foreground = new SolidColorBrush(a.TextColor);
        _field.PlaceholderText = Placeholder;
        _field.PlaceholderForeground = new SolidColorBrush(a.PlaceholderColor);
        _field.IsReadOnly = Readonly;
        _field.IsEnabled = !Disabled;

        UpdateCount();
    }

    /// <summary>字数统计：showCount 时右下角绘制「当前/最大」（黄金用例 showcount-renders-count）</summary>
    private void UpdateCount()
    {
        if (!ShowCount)
        {
            _count.Visibility = Visibility.Collapsed;
            _container.Child = _field;
            return;
        }
        var a = ResolveAppearance();
        _count.Text = LhTextareaTokens.CountText(Value, Maxlength);
        _count.FontSize = LhEpx.Px(a.CountFontSize);
        _count.Foreground = new SolidColorBrush(a.CountColor);
        _count.Visibility = Visibility.Visible;

        // 文本区在下、统计在右下角（Grid 叠层：统计盖在文本区右下）
        var grid = new Grid();
        grid.Children.Add(_field);
        grid.Children.Add(_count);
        _container.Child = grid;
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_suppressChange)
        {
            return;
        }
        var next = LhTextareaTokens.Truncate(_field.Text, Maxlength);
        if (next != _field.Text)
        {
            // 超出 maxlength：截断新输入（黄金用例 maxlength-truncates-input）
            _suppressChange = true;
            _field.Text = next;
            _field.SelectionStart = next.Length;
            _suppressChange = false;
        }
        if (!Disabled && !Readonly)
        {
            ValueChange?.Invoke(next);
        }
        if (ShowCount)
        {
            UpdateCount();
        }
    }

    private void OnGotFocus(object sender, RoutedEventArgs e)
    {
        _focused = true;
        ApplyAppearance();
        Focus?.Invoke();
    }

    private void OnLostFocus(object sender, RoutedEventArgs e)
    {
        _focused = false;
        ApplyAppearance();
        Blur?.Invoke();
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnDisabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhTextarea)d).ApplyAppearance();

    private static void OnReadonlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhTextarea)d).ApplyAppearance();

    private static void OnRowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhTextarea)d).ApplyAppearance();

    private static void OnShowCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhTextarea)d).UpdateCount();

    private static void OnMaxlengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhTextarea)d;
        self._field.Text = LhTextareaTokens.Truncate(self._field.Text, self.Maxlength);
        self.UpdateCount();
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhTextarea)d;
        var text = e.NewValue as string ?? string.Empty;
        if (self._field.Text != text)
        {
            self._suppressChange = true;
            self._field.Text = text;
            self._suppressChange = false;
        }
        if (self.ShowCount)
        {
            self.UpdateCount();
        }
    }

    private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhTextarea)d;
        self._field.PlaceholderText = e.NewValue as string ?? string.Empty;
    }
}
