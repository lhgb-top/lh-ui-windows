using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Tag;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Tag 真交互预览。方法名与 <c>docs/components/tag.md</c> 的 section 标题经 SCENE_MAP 对应。
/// </summary>
internal static class TagPreviews
{
    /// <summary>## 类型</summary>
    public static UIElement Types(DocPreviewScope scope)
    {
        var types = new[] { LhTagType.Default, LhTagType.Primary, LhTagType.Success, LhTagType.Warning, LhTagType.Danger, LhTagType.Info };
        var labels = new[] { "默认", "主要", "成功", "警告", "危险", "信息" };
        var panel = PreviewLayout.Column();
        for (var i = 0; i < types.Length; i += 3)
        {
            var row = PreviewLayout.Row();
            for (var j = i; j < i + 3; j++)
            {
                var tag = new LhTag { Type = types[j], Text = labels[j] };
                var t = types[j];
                tag.Tapped += (s, e) => scope.ShowToast($"type = {t}");
                row.Children.Add(tag);
            }
            panel.Children.Add(row);
        }
        return panel;
    }

    /// <summary>## 描边与圆角</summary>
    public static UIElement BorderAndRadius(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            PreviewLayout.Row(
                new LhTag { Type = LhTagType.Primary, Text = "实底" },
                new LhTag { Type = LhTagType.Primary, Plain = true, Text = "描边" }),
            PreviewLayout.Row(
                new LhTag { Type = LhTagType.Primary, Text = "默认圆角" },
                new LhTag { Type = LhTagType.Primary, Round = true, Text = "圆角胶囊" }));
    }

    /// <summary>## 尺寸与关闭</summary>
    public static UIElement SizesAndClose(DocPreviewScope scope)
    {
        var small = new LhTag { Size = LhTagSize.Small, Type = LhTagType.Primary, Text = "small" };
        var medium = new LhTag { Size = LhTagSize.Medium, Type = LhTagType.Primary, Text = "medium（默认）" };
        var large = new LhTag { Size = LhTagSize.Large, Type = LhTagType.Primary, Text = "large" };

        var closable = new LhTag { Type = LhTagType.Primary, Text = "可关闭", Closable = true };
        closable.Close += () => scope.ShowToast("Close 事件触发（标签已关闭）");

        return PreviewLayout.Column(
            PreviewLayout.Row(small, medium, large),
            PreviewLayout.Row(closable));
    }

    /// <summary>## 明暗主题</summary>
    public static UIElement Theme(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            PreviewLayout.Row(
                new LhTag { Type = LhTagType.Primary, Text = "主要" },
                new LhTag { Type = LhTagType.Success, Text = "成功" },
                new LhTag { Type = LhTagType.Danger, Text = "危险" }),
            PreviewLayout.Row(
                new LhTag { Type = LhTagType.Primary, Plain = true, Text = "描边" }));
    }
}
