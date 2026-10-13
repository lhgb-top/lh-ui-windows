// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/cell.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/CellPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class CellDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "cell",
        "Cell 单元格",
        new[]
        {
                "列表行单元。左侧图标 + 标题（可带必填红星）+ 说明，右侧值与指示箭头，底部可选分隔线。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/cell.json` ·",
                "黄金用例：`lh-ui-core/cases/cell.json`（6 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Basic,
        new[]
        {
            new DocSection(
                "基础用法",
                new[]
                {
                    "标题取 `color.text.primary`，值与说明取 `color.text.secondary`，三者都由令牌驱动。"
                },
                CellPreviews.Basic,
                new[]
                {
                    "var cell = new LhCell",
                    "{",
                    "    Title = \"行标题\",",
                    "    Label = \"说明文字\",",
                    "    Value = \"行内容\",",
                    "};"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "图标与必填",
                new[]
                {
                    "`Icon` 传入本地图标名（见 Icon 文档的字形表），在左侧绘制；",
                    "`Required` 为 true 时标题前绘制 `color.danger.default` 的红星。"
                },
                CellPreviews.IconAndRequired,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "分隔线与点击",
                new[]
                {
                    "`Bordered` 为 true 时底部绘制 `color.border.lighter` 分隔线；`Clickable` 为 true 时",
                    "右侧绘制指示箭头且点击触发 `Click` 事件。"
                },
                CellPreviews.DividerAndClick,
                new[]
                {
                    "var cell = new LhCell",
                    "{",
                    "    Title = \"可点\",",
                    "    Clickable = true,",
                    "    Bordered = true,",
                    "};",
                    "cell.Click += () => { /* 仅 Clickable 为 true 时触发 */ };"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "明暗主题",
                new[]
                {
                    "切换站点右上角主题，模拟器手机框内的文字色与分隔线色即变；真机通过 `LhTheme.Apply(true)` 切换。"
                },
                CellPreviews.Theme,
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
                        new DocRow(new[] { "`Click`", "点击事件；仅 `Clickable` 为 true 时触发", "`event Action`", "—" }),
                        new DocRow(new[] { "`Title`", "标题（spec 的 title 插槽）", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Label`", "标题下说明文字（label 插槽）", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Value`", "右侧值文本（value 插槽）", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Icon`", "左侧图标名（本地图标表解析）", "`string?`", "`null`" }),
                        new DocRow(new[] { "`Required`", "是否在标题前绘制必填红星", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Clickable`", "是否可点击（显隐箭头、gate Click）", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Bordered`", "是否绘制底部分隔线", "`bool`", "`true`" }),
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
                        new DocRow(new[] { "`cell.paddingHorizontal` / `cell.paddingVertical`", "内边距" }),
                        new DocRow(new[] { "`cell.gap`", "标题与说明的垂直间距" }),
                        new DocRow(new[] { "`cell.fontSizeTitle` / `fontSizeLabel` / `fontSizeValue`", "三档字号" }),
                        new DocRow(new[] { "`color.text.{primary,secondary,regular}`", "标题/说明/值/图标色" }),
                        new DocRow(new[] { "`color.danger.default`", "必填红星" }),
                        new DocRow(new[] { "`color.border.lighter`", "底部分隔线" }),
                        new DocRow(new[] { "`icon.size.medium`", "左侧图标尺寸" }),
                        new DocRow(new[] { "`spacing.{sm,xs}`", "图标间距 / 内部元素间距" }),
                        new DocRow(new[] { "`typography.lineHeight.primary`", "文字行高" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/cell.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhCellTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`title-and-value-render`", "标题与值都被渲染", "`Title_And_Value_Render`" }),
                        new DocRow(new[] { "`label-renders-secondary-tone`", "说明文字取次要文字色令牌", "`Label_Renders_Secondary_Tone`" }),
                        new DocRow(new[] { "`required-uses-danger-token`", "必填红星消费危险色令牌", "`Required_Uses_Danger_Token`" }),
                        new DocRow(new[] { "`bordered-emits-line-command`", "分隔线形态发出 LINE 指令", "`Bordered_Emits_Line_Command`" }),
                        new DocRow(new[] { "`clickable-emits-onclick`", "可点击时点击触发 onClick", "`Clickable_Emits_OnClick`" }),
                        new DocRow(new[] { "`not-clickable-no-onclick`", "非可点击时不触发 onClick", "`Not_Clickable_No_OnClick`" }),
                    })
                }),
        });
}
