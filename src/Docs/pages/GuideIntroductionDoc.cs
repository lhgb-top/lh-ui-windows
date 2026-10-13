// 本文件由 scripts/gen-doc-pages.mjs 从 docs/guide/introduction.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/Previews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class GuideIntroductionDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "introduction",
        "介绍",
        new[]
        {
                "**lh-ui-windows** 是 lh-ui 家族**原生血脉**的 Windows 端实现：基于 **C# + WinUI 3**（.NET 8，Windows App SDK）的 UI 组件库。"
        },
        Array.Empty<string>(),
        DocGroup.Guide,
        new[]
        {
            new DocSection(
                "家族中的位置",
                new[]
                {
                    "- **构建期中心**：`lh-ui-core` 是原生血脉的令牌真源（`tokens/*.json`）、组件契约（`specs/`）、黄金用例（`cases/`）与 codegen 工具。它生成 C# 令牌骨架（`LhTokens` / `LhDarkTokens` / `LhShadow` / `LhEasing`），**不生成任何组件实现**。",
                    "- **本仓职责**：手写 WinUI 3 组件、主题接入（`LhTheme.Apply` + `ThemeChanged` 事件）、属性枚举与默认值绑定、把黄金用例翻译为 xUnit 测试。",
                    "- **与 web 血脉互相独立**：不依赖 `@lh-ui/share`，发版互不约束。视觉一致性靠《设计语言基线》人工对齐。",
                    "> **历史**：本仓于 2026-10-12 取代此前的 `lh-ui-c`（C/C++ 嵌入式端）——原生血脉的第四名成员由嵌入式端改为 Windows 桌面端（总纲 D10）。"
                },
                null,
                new[]
                {
                    "                        ┌── web 血脉：share（TS core + SCSS）→ plus / vue2 / react / uniapp",
                    "lh-ui 家族（同品牌）─────┤",
                    "                        └── 原生血脉：core（JSON 令牌 + 契约 + codegen）→ android / ios / harmony / windows",
                    "                                                                    ↑ 本仓"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "设计语言",
                new[]
                {
                    "国风主题：青花瓷蓝主色（`#2B4C7C`）、宣纸白底（`#FAF6F0`）、松花绿成功、藤黄警告、朱砂危险，衬线字体栈（Noto Serif SC → STSong → SimSun）。所有视觉值都是令牌，组件不出现任何字面量。视觉表达上以 Fluent 的几何节制收口（令牌驱动的圆角层级与细描边）。"
                },
                null,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "为什么是 WinUI 3",
                new[]
                {
                    "- Windows App SDK 的现代声明式 UI，与 Compose / SwiftUI / ArkUI 心智同构，跨端契约容易对齐",
                    "- 无 Web 运行时、无 HTML/CSS 桥接，原生性能",
                    "- NuGet 分发（nM6 打通后），.NET 生态一键集成"
                },
                null,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "当前状态",
                new[]
                {
                    "- **P0 全 15 组件已交付**（令牌消费 + resolve 纯函数外观解析 + WinUI 3 控件 + xUnit 黄金用例 + demo 演示应用 + 文档站）：button / icon / tag / cell / text / input / textarea / checkbox / radio / switch / picker / dialog / toast / loading / navbar"
                },
                null,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
        });
}
