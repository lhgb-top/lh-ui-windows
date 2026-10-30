# Cargando

Indicador de carga circular giratorio. Admite tamaño y tono semántico.

> Fuente del contrato: `lh-ui-core/specs/loading.json` ·
> Casos dorados: `lh-ui-core/cases/loading.json` (5 casos, traducidos a pruebas xUnit)

::: tip Notas de plataforma
La rotación está a cargo de la animación de plataforma `ProgressRing` de WinUI (tono sobrio: solo se emite la geometría, la animación la aporta la plataforma):
la capa de tokens solo determina el diámetro, el ancho del trazo y el color del anillo.
:::

## Tamaños

Tres tamaños se mapean a los tokens de diámetro `loading.size.*`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tamaños de Loading</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--small"></span>
      <span class="sim-loading"></span>
      <span class="sim-loading sim-loading--large"></span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var loading = new LhLoading
{
    Size = LhLoadingSize.Large,
    Tone = LhLoadingTone.Primary,
};
```

## Tono

`Tone` determina el color del anillo y se corresponde uno a uno con los tokens de colores semánticos.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tono de Loading</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--primary"></span>
      <span class="sim-loading sim-loading--success"></span>
      <span class="sim-loading sim-loading--warning"></span>
    </div>
    <div class="sim-row">
      <span class="sim-loading sim-loading--danger"></span>
      <span class="sim-loading sim-loading--info"></span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `Size` | Diámetro del indicador, mapeado a los tokens `loading.size.*` | `LhLoadingSize` | `Medium` |
| `Tone` | Color del anillo, toma el token semántico correspondiente | `LhLoadingTone` | `Primary` |

### LhLoadingSize

| Valor | Token de diámetro |
| --- | --- |
| `Small` | `loading.size.small` (20epx) |
| `Medium` | `loading.size.medium` (28epx) |
| `Large` | `loading.size.large` (36epx) |

### LhLoadingTone

| Valor | Token de color del anillo |
| --- | --- |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `loading.size.{small,medium,large}` | Tres diámetros |
| `loading.strokeWidth` | Ancho del trazo del anillo de carga |
| `color.{primary,success,warning,danger,info}.default` | Cinco colores de anillo |

## Referencia de casos dorados

Cómo se mapean las 5 aserciones de `cases/loading.json` a las pruebas xUnit (`tests/LhLoadingTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `medium-size-uses-medium-token` | El tamaño mediano consume el token de diámetro medium | `Medium_Size_Uses_Medium_Token` |
| `large-size-uses-large-token` | El tamaño grande consume el token de diámetro large | `Large_Size_Uses_Large_Token` |
| `primary-tone-uses-primary-token` | El tono primary toma el token del color principal | `Primary_Tone_Uses_Primary_Token` |
| `success-tone-uses-success-token` | El tono success toma el token del color de éxito | `Success_Tone_Uses_Success_Token` |
| `render-emits-ring-command` | Hay exactamente una instrucción RING | `Render_Emits_Ring_Command` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
