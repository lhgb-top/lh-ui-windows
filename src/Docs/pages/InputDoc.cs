// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/input.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/InputPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class InputDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "input",
        "Input 输入框",
        new[]
        {
                "单行文本输入。支持类型、尺寸、禁用、只读、清除与最大字符数。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/input.json` ·",
                "黄金用例：`lh-ui-core/cases/input.json`（8 条，已翻译为 xUnit 测试）"
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
                InputPreviews.Basic,
                new[]
                {
                    "var input = new LhInput",
                    "{",
                    "    Placeholder = \"请输入关键字\",",
                    "};",
                    "input.ValueChange += value => { /* 受控：回写 Value */ };"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "尺寸",
                new[]
                {
                    "三档尺寸映射到 `input.height` / `paddingHorizontal` / `fontSize` 令牌。"
                },
                InputPreviews.Sizes,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "类型与状态",
                new[]
                {
                    "`Password` 以圆点掩码显示；`Disabled` 与 `Readonly` 都不触发 `ValueChange`，",
                    "但只有 `Disabled` 呈现禁用样式；聚焦时描边取 `color.focus` + `input.focusRingWidth`。"
                },
                InputPreviews.TypesAndStates,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "清除与字数",
                new[]
                {
                    "`Clearable` 为 true 且值非空时显示清除按钮，点击触发 `ValueChange(\"\")`；",
                    "`Maxlength` 超出时截断新输入。"
                },
                InputPreviews.ClearAndCount,
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
                        new DocRow(new[] { "`Type`", "输入类型；password 时以圆点掩码显示", "`LhInputType`", "`Text`" }),
                        new DocRow(new[] { "`Size`", "输入框尺寸，映射到尺寸令牌", "`LhInputSize`", "`Medium`" }),
                        new DocRow(new[] { "`Disabled`", "是否禁用（不响应输入、呈现禁用样式）", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Readonly`", "是否只读（可聚焦但不接受输入）", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Clearable`", "是否显示清除按钮", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Maxlength`", "最大字符数；超出时截断新输入", "`int?`", "`null`" }),
                    }),
                    new DocTable("LhInputSize", new[] { "值", "height", "paddingHorizontal", "fontSize" }, new[]
                    {
                        new DocRow(new[] { "`Small`", "`input.height.small`（32epx）", "`input.paddingHorizontal.small`（12epx）", "`input.fontSize.small`（13）" }),
                        new DocRow(new[] { "`Medium`", "`input.height.medium`（40epx）", "`input.paddingHorizontal.medium`（16epx）", "`input.fontSize.medium`（14）" }),
                        new DocRow(new[] { "`Large`", "`input.height.large`（48epx）", "`input.paddingHorizontal.large`（20epx）", "`input.fontSize.large`（16）" }),
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
                        new DocRow(new[] { "`input.height.{small,medium,large}`", "三档高度" }),
                        new DocRow(new[] { "`input.paddingHorizontal.{small,medium,large}`", "三档横向内边距" }),
                        new DocRow(new[] { "`input.fontSize.{small,medium,large}`", "三档字号" }),
                        new DocRow(new[] { "`input.borderRadius` / `input.borderWidth`", "圆角与失焦描边宽度" }),
                        new DocRow(new[] { "`input.focusRingWidth` / `color.focus`", "聚焦描边宽度与颜色" }),
                        new DocRow(new[] { "`input.clearIconSize`", "清除按钮图标尺寸" }),
                        new DocRow(new[] { "`color.background.default` / `color.text.primary`", "容器底/文字色" }),
                        new DocRow(new[] { "`color.text.placeholder`", "占位文字色" }),
                        new DocRow(new[] { "`color.border.base`", "失焦描边色" }),
                        new DocRow(new[] { "`color.disabled.{bg,text}`", "禁用态底/文字" }),
                        new DocRow(new[] { "`typography.fontFamily`", "国风衬线字体栈" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/input.json` 的 8 条断言与 xUnit 测试的对应关系（`tests/LhInputTests.cs`）：",
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
                        new DocRow(new[] { "`clearable-emits-empty-value`", "清除触发 onValueChange(\"\")", "`Clearable_Emits_Empty_Value`" }),
                        new DocRow(new[] { "`disabled-blocks-input`", "禁用态不触发且呈现禁用样式", "`Disabled_Blocks_Input`" }),
                        new DocRow(new[] { "`readonly-blocks-input`", "只读不触发但不呈现禁用样式", "`Readonly_Blocks_Input`" }),
                        new DocRow(new[] { "`placeholder-uses-placeholder-token`", "占位文字取占位令牌", "`Placeholder_Uses_Placeholder_Token`" }),
                        new DocRow(new[] { "`focus-uses-focus-token`", "聚焦描边取 focus 令牌", "`Focus_Uses_Focus_Token`" }),
                        new DocRow(new[] { "`small-size-uses-small-tokens`", "小号消费 small 尺寸令牌", "`Small_Size_Uses_Small_Tokens`" }),
                        new DocRow(new[] { "`maxlength-truncates-input`", "超出 maxlength 截断", "`Maxlength_Truncates_Input`" }),
                    })
                }),
        });
}
