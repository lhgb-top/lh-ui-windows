# Casilla de verificación

Casilla de verificación. Al marcarla se dibuja una marca de verificación y en el estado indeterminado una barra horizontal; la pulsación acierta toda la fila (caja + zona de la etiqueta).

> Fuente del contrato: `lh-ui-core/specs/checkbox.json` ·
> Casos dorados: `lh-ui-core/cases/checkbox.json` (6 casos, traducidos a pruebas xUnit)

## Uso básico

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox básica</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box"></span>
        <span class="sim-checkbox__label">Sin marcar</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Marcada</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">─</span>
        <span class="sim-checkbox__label">Indeterminada</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var checkbox = new LhCheckbox
{
    Label = "青花瓷 (porcelana azul y blanco)",
    Checked = true,
};
checkbox.Change += isChecked => { };
```

## Tamaños

Tres tamaños se mapean a los tokens de tamaño de caja `checkbox.size.*`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tamaños de Checkbox</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--small sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Pequeña</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Mediana</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--large sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Grande</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Estados

Con `Disabled` no responde a la pulsación; fondo, borde, indicador y texto toman todos los tokens `color.disabled.*`;
`Indeterminate` es el estado indeterminado (barra horizontal), que al pulsarse pasa a marcado.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Estados de Checkbox</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked sim-checkbox__box--disabled">✓</span>
        <span class="sim-checkbox__label">Marcada deshabilitada</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--disabled"></span>
        <span class="sim-checkbox__label">Sin marcar deshabilitada</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `Change` | Devolución de llamada de cambio de estado de marcado; no se dispara con disabled | `event Action<bool>` | — |
| `Checked` | Si está marcada | `bool` | `false` |
| `Indeterminate` | Si está en estado indeterminado (indicador de barra); al pulsar pasa a marcado | `bool` | `false` |
| `Disabled` | Si está deshabilitada (no responde a la pulsación y presenta el estilo deshabilitado) | `bool` | `false` |
| `Label` | Etiqueta de texto (dibujada a la derecha de la caja) | `string` | `""` |
| `Size` | Tamaño de la caja, mapeado a los tokens `checkbox.size.*` | `LhCheckboxSize` | `Medium` |

### LhCheckboxSize

| Valor | Token de tamaño |
| --- | --- |
| `Small` | `checkbox.size.small` (14epx) |
| `Medium` | `checkbox.size.medium` (16epx) |
| `Large` | `checkbox.size.large` (18epx) |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `checkbox.size.{small,medium,large}` | Tres tamaños de caja |
| `checkbox.borderRadius` / `checkbox.borderWidth` | Radio de esquina y ancho del borde de la caja |
| `checkbox.checkWidth` | Ancho del trazo de la marca de verificación / barra |
| `checkbox.gap` | Espacio entre la caja y el texto |
| `color.primary.default` / `color.onPrimary` | Fondo e indicador de los estados marcado / indeterminado |
| `color.border.base` | Color del borde sin marcar |
| `color.disabled.{bg,border,text}` | Fondo / borde / indicador del estado deshabilitado |
| `color.text.{primary,disabled}` | Color del texto de la etiqueta |
| `typography.fontSize.base` / `fontFamily` | Tamaño de fuente y pila de fuentes de la etiqueta |

## Referencia de casos dorados

Cómo se mapean las 6 aserciones de `cases/checkbox.json` a las pruebas xUnit (`tests/LhCheckboxTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | Al pulsar estando sin marcar, queda marcada | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-unchecks` | Al pulsar estando marcada, se desmarca | `Tap_Checked_Unchecks` |
| `disabled-blocks-change` | El estado deshabilitado no es interactivo | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | El estado marcado consume los tokens del color principal | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | El estado sin marcar consume el token de borde | `Unchecked_Uses_Border_Token` |
| `indeterminate-becomes-checked` | El estado indeterminado, al pulsar, queda marcado | `Indeterminate_Becomes_Checked` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
