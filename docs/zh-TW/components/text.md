# Text 文字

文字元件。按級別、字重、色調與對齊渲染單段文字，全部排版值取自 typography 令牌。

> 契約源：`lh-ui-core/specs/text.json` ·
> 黃金用例：`lh-ui-core/cases/text.json`（5 條，已翻譯為 xUnit 測試）

## 級別

六檔級別映射到 `typography.fontSize.*` 令牌；預設級別為 `base`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Text 級別</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--extra-large">青花瓷 extraLarge</div>
    <div class="sim-text sim-text--large">青花瓷 large</div>
    <div class="sim-text sim-text--medium">青花瓷 medium</div>
    <div class="sim-text">青花瓷 base</div>
    <div class="sim-text sim-text--small">青花瓷 small</div>
    <div class="sim-text sim-text--extra-small">青花瓷 extraSmall</div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var text = new LhText
{
    Text = "青花瓷",
    Level = LhTextLevel.Large,
};
```

## 字重與色調

`Weight` 映射到 `typography.fontWeight.*` 令牌；`Tone` 映射到 `color.text.*`（selected 取 `color.selected`）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>字重與色調</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--bold">加粗 bold</div>
    <div class="sim-text sim-text--secondary">次要說明 secondary</div>
    <div class="sim-text sim-text--placeholder">佔位文字 placeholder</div>
    <div class="sim-text sim-text--selected">選取態 selected</div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 對齊

`Align` 決定文字在行盒寬度內的水平定位（非設計令牌，映射到 WinUI 的 `TextAlignment`）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>對齊</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--start">左對齊 start</div>
    <div class="sim-text sim-text--center">置中 center</div>
    <div class="sim-text sim-text--end">右對齊 end</div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 明暗主題

切換站台右上角主題，模擬器手機框內的文字色即變；真機透過 `LhTheme.Apply(true)` 切換。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>暗色文字</span></div>
  <div class="sim-screen">
    <div class="sim-text">常規文字 regular</div>
    <div class="sim-text sim-text--secondary">次要說明 secondary</div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染；明/暗值全部取自 core 令牌快照</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `Text` | 文字內容（spec 的 default 插槽） | `string` | `""` |
| `Level` | 字級級別，映射到 `typography.fontSize.*` | `LhTextLevel` | `Base` |
| `Weight` | 字重，映射到 `typography.fontWeight.*` | `LhTextWeight` | `Primary` |
| `Tone` | 文字色調，映射到 `color.text.*` / `color.selected` | `LhTextTone` | `Regular` |
| `Align` | 水平對齊（映射到 `TextAlignment`） | `LhTextAlign` | `Start` |

### LhTextLevel

| 值 | 字級令牌 |
| --- | --- |
| `ExtraLarge` | `typography.fontSize.extraLarge`（20epx） |
| `Large` | `typography.fontSize.large`（18epx） |
| `Medium` | `typography.fontSize.medium`（16epx） |
| `Base` | `typography.fontSize.base`（14epx） |
| `Small` | `typography.fontSize.small`（13epx） |
| `ExtraSmall` | `typography.fontSize.extraSmall`（12epx） |

### LhTextWeight

| 值 | 字重令牌 |
| --- | --- |
| `Primary` | `typography.fontWeight.primary`（400，端側映射 Normal） |
| `Bold` | `typography.fontWeight.bold`（600，端側映射 SemiBold） |

### LhTextTone

| 值 | 顏色令牌 |
| --- | --- |
| `Primary` | `color.text.primary` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |
| `Placeholder` | `color.text.placeholder` |
| `Disabled` | `color.text.disabled` |
| `Selected` | `color.selected` |

### LhTextAlign

| 值 | WinUI 映射 |
| --- | --- |
| `Start` | `TextAlignment.Left` |
| `Center` | `TextAlignment.Center` |
| `End` | `TextAlignment.Right` |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `typography.fontSize.{extraLarge…extraSmall}` | 六檔字級 |
| `typography.fontWeight.{primary,bold}` | 兩檔字重 |
| `typography.lineHeight.primary` | 行高（行盒高度 = 字級 × 行高） |
| `typography.fontFamily` | 國風襯線字型棧 |
| `color.text.{primary,regular,secondary,placeholder,disabled}` | 中性色調 |
| `color.selected` | 選取態色調 |

## 黃金用例對照

`cases/text.json` 的 5 條斷言與 xUnit 測試的對應關係（`tests/LhTextTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `default-level-uses-base-tokens` | 預設級別消費 base 排版令牌 | `Default_Level_Uses_Base_Tokens` |
| `large-level-uses-large-token` | 大級別消費 large 字級令牌 | `Large_Level_Uses_Large_Token` |
| `bold-weight-uses-bold-token` | 粗體消費 bold 字重令牌 | `Bold_Weight_Uses_Bold_Token` |
| `secondary-tone-uses-text-secondary` | 次要色調取次要文字色令牌 | `Secondary_Tone_Uses_Text_Secondary` |
| `render-emits-single-text-command` | 有且僅有一條 TEXT 指令 | `Render_Emits_Single_Text_Command` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
