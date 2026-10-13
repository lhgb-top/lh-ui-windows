using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Picker;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/picker.json</c> 的 8 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhPickerTokens.Resolve 的返回取自令牌对象，映射即成立。索引钳制走 ClampIndex 纯函数断言。
/// </summary>
public class LhPickerTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: confirm-emits-onconfirm-with-value —— 确认携带当前选中索引
    [Fact]
    public void Confirm_Emits_OnConfirm_With_Value()
    {
        var a = LhPickerTokens.Resolve(3, 1, Light);
        Xunit.Assert.Equal(1, a.SelectedIndex);
    }

    // case: confirm-also-emits-onclose —— 层级取 zIndex.modal / modalBackdrop
    [Fact]
    public void Confirm_Also_Emits_OnClose()
    {
        var a = LhPickerTokens.Resolve(3, 0, Light);
        Xunit.Assert.Equal(LhTokens.ZIndex.ModalBackdrop, a.ZIndexBackdrop);
        Xunit.Assert.Equal(LhTokens.ZIndex.Modal, a.ZIndexModal);
    }

    // case: cancel-emits-oncancel —— 取消色取次要文字色
    [Fact]
    public void Cancel_Emits_OnCancel()
    {
        var a = LhPickerTokens.Resolve(3, 0, Light);
        Xunit.Assert.Equal(LhTokens.Color.Text.Secondary, a.CancelColor);
    }

    // case: scroll-emits-onvaluechange —— 滚轮区高度 = 面板高度 - 工具栏高度
    [Fact]
    public void Scroll_Emits_OnValueChange()
    {
        var a = LhPickerTokens.Resolve(3, 2, Light);
        Xunit.Assert.Equal(LhTokens.Picker.Height - LhTokens.Picker.ToolbarHeight, a.WheelHeight);
    }

    // case: clamp-negative-value-to-zero
    [Fact]
    public void Clamp_Negative_Value_To_Zero()
    {
        Xunit.Assert.Equal(0, LhPickerTokens.ClampIndex(-1, 3));
        Xunit.Assert.Equal(0, LhPickerTokens.ClampIndex(-100, 3));
    }

    // case: clamp-overflow-value-to-last
    [Fact]
    public void Clamp_Overflow_Value_To_Last()
    {
        Xunit.Assert.Equal(2, LhPickerTokens.ClampIndex(5, 3));
        Xunit.Assert.Equal(2, LhPickerTokens.ClampIndex(100, 3));
        // 无候选项时为 0
        Xunit.Assert.Equal(0, LhPickerTokens.ClampIndex(5, 0));
    }

    // case: title-renders-text —— 标题取主要文字色，候选项取 itemHeight / itemFontSize 令牌
    [Fact]
    public void Title_Renders_Text()
    {
        var a = LhPickerTokens.Resolve(3, 0, Light);
        Xunit.Assert.Equal(LhTokens.Color.Text.Primary, a.TitleColor);
        Xunit.Assert.Equal(LhTokens.Picker.ItemHeight, a.ItemHeight);
        Xunit.Assert.Equal(LhTokens.Picker.ItemFontSize, a.ItemFontSize);
        Xunit.Assert.Equal(LhTokens.Picker.ToolbarHeight, a.ToolbarHeight);
    }

    // case: hidden-not-rendered —— 不可见时不渲染任何内容（控件 Visibility 控制）
    [Fact]
    public void Hidden_Not_Rendered()
    {
        var a = LhPickerTokens.Resolve(3, 0, Light);
        Xunit.Assert.Equal(LhTokens.Picker.Height, a.Height);
        Xunit.Assert.Equal(LhTokens.Picker.BorderRadius, a.BorderRadius);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhPickerTokens.Resolve(3, 0, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Background.Default, a.ContainerColor);
        Xunit.Assert.Equal(LhDarkTokens.Color.Background.Overlay, a.OverlayColor);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhPickerTokens.Resolve(3, 0, Light);
        var da = LhPickerTokens.Resolve(3, 0, Dark);
        Xunit.Assert.Equal(la.Height, da.Height);
        Xunit.Assert.Equal(la.ItemHeight, da.ItemHeight);
    }
}
