# Navbar ナビゲーションバー

ページ上部に固定するバーです。左 / 中央 / 右の 3 区画で、テキストとアイコンをサポートします。

> 契約元：`lh-ui-core/specs/navbar.json` ·
> ゴールデンケース：`lh-ui-core/cases/navbar.json`（6 件、xUnit テストに翻訳済み）

## 基本的な使い方

レイヤーは `zIndex.sticky` を取り、ホストはこれをページ上部の固定バー（Grid の先頭行または Canvas の最上部）として使います。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">← 戻る</span>
      <span class="sim-navbar__title">青花瓷</span>
      <span class="sim-navbar__right">その他</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var navbar = new LhNavbar
{
    Title = "青花瓷",
    LeftText = "戻る",
    LeftIcon = "back",
    RightText = "その他",
};
navbar.LeftClick += () => { };
navbar.RightClick += () => { };
```

## アイコン

`LeftIcon` / `RightIcon` にはローカルのアイコン名を渡します（Icon ドキュメントのグリフ表を参照）。テキストとアイコンの両方が非空のときは両方を表示します。
内容がない側は、対応するクリックイベントを発火しません。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar アイコン</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">←</span>
      <span class="sim-navbar__title">アイコンのみ</span>
      <span class="sim-navbar__right">⋯</span>
    </div>
    <div class="sim-navbar">
      <span class="sim-navbar__title">両側に内容なし</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## 区切り線

`Bordered` が true のとき下部に `color.border.lighter` の区切り線を描画します。既定で有効です。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar 区切り線</span></div>
  <div class="sim-screen">
    <div class="sim-navbar sim-navbar--bordered">
      <span class="sim-navbar__title">区切り線あり</span>
    </div>
    <div class="sim-navbar sim-navbar--borderless">
      <span class="sim-navbar__title">区切り線なし</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `LeftClick` | 左側の領域をクリックしたときに発火（左側に内容がないときは発火しない） | `event Action` | — |
| `RightClick` | 右側の領域をクリックしたときに発火（右側に内容がないときは発火しない） | `event Action` | — |
| `Title` | 中央のタイトル | `string` | `""` |
| `LeftText` | 左側のボタンテキスト。LeftIcon と両方非空のときは両方表示 | `string` | `""` |
| `LeftIcon` | 左側のアイコン名（ローカルのアイコン体系で描画） | `string?` | `null` |
| `RightText` | 右側のボタンテキスト。RightIcon と両方非空のときは両方表示 | `string` | `""` |
| `RightIcon` | 右側のアイコン名（ローカルのアイコン体系で描画） | `string?` | `null` |
| `Bordered` | 下部の区切り線を描画するかどうか | `bool` | `true` |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `navbar.height` | 高さ |
| `navbar.paddingHorizontal` | 左右のパディング |
| `navbar.titleFontSize` | タイトルと両側テキストのフォントサイズ |
| `navbar.iconSize` | 両側のアイコンサイズ |
| `navbar.borderWidth` | 下部の区切り線幅 |
| `color.background.default` | 背景色 |
| `color.text.primary` | タイトルと両側テキストの色 |
| `color.border.lighter` | 下部の区切り線色 |
| `zIndex.sticky` | 固定レイヤー |
| `spacing.xs` | アイコンとテキストの間隔 |
| `typography.fontFamily` | 中華風セリフフォントスタック |

## ゴールデンケース对照

`cases/navbar.json` の 6 件のアサーションと xUnit テストの対応関係（`tests/LhNavbarTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `title-renders-text` | タイトルがフォントサイズトークンに従って描画される | `Title_Renders_Text` |
| `leftclick-emits-onleftclick` | 左側のクリックが onLeftClick を発火 | `LeftClick_Emits_OnLeftClick` |
| `rightclick-emits-onrightclick` | 右側のクリックが onRightClick を発火 | `RightClick_Emits_OnRightClick` |
| `no-left-content-no-onleftclick` | 左側に内容がないときは発火しない | `No_Left_Content_No_OnLeftClick` |
| `bordered-uses-border-token` | 区切り線が border.lighter トークンを消費 | `Bordered_Uses_Border_Token` |
| `height-uses-navbar-height-token` | 高さが navbar.height トークンを消費 | `Height_Uses_Navbar_Height_Token` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
