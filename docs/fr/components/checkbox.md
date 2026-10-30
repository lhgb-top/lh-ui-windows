# Case à cocher

Case à cocher. Cochée affiche une coche, à l'état indéterminé affiche un trait ; le clic s'applique à toute la ligne (case + zone de l'étiquette).

> Source du contrat : `lh-ui-core/specs/checkbox.json` ·
> Cas dorés : `lh-ui-core/cases/checkbox.json` (6 cas, traduits en tests xUnit)

## Usage de base

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox : base</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box"></span>
        <span class="sim-checkbox__label">Non cochée</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Cochée</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">─</span>
        <span class="sim-checkbox__label">Indéterminé</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var checkbox = new LhCheckbox
{
    Label = "青花瓷 (porcelaine bleue et blanche)",
    Checked = true,
};
checkbox.Change += isChecked => { };
```

## Dimensions

Trois dimensions mappées sur les jetons de dimension de la case `checkbox.size.*`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dimensions de Checkbox</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--small sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Petit</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Moyen</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--large sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Grand</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## États

Lorsque `Disabled` est actif, les clics ne sont pas pris en compte : le fond, la bordure, l'indicateur et le texte utilisent tous les jetons `color.disabled.*` ; `Indeterminate` correspond à l'état indéterminé (trait) ; un clic le fait passer à l'état coché.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>États de Checkbox</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked sim-checkbox__box--disabled">✓</span>
        <span class="sim-checkbox__label">Cochée et désactivée</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--disabled"></span>
        <span class="sim-checkbox__label">Non cochée et désactivée</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `Change` | Callback de changement d'état de la case ; non déclenché en cas de disabled | `event Action<bool>` | — |
| `Checked` | Cochée ou non | `bool` | `false` |
| `Indeterminate` | État indéterminé ou non (indicateur en trait) ; un clic fait passer à l'état coché | `bool` | `false` |
| `Disabled` | Désactivée ou non (ne réagit pas au clic et revêt l'apparence désactivée) | `bool` | `false` |
| `Label` | Étiquette textuelle (dessinée à droite de la case) | `string` | `""` |
| `Size` | Dimension de la case, mappée sur les jetons `checkbox.size.*` | `LhCheckboxSize` | `Medium` |

### LhCheckboxSize

| Valeur | Jeton de dimension |
| --- | --- |
| `Small` | `checkbox.size.small` (14epx) |
| `Medium` | `checkbox.size.medium` (16epx) |
| `Large` | `checkbox.size.large` (18epx) |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `checkbox.size.{small,medium,large}` | Trois dimensions de case |
| `checkbox.borderRadius` / `checkbox.borderWidth` | Arrondi de la case et largeur de bordure |
| `checkbox.checkWidth` | Largeur du trait de la coche / du trait horizontal |
| `checkbox.gap` | Espacement entre la case et le texte |
| `color.primary.default` / `color.onPrimary` | Fond et couleur de l'indicateur des états coché / indéterminé |
| `color.border.base` | Couleur de la bordure à l'état non coché |
| `color.disabled.{bg,border,text}` | Fond / bordure / indicateur de l'état désactivé |
| `color.text.{primary,disabled}` | Couleur du texte de l'étiquette |
| `typography.fontSize.base` / `fontFamily` | Taille de police et pile de polices de l'étiquette |

## Référence des cas dorés

Correspondance entre les 6 assertions de `cases/checkbox.json` et les tests xUnit (`tests/LhCheckboxTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | Un clic sur une case non cochée la coche | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-unchecks` | Un clic sur une case cochée la décoche | `Tap_Checked_Unchecks` |
| `disabled-blocks-change` | L'état désactivé n'est pas interactif | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | L'état coché consomme les jetons de la couleur principale | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | L'état non coché consomme le jeton de bordure | `Unchecked_Uses_Border_Token` |
| `indeterminate-becomes-checked` | L'état indéterminé devient coché après un clic | `Indeterminate_Becomes_Checked` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
