# Interruptor

Interruptor. Pista en forma de cápsula + deslizador circular, que se alterna al pulsar; admite deshabilitación y estado de carga.

> Fuente del contrato: `lh-ui-core/specs/switch.json` ·
> Casos dorados: `lh-ui-core/cases/switch.json` (6 casos, traducidos a pruebas xUnit)

## Uso básico

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch básico</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };
```

## Tamaños

Tres tamaños se mapean a los tokens `switch.width` / `switch.height`; el diámetro del deslizador = altura - 2 × `switch.gap`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tamaños de Switch</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--small sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--large sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Estados

Tanto `Disabled` como `Loading` no responden a la pulsación; con `Loading`, la posición del deslizador pasa a mostrar un anillo de carga del color principal.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Estados de Switch</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--checked sim-switch--disabled"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--loading"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `Change` | Devolución de llamada de cambio de estado del interruptor; no se dispara con disabled o loading | `event Action<bool>` | — |
| `Checked` | Si está activado | `bool` | `false` |
| `Disabled` | Si está deshabilitado (no responde a la pulsación y presenta el estilo deshabilitado) | `bool` | `false` |
| `Loading` | Si está cargando (muestra un anillo de carga en la posición del deslizador y no responde a la pulsación) | `bool` | `false` |
| `Size` | Tamaño del interruptor, mapeado a los tokens `switch.width` / `switch.height` | `LhSwitchSize` | `Medium` |

### LhSwitchSize

| Valor | width | height |
| --- | --- | --- |
| `Small` | `switch.width.small` (40epx) | `switch.height.small` (22epx) |
| `Medium` | `switch.width.medium` (48epx) | `switch.height.medium` (26epx) |
| `Large` | `switch.width.large` (56epx) | `switch.height.large` (30epx) |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `switch.width.{small,medium,large}` | Tres anchos de pista |
| `switch.height.{small,medium,large}` | Tres alturas de pista |
| `switch.gap` | Margen entre el deslizador y la pista (diámetro del deslizador = altura - 2 × gap) |
| `color.primary.default` | Color de la pista activada |
| `color.border.base` | Color de la pista desactivada |
| `color.onPrimary` | Color del deslizador |
| `color.disabled.bg` / `color.text.disabled` | Color de pista / deslizador del estado deshabilitado |

## Referencia de casos dorados

Cómo se mapean las 6 aserciones de `cases/switch.json` a las pruebas xUnit (`tests/LhSwitchTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | La pulsación alterna y dispara onChange | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-turns-off` | Al pulsar el estado activado, se desactiva | `Tap_Checked_Turns_Off` |
| `disabled-blocks-change` | El estado deshabilitado no es interactivo | `Disabled_Blocks_Change` |
| `loading-blocks-change` | El estado de carga no es interactivo | `Loading_Blocks_Change` |
| `checked-uses-primary-token` | El estado activado consume el token del color principal | `Checked_Uses_Primary_Token` |
| `unchecked-uses-border-token` | El estado desactivado consume el token de borde | `Unchecked_Uses_Border_Token` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
