# Toast 輕提示

自持可見性與計時的墨色膠囊浮層。支援語意類型、自動隱藏與定位。

> 契約源：`lh-ui-core/specs/toast.json` ·
> 黃金用例：`lh-ui-core/cases/toast.json`（6 條，已翻譯為 xUnit 測試）

## 基礎用法

指令式介面（spec 的 show / hide / tick）在端側落點為方法 + 內部計時：
`Show(text)` 使其可見並重設計時，`Hide()` 立即隱藏但不觸發 `Close`。
計時由 `DispatcherTimer` 承載，到期自動隱藏並觸發 `Close`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-toast">已儲存</div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var controller = new LhToastController();
var toast = new LhToast
{
    Duration = 2000,
    Position = LhToastPosition.Center,
    Controller = controller,
};
toast.Close += () => { /* 計時到期自動隱藏後觸發 */ };

// 宿主須把 toast 掛在頁面 Grid 的最後子節點（層級 zIndex.toast）
controller.Show("已儲存");
```

## 語意類型

`Type` 非 default 時在文字前繪製對應語意色圓點（直徑取字級）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast 類型</span></div>
  <div class="sim-screen">
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--success"></span>儲存成功</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--warning"></span>請檢查網路連線</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--danger"></span>刪除失敗</div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 定位

`Position` 決定在可用區域內的定位；上下兩端留 `spacing.xl` 邊距（center 時無留白）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast 定位</span></div>
  <div class="sim-screen">
    <div class="sim-toast sim-toast--top">頂部提示</div>
    <div class="sim-toast sim-toast--bottom">底部提示</div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `Close` | 計時到期自動隱藏時觸發；Hide() 主動隱藏不觸發 | `event Action` | — |
| `Type` | 語意類型；非 default 時在文字前繪製對應語意色圓點 | `LhToastType` | `Default` |
| `Duration` | 自動隱藏時長（毫秒） | `int` | `2000` |
| `Position` | 在可用區域內的定位 | `LhToastPosition` | `Center` |
| `Controller` | 宿主呼叫句柄（可選；Loaded 後註冊 Show/Hide） | `LhToastController?` | `null` |

### 方法

| 方法 | 說明 |
| --- | --- |
| `Show(string text)` | 顯示提示文字並重設自動隱藏計時 |
| `Hide()` | 立即隱藏但不觸發 Close |

### LhToastType

| 值 | 圓點色 |
| --- | --- |
| `Default` | 不繪製圓點 |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

### LhToastPosition

| 值 | 定位 |
| --- | --- |
| `Top` | 頂部（上端留 `spacing.xl` 邊距） |
| `Center` | 置中（無留白） |
| `Bottom` | 底部（下端留 `spacing.xl` 邊距） |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `toast.fontSize` | 文字字級（亦為色點直徑） |
| `toast.paddingHorizontal` / `paddingVertical` | 膠囊內邊距 |
| `toast.borderRadius` | 膠囊圓角 |
| `toast.gap` | 色點與文字間距 |
| `color.ink` | 膠囊底色 |
| `color.onPrimary` | 文字色 |
| `color.{success,warning,danger,info}.default` | 語意色點 |
| `zIndex.toast` | 浮層層級 |
| `spacing.xl` | 上下兩端留白 |
| `typography.fontFamily` | 國風襯線字型棧 |

## 黃金用例對照

`cases/toast.json` 的 6 條斷言與 xUnit 測試的對應關係（`tests/LhToastTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `show-makes-visible` | show 後可見並渲染文字 | `Show_Makes_Visible` |
| `auto-hides-after-duration` | 計時到期自動隱藏並觸發 onClose | `Auto_Hides_After_Duration` |
| `tick-before-duration-keeps-visible` | 計時未到期保持可見 | `Tick_Before_Duration_Keeps_Visible` |
| `hide-is-silent` | hide 主動隱藏不觸發 onClose | `Hide_Is_Silent` |
| `success-type-uses-success-token` | success 類型消費語意色與墨色令牌 | `Success_Type_Uses_Success_Token` |
| `hidden-emits-no-commands` | 不可見時不發射任何繪製指令 | `Hidden_Emits_No_Commands` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
