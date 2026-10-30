# Icono

Componente de icono. Resuelve el nombre del icono contra el sistema gráfico local para renderizar un glifo, y admite tamaño y tono semántico.

> Fuente del contrato: `lh-ui-core/specs/icon.json` ·
> Casos dorados: `lh-ui-core/cases/icon.json` (5 casos, traducidos a pruebas xUnit)

::: tip Notas de plataforma
El lado Windows usa actualmente glifos unicode de marcador de posición (`LhIconTokens.GlyphFor`, renderizado con TextBlock); en producción deberían sustituirse por
`FontIcon` (activos vectoriales de Segoe Fluent Icons); esto coincide con la estrategia de marcador de posición de button. Los nombres de icono registrados aparecen en la sección «Nombres de los iconos» más abajo.
:::

## Tamaños

Tres tamaños se mapean al token `icon.size`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tamaños de Icon</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--small">⌕</span>
      <span class="sim-icon">⌕</span>
      <span class="sim-icon sim-icon--large">⌕</span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var icon = new LhIcon
{
    Name = "search",
    Size = LhIconSize.Large,
};
```

## Tono

`Tone` determina el color del glifo y se corresponde uno a uno con los tokens de colores semánticos; el tono predeterminado toma `color.text.primary`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tono de Icon</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">✓ Predeterminado</span>
      <span class="sim-icon sim-icon--primary">✓ Principal</span>
      <span class="sim-icon sim-icon--success">✓ Éxito</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--warning">⚠ Advertencia</span>
      <span class="sim-icon sim-icon--danger">✕ Peligro</span>
      <span class="sim-icon sim-icon--info">ℹ Información</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--regular">✓ Regular</span>
      <span class="sim-icon sim-icon--secondary">✓ Secundario</span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Nombres de los iconos

Nombres de icono registrados actualmente en `GlyphFor` y sus glifos: `search` ⌕, `refresh` ↻, `check` ✓, `close` ✕,
`warning` ⚠, `info` ℹ, `back` ←, `more` ⋯, `star` ★, `edit` ✎, `delete` ⌫,
`plus` ＋, `minus` －, `arrow` →. Los nombres no registrados caen al glifo `◆`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Iconos registrados</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">⌕</span><span class="sim-icon">↻</span><span class="sim-icon">✓</span>
      <span class="sim-icon">✕</span><span class="sim-icon">⚠</span><span class="sim-icon">ℹ</span>
      <span class="sim-icon">←</span><span class="sim-icon">⋯</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon">★</span><span class="sim-icon">✎</span><span class="sim-icon">⌫</span>
      <span class="sim-icon">＋</span><span class="sim-icon">－</span><span class="sim-icon">→</span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Temas claro y oscuro

Cambia el tema del sitio en la esquina superior derecha y los colores de los iconos dentro del marco del simulador cambian; en el dispositivo, se alterna con `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Iconos oscuros</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--primary">★</span>
      <span class="sim-icon sim-icon--success">✓</span>
      <span class="sim-icon sim-icon--warning">⚠</span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo; los valores claro/oscuro provienen de la instantánea de tokens de core</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `Name` | Nombre del icono (resuelto por la tabla de iconos local; los nombres no registrados caen a `◆`) | `string` | `""` |
| `Size` | Tamaño del icono, mapeado a los tokens `icon.size.*` | `LhIconSize` | `Medium` |
| `Tone` | Tono del icono, mapeado a los tokens de colores semánticos / de texto | `LhIconTone` | `Regular` |

### LhIconSize

| Valor | Token de tamaño |
| --- | --- |
| `Small` | `icon.size.small` (16epx) |
| `Medium` | `icon.size.medium` (18epx) |
| `Large` | `icon.size.large` (20epx) |

### LhIconTone

| Valor | Token de color |
| --- | --- |
| `Default` | `color.text.primary` |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `icon.size.{small,medium,large}` | Tres tamaños |
| `color.text.{primary,regular,secondary}` | Tonos neutros |
| `color.{primary,success,warning,danger,info}.default` | Tonos semánticos |
| `typography.fontFamily` | Pila de fuentes del glifo |

## Referencia de casos dorados

Cómo se mapean las 5 aserciones de `cases/icon.json` a las pruebas xUnit (`tests/LhIconTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `click-emits-onclick` | La barrera deja pasar (el icono es interactivo) | `Click_Emits_OnClick` |
| `default-tone-uses-text-primary` | El tono predeterminado toma el token de texto principal | `Default_Tone_Uses_Text_Primary` |
| `primary-tone-uses-primary-token` | El tono primary toma el token del color principal | `Primary_Tone_Uses_Primary_Token` |
| `small-size-uses-small-token` | El tamaño pequeño consume el token de tamaño small | `Small_Size_Uses_Small_Token` |
| `render-emits-icon-command` | Hay exactamente una instrucción de icono | `Render_Emits_Single_Icon_Command` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
