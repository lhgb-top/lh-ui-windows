# Dialog 對話框

模態對話框。遮罩 + 置中內容卡 + 確認/取消按鈕，支援語意類型與遮罩關閉。

> 契約源：`lh-ui-core/specs/dialog.json` ·
> 黃金用例：`lh-ui-core/cases/dialog.json`（8 條，已翻譯為 xUnit 測試）

## 基礎用法

宣告式可見性：`Visible` 為 false 時不渲染任何內容；確認/取消後觸發 `Close`，
宿主據此置 `Visible` 為 false。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">刪除確認</div>
      <div class="sim-dialog__content">刪除後不可復原，確認刪除這筆記錄嗎？</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">取消</button>
        <button class="sim-btn sim-btn--danger">確認</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var dialog = new LhDialog
{
    Title = "刪除確認",
    Content = "刪除後不可復原，確認刪除這筆記錄嗎？",
    Type = LhDialogType.Danger,
    ConfirmText = "確認",
    CancelText = "取消",
};
dialog.Confirm += () => { /* 執行刪除 */ };
dialog.Close += () => dialog.Visible = false;

dialog.Visible = true;
```

## 語意類型

`Type` 決定確認按鈕的類型（顏色）；取消按鈕固定 default 類型。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog 類型</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">提示</div>
      <div class="sim-dialog__content">這是一則主色確認對話框</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">取消</button>
        <button class="sim-btn sim-btn--primary">確認</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `Confirm` | 確認時觸發 | `event Action` | — |
| `Cancel` | 取消時觸發 | `event Action` | — |
| `Close` | 確認/取消後或（closable 時）點選遮罩觸發，宿主據此置 Visible 為 false | `event Action` | — |
| `Visible` | 是否顯示；為 false 時不渲染任何內容 | `bool` | `false` |
| `Title` | 標題 | `string` | `""` |
| `Content` | 正文內容 | `string` | `""` |
| `Type` | 語意類型，決定確認按鈕的類型（顏色） | `LhDialogType` | `Default` |
| `ConfirmText` | 確認按鈕文字；為空時不顯示確認按鈕 | `string` | `"确认"` |
| `CancelText` | 取消按鈕文字；為空時不顯示取消按鈕 | `string` | `"取消"` |
| `Closable` | 點選遮罩區域是否觸發 Close | `bool` | `false` |

### LhDialogType

| 值 | 確認按鈕類型 |
| --- | --- |
| `Default` | `LhButtonType.Default` |
| `Primary` | `LhButtonType.Primary` |
| `Success` | `LhButtonType.Success` |
| `Warning` | `LhButtonType.Warning` |
| `Danger` | `LhButtonType.Danger` |
| `Info` | `LhButtonType.Info` |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `dialog.width` / `dialog.borderRadius` | 內容卡寬度與圓角 |
| `dialog.paddingHorizontal` / `paddingVertical` | 內容卡內邊距 |
| `dialog.titleFontSize` / `contentFontSize` | 標題與正文字級 |
| `dialog.gap` | 標題/內容/按鈕區間距 |
| `color.background.default` | 內容卡底色 |
| `color.background.overlay` | 遮罩色 |
| `color.text.primary` / `color.text.regular` | 標題/正文色 |
| `zIndex.modal` / `zIndex.modalBackdrop` | 內容卡/遮罩層級 |
| `shadow.base` | 內容卡投影 |

## 黃金用例對照

`cases/dialog.json` 的 8 條斷言與 xUnit 測試的對應關係（`tests/LhDialogTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `confirm-emits-onconfirm` | 確認觸發 onConfirm | `Confirm_Emits_OnConfirm` |
| `cancel-emits-oncancel` | 取消觸發 onCancel | `Cancel_Emits_OnCancel` |
| `confirm-also-emits-onclose` | 確認同時觸發 onClose | `Confirm_Also_Emits_OnClose` |
| `closable-overlay-emits-onclose` | closable 時遮罩點選觸發 onClose | `Closable_Overlay_Emits_OnClose` |
| `not-closable-blocks-overlay-close` | 非 closable 時遮罩不關閉 | `Not_Closable_Blocks_Overlay_Close` |
| `hidden-not-rendered` | 不可見時不渲染任何內容 | `Hidden_Not_Rendered` |
| `title-renders-text` | 標題與正文按字級令牌渲染 | `Title_Renders_Text` |
| `danger-type-uses-danger-token` | danger 類型消費危險色令牌 | `Danger_Type_Uses_Danger_Token` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
