# Loading ローディング

リングが回転する読み込みインジケーターです。サイズとセマンティックな色調をサポートします。

> 契約元：`lh-ui-core/specs/loading.json` ·
> ゴールデンケース：`lh-ui-core/cases/loading.json`（5 件、xUnit テストに翻訳済み）

::: tip プラットフォームメモ
回転は WinUI の `ProgressRing` プラットフォームアニメーションが担います（素朴なトーン：幾何のみを発行し、アニメーションはプラットフォームが補完）——
トークン層は直径・枠線幅・リング色だけを決定します。
:::

## サイズ

3 段階のサイズが `loading.size.*` の直径トークンに対応します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Loading サイズ</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--small"></span>
      <span class="sim-loading"></span>
      <span class="sim-loading sim-loading--large"></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var loading = new LhLoading
{
    Size = LhLoadingSize.Large,
    Tone = LhLoadingTone.Primary,
};
```

## 色調

`Tone` がリング色を決定し、セマンティックカラートークンと一対一に対応します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Loading 色調</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--primary"></span>
      <span class="sim-loading sim-loading--success"></span>
      <span class="sim-loading sim-loading--warning"></span>
    </div>
    <div class="sim-row">
      <span class="sim-loading sim-loading--danger"></span>
      <span class="sim-loading sim-loading--info"></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `Size` | インジケーターの直径。`loading.size.*` トークンに対応 | `LhLoadingSize` | `Medium` |
| `Tone` | リング色。対応するセマンティックカラートークンを取る | `LhLoadingTone` | `Primary` |

### LhLoadingSize

| 値 | 直径トークン |
| --- | --- |
| `Small` | `loading.size.small`（20epx） |
| `Medium` | `loading.size.medium`（28epx） |
| `Large` | `loading.size.large`（36epx） |

### LhLoadingTone

| 値 | リング色トークン |
| --- | --- |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `loading.size.{small,medium,large}` | 3 段階の直径 |
| `loading.strokeWidth` | 読み込みリングの枠線幅 |
| `color.{primary,success,warning,danger,info}.default` | 5 種類のリング色 |

## ゴールデンケース对照

`cases/loading.json` の 5 件のアサーションと xUnit テストの対応関係（`tests/LhLoadingTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `medium-size-uses-medium-token` | 中サイズが medium の直径トークンを消費 | `Medium_Size_Uses_Medium_Token` |
| `large-size-uses-large-token` | 大サイズが large の直径トークンを消費 | `Large_Size_Uses_Large_Token` |
| `primary-tone-uses-primary-token` | 主色の色調がプライマリトークンを取る | `Primary_Tone_Uses_Primary_Token` |
| `success-tone-uses-success-token` | 成功色の色調が成功色トークンを取る | `Success_Tone_Uses_Success_Token` |
| `render-emits-ring-command` | RING 命令はただ一つ発行される | `Render_Emits_Ring_Command` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
