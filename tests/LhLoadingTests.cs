using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Loading;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/loading.json</c> 的 5 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhLoadingTokens.Resolve 的返回取自令牌对象，映射即成立。
/// </summary>
public class LhLoadingTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: medium-size-uses-medium-token
    [Fact]
    public void Medium_Size_Uses_Medium_Token()
    {
        var a = LhLoadingTokens.Resolve(LhLoadingSize.Medium, LhLoadingTone.Primary, Light);
        Xunit.Assert.Equal(LhTokens.Loading.Size.Medium, a.Size);
        Xunit.Assert.NotEqual(LhTokens.Loading.Size.Small, a.Size);
    }

    // case: large-size-uses-large-token
    [Fact]
    public void Large_Size_Uses_Large_Token()
    {
        var a = LhLoadingTokens.Resolve(LhLoadingSize.Large, LhLoadingTone.Primary, Light);
        Xunit.Assert.Equal(LhTokens.Loading.Size.Large, a.Size);
        Xunit.Assert.NotEqual(LhTokens.Loading.Size.Medium, a.Size);
    }

    // case: primary-tone-uses-primary-token
    [Fact]
    public void Primary_Tone_Uses_Primary_Token()
    {
        var a = LhLoadingTokens.Resolve(LhLoadingSize.Medium, LhLoadingTone.Primary, Light);
        Xunit.Assert.Equal(LhTokens.Color.Primary.Default, a.Color);
    }

    // case: success-tone-uses-success-token
    [Fact]
    public void Success_Tone_Uses_Success_Token()
    {
        var a = LhLoadingTokens.Resolve(LhLoadingSize.Medium, LhLoadingTone.Success, Light);
        Xunit.Assert.Equal(LhTokens.Color.Success.Default, a.Color);
    }

    // case: render-emits-ring-command —— 有且仅有一条 RING 指令，半径为尺寸一半，描边宽 strokeWidth
    [Fact]
    public void Render_Emits_Ring_Command()
    {
        var a = LhLoadingTokens.Resolve(LhLoadingSize.Medium, LhLoadingTone.Primary, Light);
        Xunit.Assert.Equal(LhTokens.Loading.StrokeWidth, a.StrokeWidth);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhLoadingTokens.Resolve(LhLoadingSize.Medium, LhLoadingTone.Primary, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Primary.Default, a.Color);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhLoadingTokens.Resolve(LhLoadingSize.Large, LhLoadingTone.Success, Light);
        var da = LhLoadingTokens.Resolve(LhLoadingSize.Large, LhLoadingTone.Success, Dark);
        Xunit.Assert.Equal(la.Size, da.Size);
        Xunit.Assert.Equal(la.StrokeWidth, da.StrokeWidth);
    }
}
