using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Input;

/// <summary>
/// 输入框控件：单行文本输入（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/input.json</c> 逐字对齐；行为遵循 <c>cases/input.json</c>
/// 的 8 条黄金用例（已翻译为 xUnit 测试，见 tests/LhInputTests.cs）。
///
/// 受控语义：宿主监听 <see cref="ValueChange"/> 并把新值回写 <see cref="Value"/>。
/// </summary>
public sealed class LhInput : UserControl
{
    private readonly Border _container;
    private readonly TextBox _field;
    private readonly TextBlock _clearMark;
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
        nameof(Value), typeof(string), typeof(LhInput),
        new PropertyMetadata(string.Empty, OnValueChanged));

    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
        nameof(Placeholder), typeof(string), typeof(LhInput),
        new PropertyMetadata(string.Empty, OnPlaceholderChanged));

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type), typeof(LhInputType), typeof(LhInput),
        new PropertyMetadata(LhInputType.Text, OnTypeChanged));

    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size), typeof(LhInputSize), typeof(LhInput),
        new PropertyMetadata(LhInputSize.Medium, OnAppearanceChanged));

    public static readonly DependencyProperty DisabledProperty = DependencyProperty.Register(
        nameof(Disabled), typeof(bool), typeof(LhInput),
        new PropertyMetadata(false, OnDisabledChanged));

    public static readonly DependencyProperty ReadonlyProperty = DependencyProperty.Register(
        nameof(Readonly), typeof(bool), typeof(LhInput),
        new PropertyMetadata(false, OnReadonlyChanged));

    public static readonly DependencyProperty ClearableProperty = DependencyProperty.Register(
        nameof(Clearable), typeof(bool), typeof(LhInput),
        new PropertyMetadata(false, OnClearableChanged));

    public static readonly DependencyProperty MaxlengthProperty = DependencyProperty.Register(
        nameof(Maxlength), typeof(int?), typeof(LhInput),
        new PropertyMetadata(null, OnMaxlengthChanged));

    public string Value { get => (string)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
    public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }
    public LhInputType Type { get => (LhInputType)GetValue(TypeProperty); set => SetValue(TypeProperty, value); }
    public LhInputSize Size { get => (LhInputSize)GetValue(SizeProperty); set => SetValue(SizeProperty, value); }
    public bool Disabled { get => (bool)GetValue(DisabledProperty); set => SetValue(DisabledProperty, value); }
    public bool Readonly { get => (bool)GetValue(ReadonlyProperty); set => SetValue(ReadonlyProperty, value); }
    public bool Clearable { get => (bool)GetValue(ClearableProperty); set => SetValue(ClearableProperty, value); }
    public int? Maxlength { get => (int?)GetValue(MaxlengthProperty); set => SetValue(MaxlengthProperty, value); }

    public LhInput()
    {
        _field = new TextBox
        {
            BorderThickness = new Thickness(0),
            Background = new SolidColorBrush(LhColor.Transparent),
            Padding = new Thickness(0),
            VerticalContentAlignment = VerticalAlignment.Center,
            TextWrapping = TextWrapping.NoWrap,
        };
        _field.GotFocus += OnGotFocus;
        _field.LostFocus += OnLostFocus;
        _field.TextChanged += OnTextChanged;

        // 清除按钮：仅在 clearable 且 value 非空时显示
        _clearMark = new TextBlock
        {
            Text = "✕",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Right,
            Visibility = Visibility.Collapsed,
            IsHitTestVisible = true,
        };
        _clearMark.Tapped += OnClearTapped;

        _container = new Border { Child = _field };
        Content = _container;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhInputAppearance ResolveAppearance() =>
        LhInputTokens.Resolve(Size, Disabled, _focused, LhTheme.Current);

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

        _field.FontFamily = new FontFamily(LhTokens.Typography.FontFamily);
        _field.FontSize = LhEpx.Px(a.FontSize);
        _field.Foreground = new SolidColorBrush(a.TextColor);
        _field.PlaceholderText = Placeholder;
        _field.PlaceholderForeground = new SolidColorBrush(a.PlaceholderColor);
        _field.IsReadOnly = Readonly;
        _field.IsEnabled = !Disabled;
        // 密码掩码：WinUI 3 的 TextBox 无 PasswordChar（UWP 才有），
        // 正确做法是密码模式切换为 PasswordBox 控件；此处先留空，待 LhInput 双控件改造。
        // _field.PasswordChar = Type == LhInputType.Password ? "●" : string.Empty;

        UpdateClearMark();
    }

    /// <summary>清除符显隐：clearable 且 value 非空且可交互</summary>
    private void UpdateClearMark()
    {
        var show = Clearable && !Disabled && !Readonly && !string.IsNullOrEmpty(Value);
        _clearMark.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
        _container.Child = show ? CreateContentWithClear() : _field;
    }

    private Grid CreateContentWithClear()
    {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        _clearMark.FontSize = LhEpx.Px(ResolveAppearance().ClearIconSize);
        _clearMark.Foreground = new SolidColorBrush(ResolveAppearance().PlaceholderColor);
        Grid.SetColumn(_field, 0);
        Grid.SetColumn(_clearMark, 1);
        grid.Children.Add(_field);
        grid.Children.Add(_clearMark);
        return grid;
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_suppressChange)
        {
            return;
        }
        var next = LhInputTokens.Truncate(_field.Text, Maxlength);
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
        UpdateClearMark();
    }

    private void OnClearTapped(object sender, TappedRoutedEventArgs e)
    {
        e.Handled = true;
        if (Disabled || Readonly)
        {
            return;
        }
        // 清除按钮：value 非空时点击触发 onValueChange("")（黄金用例 clearable-emits-empty-value）
        _field.Text = string.Empty;
        ValueChange?.Invoke(string.Empty);
        UpdateClearMark();
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

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhInput)d).ApplyAppearance();

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhInput)d;
        var text = e.NewValue as string ?? string.Empty;
        if (self._field.Text != text)
        {
            self._suppressChange = true;
            self._field.Text = text;
            self._suppressChange = false;
        }
        self.UpdateClearMark();
    }

    private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhInput)d;
        self._field.PlaceholderText = e.NewValue as string ?? string.Empty;
    }

    private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhInput)d).ApplyAppearance();

    private static void OnDisabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhInput)d).ApplyAppearance();

    private static void OnReadonlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhInput)d).ApplyAppearance();

    private static void OnClearableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhInput)d).UpdateClearMark();

    private static void OnMaxlengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhInput)d;
        self._field.Text = LhInputTokens.Truncate(self._field.Text, self.Maxlength);
    }
}
