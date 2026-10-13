# 主题与暗色

## 令牌模型

原生血脉的令牌真源在 `lh-ui-core/tokens/*.json`，codegen 生成 Windows 产物（`src/Tokens/`，生成物 gitignore，由 `pnpm link:tokens` 同步）：

| 产物 | 内容 | 暗色形态 |
| --- | --- | --- |
| `LhTokens` | 明色全量：Button 尺寸、Color 色板、Motion、Radius、Shadow、Spacing、Typography、ZIndex | — |
| `LhDarkTokens` | 仅 `Color` 子类（**并列对象**模型） | 暗色只覆盖颜色，尺寸/动效/圆角暗色不变 |
| `LhShadow` | 阴影载体（color/radius/offsetX/offsetY/spread） | — |
| `LhEasing` | 动效缓动 | — |

> 这与 android / ios / harmony 一致（并列对象），与 docs 端（浅合并 JSON）形态不同。
> 主题层据此设计：`LhColors` 记录只承载颜色，尺寸由组件直接取 `LhTokens`。

## LhColors

`LhColorsFactory.Light()` / `Dark()` 把生成的两个静态类平铺为一份不可变记录：

```csharp
public sealed record LhColors
{
    public Color Primary { get; init; }
    public Color OnPrimary { get; init; }
    // ... 全部语义色
}

public static class LhColorsFactory
{
    public static LhColors Light() => new()
    {
        Primary = LhTokens.Color.Primary.Default,
        // ...
    };

    public static LhColors Dark() => new()
    {
        Primary = LhDarkTokens.Color.Primary.Default,
        // ...
    };
}
```

工厂方法公开：xUnit 单测可直接构造色板做断言（见各测试的 `Dark_Colors_Come_From_Dark_Tokens`）；应用侧通过 `LhTheme` 消费。

## LhTheme.Apply

```csharp
LhTheme.Apply(isDark);  // 切换色板并触发 ThemeChanged
```

组件在 `Loaded` 时订阅 `LhTheme.ThemeChanged`、`Unloaded` 时退订，收到事件后调用 `ApplyAppearance()` 重算并重绘——**主题热重绘，无需重建页面树**。未 Apply 过时 `LhTheme.Current` 回退明色板。

## 暗色预计算（重要）

web 血脉的暗色用 CSS `color-mix()` 动态求值，原生无 CSS 级联，故 core 已把暗色表达式
**预计算为字面量**（`#AARRGGBB` 形式的 `Color` 常量）。后果（总纲 R-n6）：

> **改预设主色时，暗色预计算值不会自动跟随**，必须重新人工计算并更新
> `lh-ui-core/tokens/color-dark.json`，同步刷新《设计语言基线》。

## Windows 端特有约定

- **尺寸单位是 epx**：`LhEpx.Px(token)` 把令牌数值标记为有效像素（WinUI 的有效像素与
  缩放因子相关，统一走标记函数，组件内不出现裸数字字面量）。
- **色值类型是 `Windows.UI.Color`**：`LhColors` 字段全部是 `Color`，画刷由组件包成
  `SolidColorBrush`；`#AARRGGBB` 由 codegen 直接输出为 `Color.FromArgb(...)`。
- **字重需映射**：令牌的 `typography.fontWeight.*` 是数字，WinUI 的 `FontWeights` 是
  预定义常量，统一走 `LhFontWeight.FromToken(token)`（`Common/LhFontWeight.cs`）。
- **投影走 Elevation**：Dialog 内容卡 / Picker 面板用 `Border.Elevation` +
  `ThemeShadow` 表达层级（Fluent 语言：按钮扁平、浮层有投影）。
- **聚焦视觉**：输入框聚焦用 `focus` 色 + `input.focusRingWidth` 描边（对应 WinUI 的
  FocusVisualKind 高可见聚焦框）。

## 令牌速查

完整令牌表（明/暗/说明）见 core 仓 `dist/docs/tokens.md`（`pnpm codegen` 后生成）。
组件级令牌映射见 [Button 令牌对照](/components/button#令牌对照)。
