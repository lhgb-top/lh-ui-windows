using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// 预览内布局助手：纵向/横向成行排列真实控件，间距一律取 spacing 令牌（预览零字面量）。
/// </summary>
internal static class PreviewLayout
{
    /// <summary>纵向成列（场景内多行演示）</summary>
    public static StackPanel Column(params UIElement[] children)
    {
        var panel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Spacing = LhTokens.Spacing.Md,
        };
        foreach (var c in children) panel.Children.Add(c);
        return panel;
    }

    /// <summary>横向成行（同一维度并列多个控件）</summary>
    public static StackPanel Row(params UIElement[] children)
    {
        var panel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = LhTokens.Spacing.Sm,
        };
        foreach (var c in children) panel.Children.Add(c);
        return panel;
    }

    /// <summary>纵向成列并居左（场景内成组演示）</summary>
    public static StackPanel ColumnLeft(params UIElement[] children)
    {
        var panel = Column(children);
        panel.HorizontalAlignment = HorizontalAlignment.Left;
        return panel;
    }
}
