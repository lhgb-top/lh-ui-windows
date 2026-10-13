# Icon 图标

图标组件。按本地图形体系把图标名解析为字形渲染，支持尺寸与语义色调。

> 契约源：`lh-ui-core/specs/icon.json` ·
> 黄金用例：`lh-ui-core/cases/icon.json`（5 条，已翻译为 xUnit 测试）

::: tip 端侧落点说明
Windows 端当前用 unicode 字形占位（`LhIconTokens.GlyphFor`，TextBlock 渲染），生产环境应替换为
`FontIcon`（Segoe Fluent Icons 矢量资产）；这与 button 的占位策略一致。已登记图标名见下方「图标名」一节。
:::

## 尺寸

三档尺寸映射到 `icon.size` 令牌。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icon 尺寸</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--small">⌕</span>
      <span class="sim-icon">⌕</span>
      <span class="sim-icon sim-icon--large">⌕</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var icon = new LhIcon
{
    Name = "search",
    Size = LhIconSize.Large,
};
```

## 色调

`Tone` 决定字形颜色，与语义色令牌一一对应；默认色调取 `color.text.primary`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icon 色调</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">✓ 默认</span>
      <span class="sim-icon sim-icon--primary">✓ 主色</span>
      <span class="sim-icon sim-icon--success">✓ 成功</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--warning">⚠ 警告</span>
      <span class="sim-icon sim-icon--danger">✕ 危险</span>
      <span class="sim-icon sim-icon--info">ℹ 信息</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--regular">✓ 常规</span>
      <span class="sim-icon sim-icon--secondary">✓ 次要</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 图标名

`GlyphFor` 当前登记的图标名与字形：`search` ⌕、`refresh` ↻、`check` ✓、`close` ✕、
`warning` ⚠、`info` ℹ、`back` ←、`more` ⋯、`star` ★、`edit` ✎、`delete` ⌫、
`plus` ＋、`minus` －、`arrow` →。未登记名回退 `◆`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>已登记图标</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">⌕</span><span class="sim-icon">↻</span><span class="sim-icon">✓</span>
      <span class="sim-icon">✕</span><span class="sim-icon">⚠</span><span class="sim-icon">ℹ</span>
      <span class="sim-icon">←</span><span class="sim-icon">⋯</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon">★</span><span class="sim-icon">✎</span><span class="sim-icon">⌫</span>
      <span class="sim-icon">＋</span><span class="sim-icon">－</span><span class="sim-icon">→</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 明暗主题

切换站点右上角主题，模拟器手机框内的图标颜色即变；真机通过 `LhTheme.Apply(true)` 切换。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>暗色图标</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--primary">★</span>
      <span class="sim-icon sim-icon--success">✓</span>
      <span class="sim-icon sim-icon--warning">⚠</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染；明/暗值全部取自 core 令牌快照</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `Name` | 图标名（本地图标表解析，未登记名回退 `◆`） | `string` | `""` |
| `Size` | 图标尺寸，映射到 `icon.size.*` 令牌 | `LhIconSize` | `Medium` |
| `Tone` | 图标色调，映射到语义色 / 文字色令牌 | `LhIconTone` | `Regular` |

### LhIconSize

| 值 | 尺寸令牌 |
| --- | --- |
| `Small` | `icon.size.small`（16epx） |
| `Medium` | `icon.size.medium`（18epx） |
| `Large` | `icon.size.large`（20epx） |

### LhIconTone

| 值 | 颜色令牌 |
| --- | --- |
| `Default` | `color.text.primary` |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `icon.size.{small,medium,large}` | 三档尺寸 |
| `color.text.{primary,regular,secondary}` | 中性色调 |
| `color.{primary,success,warning,danger,info}.default` | 语义色调 |
| `typography.fontFamily` | 字形字体栈 |

## 黄金用例对照

`cases/icon.json` 的 5 条断言与 xUnit 测试的对应关系（`tests/LhIconTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `click-emits-onclick` | gate 放行（图标可交互） | `Click_Emits_OnClick` |
| `default-tone-uses-text-primary` | 默认色调取主要文字色令牌 | `Default_Tone_Uses_Text_Primary` |
| `primary-tone-uses-primary-token` | 主色调取主色令牌 | `Primary_Tone_Uses_Primary_Token` |
| `small-size-uses-small-token` | 小号消费 small 尺寸令牌 | `Small_Size_Uses_Small_Token` |
| `render-emits-icon-command` | 有且仅有一条图标指令 | `Render_Emits_Single_Icon_Command` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
