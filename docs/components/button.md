# Button 按钮

基础操作按钮。触发即时操作，支持类型、尺寸、加载、禁用、图标与国风花纹底。

> 契约源：`lh-ui-core/specs/button.json` ·
> 黄金用例：`lh-ui-core/cases/button.json`（8 条，已翻译为 xUnit 测试）

## 类型

六种类型决定底色与文字色。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button 类型</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">默认按钮</button>
      <button class="sim-btn sim-btn--primary">主要按钮</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--success">成功按钮</button>
      <button class="sim-btn sim-btn--warning">警告按钮</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--danger">危险按钮</button>
      <button class="sim-btn sim-btn--info">信息按钮</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var button = new LhButton
{
    Type = LhButtonType.Primary,
    Text = "主要按钮",
};
button.Click += OnButtonClick;
```

## 尺寸

三档尺寸映射到 `button.height` / `paddingHorizontal` / `fontSize` / `iconSize` 令牌。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button 尺寸</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">小型按钮</button>
      <button class="sim-btn">中型按钮</button>
      <button class="sim-btn sim-btn--large">大型按钮</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 状态

禁用与加载中都不触发 `Click`；两者共存时按**禁用优先**处理（黄金用例 `disabled-precedes-loading`）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button 状态</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">正常</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">禁用</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>加载中
      </button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">禁用优先</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 图标与花纹底

图标名按本地图标体系解析（`LhButtonTokens.IconFor`，当前支持 search / refresh / check / close，
未登记名不绘制）。花纹底消费 `color.decoration` 令牌，45° 斜纹叠加。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>图标与花纹</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>搜索</button>
      <button class="sim-btn"><span class="sim-btn__icon"></span>默认</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--pattern">主要花纹底</button>
      <button class="sim-btn sim-btn--pattern">默认花纹底</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 明暗主题

切换站点右上角主题，模拟器手机框内的按钮颜色即变；真机通过 `LhTheme.Apply(true)` 切换。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>暗色按钮</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">主要按钮</button>
      <button class="sim-btn">默认按钮</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--disabled">禁用</button>
      <button class="sim-btn sim-btn--success">成功</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染；明/暗值全部取自 core 令牌快照</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `Click` | 点击事件；`Disabled` 或 `Loading` 为 true 时不触发 | `event Action` | — |
| `Type` | 按钮类型，决定底色与文字色 | `LhButtonType` | `Default` |
| `Size` | 按钮尺寸，映射到尺寸令牌 | `LhButtonSize` | `Medium` |
| `Disabled` | 是否禁用 | `bool` | `false` |
| `Loading` | 是否加载中（显示指示器，不触发 Click） | `bool` | `false` |
| `Icon` | 图标名（本地图标体系解析） | `string?` | `null` |
| `BgPattern` | 是否绘制国风花纹底纹 | `bool` | `false` |
| `Text` | 按钮文字内容（spec 的 default 插槽） | `string` | `""` |

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

禁用态覆盖：底色 `color.disabled.bg`、文字 `color.disabled.text`、描边 `color.disabled.border`。

## 令牌对照

按钮消费的令牌（与 `specs/button.json` 的 `tokenRefs` 逐条对齐）：

| 令牌 | 用途 |
| --- | --- |
| `button.height.{small,medium,large}` | 三档高度 |
| `button.paddingHorizontal.{small,medium,large}` | 三档横向内边距 |
| `button.fontSize.{small,medium,large}` | 三档字号 |
| `button.iconSize.{small,medium,large}` | 三档图标尺寸 |
| `button.borderRadius` / `button.borderWidth` | 圆角（跟随 `radius.base`）与描边宽度 |
| `button.fontWeight` | 字重（跟随 `typography.fontWeight.primary`，端侧映射 FontWeights） |
| `color.primary.default` / `color.onPrimary` | 主色类型底/文字 |
| `color.{success,warning,danger,info}.default` | 语义类型底色 |
| `color.background.default` / `color.text.primary` / `color.border.base` | 默认类型底/文字/描边 |
| `color.disabled.{bg,text,border}` | 禁用态三色 |
| `color.decoration` | 花纹底纹色 |
| `spacing.xs` | 图标与文字间距 |
| `typography.fontFamily` | 国风衬线字体栈 |

## 黄金用例对照

`cases/button.json` 的 8 条断言与 xUnit 测试的对应关系（`tests/LhButtonTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `click-emits-onclick` | gate 放行（可交互） | `Enabled_Button_Is_Interactive` |
| `disabled-blocks-click` | 禁用态不可交互 | `Disabled_Is_Not_Interactive` |
| `loading-blocks-click` | 加载态不可交互 | `Loading_Is_Not_Interactive` |
| `disabled-precedes-loading` | 禁用优先 + 禁用样式 | `Disabled_Precedes_Loading` |
| `default-type-renders-text` | 默认类型中性底/文字 | `Default_Type_Uses_Neutral_Tokens` |
| `primary-type-uses-primary-tokens` | 主色类型消费主色令牌 | `Primary_Type_Uses_Primary_Tokens` |
| `small-size-uses-small-tokens` | 小号消费 small 尺寸令牌 | `Small_Size_Uses_Small_Tokens` |
| `bgpattern-uses-decoration-token` | 花纹底消费装饰色 | `BgPattern_Uses_Decoration_Token` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
