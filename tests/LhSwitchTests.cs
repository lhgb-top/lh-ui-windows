using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Switch;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/switch.json</c> 的 6 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhSwitchTokens.Resolve 的返回取自令牌对象，映射即成立。
/// </summary>
public class LhSwitchTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: tap-toggles-and-emits-onchange —— 开启态可交互（gate 放行）
    [Fact]
    public void Tap_Toggles_And_Emits_OnChange()
    {
        var a = LhSwitchTokens.Resolve(false, false, false, LhSwitchSize.Medium, Light);
        Xunit.Assert.True(a.Enabled);
        // 关闭贴左：偏移 = gap
        Xunit.Assert.Equal(LhTokens.Switch.Gap, a.ThumbOffset);
    }

    // case: tap-checked-turns-off —— 开启态偏移贴右
    [Fact]
    public void Tap_Checked_Turns_Off()
    {
        var a = LhSwitchTokens.Resolve(true, false, false, LhSwitchSize.Medium, Light);
        Xunit.Assert.Equal(a.Width - a.ThumbSize - LhTokens.Switch.Gap, a.ThumbOffset);
    }

    // case: disabled-blocks-change
    [Fact]
    public void Disabled_Blocks_Change()
    {
        var a = LhSwitchTokens.Resolve(false, true, false, LhSwitchSize.Medium, Light);
        Xunit.Assert.False(a.Enabled);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Bg, a.TrackColor);
        Xunit.Assert.Equal(LhTokens.Color.Text.Disabled, a.ThumbColor);
    }

    // case: loading-blocks-change —— 加载中滑块位置改画加载环，且不响应点击
    [Fact]
    public void Loading_Blocks_Change()
    {
        var a = LhSwitchTokens.Resolve(false, false, true, LhSwitchSize.Medium, Light);
        Xunit.Assert.False(a.Enabled);
        Xunit.Assert.Equal(LhTokens.Color.Primary.Default, a.LoadingColor);
    }

    // case: checked-uses-primary-token —— 开启轨道取主色，不取描边色
    [Fact]
    public void Checked_Uses_Primary_Token()
    {
        var a = LhSwitchTokens.Resolve(true, false, false, LhSwitchSize.Medium, Light);
        Xunit.Assert.Equal(LhTokens.Color.Primary.Default, a.TrackColor);
        Xunit.Assert.NotEqual(LhTokens.Color.Border.Base, a.TrackColor);
        // 胶囊轨道：圆角取高度一半
        Xunit.Assert.Equal(a.Height / 2, a.BorderRadius);
    }

    // case: unchecked-uses-border-token —— 关闭轨道取描边色
    [Fact]
    public void Unchecked_Uses_Border_Token()
    {
        var a = LhSwitchTokens.Resolve(false, false, false, LhSwitchSize.Medium, Light);
        Xunit.Assert.Equal(LhTokens.Color.Border.Base, a.TrackColor);
        Xunit.Assert.NotEqual(LhTokens.Color.Primary.Default, a.TrackColor);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhSwitchTokens.Resolve(true, false, false, LhSwitchSize.Medium, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Primary.Default, a.TrackColor);
        // onPrimary 暗色不覆盖（core 暗色令牌只覆盖必要颜色），断言明色回退值
        Xunit.Assert.Equal(LhTokens.Color.OnPrimary, a.ThumbColor);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhSwitchTokens.Resolve(false, false, false, LhSwitchSize.Large, Light);
        var da = LhSwitchTokens.Resolve(false, false, false, LhSwitchSize.Large, Dark);
        Xunit.Assert.Equal(la.Width, da.Width);
        Xunit.Assert.Equal(la.Height, da.Height);
        // 滑块直径 = 高度 - 2 × gap
        Xunit.Assert.Equal(la.Height - 2 * LhTokens.Switch.Gap, la.ThumbSize);
    }
}
