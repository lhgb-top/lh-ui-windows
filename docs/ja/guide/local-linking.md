# ローカル連携

## ファミリーレイアウトの慣習

```
lh-ui/                        # ファミリーの親ディレクトリ（リポジトリではない）
├── lh-ui-core/               # トークン真源 + codegen
├── lh-ui-android/            # 兄弟リポジトリ
├── lh-ui-ios/
├── lh-ui-harmony/
└── lh-ui-windows/            # 本リポジトリ
```

各リポジトリが同じ親ディレクトリにある場合、トークンは**ローカル直結**します。兄弟リポジトリが存在しない（CI での単一リポジトリ clone）場合は**自動的に npm パッケージにフォールバック**します。
これは Web 血脈の vite alias ガード機構と同構です（マスタープラン D18）。

## 三段階のガード

`scripts/link-tokens.mjs` が優先度順にトークン成果物を探します：

| 優先度 | ソース | 説明 |
| --- | --- | --- |
| 1 | `../lh-ui-core/dist/windows` | ローカル直結：トークン変更が即時反映 |
| 2 | `node_modules/@lh-ui/core/dist/windows` | npm フォールバック：`@lh-ui/core` の `files:["dist"]` が各端の成果物を含む |
| 3 | — | エラーで終了、トークンがなければビルド不可 |

トークンの落とし先は `src/Tokens/` です（codegen の `relativePath` が `Tokens/*.cs`）。コンポーネント側の
`using Top.Lhgb.Ui.Tokens;` はここを指します。このディレクトリは生成物（`.gitignore`）であり、手書きでの変更は禁止です。

## 日常のフロー

```bash
# 1. トークンを変更（core リポジトリで）
cd ../lh-ui-core
vim tokens/color.json           # または spacing / typography / ...

# 2. 再生成 + 検証
pnpm codegen                    # dist/ の各端成果物を生成
pnpm verify                     # schema + 参照 + WCAG コントラスト + 契約の一致性

# 3. 本リポジトリに同期
cd ../lh-ui-windows
pnpm link:tokens                # src/Tokens/ を上書き

# 4. リグレッション（dotnet）
dotnet test                     # xUnit ゴールデンケース
dotnet build                    # コンパイルがすなわち型チェック
```

## ドキュメントサイトのトークン

ドキュメントサイトのシミュレーター CSS 変数もトークンから生成します：

```bash
pnpm gen:sim                    # docs/.vitepress/theme/sim-tokens.css を生成
pnpm docs:dev                   # 内部で先に gen:sim を実行、ポート 5186
pnpm sim:check                  # sim CSS にトークンのリテラルがないことを検証（レッドライン）
```

## CI ゲート

```bash
pnpm link:tokens:check          # トークンが同期済みか検出（ファイル数の比較、未同期なら終了コード 1）
```

## 禁止事項

- ❌ `src/Tokens/` の手書き変更（生成物、gitignore 対象）
- ❌ 生成されたトークンファイルをリポジトリにコミット
- ❌ コンポーネント内に色や間隔のリテラルを書く（例：`Color.FromArgb(0xFF,0x2B,0x4C,0x7C)`、`new GridLength(40)`、フォントサイズの数値）
- ❌ `link:tokens` を迂回して成果物を手動でコピー

## トラブルシューティング

| 症状 | 原因 | 解決策 |
| --- | --- | --- |
| コンパイルが `The type or namespace 'LhTokens' does not exist` で失敗 | トークンが未同期 | `pnpm link:tokens` |
| トークン値が想定と違う | core リポジトリが codegen を再実行していない | core リポジトリで `pnpm codegen` 後に再度 link |
| `link:tokens` が「トークン成果物が見つからない」と報告 | 兄弟リポジトリが不在かつ npm パッケージ未インストール | ファミリーレイアウトを確認するか `pnpm i -D @lh-ui/core` |
| ダークの色がおかしい | 明色のメインカラーを変えたがダークの事前計算を更新していない | `color-dark.json` を更新して再実行（R-n6） |
| コンポーネントの色がすべて明色 | エントリで `LhTheme.Apply` を呼んでいない | App / メインウィンドウの起動時に一度呼び出す |
| `dotnet build` が `MC1000` などの XAML エラーで失敗 | コンポーネントはすべてコードでビジュアルツリーを構築し、リポジトリ内に XAML はない | 誤って `.xaml` ファイルを追加していないか確認 |
