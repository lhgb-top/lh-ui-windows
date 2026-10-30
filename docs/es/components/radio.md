# Botón de radio

Botón de radio. Anillo + punto interior; la pulsación acierta toda la fila; el componente en sí no contiene lógica de grupo: el host mantiene el grupo de selección única.

> Fuente del contrato: `lh-ui-core/specs/radio.json` ·
> Casos dorados: `lh-ui-core/cases/radio.json` (6 casos, traducidos a pruebas xUnit)

## Uso básico

El host escucha `Change` y pone el `Checked` de los demás elementos del grupo a false, con lo que se implementa el grupo de selección única.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio básico</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">青花瓷 (porcelana azul y blanco)</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"></span>
        <span class="sim-radio__label">粉彩瓷 (famille rose)</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var radio = new LhRadio
{
    Label = "青花瓷 (porcelana azul y blanco)",
    Value = "qinghua",
    Checked = true,
};
radio.Change += isChecked => { /* solo se dispara sin marcar y sin deshabilitar; el host mantiene el grupo */ };
```

## Tamaños

Tres tamaños se mapean a los tokens de tamaño del anillo `radio.size.*`; el diámetro del punto interior = tamaño × `radio.dotScale`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tamaños de Radio</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--small"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Pequeño</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Mediano</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--large"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Grande</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Estados

Con `Disabled` no responde a la pulsación; el anillo exterior toma `color.disabled.border` y el punto toma `color.disabled.text`;
si se vuelve a pulsar un elemento ya marcado, no se dispara `Change`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Estados de Radio</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"></span>
        <span class="sim-radio__label">Sin marcar deshabilitado</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Marcado deshabilitado</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `Change` | Devolución de llamada de cambio de estado de marcado; solo se dispara sin marcar y sin deshabilitar (el payload siempre es true) | `event Action<bool>` | — |
| `Checked` | Si está marcado | `bool` | `false` |
| `Disabled` | Si está deshabilitado (no responde a la pulsación y presenta el estilo deshabilitado) | `bool` | `false` |
| `Label` | Etiqueta de texto (dibujada a la derecha del anillo) | `string` | `""` |
| `Value` | Valor del botón de radio (identificador dentro del grupo; el componente en sí no contiene lógica de grupo) | `string` | `""` |
| `Size` | Tamaño del anillo, mapeado a los tokens `radio.size.*` | `LhRadioSize` | `Medium` |

### LhRadioSize

| Valor | Token de tamaño |
| --- | --- |
| `Small` | `radio.size.small` (14epx) |
| `Medium` | `radio.size.medium` (16epx) |
| `Large` | `radio.size.large` (18epx) |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `radio.size.{small,medium,large}` | Tres tamaños de anillo |
| `radio.ringWidth` | Ancho del trazo del anillo exterior |
| `radio.dotScale` | Proporción del diámetro del punto interior (tamaño × dotScale) |
| `radio.gap` | Espacio entre el anillo y el texto |
| `color.primary.default` | Color del anillo y del punto en estado marcado |
| `color.border.base` | Color del anillo exterior sin marcar |
| `color.disabled.{border,text}` | Anillo / punto del estado deshabilitado |
| `color.text.{primary,disabled}` | Color del texto de la etiqueta |
| `typography.fontSize.base` / `fontFamily` | Tamaño de fuente y pila de fuentes de la etiqueta |

## Referencia de casos dorados

Cómo se mapean las 6 aserciones de `cases/radio.json` a las pruebas xUnit (`tests/LhRadioTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `tap-unchecked-emits-onchange` | Al pulsar sin marcar se dispara onChange | `Tap_Unchecked_Emits_OnChange` |
| `tap-checked-keeps-checked` | Al volver a pulsar uno ya marcado no se dispara | `Tap_Checked_Keeps_Checked` |
| `disabled-blocks-change` | El estado deshabilitado no es interactivo | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | El estado marcado consume los tokens del color principal | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | El estado sin marcar consume el token de borde | `Unchecked_Uses_Border_Token` |
| `small-size-uses-small-token` | El tamaño pequeño consume el token de tamaño small | `Small_Size_Uses_Small_Token` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
