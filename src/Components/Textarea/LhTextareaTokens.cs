using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Textarea;

/// <summary>
/// 多行输入框外观解析结果。由 <see cref="LhTextareaTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。
///
/// 高度由 rows 决定初始可见行数、内容超出时自动增高，故解析结果只给最小高度约束与推算函数。
/// </summary>
public sealed record LhTextareaAppearance
{
    public double PaddingVertical { get; init; }
    public double PaddingHorizontal { get; init; }
    public double FontSize { get; init; }
    public double BorderRadius { get; init; }
    /// <summary>描边宽度；聚焦时取 focusRingWidth，否则取 borderWidth</summary>
    public double BorderWidth { get; init; }
    /// <summary>描边色；聚焦时取 color.focus，否则取 color.border.base</summary>
    public Color BorderColor { get; init; }
    public Color ContainerColor { get; init; }
    public Color TextColor { get; init; }
    public Color PlaceholderColor { get; init; }
    /// <summary>字数统计字号（textarea.countFontSize）</summary>
    public double CountFontSize { get; init; }
    /// <summary>字数统计色（color.text.secondary）</summary>
    public Color CountColor { get; init; }
    /// <summary>最小高度（textarea.minHeight）</summary>
    public double MinHeight { get; init; }
    /// <summary>行高（typography.lineHeight.primary），用于按 rows 推算初始高度</summary>
    public double LineHeight { get; init; }
    public bool Enabled { get; init; }
}

/// <summary>
/// 多行输入框令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/textarea.json</c> 的 behaviors：
/// - 高度由 rows 决定初始可见行数，内容超出时自动增高（最小高度 textarea.minHeight）
/// - 聚焦时描边色取 color.focus、宽度 textarea.focusRingWidth；失焦回 color.border.base + borderWidth
/// - showCount 为 true 时右下角绘制字数统计，格式「当前/最大」，取 countFontSize 与 color.text.secondary
/// - disabled 为 true 时取 disabled.* 令牌
/// </summary>
public static class LhTextareaTokens
{
    /// <summary>解析多行输入框外观（黄金用例的断言入口）</summary>
    public static LhTextareaAppearance Resolve(int rows, bool disabled, bool focused, LhColors colors)
    {
        var (container, text) = disabled
            ? (colors.DisabledBg, colors.DisabledText)
            : (colors.BackgroundDefault, colors.TextPrimary);

        var borderWidth = focused ? LhTokens.Textarea.FocusRingWidth : LhTokens.Textarea.BorderWidth;
        var borderColor = focused ? colors.Focus : colors.BorderBase;

        return new LhTextareaAppearance
        {
            PaddingVertical = LhTokens.Textarea.PaddingVertical,
            PaddingHorizontal = LhTokens.Textarea.PaddingHorizontal,
            FontSize = LhTokens.Textarea.FontSize,
            BorderRadius = LhTokens.Textarea.BorderRadius,
            BorderWidth = borderWidth,
            BorderColor = borderColor,
            ContainerColor = container,
            TextColor = text,
            PlaceholderColor = colors.TextPlaceholder,
            CountFontSize = LhTokens.Textarea.CountFontSize,
            CountColor = colors.TextSecondary,
            MinHeight = LhTokens.Textarea.MinHeight,
            LineHeight = LhTokens.Typography.LineHeight.Primary,
            Enabled = !disabled,
        };
    }

    /// <summary>按 rows 与行高推算初始可见高度；不小于 minHeight 令牌</summary>
    public static double HeightForRows(int rows, LhTextareaAppearance appearance)
    {
        var natural = appearance.FontSize * appearance.LineHeight * rows + appearance.PaddingVertical * 2;
        return natural > appearance.MinHeight ? natural : appearance.MinHeight;
    }

    /// <summary>字数统计文本：声明 maxlength 时为「当前/最大」，否则只显示当前长度</summary>
    public static string CountText(string value, int? maxlength)
    {
        if (maxlength is not { } max || max < 0)
        {
            return $"{value.Length}";
        }
        return $"{value.Length}/{max}";
    }

    /// <summary>maxlength 截断：超出时截断新输入（黄金用例 maxlength-truncates-input）</summary>
    public static string Truncate(string value, int? maxlength)
    {
        if (maxlength is not { } max || max < 0)
        {
            return value;
        }
        return value.Length > max ? value[..max] : value;
    }
}
