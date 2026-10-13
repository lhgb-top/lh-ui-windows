using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Input;

/// <summary>
/// 输入框类型。枚举名与 <c>lh-ui-core/specs/input.json</c> 的 prop <c>type</c> 对齐（R-n5）。
/// Password 时以圆点掩码显示（WinUI 的 PasswordBox 密码字符）。
/// </summary>
public enum LhInputType
{
    Text,
    Password,
}

/// <summary>输入框尺寸。枚举名与 spec 的 prop <c>size</c> 对齐，映射到 input.height / paddingHorizontal / fontSize 令牌。</summary>
public enum LhInputSize
{
    Small,
    Medium,
    Large,
}

/// <summary>输入框外观解析结果。由 <see cref="LhInputTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。</summary>
public sealed record LhInputAppearance
{
    public double Height { get; init; }
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
    /// <summary>清除按钮图标尺寸（input.clearIconSize）</summary>
    public double ClearIconSize { get; init; }
    /// <summary>是否可交互（disabled 与 readonly 都不触发 onValueChange，但只 disabled 呈现禁用样式）</summary>
    public bool Enabled { get; init; }
}

/// <summary>
/// 输入框令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/input.json</c> 的 behaviors：
/// - size 映射：small/medium/large 分别取 input.height.&lt;size&gt; 等尺寸令牌
/// - disabled 为 true 时取 disabled.* 令牌，不响应输入与清除
/// - 聚焦时描边色取 color.focus、宽度 input.focusRingWidth；失焦回 color.border.base + input.borderWidth
/// </summary>
public static class LhInputTokens
{
    /// <summary>解析输入框外观（黄金用例的断言入口）</summary>
    public static LhInputAppearance Resolve(LhInputSize size, bool disabled, bool focused, LhColors colors)
    {
        var (height, paddingHorizontal, fontSize) = size switch
        {
            LhInputSize.Small => (LhTokens.Input.Height.Small, LhTokens.Input.PaddingHorizontal.Small, LhTokens.Input.FontSize.Small),
            LhInputSize.Large => (LhTokens.Input.Height.Large, LhTokens.Input.PaddingHorizontal.Large, LhTokens.Input.FontSize.Large),
            _ => (LhTokens.Input.Height.Medium, LhTokens.Input.PaddingHorizontal.Medium, LhTokens.Input.FontSize.Medium),
        };

        var (container, text) = disabled
            ? (colors.DisabledBg, colors.DisabledText)
            : (colors.BackgroundDefault, colors.TextPrimary);

        // 聚焦环：色取 color.focus、宽度 input.focusRingWidth；失焦回基础描边
        var borderWidth = focused ? LhTokens.Input.FocusRingWidth : LhTokens.Input.BorderWidth;
        var borderColor = focused ? colors.Focus : colors.BorderBase;

        return new LhInputAppearance
        {
            Height = height,
            PaddingHorizontal = paddingHorizontal,
            FontSize = fontSize,
            BorderRadius = LhTokens.Input.BorderRadius,
            BorderWidth = borderWidth,
            BorderColor = borderColor,
            ContainerColor = container,
            TextColor = text,
            PlaceholderColor = colors.TextPlaceholder,
            ClearIconSize = LhTokens.Input.ClearIconSize,
            Enabled = !disabled,
        };
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
