# Button ボタン

基本的な操作ボタンです。即時操作をトリガーし、タイプ、サイズ、読み込み、無効化、アイコン、中華風の紋様背景をサポートします。

> 契約元：`lh-ui-core/specs/button.json` ·
> ゴールデンケース：`lh-ui-core/cases/button.json`（8 件、xUnit テストに翻訳済み）

## タイプ

6 種類のタイプが背景色とテキスト色を決定します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button タイプ</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">デフォルトボタン</button>
      <button class="sim-btn sim-btn--primary">プライマリボタン</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--success">成功ボタン</button>
      <button class="sim-btn sim-btn--warning">警告ボタン</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--danger">危険ボタン</button>
      <button class="sim-btn sim-btn--info">情報ボタン</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var button = new LhButton
{
    Type = LhButtonType.Primary,
    Text = "プライマリボタン",
};
button.Click += OnButtonClick;
```

## サイズ

3 段階のサイズが `button.height` / `paddingHorizontal` / `fontSize` / `iconSize` トークンに対応します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button サイズ</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">小サイズボタン</button>
      <button class="sim-btn">中サイズボタン</button>
      <button class="sim-btn sim-btn--large">大サイズボタン</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## 状態

無効と読み込み中はどちらも `Click` を発火しません。両方指定された場合は**無効が優先**されます（ゴールデンケース `disabled-precedes-loading`）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button 状態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">通常</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">無効</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>読み込み中
      </button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">無効が優先</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## アイコンと紋様背景

アイコン名はローカルのアイコン体系で解決されます（`LhButtonTokens.IconFor`、現在は search / refresh / check / close をサポート、未登録名は描画されません）。紋様背景は `color.decoration` トークンを消費し、45° の斜線として重なり描画されます。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>アイコンと紋様</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>検索</button>
      <button class="sim-btn"><span class="sim-btn__icon"></span>デフォルト</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--pattern">プライマリ紋様背景</button>
      <button class="sim-btn sim-btn--pattern">デフォルト紋様背景</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## ライト / ダークテーマ

サイト右上のテーマを切り替えると、シミュレーターの携帯フレーム内のボタン色が変わります。実機では `LhTheme.Apply(true)` で切り替えます。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>ダークボタン</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">プライマリボタン</button>
      <button class="sim-btn">デフォルトボタン</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--disabled">無効</button>
      <button class="sim-btn sim-btn--success">成功</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません。ライト / ダークの値はすべて core のトークンスナップショットから取得</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `Click` | クリックイベント。`Disabled` または `Loading` が true の場合は発火しない | `event Action` | — |
| `Type` | ボタンタイプ。背景色とテキスト色を決定 | `LhButtonType` | `Default` |
| `Size` | ボタンサイズ。サイズトークンに対応 | `LhButtonSize` | `Medium` |
| `Disabled` | 無効かどうか | `bool` | `false` |
| `Loading` | 読み込み中かどうか（インジケーターを表示し、Click を発火しない） | `bool` | `false` |
| `Icon` | アイコン名（ローカルのアイコン体系で解決） | `string?` | `null` |
| `BgPattern` | 中華風の紋様背景を描画するかどうか | `bool` | `false` |
| `Text` | ボタンのテキスト内容（spec の default スロット） | `string` | `""` |

### LhButtonType

| 値 | 背景色 | テキスト色 |
| --- | --- | --- |
| `Default` | `color.background.default` | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.onPrimary` |
| `Success` | `color.success.default` | `color.onPrimary` |
| `Warning` | `color.warning.default` | `color.onPrimary` |
| `Danger` | `color.danger.default` | `color.onPrimary` |
| `Info` | `color.info.default` | `color.onPrimary` |

### LhButtonSize

| 値 | height | paddingHorizontal | fontSize | iconSize |
| --- | --- | --- | --- | --- |
| `Small` | `button.height.small`（32epx） | `button.paddingHorizontal.small`（16epx） | `button.fontSize.small`（13） | `button.iconSize.small`（16） |
| `Medium` | `button.height.medium`（40epx） | `button.paddingHorizontal.medium`（20epx） | `button.fontSize.medium`（14） | `button.iconSize.medium`（18） |
| `Large` | `button.height.large`（48epx） | `button.paddingHorizontal.large`（24epx） | `button.fontSize.large`（16） | `button.iconSize.large`（20） |

無効状態の上書き：背景 `color.disabled.bg`、テキスト `color.disabled.text`、ボーダー `color.disabled.border`。

## トークン对照

ボタンが消費するトークン（`specs/button.json` の `tokenRefs` と一対一で対応）：

| トークン | 用途 |
| --- | --- |
| `button.height.{small,medium,large}` | 3 段階の高さ |
| `button.paddingHorizontal.{small,medium,large}` | 3 段階の横向きパディング |
| `button.fontSize.{small,medium,large}` | 3 段階のフォントサイズ |
| `button.iconSize.{small,medium,large}` | 3 段階のアイコンサイズ |
| `button.borderRadius` / `button.borderWidth` | 角丸（`radius.base` に追従）とボーダー幅 |
| `button.fontWeight` | フォントの太さ（`typography.fontWeight.primary` に追従、エンド側で FontWeights にマッピング） |
| `color.primary.default` / `color.onPrimary` | プライマリタイプの背景 / テキスト |
| `color.{success,warning,danger,info}.default` | セマンティックタイプの背景色 |
| `color.background.default` / `color.text.primary` / `color.border.base` | デフォルトタイプの背景 / テキスト / ボーダー |
| `color.disabled.{bg,text,border}` | 無効状態の三色 |
| `color.decoration` | 紋様の色 |
| `spacing.xs` | アイコンとテキストの間隔 |
| `typography.fontFamily` | 中華風セリフフォントスタック |

## ゴールデンケース对照

`cases/button.json` の 8 件のアサーションと xUnit テストの対応関係（`tests/LhButtonTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `click-emits-ononclick` | ゲートが通す（インタラクティブ） | `Enabled_Button_Is_Interactive` |
| `disabled-blocks-click` | 無効状態はインタラクティブでない | `Disabled_Is_Not_Interactive` |
| `loading-blocks-click` | 読み込み状態はインタラクティブでない | `Loading_Is_Not_Interactive` |
| `disabled-precedes-loading` | 無効が優先 + 無効スタイル | `Disabled_Precedes_Loading` |
| `default-type-renders-text` | デフォルトタイプのニュートラルな背景 / テキスト | `Default_Type_Uses_Neutral_Tokens` |
| `primary-type-uses-primary-tokens` | プライマリタイプがプライマリトークンを消費 | `Primary_Type_Uses_Primary_Tokens` |
| `small-size-uses-small-tokens` | 小サイズが small サイズトークンを消費 | `Small_Size_Uses_Small_Tokens` |
| `bgpattern-uses-decoration-token` | 紋様背景が装飾色を消費 | `BgPattern_Uses_Decoration_Token` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
