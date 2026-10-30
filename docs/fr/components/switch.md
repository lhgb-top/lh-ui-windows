# Interrupteur

Interrupteur. Piste capsule + curseur rond, bascule au clic ; gère la désactivation et le chargement.

> Source du contrat : `lh-ui-core/specs/switch.json` ·
> Cas dorés : `lh-ui-core/cases/switch.json` (6 cas, traduits en tests xUnit)

## Usage de base

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch : base</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };
```

## Dimensions

Trois dimensions mappées sur les jetons `switch.width` / `switch.height` ; le diamètre du curseur = hauteur - 2 × `switch.gap`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dimensions de Switch</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--small sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--large sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## États

`Disabled` et `Loading` ne réagissent pas au clic ; en état `Loading`, l'anneau de chargement de la couleur principale est dessiné à la place du curseur.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>États de Switch</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--checked sim-switch--disabled"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--loading"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `Change` | Callback de changement d'état de l'interrupteur ; non déclenché en cas de disabled ou loading | `event Action<bool>` | — |
| `Checked` | Activé ou non | `bool` | `false` |
| `Disabled` | Désactivé ou non (ne réagit pas au clic et revêt l'apparence désactivée) | `bool` | `false` |
| `Loading` | En chargement ou non (affiche un anneau de chargement à la place du curseur et ne réagit pas au clic) | `bool` | `false` |
| `Size` | Dimension de l'interrupteur, mappée sur les jetons `switch.width` / `switch.height` | `LhSwitchSize` | `Medium` |

### LhSwitchSize

| Valeur | width | height |
| --- | --- | --- |
| `Small` | `switch.width.small` (40epx) | `switch.height.small` (22epx) |
| `Medium` | `switch.width.medium` (48epx) | `switch.height.medium` (26epx) |
| `Large` | `switch.width.large` (56epx) | `switch.height.large` (30epx) |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `switch.width.{small,medium,large}` | Trois largeurs de piste |
| `switch.height.{small,medium,large}` | Trois hauteurs de piste |
| `switch.gap` | Espace blanc entre le curseur et la piste (diamètre du curseur = hauteur - 2 × gap) |
| `color.primary.default` | Couleur de la piste à l'état activé |
| `color.border.base` | Couleur de la piste à l'état désactivé |
| `color.onPrimary` | Couleur du curseur |
| `color.disabled.bg` / `color.text.disabled` | Couleurs de la piste / du curseur à l'état désactivé |

## Référence des cas dorés

Correspondance entre les 6 assertions de `cases/switch.json` et les tests xUnit (`tests/LhSwitchTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | Le clic bascule l'état et déclenche onChange | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-turns-off` | Un clic sur l'état activé le désactive | `Tap_Checked_Turns_Off` |
| `disabled-blocks-change` | L'état désactivé n'est pas interactif | `Disabled_Blocks_Change` |
| `loading-blocks-change` | L'état de chargement n'est pas interactif | `Loading_Blocks_Change` |
| `checked-uses-primary-token` | L'état activé consomme le jeton de la couleur principale | `Checked_Uses_Primary_Token` |
| `unchecked-uses-border-token` | L'état désactivé consomme le jeton de bordure | `Unchecked_Uses_Border_Token` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
