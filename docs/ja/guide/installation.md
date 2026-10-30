# インストール

## 環境要件

- .NET 8 SDK（`net8.0-windows10.0.19041.0` ターゲットフレームワーク）
- Windows App SDK 1.6+（Windows 10 1809+ または Windows 11）
- ビルド時ツールチェーン：Node.js >= 20 + pnpm（トークンの接続とドキュメントサイトでのみ使用、**ランタイムには含まれない**）

## NuGet での導入（nM6 のパッケージ公開パイプライン開通後）

```bash
dotnet add package LhUi.Windows
```

```xml
<ItemGroup>
  <PackageReference Include="LhUi.Windows" Version="0.0.1" />
</ItemGroup>
```

## ソースコード統合（現段階）

ファミリーレイアウトでは本リポジトリのクラスライブラリを直接参照します（ソリューションは `src/LhUi.csproj` クラスライブラリ + `tests/` テスト + `demo/` デモアプリで構成されます）：

```
lh-ui-windows/
├── src/                  # LhUi クラスライブラリ（Components/ + Common/ + Tokens/）
├── tests/                # xUnit ゴールデンケース
├── demo/                 # デモアプリ（全形態の展示 + 明暗の切り替え）
└── docs/                 # VitePress ドキュメントサイト
```

## トークンの同期

トークンは生成物であり、`lh-ui-core` から同期しなければなりません（唯一の真源であり、手書きでの変更は禁止）：

```bash
cd ../lh-ui-core && pnpm codegen   # dist/windows のトークン成果物を生成
cd ../lh-ui-windows
pnpm install                        # ビルド時ツールチェーンをインストール
pnpm link:tokens                    # トークンを src/Tokens/ に同期
```

`pnpm link:tokens` は三段階の存在性ガード付きです：ローカルの `../lh-ui-core/dist/windows` → npm の `@lh-ui/core` 成果物 → エラー。

## インストールの検証

```bash
dotnet build     # LhUi / LhUi.Tests / LhUi.Demo をコンパイル（C# の型チェックを含む）
dotnet test      # xUnit ゴールデンケース（15 件、Resolve 純関数の外観解析をアサーション）
```
