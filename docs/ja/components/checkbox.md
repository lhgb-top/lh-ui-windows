# Checkbox チェックボックス

チェックボックスです。選択時はチェックマーク、不確定状態では横棒を描画し、クリックは行全体（ボックス + ラベル領域）にヒットします。

> 契約元：`lh-ui-core/specs/checkbox.json` ·
> ゴールデンケース：`lh-ui-core/cases/checkbox.json`（6 件、xUnit テストに翻訳済み）

## 基本的な使い方

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box"></span>
        <span class="sim-checkbox__label">未選択</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">選択済み</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">─</span>
        <span class="sim-checkbox__label">不確定</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var checkbox = new LhCheckbox
{
    Label = "青花瓷",
    Checked = true,
};
checkbox.Change += isChecked => { };
```

## サイズ

3 段階のサイズが `checkbox.size.*` のボックスサイズトークンに対応します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox サイズ</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--small sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">小サイズ</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">中サイズ</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--large sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">大サイズ</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## 状態

`Disabled` のときはクリックに応答せず、背景 / ボーダー / インジケーター / テキストのすべてが `color.disabled.*` トークンを取ります。
`Indeterminate` は不確定状態（横棒）で、クリックすると選択状態になります。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox 状態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked sim-checkbox__box--disabled">✓</span>
        <span class="sim-checkbox__label">無効（選択）</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--disabled"></span>
        <span class="sim-checkbox__label">無効（未選択）</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `Change` | 選択状態の変化コールバック。disabled のときは発火しない | `event Action<bool>` | — |
| `Checked` | 選択されているかどうか | `bool` | `false` |
| `Indeterminate` | 不確定状態（横棒指示）かどうか。クリックで選択状態になる | `bool` | `false` |
| `Disabled` | 無効かどうか（クリックに応答せず、無効スタイルを表示） | `bool` | `false` |
| `Label` | ラベルテキスト（ボックスの右側に描画） | `string` | `""` |
| `Size` | ボックスサイズ。`checkbox.size.*` トークンに対応 | `LhCheckboxSize` | `Medium` |

### LhCheckboxSize

| 値 | サイズトークン |
| --- | --- |
| `Small` | `checkbox.size.small`（14epx） |
| `Medium` | `checkbox.size.medium`（16epx） |
| `Large` | `checkbox.size.large`（18epx） |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `checkbox.size.{small,medium,large}` | 3 段階のボックスサイズ |
| `checkbox.borderRadius` / `checkbox.borderWidth` | ボックスの角丸とボーダー幅 |
| `checkbox.checkWidth` | チェックマーク / 横棒のボーダー幅 |
| `checkbox.gap` | ボックスとテキストの間隔 |
| `color.primary.default` / `color.onPrimary` | 選択 / 不確定状態の背景色とインジケーター色 |
| `color.border.base` | 未選択のボーダー色 |
| `color.disabled.{bg,border,text}` | 無効状態の背景 / ボーダー / インジケーター |
| `color.text.{primary,disabled}` | ラベルのテキスト色 |
| `typography.fontSize.base` / `fontFamily` | ラベルのフォントサイズとフォントスタック |

## ゴールデンケース对照

`cases/checkbox.json` の 6 件のアサーションと xUnit テストの対応関係（`tests/LhCheckboxTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | 未選択のタップで選択状態になる | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-unchecks` | 選択済みのタップで選択解除 | `Tap_Checked_Unchecks` |
| `disabled-blocks-change` | 無効状態はインタラクティブでない | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | 選択状態がプライマリトークンを消費 | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | 未選択状態がボーダートークンを消費 | `Unchecked_Uses_Border_Token` |
| `indeterminate-becomes-checked` | 不確定状態のタップで選択状態になる | `Indeterminate_Becomes_Checked` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
