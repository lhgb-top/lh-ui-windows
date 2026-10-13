// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/tag.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/TagPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class TagDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "tag",
        "Tag 标签",
        new[]
        {
                "用于标记和选择的标签。支持六种语义类型、三档尺寸、描边态、圆角态与可关闭态。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/tag.json` ·",
                "黄金用例：`lh-ui-core/cases/tag.json`（6 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Basic,
        new[]
        {
            new DocSection(
                "类型",
                new[]
                {
                    "六种类型决定底色、文字色与描边色。"
                },
                TagPreviews.Types,
                new[]
                {
                    "var tag = new LhTag",
                    "{",
                    "    Text = \"主要\",",
                    "    Type = LhTagType.Primary,",
                    "};"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "描边与圆角",
                new[]
                {
                    "`Plain` 为 true 时切换为语义浅底 + 语义文字 + 语义描边；`Round` 为 true 时",
                    "圆角取高度的一半（胶囊态，与 `tag.borderRadius` 令牌并列）。"
                },
                TagPreviews.BorderAndRadius,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "尺寸与关闭",
                new[]
                {
                    "三档尺寸映射到 `tag.height` / `paddingHorizontal` / `fontSize` 令牌；`Closable`",
                    "为 true 时在右侧绘制关闭符，点击触发 `Close` 事件。"
                },
                TagPreviews.SizesAndClose,
                new[]
                {
                    "var tag = new LhTag",
                    "{",
                    "    Text = \"可关\",",
                    "    Type = LhTagType.Primary,",
                    "    Closable = true,",
                    "};",
                    "tag.Close += () => { /* 点击关闭符 */ };"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "明暗主题",
                new[]
                {
                    "切换站点右上角主题，模拟器手机框内的标签颜色即变；真机通过 `LhTheme.Apply(true)` 切换。"
                },
                TagPreviews.Theme,
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
                        new DocRow(new[] { "`Close`", "点击关闭符时触发（需 `Closable` 为 true）", "`event Action`", "—" }),
                        new DocRow(new[] { "`Text`", "标签文字内容（spec 的 default 插槽）", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`Type`", "语义类型，决定底色、文字色与描边色", "`LhTagType`", "`Default`" }),
                        new DocRow(new[] { "`Size`", "标签尺寸，映射到尺寸令牌", "`LhTagSize`", "`Medium`" }),
                        new DocRow(new[] { "`Plain`", "是否为浅底描边形态", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Round`", "是否为胶囊形态（圆角取高度一半）", "`bool`", "`false`" }),
                        new DocRow(new[] { "`Closable`", "是否显示关闭符", "`bool`", "`false`" }),
                    }),
                    new DocTable("LhTagType", new[] { "值", "实心底", "浅底（plain）", "文字色" }, new[]
                    {
                        new DocRow(new[] { "`Default`", "`color.background.default`", "同左", "`color.text.primary`" }),
                        new DocRow(new[] { "`Primary`", "`color.primary.default`", "`color.primary.bg`", "`color.onPrimary` / `color.primary.default`" }),
                        new DocRow(new[] { "`Success`", "`color.success.default`", "`color.success.bg`", "`color.onPrimary` / `color.success.default`" }),
                        new DocRow(new[] { "`Warning`", "`color.warning.default`", "`color.warning.bg`", "`color.onPrimary` / `color.warning.default`" }),
                        new DocRow(new[] { "`Danger`", "`color.danger.default`", "`color.danger.bg`", "`color.onPrimary` / `color.danger.default`" }),
                        new DocRow(new[] { "`Info`", "`color.info.default`", "`color.info.bg`", "`color.onPrimary` / `color.info.default`" }),
                    }),
                    new DocTable("LhTagSize", new[] { "值", "height", "paddingHorizontal", "fontSize" }, new[]
                    {
                        new DocRow(new[] { "`Small`", "`tag.height.small`（20epx）", "`tag.paddingHorizontal.small`（8epx）", "`tag.fontSize.small`（11）" }),
                        new DocRow(new[] { "`Medium`", "`tag.height.medium`（24epx）", "`tag.paddingHorizontal.medium`（12epx）", "`tag.fontSize.medium`（12）" }),
                        new DocRow(new[] { "`Large`", "`tag.height.large`（28epx）", "`tag.paddingHorizontal.large`（16epx）", "`tag.fontSize.large`（13）" }),
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
                        new DocRow(new[] { "`tag.height.{small,medium,large}`", "三档高度" }),
                        new DocRow(new[] { "`tag.paddingHorizontal.{small,medium,large}`", "三档横向内边距" }),
                        new DocRow(new[] { "`tag.fontSize.{small,medium,large}`", "三档字号" }),
                        new DocRow(new[] { "`tag.borderRadius` / `tag.borderWidth`", "圆角与描边宽度" }),
                        new DocRow(new[] { "`color.primary.default` / `color.onPrimary`", "主色类型底/文字" }),
                        new DocRow(new[] { "`color.{success,warning,danger,info}.{default,bg}`", "语义类型实心底/浅底" }),
                        new DocRow(new[] { "`color.background.default` / `color.text.primary` / `color.border.base`", "默认类型三色" }),
                        new DocRow(new[] { "`spacing.xs`", "关闭符与文字间距" }),
                        new DocRow(new[] { "`typography.fontFamily`", "国风衬线字体栈" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/tag.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhTagTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`default-type-renders-text`", "默认类型中性底/文字/描边", "`Default_Type_Renders_Text`" }),
                        new DocRow(new[] { "`primary-type-uses-primary-tokens`", "主色类型消费主色令牌", "`Primary_Type_Uses_Primary_Tokens`" }),
                        new DocRow(new[] { "`plain-uses-bg-tokens`", "浅底形态消费语义浅底令牌", "`Plain_Uses_Bg_Tokens`" }),
                        new DocRow(new[] { "`small-size-uses-small-tokens`", "小号消费 small 尺寸令牌", "`Small_Size_Uses_Small_Tokens`" }),
                        new DocRow(new[] { "`round-makes-radius-half-height`", "胶囊态圆角为高度一半", "`Round_Makes_Radius_Half_Height`" }),
                        new DocRow(new[] { "`closable-tap-emits-onclose`", "点击关闭符触发 onClose", "`Closable_Tap_Emits_OnClose`" }),
                    })
                }),
        });
}
