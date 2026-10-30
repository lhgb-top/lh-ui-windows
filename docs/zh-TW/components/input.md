# Input 輸入框

單行文字輸入。支援類型、尺寸、停用、唯讀、清除與最大字元數。

> 契約源：`lh-ui-core/specs/input.json` ·
> 黃金用例：`lh-ui-core/cases/input.json`（8 條，已翻譯為 xUnit 測試）

## 基礎用法

受控元件：宿主監聽 `ValueChange` 並把新值回寫 `Value`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field sim-input__field--placeholder">請輸入關鍵字</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var input = new LhInput
{
    Placeholder = "請輸入關鍵字",
};
input.ValueChange += value => { /* 受控：回寫 Value */ };
```

## 尺寸

三檔尺寸映射到 `input.height` / `paddingHorizontal` / `fontSize` 令牌。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input 尺寸</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input sim-input--small"><div class="sim-input__field">小型</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">中型</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--large"><div class="sim-input__field">大型</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 類型與狀態

`Password` 以圓點遮罩顯示；`Disabled` 與 `Readonly` 都不觸發 `ValueChange`，
但只有 `Disabled` 呈現停用樣式；聚焦時描邊取 `color.focus` + `input.focusRingWidth`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input 狀態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">●●●●●●●●</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--disabled"><div class="sim-input__field">停用</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--focus"><div class="sim-input__field">聚焦描邊</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 清除與字數

`Clearable` 為 true 且值非空時顯示清除按鈕，點選觸發 `ValueChange("")`；
`Maxlength` 超出時截斷新輸入。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>清除與字數</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷</div>
        <span class="sim-input__clear">✕</span>
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
| `Type` | 輸入類型；password 時以圓點遮罩顯示 | `LhInputType` | `Text` |
| `Size` | 輸入框尺寸，映射到尺寸令牌 | `LhInputSize` | `Medium` |
| `Disabled` | 是否停用（不回應輸入、呈現停用樣式） | `bool` | `false` |
| `Readonly` | 是否唯讀（可聚焦但不接受輸入） | `bool` | `false` |
| `Clearable` | 是否顯示清除按鈕 | `bool` | `false` |
| `Maxlength` | 最大字元數；超出時截斷新輸入 | `int?` | `null` |

### LhInputSize

| 值 | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `input.height.small`（32epx） | `input.paddingHorizontal.small`（12epx） | `input.fontSize.small`（13） |
| `Medium` | `input.height.medium`（40epx） | `input.paddingHorizontal.medium`（16epx） | `input.fontSize.medium`（14） |
| `Large` | `input.height.large`（48epx） | `input.paddingHorizontal.large`（20epx） | `input.fontSize.large`（16） |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `input.height.{small,medium,large}` | 三檔高度 |
| `input.paddingHorizontal.{small,medium,large}` | 三檔橫向內邊距 |
| `input.fontSize.{small,medium,large}` | 三檔字級 |
| `input.borderRadius` / `input.borderWidth` | 圓角與失焦描邊寬度 |
| `input.focusRingWidth` / `color.focus` | 聚焦描邊寬度與顏色 |
| `input.clearIconSize` | 清除按鈕圖示尺寸 |
| `color.background.default` / `color.text.primary` | 容器底/文字色 |
| `color.text.placeholder` | 佔位文字色 |
| `color.border.base` | 失焦描邊色 |
| `color.disabled.{bg,text}` | 停用態底/文字 |
| `typography.fontFamily` | 國風襯線字型棧 |

## 黃金用例對照

`cases/input.json` 的 8 條斷言與 xUnit 測試的對應關係（`tests/LhInputTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `input-emits-onvaluechange` | 輸入觸發 onValueChange | `Input_Emits_OnValueChange` |
| `clearable-emits-empty-value` | 清除觸發 onValueChange("") | `Clearable_Emits_Empty_Value` |
| `disabled-blocks-input` | 停用態不觸發且呈現停用樣式 | `Disabled_Blocks_Input` |
| `readonly-blocks-input` | 唯讀不觸發但不呈現停用樣式 | `Readonly_Blocks_Input` |
| `placeholder-uses-placeholder-token` | 佔位文字取佔位令牌 | `Placeholder_Uses_Placeholder_Token` |
| `focus-uses-focus-token` | 聚焦描邊取 focus 令牌 | `Focus_Uses_Focus_Token` |
| `small-size-uses-small-tokens` | 小號消費 small 尺寸令牌 | `Small_Size_Uses_Small_Tokens` |
| `maxlength-truncates-input` | 超出 maxlength 截斷 | `Maxlength_Truncates_Input` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
