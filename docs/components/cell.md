# Cell 单元格

列表行单元。左侧图标 + 标题（可带必填红星）+ 说明，右侧值与指示箭头，底部可选分隔线。

> 契约源：`lh-ui-core/specs/cell.json` ·
> 黄金用例：`lh-ui-core/cases/cell.json`（6 条，已翻译为 xUnit 测试）

## 基础用法

标题取 `color.text.primary`，值与说明取 `color.text.secondary`，三者都由令牌驱动。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Cell 基础</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">行标题</div>
        <div class="sim-cell__label">说明文字</div>
      </div>
      <div class="sim-cell__value">行内容</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">带线单元格</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var cell = new LhCell
{
    Title = "行标题",
    Label = "说明文字",
    Value = "行内容",
};
```

## 图标与必填

`Icon` 传入本地图标名（见 Icon 文档的字形表），在左侧绘制；
`Required` 为 true 时标题前绘制 `color.danger.default` 的红星。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>图标与必填</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <span class="sim-cell__icon">★</span>
      <div class="sim-cell__main">
        <div class="sim-cell__title">带图标</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title"><span class="sim-cell__required">✱</span>必填项</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 分隔线与点击

`Bordered` 为 true 时底部绘制 `color.border.lighter` 分隔线；`Clickable` 为 true 时
右侧绘制指示箭头且点击触发 `Click` 事件。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>分隔线与点击</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">可点</div></div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">不可点</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var cell = new LhCell
{
    Title = "可点",
    Clickable = true,
    Bordered = true,
};
cell.Click += () => { /* 仅 Clickable 为 true 时触发 */ };
```

## 明暗主题

切换站点右上角主题，模拟器手机框内的文字色与分隔线色即变；真机通过 `LhTheme.Apply(true)` 切换。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>暗色单元格</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">行标题</div>
        <div class="sim-cell__label">说明文字</div>
      </div>
      <div class="sim-cell__value">行内容</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染；明/暗值全部取自 core 令牌快照</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `Click` | 点击事件；仅 `Clickable` 为 true 时触发 | `event Action` | — |
| `Title` | 标题（spec 的 title 插槽） | `string` | `""` |
| `Label` | 标题下说明文字（label 插槽） | `string` | `""` |
| `Value` | 右侧值文本（value 插槽） | `string` | `""` |
| `Icon` | 左侧图标名（本地图标表解析） | `string?` | `null` |
| `Required` | 是否在标题前绘制必填红星 | `bool` | `false` |
| `Clickable` | 是否可点击（显隐箭头、gate Click） | `bool` | `false` |
| `Bordered` | 是否绘制底部分隔线 | `bool` | `true` |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `cell.paddingHorizontal` / `cell.paddingVertical` | 内边距 |
| `cell.gap` | 标题与说明的垂直间距 |
| `cell.fontSizeTitle` / `fontSizeLabel` / `fontSizeValue` | 三档字号 |
| `color.text.{primary,secondary,regular}` | 标题/说明/值/图标色 |
| `color.danger.default` | 必填红星 |
| `color.border.lighter` | 底部分隔线 |
| `icon.size.medium` | 左侧图标尺寸 |
| `spacing.{sm,xs}` | 图标间距 / 内部元素间距 |
| `typography.lineHeight.primary` | 文字行高 |

## 黄金用例对照

`cases/cell.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhCellTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `title-and-value-render` | 标题与值都被渲染 | `Title_And_Value_Render` |
| `label-renders-secondary-tone` | 说明文字取次要文字色令牌 | `Label_Renders_Secondary_Tone` |
| `required-uses-danger-token` | 必填红星消费危险色令牌 | `Required_Uses_Danger_Token` |
| `bordered-emits-line-command` | 分隔线形态发出 LINE 指令 | `Bordered_Emits_Line_Command` |
| `clickable-emits-onclick` | 可点击时点击触发 onClick | `Clickable_Emits_OnClick` |
| `not-clickable-no-onclick` | 非可点击时不触发 onClick | `Not_Clickable_No_OnClick` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
