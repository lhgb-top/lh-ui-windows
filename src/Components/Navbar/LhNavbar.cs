using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Icon;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Navbar;

/// <summary>
/// 导航栏控件：页面顶部固定栏，左/中/右三段（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/navbar.json</c> 逐字对齐；行为遵循 <c>cases/navbar.json</c>
/// 的 6 条黄金用例（已翻译为 xUnit 测试，见 tests/LhNavbarTests.cs）。
///
/// 层级取 zIndex.sticky：宿主将其作为页面顶部固定栏（Grid 首行或 Canvas 顶部）。
/// </summary>
public sealed class LhNavbar : UserControl
{
    private readonly Grid _root;
    private readonly StackPanel _left;
    private readonly TextBlock _title;
    private readonly StackPanel _right;
    private readonly TextBlock _leftText;
    private readonly TextBlock _rightText;
    private readonly TextBlock _leftIcon;
    private readonly TextBlock _rightIcon;
    private readonly Rectangle _divider;

    /// <summary>点击左侧区域时触发</summary>
    public event Action? LeftClick;

    /// <summary>点击右侧区域时触发</summary>
    public event Action? RightClick;

    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title), typeof(string), typeof(LhNavbar),
        new PropertyMetadata(string.Empty, OnTitleChanged));

    public static readonly DependencyProperty LeftTextProperty = DependencyProperty.Register(
        nameof(LeftText), typeof(string), typeof(LhNavbar),
        new PropertyMetadata(string.Empty, OnLeftTextChanged));

    public static readonly DependencyProperty LeftIconProperty = DependencyProperty.Register(
        nameof(LeftIcon), typeof(string), typeof(LhNavbar),
        new PropertyMetadata(null, OnLeftIconChanged));

    public static readonly DependencyProperty RightTextProperty = DependencyProperty.Register(
        nameof(RightText), typeof(string), typeof(LhNavbar),
        new PropertyMetadata(string.Empty, OnRightTextChanged));

    public static readonly DependencyProperty RightIconProperty = DependencyProperty.Register(
        nameof(RightIcon), typeof(string), typeof(LhNavbar),
        new PropertyMetadata(null, OnRightIconChanged));

    public static readonly DependencyProperty BorderedProperty = DependencyProperty.Register(
        nameof(Bordered), typeof(bool), typeof(LhNavbar),
        new PropertyMetadata(true, OnAppearanceChanged));

    public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    public string LeftText { get => (string)GetValue(LeftTextProperty); set => SetValue(LeftTextProperty, value); }
    public string LeftIcon { get => (string)GetValue(LeftIconProperty); set => SetValue(LeftIconProperty, value); }
    public string RightText { get => (string)GetValue(RightTextProperty); set => SetValue(RightTextProperty, value); }
    public string RightIcon { get => (string)GetValue(RightIconProperty); set => SetValue(RightIconProperty, value); }
    public bool Bordered { get => (bool)GetValue(BorderedProperty); set => SetValue(BorderedProperty, value); }

    public LhNavbar()
    {
        // 图标用本地图标体系的字形（LhIconTokens.GlyphFor），尺寸取 navbar.iconSize 令牌；
        // LhIcon 控件的尺寸是分档枚举，无法表达 navbar.iconSize，故在此自持字形文本
        _leftIcon = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            Visibility = Visibility.Collapsed,
        };
        _leftText = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            TextWrapping = TextWrapping.NoWrap,
            Visibility = Visibility.Collapsed,
        };
        _left = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
        };
        _left.Children.Add(_leftIcon);
        _left.Children.Add(_leftText);
        _left.Tapped += OnLeftTapped;

        _title = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            TextAlignment = TextAlignment.Center,
            TextWrapping = TextWrapping.NoWrap,
        };

        _rightIcon = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            Visibility = Visibility.Collapsed,
        };
        _rightText = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            TextWrapping = TextWrapping.NoWrap,
            Visibility = Visibility.Collapsed,
        };
        _right = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
        };
        _right.Children.Add(_rightText);
        _right.Children.Add(_rightIcon);
        _right.Tapped += OnRightTapped;

        _divider = new Rectangle { IsHitTestVisible = false, Visibility = Visibility.Collapsed };

        _root = new Grid();
        _root.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        _root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        _root.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        _root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        _root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        Grid.SetColumn(_left, 0);
        Grid.SetColumn(_title, 1);
        Grid.SetColumn(_right, 2);
        Grid.SetColumnSpan(_divider, 3);
        Grid.SetRow(_divider, 1);
        _root.Children.Add(_left);
        _root.Children.Add(_title);
        _root.Children.Add(_right);
        _root.Children.Add(_divider);
        Content = _root;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhNavbarAppearance ResolveAppearance() =>
        LhNavbarTokens.Resolve(Bordered, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        Height = LhEpx.Px(a.Height);
        MinHeight = LhEpx.Px(a.Height);
        Padding = new Thickness(LhEpx.Px(a.PaddingHorizontal), 0, LhEpx.Px(a.PaddingHorizontal), 0);
        Background = new SolidColorBrush(a.ContainerColor);
        Canvas.SetZIndex(_root, a.ZIndex);

        var font = new FontFamily(LhTokens.Typography.FontFamily);
        var titleBrush = new SolidColorBrush(a.TitleColor);

        _title.Text = Title;
        _title.FontFamily = font;
        _title.FontSize = LhEpx.Px(a.TitleFontSize);
        _title.Foreground = titleBrush;

        _leftText.Text = LeftText;
        _leftText.FontFamily = font;
        _leftText.FontSize = LhEpx.Px(a.TitleFontSize);
        _leftText.Foreground = titleBrush;
        _leftText.Visibility = string.IsNullOrEmpty(LeftText) ? Visibility.Collapsed : Visibility.Visible;
        _left.Spacing = LhEpx.Px(LhTokens.Spacing.Xs);

        // 图标：字形取本地图标表，尺寸取 navbar.iconSize 令牌
        _leftIcon.FontFamily = font;
        _leftIcon.FontSize = LhEpx.Px(a.IconSize);
        _leftIcon.Foreground = titleBrush;
        _leftIcon.Text = LhIconTokens.GlyphFor(LeftIcon);

        _rightText.Text = RightText;
        _rightText.FontFamily = font;
        _rightText.FontSize = LhEpx.Px(a.TitleFontSize);
        _rightText.Foreground = titleBrush;
        _rightText.Visibility = string.IsNullOrEmpty(RightText) ? Visibility.Collapsed : Visibility.Visible;
        _right.Spacing = LhEpx.Px(LhTokens.Spacing.Xs);

        _rightIcon.FontFamily = font;
        _rightIcon.FontSize = LhEpx.Px(a.IconSize);
        _rightIcon.Foreground = titleBrush;
        _rightIcon.Text = LhIconTokens.GlyphFor(RightIcon);

        if (a.DividerColor is { } dividerColor)
        {
            _divider.Fill = new SolidColorBrush(dividerColor);
            _divider.Height = a.BorderWidth;
            _divider.Visibility = Visibility.Visible;
        }
        else
        {
            _divider.Visibility = Visibility.Collapsed;
        }
    }

    private void OnLeftTapped(object sender, TappedRoutedEventArgs e)
    {
        e.Handled = true;
        // 左侧无内容时不触发（黄金用例 no-left-content-no-onleftclick）
        if (HasLeftContent())
        {
            LeftClick?.Invoke();
        }
    }

    private void OnRightTapped(object sender, TappedRoutedEventArgs e)
    {
        e.Handled = true;
        if (HasRightContent())
        {
            RightClick?.Invoke();
        }
    }

    /// <summary>左侧是否有内容：文字与图标二者非空即有</summary>
    private bool HasLeftContent() => !string.IsNullOrEmpty(LeftText) || !string.IsNullOrEmpty(LeftIcon);

    /// <summary>右侧是否有内容：文字与图标二者非空即有</summary>
    private bool HasRightContent() => !string.IsNullOrEmpty(RightText) || !string.IsNullOrEmpty(RightIcon);

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhNavbar)d).ApplyAppearance();

    private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhNavbar)d;
        self._title.Text = e.NewValue as string ?? string.Empty;
    }

    private static void OnLeftTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhNavbar)d;
        var text = e.NewValue as string ?? string.Empty;
        self._leftText.Text = text;
        self._leftText.Visibility = string.IsNullOrEmpty(text) ? Visibility.Collapsed : Visibility.Visible;
    }

    private static void OnRightTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhNavbar)d;
        var text = e.NewValue as string ?? string.Empty;
        self._rightText.Text = text;
        self._rightText.Visibility = string.IsNullOrEmpty(text) ? Visibility.Collapsed : Visibility.Visible;
    }

    private static void OnLeftIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhNavbar)d;
        var name = e.NewValue as string;
        self._leftIcon.Text = LhIconTokens.GlyphFor(name);
        self._leftIcon.Visibility = string.IsNullOrEmpty(name) ? Visibility.Collapsed : Visibility.Visible;
    }

    private static void OnRightIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhNavbar)d;
        var name = e.NewValue as string;
        self._rightIcon.Text = LhIconTokens.GlyphFor(name);
        self._rightIcon.Visibility = string.IsNullOrEmpty(name) ? Visibility.Collapsed : Visibility.Visible;
    }
}
