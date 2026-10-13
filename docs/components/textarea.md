# Textarea 多行输入框

多行文本输入。按 `Rows` 给出初始可见行数，内容超出自动增高，支持字数统计。

> 契约源：`lh-ui-core/specs/textarea.json` ·
> 黄金用例：`lh-ui-core/cases/textarea.json`（6 条，已翻译为 xUnit 测试）

## 基础用法

受控组件：宿主监听 `ValueChange` 并把新值回写 `Value`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea 基础</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷<br/>粉彩瓷</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field sim-textarea__field--placeholder">请输入描述</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var textarea = new LhTextarea
{
    Rows = 3,
    Placeholder = "请输入描述",
};
textarea.ValueChange += value => { /* 受控：回写 Value */ };
```

## 字数统计

`ShowCount` 为 true 时右下角绘制字数统计；声明 `Maxlength` 时格式为「当前/最大」，
否则只显示当前长度。`Maxlength` 超出时截断新输入。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>字数统计</span></div>
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
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 状态

`Disabled` 与 `Readonly` 都不触发 `ValueChange`，但只有 `Disabled` 呈现禁用样式；
聚焦时描边取 `color.focus` + `textarea.focusRingWidth`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea 状态</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--disabled">
        <div class="sim-textarea__field">禁用</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--focus">
        <div class="sim-textarea__field">聚焦描边</div>
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
| `Rows` | 初始可见行数；高度随行数与内容增长 | `int` | `3` |
| `Disabled` | 是否禁用（不响应输入、呈现禁用样式） | `bool` | `false` |
| `Readonly` | 是否只读（可聚焦但不接受输入） | `bool` | `false` |
| `ShowCount` | 是否在右下角显示字数统计 | `bool` | `false` |
| `Maxlength` | 最大字符数；超出时截断新输入，showCount 时作为分母 | `int?` | `null` |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `textarea.paddingVertical` / `paddingHorizontal` | 内边距 |
| `textarea.fontSize` | 字号 |
| `textarea.borderRadius` / `borderWidth` | 圆角与失焦描边宽度 |
| `textarea.focusRingWidth` / `color.focus` | 聚焦描边宽度与颜色 |
| `textarea.countFontSize` / `color.text.secondary` | 字数统计字号/颜色 |
| `textarea.minHeight` | 最小高度（行数推算结果不小于它） |
| `color.background.default` / `color.text.primary` | 容器底/文字色 |
| `color.text.placeholder` | 占位文字色 |
| `color.border.base` | 失焦描边色 |
| `color.disabled.{bg,text}` | 禁用态底/文字 |
| `typography.lineHeight.primary` | 行高（按 rows 推算高度） |
| `typography.fontFamily` | 国风衬线字体栈 |

## 黄金用例对照

`cases/textarea.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhTextareaTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `input-emits-onvaluechange` | 输入触发 onValueChange | `Input_Emits_OnValueChange` |
| `disabled-blocks-input` | 禁用态不触发且呈现禁用样式 | `Disabled_Blocks_Input` |
| `showcount-renders-count` | 「当前/最大」取 countFontSize 令牌 | `ShowCount_Renders_Count` |
| `showcount-without-maxlength-renders-length-only` | 无 maxlength 只显示长度 | `ShowCount_Without_Maxlength_Renders_Length_Only` |
| `focus-uses-focus-token` | 聚焦描边取 focus 令牌 | `Focus_Uses_Focus_Token` |
| `maxlength-truncates-input` | 超出 maxlength 截断 | `Maxlength_Truncates_Input` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
