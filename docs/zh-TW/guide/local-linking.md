# 本地聯調

## 家族布局約定

```
lh-ui/                        # 家族父目錄（非倉庫）
├── lh-ui-core/               # 令牌真源 + codegen
├── lh-ui-android/            # 兄弟倉
├── lh-ui-ios/
├── lh-ui-harmony/
└── lh-ui-windows/            # 本倉
```

各倉同父目錄時，令牌**本地直連**；兄弟倉不存在（CI 單倉 clone）時**自動回退 npm 套件**。
這與 web 血脈的 vite alias 守衛機制同構（總綱 D18）。

## 三級守衛

`scripts/link-tokens.mjs` 按優先級查找令牌產物：

| 優先級 | 來源 | 說明 |
| --- | --- | --- |
| 1 | `../lh-ui-core/dist/windows` | 本地直連：改令牌即時生效 |
| 2 | `node_modules/@lh-ui/core/dist/windows` | npm 回退：`@lh-ui/core` 的 `files:["dist"]` 含各端產物 |
| 3 | — | 報錯退出，缺令牌無法構建 |

令牌落點是 `src/Tokens/`（codegen 的 `relativePath` 為 `Tokens/*.cs`），元件側
`using Top.Lhgb.Ui.Tokens;` 即指向此處。該目錄是生成物（`.gitignore`），禁止手改。

## 日常流程

```bash
# 1. 改令牌（在 core 倉）
cd ../lh-ui-core
vim tokens/color.json           # 或 spacing / typography / ...

# 2. 重新生成 + 校驗
pnpm codegen                    # 生成 dist/ 各端產物
pnpm verify                     # schema + 引用 + WCAG 對比度 + 契約一致性

# 3. 回本倉同步
cd ../lh-ui-windows
pnpm link:tokens                # 覆蓋 src/Tokens/

# 4. 回歸（dotnet）
dotnet test                     # xUnit 黃金用例
dotnet build                    # 編譯即型別檢查
```

## 文件站令牌

文件站的模擬器 CSS 變數也由令牌生成：

```bash
pnpm gen:sim                    # 生成 docs/.vitepress/theme/sim-tokens.css
pnpm docs:dev                   # 內部已先跑 gen:sim，連接埠 5186
pnpm sim:check                  # 校驗 sim CSS 裡沒有令牌字面量（紅線）
```

## CI 門禁

```bash
pnpm link:tokens:check          # 偵測令牌是否已同步（檔案數比對，未同步則退出碼 1）
```

## 禁止事項

- ❌ 手改 `src/Tokens/`（生成物，gitignore）
- ❌ 把生成的令牌檔案提交入庫
- ❌ 在元件裡寫色值/間距字面量（如 `Color.FromArgb(0xFF,0x2B,0x4C,0x7C)`、`new GridLength(40)`、字級數字）
- ❌ 繞過 `link:tokens` 手動拷貝產物

## 排查

| 症狀 | 原因 | 解決 |
| --- | --- | --- |
| 編譯報 `The type or namespace 'LhTokens' does not exist` | 令牌未同步 | `pnpm link:tokens` |
| 令牌值與預期不符 | core 倉未重新 codegen | core 倉 `pnpm codegen` 後重新 link |
| `link:tokens` 報「未找到令牌產物」 | 兄弟倉不在且未裝 npm 套件 | 檢查家族布局或 `pnpm i -D @lh-ui/core` |
| 暗色顏色不對 | 改了明色主色但沒更新暗色預計算 | 更新 `color-dark.json` 並重跑（R-n6） |
| 元件顏色全是明色 | 入口未呼叫 `LhTheme.Apply` | App / 主視窗啟動時呼叫一次 |
| `dotnet build` 報 `MC1000` 等 XAML 錯誤 | 元件均為程式碼構建視覺樹，倉內無 XAML | 檢查是否誤加了 `.xaml` 檔案 |
