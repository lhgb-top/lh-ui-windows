// 本文件由 scripts/gen-doc-pages.mjs 从 docs/guide/installation.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/Previews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class GuideInstallationDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "installation",
        "安装",
        Array.Empty<string>(),
        Array.Empty<string>(),
        DocGroup.Guide,
        new[]
        {
            new DocSection(
                "环境要求",
                new[]
                {
                    "- .NET 8 SDK（`net8.0-windows10.0.19041.0` 目标框架）",
                    "- Windows App SDK 1.6+（Windows 10 1809+ 或 Windows 11）",
                    "- 构建期工具链：Node.js >= 20 + pnpm（仅令牌连接与文档站使用，**不进入运行时**）"
                },
                null,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "NuGet 引入（nM6 发包链路打通后）",
                Array.Empty<string>(),
                null,
                new[]
                {
                    "dotnet add package LhUi.Windows",
                    "<ItemGroup>",
                    "  <PackageReference Include=\"LhUi.Windows\" Version=\"0.0.1\" />",
                    "</ItemGroup>"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "源码集成（当前阶段）",
                new[]
                {
                    "家族布局下直接引用本仓类库（解决方案含 `src/LhUi.csproj` 类库 + `tests/` 测试 + `demo/` 演示应用）："
                },
                null,
                new[]
                {
                    "lh-ui-windows/",
                    "├── src/                  # LhUi 类库（Components/ + Common/ + Tokens/）",
                    "├── tests/                # xUnit 黄金用例",
                    "├── demo/                 # 演示应用（全形态展示 + 明暗切换）",
                    "└── docs/                 # VitePress 文档站"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "同步令牌",
                new[]
                {
                    "令牌是生成物，必须从 `lh-ui-core` 同步（唯一真源，禁止手改）：",
                    "`pnpm link:tokens` 带三级存在性守卫：本地 `../lh-ui-core/dist/windows` → npm `@lh-ui/core` 产物 → 报错。"
                },
                null,
                new[]
                {
                    "cd ../lh-ui-core && pnpm codegen   # 生成 dist/windows 令牌产物",
                    "cd ../lh-ui-windows",
                    "pnpm install                        # 安装构建期工具链",
                    "pnpm link:tokens                    # 同步令牌到 src/Tokens/"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "验证安装",
                Array.Empty<string>(),
                null,
                new[]
                {
                    "dotnet build     # 编译 LhUi / LhUi.Tests / LhUi.Demo（含 C# 类型检查）",
                    "dotnet test      # xUnit 黄金用例（15 份，断言 Resolve 纯函数外观解析）"
                },
                Array.Empty<DocTable>()),
        });
}
