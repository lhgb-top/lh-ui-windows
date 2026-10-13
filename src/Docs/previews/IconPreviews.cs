using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Top.Lhgb.Ui.Components.Icon;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Icon 真交互预览。方法名与 <c>docs/components/icon.md</c> 的 section 标题经 SCENE_MAP 对应。
/// </summary>
internal static class IconPreviews
{
    /// <summary>## 尺寸</summary>
    public static UIElement Sizes(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            PreviewLayout.Row(
                IconOf(scope, LhIconSize.Small, "small"),
                IconOf(scope, LhIconSize.Medium, "medium（默认）"),
                IconOf(scope, LhIconSize.Large, "large")));
    }

    private static LhIcon IconOf(DocPreviewScope scope, LhIconSize size, string label)
    {
        var icon = new LhIcon { Size = size, Name = "star" };
        icon.Tapped += (s, e) => scope.ShowToast($"{label} 图标");
        return icon;
    }

    /// <summary>## 色调</summary>
    public static UIElement Tone(DocPreviewScope scope)
    {
        var tones = new[] { LhIconTone.Regular, LhIconTone.Secondary, LhIconTone.Primary, LhIconTone.Success, LhIconTone.Warning, LhIconTone.Danger, LhIconTone.Info };
        var row = PreviewLayout.Row();
        foreach (var tone in tones)
        {
            var icon = new LhIcon { Tone = tone, Name = "star" };
            icon.Tapped += (s, e) => scope.ShowToast($"tone = {tone}");
            row.Children.Add(icon);
        }
        return PreviewLayout.Column(row);
    }

    /// <summary>## 图标名：本地图标表解析，未登记名回退通用字形</summary>
    public static UIElement IconNames(DocPreviewScope scope)
    {
        var names = new[] { "search", "refresh", "check", "close", "warning", "info", "back", "more", "star", "edit", "delete", "plus", "minus", "arrow" };
        var panel = PreviewLayout.Column();
        for (var i = 0; i < names.Length; i += 7)
        {
            var row = PreviewLayout.Row();
            for (var j = i; j < i + 7 && j < names.Length; j++)
            {
                var name = names[j];
                var icon = new LhIcon { Name = name };
                icon.Tapped += (s, e) => scope.ShowToast($"图标名 {name}");
                row.Children.Add(icon);
            }
            panel.Children.Add(row);
        }
        // 未登记名回退
        var fallback = new LhIcon { Name = "not-registered" };
        fallback.Tapped += (s, e) => scope.ShowToast("未登记名回退为 ◆");
        panel.Children.Add(PreviewLayout.Row(new TextBlock { Text = "not-registered →", VerticalAlignment = VerticalAlignment.Center }, fallback));
        return panel;
    }

    /// <summary>## 明暗主题</summary>
    public static UIElement Theme(DocPreviewScope scope)
    {
        var primary = new LhIcon { Tone = LhIconTone.Primary, Name = "star" };
        primary.Tapped += (s, e) => scope.ShowToast("当前主题下的主色图标");
        return PreviewLayout.Column(
            PreviewLayout.Row(
                new LhIcon { Name = "search" },
                new LhIcon { Name = "check" },
                primary,
                new LhIcon { Tone = LhIconTone.Secondary, Name = "edit" }));
    }
}
