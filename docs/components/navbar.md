# Navbar 导航栏

页面顶部固定栏。左/中/右三段，支持文字与图标。

> 契约源：`lh-ui-core/specs/navbar.json` ·
> 黄金用例：`lh-ui-core/cases/navbar.json`（6 条，已翻译为 xUnit 测试）

## 基础用法

层级取 `zIndex.sticky`，宿主将其作为页面顶部固定栏（Grid 首行或 Canvas 顶部）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar 基础</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">← 返回</span>
      <span class="sim-navbar__title">青花瓷</span>
      <span class="sim-navbar__right">更多</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var navbar = new LhNavbar
{
    Title = "青花瓷",
    LeftText = "返回",
    LeftIcon = "back",
    RightText = "更多",
};
navbar.LeftClick += () => { };
navbar.RightClick += () => { };
```

## 图标

`LeftIcon` / `RightIcon` 传入本地图标名（见 Icon 文档的字形表），与文字二者非空时都显示；
两侧无内容时不触发对应点击事件。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar 图标</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">←</span>
      <span class="sim-navbar__title">仅图标</span>
      <span class="sim-navbar__right">⋯</span>
    </div>
    <div class="sim-navbar">
      <span class="sim-navbar__title">无两侧内容</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 分隔线

`Bordered` 为 true 时底部绘制 `color.border.lighter` 分隔线；默认开启。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar 分隔线</span></div>
  <div class="sim-screen">
    <div class="sim-navbar sim-navbar--bordered">
      <span class="sim-navbar__title">带分隔线</span>
    </div>
    <div class="sim-navbar sim-navbar--borderless">
      <span class="sim-navbar__title">无分隔线</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `LeftClick` | 点击左侧区域时触发（无左侧内容时不触发） | `event Action` | — |
| `RightClick` | 点击右侧区域时触发（无右侧内容时不触发） | `event Action` | — |
| `Title` | 中间标题 | `string` | `""` |
| `LeftText` | 左侧按钮文字；与 LeftIcon 二者非空时都显示 | `string` | `""` |
| `LeftIcon` | 左侧图标名（本地图标体系渲染） | `string?` | `null` |
| `RightText` | 右侧按钮文字；与 RightIcon 二者非空时都显示 | `string` | `""` |
| `RightIcon` | 右侧图标名（本地图标体系渲染） | `string?` | `null` |
| `Bordered` | 是否绘制底部分隔线 | `bool` | `true` |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `navbar.height` | 高度 |
| `navbar.paddingHorizontal` | 左右内边距 |
| `navbar.titleFontSize` | 标题与两侧文字字号 |
| `navbar.iconSize` | 两侧图标尺寸 |
| `navbar.borderWidth` | 底部分隔线宽 |
| `color.background.default` | 底色 |
| `color.text.primary` | 标题与两侧文字色 |
| `color.border.lighter` | 底部分隔线色 |
| `zIndex.sticky` | 固定层级 |
| `spacing.xs` | 图标与文字间距 |
| `typography.fontFamily` | 国风衬线字体栈 |

## 黄金用例对照

`cases/navbar.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhNavbarTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `title-renders-text` | 标题按字号令牌渲染 | `Title_Renders_Text` |
| `leftclick-emits-onleftclick` | 点击左侧触发 onLeftClick | `LeftClick_Emits_OnLeftClick` |
| `rightclick-emits-onrightclick` | 点击右侧触发 onRightClick | `RightClick_Emits_OnRightClick` |
| `no-left-content-no-onleftclick` | 无左侧内容时不触发 | `No_Left_Content_No_OnLeftClick` |
| `bordered-uses-border-token` | 分隔线消费 border.lighter 令牌 | `Bordered_Uses_Border_Token` |
| `height-uses-navbar-height-token` | 高度消费 navbar.height 令牌 | `Height_Uses_Navbar_Height_Token` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
