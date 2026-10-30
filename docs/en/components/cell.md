# Cell

A list-row cell. Left icon + title (optionally with a required asterisk) + label, right-side value and an indicator arrow, and an optional bottom divider.

> Spec source: `lh-ui-core/specs/cell.json` ·
> Golden cases: `lh-ui-core/cases/cell.json` (6 cases, translated into xUnit tests)

## Basic usage

The title takes `color.text.primary`, the value and label take `color.text.secondary` — all three are token-driven.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Cell basics</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Row title</div>
        <div class="sim-cell__label">Label text</div>
      </div>
      <div class="sim-cell__value">Row content</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Cell with divider</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var cell = new LhCell
{
    Title = "Row title",
    Label = "Label text",
    Value = "Row content",
};
```

## Icon and required

`Icon` takes a local icon name (see the glyph table in the Icon docs) and is drawn on the left;
when `Required` is true a red asterisk in `color.danger.default` is drawn before the title.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icon & required</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <span class="sim-cell__icon">★</span>
      <div class="sim-cell__main">
        <div class="sim-cell__title">With icon</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title"><span class="sim-cell__required">✱</span>Required field</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Divider and tap

When `Bordered` is true a divider in `color.border.lighter` is drawn at the bottom; when `Clickable` is true
an indicator arrow is drawn on the right and tapping fires the `Click` event.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Divider & tap</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">Clickable</div></div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">Not clickable</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var cell = new LhCell
{
    Title = "Clickable",
    Clickable = true,
    Bordered = true,
};
cell.Click += () => { /* Fires only when Clickable is true */ };
```

## Light and dark themes

Toggle the site theme in the top-right corner and the text and divider colors inside the simulator phone frame change with it; on a real device, switch via `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dark cells</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Row title</div>
        <div class="sim-cell__label">Label text</div>
      </div>
      <div class="sim-cell__value">Row content</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render; light/dark values are all taken from the core token snapshot</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `Click` | Tap callback; fired only when `Clickable` is true | `event Action` | — |
| `Title` | Title (the spec's title slot) | `string` | `""` |
| `Label` | Label text under the title (the label slot) | `string` | `""` |
| `Value` | Right-side value text (the value slot) | `string` | `""` |
| `Icon` | Left icon name (resolved by the local icon table) | `string?` | `null` |
| `Required` | Whether to draw the required red asterisk before the title | `bool` | `false` |
| `Clickable` | Whether the cell is clickable (shows the arrow; gates Click) | `bool` | `false` |
| `Bordered` | Whether to draw the bottom divider | `bool` | `true` |

## Token Reference

| Token | Usage |
| --- | --- |
| `cell.paddingHorizontal` / `cell.paddingVertical` | Paddings |
| `cell.gap` | Vertical spacing between title and label |
| `cell.fontSizeTitle` / `fontSizeLabel` / `fontSizeValue` | Three font sizes |
| `color.text.{primary,secondary,regular}` | Title / label / value / icon colors |
| `color.danger.default` | Required red asterisk |
| `color.border.lighter` | Bottom divider |
| `icon.size.medium` | Left icon size |
| `spacing.{sm,xs}` | Icon spacing / internal element spacing |
| `typography.lineHeight.primary` | Text line height |

## Golden Case Reference

How the 6 assertions of `cases/cell.json` map to xUnit tests (`tests/LhCellTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `title-and-value-render` | Both title and value are rendered | `Title_And_Value_Render` |
| `label-renders-secondary-tone` | The label takes the secondary text color token | `Label_Renders_Secondary_Tone` |
| `required-uses-danger-token` | The required asterisk consumes the danger color token | `Required_Uses_Danger_Token` |
| `bordered-emits-line-command` | The bordered form emits a LINE command | `Bordered_Emits_Line_Command` |
| `clickable-emits-onclick` | A tap fires onClick when clickable | `Clickable_Emits_OnClick` |
| `not-clickable-no-onclick` | No onClick when not clickable | `Not_Clickable_No_OnClick` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
