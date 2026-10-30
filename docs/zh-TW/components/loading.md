# Loading 載入

環形旋轉載入指示器。支援尺寸與語意色調。

> 契約源：`lh-ui-core/specs/loading.json` ·
> 黃金用例：`lh-ui-core/cases/loading.json`（5 條，已翻譯為 xUnit 測試）

::: tip 端側落點說明
旋轉由 WinUI 的 `ProgressRing` 平台動畫承擔（樸素調性：只發幾何，動畫由平台補齊）——
令牌層只決定直徑、描邊寬與環色。
:::

## 尺寸

三檔尺寸映射到 `loading.size.*` 直徑令牌。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Loading 尺寸</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--small"></span>
      <span class="sim-loading"></span>
      <span class="sim-loading sim-loading--large"></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var loading = new LhLoading
{
    Size = LhLoadingSize.Large,
    Tone = LhLoadingTone.Primary,
};
```

## 色調

`Tone` 決定環色，與語意色令牌一一對應。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Loading 色調</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--primary"></span>
      <span class="sim-loading sim-loading--success"></span>
      <span class="sim-loading sim-loading--warning"></span>
    </div>
    <div class="sim-row">
      <span class="sim-loading sim-loading--danger"></span>
      <span class="sim-loading sim-loading--info"></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `Size` | 指示器直徑，映射到 `loading.size.*` 令牌 | `LhLoadingSize` | `Medium` |
| `Tone` | 環色，取對應語意色令牌 | `LhLoadingTone` | `Primary` |

### LhLoadingSize

| 值 | 直徑令牌 |
| --- | --- |
| `Small` | `loading.size.small`（20epx） |
| `Medium` | `loading.size.medium`（28epx） |
| `Large` | `loading.size.large`（36epx） |

### LhLoadingTone

| 值 | 環色令牌 |
| --- | --- |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `loading.size.{small,medium,large}` | 三檔直徑 |
| `loading.strokeWidth` | 載入環描邊寬 |
| `color.{primary,success,warning,danger,info}.default` | 五種環色 |

## 黃金用例對照

`cases/loading.json` 的 5 條斷言與 xUnit 測試的對應關係（`tests/LhLoadingTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `medium-size-uses-medium-token` | 中號消費 medium 直徑令牌 | `Medium_Size_Uses_Medium_Token` |
| `large-size-uses-large-token` | 大號消費 large 直徑令牌 | `Large_Size_Uses_Large_Token` |
| `primary-tone-uses-primary-token` | 主色調取主色令牌 | `Primary_Tone_Uses_Primary_Token` |
| `success-tone-uses-success-token` | 成功色調取成功色令牌 | `Success_Tone_Uses_Success_Token` |
| `render-emits-ring-command` | 有且僅有一條 RING 指令 | `Render_Emits_Ring_Command` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
