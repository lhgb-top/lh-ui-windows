# Tag

A tag for marking and selection. It supports six semantic types, three sizes, a plain (outlined) form, a round (pill) form, and a closable form.

> Spec source: `lh-ui-core/specs/tag.json` ·
> Golden cases: `lh-ui-core/cases/tag.json` (6 cases, translated into xUnit tests)

## Types

Six types determine the background, text color, and border color.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tag types</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag">Default</span>
      <span class="sim-tag sim-tag--primary">Primary</span>
      <span class="sim-tag sim-tag--success">Success</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--warning">Warning</span>
      <span class="sim-tag sim-tag--danger">Danger</span>
      <span class="sim-tag sim-tag--info">Info</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var tag = new LhTag
{
    Text = "Primary",
    Type = LhTagType.Primary,
};
```

## Plain and round

When `Plain` is true the tag switches to a light semantic background + semantic text + semantic border; when `Round`
is true the corner radius takes half the height (pill form, paired with the `tag.borderRadius` token).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Plain & round</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--plain">Plain</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">Plain</span>
      <span class="sim-tag sim-tag--danger sim-tag--plain">Plain</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--round">Round</span>
      <span class="sim-tag sim-tag--success sim-tag--round sim-tag--plain">Round plain</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Sizes and close

Three sizes map to the `tag.height` / `paddingHorizontal` / `fontSize` tokens; when `Closable`
is true a close glyph is drawn on the right, and tapping it fires the `Close` event.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Sizes & close</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--small">S</span>
      <span class="sim-tag">M</span>
      <span class="sim-tag sim-tag--large">L</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">Closable <span class="sim-tag__close">✕</span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var tag = new LhTag
{
    Text = "Closable",
    Type = LhTagType.Primary,
    Closable = true,
};
tag.Close += () => { /* Close glyph tapped */ };
```

## Light and dark themes

Toggle the site theme in the top-right corner and the tag colors inside the simulator phone frame change with it; on a real device, switch via `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dark tags</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">Primary</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">Plain</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render; light/dark values are all taken from the core token snapshot</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `Close` | Fired when the close glyph is tapped (requires `Closable` to be true) | `event Action` | — |
| `Text` | Tag text content (the spec's default slot) | `string` | `""` |
| `Type` | Semantic type; determines the background, text color, and border color | `LhTagType` | `Default` |
| `Size` | Tag size; maps to the size tokens | `LhTagSize` | `Medium` |
| `Plain` | Whether to use the light-background outlined form | `bool` | `false` |
| `Round` | Whether to use the pill form (corner radius is half the height) | `bool` | `false` |
| `Closable` | Whether to show the close glyph | `bool` | `false` |

### LhTagType

| Value | Solid background | Light background (plain) | Text color |
| --- | --- | --- | --- |
| `Default` | `color.background.default` | Same | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.primary.bg` | `color.onPrimary` / `color.primary.default` |
| `Success` | `color.success.default` | `color.success.bg` | `color.onPrimary` / `color.success.default` |
| `Warning` | `color.warning.default` | `color.warning.bg` | `color.onPrimary` / `color.warning.default` |
| `Danger` | `color.danger.default` | `color.danger.bg` | `color.onPrimary` / `color.danger.default` |
| `Info` | `color.info.default` | `color.info.bg` | `color.onPrimary` / `color.info.default` |

### LhTagSize

| Value | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `tag.height.small` (20epx) | `tag.paddingHorizontal.small` (8epx) | `tag.fontSize.small` (11) |
| `Medium` | `tag.height.medium` (24epx) | `tag.paddingHorizontal.medium` (12epx) | `tag.fontSize.medium` (12) |
| `Large` | `tag.height.large` (28epx) | `tag.paddingHorizontal.large` (16epx) | `tag.fontSize.large` (13) |

## Token Reference

| Token | Usage |
| --- | --- |
| `tag.height.{small,medium,large}` | Three heights |
| `tag.paddingHorizontal.{small,medium,large}` | Three horizontal paddings |
| `tag.fontSize.{small,medium,large}` | Three font sizes |
| `tag.borderRadius` / `tag.borderWidth` | Corner radius and border width |
| `color.primary.default` / `color.onPrimary` | Primary type background / text |
| `color.{success,warning,danger,info}.{default,bg}` | Semantic type solid / light backgrounds |
| `color.background.default` / `color.text.primary` / `color.border.base` | The default type's three colors |
| `spacing.xs` | Spacing between the close glyph and the text |
| `typography.fontFamily` | Chinese-style serif font stack |

## Golden Case Reference

How the 6 assertions of `cases/tag.json` map to xUnit tests (`tests/LhTagTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `default-type-renders-text` | The default type uses a neutral background / text / border | `Default_Type_Renders_Text` |
| `primary-type-uses-primary-tokens` | The primary type consumes the primary tokens | `Primary_Type_Uses_Primary_Tokens` |
| `plain-uses-bg-tokens` | The plain form consumes the light semantic background tokens | `Plain_Uses_Bg_Tokens` |
| `small-size-uses-small-tokens` | Small consumes the small size tokens | `Small_Size_Uses_Small_Tokens` |
| `round-makes-radius-half-height` | The pill form's corner radius is half the height | `Round_Makes_Radius_Half_Height` |
| `closable-tap-emits-onclose` | Tapping the close glyph fires onClose | `Closable_Tap_Emits_OnClose` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
