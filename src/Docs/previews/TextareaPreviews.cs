using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Textarea;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Textarea 真交互预览。方法名与 <c>docs/components/textarea.md</c> 的 section 标题经 SCENE_MAP 对应。
/// </summary>
internal static class TextareaPreviews
{
    /// <summary>## 基础用法</summary>
    public static UIElement Basic(DocPreviewScope scope)
    {
        var textarea = new LhTextarea { Placeholder = "请输入多行内容" };
        textarea.ValueChange += value => scope.ShowToast($"ValueChange：{value}");
        textarea.Blur += () => scope.ShowToast("Blur 失焦事件");

        return PreviewLayout.Column(
            textarea,
            new LhTextarea
            {
                Value = "预设值：\n第二行\n第三行",
                Placeholder = "请输入多行内容",
                Rows = 4,
            });
    }

    /// <summary>## 字数统计</summary>
    public static UIElement Count(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhTextarea
            {
                Placeholder = "ShowCount 显示字数统计",
                ShowCount = true,
                Rows = 3,
            },
            new LhTextarea
            {
                Placeholder = "ShowCount + Maxlength = 20",
                ShowCount = true,
                Maxlength = 20,
                Value = "字数统计与上限同开",
                Rows = 3,
            });
    }

    /// <summary>## 状态</summary>
    public static UIElement States(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhTextarea
            {
                Placeholder = "禁用态",
                Disabled = true,
                Value = "不可编辑",
                Rows = 3,
            },
            new LhTextarea
            {
                Placeholder = "只读态",
                Readonly = true,
                Value = "只读内容",
                Rows = 3,
            });
    }
}
