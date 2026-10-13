using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Button;

/// <summary>
/// 按钮类型。枚举名与 <c>lh-ui-core/specs/button.json</c> 的 prop <c>type</c> 对齐（R-n5），
/// C# 侧用 PascalCase（spec 的 default → Default）。
///
/// 决定底色与文字色：primary 取主色底 + onPrimary 文字；其余类型取对应语义色。
/// </summary>
public enum LhButtonType
{
    Default,
    Primary,
    Success,
    Warning,
    Danger,
    Info,
}

/// <summary>
/// 按钮尺寸。枚举名与 spec 的 prop <c>size</c> 对齐。
///
/// 映射到 button.height / paddingHorizontal / fontSize / iconSize 令牌。
/// </summary>
public enum LhButtonSize
{
    Small,
    Medium,
    Large,
}

/// <summary>
/// 按钮外观解析结果：尺寸 + 颜色 + 状态。由 <see cref="LhButtonTokens.Resolve"/> 纯函数产出，
/// 组件只负责把它画出来。分离解析与渲染，是为了让黄金用例（cases/button.json）
/// 能在 xUnit 单测里断言「type/size/disabled → 令牌」的映射。
/// </summary>
public sealed record LhButtonAppearance
{
    public double Height { get; init; }
    public double PaddingHorizontal { get; init; }
    public double FontSize { get; init; }
    public double IconSize { get; init; }
    public Color ContainerColor { get; init; }
    public Color ContentColor { get; init; }
    public Color BorderColor { get; init; }
    public double BorderWidth { get; init; }
    public double BorderRadius { get; init; }
    /// <summary>花纹底纹色；bgPattern=false 时为 null（不绘制底纹）</summary>
    public Color? PatternColor { get; init; }
    /// <summary>是否可交互（disabled 与 loading 任一为 true 即不可交互，disabled 优先）</summary>
    public bool Enabled { get; init; }
}

/// <summary>
/// 按钮令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/button.json</c> 的 behaviors：
/// - disabled 为 true 时：呈现禁用样式（disabled.* 令牌），不触发 onClick
/// - loading 为 true 时：显示加载指示器，不触发 onClick
/// - disabled 与 loading 同时为 true 时按 disabled 优先处理
/// - size 映射：small/medium/large 分别取 button.height.&lt;size&gt; 等尺寸令牌
/// - type 映射：primary 取主色底 + onPrimary 文字；其余类型取对应语义色
/// - bgPattern 为 true 时以 color.decoration 绘制底纹，不影响文字对比度
/// </summary>
public static class LhButtonTokens
{
    /// <summary>解析按钮外观（黄金用例的断言入口）</summary>
    public static LhButtonAppearance Resolve(
        LhButtonType type,
        LhButtonSize size,
        bool disabled,
        bool loading,
        bool bgPattern,
        LhColors colors)
    {
        // 状态 gate：disabled 优先于 loading（黄金用例 disabled-precedes-loading）
        var enabled = !disabled && !loading;

        // type → 底色 / 文字色 / 描边色（禁用态覆盖）
        Color container;
        Color content;
        Color border;
        if (!enabled)
        {
            container = colors.DisabledBg;
            content = colors.DisabledText;
            border = colors.DisabledBorder;
        }
        else
        {
            switch (type)
            {
                case LhButtonType.Default:
                    container = colors.BackgroundDefault;
                    content = colors.TextPrimary;
                    border = colors.BorderBase;
                    break;
                case LhButtonType.Primary:
                    container = colors.Primary;
                    content = colors.OnPrimary;
                    border = LhColor.Transparent;
                    break;
                case LhButtonType.Success:
                    container = colors.Success;
                    content = colors.OnPrimary;
                    border = LhColor.Transparent;
                    break;
                case LhButtonType.Warning:
                    container = colors.Warning;
                    content = colors.OnPrimary;
                    border = LhColor.Transparent;
                    break;
                case LhButtonType.Danger:
                    container = colors.Danger;
                    content = colors.OnPrimary;
                    border = LhColor.Transparent;
                    break;
                default: // Info
                    container = colors.Info;
                    content = colors.OnPrimary;
                    border = LhColor.Transparent;
                    break;
            }
        }

        // size → 尺寸令牌（epx 数值）
        double height;
        double paddingHorizontal;
        double fontSize;
        double iconSize;
        switch (size)
        {
            case LhButtonSize.Small:
                height = LhTokens.Button.Height.Small;
                paddingHorizontal = LhTokens.Button.PaddingHorizontal.Small;
                fontSize = LhTokens.Button.FontSize.Small;
                iconSize = LhTokens.Button.IconSize.Small;
                break;
            case LhButtonSize.Large:
                height = LhTokens.Button.Height.Large;
                paddingHorizontal = LhTokens.Button.PaddingHorizontal.Large;
                fontSize = LhTokens.Button.FontSize.Large;
                iconSize = LhTokens.Button.IconSize.Large;
                break;
            default: // Medium
                height = LhTokens.Button.Height.Medium;
                paddingHorizontal = LhTokens.Button.PaddingHorizontal.Medium;
                fontSize = LhTokens.Button.FontSize.Medium;
                iconSize = LhTokens.Button.IconSize.Medium;
                break;
        }

        return new LhButtonAppearance
        {
            Height = height,
            PaddingHorizontal = paddingHorizontal,
            FontSize = fontSize,
            IconSize = iconSize,
            ContainerColor = container,
            ContentColor = content,
            BorderColor = border,
            BorderWidth = LhTokens.Button.BorderWidth,
            BorderRadius = LhTokens.Button.BorderRadius,
            PatternColor = bgPattern ? colors.Decoration : null,
            Enabled = enabled,
        };
    }

    /// <summary>
    /// 图标名 → unicode 字形的本地映射（spec：icon 是图标名，各端按本地图标体系渲染）。
    /// 未登记的图标名返回 null（不绘制），不崩溃。生产环境可换 FontIcon（Segoe Fluent Icons）。
    /// </summary>
    public static string? IconFor(string? name)
    {
        if (name is null) return null;
        return name switch
        {
            "search" => "⌕",
            "refresh" => "↻",
            "check" => "✓",
            "close" => "✕",
            _ => null,
        };
    }
}
