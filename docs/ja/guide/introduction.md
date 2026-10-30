# 紹介

**lh-ui-windows** は lh-ui ファミリーの**ネイティブ血脈**における Windows 端の実装です：**C# + WinUI 3**（.NET 8、Windows App SDK）による UI コンポーネントライブラリです。

## ファミリー内での位置づけ

```
                        ┌── Web 血脈：share（TS core + SCSS）→ plus / vue2 / react / uniapp
lh-ui ファミリー（同一ブランド）─────┤
                        └── ネイティブ血脈：core（JSON トークン + 契約 + codegen）→ android / ios / harmony / windows
                                                                    ↑ 本リポジトリ
```

- **ビルド時の中心**：`lh-ui-core` はネイティブ血脈のトークン真源（`tokens/*.json`）、コンポーネント契約（`specs/`）、ゴールデンケース（`cases/`）、codegen ツールです。C# のトークンスケルトン（`LhTokens` / `LhDarkTokens` / `LhShadow` / `LhEasing`）を生成しますが、**コンポーネント実装は一切生成しません**。
- **本リポジトリの役割**：WinUI 3 コンポーネントの手書き、テーマの接続（`LhTheme.Apply` + `ThemeChanged` イベント）、プロパティの列挙型とデフォルト値のバインディング、ゴールデンケースの xUnit テストへの翻訳。
- **Web 血脈とは互いに独立**：`@lh-ui/share` に依存せず、リリース順序の制約もありません。視覚的な一致性は『デザイン言語ベースライン』への手作業の合わせこみで保ちます。

> **歴史**：本リポジトリは 2026-10-12 にそれまでの `lh-ui-c`（C/C++ 組み込み端）を置き換えました——ネイティブ血脈の 4 番目のメンバーは組み込み端から Windows デスクトップ端に変更されました（マスタープラン D10）。

## デザイン言語

中華風テーマ：青花瓷ブルーのメインカラー（`#2B4C7C`）、宣紙ホワイトの背景（`#FAF6F0`）、成功は松花绿、警告は藤黄、危険は朱砂、セリフフォントスタック（Noto Serif SC → STSong → SimSun）。すべての視覚値がトークンであり、コンポーネントの中にリテラルは一切現れません。視覚表現は Fluent の幾何的な節度で締めくくります（トークン駆動の角丸階層と細い枠線）。

## なぜ WinUI 3 か

- Windows App SDK のモダンな宣言的 UI であり、Compose / SwiftUI / ArkUI とメンタルモデルが同形なので、跨端契約を合わせやすい
- Web ランタイムも HTML/CSS ブリッジもなく、ネイティブ性能
- NuGet 配布（nM6 開通後）で、.NET エコシステムへのワンクリック統合

## 現在の状態

- **P0 の全 15 コンポーネントを提供済み**（トークン消費 + resolve 純関数による外観解析 + WinUI 3 コントロール + xUnit ゴールデンケース + デモアプリ + ドキュメントサイト）：button / icon / tag / cell / text / input / textarea / checkbox / radio / switch / picker / dialog / toast / loading / navbar
