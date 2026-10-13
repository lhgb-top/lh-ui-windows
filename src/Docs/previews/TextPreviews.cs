using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Text;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Text 真交互预览。方法名与 <c>docs/components/text.md</c> 的 section 标题经 SCENE_MAP 对应。
/// </summary>
internal static class TextPreviews
{
    /// <summary>## 级别</summary>
    public static UIElement Levels(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhText { Level = LhTextLevel.ExtraLarge, Text = "ExtraLarge 大标题" },
            new LhText { Level = LhTextLevel.Large, Text = "Large 标题" },
            new LhText { Level = LhTextLevel.Medium, Text = "Medium 小标题" },
            new LhText { Level = LhTextLevel.Base, Text = "Base 正文（默认）" },
            new LhText { Level = LhTextLevel.Small, Text = "Small 辅助文字" },
            new LhText { Level = LhTextLevel.ExtraSmall, Text = "ExtraSmall 极小文字" });
    }

    /// <summary>## 字重与色调</summary>
    public static UIElement WeightAndTone(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            PreviewLayout.Row(
                new LhText { Weight = LhTextWeight.Primary, Text = "Primary 字重" },
                new LhText { Weight = LhTextWeight.Bold, Text = "Bold 加粗" }),
            PreviewLayout.Row(
                new LhText { Tone = LhTextTone.Primary, Text = "Primary 主色" },
                new LhText { Tone = LhTextTone.Secondary, Text = "Secondary 次级" },
                new LhText { Tone = LhTextTone.Placeholder, Text = "Placeholder 占位" }),
            PreviewLayout.Row(
                new LhText { Tone = LhTextTone.Disabled, Text = "Disabled 禁用" },
                new LhText { Tone = LhTextTone.Selected, Text = "Selected 选中" }));
    }

    /// <summary>## 对齐</summary>
    public static UIElement Alignment(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhText { Align = LhTextAlign.Start, Text = "Start 左对齐（默认）" },
            new LhText { Align = LhTextAlign.Center, Text = "Center 居中" },
            new LhText { Align = LhTextAlign.End, Text = "End 右对齐" });
    }

    /// <summary>## 明暗主题</summary>
    public static UIElement Theme(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhText { Level = LhTextLevel.Large, Text = "大标题随主题变色" },
            new LhText { Tone = LhTextTone.Secondary, Text = "次级文字随主题变色" });
    }
}
