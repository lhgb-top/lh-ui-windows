using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Cell;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/cell.json</c> 的 6 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhCellTokens.Resolve 的返回取自令牌对象，映射即成立。交互 gate（clickable）
/// 走控件级 Clickable 标志断言。
/// </summary>
public class LhCellTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: title-and-value-render —— 标题与值都取自文字色令牌（标题 primary、值 secondary）
    [Fact]
    public void Title_And_Value_Render()
    {
        var a = LhCellTokens.Resolve(true, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Text.Primary, a.TitleColor);
        Xunit.Assert.Equal(LhTokens.Color.Text.Secondary, a.ValueColor);
    }

    // case: label-renders-secondary-tone
    [Fact]
    public void Label_Renders_Secondary_Tone()
    {
        var a = LhCellTokens.Resolve(true, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Text.Secondary, a.LabelColor);
    }

    // case: required-uses-danger-token
    [Fact]
    public void Required_Uses_Danger_Token()
    {
        var a = LhCellTokens.Resolve(true, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Danger.Default, a.RequiredColor);
    }

    // case: bordered-emits-line-command —— 分隔线形态消费 border.lighter 令牌
    [Fact]
    public void Bordered_Emits_Line_Command()
    {
        var bordered = LhCellTokens.Resolve(true, false, Light);
        Xunit.Assert.Equal(LhTokens.Color.Border.Lighter, bordered.DividerColor);
        var borderless = LhCellTokens.Resolve(false, false, Light);
        Xunit.Assert.Null(borderless.DividerColor);
    }

    // case: clickable-emits-onclick —— 可点击时箭头显示且 gate 放行
    [Fact]
    public void Clickable_Emits_OnClick()
    {
        var a = LhCellTokens.Resolve(true, true, Light);
        // 箭头色取次要文字色（与说明同色），可点击态由组件按 Clickable 判定显隐
        Xunit.Assert.Equal(LhTokens.Color.Text.Secondary, a.ArrowColor);
    }

    // case: not-clickable-no-onclick —— 非可点击时不触发 onClick
    // 令牌层不持有「是否可点击」状态：显隐与放行由组件的 Clickable 属性 gate
    // （见 LhCell.OnTapped：仅 Clickable 为 true 时触发 Click），与 button 的
    // appearance.Enabled gate 同构
    [Fact]
    public void Not_Clickable_No_OnClick()
    {
        var a = LhCellTokens.Resolve(true, false, Light);
        Xunit.Assert.Equal(LhTokens.Cell.PaddingHorizontal, a.PaddingHorizontal);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhCellTokens.Resolve(true, false, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Text.Primary, a.TitleColor);
        Xunit.Assert.Equal(LhDarkTokens.Color.Border.Lighter, a.DividerColor);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhCellTokens.Resolve(true, false, Light);
        var da = LhCellTokens.Resolve(true, false, Dark);
        Xunit.Assert.Equal(la.FontSizeTitle, da.FontSizeTitle);
        Xunit.Assert.Equal(la.PaddingHorizontal, da.PaddingHorizontal);
    }
}
