// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/icon.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/IconPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class IconDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "icon",
        "Icon 图标",
        new[]
        {
                "图标组件。按本地图形体系把图标名解析为字形渲染，支持尺寸与语义色调。",
                "::: tip 端侧落点说明",
                "Windows 端当前用 unicode 字形占位（`LhIconTokens.GlyphFor`，TextBlock 渲染），生产环境应替换为",
                "`FontIcon`（Segoe Fluent Icons 矢量资产）；这与 button 的占位策略一致。已登记图标名见下方「图标名」一节。",
                ":::"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/icon.json` ·",
                "黄金用例：`lh-ui-core/cases/icon.json`（5 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Basic,
        new[]
        {
            new DocSection(
                "尺寸",
                new[]
                {
                    "三档尺寸映射到 `icon.size` 令牌。"
                },
                IconPreviews.Sizes,
                new[]
                {
                    "var icon = new LhIcon",
                    "{",
                    "    Name = \"search\",",
                    "    Size = LhIconSize.Large,",
                    "};"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "色调",
                new[]
                {
                    "`Tone` 决定字形颜色，与语义色令牌一一对应；默认色调取 `color.text.primary`。"
                },
                IconPreviews.Tone,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "图标名",
                new[]
                {
                    "`GlyphFor` 当前登记的图标名与字形：`search` ⌕、`refresh` ↻、`check` ✓、`close` ✕、",
                    "`warning` ⚠、`info` ℹ、`back` ←、`more` ⋯、`star` ★、`edit` ✎、`delete` ⌫、",
                    "`plus` ＋、`minus` －、`arrow` →。未登记名回退 `◆`。"
                },
                IconPreviews.IconNames,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "明暗主题",
                new[]
                {
                    "切换站点右上角主题，模拟器手机框内的图标颜色即变；真机通过 `LhTheme.Apply(true)` 切换。"
                },
                IconPreviews.Theme,
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
                        new DocRow(new[] { "`Name`", "图标名（本地图标表解析，未登记名回退 `◆`）", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Size`", "图标尺寸，映射到 `icon.size.*` 令牌", "`LhIconSize`", "`Medium`" }),
                        new DocRow(new[] { "`Tone`", "图标色调，映射到语义色 / 文字色令牌", "`LhIconTone`", "`Regular`" }),
                    }),
                    new DocTable("LhIconSize", new[] { "值", "尺寸令牌" }, new[]
                    {
                        new DocRow(new[] { "`Small`", "`icon.size.small`（16epx）" }),
                        new DocRow(new[] { "`Medium`", "`icon.size.medium`（18epx）" }),
                        new DocRow(new[] { "`Large`", "`icon.size.large`（20epx）" }),
                    }),
                    new DocTable("LhIconTone", new[] { "值", "颜色令牌" }, new[]
                    {
                        new DocRow(new[] { "`Default`", "`color.text.primary`" }),
                        new DocRow(new[] { "`Primary`", "`color.primary.default`" }),
                        new DocRow(new[] { "`Success`", "`color.success.default`" }),
                        new DocRow(new[] { "`Warning`", "`color.warning.default`" }),
                        new DocRow(new[] { "`Danger`", "`color.danger.default`" }),
                        new DocRow(new[] { "`Info`", "`color.info.default`" }),
                        new DocRow(new[] { "`Regular`", "`color.text.regular`" }),
                        new DocRow(new[] { "`Secondary`", "`color.text.secondary`" }),
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
                        new DocRow(new[] { "`icon.size.{small,medium,large}`", "三档尺寸" }),
                        new DocRow(new[] { "`color.text.{primary,regular,secondary}`", "中性色调" }),
                        new DocRow(new[] { "`color.{primary,success,warning,danger,info}.default`", "语义色调" }),
                        new DocRow(new[] { "`typography.fontFamily`", "字形字体栈" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/icon.json` 的 5 条断言与 xUnit 测试的对应关系（`tests/LhIconTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`click-emits-onclick`", "gate 放行（图标可交互）", "`Click_Emits_OnClick`" }),
                        new DocRow(new[] { "`default-tone-uses-text-primary`", "默认色调取主要文字色令牌", "`Default_Tone_Uses_Text_Primary`" }),
                        new DocRow(new[] { "`primary-tone-uses-primary-token`", "主色调取主色令牌", "`Primary_Tone_Uses_Primary_Token`" }),
                        new DocRow(new[] { "`small-size-uses-small-token`", "小号消费 small 尺寸令牌", "`Small_Size_Uses_Small_Token`" }),
                        new DocRow(new[] { "`render-emits-icon-command`", "有且仅有一条图标指令", "`Render_Emits_Single_Icon_Command`" }),
                    })
                }),
        });
}
