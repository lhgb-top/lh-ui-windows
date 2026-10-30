# Checkbox 核取方塊

核取方塊。選取時畫勾、不確定態畫橫槓，點選命中整列（方框 + 標籤區域）。

> 契約源：`lh-ui-core/specs/checkbox.json` ·
> 黃金用例：`lh-ui-core/cases/checkbox.json`（6 條，已翻譯為 xUnit 測試）

## 基礎用法

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box"></span>
        <span class="sim-checkbox__label">未選取</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">已選取</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">─</span>
        <span class="sim-checkbox__label">不確定</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var checkbox = new LhCheckbox
{
    Label = "青花瓷",
    Checked = true,
};
checkbox.Change += isChecked => { };
```

## 尺寸

三檔尺寸映射到 `checkbox.size.*` 方框尺寸令牌。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox 尺寸</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--small sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">小型</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">中型</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--large sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">大型</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 狀態

`Disabled` 時不回應點選，底/邊/指示符/文字全部取 `color.disabled.*` 令牌；
`Indeterminate` 為不確定態（橫槓），點選後變為選取。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox 狀態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked sim-checkbox__box--disabled">✓</span>
        <span class="sim-checkbox__label">停用已選取</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--disabled"></span>
        <span class="sim-checkbox__label">停用未選取</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `Change` | 選取態變化回呼；disabled 時不觸發 | `event Action<bool>` | — |
| `Checked` | 是否選取 | `bool` | `false` |
| `Indeterminate` | 是否為不確定態（橫槓指示）；點選後變為選取 | `bool` | `false` |
| `Disabled` | 是否停用（不回應點選且呈現停用樣式） | `bool` | `false` |
| `Label` | 文字標籤（繪製在方框右側） | `string` | `""` |
| `Size` | 方框尺寸，映射到 `checkbox.size.*` 令牌 | `LhCheckboxSize` | `Medium` |

### LhCheckboxSize

| 值 | 尺寸令牌 |
| --- | --- |
| `Small` | `checkbox.size.small`（14epx） |
| `Medium` | `checkbox.size.medium`（16epx） |
| `Large` | `checkbox.size.large`（18epx） |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `checkbox.size.{small,medium,large}` | 三檔方框尺寸 |
| `checkbox.borderRadius` / `checkbox.borderWidth` | 方框圓角與描邊寬度 |
| `checkbox.checkWidth` | 勾形/橫槓描邊寬度 |
| `checkbox.gap` | 方框與文字間距 |
| `color.primary.default` / `color.onPrimary` | 選取/不確定態底色與指示符色 |
| `color.border.base` | 未選取描邊色 |
| `color.disabled.{bg,border,text}` | 停用態底/邊/指示符 |
| `color.text.{primary,disabled}` | 標籤文字色 |
| `typography.fontSize.base` / `fontFamily` | 標籤字級與字型棧 |

## 黃金用例對照

`cases/checkbox.json` 的 6 條斷言與 xUnit 測試的對應關係（`tests/LhCheckboxTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | 未選取點選後選取 | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-unchecks` | 選取後點選取消選取 | `Tap_Checked_Unchecks` |
| `disabled-blocks-change` | 停用態不可互動 | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | 選取態消費主色令牌 | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | 未選取態消費描邊令牌 | `Unchecked_Uses_Border_Token` |
| `indeterminate-becomes-checked` | 不確定態點選後選取 | `Indeterminate_Becomes_Checked` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
