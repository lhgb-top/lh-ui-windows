# Barre de navigation

Barre fixe en haut de la page. Trois segments gauche / central / droit, gérant le texte et les icônes.

> Source du contrat : `lh-ui-core/specs/navbar.json` ·
> Cas dorés : `lh-ui-core/cases/navbar.json` (6 cas, traduits en tests xUnit)

## Usage de base

L'ordre d'empilement est `zIndex.sticky` ; l'hôte l'utilise comme barre fixe en haut de la page (première ligne du Grid ou sommet du Canvas).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar : base</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">← Retour</span>
      <span class="sim-navbar__title">青花瓷 (porcelaine bleue et blanche)</span>
      <span class="sim-navbar__right">Plus</span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var navbar = new LhNavbar
{
    Title = "青花瓷 (porcelaine bleue et blanche)",
    LeftText = "Retour",
    LeftIcon = "back",
    RightText = "Plus",
};
navbar.LeftClick += () => { };
navbar.RightClick += () => { };
```

## Icônes

`LeftIcon` / `RightIcon` prennent un nom d'icône local (voir la table des glyphes dans la documentation de Icon) ; lorsque l'icône et le texte sont tous deux renseignés, les deux s'affichent ; lorsqu'un côté n'a aucun contenu, l'événement de clic correspondant n'est pas déclenché.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icônes de Navbar</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">←</span>
      <span class="sim-navbar__title">Icônes seules</span>
      <span class="sim-navbar__right">⋯</span>
    </div>
    <div class="sim-navbar">
      <span class="sim-navbar__title">Aucun contenu sur les côtés</span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Séparateur

Lorsque `Bordered` vaut true, un séparateur `color.border.lighter` est dessiné en bas ; activé par défaut.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Séparateur de Navbar</span></div>
  <div class="sim-screen">
    <div class="sim-navbar sim-navbar--bordered">
      <span class="sim-navbar__title">Avec séparateur</span>
    </div>
    <div class="sim-navbar sim-navbar--borderless">
      <span class="sim-navbar__title">Sans séparateur</span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `LeftClick` | Déclenché lors du clic sur la zone gauche (non déclenché en l'absence de contenu à gauche) | `event Action` | — |
| `RightClick` | Déclenché lors du clic sur la zone droite (non déclenché en l'absence de contenu à droite) | `event Action` | — |
| `Title` | Titre central | `string` | `""` |
| `LeftText` | Texte du bouton de gauche ; lorsque LeftIcon et le texte sont tous deux renseignés, les deux s'affichent | `string` | `""` |
| `LeftIcon` | Nom de l'icône de gauche (rendue par le système d'icônes local) | `string?` | `null` |
| `RightText` | Texte du bouton de droite ; lorsque RightIcon et le texte sont tous deux renseignés, les deux s'affichent | `string` | `""` |
| `RightIcon` | Nom de l'icône de droite (rendue par le système d'icônes local) | `string?` | `null` |
| `Bordered` | Dessiner ou non le séparateur en bas | `bool` | `true` |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `navbar.height` | Hauteur |
| `navbar.paddingHorizontal` | Marges internes gauche et droite |
| `navbar.titleFontSize` | Taille de police du titre et des textes latéraux |
| `navbar.iconSize` | Dimension des icônes latérales |
| `navbar.borderWidth` | Largeur du séparateur en bas |
| `color.background.default` | Couleur de fond |
| `color.text.primary` | Couleur du titre et des textes latéraux |
| `color.border.lighter` | Couleur du séparateur en bas |
| `zIndex.sticky` | Ordre d'empilement de la barre fixe |
| `spacing.xs` | Espacement entre l'icône et le texte |
| `typography.fontFamily` | Pile de polices avec empattements style chinois |

## Référence des cas dorés

Correspondance entre les 6 assertions de `cases/navbar.json` et les tests xUnit (`tests/LhNavbarTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `title-renders-text` | Le titre est rendu selon le jeton de taille de police | `Title_Renders_Text` |
| `leftclick-emits-onleftclick` | Le clic à gauche déclenche onLeftClick | `LeftClick_Emits_OnLeftClick` |
| `rightclick-emits-onrightclick` | Le clic à droite déclenche onRightClick | `RightClick_Emits_OnRightClick` |
| `no-left-content-no-onleftclick` | En l'absence de contenu à gauche, pas de déclenchement | `No_Left_Content_No_OnLeftClick` |
| `bordered-uses-border-token` | Le séparateur consomme le jeton border.lighter | `Bordered_Uses_Border_Token` |
| `height-uses-navbar-height-token` | La hauteur consomme le jeton navbar.height | `Height_Uses_Navbar_Height_Token` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
