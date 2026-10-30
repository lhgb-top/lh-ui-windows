# Icon 圖示

圖示元件。按本機圖形體系把圖示名解析為字形渲染，支援尺寸與語意色調。

> 契約源：`lh-ui-core/specs/icon.json` ·
> 黃金用例：`lh-ui-core/cases/icon.json`（5 條，已翻譯為 xUnit 測試）

::: tip 端側落點說明
Windows 端當前用 unicode 字形佔位（`LhIconTokens.GlyphFor`，TextBlock 渲染），生產環境應替換為
`FontIcon`（Segoe Fluent Icons 向量資產）；這與 button 的佔位策略一致。已登記圖示名見下方「圖示名」一節。
:::

## 尺寸

三檔尺寸映射到 `icon.size` 令牌。

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
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var icon = new LhIcon
{
    Name = "search",
    Size = LhIconSize.Large,
};
```

## 色調

`Tone` 決定字形顏色，與語意色令牌一一對應；預設色調取 `color.text.primary`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icon 色調</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">✓ 預設</span>
      <span class="sim-icon sim-icon--primary">✓ 主色</span>
      <span class="sim-icon sim-icon--success">✓ 成功</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--warning">⚠ 警告</span>
      <span class="sim-icon sim-icon--danger">✕ 危險</span>
      <span class="sim-icon sim-icon--info">ℹ 資訊</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--regular">✓ 常規</span>
      <span class="sim-icon sim-icon--secondary">✓ 次要</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 圖示名

`GlyphFor` 當前登記的圖示名與字形：`search` ⌕、`refresh` ↻、`check` ✓、`close` ✕、
`warning` ⚠、`info` ℹ、`back` ←、`more` ⋯、`star` ★、`edit` ✎、`delete` ⌫、
`plus` ＋、`minus` －、`arrow` →。未登記名回退 `◆`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>已登記圖示</span></div>
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
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 明暗主題

切換站台右上角主題，模擬器手機框內的圖示顏色即變；真機透過 `LhTheme.Apply(true)` 切換。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>暗色圖示</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--primary">★</span>
      <span class="sim-icon sim-icon--success">✓</span>
      <span class="sim-icon sim-icon--warning">⚠</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染；明/暗值全部取自 core 令牌快照</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `Name` | 圖示名（本機圖示表解析，未登記名回退 `◆`） | `string` | `""` |
| `Size` | 圖示尺寸，映射到 `icon.size.*` 令牌 | `LhIconSize` | `Medium` |
| `Tone` | 圖示色調，映射到語意色 / 文字色令牌 | `LhIconTone` | `Regular` |

### LhIconSize

| 值 | 尺寸令牌 |
| --- | --- |
| `Small` | `icon.size.small`（16epx） |
| `Medium` | `icon.size.medium`（18epx） |
| `Large` | `icon.size.large`（20epx） |

### LhIconTone

| 值 | 顏色令牌 |
| --- | --- |
| `Default` | `color.text.primary` |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `icon.size.{small,medium,large}` | 三檔尺寸 |
| `color.text.{primary,regular,secondary}` | 中性色調 |
| `color.{primary,success,warning,danger,info}.default` | 語意色調 |
| `typography.fontFamily` | 字形字型棧 |

## 黃金用例對照

`cases/icon.json` 的 5 條斷言與 xUnit 測試的對應關係（`tests/LhIconTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `click-emits-onclick` | gate 放行（圖示可互動） | `Click_Emits_OnClick` |
| `default-tone-uses-text-primary` | 預設色調取主要文字色令牌 | `Default_Tone_Uses_Text_Primary` |
| `primary-tone-uses-primary-token` | 主色調取主色令牌 | `Primary_Tone_Uses_Primary_Token` |
| `small-size-uses-small-token` | 小號消費 small 尺寸令牌 | `Small_Size_Uses_Small_Token` |
| `render-emits-icon-command` | 有且僅有一條圖示指令 | `Render_Emits_Single_Icon_Command` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
