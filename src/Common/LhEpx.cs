namespace Top.Lhgb.Ui.Common;

/// <summary>
/// 尺寸令牌（epx 数值）→ WinUI 长度（double）。
///
/// WinUI 里尺寸是 effective pixels（逻辑像素），数值直接可用。本方法只做标记：
/// 在调用点标明「这里消费的是令牌数值」，组件实现禁止出现尺寸字面量
/// （AGENTS.md 红线 1）。与鸿蒙端的 lhVp 同构。
/// </summary>
public static class LhEpx
{
    /// <summary>令牌数值 → 长度（恒等换算，仅用于调用点语义标记）</summary>
    public static double Px(double tokenValue) => tokenValue;
}
