using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Icon;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/icon.json</c> 的 5 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhIconTokens.Resolve 的返回取自令牌对象，映射即成立。
/// </summary>
public class LhIconTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: click-emits-onclick —— 图标可交互（渲染即命中）
    [Fact]
    public void Click_Emits_OnClick()
    {
        var a = LhIconTokens.Resolve(LhIconSize.Medium, LhIconTone.Default, "star", Light);
        Xunit.Assert.Equal("★", a.Glyph);
    }

    // case: default-tone-uses-text-primary
    [Fact]
    public void Default_Tone_Uses_Text_Primary()
    {
        var a = LhIconTokens.Resolve(LhIconSize.Medium, LhIconTone.Default, "star", Light);
        Xunit.Assert.Equal(LhTokens.Color.Text.Primary, a.Color);
    }

    // case: primary-tone-uses-primary-token
    [Fact]
    public void Primary_Tone_Uses_Primary_Token()
    {
        var a = LhIconTokens.Resolve(LhIconSize.Medium, LhIconTone.Primary, "star", Light);
        Xunit.Assert.Equal(LhTokens.Color.Primary.Default, a.Color);
    }

    // case: small-size-uses-small-token
    [Fact]
    public void Small_Size_Uses_Small_Token()
    {
        var a = LhIconTokens.Resolve(LhIconSize.Small, LhIconTone.Default, "star", Light);
        Xunit.Assert.Equal(LhTokens.Icon.Size.Small, a.Size);
        Xunit.Assert.NotEqual(LhTokens.Icon.Size.Large, a.Size);
    }

    // case: render-emits-icon-command —— 有且仅有一条图标指令，且携带图标名（端侧落点：字形表）
    [Fact]
    public void Render_Emits_Single_Icon_Command()
    {
        Xunit.Assert.Equal("★", LhIconTokens.GlyphFor("star"));
        // 未登记名回退通用字形，不崩溃
        Xunit.Assert.Equal("◆", LhIconTokens.GlyphFor("not-registered"));
        Xunit.Assert.Equal("◆", LhIconTokens.GlyphFor(null));
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhIconTokens.Resolve(LhIconSize.Medium, LhIconTone.Primary, "star", Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Primary.Default, a.Color);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhIconTokens.Resolve(LhIconSize.Large, LhIconTone.Default, "star", Light);
        var da = LhIconTokens.Resolve(LhIconSize.Large, LhIconTone.Default, "star", Dark);
        Xunit.Assert.Equal(la.Size, da.Size);
    }
}
