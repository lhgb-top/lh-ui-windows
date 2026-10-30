# Input 入力フィールド

単一行のテキスト入力です。タイプ、サイズ、無効、読み取り専用、クリア、最大文字数をサポートします。

> 契約元：`lh-ui-core/specs/input.json` ·
> ゴールデンケース：`lh-ui-core/cases/input.json`（8 件、xUnit テストに翻訳済み）

## 基本的な使い方

コントロールされるコンポーネントです：ホストが `ValueChange` を監視し、新しい値を `Value` に書き戻します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field sim-input__field--placeholder">キーワードを入力してください</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var input = new LhInput
{
    Placeholder = "キーワードを入力してください",
};
input.ValueChange += value => { /* コントロールされる：Value に書き戻す */ };
```

## サイズ

3 段階のサイズが `input.height` / `paddingHorizontal` / `fontSize` トークンに対応します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input サイズ</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input sim-input--small"><div class="sim-input__field">小サイズ</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">中サイズ</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--large"><div class="sim-input__field">大サイズ</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## タイプと状態

`Password` はドットでマスク表示されます。`Disabled` と `Readonly` はどちらも `ValueChange` を発火しませんが、
無効スタイルを表示するのは `Disabled` だけです。フォーカス時の枠線は `color.focus` + `input.focusRingWidth` を取ります。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input 状態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">●●●●●●●●</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--disabled"><div class="sim-input__field">無効</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--focus"><div class="sim-input__field">フォーカス枠線</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## クリアと文字数

`Clearable` が true かつ値が空でないときにクリアボタンを表示し、クリックで `ValueChange("")` を発火します。
`Maxlength` を超えるときは新しい入力を切り詰めます。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>クリアと文字数</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷</div>
        <span class="sim-input__clear">✕</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `ValueChange` | 値変更コールバック（コントロールされる）。disabled / readonly のときは発火しない | `event Action<string>` | — |
| `Focus` | フォーカスを取得したときに発火 | `event Action` | — |
| `Blur` | フォーカスを失ったときに発火 | `event Action` | — |
| `Value` | 現在のテキスト値（コントロールされる） | `string` | `""` |
| `Placeholder` | プレースホルダーのテキスト。`color.text.placeholder` トークンを取る | `string` | `""` |
| `Type` | 入力タイプ。password のときはドットでマスク表示 | `LhInputType` | `Text` |
| `Size` | 入力フィールドのサイズ。サイズトークンに対応 | `LhInputSize` | `Medium` |
| `Disabled` | 無効かどうか（入力に応答せず、無効スタイルを表示） | `bool` | `false` |
| `Readonly` | 読み取り専用かどうか（フォーカスは可能だが入力を受け付けない） | `bool` | `false` |
| `Clearable` | クリアボタンを表示するかどうか | `bool` | `false` |
| `Maxlength` | 最大文字数。超えた分は新しい入力を切り詰める | `int?` | `null` |

### LhInputSize

| 値 | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `input.height.small`（32epx） | `input.paddingHorizontal.small`（12epx） | `input.fontSize.small`（13） |
| `Medium` | `input.height.medium`（40epx） | `input.paddingHorizontal.medium`（16epx） | `input.fontSize.medium`（14） |
| `Large` | `input.height.large`（48epx） | `input.paddingHorizontal.large`（20epx） | `input.fontSize.large`（16） |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `input.height.{small,medium,large}` | 3 段階の高さ |
| `input.paddingHorizontal.{small,medium,large}` | 3 段階の横向きパディング |
| `input.fontSize.{small,medium,large}` | 3 段階のフォントサイズ |
| `input.borderRadius` / `input.borderWidth` | 角丸とフォーカス外れの枠線幅 |
| `input.focusRingWidth` / `color.focus` | フォーカス枠線の幅と色 |
| `input.clearIconSize` | クリアボタンのアイコンサイズ |
| `color.background.default` / `color.text.primary` | コンテナの背景 / テキスト色 |
| `color.text.placeholder` | プレースホルダーのテキスト色 |
| `color.border.base` | フォーカス外れの枠線色 |
| `color.disabled.{bg,text}` | 無効状態の背景 / テキスト |
| `typography.fontFamily` | 中華風セリフフォントスタック |

## ゴールデンケース对照

`cases/input.json` の 8 件のアサーションと xUnit テストの対応関係（`tests/LhInputTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `input-emits-onvaluechange` | 入力が onValueChange を発火 | `Input_Emits_OnValueChange` |
| `clearable-emits-empty-value` | クリアが onValueChange("") を発火 | `Clearable_Emits_Empty_Value` |
| `disabled-blocks-input` | 無効状態は発火せず、無効スタイルを表示 | `Disabled_Blocks_Input` |
| `readonly-blocks-input` | 読み取り専用は発火しないが、無効スタイルにはならない | `Readonly_Blocks_Input` |
| `placeholder-uses-placeholder-token` | プレースホルダーのテキストがプレースホルダートークンを取る | `Placeholder_Uses_Placeholder_Token` |
| `focus-uses-focus-token` | フォーカス枠線が focus トークンを取る | `Focus_Uses_Focus_Token` |
| `small-size-uses-small-tokens` | 小サイズが small サイズトークンを消費 | `Small_Size_Uses_Small_Tokens` |
| `maxlength-truncates-input` | maxlength を超えた入力は切り詰められる | `Maxlength_Truncates_Input` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
