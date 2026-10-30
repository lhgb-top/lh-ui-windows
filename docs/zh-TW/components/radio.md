# Radio 單選鈕

單選鈕。圓環 + 內圓點，點選命中整列；元件本身不含群組邏輯，由宿主維護單選群組。

> 契約源：`lh-ui-core/specs/radio.json` ·
> 黃金用例：`lh-ui-core/cases/radio.json`（6 條，已翻譯為 xUnit 測試）

## 基礎用法

宿主監聽 `Change` 後把同群組其它項的 `Checked` 置為 false，即實現單選群組。

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
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var radio = new LhRadio
{
    Label = "青花瓷",
    Value = "qinghua",
    Checked = true,
};
radio.Change += isChecked => { /* 僅未選取且未停用時觸發；宿主維護單選群組 */ };
```

## 尺寸

三檔尺寸映射到 `radio.size.*` 圓環尺寸令牌；內圓點直徑 = 尺寸 × `radio.dotScale`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio 尺寸</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--small"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">小型</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">中型</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--large"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">大型</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 狀態

`Disabled` 時不回應點選，外環取 `color.disabled.border`、圓點取 `color.disabled.text`；
已選取態再次點選不觸發 `Change`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio 狀態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"></span>
        <span class="sim-radio__label">停用未選取</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">停用已選取</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `Change` | 選取態變化回呼；僅未選取且未停用時觸發（payload 恆為 true） | `event Action<bool>` | — |
| `Checked` | 是否選取 | `bool` | `false` |
| `Disabled` | 是否停用（不回應點選且呈現停用樣式） | `bool` | `false` |
| `Label` | 文字標籤（繪製在圓環右側） | `string` | `""` |
| `Value` | 單選值（群組內識別，元件本身不含群組邏輯） | `string` | `""` |
| `Size` | 圓環尺寸，映射到 `radio.size.*` 令牌 | `LhRadioSize` | `Medium` |

### LhRadioSize

| 值 | 尺寸令牌 |
| --- | --- |
| `Small` | `radio.size.small`（14epx） |
| `Medium` | `radio.size.medium`（16epx） |
| `Large` | `radio.size.large`（18epx） |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `radio.size.{small,medium,large}` | 三檔圓環尺寸 |
| `radio.ringWidth` | 外環描邊寬度 |
| `radio.dotScale` | 內圓點直徑比例（尺寸 × dotScale） |
| `radio.gap` | 圓環與文字間距 |
| `color.primary.default` | 選取態外環與圓點色 |
| `color.border.base` | 未選取外環色 |
| `color.disabled.{border,text}` | 停用態外環/圓點 |
| `color.text.{primary,disabled}` | 標籤文字色 |
| `typography.fontSize.base` / `fontFamily` | 標籤字級與字型棧 |

## 黃金用例對照

`cases/radio.json` 的 6 條斷言與 xUnit 測試的對應關係（`tests/LhRadioTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `tap-unchecked-emits-onchange` | 未選取點選觸發 onChange | `Tap_Unchecked_Emits_OnChange` |
| `tap-checked-keeps-checked` | 已選取再點不觸發 | `Tap_Checked_Keeps_Checked` |
| `disabled-blocks-change` | 停用態不可互動 | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | 選取態消費主色令牌 | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | 未選取態消費描邊令牌 | `Unchecked_Uses_Border_Token` |
| `small-size-uses-small-token` | 小號消費 small 尺寸令牌 | `Small_Size_Uses_Small_Token` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
