# Input 输入框

单行文本输入。支持类型、尺寸、禁用、只读、清除与最大字符数。

> 契约源：`lh-ui-core/specs/input.json` ·
> 黄金用例：`lh-ui-core/cases/input.json`（8 条，已翻译为 xUnit 测试）

## 基础用法

受控组件：宿主监听 `ValueChange` 并把新值回写 `Value`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input 基础</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field sim-input__field--placeholder">请输入关键字</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var input = new LhInput
{
    Placeholder = "请输入关键字",
};
input.ValueChange += value => { /* 受控：回写 Value */ };
```

## 尺寸

三档尺寸映射到 `input.height` / `paddingHorizontal` / `fontSize` 令牌。

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
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 类型与状态

`Password` 以圆点掩码显示；`Disabled` 与 `Readonly` 都不触发 `ValueChange`，
但只有 `Disabled` 呈现禁用样式；聚焦时描边取 `color.focus` + `input.focusRingWidth`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input 状态</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">●●●●●●●●</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--disabled"><div class="sim-input__field">禁用</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--focus"><div class="sim-input__field">聚焦描边</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 清除与字数

`Clearable` 为 true 且值非空时显示清除按钮，点击触发 `ValueChange("")`；
`Maxlength` 超出时截断新输入。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>清除与字数</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷</div>
        <span class="sim-input__clear">✕</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `ValueChange` | 值变化回调（受控）；disabled / readonly 时不触发 | `event Action<string>` | — |
| `Focus` | 获得焦点时触发 | `event Action` | — |
| `Blur` | 失去焦点时触发 | `event Action` | — |
| `Value` | 当前文本值（受控） | `string` | `""` |
| `Placeholder` | 占位提示文字，取 `color.text.placeholder` 令牌 | `string` | `""` |
| `Type` | 输入类型；password 时以圆点掩码显示 | `LhInputType` | `Text` |
| `Size` | 输入框尺寸，映射到尺寸令牌 | `LhInputSize` | `Medium` |
| `Disabled` | 是否禁用（不响应输入、呈现禁用样式） | `bool` | `false` |
| `Readonly` | 是否只读（可聚焦但不接受输入） | `bool` | `false` |
| `Clearable` | 是否显示清除按钮 | `bool` | `false` |
| `Maxlength` | 最大字符数；超出时截断新输入 | `int?` | `null` |

### LhInputSize

| 值 | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `input.height.small`（32epx） | `input.paddingHorizontal.small`（12epx） | `input.fontSize.small`（13） |
| `Medium` | `input.height.medium`（40epx） | `input.paddingHorizontal.medium`（16epx） | `input.fontSize.medium`（14） |
| `Large` | `input.height.large`（48epx） | `input.paddingHorizontal.large`（20epx） | `input.fontSize.large`（16） |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `input.height.{small,medium,large}` | 三档高度 |
| `input.paddingHorizontal.{small,medium,large}` | 三档横向内边距 |
| `input.fontSize.{small,medium,large}` | 三档字号 |
| `input.borderRadius` / `input.borderWidth` | 圆角与失焦描边宽度 |
| `input.focusRingWidth` / `color.focus` | 聚焦描边宽度与颜色 |
| `input.clearIconSize` | 清除按钮图标尺寸 |
| `color.background.default` / `color.text.primary` | 容器底/文字色 |
| `color.text.placeholder` | 占位文字色 |
| `color.border.base` | 失焦描边色 |
| `color.disabled.{bg,text}` | 禁用态底/文字 |
| `typography.fontFamily` | 国风衬线字体栈 |

## 黄金用例对照

`cases/input.json` 的 8 条断言与 xUnit 测试的对应关系（`tests/LhInputTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `input-emits-onvaluechange` | 输入触发 onValueChange | `Input_Emits_OnValueChange` |
| `clearable-emits-empty-value` | 清除触发 onValueChange("") | `Clearable_Emits_Empty_Value` |
| `disabled-blocks-input` | 禁用态不触发且呈现禁用样式 | `Disabled_Blocks_Input` |
| `readonly-blocks-input` | 只读不触发但不呈现禁用样式 | `Readonly_Blocks_Input` |
| `placeholder-uses-placeholder-token` | 占位文字取占位令牌 | `Placeholder_Uses_Placeholder_Token` |
| `focus-uses-focus-token` | 聚焦描边取 focus 令牌 | `Focus_Uses_Focus_Token` |
| `small-size-uses-small-tokens` | 小号消费 small 尺寸令牌 | `Small_Size_Uses_Small_Tokens` |
| `maxlength-truncates-input` | 超出 maxlength 截断 | `Maxlength_Truncates_Input` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
