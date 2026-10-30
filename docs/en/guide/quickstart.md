# Quick Start

## Minimal example

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui;
using Top.Lhgb.Ui.Common;

public sealed class DemoPage : Page
{
    public DemoPage()
    {
        // Injects the light palette; pass true for dark. All Lh components follow
        // automatically (LhTheme.ThemeChanged hot repaint)
        LhTheme.Apply(false);

        var button = new LhButton
        {
            Type = LhButtonType.Primary,
            Text = "Primary button",
        };
        button.Click += OnButtonClick;

        Content = new StackPanel { Children = { button } };
    }

    private void OnButtonClick() => { /* Handle the click */ }
}
```

`LhTheme.Apply(isDark)` switches the static palette and fires `ThemeChanged`; every loaded Lh component repaints automatically. Without a call it falls back to the light palette.

## Types and sizes

```csharp
// Six types
new LhButton { Text = "Default" };                                     // Default
new LhButton { Type = LhButtonType.Primary,   Text = "Primary" };
new LhButton { Type = LhButtonType.Success,   Text = "Success" };
new LhButton { Type = LhButtonType.Warning,   Text = "Warning" };
new LhButton { Type = LhButtonType.Danger,    Text = "Danger" };
new LhButton { Type = LhButtonType.Info,      Text = "Info" };

// Three sizes
new LhButton { Size = LhButtonSize.Small,  Text = "Small" };
new LhButton { Size = LhButtonSize.Medium, Text = "Medium" };   // Default
new LhButton { Size = LhButtonSize.Large,  Text = "Large" };
```

## States

```csharp
// Disabled: does not respond to taps (blocked by the Tapped gate) and renders the disabled style
new LhButton { Disabled = true, Text = "Disabled" };

// Loading: shows a ProgressRing and does not fire Click
new LhButton { Loading = true, Text = "Loading" };

// Both set: disabled wins (golden case disabled-precedes-loading)
new LhButton { Disabled = true, Loading = true, Text = "Disabled wins" };
```

## Icon and pattern background

```csharp
// Icon names resolve against the local icon system (LhButtonTokens.IconFor; currently
// search / refresh / check / close; unregistered names draw nothing)
new LhButton { Type = LhButtonType.Primary, Icon = "search", Text = "Search" };

// Chinese-style pattern background (consumes the color.decoration token)
new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = "Pattern" };
```

## Light and dark themes

```csharp
LhTheme.Apply(true);  // Switches to dark globally; every component follows at its next appearance resolution
```

## Form and feedback components

```csharp
// Controlled input: the host listens to ValueChange and writes the value back to Value
var input = new LhInput { Placeholder = "Type a keyword" };
input.ValueChange += value => input.Value = value;

// Checkbox
var checkbox = new LhCheckbox { Label = "青花瓷 (ceramic blue)" };
checkbox.Change += isChecked => { };

// Switch
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };

// Imperative toast: place the host control first, then drive it with the controller
var toast = new LhToast();
toast.Close += () => { };
var panel = new StackPanel { Children = { toast } };
toast.Show("Saved");   // Auto-hides when the timer ends and fires Close
```

## Component preview

Native UI cannot render live in a browser, so this site uses an **inline static HTML phone-frame simulator** to show the appearance:

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>lh-ui-windows</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">Default button</button>
      <button class="sim-btn sim-btn--primary">Primary button</button>
      <button class="sim-btn sim-btn--success">Success</button>
      <button class="sim-btn sim-btn--warning">Warning</button>
      <button class="sim-btn sim-btn--danger">Danger</button>
      <button class="sim-btn sim-btn--info">Info</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">Small</button>
      <button class="sim-btn sim-btn--primary sim-btn--large">Large</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Disabled</button>
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>Loading
      </button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>Search</button>
      <button class="sim-btn sim-btn--primary sim-btn--pattern">Pattern</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML mock preview (Windows / WinUI 3 appearance), not an on-device render; style values are all taken from the core token snapshot — same source, same values as on a real device. For the real appearance, see the demo app and device screenshots.</p>

> The simulator's dark mode follows the site: toggle the theme in the top-right corner and the colors inside the phone frame change (dark tokens are pre-computed by core).
