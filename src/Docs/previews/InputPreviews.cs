using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Button;
using Top.Lhgb.Ui.Components.Input;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Input 真交互预览。方法名与 <c>docs/components/input.md</c> 的 section 标题经 SCENE_MAP 对应。
/// </summary>
internal static class InputPreviews
{
    /// <summary>## 基础用法</summary>
    public static UIElement Basic(DocPreviewScope scope)
    {
        var input = new LhInput { Placeholder = "请输入内容" };
        input.ValueChange += value => scope.ShowToast($"ValueChange：{value}");
        input.Blur += () => scope.ShowToast("Blur 失焦事件");

        return PreviewLayout.Column(
            input,
            new LhInput { Value = "预设值（value 插槽）", Placeholder = "请输入内容" });
    }

    /// <summary>## 尺寸</summary>
    public static UIElement Sizes(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhInput { Size = LhInputSize.Small, Placeholder = "small" },
            new LhInput { Size = LhInputSize.Medium, Placeholder = "medium（默认）" },
            new LhInput { Size = LhInputSize.Large, Placeholder = "large" });
    }

    /// <summary>## 类型与状态</summary>
    public static UIElement TypesAndStates(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhInput { Type = LhInputType.Text, Placeholder = "text（默认）" },
            new LhInput { Type = LhInputType.Password, Placeholder = "password" },
            new LhInput { Placeholder = "禁用态", Disabled = true, Value = "不可编辑" },
            new LhInput { Placeholder = "只读态", Readonly = true, Value = "只读内容" });
    }

    /// <summary>## 清除与字数</summary>
    public static UIElement ClearAndCount(DocPreviewScope scope)
    {
        var clearable = new LhInput
        {
            Placeholder = "Clearable 输入后出现清除符",
            Clearable = true,
            Value = "试着点 ✕ 清除",
        };
        clearable.ValueChange += value => scope.ShowToast($"ValueChange：{value}");

        return PreviewLayout.Column(
            clearable,
            new LhInput
            {
                Placeholder = "Maxlength 限制 10 字",
                Maxlength = 10,
                Value = "1234567890",
            });
    }
}
