using Microsoft.UI;
using Windows.UI;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Text;

/// <summary>字号级别。枚举名与 <c>lh-ui-core/specs/text.json</c> 的 prop <c>level</c> 对齐（R-n5），映射到 typography.fontSize.* 令牌。</summary>
public enum LhTextLevel
{
    ExtraLarge,
    Large,
    Medium,
    Base,
    Small,
    ExtraSmall,
}

/// <summary>字重。枚举名与 spec 的 prop <c>weight</c> 对齐，映射到 typography.fontWeight.* 令牌。</summary>
public enum LhTextWeight
{
    Primary,
    Bold,
}

/// <summary>
/// 文字色调。枚举名与 spec 的 prop <c>tone</c> 对齐：
/// primary/regular/secondary/placeholder/disabled 取 color.text.*；selected 取 color.selected。
/// </summary>
public enum LhTextTone
{
    Primary,
    Regular,
    Secondary,
    Placeholder,
    Disabled,
    Selected,
}

/// <summary>水平对齐方式。枚举名与 spec 的 prop <c>align</c> 对齐（对齐不是设计令牌，映射到框架枚举）。</summary>
public enum LhTextAlign
{
    Start,
    Center,
    End,
}

/// <summary>
/// 文本外观解析结果。由 <see cref="LhTextTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。
/// 字号/字重/行高/文字色全部取自令牌或色板，无字面量（红线 1）。
/// </summary>
public sealed record LhTextAppearance
{
    /// <summary>字号（epx）</summary>
    public double FontSize { get; init; }
    /// <summary>字重（令牌数值）</summary>
    public int FontWeight { get; init; }
    /// <summary>行高（typography.lineHeight.primary，行盒高度 = fontSize × lineHeight）</summary>
    public double LineHeight { get; init; }
    /// <summary>文字颜色</summary>
    public Color FontColor { get; init; }
}

/// <summary>
/// 文本令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/text.json</c> 的 behaviors：
/// - level 映射：extraLarge…extraSmall 分别取 typography.fontSize.&lt;level&gt;
/// - weight 映射：primary/bold 分别取 typography.fontWeight.&lt;weight&gt;
/// - tone 映射：取 color.text.&lt;tone&gt;；selected 取 color.selected
/// - 行高取 typography.lineHeight.primary，layout 高度 = 字号 × 行高
/// </summary>
public static class LhTextTokens
{
    /// <summary>解析文本外观（黄金用例的断言入口）</summary>
    public static LhTextAppearance Resolve(LhTextLevel level, LhTextWeight weight, LhTextTone tone, LhColors colors)
    {
        var fontSize = level switch
        {
            LhTextLevel.ExtraLarge => LhTokens.Typography.FontSize.ExtraLarge,
            LhTextLevel.Large => LhTokens.Typography.FontSize.Large,
            LhTextLevel.Medium => LhTokens.Typography.FontSize.Medium,
            LhTextLevel.Small => LhTokens.Typography.FontSize.Small,
            LhTextLevel.ExtraSmall => LhTokens.Typography.FontSize.ExtraSmall,
            _ => LhTokens.Typography.FontSize.Base,
        };

        var fontWeight = weight == LhTextWeight.Bold
            ? LhTokens.Typography.FontWeight.Bold
            : LhTokens.Typography.FontWeight.Primary;

        var fontColor = tone switch
        {
            LhTextTone.Primary => colors.TextPrimary,
            LhTextTone.Regular => colors.TextRegular,
            LhTextTone.Secondary => colors.TextSecondary,
            LhTextTone.Placeholder => colors.TextPlaceholder,
            LhTextTone.Disabled => colors.TextDisabled,
            _ => colors.Selected,
        };

        return new LhTextAppearance
        {
            FontSize = fontSize,
            FontWeight = fontWeight,
            LineHeight = LhTokens.Typography.LineHeight.Primary,
            FontColor = fontColor,
        };
    }

    /// <summary>
    /// 对齐枚举 → WinUI 的 TextAlignment（spec：align 决定文字在行盒宽度内的水平定位）。
    /// 对齐不是设计令牌，映射到框架枚举不违反红线 1（红线约束的是色值/间距/圆角/字号）。
    /// </summary>
    public static TextAlignment AlignFor(LhTextAlign align) => align switch
    {
        LhTextAlign.Center => TextAlignment.Center,
        LhTextAlign.End => TextAlignment.Right,
        _ => TextAlignment.Left,
    };
}
