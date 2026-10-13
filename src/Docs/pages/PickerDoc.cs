// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/picker.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/PickerPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class PickerDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "picker",
        "Picker 选择器",
        new[]
        {
                "底部弹出的单列滚轮。支持候选项、索引钳制与工具栏。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/picker.json` ·",
                "黄金用例：`lh-ui-core/cases/picker.json`（8 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Form,
        new[]
        {
            new DocSection(
                "基础用法",
                new[]
                {
                    "声明式可见性：`Visible` 为 false 时不渲染任何内容；确认/取消后触发 `Close`，",
                    "宿主据此置 `Visible` 为 false。"
                },
                PickerPreviews.Basic,
                new[]
                {
                    "var picker = new LhPicker",
                    "{",
                    "    Options = new[] { \"青花瓷\", \"粉彩瓷\", \"玲珑瓷\" },",
                    "    Value = 1,",
                    "    Title = \"选择\",",
                    "};",
                    "picker.ValueChange += index => { /* 滚轮停止后的选中索引 */ };",
                    "picker.Confirm += index => { /* 当前选中索引 */ };",
                    "picker.Close += () => picker.Visible = false;",
                    "",
                    "picker.Visible = true;"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "索引钳制",
                new[]
                {
                    "`Value` 越界时钳制到候选项范围内：负数取 0，大于候选项数 - 1 取最后一项。"
                },
                PickerPreviews.IndexClamp,
                new[]
                {
                    "Assert.Equal(0, LhPickerTokens.ClampIndex(-1, 3));   // 负数取 0",
                    "Assert.Equal(2, LhPickerTokens.ClampIndex(100, 3));  // 越界取最后一项"
                },
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
                        new DocRow(new[] { "`ValueChange`", "滚轮停止且选中项变化时触发", "`event Action<int>`", "—" }),
                        new DocRow(new[] { "`Confirm`", "点击确认按钮时触发，携带当前选中索引", "`event Action<int>`", "—" }),
                        new DocRow(new[] { "`Cancel`", "点击取消按钮时触发", "`event Action`", "—" }),
                        new DocRow(new[] { "`Close`", "确认/取消后触发，宿主据此置 Visible 为 false", "`event Action`", "—" }),
                        new DocRow(new[] { "`Visible`", "是否显示；为 false 时不渲染任何内容", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Options`", "候选项文本数组（单列）", "`string[]?`", "`null`" }),
                        new DocRow(new[] { "`Value`", "选中项索引（0 基）；越界时钳制到候选项范围内", "`int`", "`0`" }),
                        new DocRow(new[] { "`Title`", "工具栏中间的标题", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`ConfirmText`", "确认按钮文字", "`string`", "`\"确认\"`" }),
                        new DocRow(new[] { "`CancelText`", "取消按钮文字", "`string`", "`\"取消\"`" }),
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
                        new DocRow(new[] { "`picker.height`", "面板高度" }),
                        new DocRow(new[] { "`picker.toolbarHeight`", "顶部工具栏高度（滚轮区高度 = 面板高度 - 工具栏高度）" }),
                        new DocRow(new[] { "`picker.itemHeight`", "候选项高度（中央指示器间距同此）" }),
                        new DocRow(new[] { "`picker.itemFontSize`", "候选项与工具栏字号" }),
                        new DocRow(new[] { "`picker.borderRadius`", "面板顶部圆角" }),
                        new DocRow(new[] { "`color.background.default`", "面板底色" }),
                        new DocRow(new[] { "`color.background.overlay`", "遮罩色" }),
                        new DocRow(new[] { "`color.text.primary`", "标题与候选项色" }),
                        new DocRow(new[] { "`color.text.secondary`", "取消按钮色" }),
                        new DocRow(new[] { "`color.primary.default`", "确认按钮色" }),
                        new DocRow(new[] { "`color.border.lighter`", "中央指示器线色" }),
                        new DocRow(new[] { "`zIndex.modal` / `zIndex.modalBackdrop`", "面板/遮罩层级" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/picker.json` 的 8 条断言与 xUnit 测试的对应关系（`tests/LhPickerTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`confirm-emits-onconfirm-with-value`", "确认携带当前选中索引", "`Confirm_Emits_OnConfirm_With_Value`" }),
                        new DocRow(new[] { "`confirm-also-emits-onclose`", "确认同时触发 onClose", "`Confirm_Also_Emits_OnClose`" }),
                        new DocRow(new[] { "`cancel-emits-oncancel`", "取消触发 onCancel", "`Cancel_Emits_OnCancel`" }),
                        new DocRow(new[] { "`scroll-emits-onvaluechange`", "滚轮停止触发 onValueChange", "`Scroll_Emits_OnValueChange`" }),
                        new DocRow(new[] { "`clamp-negative-value-to-zero`", "负数索引钳制到 0", "`Clamp_Negative_Value_To_Zero`" }),
                        new DocRow(new[] { "`clamp-overflow-value-to-last`", "越界索引钳制到最后一项", "`Clamp_Overflow_Value_To_Last`" }),
                        new DocRow(new[] { "`title-renders-text`", "标题按字号令牌渲染", "`Title_Renders_Text`" }),
                        new DocRow(new[] { "`hidden-not-rendered`", "不可见时不渲染任何内容", "`Hidden_Not_Rendered`" }),
                    })
                }),
        });
}
