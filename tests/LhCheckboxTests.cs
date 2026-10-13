using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Checkbox;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/checkbox.json</c> 的 6 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhCheckboxTokens.Resolve 的返回取自令牌对象，映射即成立。状态机（翻转/不确定→选中）
/// 走 NextChecked 纯函数断言。
/// </summary>
public class LhCheckboxTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: tap-toggles-and-emits-onchange —— 未选中点击后变为选中
    [Fact]
    public void Tap_Toggles_And_Emits_OnChange()
    {
        Xunit.Assert.True(LhCheckboxTokens.NextChecked(false, false));
    }

    // case: tap-checked-unchecks —— 选中点击后取消选中
    [Fact]
    public void Tap_Checked_Unchecks()
    {
        Xunit.Assert.False(LhCheckboxTokens.NextChecked(true, false));
    }

    // case: disabled-blocks-change
    [Fact]
    public void Disabled_Blocks_Change()
    {
        var a = LhCheckboxTokens.Resolve(false, false, true, LhCheckboxSize.Medium, Light);
        Xunit.Assert.False(a.Enabled);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Bg, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Border, a.BorderColor);
        Xunit.Assert.Equal(LhTokens.Color.Disabled.Text, a.MarkColor);
        Xunit.Assert.Equal(LhTokens.Color.Text.Disabled, a.LabelColor);
    }

    // case: checked-uses-primary-tokens —— 选中态实心 primary 底 + onPrimary 指示符
    [Fact]
    public void Checked_Uses_Primary_Tokens()
    {
        var a = LhCheckboxTokens.Resolve(true, false, false, LhCheckboxSize.Medium, Light);
        Xunit.Assert.Equal(LhTokens.Color.Primary.Default, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.OnPrimary, a.MarkColor);
        Xunit.Assert.Equal(LhCheckboxMark.Check, a.Mark);
    }

    // case: unchecked-uses-border-token —— 未选中只描边、无填充
    [Fact]
    public void Unchecked_Uses_Border_Token()
    {
        var a = LhCheckboxTokens.Resolve(false, false, false, LhCheckboxSize.Medium, Light);
        Xunit.Assert.Equal(LhTokens.Color.Border.Base, a.BorderColor);
        Xunit.Assert.Equal(LhColor.Transparent, a.ContainerColor);
        Xunit.Assert.Equal(LhCheckboxMark.None, a.Mark);
    }

    // case: indeterminate-becomes-checked —— 不确定态点击后变为选中，指示符为横杠
    [Fact]
    public void Indeterminate_Becomes_Checked()
    {
        Xunit.Assert.True(LhCheckboxTokens.NextChecked(false, true));
        var a = LhCheckboxTokens.Resolve(false, true, false, LhCheckboxSize.Medium, Light);
        Xunit.Assert.Equal(LhCheckboxMark.Dash, a.Mark);
        Xunit.Assert.Equal(LhTokens.Color.Primary.Default, a.ContainerColor);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhCheckboxTokens.Resolve(true, false, false, LhCheckboxSize.Medium, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Primary.Default, a.ContainerColor);
        // onPrimary 暗色不覆盖（core 暗色令牌只覆盖必要颜色），断言明色回退值
        Xunit.Assert.Equal(LhTokens.Color.OnPrimary, a.MarkColor);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhCheckboxTokens.Resolve(false, false, false, LhCheckboxSize.Large, Light);
        var da = LhCheckboxTokens.Resolve(false, false, false, LhCheckboxSize.Large, Dark);
        Xunit.Assert.Equal(la.Size, da.Size);
        Xunit.Assert.Equal(la.CheckWidth, da.CheckWidth);
    }
}
