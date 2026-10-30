# Dialog

A modal dialog. Backdrop + centered content card + confirm / cancel buttons; it supports semantic types and backdrop dismiss.

> Spec source: `lh-ui-core/specs/dialog.json` ·
> Golden cases: `lh-ui-core/cases/dialog.json` (8 cases, translated into xUnit tests)

## Basic usage

Declarative visibility: when `Visible` is false nothing is rendered; after confirm / cancel `Close` fires and the
host sets `Visible` back to false accordingly.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog basics</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">Confirm deletion</div>
      <div class="sim-dialog__content">This action cannot be undone. Delete this record?</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">Cancel</button>
        <button class="sim-btn sim-btn--danger">Confirm</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

```csharp
var dialog = new LhDialog
{
    Title = "Confirm deletion",
    Content = "This action cannot be undone. Delete this record?",
    Type = LhDialogType.Danger,
    ConfirmText = "Confirm",
    CancelText = "Cancel",
};
dialog.Confirm += () => { /* Perform the deletion */ };
dialog.Close += () => dialog.Visible = false;

dialog.Visible = true;
```

## Semantic types

`Type` decides the confirm button's type (color); the cancel button is always the default type.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog types</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">Notice</div>
      <div class="sim-dialog__content">This is a primary-confirm dialog</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">Cancel</button>
        <button class="sim-btn sim-btn--primary">Confirm</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render</p>

## API

### Props

| Prop | Description | Type | Default |
| --- | --- | --- | --- |
| `Confirm` | Fired on confirm | `event Action` | — |
| `Cancel` | Fired on cancel | `event Action` | — |
| `Close` | Fired after confirm / cancel or (when closable) on a backdrop tap; the host sets Visible to false accordingly | `event Action` | — |
| `Visible` | Whether the dialog is shown; nothing is rendered when false | `bool` | `false` |
| `Title` | Title | `string` | `""` |
| `Content` | Body content | `string` | `""` |
| `Type` | Semantic type; decides the confirm button's type (color) | `LhDialogType` | `Default` |
| `ConfirmText` | Confirm button text; the confirm button is hidden when empty | `string` | `"确认"` |
| `CancelText` | Cancel button text; the cancel button is hidden when empty | `string` | `"取消"` |
| `Closable` | Whether tapping the backdrop area fires Close | `bool` | `false` |

### LhDialogType

| Value | Confirm button type |
| --- | --- |
| `Default` | `LhButtonType.Default` |
| `Primary` | `LhButtonType.Primary` |
| `Success` | `LhButtonType.Success` |
| `Warning` | `LhButtonType.Warning` |
| `Danger` | `LhButtonType.Danger` |
| `Info` | `LhButtonType.Info` |

## Token Reference

| Token | Usage |
| --- | --- |
| `dialog.width` / `dialog.borderRadius` | Content card width and corner radius |
| `dialog.paddingHorizontal` / `paddingVertical` | Content card paddings |
| `dialog.titleFontSize` / `contentFontSize` | Title and body font sizes |
| `dialog.gap` | Spacing between title / content / buttons |
| `color.background.default` | Content card background color |
| `color.background.overlay` | Backdrop color |
| `color.text.primary` / `color.text.regular` | Title / body color |
| `zIndex.modal` / `zIndex.modalBackdrop` | Content card / backdrop elevation |
| `shadow.base` | Content card shadow |

## Golden Case Reference

How the 8 assertions of `cases/dialog.json` map to xUnit tests (`tests/LhDialogTests.cs`):

| case id | Assertion | Local test |
| --- | --- | --- |
| `confirm-emits-onconfirm` | Confirm fires onConfirm | `Confirm_Emits_OnConfirm` |
| `cancel-emits-oncancel` | Cancel fires onCancel | `Cancel_Emits_OnCancel` |
| `confirm-also-emits-onclose` | Confirm also fires onClose | `Confirm_Also_Emits_OnClose` |
| `closable-overlay-emits-onclose` | When closable, a backdrop tap fires onClose | `Closable_Overlay_Emits_OnClose` |
| `not-closable-blocks-overlay-close` | When not closable, the backdrop does not close | `Not_Closable_Blocks_Overlay_Close` |
| `hidden-not-rendered` | Nothing is rendered when invisible | `Hidden_Not_Rendered` |
| `title-renders-text` | Title and body render with the font-size tokens | `Title_Renders_Text` |
| `danger-type-uses-danger-token` | The danger type consumes the danger color token | `Danger_Type_Uses_Danger_Token` |

Plus dark-mode suites: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Run: `cd lh-ui-windows && dotnet test`.
