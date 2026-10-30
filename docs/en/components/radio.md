# Radio

A radio button. A ring plus an inner dot; the tap target covers the whole row. The component itself carries no group logic — the host maintains the radio group.

> Spec source: `lh-ui-core/specs/radio.json` ·
> Golden cases: `lh-ui-core/cases/radio.json` (6 cases, translated into xUnit tests)

## Basic usage

The host listens to `Change` and sets the other items of the same group to `Checked = false` — that is the radio group.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio basics</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">青花瓷 (ceramic blue)</span>
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
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var radio = new LhRadio
{
    Label = "青花瓷 (ceramic blue)",
    Value = "qinghua",
    Checked = true,
};
radio.Change += isChecked => { /* Fires only when unchecked and not disabled; the host maintains the group */ };
```

## Sizes

Three sizes map to the `radio.size.*` ring-size tokens; the inner dot diameter = size × `radio.dotScale`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio sizes</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--small"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Small</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Medium</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--large"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Large</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## States

When `Disabled` the radio does not respond to taps: the outer ring takes `color.disabled.border` and the dot takes
`color.disabled.text`; tapping an already-checked radio does not fire `Change`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio states</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"></span>
        <span class="sim-radio__label">Disabled unchecked</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Disabled checked</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `Change` | Checked-state callback; fires only when unchecked and not disabled (the payload is always true) | `event Action<bool>` | — |
| `Checked` | Whether the radio is checked | `bool` | `false` |
| `Disabled` | Whether the radio is disabled (does not respond to taps and renders the disabled style) | `bool` | `false` |
| `Label` | Text label (drawn to the right of the ring) | `string` | `""` |
| `Value` | Radio value (the group identifier; the component itself carries no group logic) | `string` | `""` |
| `Size` | Ring size; maps to the `radio.size.*` tokens | `LhRadioSize` | `Medium` |

### LhRadioSize

| Value | Size token |
| --- | --- |
| `Small` | `radio.size.small` (14epx) |
| `Medium` | `radio.size.medium` (16epx) |
| `Large` | `radio.size.large` (18epx) |

## Token Reference

| Token | Usage |
| --- | --- |
| `radio.size.{small,medium,large}` | Three ring sizes |
| `radio.ringWidth` | Outer ring stroke width |
| `radio.dotScale` | Inner dot diameter ratio (size × dotScale) |
| `radio.gap` | Spacing between the ring and the label |
| `color.primary.default` | Checked ring and dot color |
| `color.border.base` | Unchecked ring color |
| `color.disabled.{border,text}` | Disabled ring / dot |
| `color.text.{primary,disabled}` | Label text color |
| `typography.fontSize.base` / `fontFamily` | Label font size and font stack |

## Golden Case Reference

How the 6 assertions of `cases/radio.json` map to xUnit tests (`tests/LhRadioTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `tap-unchecked-emits-onchange` | A tap on an unchecked radio fires onChange | `Tap_Unchecked_Emits_OnChange` |
| `tap-checked-keeps-checked` | Tapping a checked radio does not fire | `Tap_Checked_Keeps_Checked` |
| `disabled-blocks-change` | The disabled state is not interactive | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | The checked state consumes the primary tokens | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | The unchecked state consumes the border token | `Unchecked_Uses_Border_Token` |
| `small-size-uses-small-token` | Small consumes the small size token | `Small_Size_Uses_Small_Token` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
