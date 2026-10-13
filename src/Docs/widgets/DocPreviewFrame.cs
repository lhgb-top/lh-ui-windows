using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Docs.Widgets;

/// <summary>
/// 预览框：对标网页模拟器的 <c>sim-phone</c> 手机框（状态栏 9:41 + 场景名），
/// 内部承载真 WinUI 3 交互预览。外观全部取 docs 令牌，零字面量。
/// </summary>
internal sealed class DocPreviewFrame : UserControl
{
    private readonly Grid _root;
    private readonly Border _statusbar;
    private readonly ContentControl _stage;

    /// <summary>场景名（md 的 section 标题），显示在状态栏右侧</summary>
    public static readonly DependencyProperty SceneProperty = DependencyProperty.Register(
        nameof(Scene), typeof(string), typeof(DocPreviewFrame),
        new PropertyMetadata(string.Empty, OnSceneChanged));

    /// <summary>预览内容（真 WinUI 3 控件树）</summary>
    public static readonly DependencyProperty PreviewProperty = DependencyProperty.Register(
        nameof(Preview), typeof(UIElement), typeof(DocPreviewFrame),
        new PropertyMetadata(null, OnPreviewChanged));

    public string Scene { get => (string)GetValue(SceneProperty); set => SetValue(SceneProperty, value); }
    public UIElement Preview { get => (UIElement)GetValue(PreviewProperty); set => SetValue(PreviewProperty, value); }

    public DocPreviewFrame()
    {
        _root = new Grid();
        _root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        _root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        // 状态栏：左侧 9:41，右侧场景名（对齐网页 sim-statusbar）
        var status = new Grid();
        status.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
        status.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        var time = new TextBlock
        {
            Text = "9:41",
            FontSize = DocPalette.ProseFontSize,
            Foreground = new SolidColorBrush(DocPalette.ProseSecondary),
            VerticalAlignment = VerticalAlignment.Center,
        };
        Grid.SetColumn(time, 0);
        status.Children.Add(time);

        _statusbar = new Border
        {
            Background = new SolidColorBrush(DocPalette.CardBackground),
            BorderBrush = new SolidColorBrush(DocPalette.CardBorder),
            BorderThickness = new Thickness(DocPalette.CardBorderWidth, DocPalette.CardBorderWidth, DocPalette.CardBorderWidth, 0),
            CornerRadius = new CornerRadius(DocPalette.CardRadius, DocPalette.CardRadius, 0, 0),
            Padding = new Thickness(DocPalette.CardPadding / 2, DocPalette.CellPaddingY / 2, DocPalette.CardPadding / 2, DocPalette.CellPaddingY / 2),
            Child = status,
        };
        var sceneName = new TextBlock
        {
            FontSize = DocPalette.ProseFontSize,
            Foreground = new SolidColorBrush(DocPalette.ProseSecondary),
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
        };
        Grid.SetColumn(sceneName, 1);
        status.Children.Add(sceneName);
        _sceneName = sceneName;

        Grid.SetRow(_statusbar, 0);
        _root.Children.Add(_statusbar);

        // 预览舞台：左侧填充对齐状态栏描边，预览自持交互
        _stage = new ContentControl
        {
            HorizontalContentAlignment = HorizontalAlignment.Stretch,
            VerticalContentAlignment = VerticalAlignment.Stretch,
            Padding = new Thickness(DocPalette.CardPadding),
            Background = new SolidColorBrush(DocPalette.CardBackground),
            BorderBrush = new SolidColorBrush(DocPalette.CardBorder),
            BorderThickness = new Thickness(DocPalette.CardBorderWidth, 0, DocPalette.CardBorderWidth, DocPalette.CardBorderWidth),
            CornerRadius = new CornerRadius(0, 0, DocPalette.CardRadius, DocPalette.CardRadius),
            MinHeight = LhTokens.Docs.Card.Padding * 3,
        };
        Grid.SetRow(_stage, 1);
        _root.Children.Add(_stage);

        Content = _root;
    }

    private readonly TextBlock _sceneName;

    private static void OnSceneChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DocPreviewFrame f) f._sceneName.Text = e.NewValue as string ?? string.Empty;
    }

    private static void OnPreviewChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DocPreviewFrame f) f._stage.Content = e.NewValue;
    }
}
