# Temas y modo oscuro

## Modelo de tokens

La fuente única de tokens del linaje nativo reside en `lh-ui-core/tokens/*.json`; el codegen genera los artefactos de Windows (`src/Tokens/`, artefactos gitignore, sincronizados mediante `pnpm link:tokens`):

| Artefacto | Contenido | Forma oscura |
| --- | --- | --- |
| `LhTokens` | Completo en claro: tamaños de Button, paleta Color, Motion, Radius, Shadow, Spacing, Typography, ZIndex | — |
| `LhDarkTokens` | Solo la subclase `Color` (modelo de **objetos en paralelo**) | El modo oscuro solo sobreescribe los colores; tamaños / animaciones / radios no cambian en oscuro |
| `LhShadow` | Portador de la sombra (color/radius/offsetX/offsetY/spread) | — |
| `LhEasing` | Suavizado del movimiento | — |

> Esto coincide con android / ios / harmony (objetos en paralelo) y difiere de la forma del lado docs (fusión superficial de JSON).
> La capa de temas se diseño en consecuencia: el registro `LhColors` solo transporta colores; los tamaños los toma el componente directamente de `LhTokens`.

## LhColors

`LhColorsFactory.Light()` / `Dark()` aplanan las dos clases estáticas generadas en un registro inmutable:

```csharp
public sealed record LhColors
{
    public Color Primary { get; init; }
    public Color OnPrimary { get; init; }
    // ... todos los colores semánticos
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

Los métodos de fábrica son públicos: las pruebas xUnit pueden construir las paletas directamente para hacer aserciones (véase `Dark_Colors_Come_From_Dark_Tokens` en cada batería de pruebas); el lado de la aplicación las consume a través de `LhTheme`.

## LhTheme.Apply

```csharp
LhTheme.Apply(isDark);  // cambia la paleta y dispara ThemeChanged
```

Los componentes se suscriben a `LhTheme.ThemeChanged` en `Loaded` y se desuscriben en `Unloaded`; al recibir el evento llaman a `ApplyAppearance()` para recalcular y repintar — **repintado del tema en caliente, sin reconstruir el árbol de la página**. Si nunca se ha llamado a Apply, `LhTheme.Current` retrocede a la paleta clara.

## Precálculo del modo oscuro (importante)

El modo oscuro del linaje web se evalúa dinámicamente con `color-mix()` de CSS; al no haber cascada CSS en nativo, core ya ha **precalculado las expresiones oscuras
como literales** (constantes `Color` en formato `#AARRGGBB`). Consecuencia (plan maestro R-n6):

> **Al cambiar el color principal del preset, los valores precalculados del modo oscuro no lo siguen automáticamente**; es necesario volver a calcularlos a mano y actualizar
> `lh-ui-core/tokens/color-dark.json`, refrescando la *Línea base del lenguaje de diseño*.

## Convenciones específicas del lado Windows

- **La unidad de tamaño es epx**: `LhEpx.Px(token)` marca el valor del token como píxeles efectivos (los píxeles efectivos de WinUI dependen del
  factor de escala; se pasa siempre por la función de marca y no aparecen literales numéricos sueltos en los componentes).
- **El tipo del valor de color es `Windows.UI.Color`**: todos los campos de `LhColors` son `Color`; los pinceles los envuelve el componente en
  `SolidColorBrush`; el `#AARRGGBB` lo emite el codegen directamente como `Color.FromArgb(...)`.
- **El peso de la fuente hay que mapearlo**: el token `typography.fontWeight.*` es numérico, mientras que `FontWeights` de WinUI son
  constantes predefinidas; se pasa siempre por `LhFontWeight.FromToken(token)` (`Common/LhFontWeight.cs`).
- **La proyección va por Elevation**: la tarjeta de contenido de Dialog y el panel de Picker usan `Border.Elevation` +
  `ThemeShadow` para expresar la elevación (el lenguaje de Fluent: botones planos, capas flotantes con sombra).
- **Aspecto del foco**: el foco del campo de entrada usa el color `focus` + el borde de `input.focusRingWidth` (corresponde al
  marco de foco de alta visibilidad FocusVisualKind de WinUI).

## Consulta rápida de tokens

La tabla completa de tokens (claro / oscuro / descripción) está en `dist/docs/tokens.md` del repositorio core (se genera tras `pnpm codegen`).
La asignación de tokens a nivel de componente se encuentra en [Referencia de tokens de Button](/es/components/button#referencia-de-tokens).
