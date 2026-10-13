using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Loading;
using Top.Lhgb.Ui.Components.Text;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Loading 真交互预览。方法名与 <c>docs/components/loading.md</c> 的 section 标题经 SCENE_MAP 对应。
/// </summary>
internal static class LoadingPreviews
{
    /// <summary>## 尺寸</summary>
    public static UIElement Sizes(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            PreviewLayout.Row(
                new LhLoading { Size = LhLoadingSize.Small },
                new LhText { Text = "small" }),
            PreviewLayout.Row(
                new LhLoading { Size = LhLoadingSize.Medium },
                new LhText { Text = "medium（默认）" }),
            PreviewLayout.Row(
                new LhLoading { Size = LhLoadingSize.Large },
                new LhText { Text = "large" }));
    }

    /// <summary>## 色调</summary>
    public static UIElement Tone(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            PreviewLayout.Row(
                new LhLoading { Tone = LhLoadingTone.Primary },
                new LhText { Text = "primary（默认）" }),
            PreviewLayout.Row(
                new LhLoading { Tone = LhLoadingTone.Success },
                new LhText { Text = "success" }),
            PreviewLayout.Row(
                new LhLoading { Tone = LhLoadingTone.Warning },
                new LhText { Text = "warning" }),
            PreviewLayout.Row(
                new LhLoading { Tone = LhLoadingTone.Danger },
                new LhText { Text = "danger" }),
            PreviewLayout.Row(
                new LhLoading { Tone = LhLoadingTone.Info },
                new LhText { Text = "info" }));
    }
}
