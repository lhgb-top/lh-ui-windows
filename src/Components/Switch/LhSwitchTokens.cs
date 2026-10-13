using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Switch;

/// <summary>开关尺寸。枚举名与 <c>lh-ui-core/specs/switch.json</c> 的 prop <c>size</c> 对齐（R-n5），映射到 switch.width / switch.height 令牌。</summary>
public enum LhSwitchSize
{
    Small,
    Medium,
    Large,
}

/// <summary>开关外观解析结果。由 <see cref="LhSwitchTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。</summary>
public sealed record LhSwitchAppearance
{
    public double Width { get; init; }
    public double Height { get; init; }
    /// <summary>滑块与轨道之间的留白（switch.gap）</summary>
    public double Gap { get; init; }
    /// <summary>滑块直径 = 高度 - 2 × gap</summary>
    public double ThumbSize { get; init; }
    /// <summary>轨道圆角（胶囊，取高度一半）</summary>
    public double BorderRadius { get; init; }
    /// <summary>轨道色：开启 primary，关闭 border.base，禁用 disabled.bg</summary>
    public Color TrackColor { get; init; }
    /// <summary>滑块色：取 color.onPrimary；禁用时取 color.text.disabled</summary>
    public Color ThumbColor { get; init; }
    /// <summary>加载环色（主色）</summary>
    public Color LoadingColor { get; init; }
    /// <summary>滑块在轨道内的水平偏移（关闭贴左、开启贴右）</summary>
    public double ThumbOffset { get; init; }
    public bool Enabled { get; init; }
}

/// <summary>
/// 开关令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/switch.json</c> 的 behaviors：
/// - size 映射：small/medium/large 分别取 switch.width.&lt;size&gt; 与 switch.height.&lt;size&gt; 令牌
/// - 轨道为胶囊（圆角取高度一半）：开启取 color.primary.default，关闭取 color.border.base，禁用取 color.disabled.bg
/// - 滑块为圆形（直径 = 高度 - 2 × switch.gap），取 color.onPrimary；禁用时取 color.text.disabled
/// - loading 为 true 时滑块位置改画加载环（主色），且不响应点击
/// </summary>
public static class LhSwitchTokens
{
    /// <summary>解析开关外观（黄金用例的断言入口）</summary>
    public static LhSwitchAppearance Resolve(bool checkedState, bool disabled, bool loading, LhSwitchSize size, LhColors colors)
    {
        var (width, height) = size switch
        {
            LhSwitchSize.Small => (LhTokens.Switch.Width.Small, LhTokens.Switch.Height.Small),
            LhSwitchSize.Large => (LhTokens.Switch.Width.Large, LhTokens.Switch.Height.Large),
            _ => (LhTokens.Switch.Width.Medium, LhTokens.Switch.Height.Medium),
        };

        // 状态 gate：disabled 或 loading 为 true 时不可交互
        Color track;
        Color thumb;
        if (disabled)
        {
            track = colors.DisabledBg;
            thumb = colors.TextDisabled;
        }
        else if (checkedState)
        {
            track = colors.Primary;
            thumb = colors.OnPrimary;
        }
        else
        {
            track = colors.BorderBase;
            thumb = colors.OnPrimary;
        }

        var thumbSize = height - 2 * LhTokens.Switch.Gap;

        return new LhSwitchAppearance
        {
            Width = width,
            Height = height,
            Gap = LhTokens.Switch.Gap,
            ThumbSize = thumbSize,
            // 胶囊轨道：圆角取高度一半
            BorderRadius = height / 2,
            TrackColor = track,
            ThumbColor = thumb,
            LoadingColor = colors.Primary,
            // 滑块水平偏移：关闭贴左、开启贴右
            ThumbOffset = checkedState ? width - thumbSize - LhTokens.Switch.Gap : LhTokens.Switch.Gap,
            Enabled = !disabled && !loading,
        };
    }
}
