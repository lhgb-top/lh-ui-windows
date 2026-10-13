// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/button.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/ButtonPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class ButtonDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "button",
        "Button 按钮",
        new[]
        {
                "基础操作按钮。触发即时操作，支持类型、尺寸、加载、禁用、图标与国风花纹底。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/button.json` ·",
                "黄金用例：`lh-ui-core/cases/button.json`（8 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Basic,
        new[]
        {
            new DocSection(
                "类型",
                new[]
                {
                    "六种类型决定底色与文字色。"
                },
                ButtonPreviews.Types,
                new[]
                {
                    "var button = new LhButton",
                    "{",
                    "    Type = LhButtonType.Primary,",
                    "    Text = \"主要按钮\",",
                    "};",
                    "button.Click += OnButtonClick;"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "尺寸",
                new[]
                {
                    "三档尺寸映射到 `button.height` / `paddingHorizontal` / `fontSize` / `iconSize` 令牌。"
                },
                ButtonPreviews.Sizes,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "状态",
                new[]
                {
                    "禁用与加载中都不触发 `Click`；两者共存时按**禁用优先**处理（黄金用例 `disabled-precedes-loading`）。"
                },
                ButtonPreviews.States,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "图标与花纹底",
                new[]
                {
                    "图标名按本地图标体系解析（`LhButtonTokens.IconFor`，当前支持 search / refresh / check / close，",
                    "未登记名不绘制）。花纹底消费 `color.decoration` 令牌，45° 斜纹叠加。"
                },
                ButtonPreviews.IconAndPattern,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "明暗主题",
                new[]
                {
                    "切换站点右上角主题，模拟器手机框内的按钮颜色即变；真机通过 `LhTheme.Apply(true)` 切换。"
                },
                ButtonPreviews.Theme,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "API",
                new[]
                {
                    "禁用态覆盖：底色 `color.disabled.bg`、文字 `color.disabled.text`、描边 `color.disabled.border`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "属性", "说明", "类型", "默认值" }, new[]
                    {
                        new DocRow(new[] { "`Click`", "点击事件；`Disabled` 或 `Loading` 为 true 时不触发", "`event Action`", "—" }),
                        new DocRow(new[] { "`Type`", "按钮类型，决定底色与文字色", "`LhButtonType`", "`Default`" }),
                        new DocRow(new[] { "`Size`", "按钮尺寸，映射到尺寸令牌", "`LhButtonSize`", "`Medium`" }),
                        new DocRow(new[] { "`Disabled`", "是否禁用", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Loading`", "是否加载中（显示指示器，不触发 Click）", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Icon`", "图标名（本地图标体系解析）", "`string?`", "`null`" }),
                        new DocRow(new[] { "`BgPattern`", "是否绘制国风花纹底纹", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Text`", "按钮文字内容（spec 的 default 插槽）", "`string`", "`\"\"`" }),
                    }),
                    new DocTable("LhButtonType", new[] { "值", "底色", "文字色" }, new[]
                    {
                        new DocRow(new[] { "`Default`", "`color.background.default`", "`color.text.primary`" }),
                        new DocRow(new[] { "`Primary`", "`color.primary.default`", "`color.onPrimary`" }),
                        new DocRow(new[] { "`Success`", "`color.success.default`", "`color.onPrimary`" }),
                        new DocRow(new[] { "`Warning`", "`color.warning.default`", "`color.onPrimary`" }),
                        new DocRow(new[] { "`Danger`", "`color.danger.default`", "`color.onPrimary`" }),
                        new DocRow(new[] { "`Info`", "`color.info.default`", "`color.onPrimary`" }),
                    }),
                    new DocTable("LhButtonSize", new[] { "值", "height", "paddingHorizontal", "fontSize", "iconSize" }, new[]
                    {
                        new DocRow(new[] { "`Small`", "`button.height.small`（32epx）", "`button.paddingHorizontal.small`（16epx）", "`button.fontSize.small`（13）", "`button.iconSize.small`（16）" }),
                        new DocRow(new[] { "`Medium`", "`button.height.medium`（40epx）", "`button.paddingHorizontal.medium`（20epx）", "`button.fontSize.medium`（14）", "`button.iconSize.medium`（18）" }),
                        new DocRow(new[] { "`Large`", "`button.height.large`（48epx）", "`button.paddingHorizontal.large`（24epx）", "`button.fontSize.large`（16）", "`button.iconSize.large`（20）" }),
                    })
                }),
            new DocSection(
                "令牌对照",
                new[]
                {
                    "按钮消费的令牌（与 `specs/button.json` 的 `tokenRefs` 逐条对齐）："
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "令牌", "用途" }, new[]
                    {
                        new DocRow(new[] { "`button.height.{small,medium,large}`", "三档高度" }),
                        new DocRow(new[] { "`button.paddingHorizontal.{small,medium,large}`", "三档横向内边距" }),
                        new DocRow(new[] { "`button.fontSize.{small,medium,large}`", "三档字号" }),
                        new DocRow(new[] { "`button.iconSize.{small,medium,large}`", "三档图标尺寸" }),
                        new DocRow(new[] { "`button.borderRadius` / `button.borderWidth`", "圆角（跟随 `radius.base`）与描边宽度" }),
                        new DocRow(new[] { "`button.fontWeight`", "字重（跟随 `typography.fontWeight.primary`，端侧映射 FontWeights）" }),
                        new DocRow(new[] { "`color.primary.default` / `color.onPrimary`", "主色类型底/文字" }),
                        new DocRow(new[] { "`color.{success,warning,danger,info}.default`", "语义类型底色" }),
                        new DocRow(new[] { "`color.background.default` / `color.text.primary` / `color.border.base`", "默认类型底/文字/描边" }),
                        new DocRow(new[] { "`color.disabled.{bg,text,border}`", "禁用态三色" }),
                        new DocRow(new[] { "`color.decoration`", "花纹底纹色" }),
                        new DocRow(new[] { "`spacing.xs`", "图标与文字间距" }),
                        new DocRow(new[] { "`typography.fontFamily`", "国风衬线字体栈" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/button.json` 的 8 条断言与 xUnit 测试的对应关系（`tests/LhButtonTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`click-emits-onclick`", "gate 放行（可交互）", "`Enabled_Button_Is_Interactive`" }),
                        new DocRow(new[] { "`disabled-blocks-click`", "禁用态不可交互", "`Disabled_Is_Not_Interactive`" }),
                        new DocRow(new[] { "`loading-blocks-click`", "加载态不可交互", "`Loading_Is_Not_Interactive`" }),
                        new DocRow(new[] { "`disabled-precedes-loading`", "禁用优先 + 禁用样式", "`Disabled_Precedes_Loading`" }),
                        new DocRow(new[] { "`default-type-renders-text`", "默认类型中性底/文字", "`Default_Type_Uses_Neutral_Tokens`" }),
                        new DocRow(new[] { "`primary-type-uses-primary-tokens`", "主色类型消费主色令牌", "`Primary_Type_Uses_Primary_Tokens`" }),
                        new DocRow(new[] { "`small-size-uses-small-tokens`", "小号消费 small 尺寸令牌", "`Small_Size_Uses_Small_Tokens`" }),
                        new DocRow(new[] { "`bgpattern-uses-decoration-token`", "花纹底消费装饰色", "`BgPattern_Uses_Decoration_Token`" }),
                    })
                }),
        });
}
