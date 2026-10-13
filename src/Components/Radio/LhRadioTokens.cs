using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Radio;

/// <summary>单选框尺寸。枚举名与 <c>lh-ui-core/specs/radio.json</c> 的 prop <c>size</c> 对齐（R-n5），映射到 radio.size.* 圆环尺寸令牌。</summary>
public enum LhRadioSize
{
    Small,
    Medium,
    Large,
}

/// <summary>单选框外观解析结果。由 <see cref="LhRadioTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。</summary>
public sealed record LhRadioAppearance
{
    /// <summary>圆环尺寸（epx）</summary>
    public double Size { get; init; }
    /// <summary>外环描边宽度（radio.ringWidth）</summary>
    public double RingWidth { get; init; }
    /// <summary>内圆点直径 = 尺寸 × radio.dotScale</summary>
    public double DotSize { get; init; }
    /// <summary>外环描边色：未选中 border.base，选中 primary</summary>
    public Color RingColor { get; init; }
    /// <summary>内圆点色：选中为 primary，未选中无圆点</summary>
    public Color DotColor { get; init; }
    /// <summary>文字标签色（disabled 时取 color.text.disabled）</summary>
    public Color LabelColor { get; init; }
    /// <summary>圆环与文字间距（radio.gap）</summary>
    public double Gap { get; init; }
    /// <summary>是否绘制内圆点</summary>
    public bool ShowDot { get; init; }
    public bool Enabled { get; init; }
}

/// <summary>
/// 单选框令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/radio.json</c> 的 behaviors：
/// - size 映射：small/medium/large 分别取 radio.size.&lt;size&gt; 圆环尺寸令牌
/// - 外环描边：未选中取 color.border.base，选中取 color.primary.default，描边宽 radio.ringWidth
/// - 选中时内部画实心圆点，直径 = 尺寸 × radio.dotScale，取 color.primary.default
/// - disabled：外环取 color.disabled.border，圆点取 color.disabled.text，文字取 color.text.disabled
/// - 点击命中整个行：仅当未选中且未禁用时变为选中并触发 onChange(checked=true)
/// </summary>
public static class LhRadioTokens
{
    /// <summary>解析单选框外观（黄金用例的断言入口）</summary>
    public static LhRadioAppearance Resolve(bool checkedState, bool disabled, LhRadioSize size, LhColors colors)
    {
        var dimension = size switch
        {
            LhRadioSize.Small => LhTokens.Radio.Size.Small,
            LhRadioSize.Large => LhTokens.Radio.Size.Large,
            _ => LhTokens.Radio.Size.Medium,
        };

        Color ring;
        Color dot;
        Color label;
        if (disabled)
        {
            ring = colors.DisabledBorder;
            dot = colors.DisabledText;
            label = colors.TextDisabled;
        }
        else if (checkedState)
        {
            ring = colors.Primary;
            dot = colors.Primary;
            label = colors.TextPrimary;
        }
        else
        {
            ring = colors.BorderBase;
            dot = colors.Primary;
            label = colors.TextPrimary;
        }

        return new LhRadioAppearance
        {
            Size = dimension,
            RingWidth = LhTokens.Radio.RingWidth,
            // 内圆点直径 = 尺寸 × radio.dotScale（dotScale 是分数）
            DotSize = dimension * LhTokens.Radio.DotScale,
            RingColor = ring,
            DotColor = dot,
            LabelColor = label,
            Gap = LhTokens.Radio.Gap,
            ShowDot = checkedState && !disabled,
            Enabled = !disabled,
        };
    }
}
