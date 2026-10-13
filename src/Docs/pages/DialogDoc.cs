// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/dialog.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/DialogPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class DialogDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "dialog",
        "Dialog 对话框",
        new[]
        {
                "模态对话框。遮罩 + 居中内容卡 + 确认/取消按钮，支持语义类型与遮罩关闭。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/dialog.json` ·",
                "黄金用例：`lh-ui-core/cases/dialog.json`（8 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Feedback,
        new[]
        {
            new DocSection(
                "基础用法",
                new[]
                {
                    "声明式可见性：`Visible` 为 false 时不渲染任何内容；确认/取消后触发 `Close`，",
                    "宿主据此置 `Visible` 为 false。"
                },
                DialogPreviews.Basic,
                new[]
                {
                    "var dialog = new LhDialog",
                    "{",
                    "    Title = \"删除确认\",",
                    "    Content = \"删除后不可恢复，确认删除这条记录吗？\",",
                    "    Type = LhDialogType.Danger,",
                    "    ConfirmText = \"确认\",",
                    "    CancelText = \"取消\",",
                    "};",
                    "dialog.Confirm += () => { /* 执行删除 */ };",
                    "dialog.Close += () => dialog.Visible = false;",
                    "",
                    "dialog.Visible = true;"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "语义类型",
                new[]
                {
                    "`Type` 决定确认按钮的类型（颜色）；取消按钮固定 default 类型。"
                },
                DialogPreviews.SemanticTypes,
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
                        new DocRow(new[] { "`Confirm`", "确认时触发", "`event Action`", "—" }),
                        new DocRow(new[] { "`Cancel`", "取消时触发", "`event Action`", "—" }),
                        new DocRow(new[] { "`Close`", "确认/取消后或（closable 时）点击遮罩触发，宿主据此置 Visible 为 false", "`event Action`", "—" }),
                        new DocRow(new[] { "`Visible`", "是否显示；为 false 时不渲染任何内容", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Title`", "标题", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Content`", "正文内容", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Type`", "语义类型，决定确认按钮的类型（颜色）", "`LhDialogType`", "`Default`" }),
                        new DocRow(new[] { "`ConfirmText`", "确认按钮文字；为空时不显示确认按钮", "`string`", "`\"确认\"`" }),
                        new DocRow(new[] { "`CancelText`", "取消按钮文字；为空时不显示取消按钮", "`string`", "`\"取消\"`" }),
                        new DocRow(new[] { "`Closable`", "点击遮罩区域是否触发 Close", "`bool`", "`false`" }),
                    }),
                    new DocTable("LhDialogType", new[] { "值", "确认按钮类型" }, new[]
                    {
                        new DocRow(new[] { "`Default`", "`LhButtonType.Default`" }),
                        new DocRow(new[] { "`Primary`", "`LhButtonType.Primary`" }),
                        new DocRow(new[] { "`Success`", "`LhButtonType.Success`" }),
                        new DocRow(new[] { "`Warning`", "`LhButtonType.Warning`" }),
                        new DocRow(new[] { "`Danger`", "`LhButtonType.Danger`" }),
                        new DocRow(new[] { "`Info`", "`LhButtonType.Info`" }),
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
                        new DocRow(new[] { "`dialog.width` / `dialog.borderRadius`", "内容卡宽度与圆角" }),
                        new DocRow(new[] { "`dialog.paddingHorizontal` / `paddingVertical`", "内容卡内边距" }),
                        new DocRow(new[] { "`dialog.titleFontSize` / `contentFontSize`", "标题与正文字号" }),
                        new DocRow(new[] { "`dialog.gap`", "标题/内容/按钮区间距" }),
                        new DocRow(new[] { "`color.background.default`", "内容卡底色" }),
                        new DocRow(new[] { "`color.background.overlay`", "遮罩色" }),
                        new DocRow(new[] { "`color.text.primary` / `color.text.regular`", "标题/正文色" }),
                        new DocRow(new[] { "`zIndex.modal` / `zIndex.modalBackdrop`", "内容卡/遮罩层级" }),
                        new DocRow(new[] { "`shadow.base`", "内容卡投影" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/dialog.json` 的 8 条断言与 xUnit 测试的对应关系（`tests/LhDialogTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`confirm-emits-onconfirm`", "确认触发 onConfirm", "`Confirm_Emits_OnConfirm`" }),
                        new DocRow(new[] { "`cancel-emits-oncancel`", "取消触发 onCancel", "`Cancel_Emits_OnCancel`" }),
                        new DocRow(new[] { "`confirm-also-emits-onclose`", "确认同时触发 onClose", "`Confirm_Also_Emits_OnClose`" }),
                        new DocRow(new[] { "`closable-overlay-emits-onclose`", "closable 时遮罩点击触发 onClose", "`Closable_Overlay_Emits_OnClose`" }),
                        new DocRow(new[] { "`not-closable-blocks-overlay-close`", "非 closable 时遮罩不关闭", "`Not_Closable_Blocks_Overlay_Close`" }),
                        new DocRow(new[] { "`hidden-not-rendered`", "不可见时不渲染任何内容", "`Hidden_Not_Rendered`" }),
                        new DocRow(new[] { "`title-renders-text`", "标题与正文按字号令牌渲染", "`Title_Renders_Text`" }),
                        new DocRow(new[] { "`danger-type-uses-danger-token`", "danger 类型消费危险色令牌", "`Danger_Type_Uses_Danger_Token`" }),
                    })
                }),
        });
}
