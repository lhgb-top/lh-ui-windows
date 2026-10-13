using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Textarea;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/textarea.json</c> 的 6 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhTextareaTokens.Resolve 的返回取自令牌对象，映射即成立。
/// </summary>
public class LhTextareaTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: input-emits-onvaluechange —— 可输入态放行
    [Fact]
    public void Input_Emits_OnValueChange()
    {
        var a = LhTextareaTokens.Resolve(3, false, false, Light);
        Xunit.Assert.True(a.Enabled);
    }

    // case: disabled-blocks-input
    [Fact]
    public void Disabled_Blocks_Input()
    {
        var a = LhTextareaTokens.Resolve(3, true, false, Light);
        Xunit.Assert.False(a.Enabled);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Bg, a.ContainerColor);
    }

    // case: showcount-renders-count —— 「当前/最大」取 countFontSize 令牌
    [Fact]
    public void ShowCount_Renders_Count()
    {
        var a = LhTextareaTokens.Resolve(3, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Textarea.CountFontSize, a.CountFontSize);
        Xunit.Assert.Equal(LhTokens.Color.Text.Secondary, a.CountColor);
        Xunit.Assert.Equal("2/10", LhTextareaTokens.CountText("青花", 10));
    }

    // case: showcount-without-maxlength-renders-length-only
    [Fact]
    public void ShowCount_Without_Maxlength_Renders_Length_Only()
    {
        Xunit.Assert.Equal("3", LhTextareaTokens.CountText("青花瓷", null));
    }

    // case: focus-uses-focus-token —— 聚焦时描边取 color.focus 与 focusRingWidth
    [Fact]
    public void Focus_Uses_Focus_Token()
    {
        var focused = LhTextareaTokens.Resolve(3, false, true, Light);
        Xunit.Assert.Equal(LhTokens.Color.Focus, focused.BorderColor);
        Xunit.Assert.Equal(LhTokens.Textarea.FocusRingWidth, focused.BorderWidth);

        var blurred = LhTextareaTokens.Resolve(3, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Border.Base, blurred.BorderColor);
        Xunit.Assert.Equal(LhTokens.Textarea.BorderWidth, blurred.BorderWidth);
    }

    // case: maxlength-truncates-input —— 超出时截断新输入
    [Fact]
    public void Maxlength_Truncates_Input()
    {
        Xunit.Assert.Equal("青花瓷", LhTextareaTokens.Truncate("青花瓷青花瓷", 3));
        Xunit.Assert.Equal("青花瓷", LhTextareaTokens.Truncate("青花瓷", null));
    }

    // 行数推算：初始可见高度 = 字号 × 行高 × rows + 上下内边距，不小于 minHeight
    [Fact]
    public void Height_Grows_With_Rows()
    {
        var a = LhTextareaTokens.Resolve(3, false, false, Light);
        var expected = a.FontSize * a.LineHeight * 3 + a.PaddingVertical * 2;
        Xunit.Assert.Equal(expected, LhTextareaTokens.HeightForRows(3, a));
        Xunit.Assert.True(LhTextareaTokens.HeightForRows(3, a) >= a.MinHeight);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhTextareaTokens.Resolve(3, false, true, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Focus, a.BorderColor);
    }

    // 排版与尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhTextareaTokens.Resolve(4, false, false, Light);
        var da = LhTextareaTokens.Resolve(4, false, false, Dark);
        Xunit.Assert.Equal(la.FontSize, da.FontSize);
        Xunit.Assert.Equal(la.MinHeight, da.MinHeight);
    }
}
