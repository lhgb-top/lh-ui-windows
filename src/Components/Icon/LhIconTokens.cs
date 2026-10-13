using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Icon;

/// <summary>
/// 图标尺寸。枚举名与 <c>lh-ui-core/specs/icon.json</c> 的 prop <c>size</c> 对齐（R-n5），
/// 映射到 icon.size.* 尺寸令牌。
/// </summary>
public enum LhIconSize
{
    Small,
    Medium,
    Large,
}

/// <summary>
/// 图标色调。枚举名与 spec 的 prop <c>tone</c> 对齐：
/// default 取主要文字色；语义色取对应 default；regular/secondary 取对应文字色。
/// </summary>
public enum LhIconTone
{
    Default,
    Primary,
    Success,
    Warning,
    Danger,
    Info,
    Regular,
    Secondary,
}

/// <summary>
/// 图标外观解析结果。由 <see cref="LhIconTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。
///
/// Glyph 是「绘制指令为 draw_icon」的端侧落点：Windows 端把图标名映射为 unicode 字形、
/// 用 TextBlock 渲染（与 button 的占位策略一致）。生产环境应换 FontIcon（Segoe Fluent Icons），
/// 此处保持务实占位。
/// </summary>
public sealed record LhIconAppearance
{
    /// <summary>图标盒尺寸（epx）</summary>
    public double Size { get; init; }
    /// <summary>字形颜色</summary>
    public Color Color { get; init; }
    /// <summary>实际渲染的 unicode 字形；未登记名回退 ◆</summary>
    public string Glyph { get; init; } = null!;
}

/// <summary>
/// 图标令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/icon.json</c> 的 behaviors：
/// - size 映射：small/medium/large 分别取 icon.size.&lt;size&gt; 尺寸令牌
/// - tone 映射：default 取 color.text.primary；语义色取对应 default；regular/secondary 取对应文字色
/// - 图标名经 GlyphFor 映射为本地字形，未登记名回退 ◆
/// </summary>
public static class LhIconTokens
{
    /// <summary>解析图标外观（黄金用例的断言入口）</summary>
    public static LhIconAppearance Resolve(LhIconSize size, LhIconTone tone, string name, LhColors colors)
    {
        var dimension = size switch
        {
            LhIconSize.Small => LhTokens.Icon.Size.Small,
            LhIconSize.Large => LhTokens.Icon.Size.Large,
            _ => LhTokens.Icon.Size.Medium,
        };

        var color = tone switch
        {
            LhIconTone.Default => colors.TextPrimary,
            LhIconTone.Primary => colors.Primary,
            LhIconTone.Success => colors.Success,
            LhIconTone.Warning => colors.Warning,
            LhIconTone.Danger => colors.Danger,
            LhIconTone.Info => colors.Info,
            LhIconTone.Regular => colors.TextRegular,
            _ => colors.TextSecondary,
        };

        return new LhIconAppearance
        {
            Size = dimension,
            Color = color,
            Glyph = GlyphFor(name),
        };
    }

    /// <summary>
    /// 图标名 → unicode 字形（端侧落点）。
    /// 未登记的图标名回退 ◆，不崩溃。生产环境应换 FontIcon（Segoe Fluent Icons）。
    /// </summary>
    public static string GlyphFor(string? name)
    {
        if (name is null) return "◆";
        return name switch
        {
            "search" => "⌕",
            "refresh" => "↻",
            "check" => "✓",
            "close" => "✕",
            "warning" => "⚠",
            "info" => "ℹ",
            "back" => "←",
            "more" => "⋯",
            "star" => "★",
            "edit" => "✎",
            "delete" => "⌫",
            "plus" => "＋",
            "minus" => "－",
            "arrow" => "→",
            _ => "◆",
        };
    }
}
