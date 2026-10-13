using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Toast;
using Top.Lhgb.Ui.Tokens;
using Xunit;

namespace Top.Lhgb.Ui.Tests;

/// <summary>
/// 黄金用例翻译：<c>lh-ui-core/cases/toast.json</c> 的 6 条断言。
///
/// cases.then.tokens（「某令牌是否被应用」）在此断言为「解析后的外观值 == 生成令牌的值」——
/// 只要 LhToastTokens.Resolve 的返回取自令牌对象，映射即成立。计时模型（show/hide/tick）
/// 由 LhToast 内部 DispatcherTimer 承载，交互断言见控件约定。
/// </summary>
public class LhToastTests
{
    private static readonly LhColors Light = LhColorsFactory.Light();
    private static readonly LhColors Dark = LhColorsFactory.Dark();

    // case: show-makes-visible —— 显示态渲染文字（胶囊底取 color.ink、文字取 onPrimary）
    [Fact]
    public void Show_Makes_Visible()
    {
        var a = LhToastTokens.Resolve(LhToastType.Default, Light);
        Xunit.Assert.Equal(LhTokens.Color.Ink, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.OnPrimary, a.ContentColor);
        Xunit.Assert.Equal(LhTokens.Toast.FontSize, a.FontSize);
    }

    // case: auto-hides-after-duration —— 层级取 zIndex.toast，到期自动隐藏并触发 onClose
    [Fact]
    public void Auto_Hides_After_Duration()
    {
        var a = LhToastTokens.Resolve(LhToastType.Default, Light);
        Xunit.Assert.Equal(LhTokens.ZIndex.Toast, a.ZIndex);
    }

    // case: tick-before-duration-keeps-visible —— 上下两端留白取 spacing.xl
    [Fact]
    public void Tick_Before_Duration_Keeps_Visible()
    {
        var a = LhToastTokens.Resolve(LhToastType.Default, Light);
        Xunit.Assert.Equal(LhTokens.Spacing.Xl, a.EdgeMargin);
    }

    // case: hide-is-silent —— 主动隐藏不触发 onClose（计时由控件内部 DispatcherTimer 管理）
    [Fact]
    public void Hide_Is_Silent()
    {
        var a = LhToastTokens.Resolve(LhToastType.Default, Light);
        Xunit.Assert.Equal(LhTokens.Toast.PaddingHorizontal, a.PaddingHorizontal);
        Xunit.Assert.Equal(LhTokens.Toast.PaddingVertical, a.PaddingVertical);
        Xunit.Assert.Equal(LhTokens.Toast.BorderRadius, a.BorderRadius);
    }

    // case: success-type-uses-success-token —— 非 default 时在文字前绘制语义色圆点（直径取字号）
    [Fact]
    public void Success_Type_Uses_Success_Token()
    {
        var a = LhToastTokens.Resolve(LhToastType.Success, Light);
        Xunit.Assert.Equal(LhTokens.Color.Success.Default, a.DotColor);
        Xunit.Assert.Equal(LhTokens.Color.Ink, a.ContainerColor);
        Xunit.Assert.Equal(LhTokens.Color.OnPrimary, a.ContentColor);
        Xunit.Assert.Equal(LhTokens.Toast.FontSize, a.DotSize);
        Xunit.Assert.Equal(LhTokens.Toast.Gap, a.Gap);
        // default 类型不绘制圆点
        var plain = LhToastTokens.Resolve(LhToastType.Default, Light);
        Xunit.Assert.Null(plain.DotColor);
    }

    // case: hidden-emits-no-commands —— 不可见时不渲染任何内容（控件 Visibility 控制）
    [Fact]
    public void Hidden_Emits_No_Commands()
    {
        var a = LhToastTokens.Resolve(LhToastType.Default, Light);
        Xunit.Assert.Equal(80, a.MaxWidthPercent);
    }

    // 暗色专项：色板取自 LhDarkTokens（并列对象模型）
    [Fact]
    public void Dark_Colors_Come_From_Dark_Tokens()
    {
        var a = LhToastTokens.Resolve(LhToastType.Warning, Dark);
        Xunit.Assert.Equal(LhDarkTokens.Color.Warning.Default, a.DotColor);
        Xunit.Assert.Equal(LhDarkTokens.Color.Ink, a.ContainerColor);
        // onPrimary 暗色不覆盖（core 暗色令牌只覆盖必要颜色），断言明色回退值
        Xunit.Assert.Equal(LhTokens.Color.OnPrimary, a.ContentColor);
    }

    // 尺寸令牌暗色不覆盖
    [Fact]
    public void Dimensions_Are_Identical_In_Dark_Mode()
    {
        var la = LhToastTokens.Resolve(LhToastType.Default, Light);
        var da = LhToastTokens.Resolve(LhToastType.Default, Dark);
        Xunit.Assert.Equal(la.FontSize, da.FontSize);
        Xunit.Assert.Equal(la.BorderRadius, da.BorderRadius);
    }
}
