# Checkbox 复选框

复选框。选中画勾、不确定画横杠，点击命中整行（方框 + 标签区域）。

> 契约源：`lh-ui-core/specs/checkbox.json` ·
> 黄金用例：`lh-ui-core/cases/checkbox.json`（6 条，已翻译为 xUnit 测试）

## 基础用法

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox 基础</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box"></span>
        <span class="sim-checkbox__label">未选中</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">选中</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">─</span>
        <span class="sim-checkbox__label">不确定</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var checkbox = new LhCheckbox
{
    Label = "青花瓷",
    Checked = true,
};
checkbox.Change += isChecked => { };
```

## 尺寸

三档尺寸映射到 `checkbox.size.*` 方框尺寸令牌。

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
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 状态

`Disabled` 时不响应点击，底/边/指示符/文字全部取 `color.disabled.*` 令牌；
`Indeterminate` 为不确定态（横杠），点击后变为选中。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox 状态</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked sim-checkbox__box--disabled">✓</span>
        <span class="sim-checkbox__label">禁用选中</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--disabled"></span>
        <span class="sim-checkbox__label">禁用未选中</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `Change` | 选中态变化回调；disabled 时不触发 | `event Action<bool>` | — |
| `Checked` | 是否选中 | `bool` | `false` |
| `Indeterminate` | 是否为不确定态（横杠指示）；点击后变为选中 | `bool` | `false` |
| `Disabled` | 是否禁用（不响应点击且呈现禁用样式） | `bool` | `false` |
| `Label` | 文字标签（绘制在方框右侧） | `string` | `""` |
| `Size` | 方框尺寸，映射到 `checkbox.size.*` 令牌 | `LhCheckboxSize` | `Medium` |

### LhCheckboxSize

| 值 | 尺寸令牌 |
| --- | --- |
| `Small` | `checkbox.size.small`（14epx） |
| `Medium` | `checkbox.size.medium`（16epx） |
| `Large` | `checkbox.size.large`（18epx） |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `checkbox.size.{small,medium,large}` | 三档方框尺寸 |
| `checkbox.borderRadius` / `checkbox.borderWidth` | 方框圆角与描边宽度 |
| `checkbox.checkWidth` | 勾形/横杠描边宽度 |
| `checkbox.gap` | 方框与文字间距 |
| `color.primary.default` / `color.onPrimary` | 选中/不确定态底色与指示符色 |
| `color.border.base` | 未选中描边色 |
| `color.disabled.{bg,border,text}` | 禁用态底/边/指示符 |
| `color.text.{primary,disabled}` | 标签文字色 |
| `typography.fontSize.base` / `fontFamily` | 标签字号与字体栈 |

## 黄金用例对照

`cases/checkbox.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhCheckboxTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | 未选中点击后选中 | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-unchecks` | 选中点击后取消选中 | `Tap_Checked_Unchecks` |
| `disabled-blocks-change` | 禁用态不可交互 | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | 选中态消费主色令牌 | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | 未选中态消费描边令牌 | `Unchecked_Uses_Border_Token` |
| `indeterminate-becomes-checked` | 不确定态点击后选中 | `Indeterminate_Becomes_Checked` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
