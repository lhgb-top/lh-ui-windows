# Switch

A switch. A pill-shaped track plus a circular thumb; tapping toggles it. It supports disabled and loading states.

> Spec source: `lh-ui-core/specs/switch.json` ·
> Golden cases: `lh-ui-core/cases/switch.json` (6 cases, translated into xUnit tests)

## Basic usage

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch basics</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };
```

## Sizes

Three sizes map to the `switch.width` / `switch.height` tokens; the thumb diameter = height - 2 × `switch.gap`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch sizes</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--small sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--large sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## States

`Disabled` and `Loading` both suppress taps; while `Loading` the thumb position is replaced by a primary-colored loading ring.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch states</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--checked sim-switch--disabled"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--loading"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `Change` | Toggled-state callback; not fired when disabled or loading | `event Action<bool>` | — |
| `Checked` | Whether the switch is on | `bool` | `false` |
| `Disabled` | Whether the switch is disabled (does not respond to taps and renders the disabled style) | `bool` | `false` |
| `Loading` | Whether the switch is loading (shows a loading ring at the thumb position and does not respond to taps) | `bool` | `false` |
| `Size` | Switch size; maps to the `switch.width` / `switch.height` tokens | `LhSwitchSize` | `Medium` |

### LhSwitchSize

| Value | width | height |
| --- | --- | --- |
| `Small` | `switch.width.small` (40epx) | `switch.height.small` (22epx) |
| `Medium` | `switch.width.medium` (48epx) | `switch.height.medium` (26epx) |
| `Large` | `switch.width.large` (56epx) | `switch.height.large` (30epx) |

## Token Reference

| Token | Usage |
| --- | --- |
| `switch.width.{small,medium,large}` | Three track widths |
| `switch.height.{small,medium,large}` | Three track heights |
| `switch.gap` | Spacing between the thumb and the track (thumb diameter = height - 2 × gap) |
| `color.primary.default` | On-state track color |
| `color.border.base` | Off-state track color |
| `color.onPrimary` | Thumb color |
| `color.disabled.bg` / `color.text.disabled` | Disabled track / thumb color |

## Golden Case Reference

How the 6 assertions of `cases/switch.json` map to xUnit tests (`tests/LhSwitchTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | A tap toggles and fires onChange | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-turns-off` | A tap on the on state turns it off | `Tap_Checked_Turns_Off` |
| `disabled-blocks-change` | The disabled state is not interactive | `Disabled_Blocks_Change` |
| `loading-blocks-change` | The loading state is not interactive | `Loading_Blocks_Change` |
| `checked-uses-primary-token` | The on state consumes the primary token | `Checked_Uses_Primary_Token` |
| `unchecked-uses-border-token` | The off state consumes the border token | `Unchecked_Uses_Border_Token` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
