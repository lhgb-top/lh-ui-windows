# Campo de entrada

Entrada de texto de una sola línea. Admite tipo, tamaño, deshabilitación, solo lectura, botón de limpieza y número máximo de caracteres.

> Fuente del contrato: `lh-ui-core/specs/input.json` ·
> Casos dorados: `lh-ui-core/cases/input.json` (8 casos, traducidos a pruebas xUnit)

## Uso básico

Componente controlado: el host escucha `ValueChange` y reescribe el nuevo valor en `Value`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input básico</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷 (porcelana azul y blanco)</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field sim-input__field--placeholder">Introduce una palabra clave</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var input = new LhInput
{
    Placeholder = "Introduce una palabra clave",
};
input.ValueChange += value => { /* controlado: reescribe Value */ };
```

## Tamaños

Tres tamaños se mapean a los tokens `input.height` / `paddingHorizontal` / `fontSize`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tamaños de Input</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input sim-input--small"><div class="sim-input__field">Pequeño</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">Mediano</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--large"><div class="sim-input__field">Grande</div></div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Tipo y estados

`Password` enmascara con puntos; tanto `Disabled` como `Readonly` no disparan `ValueChange`,
pero solo `Disabled` presenta el estilo deshabilitado; al recibir el foco, el borde toma `color.focus` + `input.focusRingWidth`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Estados de Input</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">●●●●●●●●</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--disabled"><div class="sim-input__field">Deshabilitado</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--focus"><div class="sim-input__field">Borde de foco</div></div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Limpieza y recuento de caracteres

Cuando `Clearable` es true y el valor no está vacío, se muestra el botón de limpieza; al pulsarlo se dispara `ValueChange("")`;
cuando se supera `Maxlength`, se trunca la nueva entrada.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Limpieza y recuento</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷 (porcelana azul y blanco)</div>
        <span class="sim-input__clear">✕</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `ValueChange` | Devolución de llamada de cambio de valor (controlado); no se dispara con disabled / readonly | `event Action<string>` | — |
| `Focus` | Se dispara al recibir el foco | `event Action` | — |
| `Blur` | Se dispara al perder el foco | `event Action` | — |
| `Value` | Valor de texto actual (controlado) | `string` | `""` |
| `Placeholder` | Texto del marcador de posición, toma el token `color.text.placeholder` | `string` | `""` |
| `Type` | Tipo de entrada; con password se enmascara con puntos | `LhInputType` | `Text` |
| `Size` | Tamaño del campo de entrada, mapeado a los tokens de tamaño | `LhInputSize` | `Medium` |
| `Disabled` | Si está deshabilitado (no responde a la entrada y presenta el estilo deshabilitado) | `bool` | `false` |
| `Readonly` | Si es de solo lectura (puede recibir el foco pero no acepta entrada) | `bool` | `false` |
| `Clearable` | Si se muestra el botón de limpieza | `bool` | `false` |
| `Maxlength` | Número máximo de caracteres; al superarlo se trunca la nueva entrada | `int?` | `null` |

### LhInputSize

| Valor | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `input.height.small` (32epx) | `input.paddingHorizontal.small` (12epx) | `input.fontSize.small` (13) |
| `Medium` | `input.height.medium` (40epx) | `input.paddingHorizontal.medium` (16epx) | `input.fontSize.medium` (14) |
| `Large` | `input.height.large` (48epx) | `input.paddingHorizontal.large` (20epx) | `input.fontSize.large` (16) |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `input.height.{small,medium,large}` | Tres alturas |
| `input.paddingHorizontal.{small,medium,large}` | Tres rellenos horizontales |
| `input.fontSize.{small,medium,large}` | Tres tamaños de fuente |
| `input.borderRadius` / `input.borderWidth` | Radio de esquina y ancho del borde sin foco |
| `input.focusRingWidth` / `color.focus` | Ancho y color del borde con foco |
| `input.clearIconSize` | Tamaño del icono del botón de limpieza |
| `color.background.default` / `color.text.primary` | Fondo del contenedor / color del texto |
| `color.text.placeholder` | Color del texto del marcador de posición |
| `color.border.base` | Color del borde sin foco |
| `color.disabled.{bg,text}` | Fondo / texto del estado deshabilitado |
| `typography.fontFamily` | Pila de fuentes serif de estilo chino |

## Referencia de casos dorados

Cómo se mapean las 8 aserciones de `cases/input.json` a las pruebas xUnit (`tests/LhInputTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `input-emits-onvaluechange` | La entrada dispara onValueChange | `Input_Emits_OnValueChange` |
| `clearable-emits-empty-value` | La limpieza dispara onValueChange("") | `Clearable_Emits_Empty_Value` |
| `disabled-blocks-input` | El estado deshabilitado no dispara y presenta el estilo deshabilitado | `Disabled_Blocks_Input` |
| `readonly-blocks-input` | El modo solo lectura no dispara, pero no presenta el estilo deshabilitado | `Readonly_Blocks_Input` |
| `placeholder-uses-placeholder-token` | El texto del marcador de posición toma el token de marcador | `Placeholder_Uses_Placeholder_Token` |
| `focus-uses-focus-token` | El borde con foco toma el token focus | `Focus_Uses_Focus_Token` |
| `small-size-uses-small-tokens` | El tamaño pequeño consume los tokens de tamaño small | `Small_Size_Uses_Small_Tokens` |
| `maxlength-truncates-input` | Al superar maxlength se trunca | `Maxlength_Truncates_Input` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
