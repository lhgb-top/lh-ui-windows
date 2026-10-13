// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/textarea.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/TextareaPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class TextareaDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "textarea",
        "Textarea 多行输入框",
        new[]
        {
                "多行文本输入。按 `Rows` 给出初始可见行数，内容超出自动增高，支持字数统计。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/textarea.json` ·",
                "黄金用例：`lh-ui-core/cases/textarea.json`（6 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Form,
        new[]
        {
            new DocSection(
                "基础用法",
                new[]
                {
                    "受控组件：宿主监听 `ValueChange` 并把新值回写 `Value`。"
                },
                TextareaPreviews.Basic,
                new[]
                {
                    "var textarea = new LhTextarea",
                    "{",
                    "    Rows = 3,",
                    "    Placeholder = \"请输入描述\",",
                    "};",
                    "textarea.ValueChange += value => { /* 受控：回写 Value */ };"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "字数统计",
                new[]
                {
                    "`ShowCount` 为 true 时右下角绘制字数统计；声明 `Maxlength` 时格式为「当前/最大」，",
                    "否则只显示当前长度。`Maxlength` 超出时截断新输入。"
                },
                TextareaPreviews.Count,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "状态",
                new[]
                {
                    "`Disabled` 与 `Readonly` 都不触发 `ValueChange`，但只有 `Disabled` 呈现禁用样式；",
                    "聚焦时描边取 `color.focus` + `textarea.focusRingWidth`。"
                },
                TextareaPreviews.States,
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
                        new DocRow(new[] { "`ValueChange`", "值变化回调（受控）；disabled / readonly 时不触发", "`event Action<string>`", "—" }),
                        new DocRow(new[] { "`Focus`", "获得焦点时触发", "`event Action`", "—" }),
                        new DocRow(new[] { "`Blur`", "失去焦点时触发", "`event Action`", "—" }),
                        new DocRow(new[] { "`Value`", "当前文本值（受控）", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Placeholder`", "占位提示文字，取 `color.text.placeholder` 令牌", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Rows`", "初始可见行数；高度随行数与内容增长", "`int`", "`3`" }),
                        new DocRow(new[] { "`Disabled`", "是否禁用（不响应输入、呈现禁用样式）", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Readonly`", "是否只读（可聚焦但不接受输入）", "`bool`", "`false`" }),
                        new DocRow(new[] { "`ShowCount`", "是否在右下角显示字数统计", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Maxlength`", "最大字符数；超出时截断新输入，showCount 时作为分母", "`int?`", "`null`" }),
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
                        new DocRow(new[] { "`textarea.paddingVertical` / `paddingHorizontal`", "内边距" }),
                        new DocRow(new[] { "`textarea.fontSize`", "字号" }),
                        new DocRow(new[] { "`textarea.borderRadius` / `borderWidth`", "圆角与失焦描边宽度" }),
                        new DocRow(new[] { "`textarea.focusRingWidth` / `color.focus`", "聚焦描边宽度与颜色" }),
                        new DocRow(new[] { "`textarea.countFontSize` / `color.text.secondary`", "字数统计字号/颜色" }),
                        new DocRow(new[] { "`textarea.minHeight`", "最小高度（行数推算结果不小于它）" }),
                        new DocRow(new[] { "`color.background.default` / `color.text.primary`", "容器底/文字色" }),
                        new DocRow(new[] { "`color.text.placeholder`", "占位文字色" }),
                        new DocRow(new[] { "`color.border.base`", "失焦描边色" }),
                        new DocRow(new[] { "`color.disabled.{bg,text}`", "禁用态底/文字" }),
                        new DocRow(new[] { "`typography.lineHeight.primary`", "行高（按 rows 推算高度）" }),
                        new DocRow(new[] { "`typography.fontFamily`", "国风衬线字体栈" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/textarea.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhTextareaTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`input-emits-onvaluechange`", "输入触发 onValueChange", "`Input_Emits_OnValueChange`" }),
                        new DocRow(new[] { "`disabled-blocks-input`", "禁用态不触发且呈现禁用样式", "`Disabled_Blocks_Input`" }),
                        new DocRow(new[] { "`showcount-renders-count`", "「当前/最大」取 countFontSize 令牌", "`ShowCount_Renders_Count`" }),
                        new DocRow(new[] { "`showcount-without-maxlength-renders-length-only`", "无 maxlength 只显示长度", "`ShowCount_Without_Maxlength_Renders_Length_Only`" }),
                        new DocRow(new[] { "`focus-uses-focus-token`", "聚焦描边取 focus 令牌", "`Focus_Uses_Focus_Token`" }),
                        new DocRow(new[] { "`maxlength-truncates-input`", "超出 maxlength 截断", "`Maxlength_Truncates_Input`" }),
                    })
                }),
        });
}
