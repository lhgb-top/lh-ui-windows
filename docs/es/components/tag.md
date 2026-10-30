# Etiqueta

Etiqueta para marcar y seleccionar. Admite seis tipos semánticos, tres tamaños, estado con borde, estado redondeado y estado cerrable.

> Fuente del contrato: `lh-ui-core/specs/tag.json` ·
> Casos dorados: `lh-ui-core/cases/tag.json` (6 casos, traducidos a pruebas xUnit)

## Tipos

Seis tipos determinan el color de fondo, el color del texto y el color del borde.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tipos de Tag</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag">Predeterminada</span>
      <span class="sim-tag sim-tag--primary">Principal</span>
      <span class="sim-tag sim-tag--success">Éxito</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--warning">Advertencia</span>
      <span class="sim-tag sim-tag--danger">Peligro</span>
      <span class="sim-tag sim-tag--info">Información</span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var tag = new LhTag
{
    Text = "Principal",
    Type = LhTagType.Primary,
};
```

## Borde y esquinas redondeadas

Cuando `Plain` es true, se alterna a fondo semántico claro + texto semántico + borde semántico; cuando `Round` es true,
el radio de esquina toma la mitad de la altura (forma de cápsula, en paralelo con el token `tag.borderRadius`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Borde y redondeado</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--plain">Borde</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">Borde</span>
      <span class="sim-tag sim-tag--danger sim-tag--plain">Borde</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--round">Redondeada</span>
      <span class="sim-tag sim-tag--success sim-tag--round sim-tag--plain">Redondeada con borde</span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Tamaño y cierre

Tres tamaños se mapean a los tokens `tag.height` / `paddingHorizontal` / `fontSize`; cuando `Closable`
es true, se dibuja un símbolo de cierre a la derecha y la pulsación dispara el evento `Close`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tamaño y cierre</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--small">Pequeña</span>
      <span class="sim-tag">Mediana</span>
      <span class="sim-tag sim-tag--large">Grande</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">Cerrable <span class="sim-tag__close">✕</span></span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var tag = new LhTag
{
    Text = "Cerrable",
    Type = LhTagType.Primary,
    Closable = true,
};
tag.Close += () => { /* pulsación del símbolo de cierre */ };
```

## Temas claro y oscuro

Cambia el tema del sitio en la esquina superior derecha y los colores de las etiquetas dentro del marco del simulador cambian; en el dispositivo, se alterna con `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Etiquetas oscuras</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">Principal</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">Borde</span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo; los valores claro/oscuro provienen de la instantánea de tokens de core</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `Close` | Se dispara al pulsar el símbolo de cierre (requiere `Closable` true) | `event Action` | — |
| `Text` | Contenido de texto de la etiqueta (el slot default del spec) | `string` | `""` |
| `Type` | Tipo semántico, determina el color de fondo, de texto y de borde | `LhTagType` | `Default` |
| `Size` | Tamaño de la etiqueta, mapeado a los tokens de tamaño | `LhTagSize` | `Medium` |
| `Plain` | Si tiene la forma de borde con fondo claro | `bool` | `false` |
| `Round` | Si tiene forma de cápsula (radio de esquina igual a la mitad de la altura) | `bool` | `false` |
| `Closable` | Si se muestra el símbolo de cierre | `bool` | `false` |

### LhTagType

| Valor | Fondo sólido | Fondo claro (plain) | Color del texto |
| --- | --- | --- | --- |
| `Default` | `color.background.default` | Igual que el anterior | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.primary.bg` | `color.onPrimary` / `color.primary.default` |
| `Success` | `color.success.default` | `color.success.bg` | `color.onPrimary` / `color.success.default` |
| `Warning` | `color.warning.default` | `color.warning.bg` | `color.onPrimary` / `color.warning.default` |
| `Danger` | `color.danger.default` | `color.danger.bg` | `color.onPrimary` / `color.danger.default` |
| `Info` | `color.info.default` | `color.info.bg` | `color.onPrimary` / `color.info.default` |

### LhTagSize

| Valor | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `tag.height.small` (20epx) | `tag.paddingHorizontal.small` (8epx) | `tag.fontSize.small` (11) |
| `Medium` | `tag.height.medium` (24epx) | `tag.paddingHorizontal.medium` (12epx) | `tag.fontSize.medium` (12) |
| `Large` | `tag.height.large` (28epx) | `tag.paddingHorizontal.large` (16epx) | `tag.fontSize.large` (13) |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `tag.height.{small,medium,large}` | Tres alturas |
| `tag.paddingHorizontal.{small,medium,large}` | Tres rellenos horizontales |
| `tag.fontSize.{small,medium,large}` | Tres tamaños de fuente |
| `tag.borderRadius` / `tag.borderWidth` | Radio de esquina y ancho del borde |
| `color.primary.default` / `color.onPrimary` | Fondo / texto del tipo primary |
| `color.{success,warning,danger,info}.{default,bg}` | Fondo sólido / fondo claro de los tipos semánticos |
| `color.background.default` / `color.text.primary` / `color.border.base` | Tres colores del tipo predeterminado |
| `spacing.xs` | Espacio entre el símbolo de cierre y el texto |
| `typography.fontFamily` | Pila de fuentes serif de estilo chino |

## Referencia de casos dorados

Cómo se mapean las 6 aserciones de `cases/tag.json` a las pruebas xUnit (`tests/LhTagTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `default-type-renders-text` | El tipo predeterminado usa fondo / texto / borde neutros | `Default_Type_Renders_Text` |
| `primary-type-uses-primary-tokens` | El tipo primary consume los tokens del color principal | `Primary_Type_Uses_Primary_Tokens` |
| `plain-uses-bg-tokens` | La forma de fondo claro consume los tokens de fondo semántico claro | `Plain_Uses_Bg_Tokens` |
| `small-size-uses-small-tokens` | El tamaño pequeño consume los tokens de tamaño small | `Small_Size_Uses_Small_Tokens` |
| `round-makes-radius-half-height` | En la forma de cápsula el radio es la mitad de la altura | `Round_Makes_Radius_Half_Height` |
| `closable-tap-emits-onclose` | Pulsar el símbolo de cierre dispara onClose | `Closable_Tap_Emits_OnClose` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
