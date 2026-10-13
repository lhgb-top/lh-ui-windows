# Tag 标签

用于标记和选择的标签。支持六种语义类型、三档尺寸、描边态、圆角态与可关闭态。

> 契约源：`lh-ui-core/specs/tag.json` ·
> 黄金用例：`lh-ui-core/cases/tag.json`（6 条，已翻译为 xUnit 测试）

## 类型

六种类型决定底色、文字色与描边色。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tag 类型</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag">默认</span>
      <span class="sim-tag sim-tag--primary">主要</span>
      <span class="sim-tag sim-tag--success">成功</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--warning">警告</span>
      <span class="sim-tag sim-tag--danger">危险</span>
      <span class="sim-tag sim-tag--info">信息</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var tag = new LhTag
{
    Text = "主要",
    Type = LhTagType.Primary,
};
```

## 描边与圆角

`Plain` 为 true 时切换为语义浅底 + 语义文字 + 语义描边；`Round` 为 true 时
圆角取高度的一半（胶囊态，与 `tag.borderRadius` 令牌并列）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>描边与圆角</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--plain">描边</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">描边</span>
      <span class="sim-tag sim-tag--danger sim-tag--plain">描边</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--round">圆角</span>
      <span class="sim-tag sim-tag--success sim-tag--round sim-tag--plain">圆角描边</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 尺寸与关闭

三档尺寸映射到 `tag.height` / `paddingHorizontal` / `fontSize` 令牌；`Closable`
为 true 时在右侧绘制关闭符，点击触发 `Close` 事件。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>尺寸与关闭</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--small">小</span>
      <span class="sim-tag">中</span>
      <span class="sim-tag sim-tag--large">大</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">可关 <span class="sim-tag__close">✕</span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var tag = new LhTag
{
    Text = "可关",
    Type = LhTagType.Primary,
    Closable = true,
};
tag.Close += () => { /* 点击关闭符 */ };
```

## 明暗主题

切换站点右上角主题，模拟器手机框内的标签颜色即变；真机通过 `LhTheme.Apply(true)` 切换。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>暗色标签</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">主要</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">描边</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染；明/暗值全部取自 core 令牌快照</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `Close` | 点击关闭符时触发（需 `Closable` 为 true） | `event Action` | — |
| `Text` | 标签文字内容（spec 的 default 插槽） | `string` | `""` |
| `Type` | 语义类型，决定底色、文字色与描边色 | `LhTagType` | `Default` |
| `Size` | 标签尺寸，映射到尺寸令牌 | `LhTagSize` | `Medium` |
| `Plain` | 是否为浅底描边形态 | `bool` | `false` |
| `Round` | 是否为胶囊形态（圆角取高度一半） | `bool` | `false` |
| `Closable` | 是否显示关闭符 | `bool` | `false` |

### LhTagType

| 值 | 实心底 | 浅底（plain） | 文字色 |
| --- | --- | --- | --- |
| `Default` | `color.background.default` | 同左 | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.primary.bg` | `color.onPrimary` / `color.primary.default` |
| `Success` | `color.success.default` | `color.success.bg` | `color.onPrimary` / `color.success.default` |
| `Warning` | `color.warning.default` | `color.warning.bg` | `color.onPrimary` / `color.warning.default` |
| `Danger` | `color.danger.default` | `color.danger.bg` | `color.onPrimary` / `color.danger.default` |
| `Info` | `color.info.default` | `color.info.bg` | `color.onPrimary` / `color.info.default` |

### LhTagSize

| 值 | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `tag.height.small`（20epx） | `tag.paddingHorizontal.small`（8epx） | `tag.fontSize.small`（11） |
| `Medium` | `tag.height.medium`（24epx） | `tag.paddingHorizontal.medium`（12epx） | `tag.fontSize.medium`（12） |
| `Large` | `tag.height.large`（28epx） | `tag.paddingHorizontal.large`（16epx） | `tag.fontSize.large`（13） |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `tag.height.{small,medium,large}` | 三档高度 |
| `tag.paddingHorizontal.{small,medium,large}` | 三档横向内边距 |
| `tag.fontSize.{small,medium,large}` | 三档字号 |
| `tag.borderRadius` / `tag.borderWidth` | 圆角与描边宽度 |
| `color.primary.default` / `color.onPrimary` | 主色类型底/文字 |
| `color.{success,warning,danger,info}.{default,bg}` | 语义类型实心底/浅底 |
| `color.background.default` / `color.text.primary` / `color.border.base` | 默认类型三色 |
| `spacing.xs` | 关闭符与文字间距 |
| `typography.fontFamily` | 国风衬线字体栈 |

## 黄金用例对照

`cases/tag.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhTagTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `default-type-renders-text` | 默认类型中性底/文字/描边 | `Default_Type_Renders_Text` |
| `primary-type-uses-primary-tokens` | 主色类型消费主色令牌 | `Primary_Type_Uses_Primary_Tokens` |
| `plain-uses-bg-tokens` | 浅底形态消费语义浅底令牌 | `Plain_Uses_Bg_Tokens` |
| `small-size-uses-small-tokens` | 小号消费 small 尺寸令牌 | `Small_Size_Uses_Small_Tokens` |
| `round-makes-radius-half-height` | 胶囊态圆角为高度一半 | `Round_Makes_Radius_Half_Height` |
| `closable-tap-emits-onclose` | 点击关闭符触发 onClose | `Closable_Tap_Emits_OnClose` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
