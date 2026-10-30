# Bouton radio

Bouton radio. Anneau + point central ; le clic s'applique à toute la ligne ; le composant lui-même ne contient pas de logique de groupe, c'est l'hôte qui gère le groupe radio.

> Source du contrat : `lh-ui-core/specs/radio.json` ·
> Cas dorés : `lh-ui-core/cases/radio.json` (6 cas, traduits en tests xUnit)

## Usage de base

L'hôte écoute `Change` puis passe le `Checked` des autres éléments du groupe à false : le groupe radio est ainsi constitué.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio : base</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">青花瓷 (porcelaine bleue et blanche)</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"></span>
        <span class="sim-radio__label">粉彩瓷 (famille rose)</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var radio = new LhRadio
{
    Label = "青花瓷 (porcelaine bleue et blanche)",
    Value = "qinghua",
    Checked = true,
};
radio.Change += isChecked => { /* déclenché uniquement à l'état non coché et non désactivé ; l'hôte gère le groupe radio */ };
```

## Dimensions

Trois dimensions mappées sur les jetons de dimension de l'anneau `radio.size.*` ; le diamètre du point central = dimension × `radio.dotScale`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dimensions de Radio</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--small"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Petit</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Moyen</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--large"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Grand</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## États

Lorsque `Disabled` est actif, les clics ne sont pas pris en compte : l'anneau extérieur utilise `color.disabled.border` et le point central `color.disabled.text` ; un nouveau clic sur un élément déjà sélectionné ne déclenche pas `Change`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>États de Radio</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"></span>
        <span class="sim-radio__label">Non sélectionné et désactivé</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Sélectionné et désactivé</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `Change` | Callback de changement d'état de sélection ; déclenché uniquement à l'état non coché et non désactivé (la payload vaut toujours true) | `event Action<bool>` | — |
| `Checked` | Sélectionné ou non | `bool` | `false` |
| `Disabled` | Désactivé ou non (ne réagit pas au clic et revêt l'apparence désactivée) | `bool` | `false` |
| `Label` | Étiquette textuelle (dessinée à droite de l'anneau) | `string` | `""` |
| `Value` | Valeur radio (identifiant au sein du groupe, le composant lui-même ne contient pas de logique de groupe) | `string` | `""` |
| `Size` | Dimension de l'anneau, mappée sur les jetons `radio.size.*` | `LhRadioSize` | `Medium` |

### LhRadioSize

| Valeur | Jeton de dimension |
| --- | --- |
| `Small` | `radio.size.small` (14epx) |
| `Medium` | `radio.size.medium` (16epx) |
| `Large` | `radio.size.large` (18epx) |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `radio.size.{small,medium,large}` | Trois dimensions d'anneau |
| `radio.ringWidth` | Largeur de la bordure de l'anneau extérieur |
| `radio.dotScale` | Rapport du diamètre du point central (dimension × dotScale) |
| `radio.gap` | Espacement entre l'anneau et le texte |
| `color.primary.default` | Couleur de l'anneau extérieur et du point à l'état sélectionné |
| `color.border.base` | Couleur de l'anneau extérieur à l'état non sélectionné |
| `color.disabled.{border,text}` | Anneau extérieur / point de l'état désactivé |
| `color.text.{primary,disabled}` | Couleur du texte de l'étiquette |
| `typography.fontSize.base` / `fontFamily` | Taille de police et pile de polices de l'étiquette |

## Référence des cas dorés

Correspondance entre les 6 assertions de `cases/radio.json` et les tests xUnit (`tests/LhRadioTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `tap-unchecked-emits-onchange` | Un clic sur un élément non sélectionné déclenche onChange | `Tap_Unchecked_Emits_OnChange` |
| `tap-checked-keeps-checked` | Un nouvel clic sur un élément sélectionné ne déclenche rien | `Tap_Checked_Keeps_Checked` |
| `disabled-blocks-change` | L'état désactivé n'est pas interactif | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | L'état sélectionné consomme les jetons de la couleur principale | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | L'état non sélectionné consomme le jeton de bordure | `Unchecked_Uses_Border_Token` |
| `small-size-uses-small-token` | La petite taille consomme les jetons de dimension small | `Small_Size_Uses_Small_Token` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
