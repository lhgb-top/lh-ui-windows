# 介紹

**lh-ui-windows** 是 lh-ui 家族**原生血脈**的 Windows 端實現：基於 **C# + WinUI 3**（.NET 8，Windows App SDK）的 UI 元件庫。

## 家族中的位置

```
                        ┌── web 血脈：share（TS core + SCSS）→ plus / vue2 / react / uniapp
lh-ui 家族（同品牌）─────┤
                        └── 原生血脈：core（JSON 令牌 + 契約 + codegen）→ android / ios / harmony / windows
                                                                    ↑ 本倉
```

- **構建期中心**：`lh-ui-core` 是原生血脈的令牌真源（`tokens/*.json`）、元件契約（`specs/`）、黃金用例（`cases/`）與 codegen 工具。它生成 C# 令牌骨架（`LhTokens` / `LhDarkTokens` / `LhShadow` / `LhEasing`），**不生成任何元件實現**。
- **本倉職責**：手寫 WinUI 3 元件、主題接入（`LhTheme.Apply` + `ThemeChanged` 事件）、屬性枚舉與預設值綁定、把黃金用例翻譯為 xUnit 測試。
- **與 web 血脈互相獨立**：不依賴 `@lh-ui/share`，發版互不約束。視覺一致性靠《設計語言基線》人工對齊。

> **歷史**：本倉於 2026-10-12 取代此前的 `lh-ui-c`（C/C++ 嵌入式端）——原生血脈的第四名成員由嵌入式端改為 Windows 桌面端（總綱 D10）。

## 設計語言

國風主題：青花瓷藍主色（`#2B4C7C`）、宣紙白底（`#FAF6F0`）、松花綠成功、藤黃警告、朱砂危險，襯線字型棧（Noto Serif SC → STSong → SimSun）。所有視覺值都是令牌，元件不出現任何字面量。視覺表達上以 Fluent 的幾何節制收口（令牌驅動的圓角層級與細描邊）。

## 為什麼是 WinUI 3

- Windows App SDK 的現代宣告式 UI，與 Compose / SwiftUI / ArkUI 心智同構，跨端契約容易對齊
- 無 Web 執行時、無 HTML/CSS 橋接，原生效能
- NuGet 分發（nM6 打通後），.NET 生態一鍵整合

## 當前狀態

- **P0 全 15 元件已交付**（令牌消費 + resolve 純函式外觀解析 + WinUI 3 控制項 + xUnit 黃金用例 + demo 演示應用程式 + 文件站）：button / icon / tag / cell / text / input / textarea / checkbox / radio / switch / picker / dialog / toast / loading / navbar
