# Dialog ダイアログ

モーダルダイアログです。オーバーレイ + 中央の内容カード + 確認 / キャンセルボタンで、セマンティックタイプとオーバーレイによる閉じる操作をサポートします。

> 契約元：`lh-ui-core/specs/dialog.json` ·
> ゴールデンケース：`lh-ui-core/cases/dialog.json`（8 件、xUnit テストに翻訳済み）

## 基本的な使い方

宣言的な可視性：`Visible` が false のときは何も描画しません。確認 / キャンセル後に `Close` を発火し、
ホストはそれに応じて `Visible` を false にします。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">削除の確認</div>
      <div class="sim-dialog__content">削除後は復元できません。このレコードを削除しますか？</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">キャンセル</button>
        <button class="sim-btn sim-btn--danger">確認</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var dialog = new LhDialog
{
    Title = "削除の確認",
    Content = "削除後は復元できません。このレコードを削除しますか？",
    Type = LhDialogType.Danger,
    ConfirmText = "確認",
    CancelText = "キャンセル",
};
dialog.Confirm += () => { /* 削除を実行 */ };
dialog.Close += () => dialog.Visible = false;

dialog.Visible = true;
```

## セマンティックタイプ

`Type` が確認ボタンのタイプ（色）を決定します。キャンセルボタンは常に default タイプです。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog タイプ</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">お知らせ</div>
      <div class="sim-dialog__content">これはプライマリの確認ダイアログです</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">キャンセル</button>
        <button class="sim-btn sim-btn--primary">確認</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `Confirm` | 確認時に発火 | `event Action` | — |
| `Cancel` | キャンセル時に発火 | `event Action` | — |
| `Close` | 確認 / キャンセル後、または（closable のとき）オーバーレイのクリックで発火。ホストはそれに応じて Visible を false にする | `event Action` | — |
| `Visible` | 表示するかどうか。false のときは何も描画しない | `bool` | `false` |
| `Title` | タイトル | `string` | `""` |
| `Content` | 本文の内容 | `string` | `""` |
| `Type` | セマンティックタイプ。確認ボタンのタイプ（色）を決定 | `LhDialogType` | `Default` |
| `ConfirmText` | 確認ボタンのテキスト。空のときは確認ボタンを表示しない | `string` | `"确认"` |
| `CancelText` | キャンセルボタンのテキスト。空のときはキャンセルボタンを表示しない | `string` | `"取消"` |
| `Closable` | オーバーレイ領域のクリックで Close を発火するかどうか | `bool` | `false` |

### LhDialogType

| 値 | 確認ボタンのタイプ |
| --- | --- |
| `Default` | `LhButtonType.Default` |
| `Primary` | `LhButtonType.Primary` |
| `Success` | `LhButtonType.Success` |
| `Warning` | `LhButtonType.Warning` |
| `Danger` | `LhButtonType.Danger` |
| `Info` | `LhButtonType.Info` |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `dialog.width` / `dialog.borderRadius` | 内容カードの幅と角丸 |
| `dialog.paddingHorizontal` / `paddingVertical` | 内容カードのパディング |
| `dialog.titleFontSize` / `contentFontSize` | タイトルと本文のフォントサイズ |
| `dialog.gap` | タイトル / 本文 / ボタン領域の間隔 |
| `color.background.default` | 内容カードの背景色 |
| `color.background.overlay` | オーバーレイ色 |
| `color.text.primary` / `color.text.regular` | タイトル / 本文の色 |
| `zIndex.modal` / `zIndex.modalBackdrop` | 内容カード / オーバーレイのレイヤー順 |
| `shadow.base` | 内容カードのシャドウ |

## ゴールデンケース对照

`cases/dialog.json` の 8 件のアサーションと xUnit テストの対応関係（`tests/LhDialogTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `confirm-emits-onconfirm` | 確認が onConfirm を発火 | `Confirm_Emits_OnConfirm` |
| `cancel-emits-oncancel` | キャンセルが onCancel を発火 | `Cancel_Emits_OnCancel` |
| `confirm-also-emits-onclose` | 確認は同時に onClose を発火 | `Confirm_Also_Emits_OnClose` |
| `closable-overlay-emits-onclose` | closable のときオーバーレイのクリックが onClose を発火 | `Closable_Overlay_Emits_OnClose` |
| `not-closable-blocks-overlay-close` | 非 closable のときオーバーレイでは閉じない | `Not_Closable_Blocks_Overlay_Close` |
| `hidden-not-rendered` | 非表示のときは何も描画しない | `Hidden_Not_Rendered` |
| `title-renders-text` | タイトルと本文がフォントサイズトークンに従って描画される | `Title_Renders_Text` |
| `danger-type-uses-danger-token` | danger タイプが危険色トークンを消費 | `Danger_Type_Uses_Danger_Token` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
