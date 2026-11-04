---
layout: home

hero:
  image:
    src: /logo-banner.png
    alt: lh-ui 品牌横幅
  name: lh-ui-windows
  text: Linaje nativo · extremo Windows
  tagline: Biblioteca de componentes en C# + WinUI 3 (.NET 8 / Windows App SDK), controlada por tokens, alineada con los contratos y protegida por casos dorados
  actions:
    - theme: brand
      text: Inicio rápido
      link: /es/guide/quickstart
    - theme: alt
      text: Resumen de componentes
      link: /es/components/button

features:
  - title: Controlada por tokens
    details: "Los componentes no contienen literalmente ningún valor de color o de espaciado: todos consumen los LhTokens / LhDarkTokens generados por el codegen de core, de modo que cambiar un token surte efecto de inmediato en todo el extremo."
  - title: Consistencia de contratos
    details: El nombre de las props está alineado literalmente con lh-ui-core/specs y comparte el comportamiento con los extremos de Android / iOS / HarmonyOS y con el linaje web.
  - title: Protegida por casos dorados
    details: Las aserciones de comportamiento de cases/*.json se traducen a pruebas xUnit y se pueden regresar con un solo dotnet test.
  - title: Temas claro y oscuro
    details: Se alterna con una sola línea LhTheme.Apply (paleta estática + repintado en caliente mediante el evento ThemeChanged); los tokens oscuros están precalculados por core, sin ajuste de color en tiempo de ejecución.
  - title: Lenguaje de diseño de estilo chino
    details: Color principal azul 青花瓷 (porcelana azul y blanco), fondo 宣纸白 (blanco papel de arroz), toques 藤黄 (amarillo herbáceo) y 朱砂 (cinabrio), pila de fuentes con serifa; mismo origen y mismos valores que el linaje web.
  - title: WinUI 3 nativo
    details: UI escrita a mano puramente en C# + WinUI 3, sin runtime web de JS; un ciudadano de primera clase del sistema de diseño Fluent.
---
