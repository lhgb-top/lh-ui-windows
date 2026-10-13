using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Button;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Button 真交互预览（WinUI 3 手写）。方法名与 <c>docs/components/button.md</c> 的
/// section 标题经 SCENE_MAP 一一对应，签名固定为 (DocPreviewScope) => UIElement。
/// </summary>
internal static class ButtonPreviews
{
    /// <summary>## 类型：六种类型，点击有计数反馈</summary>
    public static UIElement Types(DocPreviewScope scope)
    {
        var panel = PreviewLayout.Column();
        var types = new[] { LhButtonType.Default, LhButtonType.Primary, LhButtonType.Success, LhButtonType.Warning, LhButtonType.Danger, LhButtonType.Info };
        var labels = new[] { "默认", "主要", "成功", "警告", "危险", "信息" };
        for (var i = 0; i < types.Length; i += 2)
        {
            var row = PreviewLayout.Row();
            for (var j = i; j < i + 2; j++)
            {
                var type = types[j];
                var button = new LhButton { Type = type, Text = labels[j] + "按钮" };
                var count = 0;
                button.Click += () => scope.ShowToast($"点击了 {labels[j]}按钮（{++count}）");
                row.Children.Add(button);
            }
            panel.Children.Add(row);
        }
        return panel;
    }

    /// <summary>## 尺寸：三档</summary>
    public static UIElement Sizes(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            PreviewLayout.Row(
                SizeButton(scope, LhButtonSize.Small, "小型"),
                SizeButton(scope, LhButtonSize.Medium, "中型"),
                SizeButton(scope, LhButtonSize.Large, "大型")));
    }

    private static LhButton SizeButton(DocPreviewScope scope, LhButtonSize size, string label)
    {
        var button = new LhButton { Size = size, Text = label + "按钮" };
        var height = size switch
        {
            LhButtonSize.Small => LhTokens.Button.Height.Small,
            LhButtonSize.Large => LhTokens.Button.Height.Large,
            _ => LhTokens.Button.Height.Medium,
        };
        button.Click += () => scope.ShowToast($"{label}按钮，height = {height}epx");
        return button;
    }

    /// <summary>## 状态：禁用与加载中都不触发 Click，共存时禁用优先</summary>
    public static UIElement States(DocPreviewScope scope)
    {
        var normal = new LhButton { Type = LhButtonType.Primary, Text = "正常" };
        normal.Click += () => scope.ShowToast("正常按钮可点击");

        var disabled = new LhButton { Type = LhButtonType.Primary, Text = "禁用", Disabled = true };
        disabled.Click += () => scope.ShowToast("这不应出现：禁用按钮触发了 Click");

        var loading = new LhButton { Type = LhButtonType.Primary, Text = "加载中", Loading = true };
        loading.Click += () => scope.ShowToast("这不应出现：加载按钮触发了 Click");

        var both = new LhButton { Type = LhButtonType.Primary, Text = "禁用优先", Disabled = true, Loading = true };
        both.Click += () => scope.ShowToast("这不应出现：共存态触发了 Click");

        return PreviewLayout.Column(
            PreviewLayout.Row(normal, disabled),
            PreviewLayout.Row(loading, both));
    }

    /// <summary>## 图标与花纹底</summary>
    public static UIElement IconAndPattern(DocPreviewScope scope)
    {
        var search = new LhButton { Type = LhButtonType.Primary, Icon = "search", Text = "搜索" };
        search.Click += () => scope.ShowToast("search 图标按钮");

        var plain = new LhButton { Icon = "refresh", Text = "刷新" };
        plain.Click += () => scope.ShowToast("refresh 图标按钮");

        var patternPrimary = new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = "主要花纹底" };
        var patternDefault = new LhButton { BgPattern = true, Text = "默认花纹底" };

        return PreviewLayout.Column(
            PreviewLayout.Row(search, plain),
            PreviewLayout.Row(patternPrimary, patternDefault));
    }

    /// <summary>## 明暗主题：随宿主右上角切换实时重绘</summary>
    public static UIElement Theme(DocPreviewScope scope)
    {
        var primary = new LhButton { Type = LhButtonType.Primary, Text = "主要按钮" };
        primary.Click += () => scope.ShowToast("当前主题下的主色按钮");
        return PreviewLayout.Column(
            PreviewLayout.Row(primary, new LhButton { Text = "默认按钮" }),
            PreviewLayout.Row(
                new LhButton { Type = LhButtonType.Success, Text = "成功" },
                new LhButton { Type = LhButtonType.Danger, Text = "危险" }));
    }
}
