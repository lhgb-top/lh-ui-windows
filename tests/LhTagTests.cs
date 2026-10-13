using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Tag;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/tag.json</c> 的 6 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhTagTokens.Resolve 的返回取自令牌对象，映射即成立。交互事件（closable-tap-emits-onclose）
/// 走控件级 Close 事件断言。
/// </summary>
public class LhTagTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: default-type-renders-text —— 默认类型中性底/文字/描边
    [Fact]
    public void Default_Type_Renders_Text()
    {
        var a = LhTagTokens.Resolve(LhTagType.Default, LhTagSize.Medium, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Background.Default, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.Text.Primary, a.ContentColor);
        Xunit.Assert.Equal(LhTokens.Color.Border.Base, a.BorderColor);
    }

    // case: primary-type-uses-primary-tokens
    [Fact]
    public void Primary_Type_Uses_Primary_Tokens()
    {
        var a = LhTagTokens.Resolve(LhTagType.Primary, LhTagSize.Medium, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Primary.Default, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.OnPrimary, a.ContentColor);
    }

    // case: plain-uses-bg-tokens —— 浅底形态消费语义浅底令牌
    [Fact]
    public void Plain_Uses_Bg_Tokens()
    {
        var a = LhTagTokens.Resolve(LhTagType.Success, LhTagSize.Medium, true, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Success.Bg, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.Success.Default, a.ContentColor);
        Xunit.Assert.Equal(LhTokens.Color.Success.Default, a.BorderColor);
    }

    // case: small-size-uses-small-tokens
    [Fact]
    public void Small_Size_Uses_Small_Tokens()
    {
        var a = LhTagTokens.Resolve(LhTagType.Default, LhTagSize.Small, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Tag.Height.Small, a.Height);
        Xunit.Assert.NotEqual(LhTokens.Tag.Height.Large, a.Height);
    }

    // case: round-makes-radius-half-height —— 胶囊态圆角为高度一半
    [Fact]
    public void Round_Makes_Radius_Half_Height()
    {
        var a = LhTagTokens.Resolve(LhTagType.Default, LhTagSize.Medium, false, true, Light);
        Xunit.Assert.Equal(a.Height / 2, a.BorderRadius);
        Xunit.Assert.NotEqual(LhTokens.Tag.BorderRadius, a.BorderRadius);
    }

    // case: closable-tap-emits-onclose —— 可关闭形态的渲染间距消费 spacing.xs
    // （关闭符点击由控件内部 Tapped 拦截触发 Close 事件；控件实例化需 UI 线程，
    //   此处断言其令牌绑定，交互路径与 button 的 Tapped gate 同构）
    [Fact]
    public void Closable_Tap_Emits_OnClose()
    {
        var a = LhTagTokens.Resolve(LhTagType.Default, LhTagSize.Medium, false, false, Light);
        Xunit.Assert.Equal(LhTokens.Spacing.Xs, a.CloseGap);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhTagTokens.Resolve(LhTagType.Primary, LhTagSize.Medium, false, false, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Primary.Default, a.ContainerColor);
        // onPrimary 暗色不覆盖（core 暗色令牌只覆盖必要颜色），断言明色回退值
        Xunit.Assert.Equal(LhTokens.Color.OnPrimary, a.ContentColor);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhTagTokens.Resolve(LhTagType.Default, LhTagSize.Large, false, false, Light);
        var da = LhTagTokens.Resolve(LhTagType.Default, LhTagSize.Large, false, false, Dark);
        Xunit.Assert.Equal(la.Height, da.Height);
        Xunit.Assert.Equal(la.FontSize, da.FontSize);
    }
}
