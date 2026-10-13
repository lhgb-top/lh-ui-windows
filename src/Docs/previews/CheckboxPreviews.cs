using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Checkbox;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Checkbox 真交互预览。方法名与 <c>docs/components/checkbox.md</c> 的 section 标题经 SCENE_MAP 对应。
/// </summary>
internal static class CheckboxPreviews
{
    /// <summary>## 基础用法</summary>
    public static UIElement Basic(DocPreviewScope scope)
    {
        var checkbox = new LhCheckbox { Label = "未选中" };
        checkbox.Change += value =>
        {
            checkbox.Label = value ? "已选中" : "未选中";
            scope.ShowToast($"Change：{value}");
        };

        return PreviewLayout.Column(
            checkbox,
            new LhCheckbox { Label = "默认选中", Checked = true });
    }

    /// <summary>## 尺寸</summary>
    public static UIElement Sizes(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            PreviewLayout.Row(
                new LhCheckbox { Size = LhCheckboxSize.Small, Label = "small" },
                new LhCheckbox { Size = LhCheckboxSize.Medium, Label = "medium（默认）" },
                new LhCheckbox { Size = LhCheckboxSize.Large, Label = "large" }));
    }

    /// <summary>## 状态</summary>
    public static UIElement States(DocPreviewScope scope)
    {
        var indeterminate = new LhCheckbox
        {
            Label = "Indeterminate 半选",
            Indeterminate = true,
        };
        indeterminate.Change += value => scope.ShowToast($"半选项 Change：{value}");

        return PreviewLayout.Column(
            indeterminate,
            new LhCheckbox { Label = "禁用态（未选）", Disabled = true },
            new LhCheckbox { Label = "禁用态（已选）", Disabled = true, Checked = true });
    }
}
