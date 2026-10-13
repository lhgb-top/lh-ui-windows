// 本文件由 scripts/gen-doc-pages.mjs 从 docs/guide/local-linking.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/Previews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class GuideLocalLinkingDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "local-linking",
        "本地联调",
        Array.Empty<string>(),
        Array.Empty<string>(),
        DocGroup.Guide,
        new[]
        {
            new DocSection(
                "家族布局约定",
                new[]
                {
                    "各仓同父目录时，令牌**本地直连**；兄弟仓不存在（CI 单仓 clone）时**自动回退 npm 包**。",
                    "这与 web 血脉的 vite alias 守卫机制同构（总纲 D18）。"
                },
                null,
                new[]
                {
                    "lh-ui/                        # 家族父目录（非仓库）",
                    "├── lh-ui-core/               # 令牌真源 + codegen",
                    "├── lh-ui-android/            # 兄弟仓",
                    "├── lh-ui-ios/",
                    "├── lh-ui-harmony/",
                    "└── lh-ui-windows/            # 本仓"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "三级守卫",
                new[]
                {
                    "`scripts/link-tokens.mjs` 按优先级查找令牌产物：",
                    "令牌落点是 `src/Tokens/`（codegen 的 `relativePath` 为 `Tokens/*.cs`），组件侧",
                    "`using Top.Lhgb.Ui.Tokens;` 即指向此处。该目录是生成物（`.gitignore`），禁止手改。"
                },
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "优先级", "来源", "说明" }, new[]
                    {
                        new DocRow(new[] { "1", "`../lh-ui-core/dist/windows`", "本地直连：改令牌即时生效" }),
                        new DocRow(new[] { "2", "`node_modules/@lh-ui/core/dist/windows`", "npm 回退：`@lh-ui/core` 的 `files:[\"dist\"]` 含各端产物" }),
                        new DocRow(new[] { "3", "—", "报错退出，缺令牌无法构建" }),
                    })
                }),
            new DocSection(
                "日常流程",
                Array.Empty<string>(),
                null,
                new[]
                {
                    "# 1. 改令牌（在 core 仓）",
                    "cd ../lh-ui-core",
                    "vim tokens/color.json           # 或 spacing / typography / ...",
                    "",
                    "# 2. 重新生成 + 校验",
                    "pnpm codegen                    # 生成 dist/ 各端产物",
                    "pnpm verify                     # schema + 引用 + WCAG 对比度 + 契约一致性",
                    "",
                    "# 3. 回本仓同步",
                    "cd ../lh-ui-windows",
                    "pnpm link:tokens                # 覆盖 src/Tokens/",
                    "",
                    "# 4. 回归（dotnet）",
                    "dotnet test                     # xUnit 黄金用例",
                    "dotnet build                    # 编译即类型检查"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "文档站令牌",
                new[]
                {
                    "文档站的模拟器 CSS 变量也由令牌生成："
                },
                null,
                new[]
                {
                    "pnpm gen:sim                    # 生成 docs/.vitepress/theme/sim-tokens.css",
                    "pnpm docs:dev                   # 内部已先跑 gen:sim，端口 5186",
                    "pnpm sim:check                  # 校验 sim CSS 里没有令牌字面量（红线）"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "CI 门禁",
                Array.Empty<string>(),
                null,
                new[]
                {
                    "pnpm link:tokens:check          # 检测令牌是否已同步（文件数比对，未同步则退出码 1）"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "禁止事项",
                new[]
                {
                    "- ❌ 手改 `src/Tokens/`（生成物，gitignore）",
                    "- ❌ 把生成的令牌文件提交入库",
                    "- ❌ 在组件里写色值/间距字面量（如 `Color.FromArgb(0xFF,0x2B,0x4C,0x7C)`、`new GridLength(40)`、字号数字）",
                    "- ❌ 绕过 `link:tokens` 手动拷贝产物"
                },
                null,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
            new DocSection(
                "排查",
                Array.Empty<string>(),
                null,
                Array.Empty<string>(),
                new[]
                {
                    new DocTable("", new[] { "症状", "原因", "解决" }, new[]
                    {
                        new DocRow(new[] { "编译报 `The type or namespace 'LhTokens' does not exist`", "令牌未同步", "`pnpm link:tokens`" }),
                        new DocRow(new[] { "令牌值与预期不符", "core 仓未重新 codegen", "core 仓 `pnpm codegen` 后重新 link" }),
                        new DocRow(new[] { "`link:tokens` 报「未找到令牌产物」", "兄弟仓不在且未装 npm 包", "检查家族布局或 `pnpm i -D @lh-ui/core`" }),
                        new DocRow(new[] { "暗色颜色不对", "改了明色主色但没更新暗色预计算", "更新 `color-dark.json` 并重跑（R-n6）" }),
                        new DocRow(new[] { "组件颜色全是明色", "入口未调 `LhTheme.Apply`", "App / 主窗口启动时调用一次" }),
                        new DocRow(new[] { "`dotnet build` 报 `MC1000` 等 XAML 错误", "组件均为代码构建视觉树，仓内无 XAML", "检查是否误加了 `.xaml` 文件" }),
                    })
                }),
        });
}
