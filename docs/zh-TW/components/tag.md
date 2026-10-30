# Tag 標籤

用於標記和選擇的標籤。支援六種語意類型、三檔尺寸、描邊態、圓角態與可關閉態。

> 契約源：`lh-ui-core/specs/tag.json` ·
> 黃金用例：`lh-ui-core/cases/tag.json`（6 條，已翻譯為 xUnit 測試）

## 類型

六種類型決定底色、文字色與描邊色。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tag 類型</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag">預設</span>
      <span class="sim-tag sim-tag--primary">主要</span>
      <span class="sim-tag sim-tag--success">成功</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--warning">警告</span>
      <span class="sim-tag sim-tag--danger">危險</span>
      <span class="sim-tag sim-tag--info">資訊</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var tag = new LhTag
{
    Text = "主要",
    Type = LhTagType.Primary,
};
```

## 描邊與圓角

`Plain` 為 true 時切換為語意淺底 + 語意文字 + 語意描邊；`Round` 為 true 時
圓角取高度的一半（膠囊態，與 `tag.borderRadius` 令牌並列）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>描邊與圓角</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--plain">描邊</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">描邊</span>
      <span class="sim-tag sim-tag--danger sim-tag--plain">描邊</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--round">圓角</span>
      <span class="sim-tag sim-tag--success sim-tag--round sim-tag--plain">圓角描邊</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 尺寸與關閉

三檔尺寸映射到 `tag.height` / `paddingHorizontal` / `fontSize` 令牌；`Closable`
為 true 時在右側繪製關閉符，點選觸發 `Close` 事件。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>尺寸與關閉</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--small">小</span>
      <span class="sim-tag">中</span>
      <span class="sim-tag sim-tag--large">大</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">可關 <span class="sim-tag__close">✕</span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var tag = new LhTag
{
    Text = "可關",
    Type = LhTagType.Primary,
    Closable = true,
};
tag.Close += () => { /* 點選關閉符 */ };
```

## 明暗主題

切換站台右上角主題，模擬器手機框內的標籤顏色即變；真機透過 `LhTheme.Apply(true)` 切換。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>暗色標籤</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">主要</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">描邊</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染；明/暗值全部取自 core 令牌快照</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `Close` | 點選關閉符時觸發（需 `Closable` 為 true） | `event Action` | — |
| `Text` | 標籤文字內容（spec 的 default 插槽） | `string` | `""` |
| `Type` | 語意類型，決定底色、文字色與描邊色 | `LhTagType` | `Default` |
| `Size` | 標籤尺寸，映射到尺寸令牌 | `LhTagSize` | `Medium` |
| `Plain` | 是否為淺底描邊形態 | `bool` | `false` |
| `Round` | 是否為膠囊形態（圓角取高度一半） | `bool` | `false` |
| `Closable` | 是否顯示關閉符 | `bool` | `false` |

### LhTagType

| 值 | 實心底 | 淺底（plain） | 文字色 |
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

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `tag.height.{small,medium,large}` | 三檔高度 |
| `tag.paddingHorizontal.{small,medium,large}` | 三檔橫向內邊距 |
| `tag.fontSize.{small,medium,large}` | 三檔字級 |
| `tag.borderRadius` / `tag.borderWidth` | 圓角與描邊寬度 |
| `color.primary.default` / `color.onPrimary` | 主色類型底/文字 |
| `color.{success,warning,danger,info}.{default,bg}` | 語意類型實心底/淺底 |
| `color.background.default` / `color.text.primary` / `color.border.base` | 預設類型三色 |
| `spacing.xs` | 關閉符與文字間距 |
| `typography.fontFamily` | 國風襯線字型棧 |

## 黃金用例對照

`cases/tag.json` 的 6 條斷言與 xUnit 測試的對應關係（`tests/LhTagTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `default-type-renders-text` | 預設類型中性底/文字/描邊 | `Default_Type_Renders_Text` |
| `primary-type-uses-primary-tokens` | 主色類型消費主色令牌 | `Primary_Type_Uses_Primary_Tokens` |
| `plain-uses-bg-tokens` | 淺底形態消費語意淺底令牌 | `Plain_Uses_Bg_Tokens` |
| `small-size-uses-small-tokens` | 小號消費 small 尺寸令牌 | `Small_Size_Uses_Small_Tokens` |
| `round-makes-radius-half-height` | 膠囊態圓角為高度一半 | `Round_Makes_Radius_Half_Height` |
| `closable-tap-emits-onclose` | 點選關閉符觸發 onClose | `Closable_Tap_Emits_OnClose` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
