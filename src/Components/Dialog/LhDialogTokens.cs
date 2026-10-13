using Microsoft.UI;
using Windows.UI;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Components.Dialog;

/// <summary>
/// 对话框语义类型。枚举名与 <c>lh-ui-core/specs/dialog.json</c> 的 prop <c>type</c> 对齐（R-n5）。
/// 决定确认按钮的类型（颜色）。
/// </summary>
public enum LhDialogType
{
    Default,
    Primary,
    Success,
    Warning,
    Danger,
    Info,
}

/// <summary>对话框外观解析结果。由 <see cref="LhDialogTokens.Resolve"/> 纯函数产出，组件只负责把它画出来。</summary>
public sealed record LhDialogAppearance
{
    /// <summary>内容卡宽度（dialog.width）</summary>
    public double Width { get; init; }
    public double BorderRadius { get; init; }
    public double PaddingHorizontal { get; init; }
    public double PaddingVertical { get; init; }
    public double TitleFontSize { get; init; }
    public double ContentFontSize { get; init; }
    /// <summary>标题与内容的间距（dialog.gap）</summary>
    public double Gap { get; init; }
    /// <summary>遮罩色（color.background.overlay）</summary>
    public Color OverlayColor { get; init; }
    /// <summary>内容卡底色（color.background.default）</summary>
    public Color ContainerColor { get; init; }
    public Color TitleColor { get; init; }
    public Color ContentColor { get; init; }
    /// <summary>确认按钮类型（按 type 映射语义色）</summary>
    public LhButtonType ConfirmButtonType { get; init; }
    /// <summary>层级：遮罩取 zIndex.modalBackdrop，内容卡取 zIndex.modal</summary>
    public int ZIndexBackdrop { get; init; }
    public int ZIndexModal { get; init; }
}

/// <summary>
/// 对话框令牌绑定与外观解析（纯函数，无组件依赖）。
///
/// 令牌映射严格对齐 <c>lh-ui-core/specs/dialog.json</c> 的 behaviors：
/// - 内容卡宽度 dialog.width、圆角 dialog.borderRadius、内边距 dialog.padding*，
///   底色 color.background.default，投影 shadow.base
/// - 遮罩取 color.background.overlay
/// - 标题取 dialog.titleFontSize + color.text.primary，内容取 dialog.contentFontSize + color.text.regular
/// - 确认按钮按 type 映射语义色；取消按钮固定 default 类型
/// - 层级取 zIndex.modal（遮罩 zIndex.modalBackdrop）
/// </summary>
public static class LhDialogTokens
{
    /// <summary>解析对话框外观（黄金用例的断言入口）</summary>
    public static LhDialogAppearance Resolve(LhDialogType type, LhColors colors)
    {
        // dialog.type → 确认按钮的 button.type（语义色映射）
        var confirmType = type switch
        {
            LhDialogType.Primary => LhButtonType.Primary,
            LhDialogType.Success => LhButtonType.Success,
            LhDialogType.Warning => LhButtonType.Warning,
            LhDialogType.Danger => LhButtonType.Danger,
            LhDialogType.Info => LhButtonType.Info,
            _ => LhButtonType.Default,
        };

        return new LhDialogAppearance
        {
            Width = LhTokens.Dialog.Width,
            BorderRadius = LhTokens.Dialog.BorderRadius,
            PaddingHorizontal = LhTokens.Dialog.PaddingHorizontal,
            PaddingVertical = LhTokens.Dialog.PaddingVertical,
            TitleFontSize = LhTokens.Dialog.TitleFontSize,
            ContentFontSize = LhTokens.Dialog.ContentFontSize,
            Gap = LhTokens.Dialog.Gap,
            OverlayColor = colors.BackgroundOverlay,
            ContainerColor = colors.BackgroundDefault,
            TitleColor = colors.TextPrimary,
            ContentColor = colors.TextRegular,
            ConfirmButtonType = confirmType,
            ZIndexBackdrop = LhTokens.ZIndex.ModalBackdrop,
            ZIndexModal = LhTokens.ZIndex.Modal,
        };
    }
}
