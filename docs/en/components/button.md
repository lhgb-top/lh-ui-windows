# Button

A basic action button. It triggers an immediate action and supports type, size, loading, disabled, icon, and a Chinese-style pattern background.

> Spec source: `lh-ui-core/specs/button.json` ·
> Golden cases: `lh-ui-core/cases/button.json` (8 cases, translated into xUnit tests)

## Types

Six types determine the background and text color.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button types</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">Default button</button>
      <button class="sim-btn sim-btn--primary">Primary button</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--success">Success button</button>
      <button class="sim-btn sim-btn--warning">Warning button</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--danger">Danger button</button>
      <button class="sim-btn sim-btn--info">Info button</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var button = new LhButton
{
    Type = LhButtonType.Primary,
    Text = "Primary button",
};
button.Click += OnButtonClick;
```

## Sizes

Three sizes map to the `button.height` / `paddingHorizontal` / `fontSize` / `iconSize` tokens.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button sizes</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">Small</button>
      <button class="sim-btn">Medium</button>
      <button class="sim-btn sim-btn--large">Large</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## States

Neither disabled nor loading triggers `Click`; when both are set, **disabled wins** (golden case `disabled-precedes-loading`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button states</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">Normal</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled" disabled>Disabled</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>Loading
      </button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Disabled wins</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Icon and pattern background

Icon names resolve against the local icon system (`LhButtonTokens.IconFor`, currently supporting search / refresh / check / close;
unregistered names draw nothing). The pattern background consumes the `color.decoration` token as a 45° hatching overlay.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icons & patterns</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>Search</button>
      <button class="sim-btn"><span class="sim-btn__icon"></span>Default</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--pattern">Primary pattern</button>
      <button class="sim-btn sim-btn--pattern">Default pattern</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Light and dark themes

Toggle the site theme in the top-right corner and the button colors inside the simulator phone frame change with it; on a real device, switch via `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dark buttons</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">Primary button</button>
      <button class="sim-btn">Default button</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--disabled" disabled>Disabled</button>
      <button class="sim-btn sim-btn--success">Success</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render; light/dark values are all taken from the core token snapshot</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `Click` | Tap callback; not triggered when `Disabled` or `Loading` is true | `event Action` | — |
| `Type` | Button type; determines the background and text color | `LhButtonType` | `Default` |
| `Size` | Button size; maps to the size tokens | `LhButtonSize` | `Medium` |
| `Disabled` | Whether the button is disabled | `bool` | `false` |
| `Loading` | Whether the button is loading (shows an indicator and does not fire Click) | `bool` | `false` |
| `Icon` | Icon name (resolved by the local icon system) | `string?` | `null` |
| `BgPattern` | Whether to draw the Chinese-style background pattern | `bool` | `false` |
| `Text` | Button text content (the spec's default slot) | `string` | `""` |

### LhButtonType

| Value | Background | Text color |
| --- | --- | --- |
| `Default` | `color.background.default` | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.onPrimary` |
| `Success` | `color.success.default` | `color.onPrimary` |
| `Warning` | `color.warning.default` | `color.onPrimary` |
| `Danger` | `color.danger.default` | `color.onPrimary` |
| `Info` | `color.info.default` | `color.onPrimary` |

### LhButtonSize

| Value | height | paddingHorizontal | fontSize | iconSize |
| --- | --- | --- | --- | --- |
| `Small` | `button.height.small` (32epx) | `button.paddingHorizontal.small` (16epx) | `button.fontSize.small` (13) | `button.iconSize.small` (16) |
| `Medium` | `button.height.medium` (40epx) | `button.paddingHorizontal.medium` (20epx) | `button.fontSize.medium` (14) | `button.iconSize.medium` (18) |
| `Large` | `button.height.large` (48epx) | `button.paddingHorizontal.large` (24epx) | `button.fontSize.large` (16) | `button.iconSize.large` (20) |

The disabled state overrides: background `color.disabled.bg`, text `color.disabled.text`, border `color.disabled.border`.

## Token Reference

Tokens consumed by the button (aligned one-to-one with the `tokenRefs` of `specs/button.json`):

| Token | Usage |
| --- | --- |
| `button.height.{small,medium,large}` | Three heights |
| `button.paddingHorizontal.{small,medium,large}` | Three horizontal paddings |
| `button.fontSize.{small,medium,large}` | Three font sizes |
| `button.iconSize.{small,medium,large}` | Three icon sizes |
| `button.borderRadius` / `button.borderWidth` | Corner radius (follows `radius.base`) and border width |
| `button.fontWeight` | Font weight (follows `typography.fontWeight.primary`; mapped to FontWeights on this end) |
| `color.primary.default` / `color.onPrimary` | Primary type background / text |
| `color.{success,warning,danger,info}.default` | Semantic type backgrounds |
| `color.background.default` / `color.text.primary` / `color.border.base` | Default type background / text / border |
| `color.disabled.{bg,text,border}` | The three disabled colors |
| `color.decoration` | Pattern color |
| `spacing.xs` | Spacing between icon and text |
| `typography.fontFamily` | Chinese-style serif font stack |

## Golden Case Reference

How the 8 assertions of `cases/button.json` map to xUnit tests (`tests/LhButtonTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `click-emits-onclick` | The gate passes (interactive) | `Enabled_Button_Is_Interactive` |
| `disabled-blocks-click` | The disabled state is not interactive | `Disabled_Is_Not_Interactive` |
| `loading-blocks-click` | The loading state is not interactive | `Loading_Is_Not_Interactive` |
| `disabled-precedes-loading` | Disabled wins, with the disabled style | `Disabled_Precedes_Loading` |
| `default-type-renders-text` | The default type uses a neutral background / text | `Default_Type_Uses_Neutral_Tokens` |
| `primary-type-uses-primary-tokens` | The primary type consumes the primary tokens | `Primary_Type_Uses_Primary_Tokens` |
| `small-size-uses-small-tokens` | Small consumes the small size tokens | `Small_Size_Uses_Small_Tokens` |
| `bgpattern-uses-decoration-token` | The pattern background consumes the decoration color | `BgPattern_Uses_Decoration_Token` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
