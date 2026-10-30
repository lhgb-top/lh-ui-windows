# Botón

Botón de acción básico. Dispara una acción inmediata y admite tipo, tamaño, carga, deshabilitación, icono y fondo de patrón chino.

> Fuente del contrato: `lh-ui-core/specs/button.json` ·
> Casos dorados: `lh-ui-core/cases/button.json` (8 casos, traducidos a pruebas xUnit)

## Tipos

Seis tipos determinan el color de fondo y el color del texto.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tipos de Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">Botón predeterminado</button>
      <button class="sim-btn sim-btn--primary">Botón principal</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--success">Botón de éxito</button>
      <button class="sim-btn sim-btn--warning">Botón de advertencia</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--danger">Botón de peligro</button>
      <button class="sim-btn sim-btn--info">Botón de información</button>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var button = new LhButton
{
    Type = LhButtonType.Primary,
    Text = "Botón principal",
};
button.Click += OnButtonClick;
```

## Tamaños

Tres tamaños se mapean a los tokens `button.height` / `paddingHorizontal` / `fontSize` / `iconSize`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tamaños de Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">Botón pequeño</button>
      <button class="sim-btn">Botón mediano</button>
      <button class="sim-btn sim-btn--large">Botón grande</button>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Estados

Ni el estado deshabilitado ni el de carga disparan `Click`; cuando ambos están presentes, **la deshabilitación tiene prioridad** (caso dorado `disabled-precedes-loading`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Estados de Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">Normal</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Deshabilitado</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>Cargando
      </button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Deshabilitado prioritario</button>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Icono y fondo de patrón

El nombre del icono se resuelve contra el sistema de iconos local (`LhButtonTokens.IconFor`, admite actualmente search / refresh / check / close; los nombres no registrados no se dibujan). El fondo de patrón consume el token `color.decoration` como un tramado superpuesto a 45°.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icono y patrón</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>Buscar</button>
      <button class="sim-btn"><span class="sim-btn__icon"></span>Predeterminado</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--pattern">Patrón principal</button>
      <button class="sim-btn sim-btn--pattern">Patrón predeterminado</button>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Temas claro y oscuro

Cambia el tema del sitio en la esquina superior derecha y los colores de los botones dentro del marco del simulador cambian; en el dispositivo, se alterna con `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Botones oscuros</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">Botón principal</button>
      <button class="sim-btn">Botón predeterminado</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Deshabilitado</button>
      <button class="sim-btn sim-btn--success">Éxito</button>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo; los valores claro/oscuro provienen de la instantánea de tokens de core</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `Click` | Devolución de llamada al pulsar; no se dispara cuando `Disabled` o `Loading` es true | `event Action` | — |
| `Type` | Tipo de botón, determina el color de fondo y del texto | `LhButtonType` | `Default` |
| `Size` | Tamaño del botón, mapeado a los tokens de tamaño | `LhButtonSize` | `Medium` |
| `Disabled` | Si está deshabilitado | `bool` | `false` |
| `Loading` | Si está cargando (muestra un indicador, no dispara Click) | `bool` | `false` |
| `Icon` | Nombre del icono (resuelto por el sistema de iconos local) | `string?` | `null` |
| `BgPattern` | Si se dibuja el patrón decorativo chino | `bool` | `false` |
| `Text` | Contenido de texto del botón (el slot default del spec) | `string` | `""` |

### LhButtonType

| Valor | Fondo | Color del texto |
| --- | --- | --- |
| `Default` | `color.background.default` | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.onPrimary` |
| `Success` | `color.success.default` | `color.onPrimary` |
| `Warning` | `color.warning.default` | `color.onPrimary` |
| `Danger` | `color.danger.default` | `color.onPrimary` |
| `Info` | `color.info.default` | `color.onPrimary` |

### LhButtonSize

| Valor | height | paddingHorizontal | fontSize | iconSize |
| --- | --- | --- | --- | --- |
| `Small` | `button.height.small` (32epx) | `button.paddingHorizontal.small` (16epx) | `button.fontSize.small` (13) | `button.iconSize.small` (16) |
| `Medium` | `button.height.medium` (40epx) | `button.paddingHorizontal.medium` (20epx) | `button.fontSize.medium` (14) | `button.iconSize.medium` (18) |
| `Large` | `button.height.large` (48epx) | `button.paddingHorizontal.large` (24epx) | `button.fontSize.large` (16) | `button.iconSize.large` (20) |

La anulación del estado deshabilitado: fondo `color.disabled.bg`, texto `color.disabled.text`, borde `color.disabled.border`.

## Referencia de tokens

Tokens consumidos por el botón (alineados uno a uno con los `tokenRefs` de `specs/button.json`):

| Token | Uso |
| --- | --- |
| `button.height.{small,medium,large}` | Tres alturas |
| `button.paddingHorizontal.{small,medium,large}` | Tres rellenos horizontales |
| `button.fontSize.{small,medium,large}` | Tres tamaños de fuente |
| `button.iconSize.{small,medium,large}` | Tres tamaños de icono |
| `button.borderRadius` / `button.borderWidth` | Radio de esquina (sigue `radius.base`) y ancho del borde |
| `button.fontWeight` | Peso de la fuente (sigue `typography.fontWeight.primary`; mapeado a FontWeights en este lado) |
| `color.primary.default` / `color.onPrimary` | Fondo / texto del tipo primary |
| `color.{success,warning,danger,info}.default` | Fondos de los tipos semánticos |
| `color.background.default` / `color.text.primary` / `color.border.base` | Fondo / texto / borde del tipo predeterminado |
| `color.disabled.{bg,text,border}` | Tres colores del estado deshabilitado |
| `color.decoration` | Color del patrón |
| `spacing.xs` | Espacio entre el icono y el texto |
| `typography.fontFamily` | Pila de fuentes serif de estilo chino |

## Referencia de casos dorados

Cómo se mapean las 8 aserciones de `cases/button.json` a las pruebas xUnit (`tests/LhButtonTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `click-emits-onclick` | La barrera deja pasar (interactivo) | `Enabled_Button_Is_Interactive` |
| `disabled-blocks-click` | El estado deshabilitado no es interactivo | `Disabled_Is_Not_Interactive` |
| `loading-blocks-click` | El estado de carga no es interactivo | `Loading_Is_Not_Interactive` |
| `disabled-precedes-loading` | La deshabilitación tiene prioridad, con el estilo deshabilitado | `Disabled_Precedes_Loading` |
| `default-type-renders-text` | El tipo predeterminado usa fondo / texto neutros | `Default_Type_Uses_Neutral_Tokens` |
| `primary-type-uses-primary-tokens` | El tipo primary consume los tokens del color principal | `Primary_Type_Uses_Primary_Tokens` |
| `small-size-uses-small-tokens` | El tamaño pequeño consume los tokens de tamaño small | `Small_Size_Uses_Small_Tokens` |
| `bgpattern-uses-decoration-token` | El fondo de patrón consume el color decorativo | `BgPattern_Uses_Decoration_Token` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
