using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Button;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/button.json</c> 的 8 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhButtonTokens.Resolve 的返回取自令牌对象，映射即成立。这样黄金用例可以在
/// xUnit 里跑（交互 gate 走 appearance.Enabled 纯断言）。
/// </summary>
public class LhButtonTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: click-emits-onclick —— gate 放行
    [Fact]
    public void Enabled_Button_Is_Interactive()
    {
        var a = LhButtonTokens.Resolve(LhButtonType.Default, LhButtonSize.Medium, false, false, false, Light);
        Xunit.Assert.True(a.Enabled);
    }

    // case: disabled-blocks-click
    [Fact]
    public void Disabled_Is_Not_Interactive()
    {
        var a = LhButtonTokens.Resolve(LhButtonType.Default, LhButtonSize.Medium, true, false, false, Light);
        Xunit.Assert.False(a.Enabled);
    }

    // case: loading-blocks-click
    [Fact]
    public void Loading_Is_Not_Interactive()
    {
        var a = LhButtonTokens.Resolve(LhButtonType.Default, LhButtonSize.Medium, false, true, false, Light);
        Xunit.Assert.False(a.Enabled);
    }

    // case: disabled-precedes-loading
    [Fact]
    public void Disabled_Precedes_Loading()
    {
        var a = LhButtonTokens.Resolve(LhButtonType.Default, LhButtonSize.Medium, true, true, false, Light);
        Xunit.Assert.False(a.Enabled);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Bg, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Text, a.ContentColor);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Border, a.BorderColor);
    }

    // case: default-type-renders-text
    [Fact]
    public void Default_Type_Uses_Neutral_Tokens()
    {
        var a = LhButtonTokens.Resolve(LhButtonType.Default, LhButtonSize.Medium, false, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Background.Default, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.Text.Primary, a.ContentColor);
        Xunit.Assert.Equal(LhTokens.Color.Border.Base, a.BorderColor);
    }

    // case: primary-type-uses-primary-tokens
    [Fact]
    public void Primary_Type_Uses_Primary_Tokens()
    {
        var a = LhButtonTokens.Resolve(LhButtonType.Primary, LhButtonSize.Medium, false, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Primary.Default, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.OnPrimary, a.ContentColor);
    }

    // case: small-size-uses-small-tokens
    [Fact]
    public void Small_Size_Uses_Small_Tokens()
    {
        var a = LhButtonTokens.Resolve(LhButtonType.Default, LhButtonSize.Small, false, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Button.Height.Small, a.Height);
        Xunit.Assert.Equal(LhTokens.Button.FontSize.Small, a.FontSize);
        Xunit.Assert.NotEqual(LhTokens.Button.Height.Small, LhTokens.Button.Height.Large);
    }

    // case: bgpattern-uses-decoration-token
    [Fact]
    public void BgPattern_Uses_Decoration_Token()
    {
        var withPattern = LhButtonTokens.Resolve(LhButtonType.Default, LhButtonSize.Medium, false, false, true, Light);
        Xunit.Assert.Equal(LhTokens.Color.Decoration, withPattern.PatternColor);
        var withoutPattern = LhButtonTokens.Resolve(LhButtonType.Default, LhButtonSize.Medium, false, false, false, Light);
        Xunit.Assert.Null(withoutPattern.PatternColor);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhButtonTokens.Resolve(LhButtonType.Primary, LhButtonSize.Medium, false, false, false, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Primary.Default, a.ContainerColor);
        // onPrimary 暗色不覆盖（core 暗色令牌只覆盖必要颜色），断言明色回退值
        Xunit.Assert.Equal(LhTokens.Color.OnPrimary, a.ContentColor);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhButtonTokens.Resolve(LhButtonType.Default, LhButtonSize.Large, false, false, false, Light);
        var da = LhButtonTokens.Resolve(LhButtonType.Default, LhButtonSize.Large, false, false, false, Dark);
        Xunit.Assert.Equal(la.Height, da.Height);
        Xunit.Assert.Equal(la.FontSize, da.FontSize);
    }
}
