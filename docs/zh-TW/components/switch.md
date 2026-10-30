# Switch 開關

開關。膠囊軌道 + 圓形滑塊，點選切換；支援停用與載入中。

> 契約源：`lh-ui-core/specs/switch.json` ·
> 黃金用例：`lh-ui-core/cases/switch.json`（6 條，已翻譯為 xUnit 測試）

## 基礎用法

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };
```

## 尺寸

三檔尺寸映射到 `switch.width` / `switch.height` 令牌；滑塊直徑 = 高度 - 2 × `switch.gap`。

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
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 狀態

`Disabled` 與 `Loading` 都不回應點選；`Loading` 時滑塊位置改畫主色載入環。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch 狀態</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--checked sim-switch--disabled"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--loading"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `Change` | 開關態變化回呼；disabled 或 loading 時不觸發 | `event Action<bool>` | — |
| `Checked` | 是否開啟 | `bool` | `false` |
| `Disabled` | 是否停用（不回應點選且呈現停用樣式） | `bool` | `false` |
| `Loading` | 是否載入中（滑塊位置顯示載入環且不回應點選） | `bool` | `false` |
| `Size` | 開關尺寸，映射到 `switch.width` / `switch.height` 令牌 | `LhSwitchSize` | `Medium` |

### LhSwitchSize

| 值 | width | height |
| --- | --- | --- |
| `Small` | `switch.width.small`（40epx） | `switch.height.small`（22epx） |
| `Medium` | `switch.width.medium`（48epx） | `switch.height.medium`（26epx） |
| `Large` | `switch.width.large`（56epx） | `switch.height.large`（30epx） |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `switch.width.{small,medium,large}` | 三檔軌道寬度 |
| `switch.height.{small,medium,large}` | 三檔軌道高度 |
| `switch.gap` | 滑塊與軌道留白（滑塊直徑 = 高度 - 2 × gap） |
| `color.primary.default` | 開啟軌道色 |
| `color.border.base` | 關閉軌道色 |
| `color.onPrimary` | 滑塊色 |
| `color.disabled.bg` / `color.text.disabled` | 停用軌道/滑塊色 |

## 黃金用例對照

`cases/switch.json` 的 6 條斷言與 xUnit 測試的對應關係（`tests/LhSwitchTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | 點選切換並觸發 onChange | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-turns-off` | 開啟態點選關閉 | `Tap_Checked_Turns_Off` |
| `disabled-blocks-change` | 停用態不可互動 | `Disabled_Blocks_Change` |
| `loading-blocks-change` | 載入態不可互動 | `Loading_Blocks_Change` |
| `checked-uses-primary-token` | 開啟態消費主色令牌 | `Checked_Uses_Primary_Token` |
| `unchecked-uses-border-token` | 關閉態消費描邊令牌 | `Unchecked_Uses_Border_Token` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
