# Textarea テキストエリア

複数行のテキスト入力です。`Rows` で初期の表示行数を指定し、内容が超えると自動で高くなります。文字数カウントをサポートします。

> 契約元：`lh-ui-core/specs/textarea.json` ·
> ゴールデンケース：`lh-ui-core/cases/textarea.json`（6 件、xUnit テストに翻訳済み）

## 基本的な使い方

コントロールされるコンポーネントです：ホストが `ValueChange` を監視し、新しい値を `Value` に書き戻します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷<br/>粉彩瓷</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field sim-textarea__field--placeholder">説明を入力してください</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var textarea = new LhTextarea
{
    Rows = 3,
    Placeholder = "説明を入力してください",
};
textarea.ValueChange += value => { /* コントロールされる：Value に書き戻す */ };
```

## 文字数カウント

`ShowCount` が true のとき右下に文字数カウントを描画します。`Maxlength` を宣言しているときの形式は「現在 / 最大」で、
そうでなければ現在の長さのみを表示します。`Maxlength` を超えるときは新しい入力を切り詰めます。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>文字数カウント</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷</div>
        <div class="sim-textarea__count">3/10</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷</div>
        <div class="sim-textarea__count">3</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## 状態

`Disabled` と `Readonly` はどちらも `ValueChange` を発火しませんが、無効スタイルを表示するのは `Disabled` だけです。
フォーカス時の枠線は `color.focus` + `textarea.focusRingWidth` を取ります。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea 状態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--disabled">
        <div class="sim-textarea__field">無効</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--focus">
        <div class="sim-textarea__field">フォーカス枠線</div>
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
| `Rows` | 初期の表示行数。高さは行数と内容に応じて伸びる | `int` | `3` |
| `Disabled` | 無効かどうか（入力に応答せず、無効スタイルを表示） | `bool` | `false` |
| `Readonly` | 読み取り専用かどうか（フォーカスは可能だが入力を受け付けない） | `bool` | `false` |
| `ShowCount` | 右下に文字数カウントを表示するかどうか | `bool` | `false` |
| `Maxlength` | 最大文字数。超えた分は新しい入力を切り詰め、showCount のときは分母になる | `int?` | `null` |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `textarea.paddingVertical` / `paddingHorizontal` | パディング |
| `textarea.fontSize` | フォントサイズ |
| `textarea.borderRadius` / `borderWidth` | 角丸とフォーカス外れの枠線幅 |
| `textarea.focusRingWidth` / `color.focus` | フォーカス枠線の幅と色 |
| `textarea.countFontSize` / `color.text.secondary` | 文字数カウントのフォントサイズ / 色 |
| `textarea.minHeight` | 最小の高さ（行数からの算出値はこれを下回らない） |
| `color.background.default` / `color.text.primary` | コンテナの背景 / テキスト色 |
| `color.text.placeholder` | プレースホルダーのテキスト色 |
| `color.border.base` | フォーカス外れの枠線色 |
| `color.disabled.{bg,text}` | 無効状態の背景 / テキスト |
| `typography.lineHeight.primary` | 行の高さ（rows から高さを算出） |
| `typography.fontFamily` | 中華風セリフフォントスタック |

## ゴールデンケース对照

`cases/textarea.json` の 6 件のアサーションと xUnit テストの対応関係（`tests/LhTextareaTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `input-emits-onvaluechange` | 入力が onValueChange を発火 | `Input_Emits_OnValueChange` |
| `disabled-blocks-input` | 無効状態は発火せず、無効スタイルを表示 | `Disabled_Blocks_Input` |
| `showcount-renders-count` | 「現在 / 最大」が countFontSize トークンを取る | `ShowCount_Renders_Count` |
| `showcount-without-maxlength-renders-length-only` | maxlength がない場合は長さのみを表示 | `ShowCount_Without_Maxlength_Renders_Length_Only` |
| `focus-uses-focus-token` | フォーカス枠線が focus トークンを取る | `Focus_Uses_Focus_Token` |
| `maxlength-truncates-input` | maxlength を超えた入力は切り詰められる | `Maxlength_Truncates_Input` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
