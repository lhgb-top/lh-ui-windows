# Dialog 对话框

模态对话框。遮罩 + 居中内容卡 + 确认/取消按钮，支持语义类型与遮罩关闭。

> 契约源：`lh-ui-core/specs/dialog.json` ·
> 黄金用例：`lh-ui-core/cases/dialog.json`（8 条，已翻译为 xUnit 测试）

## 基础用法

声明式可见性：`Visible` 为 false 时不渲染任何内容；确认/取消后触发 `Close`，
宿主据此置 `Visible` 为 false。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog 基础</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">删除确认</div>
      <div class="sim-dialog__content">删除后不可恢复，确认删除这条记录吗？</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">取消</button>
        <button class="sim-btn sim-btn--danger">确认</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var dialog = new LhDialog
{
    Title = "删除确认",
    Content = "删除后不可恢复，确认删除这条记录吗？",
    Type = LhDialogType.Danger,
    ConfirmText = "确认",
    CancelText = "取消",
};
dialog.Confirm += () => { /* 执行删除 */ };
dialog.Close += () => dialog.Visible = false;

dialog.Visible = true;
```

## 语义类型

`Type` 决定确认按钮的类型（颜色）；取消按钮固定 default 类型。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog 类型</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">提示</div>
      <div class="sim-dialog__content">这是一条主色确认对话框</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">取消</button>
        <button class="sim-btn sim-btn--primary">确认</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `Confirm` | 确认时触发 | `event Action` | — |
| `Cancel` | 取消时触发 | `event Action` | — |
| `Close` | 确认/取消后或（closable 时）点击遮罩触发，宿主据此置 Visible 为 false | `event Action` | — |
| `Visible` | 是否显示；为 false 时不渲染任何内容 | `bool` | `false` |
| `Title` | 标题 | `string` | `""` |
| `Content` | 正文内容 | `string` | `""` |
| `Type` | 语义类型，决定确认按钮的类型（颜色） | `LhDialogType` | `Default` |
| `ConfirmText` | 确认按钮文字；为空时不显示确认按钮 | `string` | `"确认"` |
| `CancelText` | 取消按钮文字；为空时不显示取消按钮 | `string` | `"取消"` |
| `Closable` | 点击遮罩区域是否触发 Close | `bool` | `false` |

### LhDialogType

| 值 | 确认按钮类型 |
| --- | --- |
| `Default` | `LhButtonType.Default` |
| `Primary` | `LhButtonType.Primary` |
| `Success` | `LhButtonType.Success` |
| `Warning` | `LhButtonType.Warning` |
| `Danger` | `LhButtonType.Danger` |
| `Info` | `LhButtonType.Info` |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `dialog.width` / `dialog.borderRadius` | 内容卡宽度与圆角 |
| `dialog.paddingHorizontal` / `paddingVertical` | 内容卡内边距 |
| `dialog.titleFontSize` / `contentFontSize` | 标题与正文字号 |
| `dialog.gap` | 标题/内容/按钮区间距 |
| `color.background.default` | 内容卡底色 |
| `color.background.overlay` | 遮罩色 |
| `color.text.primary` / `color.text.regular` | 标题/正文色 |
| `zIndex.modal` / `zIndex.modalBackdrop` | 内容卡/遮罩层级 |
| `shadow.base` | 内容卡投影 |

## 黄金用例对照

`cases/dialog.json` 的 8 条断言与 xUnit 测试的对应关系（`tests/LhDialogTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `confirm-emits-onconfirm` | 确认触发 onConfirm | `Confirm_Emits_OnConfirm` |
| `cancel-emits-oncancel` | 取消触发 onCancel | `Cancel_Emits_OnCancel` |
| `confirm-also-emits-onclose` | 确认同时触发 onClose | `Confirm_Also_Emits_OnClose` |
| `closable-overlay-emits-onclose` | closable 时遮罩点击触发 onClose | `Closable_Overlay_Emits_OnClose` |
| `not-closable-blocks-overlay-close` | 非 closable 时遮罩不关闭 | `Not_Closable_Blocks_Overlay_Close` |
| `hidden-not-rendered` | 不可见时不渲染任何内容 | `Hidden_Not_Rendered` |
| `title-renders-text` | 标题与正文按字号令牌渲染 | `Title_Renders_Text` |
| `danger-type-uses-danger-token` | danger 类型消费危险色令牌 | `Danger_Type_Uses_Danger_Token` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
