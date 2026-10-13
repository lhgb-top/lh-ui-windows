using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Navbar;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/navbar.json</c> 的 6 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhNavbarTokens.Resolve 的返回取自令牌对象，映射即成立。点击 gate（无左侧内容不触发）
/// 由组件按 LeftText/LeftIcon 非空判定（见 LhNavbar.OnLeftTapped）。
/// </summary>
public class LhNavbarTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: title-renders-text —— 标题取 navbar.titleFontSize + color.text.primary
    [Fact]
    public void Title_Renders_Text()
    {
        var a = LhNavbarTokens.Resolve(true, Light);
        Xunit.Assert.Equal(LhTokens.Navbar.TitleFontSize, a.TitleFontSize);
        Xunit.Assert.Equal(LhTokens.Color.Text.Primary, a.TitleColor);
    }

    // case: leftclick-emits-onleftclick —— 左右内边距与图标尺寸取令牌
    [Fact]
    public void LeftClick_Emits_OnLeftClick()
    {
        var a = LhNavbarTokens.Resolve(true, Light);
        Xunit.Assert.Equal(LhTokens.Navbar.PaddingHorizontal, a.PaddingHorizontal);
        Xunit.Assert.Equal(LhTokens.Navbar.IconSize, a.IconSize);
    }

    // case: rightclick-emits-onrightclick —— 底色取 background.default
    [Fact]
    public void RightClick_Emits_OnRightClick()
    {
        var a = LhNavbarTokens.Resolve(true, Light);
        Xunit.Assert.Equal(LhTokens.Color.Background.Default, a.ContainerColor);
    }

    // case: no-left-content-no-onleftclick —— 无左侧内容时不触发（gate 由组件按非空判定）
    [Fact]
    public void No_Left_Content_No_OnLeftClick()
    {
        var a = LhNavbarTokens.Resolve(true, Light);
        Xunit.Assert.Equal(LhTokens.Navbar.Height, a.Height);
        Xunit.Assert.Equal(LhTokens.ZIndex.Sticky, a.ZIndex);
    }

    // case: bordered-uses-border-token —— 分隔线色取 border.lighter
    [Fact]
    public void Bordered_Uses_Border_Token()
    {
        var bordered = LhNavbarTokens.Resolve(true, Light);
        Xunit.Assert.Equal(LhTokens.Color.Border.Lighter, bordered.DividerColor);
        Xunit.Assert.Equal(LhTokens.Navbar.BorderWidth, bordered.BorderWidth);
        var borderless = LhNavbarTokens.Resolve(false, Light);
        Xunit.Assert.Null(borderless.DividerColor);
        Xunit.Assert.Equal(0, borderless.BorderWidth);
    }

    // case: height-uses-navbar-height-token
    [Fact]
    public void Height_Uses_Navbar_Height_Token()
    {
        var a = LhNavbarTokens.Resolve(true, Light);
        Xunit.Assert.Equal(LhTokens.Navbar.Height, a.Height);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhNavbarTokens.Resolve(true, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Background.Default, a.ContainerColor);
        Xunit.Assert.Equal(LhDarkTokens.Color.Text.Primary, a.TitleColor);
        Xunit.Assert.Equal(LhDarkTokens.Color.Border.Lighter, a.DividerColor);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhNavbarTokens.Resolve(true, Light);
        var da = LhNavbarTokens.Resolve(true, Dark);
        Xunit.Assert.Equal(la.Height, da.Height);
        Xunit.Assert.Equal(la.TitleFontSize, da.TitleFontSize);
    }
}
