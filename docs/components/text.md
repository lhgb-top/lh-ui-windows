# Text 文本

文本组件。按级别、字重、色调与对齐渲染单段文字，全部排版值取自 typography 令牌。

> 契约源：`lh-ui-core/specs/text.json` ·
> 黄金用例：`lh-ui-core/cases/text.json`（5 条，已翻译为 xUnit 测试）

## 级别

六档级别映射到 `typography.fontSize.*` 令牌；默认级别为 `base`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Text 级别</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--extra-large">青花瓷 extraLarge</div>
    <div class="sim-text sim-text--large">青花瓷 large</div>
    <div class="sim-text sim-text--medium">青花瓷 medium</div>
    <div class="sim-text">青花瓷 base</div>
    <div class="sim-text sim-text--small">青花瓷 small</div>
    <div class="sim-text sim-text--extra-small">青花瓷 extraSmall</div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var text = new LhText
{
    Text = "青花瓷",
    Level = LhTextLevel.Large,
};
```

## 字重与色调

`Weight` 映射到 `typography.fontWeight.*` 令牌；`Tone` 映射到 `color.text.*`（selected 取 `color.selected`）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>字重与色调</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--bold">加粗 bold</div>
    <div class="sim-text sim-text--secondary">次要说明 secondary</div>
    <div class="sim-text sim-text--placeholder">占位文字 placeholder</div>
    <div class="sim-text sim-text--selected">选中态 selected</div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 对齐

`Align` 决定文字在行盒宽度内的水平定位（非设计令牌，映射到 WinUI 的 `TextAlignment`）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>对齐</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--start">左对齐 start</div>
    <div class="sim-text sim-text--center">居中 center</div>
    <div class="sim-text sim-text--end">右对齐 end</div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 明暗主题

切换站点右上角主题，模拟器手机框内的文字色即变；真机通过 `LhTheme.Apply(true)` 切换。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>暗色文本</span></div>
  <div class="sim-screen">
    <div class="sim-text">常规文字 regular</div>
    <div class="sim-text sim-text--secondary">次要说明 secondary</div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染；明/暗值全部取自 core 令牌快照</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `Text` | 文字内容（spec 的 default 插槽） | `string` | `""` |
| `Level` | 字号级别，映射到 `typography.fontSize.*` | `LhTextLevel` | `Base` |
| `Weight` | 字重，映射到 `typography.fontWeight.*` | `LhTextWeight` | `Primary` |
| `Tone` | 文字色调，映射到 `color.text.*` / `color.selected` | `LhTextTone` | `Regular` |
| `Align` | 水平对齐（映射到 `TextAlignment`） | `LhTextAlign` | `Start` |

### LhTextLevel

| 值 | 字号令牌 |
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
| `Primary` | `typography.fontWeight.primary`（400，端侧映射 Normal） |
| `Bold` | `typography.fontWeight.bold`（600，端侧映射 SemiBold） |

### LhTextTone

| 值 | 颜色令牌 |
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

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `typography.fontSize.{extraLarge…extraSmall}` | 六档字号 |
| `typography.fontWeight.{primary,bold}` | 两档字重 |
| `typography.lineHeight.primary` | 行高（行盒高度 = 字号 × 行高） |
| `typography.fontFamily` | 国风衬线字体栈 |
| `color.text.{primary,regular,secondary,placeholder,disabled}` | 中性色调 |
| `color.selected` | 选中态色调 |

## 黄金用例对照

`cases/text.json` 的 5 条断言与 xUnit 测试的对应关系（`tests/LhTextTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `default-level-uses-base-tokens` | 默认级别消费 base 排版令牌 | `Default_Level_Uses_Base_Tokens` |
| `large-level-uses-large-token` | 大级别消费 large 字号令牌 | `Large_Level_Uses_Large_Token` |
| `bold-weight-uses-bold-token` | 粗体消费 bold 字重令牌 | `Bold_Weight_Uses_Bold_Token` |
| `secondary-tone-uses-text-secondary` | 次要色调取次要文字色令牌 | `Secondary_Tone_Uses_Text_Secondary` |
| `render-emits-single-text-command` | 有且仅有一条 TEXT 指令 | `Render_Emits_Single_Text_Command` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
