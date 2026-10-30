# Champ de saisie

Saisie de texte sur une seule ligne. Gère le type, la dimension, la désactivation, la lecture seule, l'effacement et le nombre maximal de caractères.

> Source du contrat : `lh-ui-core/specs/input.json` ·
> Cas dorés : `lh-ui-core/cases/input.json` (8 cas, traduits en tests xUnit)

## Usage de base

Composant contrôlé : l'hôte écoute `ValueChange` et réécrit la nouvelle valeur dans `Value`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input : base</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷 (porcelaine bleue et blanche)</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field sim-input__field--placeholder">Saisissez un mot-clé</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var input = new LhInput
{
    Placeholder = "Saisissez un mot-clé",
};
input.ValueChange += value => { /* contrôlé : réécrire Value */ };
```

## Dimensions

Trois dimensions mappées sur les jetons `input.height` / `paddingHorizontal` / `fontSize`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dimensions de Input</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input sim-input--small"><div class="sim-input__field">Petit</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">Moyen</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--large"><div class="sim-input__field">Grand</div></div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Type et états

`Password` affiche la valeur masquée par des points ; `Disabled` et `Readonly` ne déclenchent pas `ValueChange`, mais seul `Disabled` revêt l'apparence désactivée ; lors de la prise de focus, la bordure utilise `color.focus` + `input.focusRingWidth`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>États de Input</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">●●●●●●●●</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--disabled"><div class="sim-input__field">Désactivé</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--focus"><div class="sim-input__field">Bordure de focus</div></div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Effacement et comptage

Lorsque `Clearable` vaut true et que la valeur n'est pas vide, un bouton d'effacement s'affiche ; son clic déclenche `ValueChange("")` ; lorsque `Maxlength` est dépassé, les nouvelles saisies sont tronquées.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Effacement et comptage</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷 (porcelaine bleue et blanche)</div>
        <span class="sim-input__clear">✕</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `ValueChange` | Callback de changement de valeur (contrôlé) ; non déclenché en cas de disabled / readonly | `event Action<string>` | — |
| `Focus` | Déclenché lors de la prise de focus | `event Action` | — |
| `Blur` | Déclenché lors de la perte de focus | `event Action` | — |
| `Value` | Valeur textuelle courante (contrôlée) | `string` | `""` |
| `Placeholder` | Texte d'invite de l'espace réservé, utilise le jeton `color.text.placeholder` | `string` | `""` |
| `Type` | Type de saisie ; en mode password, affichage masqué par des points | `LhInputType` | `Text` |
| `Size` | Dimension du champ de saisie, mappée sur les jetons de dimension | `LhInputSize` | `Medium` |
| `Disabled` | Désactivé ou non (ne réagit pas à la saisie, revêt l'apparence désactivée) | `bool` | `false` |
| `Readonly` | Lecture seule ou non (peut recevoir le focus mais n'accepte pas la saisie) | `bool` | `false` |
| `Clearable` | Afficher ou non le bouton d'effacement | `bool` | `false` |
| `Maxlength` | Nombre maximal de caractères ; les saisies dépassant la limite sont tronquées | `int?` | `null` |

### LhInputSize

| Valeur | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `input.height.small` (32epx) | `input.paddingHorizontal.small` (12epx) | `input.fontSize.small` (13) |
| `Medium` | `input.height.medium` (40epx) | `input.paddingHorizontal.medium` (16epx) | `input.fontSize.medium` (14) |
| `Large` | `input.height.large` (48epx) | `input.paddingHorizontal.large` (20epx) | `input.fontSize.large` (16) |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `input.height.{small,medium,large}` | Trois hauteurs |
| `input.paddingHorizontal.{small,medium,large}` | Trois marges internes horizontales |
| `input.fontSize.{small,medium,large}` | Trois tailles de police |
| `input.borderRadius` / `input.borderWidth` | Arrondi et largeur de la bordure hors focus |
| `input.focusRingWidth` / `color.focus` | Largeur et couleur de la bordure de focus |
| `input.clearIconSize` | Dimension de l'icône du bouton d'effacement |
| `color.background.default` / `color.text.primary` | Fond du conteneur / couleur du texte |
| `color.text.placeholder` | Couleur du texte de l'espace réservé |
| `color.border.base` | Couleur de la bordure hors focus |
| `color.disabled.{bg,text}` | Fond / texte de l'état désactivé |
| `typography.fontFamily` | Pile de polices avec empattements style chinois |

## Référence des cas dorés

Correspondance entre les 8 assertions de `cases/input.json` et les tests xUnit (`tests/LhInputTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `input-emits-onvaluechange` | La saisie déclenche onValueChange | `Input_Emits_OnValueChange` |
| `clearable-emits-empty-value` | L'effacement déclenche onValueChange("") | `Clearable_Emits_Empty_Value` |
| `disabled-blocks-input` | L'état désactivé ne déclenche pas et revêt l'apparence désactivée | `Disabled_Blocks_Input` |
| `readonly-blocks-input` | La lecture seule ne déclenche pas mais ne revêt pas l'apparence désactivée | `Readonly_Blocks_Input` |
| `placeholder-uses-placeholder-token` | Le texte de l'espace réservé utilise le jeton placeholder | `Placeholder_Uses_Placeholder_Token` |
| `focus-uses-focus-token` | La bordure de focus utilise le jeton focus | `Focus_Uses_Focus_Token` |
| `small-size-uses-small-tokens` | La petite taille consomme les jetons de dimension small | `Small_Size_Uses_Small_Tokens` |
| `maxlength-truncates-input` | Au-delà de maxlength, la saisie est tronquée | `Maxlength_Truncates_Input` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
