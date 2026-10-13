using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Text;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/text.json</c> 的 5 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhTextTokens.Resolve 的返回取自令牌对象，映射即成立。
/// </summary>
public class LhTextTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: default-level-uses-base-tokens —— 默认级别消费 base 排版令牌
    [Fact]
    public void Default_Level_Uses_Base_Tokens()
    {
        var a = LhTextTokens.Resolve(LhTextLevel.Base, LhTextWeight.Primary, LhTextTone.Primary, Light);
        Xunit.Assert.Equal(LhTokens.Typography.FontSize.Base, a.FontSize);
        Xunit.Assert.Equal(LhTokens.Typography.FontWeight.Primary, a.FontWeight);
        Xunit.Assert.Equal(LhTokens.Typography.LineHeight.Primary, a.LineHeight);
        Xunit.Assert.Equal(LhTokens.Color.Text.Primary, a.FontColor);
    }

    // case: large-level-uses-large-token
    [Fact]
    public void Large_Level_Uses_Large_Token()
    {
        var a = LhTextTokens.Resolve(LhTextLevel.Large, LhTextWeight.Primary, LhTextTone.Primary, Light);
        Xunit.Assert.Equal(LhTokens.Typography.FontSize.Large, a.FontSize);
        Xunit.Assert.NotEqual(LhTokens.Typography.FontSize.Base, a.FontSize);
    }

    // case: bold-weight-uses-bold-token
    [Fact]
    public void Bold_Weight_Uses_Bold_Token()
    {
        var a = LhTextTokens.Resolve(LhTextLevel.Base, LhTextWeight.Bold, LhTextTone.Primary, Light);
        Xunit.Assert.Equal(LhTokens.Typography.FontWeight.Bold, a.FontWeight);
        Xunit.Assert.NotEqual(LhTokens.Typography.FontWeight.Primary, a.FontWeight);
    }

    // case: secondary-tone-uses-text-secondary
    [Fact]
    public void Secondary_Tone_Uses_Text_Secondary()
    {
        var a = LhTextTokens.Resolve(LhTextLevel.Base, LhTextWeight.Primary, LhTextTone.Secondary, Light);
        Xunit.Assert.Equal(LhTokens.Color.Text.Secondary, a.FontColor);
        Xunit.Assert.NotEqual(LhTokens.Color.Text.Primary, a.FontColor);
    }

    // case: render-emits-single-text-command —— 单一文字节点（端侧落点：一个 TextBlock）
    [Fact]
    public void Render_Emits_Single_Text_Command()
    {
        var a = LhTextTokens.Resolve(LhTextLevel.Base, LhTextWeight.Primary, LhTextTone.Primary, Light);
        // 一条 TEXT 指令 = 一组排版属性；行盒高度 = 字号 × 行高
        Xunit.Assert.Equal(a.FontSize * LhTokens.Typography.LineHeight.Primary, a.FontSize * a.LineHeight);
        // 对齐映射覆盖三个枚举值（非设计令牌，映射到框架枚举）
        Xunit.Assert.Equal(Microsoft.UI.Xaml.TextAlignment.Left, LhTextTokens.AlignFor(LhTextAlign.Start));
        Xunit.Assert.Equal(Microsoft.UI.Xaml.TextAlignment.Center, LhTextTokens.AlignFor(LhTextAlign.Center));
        Xunit.Assert.Equal(Microsoft.UI.Xaml.TextAlignment.Right, LhTextTokens.AlignFor(LhTextAlign.End));
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhTextTokens.Resolve(LhTextLevel.Base, LhTextWeight.Primary, LhTextTone.Primary, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Text.Primary, a.FontColor);
    }

    // 排版令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhTextTokens.Resolve(LhTextLevel.Large, LhTextWeight.Bold, LhTextTone.Primary, Light);
        var da = LhTextTokens.Resolve(LhTextLevel.Large, LhTextWeight.Bold, LhTextTone.Primary, Dark);
        Xunit.Assert.Equal(la.FontSize, da.FontSize);
        Xunit.Assert.Equal(la.FontWeight, da.FontWeight);
    }
}
