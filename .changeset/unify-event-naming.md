---
'Top.Lhgb.Ui.Windows': patch
---

# 事件命名统一为 LhOn* 前缀（对齐 core spec 契约）

此前事件命名三套风格混用：button/cell 用 `LhOnClick`、input/textarea 用 `LhOnFocus`+`ValueChange`、dialog/picker/tag/toast 用无前缀的 `Confirm/Cancel/Close`、checkbox/radio/switch 用 `Change`、navbar 用 `LeftClick/RightClick`。

统一映射（spec 驼峰 → C# PascalCase + `LhOn` 前缀）：

- `onChange` → `LhOnChange`（Checkbox / Radio / Switch）
- `onConfirm` / `onCancel` / `onClose` → `LhOnConfirm` / `LhOnCancel` / `LhOnClose`（Dialog / Picker / Tag / Toast）
- `onValueChange` → `LhOnValueChange`（Input / Textarea / Picker）
- `onLeftClick` / `onRightClick` → `LhOnLeftClick` / `LhOnRightClick`（Navbar）
- `LhOnClick` / `LhOnFocus` / `LhOnBlur` 不变

同步更新：组件实现（声明 + Invoke 点）、demo 订阅、Docs 页面与 Previews、`tests/`、`docs/components/*.md`、AGENTS.md 红线 6 的命名规范说明。

> 包未正式发包（0.0.1 未上架 NuGet），直接统一、无兼容负担；**按 breaking 记**——若有存量本地引用，订阅处事件名需同步改名。`dotnet build` 0 错误、`dotnet test` 126 测试全过。
