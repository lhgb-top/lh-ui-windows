using Microsoft.UI.Text;
using Windows.UI.Text;

namespace Top.Lhgb.Ui.Common;

/// <summary>
/// 令牌字重数值（typography.fontWeight.*，400 / 600）→ WinUI <see cref="FontWeight"/> 的映射助手。
///
/// 字重本身不是色值/间距/圆角/字号，但为避免「>= 600」这类魔法比较散落在各组件里，
/// 统一在此收口（红线 1 的零字面量精神）。
/// </summary>
public static class LhFontWeight
{
    /// <summary>把令牌字重数值映射到最接近的 WinUI 预定义字重</summary>
    public static FontWeight FromToken(int tokenWeight) => tokenWeight switch
    {
        >= 700 => FontWeights.Bold,
        >= 600 => FontWeights.SemiBold,
        >= 500 => FontWeights.Medium,
        _ => FontWeights.Normal,
    };
}
