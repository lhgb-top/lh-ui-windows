# Navbar 導覽列

頁面頂部固定欄。左/中/右三段，支援文字與圖示。

> 契約源：`lh-ui-core/specs/navbar.json` ·
> 黃金用例：`lh-ui-core/cases/navbar.json`（6 條，已翻譯為 xUnit 測試）

## 基礎用法

層級取 `zIndex.sticky`，宿主將其作為頁面頂部固定欄（Grid 首列或 Canvas 頂部）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">← 返回</span>
      <span class="sim-navbar__title">青花瓷</span>
      <span class="sim-navbar__right">更多</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

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

## 圖示

`LeftIcon` / `RightIcon` 傳入本機圖示名（見 Icon 文件的字形表），與文字二者非空時都顯示；
兩側無內容時不觸發對應點選事件。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar 圖示</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">←</span>
      <span class="sim-navbar__title">僅圖示</span>
      <span class="sim-navbar__right">⋯</span>
    </div>
    <div class="sim-navbar">
      <span class="sim-navbar__title">無兩側內容</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 分隔線

`Bordered` 為 true 時底部繪製 `color.border.lighter` 分隔線；預設開啟。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar 分隔線</span></div>
  <div class="sim-screen">
    <div class="sim-navbar sim-navbar--bordered">
      <span class="sim-navbar__title">帶分隔線</span>
    </div>
    <div class="sim-navbar sim-navbar--borderless">
      <span class="sim-navbar__title">無分隔線</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `LeftClick` | 點選左側區域時觸發（無左側內容時不觸發） | `event Action` | — |
| `RightClick` | 點選右側區域時觸發（無右側內容時不觸發） | `event Action` | — |
| `Title` | 中間標題 | `string` | `""` |
| `LeftText` | 左側按鈕文字；與 LeftIcon 二者非空時都顯示 | `string` | `""` |
| `LeftIcon` | 左側圖示名（本機圖示體系渲染） | `string?` | `null` |
| `RightText` | 右側按鈕文字；與 RightIcon 二者非空時都顯示 | `string` | `""` |
| `RightIcon` | 右側圖示名（本機圖示體系渲染） | `string?` | `null` |
| `Bordered` | 是否繪製底部分隔線 | `bool` | `true` |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `navbar.height` | 高度 |
| `navbar.paddingHorizontal` | 左右內邊距 |
| `navbar.titleFontSize` | 標題與兩側文字字級 |
| `navbar.iconSize` | 兩側圖示尺寸 |
| `navbar.borderWidth` | 底部分隔線寬 |
| `color.background.default` | 底色 |
| `color.text.primary` | 標題與兩側文字色 |
| `color.border.lighter` | 底部分隔線色 |
| `zIndex.sticky` | 固定層級 |
| `spacing.xs` | 圖示與文字間距 |
| `typography.fontFamily` | 國風襯線字型棧 |

## 黃金用例對照

`cases/navbar.json` 的 6 條斷言與 xUnit 測試的對應關係（`tests/LhNavbarTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `title-renders-text` | 標題按字級令牌渲染 | `Title_Renders_Text` |
| `leftclick-emits-onleftclick` | 點選左側觸發 onLeftClick | `LeftClick_Emits_OnLeftClick` |
| `rightclick-emits-onrightclick` | 點選右側觸發 onRightClick | `RightClick_Emits_OnRightClick` |
| `no-left-content-no-onleftclick` | 無左側內容時不觸發 | `No_Left_Content_No_OnLeftClick` |
| `bordered-uses-border-token` | 分隔線消費 border.lighter 令牌 | `Bordered_Uses_Border_Token` |
| `height-uses-navbar-height-token` | 高度消費 navbar.height 令牌 | `Height_Uses_Navbar_Height_Token` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
