# Texto

Componente de texto. Renderiza un único párrafo de texto según nivel, peso, tono y alineación; todos los valores tipográficos provienen de los tokens de typography.

> Fuente del contrato: `lh-ui-core/specs/text.json` ·
> Casos dorados: `lh-ui-core/cases/text.json` (5 casos, traducidos a pruebas xUnit)

## Niveles

Seis niveles se mapean a los tokens `typography.fontSize.*`; el nivel predeterminado es `base`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Niveles de Text</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--extra-large">青花瓷 (porcelana azul y blanco) extraLarge</div>
    <div class="sim-text sim-text--large">青花瓷 (porcelana azul y blanco) large</div>
    <div class="sim-text sim-text--medium">青花瓷 (porcelana azul y blanco) medium</div>
    <div class="sim-text">青花瓷 (porcelana azul y blanco) base</div>
    <div class="sim-text sim-text--small">青花瓷 (porcelana azul y blanco) small</div>
    <div class="sim-text sim-text--extra-small">青花瓷 (porcelana azul y blanco) extraSmall</div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var text = new LhText
{
    Text = "青花瓷 (porcelana azul y blanco)",
    Level = LhTextLevel.Large,
};
```

## Peso y tono

`Weight` se mapea a los tokens `typography.fontWeight.*`; `Tone` se mapea a `color.text.*` (selected toma `color.selected`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Peso y tono</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--bold">Negrita bold</div>
    <div class="sim-text sim-text--secondary">Descripción secundaria secondary</div>
    <div class="sim-text sim-text--placeholder">Marcador de posición placeholder</div>
    <div class="sim-text sim-text--selected">Estado seleccionado selected</div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Alineación

`Align` determina la posición horizontal del texto dentro del ancho de la caja de línea (no es un token de diseño, se mapea a `TextAlignment` de WinUI).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Alineación</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--start">Alineado a la izquierda start</div>
    <div class="sim-text sim-text--center">Centrado center</div>
    <div class="sim-text sim-text--end">Alineado a la derecha end</div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Temas claro y oscuro

Cambia el tema del sitio en la esquina superior derecha y los colores del texto dentro del marco del simulador cambian; en el dispositivo, se alterna con `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Texto oscuro</span></div>
  <div class="sim-screen">
    <div class="sim-text">Texto regular</div>
    <div class="sim-text sim-text--secondary">Descripción secundaria secondary</div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo; los valores claro/oscuro provienen de la instantánea de tokens de core</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `Text` | Contenido del texto (el slot default del spec) | `string` | `""` |
| `Level` | Nivel de tamaño de fuente, mapeado a `typography.fontSize.*` | `LhTextLevel` | `Base` |
| `Weight` | Peso de la fuente, mapeado a `typography.fontWeight.*` | `LhTextWeight` | `Primary` |
| `Tone` | Tono del texto, mapeado a `color.text.*` / `color.selected` | `LhTextTone` | `Regular` |
| `Align` | Alineación horizontal (mapeada a `TextAlignment`) | `LhTextAlign` | `Start` |

### LhTextLevel

| Valor | Token de tamaño de fuente |
| --- | --- |
| `ExtraLarge` | `typography.fontSize.extraLarge` (20epx) |
| `Large` | `typography.fontSize.large` (18epx) |
| `Medium` | `typography.fontSize.medium` (16epx) |
| `Base` | `typography.fontSize.base` (14epx) |
| `Small` | `typography.fontSize.small` (13epx) |
| `ExtraSmall` | `typography.fontSize.extraSmall` (12epx) |

### LhTextWeight

| Valor | Token de peso |
| --- | --- |
| `Primary` | `typography.fontWeight.primary` (400, mapeado a Normal en este lado) |
| `Bold` | `typography.fontWeight.bold` (600, mapeado a SemiBold en este lado) |

### LhTextTone

| Valor | Token de color |
| --- | --- |
| `Primary` | `color.text.primary` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |
| `Placeholder` | `color.text.placeholder` |
| `Disabled` | `color.text.disabled` |
| `Selected` | `color.selected` |

### LhTextAlign

| Valor | Mapeo WinUI |
| --- | --- |
| `Start` | `TextAlignment.Left` |
| `Center` | `TextAlignment.Center` |
| `End` | `TextAlignment.Right` |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `typography.fontSize.{extraLarge…extraSmall}` | Seis tamaños de fuente |
| `typography.fontWeight.{primary,bold}` | Dos pesos de fuente |
| `typography.lineHeight.primary` | Altura de línea (la altura de la caja de línea = tamaño de fuente × altura de línea) |
| `typography.fontFamily` | Pila de fuentes serif de estilo chino |
| `color.text.{primary,regular,secondary,placeholder,disabled}` | Tonos neutros |
| `color.selected` | Tono del estado seleccionado |

## Referencia de casos dorados

Cómo se mapean las 5 aserciones de `cases/text.json` a las pruebas xUnit (`tests/LhTextTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `default-level-uses-base-tokens` | El nivel predeterminado consume los tokens tipográficos base | `Default_Level_Uses_Base_Tokens` |
| `large-level-uses-large-token` | El nivel large consume el token de tamaño de fuente large | `Large_Level_Uses_Large_Token` |
| `bold-weight-uses-bold-token` | La negrita consume el token de peso bold | `Bold_Weight_Uses_Bold_Token` |
| `secondary-tone-uses-text-secondary` | El tono secundario toma el token de texto secundario | `Secondary_Tone_Uses_Text_Secondary` |
| `render-emits-single-text-command` | Hay exactamente una instrucción TEXT | `Render_Emits_Single_Text_Command` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
