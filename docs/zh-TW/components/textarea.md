# Textarea 多行輸入框

多行文字輸入。按 `Rows` 給出初始可見行數，內容超出自動增高，支援字數統計。

> 契約源：`lh-ui-core/specs/textarea.json` ·
> 黃金用例：`lh-ui-core/cases/textarea.json`（6 條，已翻譯為 xUnit 測試）

## 基礎用法

受控元件：宿主監聽 `ValueChange` 並把新值回寫 `Value`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷<br/>粉彩瓷</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field sim-textarea__field--placeholder">請輸入描述</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var textarea = new LhTextarea
{
    Rows = 3,
    Placeholder = "請輸入描述",
};
textarea.ValueChange += value => { /* 受控：回寫 Value */ };
```

## 字數統計

`ShowCount` 為 true 時右下角繪製字數統計；宣告 `Maxlength` 時格式為「目前/最大」，
否則只顯示目前長度。`Maxlength` 超出時截斷新輸入。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>字數統計</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷</div>
        <div class="sim-textarea__count">3/10</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷</div>
        <div class="sim-textarea__count">3</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 狀態

`Disabled` 與 `Readonly` 都不觸發 `ValueChange`，但只有 `Disabled` 呈現停用樣式；
聚焦時描邊取 `color.focus` + `textarea.focusRingWidth`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea 狀態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--disabled">
        <div class="sim-textarea__field">停用</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--focus">
        <div class="sim-textarea__field">聚焦描邊</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `ValueChange` | 值變化回呼（受控）；disabled / readonly 時不觸發 | `event Action<string>` | — |
| `Focus` | 獲得焦點時觸發 | `event Action` | — |
| `Blur` | 失去焦點時觸發 | `event Action` | — |
| `Value` | 當前文字值（受控） | `string` | `""` |
| `Placeholder` | 佔位提示文字，取 `color.text.placeholder` 令牌 | `string` | `""` |
| `Rows` | 初始可見行數；高度隨行數與內容增長 | `int` | `3` |
| `Disabled` | 是否停用（不回應輸入、呈現停用樣式） | `bool` | `false` |
| `Readonly` | 是否唯讀（可聚焦但不接受輸入） | `bool` | `false` |
| `ShowCount` | 是否在右下角顯示字數統計 | `bool` | `false` |
| `Maxlength` | 最大字元數；超出時截斷新輸入，showCount 時作為分母 | `int?` | `null` |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `textarea.paddingVertical` / `paddingHorizontal` | 內邊距 |
| `textarea.fontSize` | 字級 |
| `textarea.borderRadius` / `borderWidth` | 圓角與失焦描邊寬度 |
| `textarea.focusRingWidth` / `color.focus` | 聚焦描邊寬度與顏色 |
| `textarea.countFontSize` / `color.text.secondary` | 字數統計字級/顏色 |
| `textarea.minHeight` | 最小高度（行數推算結果不小於它） |
| `color.background.default` / `color.text.primary` | 容器底/文字色 |
| `color.text.placeholder` | 佔位文字色 |
| `color.border.base` | 失焦描邊色 |
| `color.disabled.{bg,text}` | 停用態底/文字 |
| `typography.lineHeight.primary` | 行高（按 rows 推算高度） |
| `typography.fontFamily` | 國風襯線字型棧 |

## 黃金用例對照

`cases/textarea.json` 的 6 條斷言與 xUnit 測試的對應關係（`tests/LhTextareaTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `input-emits-onvaluechange` | 輸入觸發 onValueChange | `Input_Emits_OnValueChange` |
| `disabled-blocks-input` | 停用態不觸發且呈現停用樣式 | `Disabled_Blocks_Input` |
| `showcount-renders-count` | 「目前/最大」取 countFontSize 令牌 | `ShowCount_Renders_Count` |
| `showcount-without-maxlength-renders-length-only` | 無 maxlength 只顯示長度 | `ShowCount_Without_Maxlength_Renders_Length_Only` |
| `focus-uses-focus-token` | 聚焦描邊取 focus 令牌 | `Focus_Uses_Focus_Token` |
| `maxlength-truncates-input` | 超出 maxlength 截斷 | `Maxlength_Truncates_Input` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
