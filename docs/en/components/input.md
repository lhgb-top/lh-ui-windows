# Input

A single-line text input. It supports type, size, disabled, readonly, clearable, and a maximum character count.

> Spec source: `lh-ui-core/specs/input.json` ·
> Golden cases: `lh-ui-core/cases/input.json` (8 cases, translated into xUnit tests)

## Basic usage

A controlled component: the host listens to `ValueChange` and writes the new value back to `Value`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input basics</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷 (ceramic blue)</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field sim-input__field--placeholder">Type a keyword</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var input = new LhInput
{
    Placeholder = "Type a keyword",
};
input.ValueChange += value => { /* Controlled: write the value back to Value */ };
```

## Sizes

Three sizes map to the `input.height` / `paddingHorizontal` / `fontSize` tokens.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input sizes</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input sim-input--small"><div class="sim-input__field">Small</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">Medium</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--large"><div class="sim-input__field">Large</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Types and states

`Password` masks the text with dots; `Disabled` and `Readonly` both suppress `ValueChange`,
but only `Disabled` renders the disabled style; when focused the border takes `color.focus` + `input.focusRingWidth`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input states</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">●●●●●●●●</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--disabled"><div class="sim-input__field">Disabled</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--focus"><div class="sim-input__field">Focus border</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Clear and character count

When `Clearable` is true and the value is non-empty, a clear button is shown and tapping it fires `ValueChange("")`;
when `Maxlength` is exceeded, new input is truncated.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Clear & count</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷 (ceramic blue)</div>
        <span class="sim-input__clear">✕</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `ValueChange` | Value-change callback (controlled); not fired when disabled / readonly | `event Action<string>` | — |
| `Focus` | Fired when the field gains focus | `event Action` | — |
| `Blur` | Fired when the field loses focus | `event Action` | — |
| `Value` | Current text value (controlled) | `string` | `""` |
| `Placeholder` | Placeholder hint text; takes the `color.text.placeholder` token | `string` | `""` |
| `Type` | Input type; password masks the text with dots | `LhInputType` | `Text` |
| `Size` | Input size; maps to the size tokens | `LhInputSize` | `Medium` |
| `Disabled` | Whether the input is disabled (no input accepted; renders the disabled style) | `bool` | `false` |
| `Readonly` | Whether the input is read-only (focusable but accepts no input) | `bool` | `false` |
| `Clearable` | Whether to show the clear button | `bool` | `false` |
| `Maxlength` | Maximum character count; new input is truncated when exceeded | `int?` | `null` |

### LhInputSize

| Value | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `input.height.small` (32epx) | `input.paddingHorizontal.small` (12epx) | `input.fontSize.small` (13) |
| `Medium` | `input.height.medium` (40epx) | `input.paddingHorizontal.medium` (16epx) | `input.fontSize.medium` (14) |
| `Large` | `input.height.large` (48epx) | `input.paddingHorizontal.large` (20epx) | `input.fontSize.large` (16) |

## Token Reference

| Token | Usage |
| --- | --- |
| `input.height.{small,medium,large}` | Three heights |
| `input.paddingHorizontal.{small,medium,large}` | Three horizontal paddings |
| `input.fontSize.{small,medium,large}` | Three font sizes |
| `input.borderRadius` / `input.borderWidth` | Corner radius and unfocused border width |
| `input.focusRingWidth` / `color.focus` | Focused border width and color |
| `input.clearIconSize` | Clear-button icon size |
| `color.background.default` / `color.text.primary` | Container background / text color |
| `color.text.placeholder` | Placeholder text color |
| `color.border.base` | Unfocused border color |
| `color.disabled.{bg,text}` | Disabled background / text |
| `typography.fontFamily` | Chinese-style serif font stack |

## Golden Case Reference

How the 8 assertions of `cases/input.json` map to xUnit tests (`tests/LhInputTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `input-emits-onvaluechange` | Typing fires onValueChange | `Input_Emits_OnValueChange` |
| `clearable-emits-empty-value` | Clearing fires onValueChange("") | `Clearable_Emits_Empty_Value` |
| `disabled-blocks-input` | The disabled state does not fire and renders the disabled style | `Disabled_Blocks_Input` |
| `readonly-blocks-input` | Readonly does not fire but renders no disabled style | `Readonly_Blocks_Input` |
| `placeholder-uses-placeholder-token` | The placeholder text takes the placeholder token | `Placeholder_Uses_Placeholder_Token` |
| `focus-uses-focus-token` | The focused border takes the focus token | `Focus_Uses_Focus_Token` |
| `small-size-uses-small-tokens` | Small consumes the small size tokens | `Small_Size_Uses_Small_Tokens` |
| `maxlength-truncates-input` | Input beyond maxlength is truncated | `Maxlength_Truncates_Input` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
