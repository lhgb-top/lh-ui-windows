---
layout: home

hero:
  image:
    src: /logo-banner.png
    alt: lh-ui 品牌横幅
  name: lh-ui-windows
  text: 原生血脈 · Windows 端
  tagline: C# + WinUI 3 元件庫（.NET 8 / Windows App SDK），令牌驅動、契約對齊、黃金用例守護
  actions:
    - theme: brand
      text: 快速開始
      link: /zh-TW/guide/quickstart
    - theme: alt
      text: 元件總覽
      link: /zh-TW/components/button

features:
  - title: 令牌驅動
    details: 元件零硬編碼色值與間距，全部消費 core codegen 生成的 LhTokens / LhDarkTokens，改令牌全端即時生效。
  - title: 契約一致
    details: 屬性命名與 lh-ui-core/specs 逐字對齊，與 Android / iOS / 鴻蒙端及 web 血脈行為共享。
  - title: 黃金用例守護
    details: cases/*.json 的行為斷言翻譯為 xUnit 測試，dotnet test 一鍵回歸。
  - title: 明暗雙主題
    details: LhTheme.Apply 一行切換（靜態色板 + ThemeChanged 事件熱重繪），暗色令牌由 core 預計算，無執行時調色。
  - title: 國風設計語言
    details: 青花瓷藍主色、宣紙白底、藤黃朱砂點綴，襯線字型棧，與 web 血脈同源同值。
  - title: WinUI 3 原生
    details: 純 C# + WinUI 3 手寫 UI，無 JS Web 執行時，Fluent 設計體系的一等公民。
---
