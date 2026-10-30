# Tag タグ

マーキングと選択に使うタグです。6 種類のセマンティックタイプ、3 段階のサイズ、アウトライン、角丸、閉じるボタンをサポートします。

> 契約元：`lh-ui-core/specs/tag.json` ·
> ゴールデンケース：`lh-ui-core/cases/tag.json`（6 件、xUnit テストに翻訳済み）

## タイプ

6 種類のタイプが背景色、テキスト色、ボーダー色を決定します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tag タイプ</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag">デフォルト</span>
      <span class="sim-tag sim-tag--primary">主要</span>
      <span class="sim-tag sim-tag--success">成功</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--warning">警告</span>
      <span class="sim-tag sim-tag--danger">危険</span>
      <span class="sim-tag sim-tag--info">情報</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var tag = new LhTag
{
    Text = "主要",
    Type = LhTagType.Primary,
};
```

## アウトラインと角丸

`Plain` を true にすると、セマンティックな浅い背景 + セマンティックなテキスト + セマンティックなボーダーに切り替わります。`Round` を true にすると角丸が高さの半分になります（カプセル形態、`tag.borderRadius` トークンとは並列）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>アウトラインと角丸</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--plain">アウトライン</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">アウトライン</span>
      <span class="sim-tag sim-tag--danger sim-tag--plain">アウトライン</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--round">角丸</span>
      <span class="sim-tag sim-tag--success sim-tag--round sim-tag--plain">角丸アウトライン</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## サイズと閉じる

3 段階のサイズが `tag.height` / `paddingHorizontal` / `fontSize` トークンに対応します。`Closable` を true にすると右側に閉じるシンボルを描画し、クリックで `Close` イベントを発火します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>サイズと閉じる</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--small">小</span>
      <span class="sim-tag">中</span>
      <span class="sim-tag sim-tag--large">大</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">閉じる可 <span class="sim-tag__close">✕</span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var tag = new LhTag
{
    Text = "閉じる可",
    Type = LhTagType.Primary,
    Closable = true,
};
tag.Close += () => { /* 閉じるシンボルをクリック */ };
```

## ライト / ダークテーマ

サイト右上のテーマを切り替えると、シミュレーターの携帯フレーム内のタグ色が変わります。実機では `LhTheme.Apply(true)` で切り替えます。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>ダークタグ</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">主要</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">アウトライン</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません。ライト / ダークの値はすべて core のトークンスナップショットから取得</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `Close` | 閉じるシンボルのクリック時に発火（`Closable` が true であること） | `event Action` | — |
| `Text` | タグのテキスト内容（spec の default スロット） | `string` | `""` |
| `Type` | セマンティックタイプ。背景色、テキスト色、ボーダー色を決定 | `LhTagType` | `Default` |
| `Size` | タグのサイズ。サイズトークンに対応 | `LhTagSize` | `Medium` |
| `Plain` | 浅い背景のアウトライン形態かどうか | `bool` | `false` |
| `Round` | カプセル形態かどうか（角丸が高さの半分になる） | `bool` | `false` |
| `Closable` | 閉じるシンボルを表示するかどうか | `bool` | `false` |

### LhTagType

| 値 | 塗り背景 | 浅い背景（plain） | テキスト色 |
| --- | --- | --- | --- |
| `Default` | `color.background.default` | 同左 | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.primary.bg` | `color.onPrimary` / `color.primary.default` |
| `Success` | `color.success.default` | `color.success.bg` | `color.onPrimary` / `color.success.default` |
| `Warning` | `color.warning.default` | `color.warning.bg` | `color.onPrimary` / `color.warning.default` |
| `Danger` | `color.danger.default` | `color.danger.bg` | `color.onPrimary` / `color.danger.default` |
| `Info` | `color.info.default` | `color.info.bg` | `color.onPrimary` / `color.info.default` |

### LhTagSize

| 値 | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `tag.height.small`（20epx） | `tag.paddingHorizontal.small`（8epx） | `tag.fontSize.small`（11） |
| `Medium` | `tag.height.medium`（24epx） | `tag.paddingHorizontal.medium`（12epx） | `tag.fontSize.medium`（12） |
| `Large` | `tag.height.large`（28epx） | `tag.paddingHorizontal.large`（16epx） | `tag.fontSize.large`（13） |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `tag.height.{small,medium,large}` | 3 段階の高さ |
| `tag.paddingHorizontal.{small,medium,large}` | 3 段階の横向きパディング |
| `tag.fontSize.{small,medium,large}` | 3 段階のフォントサイズ |
| `tag.borderRadius` / `tag.borderWidth` | 角丸とボーダー幅 |
| `color.primary.default` / `color.onPrimary` | プライマリタイプの背景 / テキスト |
| `color.{success,warning,danger,info}.{default,bg}` | セマンティックタイプの塗り背景 / 浅い背景 |
| `color.background.default` / `color.text.primary` / `color.border.base` | デフォルトタイプの三色 |
| `spacing.xs` | 閉じるシンボルとテキストの間隔 |
| `typography.fontFamily` | 中華風セリフフォントスタック |

## ゴールデンケース对照

`cases/tag.json` の 6 件のアサーションと xUnit テストの対応関係（`tests/LhTagTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `default-type-renders-text` | デフォルトタイプのニュートラルな背景 / テキスト / ボーダー | `Default_Type_Renders_Text` |
| `primary-type-uses-primary-tokens` | プライマリタイプがプライマリトークンを消費 | `Primary_Type_Uses_Primary_Tokens` |
| `plain-uses-bg-tokens` | アウトライン形態がセマンティックな浅い背景トークンを消費 | `Plain_Uses_Bg_Tokens` |
| `small-size-uses-small-tokens` | 小サイズが small サイズトークンを消費 | `Small_Size_Uses_Small_Tokens` |
| `round-makes-radius-half-height` | カプセル形態の角丸が高さの半分になる | `Round_Makes_Radius_Half_Height` |
| `closable-tap-emits-onclose` | 閉じるシンボルのクリックが onClose を発火 | `Closable_Tap_Emits_OnClose` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
