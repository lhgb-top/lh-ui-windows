using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Cell;

/// <summary>
/// 单元格外观解析结果。由 <see cref="LhCellTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。
///
/// 高度由内容（标题行 + 可选说明行）与内边距决定，故解析结果不含高度。
/// </summary>
public sealed record LhCellAppearance
{
    public double PaddingHorizontal { get; init; }
    public double PaddingVertical { get; init; }
    /// <summary>标题与说明文字之间的垂直间距</summary>
    public double Gap { get; init; }
    public double FontSizeTitle { get; init; }
    public double FontSizeLabel { get; init; }
    public double FontSizeValue { get; init; }
    /// <summary>行高（typography.lineHeight.primary）</summary>
    public double LineHeight { get; init; }
    /// <summary>左侧图标尺寸（icon.size.medium）</summary>
    public double IconSize { get; init; }
    public Color TitleColor { get; init; }
    public Color LabelColor { get; init; }
    public Color ValueColor { get; init; }
    public Color IconColor { get; init; }
    /// <summary>必填红星色（color.danger.default）</summary>
    public Color RequiredColor { get; init; }
    /// <summary>右侧指示箭头色（color.text.secondary）</summary>
    public Color ArrowColor { get; init; }
    /// <summary>底部分隔线色（color.border.lighter）；bordered 为 false 时为 null</summary>
    public Color? DividerColor { get; init; }
    /// <summary>标题与左侧图标的水平间距（spacing.sm）</summary>
    public double IconGap { get; init; }
}

/// <summary>
/// 单元格令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/cell.json</c> 的 behaviors：
/// - 标题取 cell.fontSizeTitle 与 color.text.primary
/// - 说明取 cell.fontSizeLabel 与 color.text.secondary
/// - 值取 cell.fontSizeValue 与 color.text.secondary 并右对齐
/// - required 为 true 时标题前绘制红星（danger 色）
/// - icon 非空时在左侧绘制尺寸 icon.size.medium 的图标
/// - clickable 为 true 时右侧绘制指示箭头
/// - bordered 为 true 时底部绘制分隔线（color.border.lighter）
/// </summary>
public static class LhCellTokens
{
    /// <summary>
    /// 解析单元格外观（黄金用例的断言入口）。
    ///
    /// 图标是否绘制由组件按 <c>icon != null</c> 判断（令牌侧无开关），故入参只有
    /// bordered / clickable 两个状态开关。
    /// </summary>
    public static LhCellAppearance Resolve(bool bordered, bool clickable, LhColors colors)
    {
        return new LhCellAppearance
        {
            PaddingHorizontal = LhTokens.Cell.PaddingHorizontal,
            PaddingVertical = LhTokens.Cell.PaddingVertical,
            Gap = LhTokens.Cell.Gap,
            FontSizeTitle = LhTokens.Cell.FontSizeTitle,
            FontSizeLabel = LhTokens.Cell.FontSizeLabel,
            FontSizeValue = LhTokens.Cell.FontSizeValue,
            LineHeight = LhTokens.Typography.LineHeight.Primary,
            IconSize = LhTokens.Icon.Size.Medium,
            TitleColor = colors.TextPrimary,
            LabelColor = colors.TextSecondary,
            ValueColor = colors.TextSecondary,
            IconColor = colors.TextRegular,
            RequiredColor = colors.Danger,
            ArrowColor = colors.TextSecondary,
            DividerColor = bordered ? colors.BorderLighter : null,
            IconGap = LhTokens.Spacing.Sm,
        };
    }
}
