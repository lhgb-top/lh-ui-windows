# Text

A text component. It renders a single run of text by level, weight, tone, and alignment; every typography value comes from the typography tokens.

> Spec source: `lh-ui-core/specs/text.json` ·
> Golden cases: `lh-ui-core/cases/text.json` (5 cases, translated into xUnit tests)

## Levels

Six levels map to the `typography.fontSize.*` tokens; the default level is `base`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Text levels</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--extra-large">Extra large extraLarge</div>
    <div class="sim-text sim-text--large">Large large</div>
    <div class="sim-text sim-text--medium">Medium medium</div>
    <div class="sim-text">Base base</div>
    <div class="sim-text sim-text--small">Small small</div>
    <div class="sim-text sim-text--extra-small">Extra small extraSmall</div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var text = new LhText
{
    Text = "Title text",
    Level = LhTextLevel.Large,
};
```

## Weight and tone

`Weight` maps to the `typography.fontWeight.*` tokens; `Tone` maps to `color.text.*` (selected takes `color.selected`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Weight & tone</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--bold">Bold bold</div>
    <div class="sim-text sim-text--secondary">Secondary label secondary</div>
    <div class="sim-text sim-text--placeholder">Placeholder text placeholder</div>
    <div class="sim-text sim-text--selected">Selected state selected</div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Alignment

`Align` decides the horizontal placement inside the line-box width (not a design token; mapped to WinUI's `TextAlignment`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Alignment</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--start">Left-aligned start</div>
    <div class="sim-text sim-text--center">Centered center</div>
    <div class="sim-text sim-text--end">Right-aligned end</div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Light and dark themes

Toggle the site theme in the top-right corner and the text colors inside the simulator phone frame change with it; on a real device, switch via `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dark text</span></div>
  <div class="sim-screen">
    <div class="sim-text">Regular text regular</div>
    <div class="sim-text sim-text--secondary">Secondary label secondary</div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render; light/dark values are all taken from the core token snapshot</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `Text` | Text content (the spec's default slot) | `string` | `""` |
| `Level` | Font-size level; maps to `typography.fontSize.*` | `LhTextLevel` | `Base` |
| `Weight` | Font weight; maps to `typography.fontWeight.*` | `LhTextWeight` | `Primary` |
| `Tone` | Text tone; maps to `color.text.*` / `color.selected` | `LhTextTone` | `Regular` |
| `Align` | Horizontal alignment (mapped to `TextAlignment`) | `LhTextAlign` | `Start` |

### LhTextLevel

| Value | Size token |
| --- | --- |
| `ExtraLarge` | `typography.fontSize.extraLarge` (20epx) |
| `Large` | `typography.fontSize.large` (18epx) |
| `Medium` | `typography.fontSize.medium` (16epx) |
| `Base` | `typography.fontSize.base` (14epx) |
| `Small` | `typography.fontSize.small` (13epx) |
| `ExtraSmall` | `typography.fontSize.extraSmall` (12epx) |

### LhTextWeight

| Value | Weight token |
| --- | --- |
| `Primary` | `typography.fontWeight.primary` (400; mapped to Normal on this end) |
| `Bold` | `typography.fontWeight.bold` (600; mapped to SemiBold on this end) |

### LhTextTone

| Value | Color token |
| --- | --- |
| `Primary` | `color.text.primary` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |
| `Placeholder` | `color.text.placeholder` |
| `Disabled` | `color.text.disabled` |
| `Selected` | `color.selected` |

### LhTextAlign

| Value | WinUI mapping |
| --- | --- |
| `Start` | `TextAlignment.Left` |
| `Center` | `TextAlignment.Center` |
| `End` | `TextAlignment.Right` |

## Token Reference

| Token | Usage |
| --- | --- |
| `typography.fontSize.{extraLarge…extraSmall}` | Six font sizes |
| `typography.fontWeight.{primary,bold}` | Two font weights |
| `typography.lineHeight.primary` | Line height (line-box height = font size × line height) |
| `typography.fontFamily` | Chinese-style serif font stack |
| `color.text.{primary,regular,secondary,placeholder,disabled}` | Neutral tones |
| `color.selected` | Selected-state tone |

## Golden Case Reference

How the 5 assertions of `cases/text.json` map to xUnit tests (`tests/LhTextTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `default-level-uses-base-tokens` | The default level consumes the base typography tokens | `Default_Level_Uses_Base_Tokens` |
| `large-level-uses-large-token` | The large level consumes the large font-size token | `Large_Level_Uses_Large_Token` |
| `bold-weight-uses-bold-token` | Bold consumes the bold weight token | `Bold_Weight_Uses_Bold_Token` |
| `secondary-tone-uses-text-secondary` | The secondary tone takes the secondary text color token | `Secondary_Tone_Uses_Text_Secondary` |
| `render-emits-single-text-command` | Exactly one TEXT command is emitted | `Render_Emits_Single_Text_Command` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
