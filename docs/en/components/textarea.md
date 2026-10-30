# Textarea

A multi-line text input. It provides an initial visible line count via `Rows`, grows automatically when content overflows, and supports a character counter.

> Spec source: `lh-ui-core/specs/textarea.json` ·
> Golden cases: `lh-ui-core/cases/textarea.json` (6 cases, translated into xUnit tests)

## Basic usage

A controlled component: the host listens to `ValueChange` and writes the new value back to `Value`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea basics</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷 (ceramic blue)<br/>粉彩瓷 (famille rose)</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field sim-textarea__field--placeholder">Type a description</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var textarea = new LhTextarea
{
    Rows = 3,
    Placeholder = "Type a description",
};
textarea.ValueChange += value => { /* Controlled: write the value back to Value */ };
```

## Character counter

When `ShowCount` is true a character counter is drawn at the bottom right; when `Maxlength` is declared the format is
"current / maximum", otherwise only the current length is shown. Input beyond `Maxlength` is truncated.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Character counter</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷 (ceramic blue)</div>
        <div class="sim-textarea__count">3/10</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷 (ceramic blue)</div>
        <div class="sim-textarea__count">3</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## States

`Disabled` and `Readonly` both suppress `ValueChange`, but only `Disabled` renders the disabled style;
when focused the border takes `color.focus` + `textarea.focusRingWidth`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea states</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--disabled">
        <div class="sim-textarea__field">Disabled</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--focus">
        <div class="sim-textarea__field">Focus border</div>
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
| `Rows` | Initial visible line count; the height grows with the line count and the content | `int` | `3` |
| `Disabled` | Whether the input is disabled (no input accepted; renders the disabled style) | `bool` | `false` |
| `Readonly` | Whether the input is read-only (focusable but accepts no input) | `bool` | `false` |
| `ShowCount` | Whether to show the character counter at the bottom right | `bool` | `false` |
| `Maxlength` | Maximum character count; truncates new input when exceeded and serves as the denominator of showCount | `int?` | `null` |

## Token Reference

| Token | Usage |
| --- | --- |
| `textarea.paddingVertical` / `paddingHorizontal` | Paddings |
| `textarea.fontSize` | Font size |
| `textarea.borderRadius` / `borderWidth` | Corner radius and unfocused border width |
| `textarea.focusRingWidth` / `color.focus` | Focused border width and color |
| `textarea.countFontSize` / `color.text.secondary` | Counter font size / color |
| `textarea.minHeight` | Minimum height (the line-count-derived result is never smaller than it) |
| `color.background.default` / `color.text.primary` | Container background / text color |
| `color.text.placeholder` | Placeholder text color |
| `color.border.base` | Unfocused border color |
| `color.disabled.{bg,text}` | Disabled background / text |
| `typography.lineHeight.primary` | Line height (the height is derived from rows) |
| `typography.fontFamily` | Chinese-style serif font stack |

## Golden Case Reference

How the 6 assertions of `cases/textarea.json` map to xUnit tests (`tests/LhTextareaTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `input-emits-onvaluechange` | Typing fires onValueChange | `Input_Emits_OnValueChange` |
| `disabled-blocks-input` | The disabled state does not fire and renders the disabled style | `Disabled_Blocks_Input` |
| `showcount-renders-count` | "Current / maximum" takes the countFontSize token | `ShowCount_Renders_Count` |
| `showcount-without-maxlength-renders-length-only` | Without maxlength only the length is shown | `ShowCount_Without_Maxlength_Renders_Length_Only` |
| `focus-uses-focus-token` | The focused border takes the focus token | `Focus_Uses_Focus_Token` |
| `maxlength-truncates-input` | Input beyond maxlength is truncated | `Maxlength_Truncates_Input` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
