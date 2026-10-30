# Celda

Celda de fila de lista. Icono izquierdo + título (con estrella roja de obligatorio opcional) + descripción, valor a la derecha y flecha de indicación, línea de separación inferior opcional.

> Fuente del contrato: `lh-ui-core/specs/cell.json` ·
> Casos dorados: `lh-ui-core/cases/cell.json` (6 casos, traducidos a pruebas xUnit)

## Uso básico

El título toma `color.text.primary`, el valor y la descripción toman `color.text.secondary`; los tres están controlados por tokens.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Cell básica</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Título de la fila</div>
        <div class="sim-cell__label">Texto descriptivo</div>
      </div>
      <div class="sim-cell__value">Contenido de la fila</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Celda con separador</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var cell = new LhCell
{
    Title = "Título de la fila",
    Label = "Texto descriptivo",
    Value = "Contenido de la fila",
};
```

## Icono y obligatorio

`Icon` recibe un nombre de icono local (véase la tabla de glifos de la documentación de Icon) y se dibuja a la izquierda;
cuando `Required` es true, se dibuja una estrella roja `color.danger.default` delante del título.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icono y obligatorio</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <span class="sim-cell__icon">★</span>
      <div class="sim-cell__main">
        <div class="sim-cell__title">Con icono</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title"><span class="sim-cell__required">✱</span>Campo obligatorio</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Línea de separación y pulsación

Cuando `Bordered` es true, se dibuja una línea de separación `color.border.lighter` en la parte inferior; cuando `Clickable` es true,
se dibuja una flecha de indicación a la derecha y la pulsación dispara el evento `Click`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Separador y pulsación</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">Pulsable</div></div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">No pulsable</div></div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var cell = new LhCell
{
    Title = "Pulsable",
    Clickable = true,
    Bordered = true,
};
cell.Click += () => { /* solo se dispara cuando Clickable es true */ };
```

## Temas claro y oscuro

Cambia el tema del sitio en la esquina superior derecha y los colores del texto y de la línea de separación dentro del marco del simulador cambian; en el dispositivo, se alterna con `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Celdas oscuras</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Título de la fila</div>
        <div class="sim-cell__label">Texto descriptivo</div>
      </div>
      <div class="sim-cell__value">Contenido de la fila</div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo; los valores claro/oscuro provienen de la instantánea de tokens de core</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `Click` | Evento de pulsación; solo se dispara cuando `Clickable` es true | `event Action` | — |
| `Title` | Título (el slot title del spec) | `string` | `""` |
| `Label` | Texto descriptivo bajo el título (slot label) | `string` | `""` |
| `Value` | Texto del valor a la derecha (slot value) | `string` | `""` |
| `Icon` | Nombre del icono izquierdo (resuelto por la tabla de iconos local) | `string?` | `null` |
| `Required` | Si se dibuja la estrella roja de obligatorio delante del título | `bool` | `false` |
| `Clickable` | Si es pulsable (muestra/oculta la flecha y controla el disparo de Click) | `bool` | `false` |
| `Bordered` | Si se dibuja la línea de separación inferior | `bool` | `true` |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `cell.paddingHorizontal` / `cell.paddingVertical` | Relleno interno |
| `cell.gap` | Espacio vertical entre título y descripción |
| `cell.fontSizeTitle` / `fontSizeLabel` / `fontSizeValue` | Tres tamaños de fuente |
| `color.text.{primary,secondary,regular}` | Color de título / descripción / valor / icono |
| `color.danger.default` | Estrella roja de obligatorio |
| `color.border.lighter` | Línea de separación inferior |
| `icon.size.medium` | Tamaño del icono izquierdo |
| `spacing.{sm,xs}` | Espacio del icono / espacio interno entre elementos |
| `typography.lineHeight.primary` | Altura de línea del texto |

## Referencia de casos dorados

Cómo se mapean las 6 aserciones de `cases/cell.json` a las pruebas xUnit (`tests/LhCellTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `title-and-value-render` | El título y el valor se renderizan | `Title_And_Value_Render` |
| `label-renders-secondary-tone` | El texto descriptivo toma el token de texto secundario | `Label_Renders_Secondary_Tone` |
| `required-uses-danger-token` | La estrella roja de obligatorio consume el token de color de peligro | `Required_Uses_Danger_Token` |
| `bordered-emits-line-command` | La forma con separador emite una instrucción LINE | `Bordered_Emits_Line_Command` |
| `clickable-emits-onclick` | Al ser pulsable, la pulsación dispara onClick | `Clickable_Emits_OnClick` |
| `not-clickable-no-onclick` | Al no ser pulsable, no se dispara onClick | `Not_Clickable_No_OnClick` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
