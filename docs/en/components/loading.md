# Loading

A circular spinning loading indicator. It supports sizes and semantic tones.

> Spec source: `lh-ui-core/specs/loading.json` ·
> Golden cases: `lh-ui-core/cases/loading.json` (5 cases, translated into xUnit tests)

::: tip Platform notes
The spinning is carried by WinUI's `ProgressRing` platform animation (an austere stance: only geometry is emitted,
the platform fills in the animation) — the token layer decides only the diameter, the stroke width, and the ring color.
:::

## Sizes

Three sizes map to the `loading.size.*` diameter tokens.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Loading sizes</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--small"></span>
      <span class="sim-loading"></span>
      <span class="sim-loading sim-loading--large"></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var loading = new LhLoading
{
    Size = LhLoadingSize.Large,
    Tone = LhLoadingTone.Primary,
};
```

## Tones

`Tone` decides the ring color and maps one-to-one onto the semantic color tokens.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Loading tones</span></div>
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
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `Size` | Indicator diameter; maps to the `loading.size.*` tokens | `LhLoadingSize` | `Medium` |
| `Tone` | Ring color; takes the matching semantic color token | `LhLoadingTone` | `Primary` |

### LhLoadingSize

| Value | Diameter token |
| --- | --- |
| `Small` | `loading.size.small` (20epx) |
| `Medium` | `loading.size.medium` (28epx) |
| `Large` | `loading.size.large` (36epx) |

### LhLoadingTone

| Value | Ring color token |
| --- | --- |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

## Token Reference

| Token | Usage |
| --- | --- |
| `loading.size.{small,medium,large}` | Three diameters |
| `loading.strokeWidth` | Loading ring stroke width |
| `color.{primary,success,warning,danger,info}.default` | Five ring colors |

## Golden Case Reference

How the 5 assertions of `cases/loading.json` map to xUnit tests (`tests/LhLoadingTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `medium-size-uses-medium-token` | Medium consumes the medium diameter token | `Medium_Size_Uses_Medium_Token` |
| `large-size-uses-large-token` | Large consumes the large diameter token | `Large_Size_Uses_Large_Token` |
| `primary-tone-uses-primary-token` | The primary tone takes the primary color token | `Primary_Tone_Uses_Primary_Token` |
| `success-tone-uses-success-token` | The success tone takes the success color token | `Success_Tone_Uses_Success_Token` |
| `render-emits-ring-command` | Exactly one RING command is emitted | `Render_Emits_Ring_Command` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
