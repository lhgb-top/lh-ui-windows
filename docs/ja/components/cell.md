# Cell セル

リスト行のセルです。左側にアイコン + タイトル（必須の赤い星を付けられる）+ 説明、右側に値と指示矢印、下部にオプションの区切り線を配置します。

> 契約元：`lh-ui-core/specs/cell.json` ·
> ゴールデンケース：`lh-ui-core/cases/cell.json`（6 件、xUnit テストに翻訳済み）

## 基本的な使い方

タイトルは `color.text.primary`、値と説明は `color.text.secondary` を取り、3 つすべてトークン駆動です。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Cell 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">行のタイトル</div>
        <div class="sim-cell__label">説明テキスト</div>
      </div>
      <div class="sim-cell__value">行の内容</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">区切り線付きセル</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var cell = new LhCell
{
    Title = "行のタイトル",
    Label = "説明テキスト",
    Value = "行の内容",
};
```

## アイコンと必須

`Icon` にローカルのアイコン名を渡すと（Icon ドキュメントのグリフ表を参照）左側に描画されます。`Required` を true にするとタイトルの前に `color.danger.default` の赤い星を描画します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>アイコンと必須</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <span class="sim-cell__icon">★</span>
      <div class="sim-cell__main">
        <div class="sim-cell__title">アイコン付き</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title"><span class="sim-cell__required">✱</span>必須項目</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## 区切り線とクリック

`Bordered` を true にすると下部に `color.border.lighter` の区切り線を描画します。`Clickable` を true にすると右側に指示矢印を描画し、クリックで `Click` イベントを発火します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>区切り線とクリック</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">クリック可</div></div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">クリック不可</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var cell = new LhCell
{
    Title = "クリック可",
    Clickable = true,
    Bordered = true,
};
cell.Click += () => { /* Clickable が true のときのみ発火 */ };
```

## ライト / ダークテーマ

サイト右上のテーマを切り替えると、シミュレーターの携帯フレーム内のテキスト色と区切り線色が変わります。実機では `LhTheme.Apply(true)` で切り替えます。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>ダークセル</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">行のタイトル</div>
        <div class="sim-cell__label">説明テキスト</div>
      </div>
      <div class="sim-cell__value">行の内容</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません。ライト / ダークの値はすべて core のトークンスナップショットから取得</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `Click` | クリックイベント。`Clickable` が true のときのみ発火 | `event Action` | — |
| `Title` | タイトル（spec の title スロット） | `string` | `""` |
| `Label` | タイトル下の説明テキスト（label スロット） | `string` | `""` |
| `Value` | 右側の値テキスト（value スロット） | `string` | `""` |
| `Icon` | 左側のアイコン名（ローカルのアイックンテーブルで解決） | `string?` | `null` |
| `Required` | タイトルの前に必須の赤い星を描画するかどうか | `bool` | `false` |
| `Clickable` | クリック可能かどうか（矢印の表示、Click のゲート） | `bool` | `false` |
| `Bordered` | 底部の区切り線を描画するかどうか | `bool` | `true` |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `cell.paddingHorizontal` / `cell.paddingVertical` | パディング |
| `cell.gap` | タイトルと説明の垂直間隔 |
| `cell.fontSizeTitle` / `fontSizeLabel` / `fontSizeValue` | 3 段階のフォントサイズ |
| `color.text.{primary,secondary,regular}` | タイトル / 説明 / 値 / アイコン色 |
| `color.danger.default` | 必須の赤い星 |
| `color.border.lighter` | 底部の区切り線 |
| `icon.size.medium` | 左側のアイコンサイズ |
| `spacing.{sm,xs}` | アイコンの間隔 / 内部要素の間隔 |
| `typography.lineHeight.primary` | テキストの行高 |

## ゴールデンケース对照

`cases/cell.json` の 6 件のアサーションと xUnit テストの対応関係（`tests/LhCellTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `title-and-value-render` | タイトルと値がどちらも描画される | `Title_And_Value_Render` |
| `label-renders-secondary-tone` | 説明テキストが補助テキストカラートークンを取る | `Label_Renders_Secondary_Tone` |
| `required-uses-danger-token` | 必須の赤い星が危険色トークンを消費 | `Required_Uses_Danger_Token` |
| `bordered-emits-line-command` | 区切り線の形態が LINE コマンドを発行する | `Bordered_Emits_Line_Command` |
| `clickable-emits-onclick` | クリック可能な場合にクリックで onClick を発火 | `Clickable_Emits_OnClick` |
| `not-clickable-no-onclick` | クリック可能でない場合 onClick を発火しない | `Not_Clickable_No_OnClick` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
