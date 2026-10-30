# Icon

An icon component. It resolves an icon name into a glyph through the local icon system and supports sizes and semantic tones.

> Spec source: `lh-ui-core/specs/icon.json` ·
> Golden cases: `lh-ui-core/cases/icon.json` (5 cases, translated into xUnit tests)

::: tip Platform notes
The Windows end currently placeholders with unicode glyphs (`LhIconTokens.GlyphFor`, rendered by a TextBlock); production
use should swap in `FontIcon` (Segoe Fluent Icons vector assets). This matches button's placeholder strategy. The
registered icon names are listed in the Icon names section below.
:::

## Sizes

Three sizes map to the `icon.size` tokens.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icon sizes</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--small">⌕</span>
      <span class="sim-icon">⌕</span>
      <span class="sim-icon sim-icon--large">⌕</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var icon = new LhIcon
{
    Name = "search",
    Size = LhIconSize.Large,
};
```

## Tones

`Tone` decides the glyph color and maps one-to-one onto the semantic color tokens; the default tone takes `color.text.primary`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icon tones</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">✓ Default</span>
      <span class="sim-icon sim-icon--primary">✓ Primary</span>
      <span class="sim-icon sim-icon--success">✓ Success</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--warning">⚠ Warning</span>
      <span class="sim-icon sim-icon--danger">✕ Danger</span>
      <span class="sim-icon sim-icon--info">ℹ Info</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--regular">✓ Regular</span>
      <span class="sim-icon sim-icon--secondary">✓ Secondary</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Icon names

The icon names currently registered in `GlyphFor` and their glyphs: `search` ⌕, `refresh` ↻, `check` ✓, `close` ✕,
`warning` ⚠, `info` ℹ, `back` ←, `more` ⋯, `star` ★, `edit` ✎, `delete` ⌫,
`plus` ＋, `minus` －, `arrow` →. Unregistered names fall back to `◆`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Registered icons</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">⌕</span><span class="sim-icon">↻</span><span class="sim-icon">✓</span>
      <span class="sim-icon">✕</span><span class="sim-icon">⚠</span><span class="sim-icon">ℹ</span>
      <span class="sim-icon">←</span><span class="sim-icon">⋯</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon">★</span><span class="sim-icon">✎</span><span class="sim-icon">⌫</span>
      <span class="sim-icon">＋</span><span class="sim-icon">－</span><span class="sim-icon">→</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Light and dark themes

Toggle the site theme in the top-right corner and the icon colors inside the simulator phone frame change with it; on a real device, switch via `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dark icons</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--primary">★</span>
      <span class="sim-icon sim-icon--success">✓</span>
      <span class="sim-icon sim-icon--warning">⚠</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render; light/dark values are all taken from the core token snapshot</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `Name` | Icon name (resolved by the local icon table; unregistered names fall back to `◆`) | `string` | `""` |
| `Size` | Icon size; maps to the `icon.size.*` tokens | `LhIconSize` | `Medium` |
| `Tone` | Icon tone; maps to the semantic / text color tokens | `LhIconTone` | `Regular` |

### LhIconSize

| Value | Size token |
| --- | --- |
| `Small` | `icon.size.small` (16epx) |
| `Medium` | `icon.size.medium` (18epx) |
| `Large` | `icon.size.large` (20epx) |

### LhIconTone

| Value | Color token |
| --- | --- |
| `Default` | `color.text.primary` |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |

## Token Reference

| Token | Usage |
| --- | --- |
| `icon.size.{small,medium,large}` | Three sizes |
| `color.text.{primary,regular,secondary}` | Neutral tones |
| `color.{primary,success,warning,danger,info}.default` | Semantic tones |
| `typography.fontFamily` | Glyph font stack |

## Golden Case Reference

How the 5 assertions of `cases/icon.json` map to xUnit tests (`tests/LhIconTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `click-emits-onclick` | The gate passes (the icon is interactive) | `Click_Emits_OnClick` |
| `default-tone-uses-text-primary` | The default tone takes the primary text color token | `Default_Tone_Uses_Text_Primary` |
| `primary-tone-uses-primary-token` | The primary tone takes the primary color token | `Primary_Tone_Uses_Primary_Token` |
| `small-size-uses-small-token` | Small consumes the small size token | `Small_Size_Uses_Small_Token` |
| `render-emits-icon-command` | Exactly one icon command is emitted | `Render_Emits_Single_Icon_Command` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
