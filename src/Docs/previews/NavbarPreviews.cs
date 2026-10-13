using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Navbar;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Navbar 真交互预览。方法名与 <c>docs/components/navbar.md</c> 的 section 标题经 SCENE_MAP 对应。
/// </summary>
internal static class NavbarPreviews
{
    /// <summary>## 基础用法</summary>
    public static UIElement Basic(DocPreviewScope scope)
    {
        var navbar = new LhNavbar
        {
            Title = "页面标题",
            LeftText = "返回",
            RightText = "更多",
        };
        navbar.LeftClick += () => scope.ShowToast("LeftClick：返回（宿主里通常做出栈）");
        navbar.RightClick += () => scope.ShowToast("RightClick：更多");

        return PreviewLayout.Column(navbar);
    }

    /// <summary>## 图标</summary>
    public static UIElement Icons(DocPreviewScope scope)
    {
        var navbar = new LhNavbar
        {
            Title = "图标导航栏",
            LeftIcon = "back",
            RightIcon = "search",
        };
        navbar.LeftClick += () => scope.ShowToast("LeftIcon 点击");
        navbar.RightClick += () => scope.ShowToast("RightIcon 点击");

        return PreviewLayout.Column(navbar);
    }

    /// <summary>## 分隔线</summary>
    public static UIElement Divider(DocPreviewScope scope)
    {
        return PreviewLayout.Column(
            new LhNavbar { Title = "带分隔线（Bordered = true，默认）", Bordered = true },
            new LhNavbar { Title = "无分隔线（Bordered = false）", Bordered = false });
    }
}
