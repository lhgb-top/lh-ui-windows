# Checkbox

A checkbox. Checked draws a check mark, indeterminate draws a dash, and the tap target covers the whole row (box + label area).

> Spec source: `lh-ui-core/specs/checkbox.json` ·
> Golden cases: `lh-ui-core/cases/checkbox.json` (6 cases, translated into xUnit tests)

## Basic usage

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox basics</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box"></span>
        <span class="sim-checkbox__label">Unchecked</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Checked</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">─</span>
        <span class="sim-checkbox__label">Indeterminate</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var checkbox = new LhCheckbox
{
    Label = "青花瓷 (ceramic blue)",
    Checked = true,
};
checkbox.Change += isChecked => { };
```

## Sizes

Three sizes map to the `checkbox.size.*` box-size tokens.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox sizes</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--small sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Small</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Medium</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--large sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Large</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## States

When `Disabled` the checkbox does not respond to taps and the background / border / indicator / text all take the
`color.disabled.*` tokens; `Indeterminate` is the indeterminate state (dash) and becomes checked after a tap.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox states</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked sim-checkbox__box--disabled">✓</span>
        <span class="sim-checkbox__label">Disabled checked</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--disabled"></span>
        <span class="sim-checkbox__label">Disabled unchecked</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `Change` | Checked-state callback; not fired when disabled | `event Action<bool>` | — |
| `Checked` | Whether the checkbox is checked | `bool` | `false` |
| `Indeterminate` | Whether the checkbox is in the indeterminate state (dash indicator); becomes checked after a tap | `bool` | `false` |
| `Disabled` | Whether the checkbox is disabled (does not respond to taps and renders the disabled style) | `bool` | `false` |
| `Label` | Text label (drawn to the right of the box) | `string` | `""` |
| `Size` | Box size; maps to the `checkbox.size.*` tokens | `LhCheckboxSize` | `Medium` |

### LhCheckboxSize

| Value | Size token |
| --- | --- |
| `Small` | `checkbox.size.small` (14epx) |
| `Medium` | `checkbox.size.medium` (16epx) |
| `Large` | `checkbox.size.large` (18epx) |

## Token Reference

| Token | Usage |
| --- | --- |
| `checkbox.size.{small,medium,large}` | Three box sizes |
| `checkbox.borderRadius` / `checkbox.borderWidth` | Box corner radius and border width |
| `checkbox.checkWidth` | Check / dash stroke width |
| `checkbox.gap` | Spacing between the box and the label |
| `color.primary.default` / `color.onPrimary` | Checked / indeterminate background and indicator color |
| `color.border.base` | Unchecked border color |
| `color.disabled.{bg,border,text}` | Disabled background / border / indicator |
| `color.text.{primary,disabled}` | Label text color |
| `typography.fontSize.base` / `fontFamily` | Label font size and font stack |

## Golden Case Reference

How the 6 assertions of `cases/checkbox.json` map to xUnit tests (`tests/LhCheckboxTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | An unchecked tap turns it checked | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-unchecks` | A checked tap unchecks it | `Tap_Checked_Unchecks` |
| `disabled-blocks-change` | The disabled state is not interactive | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | The checked state consumes the primary tokens | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | The unchecked state consumes the border token | `Unchecked_Uses_Border_Token` |
| `indeterminate-becomes-checked` | An indeterminate tap turns it checked | `Indeterminate_Becomes_Checked` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
