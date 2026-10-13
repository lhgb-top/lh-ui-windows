# 本地联调

## 家族布局约定

```
lh-ui/                        # 家族父目录（非仓库）
├── lh-ui-core/               # 令牌真源 + codegen
├── lh-ui-android/            # 兄弟仓
├── lh-ui-ios/
├── lh-ui-harmony/
└── lh-ui-windows/            # 本仓
```

各仓同父目录时，令牌**本地直连**；兄弟仓不存在（CI 单仓 clone）时**自动回退 npm 包**。
这与 web 血脉的 vite alias 守卫机制同构（总纲 D18）。

## 三级守卫

`scripts/link-tokens.mjs` 按优先级查找令牌产物：

| 优先级 | 来源 | 说明 |
| --- | --- | --- |
| 1 | `../lh-ui-core/dist/windows` | 本地直连：改令牌即时生效 |
| 2 | `node_modules/@lh-ui/core/dist/windows` | npm 回退：`@lh-ui/core` 的 `files:["dist"]` 含各端产物 |
| 3 | — | 报错退出，缺令牌无法构建 |

令牌落点是 `src/Tokens/`（codegen 的 `relativePath` 为 `Tokens/*.cs`），组件侧
`using Top.Lhgb.Ui.Tokens;` 即指向此处。该目录是生成物（`.gitignore`），禁止手改。

## 日常流程

```bash
# 1. 改令牌（在 core 仓）
cd ../lh-ui-core
vim tokens/color.json           # 或 spacing / typography / ...

# 2. 重新生成 + 校验
pnpm codegen                    # 生成 dist/ 各端产物
pnpm verify                     # schema + 引用 + WCAG 对比度 + 契约一致性

# 3. 回本仓同步
cd ../lh-ui-windows
pnpm link:tokens                # 覆盖 src/Tokens/

# 4. 回归（dotnet）
dotnet test                     # xUnit 黄金用例
dotnet build                    # 编译即类型检查
```

## 文档站令牌

文档站的模拟器 CSS 变量也由令牌生成：

```bash
pnpm gen:sim                    # 生成 docs/.vitepress/theme/sim-tokens.css
pnpm docs:dev                   # 内部已先跑 gen:sim，端口 5186
pnpm sim:check                  # 校验 sim CSS 里没有令牌字面量（红线）
```

## CI 门禁

```bash
pnpm link:tokens:check          # 检测令牌是否已同步（文件数比对，未同步则退出码 1）
```

## 禁止事项

- ❌ 手改 `src/Tokens/`（生成物，gitignore）
- ❌ 把生成的令牌文件提交入库
- ❌ 在组件里写色值/间距字面量（如 `Color.FromArgb(0xFF,0x2B,0x4C,0x7C)`、`new GridLength(40)`、字号数字）
- ❌ 绕过 `link:tokens` 手动拷贝产物

## 排查

| 症状 | 原因 | 解决 |
| --- | --- | --- |
| 编译报 `The type or namespace 'LhTokens' does not exist` | 令牌未同步 | `pnpm link:tokens` |
| 令牌值与预期不符 | core 仓未重新 codegen | core 仓 `pnpm codegen` 后重新 link |
| `link:tokens` 报「未找到令牌产物」 | 兄弟仓不在且未装 npm 包 | 检查家族布局或 `pnpm i -D @lh-ui/core` |
| 暗色颜色不对 | 改了明色主色但没更新暗色预计算 | 更新 `color-dark.json` 并重跑（R-n6） |
| 组件颜色全是明色 | 入口未调 `LhTheme.Apply` | App / 主窗口启动时调用一次 |
| `dotnet build` 报 `MC1000` 等 XAML 错误 | 组件均为代码构建视觉树，仓内无 XAML | 检查是否误加了 `.xaml` 文件 |
