using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Toast;

/// <summary>
/// 轻提示语义类型。枚举名与 <c>lh-ui-core/specs/toast.json</c> 的 prop <c>type</c> 对齐（R-n5）。
/// 非 default 时在文字前绘制对应语义色圆点。
/// </summary>
public enum LhToastType
{
    Default,
    Success,
    Warning,
    Danger,
    Info,
}

/// <summary>轻提示定位。枚举名与 spec 的 prop <c>position</c> 对齐。在可用区域内定位；上下两端留 spacing.xl 边距。</summary>
public enum LhToastPosition
{
    Top,
    Center,
    Bottom,
}

/// <summary>轻提示外观解析结果。由 <see cref="LhToastTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。</summary>
public sealed record LhToastAppearance
{
    public double FontSize { get; init; }
    public double PaddingHorizontal { get; init; }
    public double PaddingVertical { get; init; }
    public double BorderRadius { get; init; }
    /// <summary>类型色点与文字的间距（toast.gap）</summary>
    public double Gap { get; init; }
    /// <summary>胶囊底色（color.ink）</summary>
    public Color ContainerColor { get; init; }
    /// <summary>文字色（color.onPrimary）</summary>
    public Color ContentColor { get; init; }
    /// <summary>类型色点色；type 为 default 时为 null（不绘制）</summary>
    public Color? DotColor { get; init; }
    /// <summary>色点直径（取字号）</summary>
    public double DotSize { get; init; }
    /// <summary>层级（zIndex.toast）</summary>
    public int ZIndex { get; init; }
    /// <summary>上下两端留白（spacing.xl）</summary>
    public double EdgeMargin { get; init; }
    /// <summary>最大宽度占比（封顶可用宽度的 80%）</summary>
    public double MaxWidthPercent { get; init; }
}

/// <summary>
/// 轻提示令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/toast.json</c> 的 behaviors：
/// - 胶囊底取 color.ink，文字取 color.onPrimary，圆角取 toast.borderRadius
/// - type 非 default 时在文字前绘制直径为字号的语义色圆点，间距取 toast.gap
/// - layout 宽度由文字测量得出并封顶可用宽度的 80%，按 position 定位
/// - 层级取 zIndex.toast，宿主须在其他内容之后最后绘制
///
/// 计时模型（spec 的 tick/hide）在端侧由 <see cref="LhToastController"/> 的
/// DispatcherTimer 承载：show 重置计时，到期自动隐藏并触发 OnClose；hide 主动隐藏不触发。
/// </summary>
public static class LhToastTokens
{
    /// <summary>解析轻提示外观（黄金用例的断言入口）</summary>
    public static LhToastAppearance Resolve(LhToastType type, LhColors colors)
    {
        Color? dotColor = type switch
        {
            LhToastType.Success => colors.Success,
            LhToastType.Warning => colors.Warning,
            LhToastType.Danger => colors.Danger,
            LhToastType.Info => colors.Info,
            _ => null,
        };

        return new LhToastAppearance
        {
            FontSize = LhTokens.Toast.FontSize,
            PaddingHorizontal = LhTokens.Toast.PaddingHorizontal,
            PaddingVertical = LhTokens.Toast.PaddingVertical,
            BorderRadius = LhTokens.Toast.BorderRadius,
            Gap = LhTokens.Toast.Gap,
            ContainerColor = colors.Ink,
            ContentColor = colors.OnPrimary,
            DotColor = dotColor,
            // type 非 default 时色点直径取字号
            DotSize = LhTokens.Toast.FontSize,
            ZIndex = LhTokens.ZIndex.Toast,
            EdgeMargin = LhTokens.Spacing.Xl,
            MaxWidthPercent = 80,
        };
    }
}
