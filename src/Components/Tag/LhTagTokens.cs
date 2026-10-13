using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Tag;

/// <summary>
/// 标签语义类型。枚举名与 <c>lh-ui-core/specs/tag.json</c> 的 prop <c>type</c> 对齐（R-n5）。
/// 决定底色、文字色与描边色；plain 形态下取对应语义浅底 + 语义字 + 语义描边。
/// </summary>
public enum LhTagType
{
    Default,
    Primary,
    Success,
    Warning,
    Danger,
    Info,
}

/// <summary>标签尺寸。枚举名与 spec 的 prop <c>size</c> 对齐，映射到 tag.height / paddingHorizontal / fontSize 令牌。</summary>
public enum LhTagSize
{
    Small,
    Medium,
    Large,
}

/// <summary>标签外观解析结果。由 <see cref="LhTagTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。</summary>
public sealed record LhTagAppearance
{
    public double Height { get; init; }
    public double PaddingHorizontal { get; init; }
    public double FontSize { get; init; }
    /// <summary>圆角；round 为 true 时取高度一半（胶囊），否则取 tag.borderRadius</summary>
    public double BorderRadius { get; init; }
    public double BorderWidth { get; init; }
    public Color ContainerColor { get; init; }
    public Color ContentColor { get; init; }
    public Color BorderColor { get; init; }
    /// <summary>关闭符与文字的间距（spacing.xs）；closable 为 false 时无意义</summary>
    public double CloseGap { get; init; }
}

/// <summary>
/// 标签令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/tag.json</c> 的 behaviors：
/// - size 映射：small/medium/large 分别取 tag.height.&lt;size&gt; 等尺寸令牌
/// - plain 为 false 时：default 取 backgroundDefault 底 + textPrimary 字 + borderBase 描边；
///   其余类型取语义色实心底 + onPrimary 字、无描边
/// - plain 为 true 时：default 同上；其余类型取语义 bg 浅底 + 语义 default 字 + 语义 default 描边
/// - round 为 true 时圆角取高度一半，否则取 tag.borderRadius
/// - 关闭符与文字间距取 spacing.xs
/// </summary>
public static class LhTagTokens
{
    /// <summary>解析标签外观（黄金用例的断言入口）</summary>
    public static LhTagAppearance Resolve(LhTagType type, LhTagSize size, bool plain, bool round, LhColors colors)
    {
        var (height, paddingHorizontal, fontSize) = size switch
        {
            LhTagSize.Small => (LhTokens.Tag.Height.Small, LhTokens.Tag.PaddingHorizontal.Small, LhTokens.Tag.FontSize.Small),
            LhTagSize.Large => (LhTokens.Tag.Height.Large, LhTokens.Tag.PaddingHorizontal.Large, LhTokens.Tag.FontSize.Large),
            _ => (LhTokens.Tag.Height.Medium, LhTokens.Tag.PaddingHorizontal.Medium, LhTokens.Tag.FontSize.Medium),
        };

        // type → 底色 / 文字色 / 描边色
        Color container;
        Color content;
        Color border;
        if (type == LhTagType.Default)
        {
            // default 类型两种形态一致：中性底 + 主文字 + 基础描边
            container = colors.BackgroundDefault;
            content = colors.TextPrimary;
            border = colors.BorderBase;
        }
        else
        {
            var semantic = type switch
            {
                LhTagType.Primary => (solid: colors.Primary, bg: colors.PrimaryBg),
                LhTagType.Success => (solid: colors.Success, bg: colors.SuccessBg),
                LhTagType.Warning => (solid: colors.Warning, bg: colors.WarningBg),
                LhTagType.Danger => (solid: colors.Danger, bg: colors.DangerBg),
                _ => (solid: colors.Info, bg: colors.InfoBg),
            };
            if (plain)
            {
                // 浅底形态：语义 bg 浅底 + 语义 default 字 + 语义 default 描边
                container = semantic.bg;
                content = semantic.solid;
                border = semantic.solid;
            }
            else
            {
                // 实心底形态：语义色实心底 + onPrimary 字、无描边
                container = semantic.solid;
                content = colors.OnPrimary;
                border = LhColor.Transparent;
            }
        }

        return new LhTagAppearance
        {
            Height = height,
            PaddingHorizontal = paddingHorizontal,
            FontSize = fontSize,
            // round 为 true 时圆角取高度一半（胶囊），否则取标签圆角令牌
            BorderRadius = round ? height / 2 : LhTokens.Tag.BorderRadius,
            BorderWidth = LhTokens.Tag.BorderWidth,
            ContainerColor = container,
            ContentColor = content,
            BorderColor = border,
            CloseGap = LhTokens.Spacing.Xs,
        };
    }
}
