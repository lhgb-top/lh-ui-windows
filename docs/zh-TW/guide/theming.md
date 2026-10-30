# 主題與暗色

## 令牌模型

原生血脈的令牌真源在 `lh-ui-core/tokens/*.json`，codegen 生成 Windows 產物（`src/Tokens/`，生成物 gitignore，由 `pnpm link:tokens` 同步）：

| 產物 | 內容 | 暗色形態 |
| --- | --- | --- |
| `LhTokens` | 明色全量：Button 尺寸、Color 色板、Motion、Radius、Shadow、Spacing、Typography、ZIndex | — |
| `LhDarkTokens` | 僅 `Color` 子類（**並列物件**模型） | 暗色只覆蓋顏色，尺寸/動效/圓角暗色不變 |
| `LhShadow` | 陰影載體（color/radius/offsetX/offsetY/spread） | — |
| `LhEasing` | 動效緩動 | — |

> 這與 android / ios / harmony 一致（並列物件），與 docs 端（淺合併 JSON）形態不同。
> 主題層據此設計：`LhColors` 記錄只承載顏色，尺寸由元件直接取 `LhTokens`。

## LhColors

`LhColorsFactory.Light()` / `Dark()` 把生成的兩個靜態類平鋪為一份不可變記錄：

```csharp
public sealed record LhColors
{
    public Color Primary { get; init; }
    public Color OnPrimary { get; init; }
    // ... 全部語意色
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

工廠方法公開：xUnit 單測可直接構造色板做斷言（見各測試的 `Dark_Colors_Come_From_Dark_Tokens`）；應用側透過 `LhTheme` 消費。

## LhTheme.Apply

```csharp
LhTheme.Apply(isDark);  // 切換色板並觸發 ThemeChanged
```

元件在 `Loaded` 時訂閱 `LhTheme.ThemeChanged`、`Unloaded` 時退訂，收到事件後呼叫 `ApplyAppearance()` 重算並重繪——**主題熱重繪，無需重建頁面樹**。未 Apply 過時 `LhTheme.Current` 回退明色板。

## 暗色預計算（重要）

web 血脈的暗色用 CSS `color-mix()` 動態求值，原生無 CSS 級聯，故 core 已把暗色表達式
**預計算為字面量**（`#AARRGGBB` 形式的 `Color` 常數）。後果（總綱 R-n6）：

> **改預設主色時，暗色預計算值不會自動跟隨**，必須重新人工計算並更新
> `lh-ui-core/tokens/color-dark.json`，同步重新整理《設計語言基線》。

## Windows 端特有約定

- **尺寸單位是 epx**：`LhEpx.Px(token)` 把令牌數值標記為有效像素（WinUI 的有效像素與
  縮放因子相關，統一走標記函式，元件內不出現裸數字字面量）。
- **色值型別是 `Windows.UI.Color`**：`LhColors` 欄位全部是 `Color`，畫刷由元件包成
  `SolidColorBrush`；`#AARRGGBB` 由 codegen 直接輸出為 `Color.FromArgb(...)`。
- **字重需映射**：令牌的 `typography.fontWeight.*` 是數字，WinUI 的 `FontWeights` 是
  預定義常數，統一走 `LhFontWeight.FromToken(token)`（`Common/LhFontWeight.cs`）。
- **投影走 Elevation**：Dialog 內容卡 / Picker 面板用 `Border.Elevation` +
  `ThemeShadow` 表達層級（Fluent 語言：按鈕扁平、浮層有投影）。
- **聚焦視覺**：輸入框聚焦用 `focus` 色 + `input.focusRingWidth` 描邊（對應 WinUI 的
  FocusVisualKind 高可見聚焦框）。

## 令牌速查

完整令牌表（明/暗/說明）見 core 倉 `dist/docs/tokens.md`（`pnpm codegen` 後生成）。
元件級令牌映射見 [Button 令牌對照](/zh-TW/components/button#令牌對照)。
