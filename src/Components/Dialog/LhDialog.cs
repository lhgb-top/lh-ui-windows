using System;
using System.Numerics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Dialog;

/// <summary>
/// 对话框控件：遮罩 + 居中内容卡 + 确认/取消按钮（WinUI 3 手写，core codegen 不生成组件体）。
///
/// 属性与 <c>lh-ui-core/specs/dialog.json</c> 逐字对齐；行为遵循 <c>cases/dialog.json</c>
/// 的 8 条黄金用例（已翻译为 xUnit 测试，见 tests/LhDialogTests.cs）。
///
/// 声明式可见性：<see cref="Visible"/> 为 false 时不渲染任何内容；确认/取消后触发
/// <see cref="Close"/>，宿主据此置 Visible 为 false。也可通过 <see cref="LhDialogController"/>
/// 命令式调用（与 toast 同构的句柄模式）。
/// </summary>
public sealed class LhDialog : UserControl
{
    private readonly Rectangle _overlay;
    private readonly Border _card;
    private readonly TextBlock _title;
    private readonly TextBlock _content;
    private readonly LhButton _cancelButton;
    private readonly LhButton _confirmButton;

    /// <summary>确认时触发</summary>
    public event Action? Confirm;

    /// <summary>取消时触发</summary>
    public event Action? Cancel;

    /// <summary>确认/取消后或（closable 时）点击遮罩触发，宿主据此置 Visible 为 false</summary>
    public event Action? Close;

    public static readonly DependencyProperty VisibleProperty = DependencyProperty.Register(
        nameof(Visible), typeof(bool), typeof(LhDialog),
        new PropertyMetadata(false, OnVisibleChanged));

    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title), typeof(string), typeof(LhDialog),
        new PropertyMetadata(string.Empty, OnTitleChanged));

    // new：Content 是 spec 的内容文本插槽（string），与 UserControl.Content（视觉根 UIElement）同名；
    // 视觉根改用 base.Content 赋值，两者互不干扰
    public static new readonly DependencyProperty ContentProperty = DependencyProperty.Register(
        nameof(Content), typeof(string), typeof(LhDialog),
        new PropertyMetadata(string.Empty, OnContentChanged));

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type), typeof(LhDialogType), typeof(LhDialog),
        new PropertyMetadata(LhDialogType.Default, OnAppearanceChanged));

    public static readonly DependencyProperty ConfirmTextProperty = DependencyProperty.Register(
        nameof(ConfirmText), typeof(string), typeof(LhDialog),
        new PropertyMetadata("确认", OnButtonsChanged));

    public static readonly DependencyProperty CancelTextProperty = DependencyProperty.Register(
        nameof(CancelText), typeof(string), typeof(LhDialog),
        new PropertyMetadata("取消", OnButtonsChanged));

    public static readonly DependencyProperty ClosableProperty = DependencyProperty.Register(
        nameof(Closable), typeof(bool), typeof(LhDialog),
        new PropertyMetadata(false, OnClosableChanged));

    public bool Visible { get => (bool)GetValue(VisibleProperty); set => SetValue(VisibleProperty, value); }
    public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    public new string Content { get => (string)GetValue(ContentProperty); set => SetValue(ContentProperty, value); }
    public LhDialogType Type { get => (LhDialogType)GetValue(TypeProperty); set => SetValue(TypeProperty, value); }
    public string ConfirmText { get => (string)GetValue(ConfirmTextProperty); set => SetValue(ConfirmTextProperty, value); }
    public string CancelText { get => (string)GetValue(CancelTextProperty); set => SetValue(CancelTextProperty, value); }
    public bool Closable { get => (bool)GetValue(ClosableProperty); set => SetValue(ClosableProperty, value); }

    public LhDialog()
    {
        _title = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            TextWrapping = TextWrapping.Wrap,
        };
        _content = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            TextWrapping = TextWrapping.Wrap,
        };

        // 取消按钮固定 default 类型；确认按钮按 type 映射语义色
        _cancelButton = new LhButton { Type = LhButtonType.Default };
        _cancelButton.Click += OnCancelClick;
        _confirmButton = new LhButton();
        _confirmButton.Click += OnConfirmClick;

        var buttons = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Spacing = LhEpx.Px(LhTokens.Dialog.Gap),
        };
        buttons.Children.Add(_cancelButton);
        buttons.Children.Add(_confirmButton);

        var cardContent = new StackPanel
        {
            Orientation = Orientation.Vertical,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Spacing = LhEpx.Px(LhTokens.Dialog.Gap),
        };
        cardContent.Children.Add(_title);
        cardContent.Children.Add(_content);
        cardContent.Children.Add(buttons);

        _card = new Border { Child = cardContent };
        _overlay = new Rectangle { Fill = new SolidColorBrush(LhColor.Transparent) };
        _overlay.Tapped += OnOverlayTapped;

        var root = new Grid();
        root.Children.Add(_overlay);
        root.Children.Add(_card);
        // UserControl.Content 是 UIElement（视觉根），而本控件按 spec 对齐另有一个
        // string Content 属性（内容文本插槽）；两者是不同的依赖属性，视觉根走 base.Content
        base.Content = root;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        ApplyAppearance();
        UpdateVisibility();
    }

    /// <summary>按当前属性与主题色板解析外观（测试与组件共用同一条解析路径）</summary>
    public LhDialogAppearance ResolveAppearance() =>
        LhDialogTokens.Resolve(Type, LhTheme.Current);

    private void ApplyAppearance()
    {
        var a = ResolveAppearance();

        // 遮罩：closable 为 true 时点击触发 Close，否则吞掉遮罩点击
        _overlay.Fill = new SolidColorBrush(a.OverlayColor);

        // 内容卡：宽度/圆角/内边距/底色/投影
        _card.Width = LhEpx.Px(a.Width);
        _card.Background = new SolidColorBrush(a.ContainerColor);
        _card.CornerRadius = new CornerRadius(a.BorderRadius);
        _card.Padding = new Thickness(
            LhEpx.Px(a.PaddingHorizontal),
            LhEpx.Px(a.PaddingVertical),
            LhEpx.Px(a.PaddingHorizontal),
            LhEpx.Px(a.PaddingVertical));
        _card.HorizontalAlignment = HorizontalAlignment.Center;
        _card.VerticalAlignment = VerticalAlignment.Center;
        // WinUI 3 的 Border 无 Elevation 属性（那是 Compose 概念）；投影走 ThemeShadow，
        // 以 z 轴 Translation 产生深度。令牌的 BlurRadius 近似作 z 深度消费（投影半径自动换算）。
        _card.Shadow = new ThemeShadow();
        _card.Translation = new Vector3(0, 0, (float)LhTokens.Shadow.Base.BlurRadius);
        Canvas.SetZIndex(_card, a.ZIndexModal);
        Canvas.SetZIndex(_overlay, a.ZIndexBackdrop);

        var font = new FontFamily(LhTokens.Typography.FontFamily);
        _title.Text = Title;
        _title.FontFamily = font;
        _title.FontSize = LhEpx.Px(a.TitleFontSize);
        _title.Foreground = new SolidColorBrush(a.TitleColor);

        _content.Text = Content;
        _content.FontFamily = font;
        _content.FontSize = LhEpx.Px(a.ContentFontSize);
        _content.Foreground = new SolidColorBrush(a.ContentColor);

        _confirmButton.Type = a.ConfirmButtonType;
        _confirmButton.Text = ConfirmText;
        _cancelButton.Text = CancelText;
    }

    /// <summary>visible 为 false 时不渲染任何内容（黄金用例 hidden-not-rendered）</summary>
    private void UpdateVisibility()
    {
        Visibility = Visible ? Visibility.Visible : Visibility.Collapsed;
        _title.Visibility = string.IsNullOrEmpty(Title) ? Visibility.Collapsed : Visibility.Visible;
        _content.Visibility = string.IsNullOrEmpty(Content) ? Visibility.Collapsed : Visibility.Visible;
    }

    private void OnConfirmClick()
    {
        // 确认触发 onConfirm，同时触发 onClose（宿主据此隐藏）
        Confirm?.Invoke();
        Close?.Invoke();
    }

    private void OnCancelClick()
    {
        // 取消触发 onCancel，同时触发 onClose
        Cancel?.Invoke();
        Close?.Invoke();
    }

    private void OnOverlayTapped(object sender, TappedRoutedEventArgs e)
    {
        // closable 为 true 时点击遮罩触发 onClose；否则吞掉遮罩点击
        if (Closable)
        {
            Close?.Invoke();
        }
        e.Handled = true;
    }

    private void OnThemeChanged(object? sender, EventArgs e) => ApplyAppearance();
    private void OnLoaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged += OnThemeChanged;
    private void OnUnloaded(object sender, RoutedEventArgs e) => LhTheme.ThemeChanged -= OnThemeChanged;

    private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhDialog)d).ApplyAppearance();

    private static void OnVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LhDialog)d).UpdateVisibility();

    private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhDialog)d;
        self._title.Text = e.NewValue as string ?? string.Empty;
        self.UpdateVisibility();
    }

    private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhDialog)d;
        self._content.Text = e.NewValue as string ?? string.Empty;
        self.UpdateVisibility();
    }

    private static void OnButtonsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (LhDialog)d;
        self._confirmButton.Text = self.ConfirmText;
        self._cancelButton.Text = self.CancelText;
        // 为空时不显示对应按钮
        self._confirmButton.Visibility = string.IsNullOrEmpty(self.ConfirmText) ? Visibility.Collapsed : Visibility.Visible;
        self._cancelButton.Visibility = string.IsNullOrEmpty(self.CancelText) ? Visibility.Collapsed : Visibility.Visible;
    }

    private static void OnClosableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // closable 只影响遮罩点击行为，无视觉变化
    }
}
