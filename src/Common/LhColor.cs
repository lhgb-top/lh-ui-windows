using Windows.UI;

namespace Top.Lhgb.Ui.Common;

/// <summary>
/// 颜色构造的统一入口。
///
/// <c>Microsoft.UI.Colors</c> 是 WinRT 静态类，取其属性要走激活工厂，
/// 在测试宿主（非打包上下文，无 WinUI 注册表项）里会抛
/// <c>REGDB_E_CLASSNOTREG</c>（0x80040154）。<c>Windows.UI.Color</c> 是纯值类型结构体，
/// <c>FromArgb</c> 不涉及激活，任何上下文都能用，因此组件与令牌统一走这里构造颜色，
/// 禁止直接用 <c>Microsoft.UI.Colors.*</c>（AGENTS.md 红线 4 的颜色通道）。
/// </summary>
public static class LhColor
{
    /// <summary>完全透明（alpha = 0），等价于 Microsoft.UI.Colors.Transparent</summary>
    public static Color Transparent => Color.FromArgb(0, 0, 0, 0);

    /// <summary>令牌 RGBA → WinUI 颜色</summary>
    public static Color Argb(byte a, byte r, byte g, byte b) => Color.FromArgb(a, r, g, b);
}
