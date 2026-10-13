# Radio 单选框

单选框。圆环 + 内圆点，点击命中整行；组件本身不含组逻辑，由宿主维护单选组。

> 契约源：`lh-ui-core/specs/radio.json` ·
> 黄金用例：`lh-ui-core/cases/radio.json`（6 条，已翻译为 xUnit 测试）

## 基础用法

宿主监听 `Change` 后把同组其它项的 `Checked` 置为 false，即实现单选组。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio 基础</span></div>
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
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var radio = new LhRadio
{
    Label = "青花瓷",
    Value = "qinghua",
    Checked = true,
};
radio.Change += isChecked => { /* 仅未选中且未禁用时触发；宿主维护单选组 */ };
```

## 尺寸

三档尺寸映射到 `radio.size.*` 圆环尺寸令牌；内圆点直径 = 尺寸 × `radio.dotScale`。

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
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 状态

`Disabled` 时不响应点击，外环取 `color.disabled.border`、圆点取 `color.disabled.text`；
已选中态再次点击不触发 `Change`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio 状态</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"></span>
        <span class="sim-radio__label">禁用未选中</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">禁用选中</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `Change` | 选中态变化回调；仅未选中且未禁用时触发（payload 恒为 true） | `event Action<bool>` | — |
| `Checked` | 是否选中 | `bool` | `false` |
| `Disabled` | 是否禁用（不响应点击且呈现禁用样式） | `bool` | `false` |
| `Label` | 文字标签（绘制在圆环右侧） | `string` | `""` |
| `Value` | 单选值（组内标识，组件本身不含组逻辑） | `string` | `""` |
| `Size` | 圆环尺寸，映射到 `radio.size.*` 令牌 | `LhRadioSize` | `Medium` |

### LhRadioSize

| 值 | 尺寸令牌 |
| --- | --- |
| `Small` | `radio.size.small`（14epx） |
| `Medium` | `radio.size.medium`（16epx） |
| `Large` | `radio.size.large`（18epx） |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `radio.size.{small,medium,large}` | 三档圆环尺寸 |
| `radio.ringWidth` | 外环描边宽度 |
| `radio.dotScale` | 内圆点直径比例（尺寸 × dotScale） |
| `radio.gap` | 圆环与文字间距 |
| `color.primary.default` | 选中态外环与圆点色 |
| `color.border.base` | 未选中外环色 |
| `color.disabled.{border,text}` | 禁用态外环/圆点 |
| `color.text.{primary,disabled}` | 标签文字色 |
| `typography.fontSize.base` / `fontFamily` | 标签字号与字体栈 |

## 黄金用例对照

`cases/radio.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhRadioTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `tap-unchecked-emits-onchange` | 未选中点击触发 onChange | `Tap_Unchecked_Emits_OnChange` |
| `tap-checked-keeps-checked` | 已选中再点不触发 | `Tap_Checked_Keeps_Checked` |
| `disabled-blocks-change` | 禁用态不可交互 | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | 选中态消费主色令牌 | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | 未选中态消费描边令牌 | `Unchecked_Uses_Border_Token` |
| `small-size-uses-small-token` | 小号消费 small 尺寸令牌 | `Small_Size_Uses_Small_Token` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
