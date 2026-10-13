---
'@lh-ui/windows': minor
---

新增原生文档模块（D21）：类库内 `src/Docs/`（纯 C# 代码后端、零 .xaml）+ 极薄 WinExe `docsapp/`，照 Android `:docsui` 范式落地四支柱——令牌先行（外观全取 `LhTokens.Docs.*`，经 DocPalette 按明暗解析，零字面量）、内容与预览分离（正文由 `pnpm gen:docs` 从 md 生成、禁手改；真交互预览手写在 `previews/`）、宿主零依赖（手写返回栈 + LhNavbar dogfood + 全局 LhToast + 暗色热重绘）、双形态分发（`LhDocs.Show()` 开窗口 / 嵌入任意窗口）。

同时修复构建链：tests 工程补 `CopyLocalLockFileAssemblies`（testhost 依赖落盘），库与 demo/docsapp 的 PRI 生成链在缺 VS 工作负载时由 `AppxGeneratePriEnabled=false` 显式关闭；修正 `gen-doc-pages.mjs` 的 C# 字符串转义（`\"` 而非 verbatim 式 `""`）；AGENTS.md 色值 gotcha 澄清（结构体是 `Windows.UI.Color`，`Microsoft.UI.Colors` 静态表才是红线 4b 禁用的那个）。
