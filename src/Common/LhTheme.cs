using System;
namespace Top.Lhgb.Ui.Common;

/// <summary>
/// 主题工具：持有当前色板与明暗态，供所有 Lh 组件消费。
/// 应用入口（App 启动或主窗口 Loaded）调用一次 <see cref="Apply"/> 即可。
///
/// 用法：
///   LhTheme.Apply(false);              // 注入明色板
///   LhTheme.Apply(true);               // 切换暗色板，触发 ThemeChanged 重绘
///   var isDark = LhTheme.IsDark;       // 读当前态
///   var colors = LhTheme.Current;      // 取当前色板（未 Apply 时回退明色板）
///
/// 与鸿蒙端的 AppStorage 注入同构：色板是单一可变真源，组件在 Loaded 时订阅
/// <see cref="ThemeChanged"/>，收到事件后重算外观并重绘（主题热重绘，无需重建页面）。
/// </summary>
public static class LhTheme
{
    private static LhColors _current = LhColorsFactory.Light();
    private static bool _isDark;

    /// <summary>当前色板；未 Apply 过时为明色板</summary>
    public static LhColors Current => _current;

    /// <summary>当前是否暗色</summary>
    public static bool IsDark => _isDark;

    /// <summary>主题变化时触发，组件据此重算外观（主题热重绘）</summary>
    public static event EventHandler? ThemeChanged;

    /// <summary>切换主题：注入对应色板并通知所有订阅组件</summary>
    public static void Apply(bool isDark)
    {
        _isDark = isDark;
        _current = isDark ? LhColorsFactory.Dark() : LhColorsFactory.Light();
        ThemeChanged?.Invoke(null, EventArgs.Empty);
    }
}
