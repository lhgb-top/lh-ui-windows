# Switch スイッチ

スイッチです。カプセル型のトラック + 円形のつまみで、クリックで切り替わります。無効と読み込み中をサポートします。

> 契約元：`lh-ui-core/specs/switch.json` ·
> ゴールデンケース：`lh-ui-core/cases/switch.json`（6 件、xUnit テストに翻訳済み）

## 基本的な使い方

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };
```

## サイズ

3 段階のサイズが `switch.width` / `switch.height` トークンに対応します。つまみの直径 = 高さ - 2 × `switch.gap` です。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch サイズ</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--small sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--large sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## 状態

`Disabled` と `Loading` はどちらもクリックに応答しません。`Loading` のときはつまみの位置に主色の読み込みリングを描画します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch 状態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--checked sim-switch--disabled"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--loading"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `Change` | スイッチ状態の変化コールバック。disabled または loading のときは発火しない | `event Action<bool>` | — |
| `Checked` | オンかどうか | `bool` | `false` |
| `Disabled` | 無効かどうか（クリックに応答せず、無効スタイルを表示） | `bool` | `false` |
| `Loading` | 読み込み中かどうか（つまみの位置に読み込みリングを表示し、クリックに応答しない） | `bool` | `false` |
| `Size` | スイッチのサイズ。`switch.width` / `switch.height` トークンに対応 | `LhSwitchSize` | `Medium` |

### LhSwitchSize

| 値 | width | height |
| --- | --- | --- |
| `Small` | `switch.width.small`（40epx） | `switch.height.small`（22epx） |
| `Medium` | `switch.width.medium`（48epx） | `switch.height.medium`（26epx） |
| `Large` | `switch.width.large`（56epx） | `switch.height.large`（30epx） |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `switch.width.{small,medium,large}` | 3 段階のトラック幅 |
| `switch.height.{small,medium,large}` | 3 段階のトラック高さ |
| `switch.gap` | つまみとトラックの余白（つまみの直径 = 高さ - 2 × gap） |
| `color.primary.default` | オンのトラック色 |
| `color.border.base` | オフのトラック色 |
| `color.onPrimary` | つまみの色 |
| `color.disabled.bg` / `color.text.disabled` | 無効状態のトラック / つまみの色 |

## ゴールデンケース对照

`cases/switch.json` の 6 件のアサーションと xUnit テストの対応関係（`tests/LhSwitchTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | クリックが反転し onChange を発火 | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-turns-off` | オンのタップでオフになる | `Tap_Checked_Turns_Off` |
| `disabled-blocks-change` | 無効状態はインタラクティブでない | `Disabled_Blocks_Change` |
| `loading-blocks-change` | 読み込み状態はインタラクティブでない | `Loading_Blocks_Change` |
| `checked-uses-primary-token` | オンがプライマリトークンを消費 | `Checked_Uses_Primary_Token` |
| `unchecked-uses-border-token` | オフがボーダートークンを消費 | `Unchecked_Uses_Border_Token` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
