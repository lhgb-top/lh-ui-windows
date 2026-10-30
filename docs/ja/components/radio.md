# Radio ラジオボタン

ラジオボタンです。リング + 内側の円点で、クリックは行全体にヒットします。コンポーネント自体はグループのロジックを持たず、ラジオグループはホスト側が維持します。

> 契約元：`lh-ui-core/specs/radio.json` ·
> ゴールデンケース：`lh-ui-core/cases/radio.json`（6 件、xUnit テストに翻訳済み）

## 基本的な使い方

ホストは `Change` を監視し、同じグループ内の他の項目の `Checked` を false にすればラジオグループが実現します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">青花瓷</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"></span>
        <span class="sim-radio__label">粉彩瓷</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var radio = new LhRadio
{
    Label = "青花瓷",
    Value = "qinghua",
    Checked = true,
};
radio.Change += isChecked => { /* 未選択かつ無効でないときのみ発火；ラジオグループはホストが維持 */ };
```

## サイズ

3 段階のサイズが `radio.size.*` のリングサイズトークンに対応します。内側の円点の直径 = サイズ × `radio.dotScale` です。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio サイズ</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--small"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">小サイズ</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">中サイズ</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--large"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">大サイズ</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## 状態

`Disabled` のときはクリックに応答せず、外側のリングは `color.disabled.border`、円点は `color.disabled.text` を取ります。
選択済みの状態で再度クリックしても `Change` は発火しません。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio 状態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"></span>
        <span class="sim-radio__label">無効（未選択）</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">無効（選択）</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `Change` | 選択状態の変化コールバック。未選択かつ無効でないときのみ発火（payload は常に true） | `event Action<bool>` | — |
| `Checked` | 選択されているかどうか | `bool` | `false` |
| `Disabled` | 無効かどうか（クリックに応答せず、無効スタイルを表示） | `bool` | `false` |
| `Label` | ラベルテキスト（リングの右側に描画） | `string` | `""` |
| `Value` | ラジオ値（グループ内の識別子、コンポーネント自体はグループのロジックを持たない） | `string` | `""` |
| `Size` | リングサイズ。`radio.size.*` トークンに対応 | `LhRadioSize` | `Medium` |

### LhRadioSize

| 値 | サイズトークン |
| --- | --- |
| `Small` | `radio.size.small`（14epx） |
| `Medium` | `radio.size.medium`（16epx） |
| `Large` | `radio.size.large`（18epx） |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `radio.size.{small,medium,large}` | 3 段階のリングサイズ |
| `radio.ringWidth` | 外側のリングのボーダー幅 |
| `radio.dotScale` | 内側の円点の直径比率（サイズ × dotScale） |
| `radio.gap` | リングとテキストの間隔 |
| `color.primary.default` | 選択状態のリングと円点の色 |
| `color.border.base` | 未選択のリング色 |
| `color.disabled.{border,text}` | 無効状態のリング / 円点 |
| `color.text.{primary,disabled}` | ラベルのテキスト色 |
| `typography.fontSize.base` / `fontFamily` | ラベルのフォントサイズとフォントスタック |

## ゴールデンケース对照

`cases/radio.json` の 6 件のアサーションと xUnit テストの対応関係（`tests/LhRadioTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `tap-unchecked-emits-onchange` | 未選択のタップが onChange を発火 | `Tap_Unchecked_Emits_OnChange` |
| `tap-checked-keeps-checked` | 選択済みの再タップは発火しない | `Tap_Checked_Keeps_Checked` |
| `disabled-blocks-change` | 無効状態はインタラクティブでない | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | 選択状態がプライマリトークンを消費 | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | 未選択状態がボーダートークンを消費 | `Unchecked_Uses_Border_Token` |
| `small-size-uses-small-token` | 小サイズが small サイズトークンを消費 | `Small_Size_Uses_Small_Token` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
