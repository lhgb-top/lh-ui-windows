# Étiquette

Étiquette de marquage et de sélection. Gère six types sémantiques, trois dimensions, la forme contour, la forme arrondie et la forme refermable.

> Source du contrat : `lh-ui-core/specs/tag.json` ·
> Cas dorés : `lh-ui-core/cases/tag.json` (6 cas, traduits en tests xUnit)

## Types

Six types déterminent la couleur de fond, la couleur du texte et la couleur de la bordure.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Types de Tag</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag">Par défaut</span>
      <span class="sim-tag sim-tag--primary">Principal</span>
      <span class="sim-tag sim-tag--success">Succès</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--warning">Avertissement</span>
      <span class="sim-tag sim-tag--danger">Danger</span>
      <span class="sim-tag sim-tag--info">Information</span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var tag = new LhTag
{
    Text = "Principal",
    Type = LhTagType.Primary,
};
```

## Contour et arrondi

Lorsque `Plain` vaut true, l'étiquette passe à un fond sémantique clair + texte sémantique + bordure sémantique ; lorsque `Round` vaut true, l'arrondi vaut la moitié de la hauteur (forme capsule, par ailleurs couverte par le jeton `tag.borderRadius`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Contour et arrondi</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--plain">Contour</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">Contour</span>
      <span class="sim-tag sim-tag--danger sim-tag--plain">Contour</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--round">Arrondi</span>
      <span class="sim-tag sim-tag--success sim-tag--round sim-tag--plain">Arrondi contour</span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Dimension et fermeture

Trois dimensions mappées sur les jetons `tag.height` / `paddingHorizontal` / `fontSize` ; lorsque `Closable` vaut true, un symbole de fermeture est dessiné à droite et son clic déclenche l'événement `Close`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dimension et fermeture</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--small">Petit</span>
      <span class="sim-tag">Moyen</span>
      <span class="sim-tag sim-tag--large">Grand</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">Refermable <span class="sim-tag__close">✕</span></span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var tag = new LhTag
{
    Text = "Refermable",
    Type = LhTagType.Primary,
    Closable = true,
};
tag.Close += () => { /* clic sur le symbole de fermeture */ };
```

## Thèmes clair et sombre

Basculez le thème du site en haut à droite : les couleurs des étiquettes dans le cadre du simulateur changent ; sur appareil réel, via `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Étiquettes en sombre</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">Principal</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">Contour</span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel ; les valeurs clair/sombre proviennent toutes de l'instantané des jetons core</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `Close` | Déclenché lors du clic sur le symbole de fermeture (requiert `Closable` à true) | `event Action` | — |
| `Text` | Contenu textuel de l'étiquette (slot default du spec) | `string` | `""` |
| `Type` | Type sémantique, détermine la couleur de fond, la couleur du texte et la couleur de la bordure | `LhTagType` | `Default` |
| `Size` | Dimension de l'étiquette, mappée sur les jetons de dimension | `LhTagSize` | `Medium` |
| `Plain` | Forme contour à fond clair ou non | `bool` | `false` |
| `Round` | Forme capsule ou non (l'arrondi vaut la moitié de la hauteur) | `bool` | `false` |
| `Closable` | Afficher ou non le symbole de fermeture | `bool` | `false` |

### LhTagType

| Valeur | Fond opaque | Fond clair (plain) | Couleur du texte |
| --- | --- | --- | --- |
| `Default` | `color.background.default` | Identique | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.primary.bg` | `color.onPrimary` / `color.primary.default` |
| `Success` | `color.success.default` | `color.success.bg` | `color.onPrimary` / `color.success.default` |
| `Warning` | `color.warning.default` | `color.warning.bg` | `color.onPrimary` / `color.warning.default` |
| `Danger` | `color.danger.default` | `color.danger.bg` | `color.onPrimary` / `color.danger.default` |
| `Info` | `color.info.default` | `color.info.bg` | `color.onPrimary` / `color.info.default` |

### LhTagSize

| Valeur | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `tag.height.small` (20epx) | `tag.paddingHorizontal.small` (8epx) | `tag.fontSize.small` (11) |
| `Medium` | `tag.height.medium` (24epx) | `tag.paddingHorizontal.medium` (12epx) | `tag.fontSize.medium` (12) |
| `Large` | `tag.height.large` (28epx) | `tag.paddingHorizontal.large` (16epx) | `tag.fontSize.large` (13) |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `tag.height.{small,medium,large}` | Trois hauteurs |
| `tag.paddingHorizontal.{small,medium,large}` | Trois marges internes horizontales |
| `tag.fontSize.{small,medium,large}` | Trois tailles de police |
| `tag.borderRadius` / `tag.borderWidth` | Arrondi et largeur de bordure |
| `color.primary.default` / `color.onPrimary` | Fond / texte du type principal |
| `color.{success,warning,danger,info}.{default,bg}` | Fond opaque / fond clair des types sémantiques |
| `color.background.default` / `color.text.primary` / `color.border.base` | Les trois couleurs du type par défaut |
| `spacing.xs` | Espacement entre le symbole de fermeture et le texte |
| `typography.fontFamily` | Pile de polices avec empattements style chinois |

## Référence des cas dorés

Correspondance entre les 6 assertions de `cases/tag.json` et les tests xUnit (`tests/LhTagTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `default-type-renders-text` | Fond / texte / bordure neutres du type par défaut | `Default_Type_Renders_Text` |
| `primary-type-uses-primary-tokens` | Le type principal consomme les jetons de la couleur principale | `Primary_Type_Uses_Primary_Tokens` |
| `plain-uses-bg-tokens` | La forme fond clair consomme les jetons de fond clair sémantiques | `Plain_Uses_Bg_Tokens` |
| `small-size-uses-small-tokens` | La petite taille consomme les jetons de dimension small | `Small_Size_Uses_Small_Tokens` |
| `round-makes-radius-half-height` | L'arrondi de la forme capsule vaut la moitié de la hauteur | `Round_Makes_Radius_Half_Height` |
| `closable-tap-emits-onclose` | Le clic sur le symbole de fermeture déclenche onClose | `Closable_Tap_Emits_OnClose` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
