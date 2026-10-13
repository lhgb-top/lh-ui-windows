# 快速开始

## 最小示例

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui;
using Top.Lhgb.Ui.Common;

public sealed class DemoPage : Page
{
    public DemoPage()
    {
        // 注入明色板；切暗色传 true。所有 Lh 组件自动跟随（LhTheme.ThemeChanged 热重绘）
        LhTheme.Apply(false);

        var button = new LhButton
        {
            Type = LhButtonType.Primary,
            Text = "主要按钮",
        };
        button.Click += OnButtonClick;

        Content = new StackPanel { Children = { button } };
    }

    private void OnButtonClick() => { /* 处理点击 */ }
}
```

`LhTheme.Apply(isDark)` 切换静态色板并触发 `ThemeChanged`，所有已加载的 Lh 组件自动重绘；不调用时回退明色板。

## 类型与尺寸

```csharp
// 六种类型
new LhButton { Text = "默认" };                                     // Default
new LhButton { Type = LhButtonType.Primary,   Text = "主要" };
new LhButton { Type = LhButtonType.Success,   Text = "成功" };
new LhButton { Type = LhButtonType.Warning,   Text = "警告" };
new LhButton { Type = LhButtonType.Danger,    Text = "危险" };
new LhButton { Type = LhButtonType.Info,      Text = "信息" };

// 三种尺寸
new LhButton { Size = LhButtonSize.Small,  Text = "小型" };
new LhButton { Size = LhButtonSize.Medium, Text = "中型" };   // 默认
new LhButton { Size = LhButtonSize.Large,  Text = "大型" };
```

## 状态

```csharp
// 禁用：不响应点击（Tapped gate 拦截），呈现禁用样式
new LhButton { Disabled = true, Text = "禁用" };

// 加载中：显示 ProgressRing，不触发 Click
new LhButton { Loading = true, Text = "加载中" };

// 两者共存：按禁用优先处理（黄金用例 disabled-precedes-loading）
new LhButton { Disabled = true, Loading = true, Text = "禁用优先" };
```

## 图标与花纹底

```csharp
// 图标名按本地图标体系解析（LhButtonTokens.IconFor，当前支持 search / refresh / check / close，
// 未登记名不绘制）
new LhButton { Type = LhButtonType.Primary, Icon = "search", Text = "搜索" };

// 国风花纹底纹（消费 color.decoration 令牌）
new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = "花纹底" };
```

## 明暗主题

```csharp
LhTheme.Apply(true);  // 全局切暗色；所有组件下次外观解析时自动跟随
```

## 表单与反馈组件

```csharp
// 受控输入：宿主监听 ValueChange 并回写 Value
var input = new LhInput { Placeholder = "请输入关键字" };
input.ValueChange += value => input.Value = value;

// 复选框
var checkbox = new LhCheckbox { Label = "青花瓷" };
checkbox.Change += isChecked => { };

// 开关
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };

// 指令式轻提示：先放宿主控件，再用控制器驱动
var toast = new LhToast();
toast.Close += () => { };
var panel = new StackPanel { Children = { toast } };
toast.Show("已保存");   // 到期自动隐藏并触发 Close
```

## 组件示例预览

原生 UI 无法在浏览器实时渲染，本站用**内联静态 HTML 手机框模拟器**展示外观：

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>lh-ui-windows</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">默认按钮</button>
      <button class="sim-btn sim-btn--primary">主要按钮</button>
      <button class="sim-btn sim-btn--success">成功</button>
      <button class="sim-btn sim-btn--warning">警告</button>
      <button class="sim-btn sim-btn--danger">危险</button>
      <button class="sim-btn sim-btn--info">信息</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">小型</button>
      <button class="sim-btn sim-btn--primary sim-btn--large">大型</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">禁用</button>
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>加载中
      </button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>搜索</button>
      <button class="sim-btn sim-btn--primary sim-btn--pattern">花纹底</button>
    </div>
  </div>
</div>
<p class="sim-note">Windows / WinUI 3 外观的 HTML 模拟预览，非真机渲染；样式值全部取自 core 令牌快照，与真机同源同值。真机外观以 demo 演示应用与设备截图为准。</p>

> 模拟器的暗色与站点联动：右上角切换主题，手机框内颜色即变（暗色令牌由 core 预计算）。
