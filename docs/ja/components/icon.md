# Icon アイコン

アイコンコンポーネントです。ローカルのグラフィック体系に従ってアイコン名をグリフとして解決し、サイズとセマンティックな色調をサポートします。

> 契約元：`lh-ui-core/specs/icon.json` ·
> ゴールデンケース：`lh-ui-core/cases/icon.json`（5 件、xUnit テストに翻訳済み）

::: tip プラットフォームメモ
Windows 端では現在 unicode グリフで代用しています（`LhIconTokens.GlyphFor`、TextBlock で描画）。本番環境では `FontIcon`（Segoe Fluent Icons のベクターアセット）に差し替えるべきです。これは button の代用戦略と一致します。登録済みのアイコン名は下記「アイコン名」の節を参照してください。
:::

## サイズ

3 段階のサイズが `icon.size` トークンに対応します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icon サイズ</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--small">⌕</span>
      <span class="sim-icon">⌕</span>
      <span class="sim-icon sim-icon--large">⌕</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var icon = new LhIcon
{
    Name = "search",
    Size = LhIconSize.Large,
};
```

## 色調

`Tone` がグリフの色を決定し、セマンティックカラートークンと一対一に対応します。デフォルトの色調は `color.text.primary` を取ります。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icon 色調</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">✓ デフォルト</span>
      <span class="sim-icon sim-icon--primary">✓ 主色</span>
      <span class="sim-icon sim-icon--success">✓ 成功</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--warning">⚠ 警告</span>
      <span class="sim-icon sim-icon--danger">✕ 危険</span>
      <span class="sim-icon sim-icon--info">ℹ 情報</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--regular">✓ 通常</span>
      <span class="sim-icon sim-icon--secondary">✓ 補助</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## アイコン名

`GlyphFor` に現在登録されているアイコン名とグリフ：`search` ⌕、`refresh` ↻、`check` ✓、`close` ✕、
`warning` ⚠、`info` ℹ、`back` ←、`more` ⋯、`star` ★、`edit` ✎、`delete` ⌫、
`plus` ＋、`minus` －、`arrow` →。未登録名は `◆` にフォールバックします。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>登録済みアイコン</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">⌕</span><span class="sim-icon">↻</span><span class="sim-icon">✓</span>
      <span class="sim-icon">✕</span><span class="sim-icon">⚠</span><span class="sim-icon">ℹ</span>
      <span class="sim-icon">←</span><span class="sim-icon">⋯</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon">★</span><span class="sim-icon">✎</span><span class="sim-icon">⌫</span>
      <span class="sim-icon">＋</span><span class="sim-icon">－</span><span class="sim-icon">→</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## ライト / ダークテーマ

サイト右上のテーマを切り替えると、シミュレーターの携帯フレーム内のアイコン色が変わります。実機では `LhTheme.Apply(true)` で切り替えます。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>ダークアイコン</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--primary">★</span>
      <span class="sim-icon sim-icon--success">✓</span>
      <span class="sim-icon sim-icon--warning">⚠</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません。ライト / ダークの値はすべて core のトークンスナップショットから取得</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `Name` | アイコン名（ローカルのアイックンテーブルで解決、未登録名は `◆` にフォールバック） | `string` | `""` |
| `Size` | アイコンサイズ。`icon.size.*` トークンに対応 | `LhIconSize` | `Medium` |
| `Tone` | アイコンの色調。セマンティックカラー / テキストカラートークンに対応 | `LhIconTone` | `Regular` |

### LhIconSize

| 値 | サイズトークン |
| --- | --- |
| `Small` | `icon.size.small`（16epx） |
| `Medium` | `icon.size.medium`（18epx） |
| `Large` | `icon.size.large`（20epx） |

### LhIconTone

| 値 | カラートークン |
| --- | --- |
| `Default` | `color.text.primary` |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `icon.size.{small,medium,large}` | 3 段階のサイズ |
| `color.text.{primary,regular,secondary}` | ニュートラルな色調 |
| `color.{primary,success,warning,danger,info}.default` | セマンティックな色調 |
| `typography.fontFamily` | グリフのフォントスタック |

## ゴールデンケース对照

`cases/icon.json` の 5 件のアサーションと xUnit テストの対応関係（`tests/LhIconTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `click-emits-onclick` | ゲートが通す（アイコンはインタラクティブ） | `Click_Emits_OnClick` |
| `default-tone-uses-text-primary` | デフォルトの色調が主要テキストカラートークンを取る | `Default_Tone_Uses_Text_Primary` |
| `primary-tone-uses-primary-token` | 主色の色調がプライマリトークンを取る | `Primary_Tone_Uses_Primary_Token` |
| `small-size-uses-small-token` | 小サイズが small サイズトークンを消費 | `Small_Size_Uses_Small_Token` |
| `render-emits-icon-command` | アイコンコマンドはただ一つ発行される | `Render_Emits_Single_Icon_Command` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
