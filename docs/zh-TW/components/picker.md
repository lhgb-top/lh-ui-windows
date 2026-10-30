# Picker 選擇器

底部彈出的單列滾輪。支援候選項、索引鉗制與工具列。

> 契約源：`lh-ui-core/specs/picker.json` ·
> 黃金用例：`lh-ui-core/cases/picker.json`（8 條，已翻譯為 xUnit 測試）

## 基礎用法

宣告式可見性：`Visible` 為 false 時不渲染任何內容；確認/取消後觸發 `Close`，
宿主據此置 `Visible` 為 false。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Picker 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-picker-overlay"></div>
    <div class="sim-picker">
      <div class="sim-picker__toolbar">
        <span class="sim-picker__cancel">取消</span>
        <span class="sim-picker__title">選擇</span>
        <span class="sim-picker__confirm">確認</span>
      </div>
      <div class="sim-picker__wheel">
        <span class="sim-picker__item">青花瓷</span>
        <span class="sim-picker__item sim-picker__item--active">粉彩瓷</span>
        <span class="sim-picker__item">玲瓏瓷</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var picker = new LhPicker
{
    Options = new[] { "青花瓷", "粉彩瓷", "玲瓏瓷" },
    Value = 1,
    Title = "選擇",
};
picker.ValueChange += index => { /* 滾輪停止後的選中索引 */ };
picker.Confirm += index => { /* 當前選中索引 */ };
picker.Close += () => picker.Visible = false;

picker.Visible = true;
```

## 索引鉗制

`Value` 越界時鉗制到候選項範圍內：負數取 0，大於候選項數 - 1 取最後一項。

```csharp
Assert.Equal(0, LhPickerTokens.ClampIndex(-1, 3));   // 負數取 0
Assert.Equal(2, LhPickerTokens.ClampIndex(100, 3));  // 越界取最後一項
```

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `ValueChange` | 滾輪停止且選中項變化時觸發 | `event Action<int>` | — |
| `Confirm` | 點選確認按鈕時觸發，攜帶當前選中索引 | `event Action<int>` | — |
| `Cancel` | 點選取消按鈕時觸發 | `event Action` | — |
| `Close` | 確認/取消後觸發，宿主據此置 Visible 為 false | `event Action` | — |
| `Visible` | 是否顯示；為 false 時不渲染任何內容 | `bool` | `false` |
| `Options` | 候選項文字陣列（單列） | `string[]?` | `null` |
| `Value` | 選中項索引（0 基）；越界時鉗制到候選項範圍內 | `int` | `0` |
| `Title` | 工具列中間的標題 | `string` | `""` |
| `ConfirmText` | 確認按鈕文字 | `string` | `"确认"` |
| `CancelText` | 取消按鈕文字 | `string` | `"取消"` |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `picker.height` | 面板高度 |
| `picker.toolbarHeight` | 頂部工具列高度（滾輪區高度 = 面板高度 - 工具列高度） |
| `picker.itemHeight` | 候選項高度（中央指示器間距同此） |
| `picker.itemFontSize` | 候選項與工具列字級 |
| `picker.borderRadius` | 面板頂部圓角 |
| `color.background.default` | 面板底色 |
| `color.background.overlay` | 遮罩色 |
| `color.text.primary` | 標題與候選項色 |
| `color.text.secondary` | 取消按鈕色 |
| `color.primary.default` | 確認按鈕色 |
| `color.border.lighter` | 中央指示器線色 |
| `zIndex.modal` / `zIndex.modalBackdrop` | 面板/遮罩層級 |

## 黃金用例對照

`cases/picker.json` 的 8 條斷言與 xUnit 測試的對應關係（`tests/LhPickerTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `confirm-emits-onconfirm-with-value` | 確認攜帶當前選中索引 | `Confirm_Emits_OnConfirm_With_Value` |
| `confirm-also-emits-onclose` | 確認同時觸發 onClose | `Confirm_Also_Emits_OnClose` |
| `cancel-emits-oncancel` | 取消觸發 onCancel | `Cancel_Emits_OnCancel` |
| `scroll-emits-onvaluechange` | 滾輪停止觸發 onValueChange | `Scroll_Emits_OnValueChange` |
| `clamp-negative-value-to-zero` | 負數索引鉗制到 0 | `Clamp_Negative_Value_To_Zero` |
| `clamp-overflow-value-to-last` | 越界索引鉗制到最後一項 | `Clamp_Overflow_Value_To_Last` |
| `title-renders-text` | 標題按字級令牌渲染 | `Title_Renders_Text` |
| `hidden-not-rendered` | 不可見時不渲染任何內容 | `Hidden_Not_Rendered` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
