---
layout: home

hero:
  name: lh-ui-windows
  text: ネイティブ血脈 · Windows 端
  tagline: C# + WinUI 3 コンポーネントライブラリ（.NET 8 / Windows App SDK）、トークン駆動・契約アラインメント・ゴールデンケースによる保護
  actions:
    - theme: brand
      text: クイックスタート
      link: /ja/guide/quickstart
    - theme: alt
      text: コンポーネント一覧
      link: /ja/components/button

features:
  - title: トークン駆動
    details: コンポーネント内に色や間隔のハードコードは一切なく、すべて core の codegen が生成した LhTokens / LhDarkTokens を消費します。トークンを変更すれば全端に即時反映されます。
  - title: 契約の一致
    details: プロパティの命名が lh-ui-core/specs と文字通り一致し、Android / iOS / HarmonyOS 端および Web 血脈と振る舞いを共有します。
  - title: ゴールデンケースによる保護
    details: cases/*.json の振る舞いアサーションを xUnit テストに翻訳し、dotnet test 一行でリグレッションできます。
  - title: ライト / ダーク両テーマ
    details: LhTheme.Apply 一行で切り替え（静的パレット + ThemeChanged イベントによるホットリフレッシュ）。ダークトークンは core が事前計算し、ランタイムの調色はありません。
  - title: 中華風デザイン言語
    details: 青花瓷ブルーのメインカラー、宣紙ホワイトの背景、藤黄と朱砂のアクセント、セリフフォントスタック。Web 血脈と同じソース・同じ値です。
  - title: WinUI 3 ネイティブ
    details: 純 C# + WinUI 3 で手書きした UI。JS Web ランタイムはなく、Fluent デザイン体系の一級市民です。
---
