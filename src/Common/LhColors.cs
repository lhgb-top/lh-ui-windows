using Microsoft.UI;
using Windows.UI;
using Microsoft.UI.Xaml.Media;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Common;

/// <summary>
/// 主题色板：把 core 生成的 LhTokens / LhDarkTokens 平铺为一份不可变记录。
///
/// 暗色令牌是**并列对象**模型（core codegen 的 windows target 只在 LhDarkTokens 里
/// 放 Color 子类，尺寸/动效/圆角暗色不覆盖）。本记录只承载颜色，尺寸令牌由组件直接
/// 取 LhTokens（尺寸无暗色变体）。
///
/// 工厂方法公开是为了让单测能直接构造色板做断言；应用侧应通过 LhTheme 消费。
/// </summary>
public sealed record LhColors
{
    public Color Primary { get; init; }
    public Color PrimaryHover { get; init; }
    public Color PrimaryActive { get; init; }
    public Color PrimaryBg { get; init; }
    public Color PrimaryBorder { get; init; }
    public Color OnPrimary { get; init; }
    public Color Success { get; init; }
    public Color SuccessHover { get; init; }
    public Color SuccessActive { get; init; }
    public Color SuccessBg { get; init; }
    public Color Warning { get; init; }
    public Color WarningHover { get; init; }
    public Color WarningActive { get; init; }
    public Color WarningBg { get; init; }
    public Color Danger { get; init; }
    public Color DangerHover { get; init; }
    public Color DangerActive { get; init; }
    public Color DangerBg { get; init; }
    public Color Info { get; init; }
    public Color InfoHover { get; init; }
    public Color InfoActive { get; init; }
    public Color InfoBg { get; init; }
    public Color TextPrimary { get; init; }
    public Color TextRegular { get; init; }
    public Color TextSecondary { get; init; }
    public Color TextPlaceholder { get; init; }
    public Color TextDisabled { get; init; }
    public Color BackgroundDefault { get; init; }
    public Color BackgroundPage { get; init; }
    public Color BackgroundDisabled { get; init; }
    public Color BackgroundOverlay { get; init; }
    public Color BorderBase { get; init; }
    public Color BorderLight { get; init; }
    public Color BorderLighter { get; init; }
    public Color BorderExtraLight { get; init; }
    public Color DisabledBg { get; init; }
    public Color DisabledText { get; init; }
    public Color DisabledBorder { get; init; }
    public Color Decoration { get; init; }
    public Color HoverBg { get; init; }
    public Color Selected { get; init; }
    public Color Focus { get; init; }
    /// <summary>中性黑（墨）：toast 胶囊底色（toast 契约 tokenRefs: color.ink）</summary>
    public Color Ink { get; init; }
}

/// <summary>明色色板：逐字段取 LhTokens.Color</summary>
public static class LhColorsFactory
{
    public static LhColors Light() =>
        new LhColors
        {
            Primary = LhTokens.Color.Primary.Default,
            PrimaryHover = LhTokens.Color.Primary.Hover,
            PrimaryActive = LhTokens.Color.Primary.Active,
            PrimaryBg = LhTokens.Color.Primary.Bg,
            PrimaryBorder = LhTokens.Color.Primary.Border,
            OnPrimary = LhTokens.Color.OnPrimary,
            Success = LhTokens.Color.Success.Default,
            SuccessHover = LhTokens.Color.Success.Hover,
            SuccessActive = LhTokens.Color.Success.Active,
            SuccessBg = LhTokens.Color.Success.Bg,
            Warning = LhTokens.Color.Warning.Default,
            WarningHover = LhTokens.Color.Warning.Hover,
            WarningActive = LhTokens.Color.Warning.Active,
            WarningBg = LhTokens.Color.Warning.Bg,
            Danger = LhTokens.Color.Danger.Default,
            DangerHover = LhTokens.Color.Danger.Hover,
            DangerActive = LhTokens.Color.Danger.Active,
            DangerBg = LhTokens.Color.Danger.Bg,
            Info = LhTokens.Color.Info.Default,
            InfoHover = LhTokens.Color.Info.Hover,
            InfoActive = LhTokens.Color.Info.Active,
            InfoBg = LhTokens.Color.Info.Bg,
            TextPrimary = LhTokens.Color.Text.Primary,
            TextRegular = LhTokens.Color.Text.Regular,
            TextSecondary = LhTokens.Color.Text.Secondary,
            TextPlaceholder = LhTokens.Color.Text.Placeholder,
            TextDisabled = LhTokens.Color.Text.Disabled,
            BackgroundDefault = LhTokens.Color.Background.Default,
            BackgroundPage = LhTokens.Color.Background.Page,
            BackgroundDisabled = LhTokens.Color.Background.Disabled,
            BackgroundOverlay = LhTokens.Color.Background.Overlay,
            BorderBase = LhTokens.Color.Border.Base,
            BorderLight = LhTokens.Color.Border.Light,
            BorderLighter = LhTokens.Color.Border.Lighter,
            BorderExtraLight = LhTokens.Color.Border.ExtraLight,
            DisabledBg = LhTokens.Color.Disabled.Bg,
            DisabledText = LhTokens.Color.Disabled.Text,
            DisabledBorder = LhTokens.Color.Disabled.Border,
            Decoration = LhTokens.Color.Decoration,
            HoverBg = LhTokens.Color.HoverBg,
            Selected = LhTokens.Color.Selected,
            Focus = LhTokens.Color.Focus,
            Ink = LhTokens.Color.Ink,
        };

    /// <summary>暗色色板：逐字段取 LhDarkTokens.Color（并列对象，仅颜色覆盖）</summary>
    public static LhColors Dark() =>
        new LhColors
        {
            Primary = LhDarkTokens.Color.Primary.Default,
            PrimaryHover = LhDarkTokens.Color.Primary.Hover,
            PrimaryActive = LhDarkTokens.Color.Primary.Active,
            PrimaryBg = LhDarkTokens.Color.Primary.Bg,
            PrimaryBorder = LhDarkTokens.Color.Primary.Border,
            // onPrimary 暗色不覆盖（core 暗色令牌只覆盖必要颜色），回退明色值
            OnPrimary = LhTokens.Color.OnPrimary,
            Success = LhDarkTokens.Color.Success.Default,
            SuccessHover = LhDarkTokens.Color.Success.Hover,
            SuccessActive = LhDarkTokens.Color.Success.Active,
            SuccessBg = LhDarkTokens.Color.Success.Bg,
            Warning = LhDarkTokens.Color.Warning.Default,
            WarningHover = LhDarkTokens.Color.Warning.Hover,
            WarningActive = LhDarkTokens.Color.Warning.Active,
            WarningBg = LhDarkTokens.Color.Warning.Bg,
            Danger = LhDarkTokens.Color.Danger.Default,
            DangerHover = LhDarkTokens.Color.Danger.Hover,
            DangerActive = LhDarkTokens.Color.Danger.Active,
            DangerBg = LhDarkTokens.Color.Danger.Bg,
            Info = LhDarkTokens.Color.Info.Default,
            InfoHover = LhDarkTokens.Color.Info.Hover,
            InfoActive = LhDarkTokens.Color.Info.Active,
            InfoBg = LhDarkTokens.Color.Info.Bg,
            TextPrimary = LhDarkTokens.Color.Text.Primary,
            TextRegular = LhDarkTokens.Color.Text.Regular,
            TextSecondary = LhDarkTokens.Color.Text.Secondary,
            TextPlaceholder = LhDarkTokens.Color.Text.Placeholder,
            TextDisabled = LhDarkTokens.Color.Text.Disabled,
            BackgroundDefault = LhDarkTokens.Color.Background.Default,
            BackgroundPage = LhDarkTokens.Color.Background.Page,
            BackgroundDisabled = LhDarkTokens.Color.Background.Disabled,
            BackgroundOverlay = LhDarkTokens.Color.Background.Overlay,
            BorderBase = LhDarkTokens.Color.Border.Base,
            BorderLight = LhDarkTokens.Color.Border.Light,
            BorderLighter = LhDarkTokens.Color.Border.Lighter,
            BorderExtraLight = LhDarkTokens.Color.Border.ExtraLight,
            DisabledBg = LhDarkTokens.Color.Disabled.Bg,
            DisabledText = LhDarkTokens.Color.Disabled.Text,
            DisabledBorder = LhDarkTokens.Color.Disabled.Border,
            Decoration = LhDarkTokens.Color.Decoration,
            HoverBg = LhDarkTokens.Color.HoverBg,
            Selected = LhDarkTokens.Color.Selected,
            Focus = LhDarkTokens.Color.Focus,
            Ink = LhDarkTokens.Color.Ink,
        };
}
