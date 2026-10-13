---
layout: home

hero:
  name: lh-ui-windows
  text: 原生血脉 · Windows 端
  tagline: C# + WinUI 3 组件库（.NET 8 / Windows App SDK），令牌驱动、契约对齐、黄金用例守护
  actions:
    - theme: brand
      text: 快速开始
      link: /guide/quickstart
    - theme: alt
      text: 组件总览
      link: /components/button

features:
  - title: 令牌驱动
    details: 组件零硬编码色值与间距，全部消费 core codegen 生成的 LhTokens / LhDarkTokens，改令牌全端即时生效。
  - title: 契约一致
    details: 属性命名与 lh-ui-core/specs 逐字对齐，与 Android / iOS / 鸿蒙端及 web 血脉行为共享。
  - title: 黄金用例守护
    details: cases/*.json 的行为断言翻译为 xUnit 测试，dotnet test 一键回归。
  - title: 明暗双主题
    details: LhTheme.Apply 一行切换（静态色板 + ThemeChanged 事件热重绘），暗色令牌由 core 预计算，无运行时调色。
  - title: 国风设计语言
    details: 青花瓷蓝主色、宣纸白底、藤黄朱砂点缀，衬线字体栈，与 web 血脉同源同值。
  - title: WinUI 3 原生
    details: 纯 C# + WinUI 3 手写 UI，无 JS Web 运行时，Fluent 设计体系的一等公民。
---
