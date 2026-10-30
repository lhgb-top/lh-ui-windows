# Introducción

**lh-ui-windows** es la implementación para Windows del **linaje nativo** de la familia lh-ui: una biblioteca de componentes de UI basada en **C# + WinUI 3** (.NET 8, Windows App SDK).

## Posición en la familia

```
                        ┌── linaje web: share (core TS + SCSS) → plus / vue2 / react / uniapp
familia lh-ui (una marca)├────┤
                        └── linaje nativo: core (tokens JSON + contratos + codegen) → android / ios / harmony / windows
                                                                    ↑ este repositorio
```

- **Centro de compilación**: `lh-ui-core` es la fuente única de tokens del linaje nativo (`tokens/*.json`), los contratos de los componentes (`specs/`), los casos dorados (`cases/`) y las herramientas de codegen. Genera los esqueletos de tokens de C# (`LhTokens` / `LhDarkTokens` / `LhShadow` / `LhEasing`), pero **no genera ninguna implementación de componentes**.
- **Responsabilidades de este repositorio**: componentes WinUI 3 escritos a mano, integración de temas (`LhTheme.Apply` + evento `ThemeChanged`), enums de props y vinculación de valores predeterminados, y traducción de los casos dorados a pruebas xUnit.
- **Independencia del linaje web**: no depende de `@lh-ui/share` y las liberaciones no se condicionan entre sí. La consistencia visual se mantiene mediante la alineación manual con la *Línea base del lenguaje de diseño*.

> **Historia**: este repositorio sustituyó el 2026-10-12 al anterior `lh-ui-c` (extremo embebido en C/C++) — el cuarto miembro del linaje nativo pasó de ser un extremo embebido al extremo de escritorio de Windows (plan maestro D10).

## Lenguaje de diseño

Temática de estilo chino: color principal azul 青花瓷 (porcelana azul y blanco) (`#2B4C7C`), fondo 宣纸白 (blanco papel de arroz) (`#FAF6F0`), éxito en 松花绿 (verde pino), advertencia 藤黄 (amarillo herbáceo), peligro 朱砂 (cinabrio) y una pila de fuentes con serifa (Noto Serif SC → STSong → SimSun). Todos los valores visuales son tokens: no aparece ningún literal en los componentes. Visualmente, la expresión se ciñe a la contención geométrica de Fluent (una jerarquía de radios de esquina controlada por tokens y trazos finos).

## Por qué WinUI 3

- La UI declarativa moderna del Windows App SDK, mentalmente isomorfa con Compose / SwiftUI / ArkUI, lo que facilita la alineación de los contratos entre plataformas.
- Sin runtime web, sin puente HTML/CSS: rendimiento nativo.
- Distribución mediante NuGet (una vez que esté lista la cadena de publicación nM6), integración en un paso en el ecosistema .NET.

## Estado actual

- **Los 15 componentes P0 ya están entregados** (consumo de tokens + resolución del aspecto con funciones puras resolve + controles WinUI 3 + casos dorados xUnit + una app de demostración + este sitio de documentación): button / icon / tag / cell / text / input / textarea / checkbox / radio / switch / picker / dialog / toast / loading / navbar.
