// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/switch.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/SwitchPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class SwitchDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "switch",
        "Switch 开关",
        new[]
        {
                "开关。胶囊轨道 + 圆形滑块，点击切换；支持禁用与加载中。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/switch.json` ·",
                "黄金用例：`lh-ui-core/cases/switch.json`（6 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Form,
        new[]
        {
            new DocSection(
                "基础用法",
                Array.Empty<string>(),
                SwitchPreviews.Basic,
                new[]
                {
                    "var sw = new LhSwitch { Checked = true };",
                    "sw.Change += isChecked => { };"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "尺寸",
                new[]
                {
                    "三档尺寸映射到 `switch.width` / `switch.height` 令牌；滑块直径 = 高度 - 2 × `switch.gap`。"
                },
                SwitchPreviews.Sizes,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "状态",
                new[]
                {
                    "`Disabled` 与 `Loading` 都不响应点击；`Loading` 时滑块位置改画主色加载环。"
                },
                SwitchPreviews.States,
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
                        new DocRow(new[] { "`Change`", "开关态变化回调；disabled 或 loading 时不触发", "`event Action<bool>`", "—" }),
                        new DocRow(new[] { "`Checked`", "是否开启", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Disabled`", "是否禁用（不响应点击且呈现禁用样式）", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Loading`", "是否加载中（滑块位置显示加载环且不响应点击）", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Size`", "开关尺寸，映射到 `switch.width` / `switch.height` 令牌", "`LhSwitchSize`", "`Medium`" }),
                    }),
                    new DocTable("LhSwitchSize", new[] { "值", "width", "height" }, new[]
                    {
                        new DocRow(new[] { "`Small`", "`switch.width.small`（40epx）", "`switch.height.small`（22epx）" }),
                        new DocRow(new[] { "`Medium`", "`switch.width.medium`（48epx）", "`switch.height.medium`（26epx）" }),
                        new DocRow(new[] { "`Large`", "`switch.width.large`（56epx）", "`switch.height.large`（30epx）" }),
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
                        new DocRow(new[] { "`switch.width.{small,medium,large}`", "三档轨道宽度" }),
                        new DocRow(new[] { "`switch.height.{small,medium,large}`", "三档轨道高度" }),
                        new DocRow(new[] { "`switch.gap`", "滑块与轨道留白（滑块直径 = 高度 - 2 × gap）" }),
                        new DocRow(new[] { "`color.primary.default`", "开启轨道色" }),
                        new DocRow(new[] { "`color.border.base`", "关闭轨道色" }),
                        new DocRow(new[] { "`color.onPrimary`", "滑块色" }),
                        new DocRow(new[] { "`color.disabled.bg` / `color.text.disabled`", "禁用轨道/滑块色" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/switch.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhSwitchTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`tap-toggles-and-emits-onchange`", "点击切换并触发 onChange", "`Tap_Toggles_And_Emits_OnChange`" }),
                        new DocRow(new[] { "`tap-checked-turns-off`", "开启态点击关闭", "`Tap_Checked_Turns_Off`" }),
                        new DocRow(new[] { "`disabled-blocks-change`", "禁用态不可交互", "`Disabled_Blocks_Change`" }),
                        new DocRow(new[] { "`loading-blocks-change`", "加载态不可交互", "`Loading_Blocks_Change`" }),
                        new DocRow(new[] { "`checked-uses-primary-token`", "开启态消费主色令牌", "`Checked_Uses_Primary_Token`" }),
                        new DocRow(new[] { "`unchecked-uses-border-token`", "关闭态消费描边令牌", "`Unchecked_Uses_Border_Token`" }),
                    })
                }),
        });
}
