# Picker ピッカー

下部からポップアップする単列ホイールです。候補項目、インデックスのクランプ、ツールバーをサポートします。

> 契約元：`lh-ui-core/specs/picker.json` ·
> ゴールデンケース：`lh-ui-core/cases/picker.json`（8 件、xUnit テストに翻訳済み）

## 基本的な使い方

宣言的な可視性：`Visible` が false のときは何も描画しません。確認 / キャンセル後に `Close` を発火し、
ホストはそれに応じて `Visible` を false にします。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Picker 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-picker-overlay"></div>
    <div class="sim-picker">
      <div class="sim-picker__toolbar">
        <span class="sim-picker__cancel">キャンセル</span>
        <span class="sim-picker__title">選択</span>
        <span class="sim-picker__confirm">確認</span>
      </div>
      <div class="sim-picker__wheel">
        <span class="sim-picker__item">青花瓷</span>
        <span class="sim-picker__item sim-picker__item--active">粉彩瓷</span>
        <span class="sim-picker__item">玲珑瓷</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var picker = new LhPicker
{
    Options = new[] { "青花瓷", "粉彩瓷", "玲珑瓷" },
    Value = 1,
    Title = "選択",
};
picker.ValueChange += index => { /* ホイール停止後の選択インデックス */ };
picker.Confirm += index => { /* 現在の選択インデックス */ };
picker.Close += () => picker.Visible = false;

picker.Visible = true;
```

## インデックスのクランプ

`Value` が範囲外のときは候補項目の範囲にクランプします：負数は 0、候補項目数 - 1 より大きい場合は最後の項目を取ります。

```csharp
Assert.Equal(0, LhPickerTokens.ClampIndex(-1, 3));   // 負数は 0 を取る
Assert.Equal(2, LhPickerTokens.ClampIndex(100, 3));  // 範囲外は最後の項目を取る
```

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `ValueChange` | ホイールが停止し、選択項目が変化したときに発火 | `event Action<int>` | — |
| `Confirm` | 確認ボタンのクリックで発火し、現在の選択インデックスを伴う | `event Action<int>` | — |
| `Cancel` | キャンセルボタンのクリックで発火 | `event Action` | — |
| `Close` | 確認 / キャンセル後に発火。ホストはそれに応じて Visible を false にする | `event Action` | — |
| `Visible` | 表示するかどうか。false のときは何も描画しない | `bool` | `false` |
| `Options` | 候補項目のテキスト配列（単列） | `string[]?` | `null` |
| `Value` | 選択項目のインデックス（0 始まり）。範囲外のときは候補項目の範囲にクランプ | `int` | `0` |
| `Title` | ツールバー中央のタイトル | `string` | `""` |
| `ConfirmText` | 確認ボタンのテキスト | `string` | `"确认"` |
| `CancelText` | キャンセルボタンのテキスト | `string` | `"取消"` |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `picker.height` | パネルの高さ |
| `picker.toolbarHeight` | 上部ツールバーの高さ（ホイール領域の高さ = パネルの高さ - ツールバーの高さ） |
| `picker.itemHeight` | 候補項目の高さ（中央インジケーターの間隔もこれと同じ） |
| `picker.itemFontSize` | 候補項目とツールバーのフォントサイズ |
| `picker.borderRadius` | パネル上部の角丸 |
| `color.background.default` | パネルの背景色 |
| `color.background.overlay` | オーバーレイ色 |
| `color.text.primary` | タイトルと候補項目の色 |
| `color.text.secondary` | キャンセルボタンの色 |
| `color.primary.default` | 確認ボタンの色 |
| `color.border.lighter` | 中央インジケーターの線色 |
| `zIndex.modal` / `zIndex.modalBackdrop` | パネル / オーバーレイのレイヤー順 |

## ゴールデンケース对照

`cases/picker.json` の 8 件のアサーションと xUnit テストの対応関係（`tests/LhPickerTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `confirm-emits-onconfirm-with-value` | 確認が現在の選択インデックスを伴う | `Confirm_Emits_OnConfirm_With_Value` |
| `confirm-also-emits-onclose` | 確認は同時に onClose を発火 | `Confirm_Also_Emits_OnClose` |
| `cancel-emits-oncancel` | キャンセルが onCancel を発火 | `Cancel_Emits_OnCancel` |
| `scroll-emits-onvaluechange` | ホイール停止が onValueChange を発火 | `Scroll_Emits_OnValueChange` |
| `clamp-negative-value-to-zero` | 負数のインデックスを 0 にクランプ | `Clamp_Negative_Value_To_Zero` |
| `clamp-overflow-value-to-last` | 範囲外のインデックスを最後の項目にクランプ | `Clamp_Overflow_Value_To_Last` |
| `title-renders-text` | タイトルがフォントサイズトークンに従って描画される | `Title_Renders_Text` |
| `hidden-not-rendered` | 非表示のときは何も描画しない | `Hidden_Not_Rendered` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
