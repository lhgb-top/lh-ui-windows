// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/text.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/TextPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class TextDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "text",
        "Text 文本",
        new[]
        {
                "文本组件。按级别、字重、色调与对齐渲染单段文字，全部排版值取自 typography 令牌。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/text.json` ·",
                "黄金用例：`lh-ui-core/cases/text.json`（5 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Basic,
        new[]
        {
            new DocSection(
                "级别",
                new[]
                {
                    "六档级别映射到 `typography.fontSize.*` 令牌；默认级别为 `base`。"
                },
                TextPreviews.Levels,
                new[]
                {
                    "var text = new LhText",
                    "{",
                    "    Text = \"青花瓷\",",
                    "    Level = LhTextLevel.Large,",
                    "};"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "字重与色调",
                new[]
                {
                    "`Weight` 映射到 `typography.fontWeight.*` 令牌；`Tone` 映射到 `color.text.*`（selected 取 `color.selected`）。"
                },
                TextPreviews.WeightAndTone,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "对齐",
                new[]
                {
                    "`Align` 决定文字在行盒宽度内的水平定位（非设计令牌，映射到 WinUI 的 `TextAlignment`）。"
                },
                TextPreviews.Alignment,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "明暗主题",
                new[]
                {
                    "切换站点右上角主题，模拟器手机框内的文字色即变；真机通过 `LhTheme.Apply(true)` 切换。"
                },
                TextPreviews.Theme,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "API",
                Array.Empty<string>(),
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "属性", "说明", "类型", "默认值" }, new[]
                    {
                        new DocRow(new[] { "`Text`", "文字内容（spec 的 default 插槽）", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Level`", "字号级别，映射到 `typography.fontSize.*`", "`LhTextLevel`", "`Base`" }),
                        new DocRow(new[] { "`Weight`", "字重，映射到 `typography.fontWeight.*`", "`LhTextWeight`", "`Primary`" }),
                        new DocRow(new[] { "`Tone`", "文字色调，映射到 `color.text.*` / `color.selected`", "`LhTextTone`", "`Regular`" }),
                        new DocRow(new[] { "`Align`", "水平对齐（映射到 `TextAlignment`）", "`LhTextAlign`", "`Start`" }),
                    }),
                    new DocTable("LhTextLevel", new[] { "值", "字号令牌" }, new[]
                    {
                        new DocRow(new[] { "`ExtraLarge`", "`typography.fontSize.extraLarge`（20epx）" }),
                        new DocRow(new[] { "`Large`", "`typography.fontSize.large`（18epx）" }),
                        new DocRow(new[] { "`Medium`", "`typography.fontSize.medium`（16epx）" }),
                        new DocRow(new[] { "`Base`", "`typography.fontSize.base`（14epx）" }),
                        new DocRow(new[] { "`Small`", "`typography.fontSize.small`（13epx）" }),
                        new DocRow(new[] { "`ExtraSmall`", "`typography.fontSize.extraSmall`（12epx）" }),
                    }),
                    new DocTable("LhTextWeight", new[] { "值", "字重令牌" }, new[]
                    {
                        new DocRow(new[] { "`Primary`", "`typography.fontWeight.primary`（400，端侧映射 Normal）" }),
                        new DocRow(new[] { "`Bold`", "`typography.fontWeight.bold`（600，端侧映射 SemiBold）" }),
                    }),
                    new DocTable("LhTextTone", new[] { "值", "颜色令牌" }, new[]
                    {
                        new DocRow(new[] { "`Primary`", "`color.text.primary`" }),
                        new DocRow(new[] { "`Regular`", "`color.text.regular`" }),
                        new DocRow(new[] { "`Secondary`", "`color.text.secondary`" }),
                        new DocRow(new[] { "`Placeholder`", "`color.text.placeholder`" }),
                        new DocRow(new[] { "`Disabled`", "`color.text.disabled`" }),
                        new DocRow(new[] { "`Selected`", "`color.selected`" }),
                    }),
                    new DocTable("LhTextAlign", new[] { "值", "WinUI 映射" }, new[]
                    {
                        new DocRow(new[] { "`Start`", "`TextAlignment.Left`" }),
                        new DocRow(new[] { "`Center`", "`TextAlignment.Center`" }),
                        new DocRow(new[] { "`End`", "`TextAlignment.Right`" }),
                    })
                }),
            new DocSection(
                "令牌对照",
                Array.Empty<string>(),
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "令牌", "用途" }, new[]
                    {
                        new DocRow(new[] { "`typography.fontSize.{extraLarge…extraSmall}`", "六档字号" }),
                        new DocRow(new[] { "`typography.fontWeight.{primary,bold}`", "两档字重" }),
                        new DocRow(new[] { "`typography.lineHeight.primary`", "行高（行盒高度 = 字号 × 行高）" }),
                        new DocRow(new[] { "`typography.fontFamily`", "国风衬线字体栈" }),
                        new DocRow(new[] { "`color.text.{primary,regular,secondary,placeholder,disabled}`", "中性色调" }),
                        new DocRow(new[] { "`color.selected`", "选中态色调" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/text.json` 的 5 条断言与 xUnit 测试的对应关系（`tests/LhTextTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`default-level-uses-base-tokens`", "默认级别消费 base 排版令牌", "`Default_Level_Uses_Base_Tokens`" }),
                        new DocRow(new[] { "`large-level-uses-large-token`", "大级别消费 large 字号令牌", "`Large_Level_Uses_Large_Token`" }),
                        new DocRow(new[] { "`bold-weight-uses-bold-token`", "粗体消费 bold 字重令牌", "`Bold_Weight_Uses_Bold_Token`" }),
                        new DocRow(new[] { "`secondary-tone-uses-text-secondary`", "次要色调取次要文字色令牌", "`Secondary_Tone_Uses_Text_Secondary`" }),
                        new DocRow(new[] { "`render-emits-single-text-command`", "有且仅有一条 TEXT 指令", "`Render_Emits_Single_Text_Command`" }),
                    })
                }),
        });
}
