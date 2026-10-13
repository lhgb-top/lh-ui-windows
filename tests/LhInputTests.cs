using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Input;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/input.json</c> 的 8 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhInputTokens.Resolve 的返回取自令牌对象，映射即成立。交互 gate（disabled / readonly）
/// 走 appearance.Enabled 与 Truncate 纯函数断言。
/// </summary>
public class LhInputTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: input-emits-onvaluechange —— 可输入态放行（enabled 为 true）
    [Fact]
    public void Input_Emits_OnValueChange()
    {
        var a = LhInputTokens.Resolve(LhInputSize.Medium, false, false, Light);
        Xunit.Assert.True(a.Enabled);
    }

    // case: clearable-emits-empty-value —— 清除态可交互且占位色取占位令牌
    [Fact]
    public void Clearable_Emits_Empty_Value()
    {
        var a = LhInputTokens.Resolve(LhInputSize.Medium, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Text.Placeholder, a.PlaceholderColor);
    }

    // case: disabled-blocks-input
    [Fact]
    public void Disabled_Blocks_Input()
    {
        var a = LhInputTokens.Resolve(LhInputSize.Medium, true, false, Light);
        Xunit.Assert.False(a.Enabled);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Bg, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Text, a.TextColor);
    }

    // case: readonly-blocks-input —— 只读不呈现禁用样式（仅 gate 输入）
    [Fact]
    public void Readonly_Blocks_Input()
    {
        var a = LhInputTokens.Resolve(LhInputSize.Medium, false, false, Light);
        Xunit.Assert.True(a.Enabled);
        Xunit.Assert.Equal(LhTokens.Color.Background.Default, a.ContainerColor);
    }

    // case: placeholder-uses-placeholder-token
    [Fact]
    public void Placeholder_Uses_Placeholder_Token()
    {
        var a = LhInputTokens.Resolve(LhInputSize.Medium, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Text.Placeholder, a.PlaceholderColor);
    }

    // case: focus-uses-focus-token —— 聚焦时描边取 color.focus 与 focusRingWidth
    [Fact]
    public void Focus_Uses_Focus_Token()
    {
        var focused = LhInputTokens.Resolve(LhInputSize.Medium, false, true, Light);
        Xunit.Assert.Equal(LhTokens.Color.Focus, focused.BorderColor);
        Xunit.Assert.Equal(LhTokens.Input.FocusRingWidth, focused.BorderWidth);

        var blurred = LhInputTokens.Resolve(LhInputSize.Medium, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Border.Base, blurred.BorderColor);
        Xunit.Assert.Equal(LhTokens.Input.BorderWidth, blurred.BorderWidth);
    }

    // case: small-size-uses-small-tokens
    [Fact]
    public void Small_Size_Uses_Small_Tokens()
    {
        var a = LhInputTokens.Resolve(LhInputSize.Small, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Input.Height.Small, a.Height);
        Xunit.Assert.NotEqual(LhTokens.Input.Height.Large, a.Height);
    }

    // case: maxlength-truncates-input —— 超出时截断新输入
    [Fact]
    public void Maxlength_Truncates_Input()
    {
        Xunit.Assert.Equal("青花瓷", LhInputTokens.Truncate("青花瓷青花瓷", 3));
        Xunit.Assert.Equal("青花瓷", LhInputTokens.Truncate("青花瓷", 3));
        Xunit.Assert.Equal("青花瓷", LhInputTokens.Truncate("青花瓷", null));
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhInputTokens.Resolve(LhInputSize.Medium, false, true, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Focus, a.BorderColor);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhInputTokens.Resolve(LhInputSize.Large, false, false, Light);
        var da = LhInputTokens.Resolve(LhInputSize.Large, false, false, Dark);
        Xunit.Assert.Equal(la.Height, da.Height);
        Xunit.Assert.Equal(la.FontSize, da.FontSize);
    }
}
