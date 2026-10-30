# Button 按鈕

基礎操作按鈕。觸發即時操作，支援類型、尺寸、載入、停用、圖示與國風花紋底。

> 契約源：`lh-ui-core/specs/button.json` ·
> 黃金用例：`lh-ui-core/cases/button.json`（8 條，已翻譯為 xUnit 測試）

## 類型

六種類型決定底色與文字色。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button 類型</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">預設按鈕</button>
      <button class="sim-btn sim-btn--primary">主要按鈕</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--success">成功按鈕</button>
      <button class="sim-btn sim-btn--warning">警告按鈕</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--danger">危險按鈕</button>
      <button class="sim-btn sim-btn--info">資訊按鈕</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var button = new LhButton
{
    Type = LhButtonType.Primary,
    Text = "主要按鈕",
};
button.Click += OnButtonClick;
```

## 尺寸

三檔尺寸映射到 `button.height` / `paddingHorizontal` / `fontSize` / `iconSize` 令牌。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button 尺寸</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">小型按鈕</button>
      <button class="sim-btn">中型按鈕</button>
      <button class="sim-btn sim-btn--large">大型按鈕</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 狀態

停用與載入中都不觸發 `Click`；兩者共存時按**停用優先**處理（黃金用例 `disabled-precedes-loading`）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button 狀態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">正常</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">停用</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>載入中
      </button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">停用優先</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 圖示與花紋底

圖示名按本機圖示體系解析（`LhButtonTokens.IconFor`，當前支援 search / refresh / check / close，
未登記名不繪製）。花紋底消費 `color.decoration` 令牌，45° 斜紋疊加。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>圖示與花紋</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>搜尋</button>
      <button class="sim-btn"><span class="sim-btn__icon"></span>預設</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--pattern">主要花紋底</button>
      <button class="sim-btn sim-btn--pattern">預設花紋底</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 明暗主題

切換站台右上角主題，模擬器手機框內的按鈕顏色即變；真機透過 `LhTheme.Apply(true)` 切換。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>暗色按鈕</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">主要按鈕</button>
      <button class="sim-btn">預設按鈕</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--disabled">停用</button>
      <button class="sim-btn sim-btn--success">成功</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染；明/暗值全部取自 core 令牌快照</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `Click` | 點選事件；`Disabled` 或 `Loading` 為 true 時不觸發 | `event Action` | — |
| `Type` | 按鈕類型，決定底色與文字色 | `LhButtonType` | `Default` |
| `Size` | 按鈕尺寸，映射到尺寸令牌 | `LhButtonSize` | `Medium` |
| `Disabled` | 是否停用 | `bool` | `false` |
| `Loading` | 是否載入中（顯示指示器，不觸發 Click） | `bool` | `false` |
| `Icon` | 圖示名（本機圖示體系解析） | `string?` | `null` |
| `BgPattern` | 是否繪製國風花紋底紋 | `bool` | `false` |
| `Text` | 按鈕文字內容（spec 的 default 插槽） | `string` | `""` |

### LhButtonType

| 值 | 底色 | 文字色 |
| --- | --- | --- |
| `Default` | `color.background.default` | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.onPrimary` |
| `Success` | `color.success.default` | `color.onPrimary` |
| `Warning` | `color.warning.default` | `color.onPrimary` |
| `Danger` | `color.danger.default` | `color.onPrimary` |
| `Info` | `color.info.default` | `color.onPrimary` |

### LhButtonSize

| 值 | height | paddingHorizontal | fontSize | iconSize |
| --- | --- | --- | --- | --- |
| `Small` | `button.height.small`（32epx） | `button.paddingHorizontal.small`（16epx） | `button.fontSize.small`（13） | `button.iconSize.small`（16） |
| `Medium` | `button.height.medium`（40epx） | `button.paddingHorizontal.medium`（20epx） | `button.fontSize.medium`（14） | `button.iconSize.medium`（18） |
| `Large` | `button.height.large`（48epx） | `button.paddingHorizontal.large`（24epx） | `button.fontSize.large`（16） | `button.iconSize.large`（20） |

停用態覆蓋：底色 `color.disabled.bg`、文字 `color.disabled.text`、描邊 `color.disabled.border`。

## 令牌對照

按鈕消費的令牌（與 `specs/button.json` 的 `tokenRefs` 逐條對齊）：

| 令牌 | 用途 |
| --- | --- |
| `button.height.{small,medium,large}` | 三檔高度 |
| `button.paddingHorizontal.{small,medium,large}` | 三檔橫向內邊距 |
| `button.fontSize.{small,medium,large}` | 三檔字級 |
| `button.iconSize.{small,medium,large}` | 三檔圖示尺寸 |
| `button.borderRadius` / `button.borderWidth` | 圓角（跟隨 `radius.base`）與描邊寬度 |
| `button.fontWeight` | 字重（跟隨 `typography.fontWeight.primary`，端側映射 FontWeights） |
| `color.primary.default` / `color.onPrimary` | 主色類型底/文字 |
| `color.{success,warning,danger,info}.default` | 語意類型底色 |
| `color.background.default` / `color.text.primary` / `color.border.base` | 預設類型底/文字/描邊 |
| `color.disabled.{bg,text,border}` | 停用態三色 |
| `color.decoration` | 花紋底紋色 |
| `spacing.xs` | 圖示與文字間距 |
| `typography.fontFamily` | 國風襯線字型棧 |

## 黃金用例對照

`cases/button.json` 的 8 條斷言與 xUnit 測試的對應關係（`tests/LhButtonTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `click-emits-onclick` | gate 放行（可互動） | `Enabled_Button_Is_Interactive` |
| `disabled-blocks-click` | 停用態不可互動 | `Disabled_Is_Not_Interactive` |
| `loading-blocks-click` | 載入態不可互動 | `Loading_Is_Not_Interactive` |
| `disabled-precedes-loading` | 停用優先 + 停用樣式 | `Disabled_Precedes_Loading` |
| `default-type-renders-text` | 預設類型中性底/文字 | `Default_Type_Uses_Neutral_Tokens` |
| `primary-type-uses-primary-tokens` | 主色類型消費主色令牌 | `Primary_Type_Uses_Primary_Tokens` |
| `small-size-uses-small-tokens` | 小號消費 small 尺寸令牌 | `Small_Size_Uses_Small_Tokens` |
| `bgpattern-uses-decoration-token` | 花紋底消費裝飾色 | `BgPattern_Uses_Decoration_Token` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
