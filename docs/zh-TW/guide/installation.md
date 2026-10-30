# 安裝

## 環境要求

- .NET 8 SDK（`net8.0-windows10.0.19041.0` 目標框架）
- Windows App SDK 1.6+（Windows 10 1809+ 或 Windows 11）
- 構建期工具鏈：Node.js >= 20 + pnpm（僅令牌連接與文件站使用，**不進入執行時**）

## NuGet 引入（nM6 發包鏈路打通後）

```bash
dotnet add package LhUi.Windows
```

```xml
<ItemGroup>
  <PackageReference Include="LhUi.Windows" Version="0.0.1" />
</ItemGroup>
```

## 原始碼整合（當前階段）

家族布局下直接引用本倉類庫（解決方案含 `src/LhUi.csproj` 類庫 + `tests/` 測試 + `demo/` 演示應用程式）：

```
lh-ui-windows/
├── src/                  # LhUi 類庫（Components/ + Common/ + Tokens/）
├── tests/                # xUnit 黃金用例
├── demo/                 # 演示應用程式（全形態展示 + 明暗切換）
└── docs/                 # VitePress 文件站
```

## 同步令牌

令牌是生成物，必須從 `lh-ui-core` 同步（唯一真源，禁止手改）：

```bash
cd ../lh-ui-core && pnpm codegen   # 生成 dist/windows 令牌產物
cd ../lh-ui-windows
pnpm install                        # 安裝構建期工具鏈
pnpm link:tokens                    # 同步令牌到 src/Tokens/
```

`pnpm link:tokens` 帶三級存在性守衛：本地 `../lh-ui-core/dist/windows` → npm `@lh-ui/core` 產物 → 報錯。

## 驗證安裝

```bash
dotnet build     # 編譯 LhUi / LhUi.Tests / LhUi.Demo（含 C# 型別檢查）
dotnet test      # xUnit 黃金用例（15 份，斷言 Resolve 純函式外觀解析）
```
