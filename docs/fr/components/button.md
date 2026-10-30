# Bouton

Bouton d'action de base. Déclenche une action immédiate ; gère le type, la dimension, le chargement, la désactivation, l'icône et le motif décoratif style chinois.

> Source du contrat : `lh-ui-core/specs/button.json` ·
> Cas dorés : `lh-ui-core/cases/button.json` (8 cas, traduits en tests xUnit)

## Types

Six types déterminent la couleur de fond et la couleur du texte.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Types de Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">Bouton par défaut</button>
      <button class="sim-btn sim-btn--primary">Bouton principal</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--success">Bouton succès</button>
      <button class="sim-btn sim-btn--warning">Bouton avertissement</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--danger">Bouton danger</button>
      <button class="sim-btn sim-btn--info">Bouton info</button>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var button = new LhButton
{
    Type = LhButtonType.Primary,
    Text = "Bouton principal",
};
button.Click += OnButtonClick;
```

## Dimensions

Trois dimensions mappées sur les jetons `button.height` / `paddingHorizontal` / `fontSize` / `iconSize`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dimensions de Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">Bouton petit</button>
      <button class="sim-btn">Bouton moyen</button>
      <button class="sim-btn sim-btn--large">Bouton grand</button>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## États

Désactivé et chargement ne déclenchent pas `Click` ; lorsque les deux se cumulent, le **désactivé prime** (cas doré `disabled-precedes-loading`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>États de Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">Normal</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Désactivé</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>Chargement
      </button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Désactivé prioritaire</button>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Icône et motif décoratif

Le nom de l'icône est résolu par le système d'icônes local (`LhButtonTokens.IconFor` ; actuellement search / refresh / check / close à l'appui, les noms non enregistrés ne sont pas dessinés). Le motif décoratif consomme le jeton `color.decoration` et se superpose en hachures à 45°.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icône et motif</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>Rechercher</button>
      <button class="sim-btn"><span class="sim-btn__icon"></span>Par défaut</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--pattern">Motif principal</button>
      <button class="sim-btn sim-btn--pattern">Motif par défaut</button>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Thèmes clair et sombre

Basculez le thème du site en haut à droite : les couleurs des boutons dans le cadre du simulateur changent ; sur appareil réel, via `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Bouton en sombre</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">Bouton principal</button>
      <button class="sim-btn">Bouton par défaut</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Désactivé</button>
      <button class="sim-btn sim-btn--success">Succès</button>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel ; les valeurs clair/sombre proviennent toutes de l'instantané des jetons core</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `Click` | Événement de clic ; non déclenché si `Disabled` ou `Loading` vaut true | `event Action` | — |
| `Type` | Type de bouton, détermine la couleur de fond et du texte | `LhButtonType` | `Default` |
| `Size` | Dimension du bouton, mappée sur les jetons de dimension | `LhButtonSize` | `Medium` |
| `Disabled` | Désactivé ou non | `bool` | `false` |
| `Loading` | En chargement ou non (affiche un indicateur, ne déclenche pas Click) | `bool` | `false` |
| `Icon` | Nom de l'icône (résolu par le système d'icônes local) | `string?` | `null` |
| `BgPattern` | Dessiner ou non le motif décoratif style chinois | `bool` | `false` |
| `Text` | Contenu textuel du bouton (slot default du spec) | `string` | `""` |

### LhButtonType

| Valeur | Fond | Couleur du texte |
| --- | --- | --- |
| `Default` | `color.background.default` | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.onPrimary` |
| `Success` | `color.success.default` | `color.onPrimary` |
| `Warning` | `color.warning.default` | `color.onPrimary` |
| `Danger` | `color.danger.default` | `color.onPrimary` |
| `Info` | `color.info.default` | `color.onPrimary` |

### LhButtonSize

| Valeur | height | paddingHorizontal | fontSize | iconSize |
| --- | --- | --- | --- | --- |
| `Small` | `button.height.small` (32epx) | `button.paddingHorizontal.small` (16epx) | `button.fontSize.small` (13) | `button.iconSize.small` (16) |
| `Medium` | `button.height.medium` (40epx) | `button.paddingHorizontal.medium` (20epx) | `button.fontSize.medium` (14) | `button.iconSize.medium` (18) |
| `Large` | `button.height.large` (48epx) | `button.paddingHorizontal.large` (24epx) | `button.fontSize.large` (16) | `button.iconSize.large` (20) |

Surcharges de l'état désactivé : fond `color.disabled.bg`, texte `color.disabled.text`, bordure `color.disabled.border`.

## Référence des jetons

Jetons consommés par le bouton (alignés un à un avec les `tokenRefs` de `specs/button.json`) :

| Jeton | Utilisation |
| --- | --- |
| `button.height.{small,medium,large}` | Trois hauteurs |
| `button.paddingHorizontal.{small,medium,large}` | Trois marges internes horizontales |
| `button.fontSize.{small,medium,large}` | Trois tailles de police |
| `button.iconSize.{small,medium,large}` | Trois tailles d'icône |
| `button.borderRadius` / `button.borderWidth` | Arrondi (suit `radius.base`) et largeur de bordure |
| `button.fontWeight` | Graisse de police (suit `typography.fontWeight.primary`, mappée à FontWeights côté appareil) |
| `color.primary.default` / `color.onPrimary` | Fond / texte du type principal |
| `color.{success,warning,danger,info}.default` | Fonds des types sémantiques |
| `color.background.default` / `color.text.primary` / `color.border.base` | Fond / texte / bordure du type par défaut |
| `color.disabled.{bg,text,border}` | Les trois couleurs de l'état désactivé |
| `color.decoration` | Couleur du motif décoratif |
| `spacing.xs` | Espacement entre l'icône et le texte |
| `typography.fontFamily` | Pile de polices avec empattements style chinois |

## Référence des cas dorés

Correspondance entre les 8 assertions de `cases/button.json` et les tests xUnit (`tests/LhButtonTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `click-emits-onclick` | Les gate laissent passer (interactif) | `Enabled_Button_Is_Interactive` |
| `disabled-blocks-click` | L'état désactivé n'est pas interactif | `Disabled_Is_Not_Interactive` |
| `loading-blocks-click` | L'état de chargement n'est pas interactif | `Loading_Is_Not_Interactive` |
| `disabled-precedes-loading` | Le désactivé prime + style désactivé | `Disabled_Precedes_Loading` |
| `default-type-renders-text` | Fond / texte neutres du type par défaut | `Default_Type_Uses_Neutral_Tokens` |
| `primary-type-uses-primary-tokens` | Le type principal consomme les jetons de la couleur principale | `Primary_Type_Uses_Primary_Tokens` |
| `small-size-uses-small-tokens` | La petite taille consomme les jetons de dimension small | `Small_Size_Uses_Small_Tokens` |
| `bgpattern-uses-decoration-token` | Le motif décoratif consomme la couleur de décoration | `BgPattern_Uses_Decoration_Token` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
