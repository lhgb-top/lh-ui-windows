# Text テキスト

テキストコンポーネントです。レベル、フォントの太さ、色調、アラインメントに従って単一のテキストを描画します。タイポグラフィの値はすべて typography トークンから取ります。

> 契約元：`lh-ui-core/specs/text.json` ·
> ゴールデンケース：`lh-ui-core/cases/text.json`（5 件、xUnit テストに翻訳済み）

## レベル

6 段階のレベルが `typography.fontSize.*` トークンに対応します。デフォルトレベルは `base` です。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Text レベル</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--extra-large">青花瓷 extraLarge</div>
    <div class="sim-text sim-text--large">青花瓷 large</div>
    <div class="sim-text sim-text--medium">青花瓷 medium</div>
    <div class="sim-text">青花瓷 base</div>
    <div class="sim-text sim-text--small">青花瓷 small</div>
    <div class="sim-text sim-text--extra-small">青花瓷 extraSmall</div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var text = new LhText
{
    Text = "青花瓷",
    Level = LhTextLevel.Large,
};
```

## フォントの太さと色調

`Weight` は `typography.fontWeight.*` トークンに対応します。`Tone` は `color.text.*` に対応し（selected は `color.selected` を取る）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>太さと色調</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--bold">太字 bold</div>
    <div class="sim-text sim-text--secondary">補助説明 secondary</div>
    <div class="sim-text sim-text--placeholder">プレースホルダー placeholder</div>
    <div class="sim-text sim-text--selected">選択状態 selected</div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## アラインメント

`Align` が行ボックス幅の中での水平位置を決定します（デザイントークンではなく、WinUI の `TextAlignment` にマッピングされます）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>アラインメント</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--start">左寄せ start</div>
    <div class="sim-text sim-text--center">中央寄せ center</div>
    <div class="sim-text sim-text--end">右寄せ end</div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## ライト / ダークテーマ

サイト右上のテーマを切り替えると、シミュレーターの携帯フレーム内のテキスト色が変わります。実機では `LhTheme.Apply(true)` で切り替えます。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>ダークテキスト</span></div>
  <div class="sim-screen">
    <div class="sim-text">通常テキスト regular</div>
    <div class="sim-text sim-text--secondary">補助説明 secondary</div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません。ライト / ダークの値はすべて core のトークンスナップショットから取得</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `Text` | テキスト内容（spec の default スロット） | `string` | `""` |
| `Level` | フォントサイズのレベル。`typography.fontSize.*` に対応 | `LhTextLevel` | `Base` |
| `Weight` | フォントの太さ。`typography.fontWeight.*` に対応 | `LhTextWeight` | `Primary` |
| `Tone` | テキストの色調。`color.text.*` / `color.selected` に対応 | `LhTextTone` | `Regular` |
| `Align` | 水平アラインメント（`TextAlignment` にマッピング） | `LhTextAlign` | `Start` |

### LhTextLevel

| 値 | フォントサイズトークン |
| --- | --- |
| `ExtraLarge` | `typography.fontSize.extraLarge`（20epx） |
| `Large` | `typography.fontSize.large`（18epx） |
| `Medium` | `typography.fontSize.medium`（16epx） |
| `Base` | `typography.fontSize.base`（14epx） |
| `Small` | `typography.fontSize.small`（13epx） |
| `ExtraSmall` | `typography.fontSize.extraSmall`（12epx） |

### LhTextWeight

| 値 | フォントの太さトークン |
| --- | --- |
| `Primary` | `typography.fontWeight.primary`（400、エンド側で Normal にマッピング） |
| `Bold` | `typography.fontWeight.bold`（600、エンド側で SemiBold にマッピング） |

### LhTextTone

| 値 | カラートークン |
| --- | --- |
| `Primary` | `color.text.primary` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |
| `Placeholder` | `color.text.placeholder` |
| `Disabled` | `color.text.disabled` |
| `Selected` | `color.selected` |

### LhTextAlign

| 値 | WinUI マッピング |
| --- | --- |
| `Start` | `TextAlignment.Left` |
| `Center` | `TextAlignment.Center` |
| `End` | `TextAlignment.Right` |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `typography.fontSize.{extraLarge…extraSmall}` | 6 段階のフォントサイズ |
| `typography.fontWeight.{primary,bold}` | 2 段階のフォントの太さ |
| `typography.lineHeight.primary` | 行高（行ボックスの高さ = フォントサイズ × 行高） |
| `typography.fontFamily` | 中華風セリフフォントスタック |
| `color.text.{primary,regular,secondary,placeholder,disabled}` | ニュートラルな色調 |
| `color.selected` | 選択状態の色調 |

## ゴールデンケース对照

`cases/text.json` の 5 件のアサーションと xUnit テストの対応関係（`tests/LhTextTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `default-level-uses-base-tokens` | デフォルトレベルが base タイポグラフィトークンを消費 | `Default_Level_Uses_Base_Tokens` |
| `large-level-uses-large-token` | large レベルが large フォントサイズトークンを消費 | `Large_Level_Uses_Large_Token` |
| `bold-weight-uses-bold-token` | 太字が bold フォントの太さトークンを消費 | `Bold_Weight_Uses_Bold_Token` |
| `secondary-tone-uses-text-secondary` | 補助色調が補助テキストカラートークンを取る | `Secondary_Tone_Uses_Text_Secondary` |
| `render-emits-single-text-command` | TEXT コマンドはただ一つ発行される | `Render_Emits_Single_Text_Command` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
