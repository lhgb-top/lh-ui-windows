using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Switch;
using Top.Lhgb.Ui.Components.Text;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Switch 真交互预览。方法名与 <c>docs/components/switch.md</c> 的 section 标题经 SCENE_MAP 对应。
/// </summary>
internal static class SwitchPreviews
{
    /// <summary>## 基础用法</summary>
    public static UIElement Basic(DocPreviewScope scope)
    {
        var sw = new LhSwitch { Checked = true };
        sw.Change += value => scope.ShowToast($"Change：{value}");

        return PreviewLayout.Column(
            PreviewLayout.Row(new LhText { Text = "开" }, sw),
            PreviewLayout.Row(new LhText { Text = "关" }, new LhSwitch()));
    }

    /// <summary>## 尺寸</summary>
    public static UIElement Sizes(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            PreviewLayout.Row(
                new LhSwitch { Size = LhSwitchSize.Small, Checked = true },
                new LhText { Text = "small" }),
            PreviewLayout.Row(
                new LhSwitch { Size = LhSwitchSize.Medium, Checked = true },
                new LhText { Text = "medium（默认）" }),
            PreviewLayout.Row(
                new LhSwitch { Size = LhSwitchSize.Large, Checked = true },
                new LhText { Text = "large" }));
    }

    /// <summary>## 状态</summary>
    public static UIElement States(DocPreviewScope scope)
    {
        var loading = new LhSwitch { Checked = true, Loading = true };
        loading.Change += value => scope.ShowToast($"加载中开关 Change：{value}");

        return PreviewLayout.Column(
            PreviewLayout.Row(loading, new LhText { Text = "Loading 加载中（不响应点击）" }),
            PreviewLayout.Row(new LhSwitch { Disabled = true }, new LhText { Text = "禁用态（关）" }),
            PreviewLayout.Row(new LhSwitch { Disabled = true, Checked = true }, new LhText { Text = "禁用态（开）" }));
    }
}
