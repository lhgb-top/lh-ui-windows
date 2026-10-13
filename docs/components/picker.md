# Picker 选择器

底部弹出的单列滚轮。支持候选项、索引钳制与工具栏。

> 契约源：`lh-ui-core/specs/picker.json` ·
> 黄金用例：`lh-ui-core/cases/picker.json`（8 条，已翻译为 xUnit 测试）

## 基础用法

声明式可见性：`Visible` 为 false 时不渲染任何内容；确认/取消后触发 `Close`，
宿主据此置 `Visible` 为 false。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Picker 基础</span></div>
  <div class="sim-screen">
    <div class="sim-picker-overlay"></div>
    <div class="sim-picker">
      <div class="sim-picker__toolbar">
        <span class="sim-picker__cancel">取消</span>
        <span class="sim-picker__title">选择</span>
        <span class="sim-picker__confirm">确认</span>
      </div>
      <div class="sim-picker__wheel">
        <span class="sim-picker__item">青花瓷</span>
        <span class="sim-picker__item sim-picker__item--active">粉彩瓷</span>
        <span class="sim-picker__item">玲珑瓷</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var picker = new LhPicker
{
    Options = new[] { "青花瓷", "粉彩瓷", "玲珑瓷" },
    Value = 1,
    Title = "选择",
};
picker.ValueChange += index => { /* 滚轮停止后的选中索引 */ };
picker.Confirm += index => { /* 当前选中索引 */ };
picker.Close += () => picker.Visible = false;

picker.Visible = true;
```

## 索引钳制

`Value` 越界时钳制到候选项范围内：负数取 0，大于候选项数 - 1 取最后一项。

```csharp
Assert.Equal(0, LhPickerTokens.ClampIndex(-1, 3));   // 负数取 0
Assert.Equal(2, LhPickerTokens.ClampIndex(100, 3));  // 越界取最后一项
```

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `ValueChange` | 滚轮停止且选中项变化时触发 | `event Action<int>` | — |
| `Confirm` | 点击确认按钮时触发，携带当前选中索引 | `event Action<int>` | — |
| `Cancel` | 点击取消按钮时触发 | `event Action` | — |
| `Close` | 确认/取消后触发，宿主据此置 Visible 为 false | `event Action` | — |
| `Visible` | 是否显示；为 false 时不渲染任何内容 | `bool` | `false` |
| `Options` | 候选项文本数组（单列） | `string[]?` | `null` |
| `Value` | 选中项索引（0 基）；越界时钳制到候选项范围内 | `int` | `0` |
| `Title` | 工具栏中间的标题 | `string` | `""` |
| `ConfirmText` | 确认按钮文字 | `string` | `"确认"` |
| `CancelText` | 取消按钮文字 | `string` | `"取消"` |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `picker.height` | 面板高度 |
| `picker.toolbarHeight` | 顶部工具栏高度（滚轮区高度 = 面板高度 - 工具栏高度） |
| `picker.itemHeight` | 候选项高度（中央指示器间距同此） |
| `picker.itemFontSize` | 候选项与工具栏字号 |
| `picker.borderRadius` | 面板顶部圆角 |
| `color.background.default` | 面板底色 |
| `color.background.overlay` | 遮罩色 |
| `color.text.primary` | 标题与候选项色 |
| `color.text.secondary` | 取消按钮色 |
| `color.primary.default` | 确认按钮色 |
| `color.border.lighter` | 中央指示器线色 |
| `zIndex.modal` / `zIndex.modalBackdrop` | 面板/遮罩层级 |

## 黄金用例对照

`cases/picker.json` 的 8 条断言与 xUnit 测试的对应关系（`tests/LhPickerTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `confirm-emits-onconfirm-with-value` | 确认携带当前选中索引 | `Confirm_Emits_OnConfirm_With_Value` |
| `confirm-also-emits-onclose` | 确认同时触发 onClose | `Confirm_Also_Emits_OnClose` |
| `cancel-emits-oncancel` | 取消触发 onCancel | `Cancel_Emits_OnCancel` |
| `scroll-emits-onvaluechange` | 滚轮停止触发 onValueChange | `Scroll_Emits_OnValueChange` |
| `clamp-negative-value-to-zero` | 负数索引钳制到 0 | `Clamp_Negative_Value_To_Zero` |
| `clamp-overflow-value-to-last` | 越界索引钳制到最后一项 | `Clamp_Overflow_Value_To_Last` |
| `title-renders-text` | 标题按字号令牌渲染 | `Title_Renders_Text` |
| `hidden-not-rendered` | 不可见时不渲染任何内容 | `Hidden_Not_Rendered` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
