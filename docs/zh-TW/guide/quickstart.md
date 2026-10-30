# 快速開始

## 最小範例

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui;
using Top.Lhgb.Ui.Common;

public sealed class DemoPage : Page
{
    public DemoPage()
    {
        // 注入明色板；切暗色傳 true。所有 Lh 元件自動跟隨（LhTheme.ThemeChanged 熱重繪）
        LhTheme.Apply(false);

        var button = new LhButton
        {
            Type = LhButtonType.Primary,
            Text = "主要按鈕",
        };
        button.Click += OnButtonClick;

        Content = new StackPanel { Children = { button } };
    }

    private void OnButtonClick() => { /* 處理點選 */ }
}
```

`LhTheme.Apply(isDark)` 切換靜態色板並觸發 `ThemeChanged`，所有已載入的 Lh 元件自動重繪；不呼叫時回退明色板。

## 類型與尺寸

```csharp
// 六種類型
new LhButton { Text = "預設" };                                     // Default
new LhButton { Type = LhButtonType.Primary,   Text = "主要" };
new LhButton { Type = LhButtonType.Success,   Text = "成功" };
new LhButton { Type = LhButtonType.Warning,   Text = "警告" };
new LhButton { Type = LhButtonType.Danger,    Text = "危險" };
new LhButton { Type = LhButtonType.Info,      Text = "資訊" };

// 三種尺寸
new LhButton { Size = LhButtonSize.Small,  Text = "小型" };
new LhButton { Size = LhButtonSize.Medium, Text = "中型" };   // 預設
new LhButton { Size = LhButtonSize.Large,  Text = "大型" };
```

## 狀態

```csharp
// 停用：不回應點選（Tapped gate 攔截），呈現停用樣式
new LhButton { Disabled = true, Text = "停用" };

// 載入中：顯示 ProgressRing，不觸發 Click
new LhButton { Loading = true, Text = "載入中" };

// 兩者共存：按停用優先處理（黃金用例 disabled-precedes-loading）
new LhButton { Disabled = true, Loading = true, Text = "停用優先" };
```

## 圖示與花紋底

```csharp
// 圖示名按本機圖示體系解析（LhButtonTokens.IconFor，當前支援 search / refresh / check / close，
// 未登記名不繪製）
new LhButton { Type = LhButtonType.Primary, Icon = "search", Text = "搜尋" };

// 國風花紋底紋（消費 color.decoration 令牌）
new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = "花紋底" };
```

## 明暗主題

```csharp
LhTheme.Apply(true);  // 全域切暗色；所有元件下次外觀解析時自動跟隨
```

## 表單與反饋元件

```csharp
// 受控輸入：宿主監聽 ValueChange 並回寫 Value
var input = new LhInput { Placeholder = "請輸入關鍵字" };
input.ValueChange += value => input.Value = value;

// 核取方塊
var checkbox = new LhCheckbox { Label = "青花瓷" };
checkbox.Change += isChecked => { };

// 開關
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };

// 指令式輕提示：先放宿主控制項，再用控制器驅動
var toast = new LhToast();
toast.Close += () => { };
var panel = new StackPanel { Children = { toast } };
toast.Show("已儲存");   // 到期自動隱藏並觸發 Close
```

## 元件範例預覽

原生 UI 無法在瀏覽器即時渲染，本站用**內嵌靜態 HTML 手機框模擬器**展示外觀：

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>lh-ui-windows</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">預設按鈕</button>
      <button class="sim-btn sim-btn--primary">主要按鈕</button>
      <button class="sim-btn sim-btn--success">成功</button>
      <button class="sim-btn sim-btn--warning">警告</button>
      <button class="sim-btn sim-btn--danger">危險</button>
      <button class="sim-btn sim-btn--info">資訊</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">小型</button>
      <button class="sim-btn sim-btn--primary sim-btn--large">大型</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">停用</button>
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>載入中
      </button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>搜尋</button>
      <button class="sim-btn sim-btn--primary sim-btn--pattern">花紋底</button>
    </div>
  </div>
</div>
<p class="sim-note">Windows / WinUI 3 外觀的 HTML 模擬預覽，非真機渲染；樣式值全部取自 core 令牌快照，與真機同源同值。真機外觀以 demo 演示應用程式與裝置截圖為準。</p>

> 模擬器的暗色與站點聯動：右上角切換主題，手機框內顏色即變（暗色令牌由 core 預計算）。
