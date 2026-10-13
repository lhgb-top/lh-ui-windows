# Switch 开关

开关。胶囊轨道 + 圆形滑块，点击切换；支持禁用与加载中。

> 契约源：`lh-ui-core/specs/switch.json` ·
> 黄金用例：`lh-ui-core/cases/switch.json`（6 条，已翻译为 xUnit 测试）

## 基础用法

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch 基础</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };
```

## 尺寸

三档尺寸映射到 `switch.width` / `switch.height` 令牌；滑块直径 = 高度 - 2 × `switch.gap`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch 尺寸</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--small sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--large sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 状态

`Disabled` 与 `Loading` 都不响应点击；`Loading` 时滑块位置改画主色加载环。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch 状态</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--checked sim-switch--disabled"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--loading"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `Change` | 开关态变化回调；disabled 或 loading 时不触发 | `event Action<bool>` | — |
| `Checked` | 是否开启 | `bool` | `false` |
| `Disabled` | 是否禁用（不响应点击且呈现禁用样式） | `bool` | `false` |
| `Loading` | 是否加载中（滑块位置显示加载环且不响应点击） | `bool` | `false` |
| `Size` | 开关尺寸，映射到 `switch.width` / `switch.height` 令牌 | `LhSwitchSize` | `Medium` |

### LhSwitchSize

| 值 | width | height |
| --- | --- | --- |
| `Small` | `switch.width.small`（40epx） | `switch.height.small`（22epx） |
| `Medium` | `switch.width.medium`（48epx） | `switch.height.medium`（26epx） |
| `Large` | `switch.width.large`（56epx） | `switch.height.large`（30epx） |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `switch.width.{small,medium,large}` | 三档轨道宽度 |
| `switch.height.{small,medium,large}` | 三档轨道高度 |
| `switch.gap` | 滑块与轨道留白（滑块直径 = 高度 - 2 × gap） |
| `color.primary.default` | 开启轨道色 |
| `color.border.base` | 关闭轨道色 |
| `color.onPrimary` | 滑块色 |
| `color.disabled.bg` / `color.text.disabled` | 禁用轨道/滑块色 |

## 黄金用例对照

`cases/switch.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhSwitchTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | 点击切换并触发 onChange | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-turns-off` | 开启态点击关闭 | `Tap_Checked_Turns_Off` |
| `disabled-blocks-change` | 禁用态不可交互 | `Disabled_Blocks_Change` |
| `loading-blocks-change` | 加载态不可交互 | `Loading_Blocks_Change` |
| `checked-uses-primary-token` | 开启态消费主色令牌 | `Checked_Uses_Primary_Token` |
| `unchecked-uses-border-token` | 关闭态消费描边令牌 | `Unchecked_Uses_Border_Token` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
