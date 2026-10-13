// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/checkbox.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/CheckboxPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class CheckboxDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "checkbox",
        "Checkbox 复选框",
        new[]
        {
                "复选框。选中画勾、不确定画横杠，点击命中整行（方框 + 标签区域）。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/checkbox.json` ·",
                "黄金用例：`lh-ui-core/cases/checkbox.json`（6 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Form,
        new[]
        {
            new DocSection(
                "基础用法",
                Array.Empty<string>(),
                CheckboxPreviews.Basic,
                new[]
                {
                    "var checkbox = new LhCheckbox",
                    "{",
                    "    Label = \"青花瓷\",",
                    "    Checked = true,",
                    "};",
                    "checkbox.Change += isChecked => { };"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "尺寸",
                new[]
                {
                    "三档尺寸映射到 `checkbox.size.*` 方框尺寸令牌。"
                },
                CheckboxPreviews.Sizes,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "状态",
                new[]
                {
                    "`Disabled` 时不响应点击，底/边/指示符/文字全部取 `color.disabled.*` 令牌；",
                    "`Indeterminate` 为不确定态（横杠），点击后变为选中。"
                },
                CheckboxPreviews.States,
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
                        new DocRow(new[] { "`Change`", "选中态变化回调；disabled 时不触发", "`event Action<bool>`", "—" }),
                        new DocRow(new[] { "`Checked`", "是否选中", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Indeterminate`", "是否为不确定态（横杠指示）；点击后变为选中", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Disabled`", "是否禁用（不响应点击且呈现禁用样式）", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Label`", "文字标签（绘制在方框右侧）", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Size`", "方框尺寸，映射到 `checkbox.size.*` 令牌", "`LhCheckboxSize`", "`Medium`" }),
                    }),
                    new DocTable("LhCheckboxSize", new[] { "值", "尺寸令牌" }, new[]
                    {
                        new DocRow(new[] { "`Small`", "`checkbox.size.small`（14epx）" }),
                        new DocRow(new[] { "`Medium`", "`checkbox.size.medium`（16epx）" }),
                        new DocRow(new[] { "`Large`", "`checkbox.size.large`（18epx）" }),
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
                        new DocRow(new[] { "`checkbox.size.{small,medium,large}`", "三档方框尺寸" }),
                        new DocRow(new[] { "`checkbox.borderRadius` / `checkbox.borderWidth`", "方框圆角与描边宽度" }),
                        new DocRow(new[] { "`checkbox.checkWidth`", "勾形/横杠描边宽度" }),
                        new DocRow(new[] { "`checkbox.gap`", "方框与文字间距" }),
                        new DocRow(new[] { "`color.primary.default` / `color.onPrimary`", "选中/不确定态底色与指示符色" }),
                        new DocRow(new[] { "`color.border.base`", "未选中描边色" }),
                        new DocRow(new[] { "`color.disabled.{bg,border,text}`", "禁用态底/边/指示符" }),
                        new DocRow(new[] { "`color.text.{primary,disabled}`", "标签文字色" }),
                        new DocRow(new[] { "`typography.fontSize.base` / `fontFamily`", "标签字号与字体栈" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/checkbox.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhCheckboxTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`tap-toggles-and-emits-onchange`", "未选中点击后选中", "`Tap_Toggles_And_Emits_OnChange`" }),
                        new DocRow(new[] { "`tap-checked-unchecks`", "选中点击后取消选中", "`Tap_Checked_Unchecks`" }),
                        new DocRow(new[] { "`disabled-blocks-change`", "禁用态不可交互", "`Disabled_Blocks_Change`" }),
                        new DocRow(new[] { "`checked-uses-primary-tokens`", "选中态消费主色令牌", "`Checked_Uses_Primary_Tokens`" }),
                        new DocRow(new[] { "`unchecked-uses-border-token`", "未选中态消费描边令牌", "`Unchecked_Uses_Border_Token`" }),
                        new DocRow(new[] { "`indeterminate-becomes-checked`", "不确定态点击后选中", "`Indeterminate_Becomes_Checked`" }),
                    })
                }),
        });
}
