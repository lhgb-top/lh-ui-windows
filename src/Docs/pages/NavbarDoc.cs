// 本文件由 scripts/gen-doc-pages.mjs 从 docs/components/navbar.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/NavbarPreviews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class NavbarDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "navbar",
        "Navbar 导航栏",
        new[]
        {
                "页面顶部固定栏。左/中/右三段，支持文字与图标。"
        },
        new[]
        {
                "契约源：`lh-ui-core/specs/navbar.json` ·",
                "黄金用例：`lh-ui-core/cases/navbar.json`（6 条，已翻译为 xUnit 测试）"
        },
        DocGroup.Navigation,
        new[]
        {
            new DocSection(
                "基础用法",
                new[]
                {
                    "层级取 `zIndex.sticky`，宿主将其作为页面顶部固定栏（Grid 首行或 Canvas 顶部）。"
                },
                NavbarPreviews.Basic,
                new[]
                {
                    "var navbar = new LhNavbar",
                    "{",
                    "    Title = \"青花瓷\",",
                    "    LeftText = \"返回\",",
                    "    LeftIcon = \"back\",",
                    "    RightText = \"更多\",",
                    "};",
                    "navbar.LeftClick += () => { };",
                    "navbar.RightClick += () => { };"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "图标",
                new[]
                {
                    "`LeftIcon` / `RightIcon` 传入本地图标名（见 Icon 文档的字形表），与文字二者非空时都显示；",
                    "两侧无内容时不触发对应点击事件。"
                },
                NavbarPreviews.Icons,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "分隔线",
                new[]
                {
                    "`Bordered` 为 true 时底部绘制 `color.border.lighter` 分隔线；默认开启。"
                },
                NavbarPreviews.Divider,
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
                        new DocRow(new[] { "`LeftClick`", "点击左侧区域时触发（无左侧内容时不触发）", "`event Action`", "—" }),
                        new DocRow(new[] { "`RightClick`", "点击右侧区域时触发（无右侧内容时不触发）", "`event Action`", "—" }),
                        new DocRow(new[] { "`Title`", "中间标题", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`LeftText`", "左侧按钮文字；与 LeftIcon 二者非空时都显示", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`LeftIcon`", "左侧图标名（本地图标体系渲染）", "`string?`", "`null`" }),
                        new DocRow(new[] { "`RightText`", "右侧按钮文字；与 RightIcon 二者非空时都显示", "`string`", "`\"\"`" }),
                        new DocRow(new[] { "`RightIcon`", "右侧图标名（本地图标体系渲染）", "`string?`", "`null`" }),
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
                        new DocRow(new[] { "`navbar.height`", "高度" }),
                        new DocRow(new[] { "`navbar.paddingHorizontal`", "左右内边距" }),
                        new DocRow(new[] { "`navbar.titleFontSize`", "标题与两侧文字字号" }),
                        new DocRow(new[] { "`navbar.iconSize`", "两侧图标尺寸" }),
                        new DocRow(new[] { "`navbar.borderWidth`", "底部分隔线宽" }),
                        new DocRow(new[] { "`color.background.default`", "底色" }),
                        new DocRow(new[] { "`color.text.primary`", "标题与两侧文字色" }),
                        new DocRow(new[] { "`color.border.lighter`", "底部分隔线色" }),
                        new DocRow(new[] { "`zIndex.sticky`", "固定层级" }),
                        new DocRow(new[] { "`spacing.xs`", "图标与文字间距" }),
                        new DocRow(new[] { "`typography.fontFamily`", "国风衬线字体栈" }),
                    })
                }),
            new DocSection(
                "黄金用例对照",
                new[]
                {
                    "`cases/navbar.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhNavbarTests.cs`）：",
                    "另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。",
                    "运行：`cd lh-ui-windows && dotnet test`。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "case id", "断言", "本地测试" }, new[]
                    {
                        new DocRow(new[] { "`title-renders-text`", "标题按字号令牌渲染", "`Title_Renders_Text`" }),
                        new DocRow(new[] { "`leftclick-emits-onleftclick`", "点击左侧触发 onLeftClick", "`LeftClick_Emits_OnLeftClick`" }),
                        new DocRow(new[] { "`rightclick-emits-onrightclick`", "点击右侧触发 onRightClick", "`RightClick_Emits_OnRightClick`" }),
                        new DocRow(new[] { "`no-left-content-no-onleftclick`", "无左侧内容时不触发", "`No_Left_Content_No_OnLeftClick`" }),
                        new DocRow(new[] { "`bordered-uses-border-token`", "分隔线消费 border.lighter 令牌", "`Bordered_Uses_Border_Token`" }),
                        new DocRow(new[] { "`height-uses-navbar-height-token`", "高度消费 navbar.height 令牌", "`Height_Uses_Navbar_Height_Token`" }),
                    })
                }),
        });
}
