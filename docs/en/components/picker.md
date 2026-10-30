# Picker

A bottom-sheet single-column wheel picker. It supports options, index clamping, and a toolbar.

> Spec source: `lh-ui-core/specs/picker.json` ·
> Golden cases: `lh-ui-core/cases/picker.json` (8 cases, translated into xUnit tests)

## Basic usage

Declarative visibility: when `Visible` is false nothing is rendered; after confirm / cancel `Close` fires and the
host sets `Visible` back to false accordingly.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Picker basics</span></div>
  <div class="sim-screen">
    <div class="sim-picker-overlay"></div>
    <div class="sim-picker">
      <div class="sim-picker__toolbar">
        <span class="sim-picker__cancel">Cancel</span>
        <span class="sim-picker__title">Select</span>
        <span class="sim-picker__confirm">Confirm</span>
      </div>
      <div class="sim-picker__wheel">
        <span class="sim-picker__item">Option one</span>
        <span class="sim-picker__item sim-picker__item--active">Option two</span>
        <span class="sim-picker__item">Option three</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var picker = new LhPicker
{
    Options = new[] { "Option one", "Option two", "Option three" },
    Value = 1,
    Title = "Select",
};
picker.ValueChange += index => { /* Selected index after the wheel stops */ };
picker.Confirm += index => { /* Currently selected index */ };
picker.Close += () => picker.Visible = false;

picker.Visible = true;
```

## Index clamping

When `Value` is out of range it is clamped into the options range: negatives become 0, and anything above
the option count - 1 becomes the last item.

```csharp
Assert.Equal(0, LhPickerTokens.ClampIndex(-1, 3));   // Negatives become 0
Assert.Equal(2, LhPickerTokens.ClampIndex(100, 3));  // Overflow becomes the last item
```

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `ValueChange` | Fired when the wheel stops and the selected item changes | `event Action<int>` | — |
| `Confirm` | Fired when the confirm button is tapped, carrying the currently selected index | `event Action<int>` | — |
| `Cancel` | Fired when the cancel button is tapped | `event Action` | — |
| `Close` | Fired after confirm / cancel; the host sets Visible to false accordingly | `event Action` | — |
| `Visible` | Whether the picker is shown; nothing is rendered when false | `bool` | `false` |
| `Options` | Array of option texts (a single column) | `string[]?` | `null` |
| `Value` | Selected index (0-based); clamped into the options range when out of range | `int` | `0` |
| `Title` | Title in the middle of the toolbar | `string` | `""` |
| `ConfirmText` | Confirm button text | `string` | `"确认"` |
| `CancelText` | Cancel button text | `string` | `"取消"` |

## Token Reference

| Token | Usage |
| --- | --- |
| `picker.height` | Panel height |
| `picker.toolbarHeight` | Top toolbar height (wheel area height = panel height - toolbar height) |
| `picker.itemHeight` | Option height (the central indicator spacing is the same) |
| `picker.itemFontSize` | Option and toolbar font size |
| `picker.borderRadius` | Top corner radius of the panel |
| `color.background.default` | Panel background color |
| `color.background.overlay` | Backdrop color |
| `color.text.primary` | Title and option color |
| `color.text.secondary` | Cancel button color |
| `color.primary.default` | Confirm button color |
| `color.border.lighter` | Central indicator line color |
| `zIndex.modal` / `zIndex.modalBackdrop` | Panel / backdrop elevation |

## Golden Case Reference

How the 8 assertions of `cases/picker.json` map to xUnit tests (`tests/LhPickerTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `confirm-emits-onconfirm-with-value` | Confirm carries the currently selected index | `Confirm_Emits_OnConfirm_With_Value` |
| `confirm-also-emits-onclose` | Confirm also fires onClose | `Confirm_Also_Emits_OnClose` |
| `cancel-emits-oncancel` | Cancel fires onCancel | `Cancel_Emits_OnCancel` |
| `scroll-emits-onvaluechange` | The wheel stopping fires onValueChange | `Scroll_Emits_OnValueChange` |
| `clamp-negative-value-to-zero` | A negative index is clamped to 0 | `Clamp_Negative_Value_To_Zero` |
| `clamp-overflow-value-to-last` | An overflowing index is clamped to the last item | `Clamp_Overflow_Value_To_Last` |
| `title-renders-text` | The title renders with the font-size token | `Title_Renders_Text` |
| `hidden-not-rendered` | Nothing is rendered when invisible | `Hidden_Not_Rendered` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
