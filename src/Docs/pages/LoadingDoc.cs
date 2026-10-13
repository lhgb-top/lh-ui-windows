// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/loading.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/LoadingPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class LoadingDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "loading",
        "Loading 加载指示器",
        new[]
        {
                "环形旋转加载指示器。支持尺寸与语义色调。",
                "::: tip 端侧落点说明",
                "旋转由 WinUI 的 `ProgressRing` 平台动画承担（朴素调性：只发几何，动画由平台补齐）——",
                "令牌层只决定直径、描边宽与环色。",
                ":::"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/loading.json` ·",
                "黄金用例：`lh-ui-core/cases/loading.json`（5 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Feedback,
        new[]
        {
            new DocSection(
                "尺寸",
                new[]
                {
                    "三档尺寸映射到 `loading.size.*` 直径令牌。"
                },
                LoadingPreviews.Sizes,
                new[]
                {
                    "var loading = new LhLoading",
                    "{",
                    "    Size = LhLoadingSize.Large,",
                    "    Tone = LhLoadingTone.Primary,",
                    "};"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "色调",
                new[]
                {
                    "`Tone` 决定环色，与语义色令牌一一对应。"
                },
                LoadingPreviews.Tone,
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
                        new DocRow(new[] { "`Size`", "指示器直径，映射到 `loading.size.*` 令牌", "`LhLoadingSize`", "`Medium`" }),
                        new DocRow(new[] { "`Tone`", "环色，取对应语义色令牌", "`LhLoadingTone`", "`Primary`" }),
                    }),
                    new DocTable("LhLoadingSize", new[] { "值", "直径令牌" }, new[]
                    {
                        new DocRow(new[] { "`Small`", "`loading.size.small`（20epx）" }),
                        new DocRow(new[] { "`Medium`", "`loading.size.medium`（28epx）" }),
                        new DocRow(new[] { "`Large`", "`loading.size.large`（36epx）" }),
                    }),
                    new DocTable("LhLoadingTone", new[] { "值", "环色令牌" }, new[]
                    {
                        new DocRow(new[] { "`Primary`", "`color.primary.default`" }),
                        new DocRow(new[] { "`Success`", "`color.success.default`" }),
                        new DocRow(new[] { "`Warning`", "`color.warning.default`" }),
                        new DocRow(new[] { "`Danger`", "`color.danger.default`" }),
                        new DocRow(new[] { "`Info`", "`color.info.default`" }),
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
                        new DocRow(new[] { "`loading.size.{small,medium,large}`", "三档直径" }),
                        new DocRow(new[] { "`loading.strokeWidth`", "加载环描边宽" }),
                        new DocRow(new[] { "`color.{primary,success,warning,danger,info}.default`", "五种环色" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/loading.json` 的 5 条断言与 xUnit 测试的对应关系（`tests/LhLoadingTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`medium-size-uses-medium-token`", "中号消费 medium 直径令牌", "`Medium_Size_Uses_Medium_Token`" }),
                        new DocRow(new[] { "`large-size-uses-large-token`", "大号消费 large 直径令牌", "`Large_Size_Uses_Large_Token`" }),
                        new DocRow(new[] { "`primary-tone-uses-primary-token`", "主色调取主色令牌", "`Primary_Tone_Uses_Primary_Token`" }),
                        new DocRow(new[] { "`success-tone-uses-success-token`", "成功色调取成功色令牌", "`Success_Tone_Uses_Success_Token`" }),
                        new DocRow(new[] { "`render-emits-ring-command`", "有且仅有一条 RING 指令", "`Render_Emits_Ring_Command`" }),
                    })
                }),
        });
}
