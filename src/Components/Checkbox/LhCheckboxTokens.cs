using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Checkbox;

/// <summary>复选框尺寸。枚举名与 <c>lh-ui-core/specs/checkbox.json</c> 的 prop <c>size</c> 对齐（R-n5），映射到 checkbox.size.* 方框尺寸令牌。</summary>
public enum LhCheckboxSize
{
    Small,
    Medium,
    Large,
}

/// <summary>复选框指示符形态：未选中无指示符，选中为勾形，不确定为横杠。</summary>
public enum LhCheckboxMark
{
    None,
    Check,
    Dash,
}

/// <summary>复选框外观解析结果。由 <see cref="LhCheckboxTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。</summary>
public sealed record LhCheckboxAppearance
{
    /// <summary>方框尺寸（epx）</summary>
    public double Size { get; init; }
    public double BorderRadius { get; init; }
    public double BorderWidth { get; init; }
    /// <summary>勾形/横杠描边宽度（checkbox.checkWidth）</summary>
    public double CheckWidth { get; init; }
    /// <summary>方框底色：选中/不确定为 primary，未选中透明（露出底色）</summary>
    public Color ContainerColor { get; init; }
    /// <summary>指示符颜色：选中/不确定为 onPrimary，未选中无意义</summary>
    public Color MarkColor { get; init; }
    /// <summary>方框描边色：未选中为 border.base，选中/不确定为 primary（被实心底覆盖）</summary>
    public Color BorderColor { get; init; }
    /// <summary>文字标签色（disabled 时取 color.text.disabled）</summary>
    public Color LabelColor { get; init; }
    /// <summary>方框与文字间距（checkbox.gap）</summary>
    public double Gap { get; init; }
    /// <summary>指示符形态</summary>
    public LhCheckboxMark Mark { get; init; }
    public bool Enabled { get; init; }
}

/// <summary>
/// 复选框令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/checkbox.json</c> 的 behaviors：
/// - size 映射：small/medium/large 分别取 checkbox.size.&lt;size&gt; 方框尺寸令牌
/// - 未选中：只描边（color.border.base），无填充
/// - 选中/不确定：实心 primary 底 + onPrimary 指示符；选中画勾形，不确定画横杠
/// - disabled：底/边/指示符/文字全部取 disabled.* 令牌，且不响应点击
/// - 点击命中整个行（方框 + 标签区域）：indeterminate 变为选中，否则翻转选中态
/// </summary>
public static class LhCheckboxTokens
{
    /// <summary>解析复选框外观（黄金用例的断言入口）</summary>
    public static LhCheckboxAppearance Resolve(bool checkedState, bool indeterminate, bool disabled, LhCheckboxSize size, LhColors colors)
    {
        var dimension = size switch
        {
            LhCheckboxSize.Small => LhTokens.Checkbox.Size.Small,
            LhCheckboxSize.Large => LhTokens.Checkbox.Size.Large,
            _ => LhTokens.Checkbox.Size.Medium,
        };

        var active = checkedState || indeterminate;

        // 指示符形态：选中画勾，不确定画横杠，未选中无指示符
        var mark = indeterminate ? LhCheckboxMark.Dash : (checkedState ? LhCheckboxMark.Check : LhCheckboxMark.None);

        Color container;
        Color markColor;
        Color border;
        Color label;
        if (disabled)
        {
            // disabled：底/边/指示符/文字全部取 disabled.* 令牌
            container = colors.DisabledBg;
            markColor = colors.DisabledText;
            border = colors.DisabledBorder;
            label = colors.TextDisabled;
        }
        else if (active)
        {
            container = colors.Primary;
            markColor = colors.OnPrimary;
            border = colors.Primary;
            label = colors.TextPrimary;
        }
        else
        {
            // 未选中：只描边，无填充
            container = LhColor.Transparent;
            markColor = colors.OnPrimary;
            border = colors.BorderBase;
            label = colors.TextPrimary;
        }

        return new LhCheckboxAppearance
        {
            Size = dimension,
            BorderRadius = LhTokens.Checkbox.BorderRadius,
            BorderWidth = LhTokens.Checkbox.BorderWidth,
            CheckWidth = LhTokens.Checkbox.CheckWidth,
            ContainerColor = container,
            MarkColor = markColor,
            BorderColor = border,
            LabelColor = label,
            Gap = LhTokens.Checkbox.Gap,
            Mark = mark,
            Enabled = !disabled,
        };
    }

    /// <summary>
    /// 点击后的新选中态（状态机，黄金用例的断言入口）：
    /// indeterminate 变为选中，否则翻转选中态。
    /// </summary>
    public static bool NextChecked(bool checkedState, bool indeterminate) => indeterminate || !checkedState;
}
