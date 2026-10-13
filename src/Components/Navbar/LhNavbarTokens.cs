using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Navbar;

/// <summary>导航栏外观解析结果。由 <see cref="LhNavbarTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。</summary>
public sealed record LhNavbarAppearance
{
    /// <summary>高度（navbar.height）</summary>
    public double Height { get; init; }
    /// <summary>左右内边距（navbar.paddingHorizontal）</summary>
    public double PaddingHorizontal { get; init; }
    /// <summary>标题字号（navbar.titleFontSize）</summary>
    public double TitleFontSize { get; init; }
    /// <summary>左右图标尺寸（navbar.iconSize）</summary>
    public double IconSize { get; init; }
    /// <summary>底部分隔线宽（navbar.borderWidth）；bordered 为 false 时为 0</summary>
    public double BorderWidth { get; init; }
    /// <summary>底色（color.background.default）</summary>
    public Color ContainerColor { get; init; }
    /// <summary>标题与两侧文字色（color.text.primary）</summary>
    public Color TitleColor { get; init; }
    /// <summary>底部分隔线色（color.border.lighter）；bordered 为 false 时为 null</summary>
    public Color? DividerColor { get; init; }
    /// <summary>层级（zIndex.sticky）</summary>
    public int ZIndex { get; init; }
}

/// <summary>
/// 导航栏令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/navbar.json</c> 的 behaviors：
/// - 高度 navbar.height，左右内边距 navbar.paddingHorizontal，底色 color.background.default
/// - 标题取 navbar.titleFontSize + color.text.primary，水平居中于可用宽度
/// - 左侧文字取 color.text.primary，图标尺寸 navbar.iconSize；右侧同之
/// - bordered 为 true 时底部绘制分隔线（color.border.lighter，宽 navbar.borderWidth）
/// - 层级取 zIndex.sticky，宿主将其作为页面顶部固定栏
/// </summary>
public static class LhNavbarTokens
{
    /// <summary>解析导航栏外观（黄金用例的断言入口）</summary>
    public static LhNavbarAppearance Resolve(bool bordered, LhColors colors)
    {
        return new LhNavbarAppearance
        {
            Height = LhTokens.Navbar.Height,
            PaddingHorizontal = LhTokens.Navbar.PaddingHorizontal,
            TitleFontSize = LhTokens.Navbar.TitleFontSize,
            IconSize = LhTokens.Navbar.IconSize,
            BorderWidth = bordered ? LhTokens.Navbar.BorderWidth : 0,
            ContainerColor = colors.BackgroundDefault,
            TitleColor = colors.TextPrimary,
            DividerColor = bordered ? colors.BorderLighter : null,
            ZIndex = LhTokens.ZIndex.Sticky,
        };
    }
}
