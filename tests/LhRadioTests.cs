using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Radio;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/radio.json</c> 的 6 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhRadioTokens.Resolve 的返回取自令牌对象，映射即成立。交互 gate（已选中不触发、
/// 禁用不触发）走组件 OnTapped 的 checked && enabled 判定（见 LhRadio.cs）。
/// </summary>
public class LhRadioTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: tap-unchecked-emits-onchange —— 未选中可点击（enabled 为 true 且未选中时放行）
    [Fact]
    public void Tap_Unchecked_Emits_OnChange()
    {
        var a = LhRadioTokens.Resolve(false, false, LhRadioSize.Medium, Light);
        Xunit.Assert.True(a.Enabled);
        Xunit.Assert.False(a.ShowDot);
    }

    // case: tap-checked-keeps-checked —— 已选中态圆点显示且描边取主色
    [Fact]
    public void Tap_Checked_Keeps_Checked()
    {
        var a = LhRadioTokens.Resolve(true, false, LhRadioSize.Medium, Light);
        Xunit.Assert.True(a.ShowDot);
        Xunit.Assert.Equal(LhTokens.Color.Primary.Default, a.RingColor);
        Xunit.Assert.Equal(LhTokens.Color.Primary.Default, a.DotColor);
    }

    // case: disabled-blocks-change
    [Fact]
    public void Disabled_Blocks_Change()
    {
        var a = LhRadioTokens.Resolve(false, true, LhRadioSize.Medium, Light);
        Xunit.Assert.False(a.Enabled);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Border, a.RingColor);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Text, a.DotColor);
        Xunit.Assert.Equal(LhTokens.Color.Text.Disabled, a.LabelColor);
        // 禁用态即使 checked 也不绘制圆点
        var checkedDisabled = LhRadioTokens.Resolve(true, true, LhRadioSize.Medium, Light);
        Xunit.Assert.False(checkedDisabled.ShowDot);
    }

    // case: checked-uses-primary-tokens
    [Fact]
    public void Checked_Uses_Primary_Tokens()
    {
        var a = LhRadioTokens.Resolve(true, false, LhRadioSize.Medium, Light);
        Xunit.Assert.Equal(LhTokens.Color.Primary.Default, a.RingColor);
        // 内圆点直径 = 尺寸 × radio.dotScale
        Xunit.Assert.Equal(a.Size * LhTokens.Radio.DotScale, a.DotSize);
    }

    // case: unchecked-uses-border-token
    [Fact]
    public void Unchecked_Uses_Border_Token()
    {
        var a = LhRadioTokens.Resolve(false, false, LhRadioSize.Medium, Light);
        Xunit.Assert.Equal(LhTokens.Color.Border.Base, a.RingColor);
        Xunit.Assert.NotEqual(LhTokens.Color.Primary.Default, a.RingColor);
    }

    // case: small-size-uses-small-token
    [Fact]
    public void Small_Size_Uses_Small_Token()
    {
        var a = LhRadioTokens.Resolve(false, false, LhRadioSize.Small, Light);
        Xunit.Assert.Equal(LhTokens.Radio.Size.Small, a.Size);
        Xunit.Assert.NotEqual(LhTokens.Radio.Size.Large, a.Size);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhRadioTokens.Resolve(true, false, LhRadioSize.Medium, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Primary.Default, a.RingColor);
        Xunit.Assert.Equal(LhDarkTokens.Color.Primary.Default, a.DotColor);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhRadioTokens.Resolve(false, false, LhRadioSize.Large, Light);
        var da = LhRadioTokens.Resolve(false, false, LhRadioSize.Large, Dark);
        Xunit.Assert.Equal(la.Size, da.Size);
        Xunit.Assert.Equal(la.RingWidth, da.RingWidth);
    }
}
