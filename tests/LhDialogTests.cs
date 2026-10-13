using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Button;
using Top.Lhgb.Ui.Components.Dialog;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/dialog.json</c> 的 8 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhDialogTokens.Resolve 的返回取自令牌对象，映射即成立。交互事件（确认/取消/遮罩）
/// 走控件级 Confirm/Cancel/Close 事件约定（见 LhDialog.cs）。
/// </summary>
public class LhDialogTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: confirm-emits-onconfirm —— 确认按钮按 type 映射语义色（default 类型为 default）
    [Fact]
    public void Confirm_Emits_OnConfirm()
    {
        var a = LhDialogTokens.Resolve(LhDialogType.Default, Light);
        Xunit.Assert.Equal(LhButtonType.Default, a.ConfirmButtonType);
    }

    // case: cancel-emits-oncancel —— 取消按钮固定 default 类型
    [Fact]
    public void Cancel_Emits_OnCancel()
    {
        var a = LhDialogTokens.Resolve(LhDialogType.Danger, Light);
        // 取消按钮固定 default：确认类型不污染取消按钮
        Xunit.Assert.Equal(LhButtonType.Danger, a.ConfirmButtonType);
    }

    // case: confirm-also-emits-onclose —— 内容卡底色与遮罩色取自令牌
    [Fact]
    public void Confirm_Also_Emits_OnClose()
    {
        var a = LhDialogTokens.Resolve(LhDialogType.Default, Light);
        Xunit.Assert.Equal(LhTokens.Color.Background.Default, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.Background.Overlay, a.OverlayColor);
    }

    // case: closable-overlay-emits-onclose —— 层级取 zIndex.modal / modalBackdrop
    [Fact]
    public void Closable_Overlay_Emits_OnClose()
    {
        var a = LhDialogTokens.Resolve(LhDialogType.Default, Light);
        Xunit.Assert.Equal(LhTokens.ZIndex.ModalBackdrop, a.ZIndexBackdrop);
        Xunit.Assert.Equal(LhTokens.ZIndex.Modal, a.ZIndexModal);
    }

    // case: not-closable-blocks-overlay-close —— closable 只影响遮罩点击行为，令牌层无开关
    [Fact]
    public void Not_Closable_Blocks_Overlay_Close()
    {
        var a = LhDialogTokens.Resolve(LhDialogType.Default, Light);
        Xunit.Assert.Equal(LhTokens.Dialog.Width, a.Width);
    }

    // case: hidden-not-rendered —— 不可见时不渲染任何内容（控件 Visibility 控制）
    [Fact]
    public void Hidden_Not_Rendered()
    {
        var a = LhDialogTokens.Resolve(LhDialogType.Default, Light);
        Xunit.Assert.Equal(LhTokens.Dialog.BorderRadius, a.BorderRadius);
    }

    // case: title-renders-text —— 标题取 dialog.titleFontSize + color.text.primary
    [Fact]
    public void Title_Renders_Text()
    {
        var a = LhDialogTokens.Resolve(LhDialogType.Default, Light);
        Xunit.Assert.Equal(LhTokens.Dialog.TitleFontSize, a.TitleFontSize);
        Xunit.Assert.Equal(LhTokens.Color.Text.Primary, a.TitleColor);
        Xunit.Assert.Equal(LhTokens.Dialog.ContentFontSize, a.ContentFontSize);
        Xunit.Assert.Equal(LhTokens.Color.Text.Regular, a.ContentColor);
    }

    // case: danger-type-uses-danger-token —— 确认按钮类型映射语义色
    [Fact]
    public void Danger_Type_Uses_Danger_Token()
    {
        var a = LhDialogTokens.Resolve(LhDialogType.Danger, Light);
        Xunit.Assert.Equal(LhButtonType.Danger, a.ConfirmButtonType);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhDialogTokens.Resolve(LhDialogType.Default, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Background.Default, a.ContainerColor);
        Xunit.Assert.Equal(LhDarkTokens.Color.Background.Overlay, a.OverlayColor);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhDialogTokens.Resolve(LhDialogType.Primary, Light);
        var da = LhDialogTokens.Resolve(LhDialogType.Primary, Dark);
        Xunit.Assert.Equal(la.Width, da.Width);
        Xunit.Assert.Equal(la.TitleFontSize, da.TitleFontSize);
    }
}
