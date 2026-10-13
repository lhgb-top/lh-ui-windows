# Toast 轻提示

自持可见性与计时的墨色胶囊浮层。支持语义类型、自动隐藏与定位。

> 契约源：`lh-ui-core/specs/toast.json` ·
> 黄金用例：`lh-ui-core/cases/toast.json`（6 条，已翻译为 xUnit 测试）

## 基础用法

指令式接口（spec 的 show / hide / tick）在端侧落点为方法 + 内部计时：
`Show(text)` 使其可见并重置计时，`Hide()` 立即隐藏但不触发 `Close`。
计时由 `DispatcherTimer` 承载，到期自动隐藏并触发 `Close`。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast 基础</span></div>
  <div class="sim-screen">
    <div class="sim-toast">已保存</div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var controller = new LhToastController();
var toast = new LhToast
{
    Duration = 2000,
    Position = LhToastPosition.Center,
    Controller = controller,
};
toast.Close += () => { /* 计时到期自动隐藏后触发 */ };

// 宿主须把 toast 挂在页面 Grid 的最后子节点（层级 zIndex.toast）
controller.Show("已保存");
```

## 语义类型

`Type` 非 default 时在文字前绘制对应语义色圆点（直径取字号）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast 类型</span></div>
  <div class="sim-screen">
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--success"></span>保存成功</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--warning"></span>请检查网络连接</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--danger"></span>删除失败</div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## 定位

`Position` 决定在可用区域内的定位；上下两端留 `spacing.xl` 边距（center 时无留白）。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast 定位</span></div>
  <div class="sim-screen">
    <div class="sim-toast sim-toast--top">顶部提示</div>
    <div class="sim-toast sim-toast--bottom">底部提示</div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `Close` | 计时到期自动隐藏时触发；Hide() 主动隐藏不触发 | `event Action` | — |
| `Type` | 语义类型；非 default 时在文字前绘制对应语义色圆点 | `LhToastType` | `Default` |
| `Duration` | 自动隐藏时长（毫秒） | `int` | `2000` |
| `Position` | 在可用区域内的定位 | `LhToastPosition` | `Center` |
| `Controller` | 宿主调用句柄（可选；Loaded 后注册 Show/Hide） | `LhToastController?` | `null` |

### 方法

| 方法 | 说明 |
| --- | --- |
| `Show(string text)` | 显示提示文字并重置自动隐藏计时 |
| `Hide()` | 立即隐藏但不触发 Close |

### LhToastType

| 值 | 圆点色 |
| --- | --- |
| `Default` | 不绘制圆点 |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

### LhToastPosition

| 值 | 定位 |
| --- | --- |
| `Top` | 顶部（上端留 `spacing.xl` 边距） |
| `Center` | 居中（无留白） |
| `Bottom` | 底部（下端留 `spacing.xl` 边距） |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `toast.fontSize` | 文字字号（亦为色点直径） |
| `toast.paddingHorizontal` / `paddingVertical` | 胶囊内边距 |
| `toast.borderRadius` | 胶囊圆角 |
| `toast.gap` | 色点与文字间距 |
| `color.ink` | 胶囊底色 |
| `color.onPrimary` | 文字色 |
| `color.{success,warning,danger,info}.default` | 语义色点 |
| `zIndex.toast` | 浮层层级 |
| `spacing.xl` | 上下两端留白 |
| `typography.fontFamily` | 国风衬线字体栈 |

## 黄金用例对照

`cases/toast.json` 的 6 条断言与 xUnit 测试的对应关系（`tests/LhToastTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `show-makes-visible` | show 后可见并渲染文字 | `Show_Makes_Visible` |
| `auto-hides-after-duration` | 计时到期自动隐藏并触发 onClose | `Auto_Hides_After_Duration` |
| `tick-before-duration-keeps-visible` | 计时未到期保持可见 | `Tick_Before_Duration_Keeps_Visible` |
| `hide-is-silent` | hide 主动隐藏不触发 onClose | `Hide_Is_Silent` |
| `success-type-uses-success-token` | success 类型消费语义色与墨色令牌 | `Success_Type_Uses_Success_Token` |
| `hidden-emits-no-commands` | 不可见时不发射任何绘制指令 | `Hidden_Emits_No_Commands` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
