using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Picker;

/// <summary>选择器外观解析结果。由 <see cref="LhPickerTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。</summary>
public sealed record LhPickerAppearance
{
    /// <summary>面板高度（picker.height）</summary>
    public double Height { get; init; }
    /// <summary>候选项高度（picker.itemHeight）</summary>
    public double ItemHeight { get; init; }
    /// <summary>顶部工具栏高度（picker.toolbarHeight）</summary>
    public double ToolbarHeight { get; init; }
    /// <summary>候选项字号（picker.itemFontSize）</summary>
    public double ItemFontSize { get; init; }
    /// <summary>面板顶部圆角（picker.borderRadius）</summary>
    public double BorderRadius { get; init; }
    /// <summary>面板底色（color.background.default）</summary>
    public Color ContainerColor { get; init; }
    /// <summary>遮罩色（color.background.overlay）</summary>
    public Color OverlayColor { get; init; }
    /// <summary>标题色（color.text.primary）</summary>
    public Color TitleColor { get; init; }
    /// <summary>取消按钮色（color.text.secondary）</summary>
    public Color CancelColor { get; init; }
    /// <summary>确认按钮色（color.primary.default）</summary>
    public Color ConfirmColor { get; init; }
    /// <summary>中央指示器线色（color.border.lighter）</summary>
    public Color IndicatorColor { get; init; }
    /// <summary>滚轮区高度 = 面板高度 - 工具栏高度</summary>
    public double WheelHeight { get; init; }
    /// <summary>层级：遮罩取 zIndex.modalBackdrop，面板取 zIndex.modal</summary>
    public int ZIndexBackdrop { get; init; }
    public int ZIndexModal { get; init; }
    /// <summary>钳制后的选中索引（负数取 0，越界取最后一项）</summary>
    public int SelectedIndex { get; init; }
}

/// <summary>
/// 选择器令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/picker.json</c> 的 behaviors：
/// - 面板高度 picker.height，从底部弹出，顶部圆角 picker.borderRadius，底色 color.background.default
/// - 工具栏高度 picker.toolbarHeight：左取消、中标题、右确认
/// - 候选项高度 picker.itemHeight、字号 picker.itemFontSize；中央指示器为两条水平线
///   （color.border.lighter），间距 = picker.itemHeight
/// - 遮罩取 color.background.overlay，层级取 zIndex.modal / zIndex.modalBackdrop
/// - value 越界时钳制：负数取 0，大于候选项数 - 1 取最后一项
/// </summary>
public static class LhPickerTokens
{
    /// <summary>解析选择器外观（黄金用例的断言入口）</summary>
    public static LhPickerAppearance Resolve(int optionCount, int value, LhColors colors)
    {
        return new LhPickerAppearance
        {
            Height = LhTokens.Picker.Height,
            ItemHeight = LhTokens.Picker.ItemHeight,
            ToolbarHeight = LhTokens.Picker.ToolbarHeight,
            ItemFontSize = LhTokens.Picker.ItemFontSize,
            BorderRadius = LhTokens.Picker.BorderRadius,
            ContainerColor = colors.BackgroundDefault,
            OverlayColor = colors.BackgroundOverlay,
            TitleColor = colors.TextPrimary,
            CancelColor = colors.TextSecondary,
            ConfirmColor = colors.Primary,
            IndicatorColor = colors.BorderLighter,
            WheelHeight = LhTokens.Picker.Height - LhTokens.Picker.ToolbarHeight,
            ZIndexBackdrop = LhTokens.ZIndex.ModalBackdrop,
            ZIndexModal = LhTokens.ZIndex.Modal,
            // value 越界钳制：负数取 0，大于候选项数 - 1 取最后一项
            SelectedIndex = ClampIndex(value, optionCount),
        };
    }

    /// <summary>索引钳制：负数取 0，越界取最后一项；无候选项时为 0</summary>
    public static int ClampIndex(int value, int optionCount)
    {
        if (optionCount <= 0)
        {
            return 0;
        }
        if (value < 0)
        {
            return 0;
        }
        if (value > optionCount - 1)
        {
            return optionCount - 1;
        }
        return value;
    }
}
