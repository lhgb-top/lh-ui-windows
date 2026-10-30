# Navbar

A fixed bar at the top of a page. Three sections — left, center, right — supporting text and icons.

> Spec source: `lh-ui-core/specs/navbar.json` ·
> Golden cases: `lh-ui-core/cases/navbar.json` (6 cases, translated into xUnit tests)

## Basic usage

The elevation is `zIndex.sticky`; the host mounts it as the fixed top bar of the page (the first Grid row or the top of a Canvas).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar basics</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">← Back</span>
      <span class="sim-navbar__title">青花瓷 (ceramic blue)</span>
      <span class="sim-navbar__right">More</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var navbar = new LhNavbar
{
    Title = "青花瓷 (ceramic blue)",
    LeftText = "Back",
    LeftIcon = "back",
    RightText = "More",
};
navbar.LeftClick += () => { };
navbar.RightClick += () => { };
```

## Icons

`LeftIcon` / `RightIcon` take a local icon name (see the glyph table in the Icon docs); when an icon and a text are both
non-empty both are shown; when a side has no content, its tap event is not fired.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar icons</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">←</span>
      <span class="sim-navbar__title">Icon only</span>
      <span class="sim-navbar__right">⋯</span>
    </div>
    <div class="sim-navbar">
      <span class="sim-navbar__title">No side content</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Divider

When `Bordered` is true a divider in `color.border.lighter` is drawn at the bottom; it is on by default.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar divider</span></div>
  <div class="sim-screen">
    <div class="sim-navbar sim-navbar--bordered">
      <span class="sim-navbar__title">With divider</span>
    </div>
    <div class="sim-navbar sim-navbar--borderless">
      <span class="sim-navbar__title">Without divider</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `LeftClick` | Fired when the left area is tapped (not fired when the left side has no content) | `event Action` | — |
| `RightClick` | Fired when the right area is tapped (not fired when the right side has no content) | `event Action` | — |
| `Title` | Center title | `string` | `""` |
| `LeftText` | Left button text; shown together with LeftIcon when both are non-empty | `string` | `""` |
| `LeftIcon` | Left icon name (rendered by the local icon system) | `string?` | `null` |
| `RightText` | Right button text; shown together with RightIcon when both are non-empty | `string` | `""` |
| `RightIcon` | Right icon name (rendered by the local icon system) | `string?` | `null` |
| `Bordered` | Whether to draw the bottom divider | `bool` | `true` |

## Token Reference

| Token | Usage |
| --- | --- |
| `navbar.height` | Height |
| `navbar.paddingHorizontal` | Left and right paddings |
| `navbar.titleFontSize` | Font size of the title and both sides' texts |
| `navbar.iconSize` | Icon size on both sides |
| `navbar.borderWidth` | Bottom divider width |
| `color.background.default` | Background color |
| `color.text.primary` | Title and side text color |
| `color.border.lighter` | Bottom divider color |
| `zIndex.sticky` | Sticky elevation |
| `spacing.xs` | Spacing between icon and text |
| `typography.fontFamily` | Chinese-style serif font stack |

## Golden Case Reference

How the 6 assertions of `cases/navbar.json` map to xUnit tests (`tests/LhNavbarTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `title-renders-text` | The title renders with the font-size token | `Title_Renders_Text` |
| `leftclick-emits-onleftclick` | A left tap fires onLeftClick | `LeftClick_Emits_OnLeftClick` |
| `rightclick-emits-onrightclick` | A right tap fires onRightClick | `RightClick_Emits_OnRightClick` |
| `no-left-content-no-onleftclick` | Not fired when the left side has no content | `No_Left_Content_No_OnLeftClick` |
| `bordered-uses-border-token` | The divider consumes the border.lighter token | `Bordered_Uses_Border_Token` |
| `height-uses-navbar-height-token` | The height consumes the navbar.height token | `Height_Uses_Navbar_Height_Token` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
