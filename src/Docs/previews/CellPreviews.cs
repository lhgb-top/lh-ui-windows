using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Cell;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Cell 真交互预览。方法名与 <c>docs/components/cell.md</c> 的 section 标题经 SCENE_MAP 对应。
/// </summary>
internal static class CellPreviews
{
    /// <summary>## 基础用法</summary>
    public static UIElement Basic(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhCell { Title = "标题文字" },
            new LhCell { Title = "标题文字", Label = "说明文字（label 插槽）" },
            new LhCell { Title = "标题文字", Value = "右侧值（value 插槽）" });
    }

    /// <summary>## 图标与必填</summary>
    public static UIElement IconAndRequired(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhCell { Title = "带图标", Icon = "search" },
            new LhCell { Title = "必填项", Required = true, Value = "请填写" },
            new LhCell { Title = "图标 + 必填", Icon = "check", Required = true });
    }

    /// <summary>## 分隔线与点击</summary>
    public static UIElement DividerAndClick(DocPreviewScope scope)
    {
        var clickable = new LhCell
        {
            Title = "可点击单元格",
            Value = "点击触发 Click",
            Clickable = true,
        };
        clickable.Click += () => scope.ShowToast("Cell 的 Click 事件触发（Clickable = true）");

        return PreviewLayout.Column(
            new LhCell { Title = "默认带分隔线（Bordered = true）" },
            new LhCell { Title = "无分隔线（Bordered = false）", Bordered = false },
            clickable);
    }

    /// <summary>## 明暗主题</summary>
    public static UIElement Theme(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhCell { Title = "标题文字", Label = "说明文字", Value = "右侧值" },
            new LhCell { Title = "必填项", Required = true, Icon = "search", Value = "值" });
    }
}
