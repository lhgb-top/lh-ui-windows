# Toast

An ink-colored pill overlay that owns its own visibility and timing. It supports semantic types, auto-hide, and positioning.

> Spec source: `lh-ui-core/specs/toast.json` ·
> Golden cases: `lh-ui-core/cases/toast.json` (6 cases, translated into xUnit tests)

## Basic usage

The imperative interface (the spec's show / hide / tick) lands on this end as methods plus an internal timer:
`Show(text)` makes the toast visible and resets the timer; `Hide()` hides it immediately without firing `Close`.
The timer is carried by a `DispatcherTimer`; when it expires the toast auto-hides and fires `Close`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast basics</span></div>
  <div class="sim-screen">
    <div class="sim-toast">Saved</div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var controller = new LhToastController();
var toast = new LhToast
{
    Duration = 2000,
    Position = LhToastPosition.Center,
    Controller = controller,
};
toast.Close += () => { /* Fired after the timer expires and the toast auto-hides */ };

// The host must mount the toast as the last child of the page Grid (elevation zIndex.toast)
controller.Show("Saved");
```

## Semantic types

When `Type` is not default, a dot in the matching semantic color is drawn before the text (its diameter equals the font size).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast types</span></div>
  <div class="sim-screen">
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--success"></span>Saved successfully</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--warning"></span>Please check your network connection</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--danger"></span>Deletion failed</div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## Positioning

`Position` decides the placement inside the available area; the top and bottom ends keep a `spacing.xl` margin (center has none).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast positions</span></div>
  <div class="sim-screen">
    <div class="sim-toast sim-toast--top">Top toast</div>
    <div class="sim-toast sim-toast--bottom">Bottom toast</div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `Close` | Fired when the timer expires and the toast auto-hides; not fired by an explicit `Hide()` | `event Action` | — |
| `Type` | Semantic type; when not default a dot in the matching semantic color is drawn before the text | `LhToastType` | `Default` |
| `Duration` | Auto-hide duration (milliseconds) | `int` | `2000` |
| `Position` | Placement inside the available area | `LhToastPosition` | `Center` |
| `Controller` | Host-side handle (optional; registers Show / Hide after Loaded) | `LhToastController?` | `null` |

### Methods

| Method | Description |
| --- | --- |
| `Show(string text)` | Shows the toast text and resets the auto-hide timer |
| `Hide()` | Hides immediately without firing Close |

### LhToastType

| Value | Dot color |
| --- | --- |
| `Default` | No dot drawn |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

### LhToastPosition

| Value | Placement |
| --- | --- |
| `Top` | Top (a `spacing.xl` margin at the top end) |
| `Center` | Centered (no margin) |
| `Bottom` | Bottom (a `spacing.xl` margin at the bottom end) |

## Token Reference

| Token | Usage |
| --- | --- |
| `toast.fontSize` | Text font size (also the dot diameter) |
| `toast.paddingHorizontal` / `paddingVertical` | Pill paddings |
| `toast.borderRadius` | Pill corner radius |
| `toast.gap` | Spacing between the dot and the text |
| `color.ink` | Pill background color |
| `color.onPrimary` | Text color |
| `color.{success,warning,danger,info}.default` | Semantic dots |
| `zIndex.toast` | Overlay elevation |
| `spacing.xl` | Top and bottom end margins |
| `typography.fontFamily` | Chinese-style serif font stack |

## Golden Case Reference

How the 6 assertions of `cases/toast.json` map to xUnit tests (`tests/LhToastTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `show-makes-visible` | show makes it visible and renders the text | `Show_Makes_Visible` |
| `auto-hides-after-duration` | The timer expiring auto-hides it and fires onClose | `Auto_Hides_After_Duration` |
| `tick-before-duration-keeps-visible` | Staying before the timer keeps it visible | `Tick_Before_Duration_Keeps_Visible` |
| `hide-is-silent` | An explicit hide does not fire onClose | `Hide_Is_Silent` |
| `success-type-uses-success-token` | The success type consumes the semantic and ink tokens | `Success_Type_Uses_Success_Token` |
| `hidden-emits-no-commands` | When invisible no draw commands are emitted | `Hidden_Emits_No_Commands` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
