using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Radio;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Radio 真交互预览。方法名与 <c>docs/components/radio.md</c> 的 section 标题经 SCENE_MAP 对应。
///
/// 单选语义由宿主维护：本库 Radio 是受控组件，Checked 由外部状态驱动，
/// 预览里手工模拟「同组互斥」展示 Change 的用法（真组逻辑在业务层）。
/// </summary>
internal static class RadioPreviews
{
    /// <summary>## 基础用法</summary>
    public static UIElement Basic(DocPreviewScope scope)
    {
        var optionA = new LhRadio { Label = "选项 A", Value = "a", Checked = true };
        var optionB = new LhRadio { Label = "选项 B", Value = "b" };
        var optionC = new LhRadio { Label = "选项 C", Value = "c" };

        LhRadio[] group = { optionA, optionB, optionC };
        foreach (var radio in group)
        {
            radio.Change += value =>
            {
                if (!value) return;
                foreach (var other in group) other.Checked = other == radio;
                scope.ShowToast($"选中：{radio.Label}（value = {radio.Value}）");
            };
        }

        return PreviewLayout.Column(group);
    }

    /// <summary>## 尺寸</summary>
    public static UIElement Sizes(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            PreviewLayout.Row(
                new LhRadio { Size = LhRadioSize.Small, Label = "small", Checked = true },
                new LhRadio { Size = LhRadioSize.Medium, Label = "medium（默认）", Checked = true },
                new LhRadio { Size = LhRadioSize.Large, Label = "large", Checked = true }));
    }

    /// <summary>## 状态</summary>
    public static UIElement States(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhRadio { Label = "禁用态（未选）", Disabled = true },
            new LhRadio { Label = "禁用态（已选）", Disabled = true, Checked = true });
    }
}
