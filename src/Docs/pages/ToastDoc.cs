// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/toast.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/ToastPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class ToastDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "toast",
        "Toast 轻提示",
        new[]
        {
                "自持可见性与计时的墨色胶囊浮层。支持语义类型、自动隐藏与定位。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/toast.json` ·",
                "黄金用例：`lh-ui-core/cases/toast.json`（6 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Feedback,
        new[]
        {
            new DocSection(
                "基础用法",
                new[]
                {
                    "指令式接口（spec 的 show / hide / tick）在端侧落点为方法 + 内部计时：",
                    "`Show(text)` 使其可见并重置计时，`Hide()` 立即隐藏但不触发 `Close`。",
                    "计时由 `DispatcherTimer` 承载，到期自动隐藏并触发 `Close`。"
                },
                ToastPreviews.Basic,
                new[]
                {
                    "var controller = new LhToastController();",
                    "var toast = new LhToast",
                    "{",
                    "    Duration = 2000,",
                    "    Position = LhToastPosition.Center,",
                    "    Controller = controller,",
                    "};",
                    "toast.Close += () => { /* 计时到期自动隐藏后触发 */ };",
                    "",
                    "// 宿主须把 toast 挂在页面 Grid 的最后子节点（层级 zIndex.toast）",
                    "controller.Show(\"已保存\");"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "语义类型",
                new[]
                {
                    "`Type` 非 default 时在文字前绘制对应语义色圆点（直径取字号）。"
                },
                ToastPreviews.SemanticTypes,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "定位",
                new[]
                {
                    "`Position` 决定在可用区域内的定位；上下两端留 `spacing.xl` 边距（center 时无留白）。"
                },
                ToastPreviews.Position,
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
                        new DocRow(new[] { "`Close`", "计时到期自动隐藏时触发；Hide() 主动隐藏不触发", "`event Action`", "—" }),
                        new DocRow(new[] { "`Type`", "语义类型；非 default 时在文字前绘制对应语义色圆点", "`LhToastType`", "`Default`" }),
                        new DocRow(new[] { "`Duration`", "自动隐藏时长（毫秒）", "`int`", "`2000`" }),
                        new DocRow(new[] { "`Position`", "在可用区域内的定位", "`LhToastPosition`", "`Center`" }),
                        new DocRow(new[] { "`Controller`", "宿主调用句柄（可选；Loaded 后注册 Show/Hide）", "`LhToastController?`", "`null`" }),
                    }),
                    new DocTable("方法", new[] { "方法", "说明" }, new[]
                    {
                        new DocRow(new[] { "`Show(string text)`", "显示提示文字并重置自动隐藏计时" }),
                        new DocRow(new[] { "`Hide()`", "立即隐藏但不触发 Close" }),
                    }),
                    new DocTable("LhToastType", new[] { "值", "圆点色" }, new[]
                    {
                        new DocRow(new[] { "`Default`", "不绘制圆点" }),
                        new DocRow(new[] { "`Success`", "`color.success.default`" }),
                        new DocRow(new[] { "`Warning`", "`color.warning.default`" }),
                        new DocRow(new[] { "`Danger`", "`color.danger.default`" }),
                        new DocRow(new[] { "`Info`", "`color.info.default`" }),
                    }),
                    new DocTable("LhToastPosition", new[] { "值", "定位" }, new[]
                    {
                        new DocRow(new[] { "`Top`", "顶部（上端留 `spacing.xl` 边距）" }),
                        new DocRow(new[] { "`Center`", "居中（无留白）" }),
                        new DocRow(new[] { "`Bottom`", "底部（下端留 `spacing.xl` 边距）" }),
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
                        new DocRow(new[] { "`toast.fontSize`", "文字字号（亦为色点直径）" }),
                        new DocRow(new[] { "`toast.paddingHorizontal` / `paddingVertical`", "胶囊内边距" }),
                        new DocRow(new[] { "`toast.borderRadius`", "胶囊圆角" }),
                        new DocRow(new[] { "`toast.gap`", "色点与文字间距" }),
                        new DocRow(new[] { "`color.ink`", "胶囊底色" }),
                        new DocRow(new[] { "`color.onPrimary`", "文字色" }),
                        new DocRow(new[] { "`color.{success,warning,danger,info}.default`", "语义色点" }),
                        new DocRow(new[] { "`zIndex.toast`", "浮层层级" }),
                        new DocRow(new[] { "`spacing.xl`", "上下两端留白" }),
                        new DocRow(new[] { "`typography.fontFamily`", "国风衬线字体栈" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/toast.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhToastTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`show-makes-visible`", "show 后可见并渲染文字", "`Show_Makes_Visible`" }),
                        new DocRow(new[] { "`auto-hides-after-duration`", "计时到期自动隐藏并触发 onClose", "`Auto_Hides_After_Duration`" }),
                        new DocRow(new[] { "`tick-before-duration-keeps-visible`", "计时未到期保持可见", "`Tick_Before_Duration_Keeps_Visible`" }),
                        new DocRow(new[] { "`hide-is-silent`", "hide 主动隐藏不触发 onClose", "`Hide_Is_Silent`" }),
                        new DocRow(new[] { "`success-type-uses-success-token`", "success 类型消费语义色与墨色令牌", "`Success_Type_Uses_Success_Token`" }),
                        new DocRow(new[] { "`hidden-emits-no-commands`", "不可见时不发射任何绘制指令", "`Hidden_Emits_No_Commands`" }),
                    })
                }),
        });
}
