using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Loading;

/// <summary>
/// 加载指示器色调。枚举名与 <c>lh-ui-core/specs/loading.json</c> 的 prop <c>tone</c> 对齐（R-n5）。
/// 环色取对应语义色令牌。
/// </summary>
public enum LhLoadingTone
{
    Primary,
    Success,
    Warning,
    Danger,
    Info,
}

/// <summary>加载指示器尺寸。枚举名与 spec 的 prop <c>size</c> 对齐，映射到 loading.size.* 直径令牌。</summary>
public enum LhLoadingSize
{
    Small,
    Medium,
    Large,
}

/// <summary>加载指示器外观解析结果。由 <see cref="LhLoadingTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。</summary>
public sealed record LhLoadingAppearance
{
    /// <summary>指示器直径（epx）</summary>
    public double Size { get; init; }
    /// <summary>加载环描边宽（loading.strokeWidth）</summary>
    public double StrokeWidth { get; init; }
    /// <summary>环色（对应语义色）</summary>
    public Color Color { get; init; }
}

/// <summary>
/// 加载指示器令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/loading.json</c> 的 behaviors：
/// - size 映射：small/medium/large 分别取 loading.size.&lt;size&gt; 直径令牌
/// - 绘制指令为 draw_ring，半径为尺寸一半，描边宽 loading.strokeWidth
/// - tone 映射：取对应语义色令牌
///
/// WinUI 的 ProgressRing 自带环形旋转动画，故「旋转由后端驱动」在此端由
/// 框架动画承担（朴素调性：只发几何，动画由平台补齐）。
/// </summary>
public static class LhLoadingTokens
{
    /// <summary>解析加载指示器外观（黄金用例的断言入口）</summary>
    public static LhLoadingAppearance Resolve(LhLoadingSize size, LhLoadingTone tone, LhColors colors)
    {
        var dimension = size switch
        {
            LhLoadingSize.Small => LhTokens.Loading.Size.Small,
            LhLoadingSize.Large => LhTokens.Loading.Size.Large,
            _ => LhTokens.Loading.Size.Medium,
        };

        var color = tone switch
        {
            LhLoadingTone.Success => colors.Success,
            LhLoadingTone.Warning => colors.Warning,
            LhLoadingTone.Danger => colors.Danger,
            LhLoadingTone.Info => colors.Info,
            _ => colors.Primary,
        };

        return new LhLoadingAppearance
        {
            Size = dimension,
            StrokeWidth = LhTokens.Loading.StrokeWidth,
            Color = color,
        };
    }
}
