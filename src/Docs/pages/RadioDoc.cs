// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/radio.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/RadioPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class RadioDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "radio",
        "Radio 单选框",
        new[]
        {
                "单选框。圆环 + 内圆点，点击命中整行；组件本身不含组逻辑，由宿主维护单选组。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/radio.json` ·",
                "黄金用例：`lh-ui-core/cases/radio.json`（6 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Form,
        new[]
        {
            new DocSection(
                "基础用法",
                new[]
                {
                    "宿主监听 `Change` 后把同组其它项的 `Checked` 置为 false，即实现单选组。"
                },
                RadioPreviews.Basic,
                new[]
                {
                    "var radio = new LhRadio",
                    "{",
                    "    Label = \"青花瓷\",",
                    "    Value = \"qinghua\",",
                    "    Checked = true,",
                    "};",
                    "radio.Change += isChecked => { /* 仅未选中且未禁用时触发；宿主维护单选组 */ };"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "尺寸",
                new[]
                {
                    "三档尺寸映射到 `radio.size.*` 圆环尺寸令牌；内圆点直径 = 尺寸 × `radio.dotScale`。"
                },
                RadioPreviews.Sizes,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "状态",
                new[]
                {
                    "`Disabled` 时不响应点击，外环取 `color.disabled.border`、圆点取 `color.disabled.text`；",
                    "已选中态再次点击不触发 `Change`。"
                },
                RadioPreviews.States,
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
                        new DocRow(new[] { "`Change`", "选中态变化回调；仅未选中且未禁用时触发（payload 恒为 true）", "`event Action<bool>`", "—" }),
                        new DocRow(new[] { "`Checked`", "是否选中", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Disabled`", "是否禁用（不响应点击且呈现禁用样式）", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Label`", "文字标签（绘制在圆环右侧）", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Value`", "单选值（组内标识，组件本身不含组逻辑）", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Size`", "圆环尺寸，映射到 `radio.size.*` 令牌", "`LhRadioSize`", "`Medium`" }),
                    }),
                    new DocTable("LhRadioSize", new[] { "值", "尺寸令牌" }, new[]
                    {
                        new DocRow(new[] { "`Small`", "`radio.size.small`（14epx）" }),
                        new DocRow(new[] { "`Medium`", "`radio.size.medium`（16epx）" }),
                        new DocRow(new[] { "`Large`", "`radio.size.large`（18epx）" }),
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
                        new DocRow(new[] { "`radio.size.{small,medium,large}`", "三档圆环尺寸" }),
                        new DocRow(new[] { "`radio.ringWidth`", "外环描边宽度" }),
                        new DocRow(new[] { "`radio.dotScale`", "内圆点直径比例（尺寸 × dotScale）" }),
                        new DocRow(new[] { "`radio.gap`", "圆环与文字间距" }),
                        new DocRow(new[] { "`color.primary.default`", "选中态外环与圆点色" }),
                        new DocRow(new[] { "`color.border.base`", "未选中外环色" }),
                        new DocRow(new[] { "`color.disabled.{border,text}`", "禁用态外环/圆点" }),
                        new DocRow(new[] { "`color.text.{primary,disabled}`", "标签文字色" }),
                        new DocRow(new[] { "`typography.fontSize.base` / `fontFamily`", "标签字号与字体栈" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/radio.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhRadioTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`tap-unchecked-emits-onchange`", "未选中点击触发 onChange", "`Tap_Unchecked_Emits_OnChange`" }),
                        new DocRow(new[] { "`tap-checked-keeps-checked`", "已选中再点不触发", "`Tap_Checked_Keeps_Checked`" }),
                        new DocRow(new[] { "`disabled-blocks-change`", "禁用态不可交互", "`Disabled_Blocks_Change`" }),
                        new DocRow(new[] { "`checked-uses-primary-tokens`", "选中态消费主色令牌", "`Checked_Uses_Primary_Tokens`" }),
                        new DocRow(new[] { "`unchecked-uses-border-token`", "未选中态消费描边令牌", "`Unchecked_Uses_Border_Token`" }),
                        new DocRow(new[] { "`small-size-uses-small-token`", "小号消费 small 尺寸令牌", "`Small_Size_Uses_Small_Token`" }),
                    })
                }),
        });
}
