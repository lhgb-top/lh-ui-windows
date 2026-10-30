# Texte

Composant de texte. Rend un passage de texte selon le niveau, la graisse, la tonalité et l'alignement ; toutes les valeurs typographiques proviennent des jetons typography.

> Source du contrat : `lh-ui-core/specs/text.json` ·
> Cas dorés : `lh-ui-core/cases/text.json` (5 cas, traduits en tests xUnit)

## Niveaux

Six niveaux mappés sur les jetons `typography.fontSize.*` ; le niveau par défaut est `base`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Niveaux de Text</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--extra-large">青花瓷 (porcelaine bleue et blanche) extraLarge</div>
    <div class="sim-text sim-text--large">青花瓷 (porcelaine bleue et blanche) large</div>
    <div class="sim-text sim-text--medium">青花瓷 (porcelaine bleue et blanche) medium</div>
    <div class="sim-text">青花瓷 (porcelaine bleue et blanche) base</div>
    <div class="sim-text sim-text--small">青花瓷 (porcelaine bleue et blanche) small</div>
    <div class="sim-text sim-text--extra-small">青花瓷 (porcelaine bleue et blanche) extraSmall</div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var text = new LhText
{
    Text = "青花瓷 (porcelaine bleue et blanche)",
    Level = LhTextLevel.Large,
};
```

## Graisse et tonalité

`Weight` est mappé sur les jetons `typography.fontWeight.*` ; `Tone` est mappé sur `color.text.*` (selected utilise `color.selected`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Graisse et tonalité</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--bold">Gras bold</div>
    <div class="sim-text sim-text--secondary">Description secondaire secondary</div>
    <div class="sim-text sim-text--placeholder">Espace réservé placeholder</div>
    <div class="sim-text sim-text--selected">État sélectionné selected</div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Alignement

`Align` détermine le positionnement horizontal du texte dans la largeur de la boîte de ligne (ce n'est pas un jeton de design ; il est mappé au `TextAlignment` de WinUI).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Alignement</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--start">Aligné à gauche start</div>
    <div class="sim-text sim-text--center">Centré center</div>
    <div class="sim-text sim-text--end">Aligné à droite end</div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Thèmes clair et sombre

Basculez le thème du site en haut à droite : les couleurs du texte dans le cadre du simulateur changent ; sur appareil réel, via `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Texte en sombre</span></div>
  <div class="sim-screen">
    <div class="sim-text">Texte régulier regular</div>
    <div class="sim-text sim-text--secondary">Description secondaire secondary</div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel ; les valeurs clair/sombre proviennent toutes de l'instantané des jetons core</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `Text` | Contenu textuel (slot default du spec) | `string` | `""` |
| `Level` | Niveau de taille de police, mappé sur `typography.fontSize.*` | `LhTextLevel` | `Base` |
| `Weight` | Graisse de police, mappée sur `typography.fontWeight.*` | `LhTextWeight` | `Primary` |
| `Tone` | Tonalité du texte, mappée sur `color.text.*` / `color.selected` | `LhTextTone` | `Regular` |
| `Align` | Alignement horizontal (mappé sur `TextAlignment`) | `LhTextAlign` | `Start` |

### LhTextLevel

| Valeur | Jeton de taille de police |
| --- | --- |
| `ExtraLarge` | `typography.fontSize.extraLarge` (20epx) |
| `Large` | `typography.fontSize.large` (18epx) |
| `Medium` | `typography.fontSize.medium` (16epx) |
| `Base` | `typography.fontSize.base` (14epx) |
| `Small` | `typography.fontSize.small` (13epx) |
| `ExtraSmall` | `typography.fontSize.extraSmall` (12epx) |

### LhTextWeight

| Valeur | Jeton de graisse |
| --- | --- |
| `Primary` | `typography.fontWeight.primary` (400, mappé à Normal côté appareil) |
| `Bold` | `typography.fontWeight.bold` (600, mappé à SemiBold côté appareil) |

### LhTextTone

| Valeur | Jeton de couleur |
| --- | --- |
| `Primary` | `color.text.primary` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |
| `Placeholder` | `color.text.placeholder` |
| `Disabled` | `color.text.disabled` |
| `Selected` | `color.selected` |

### LhTextAlign

| Valeur | Mappage WinUI |
| --- | --- |
| `Start` | `TextAlignment.Left` |
| `Center` | `TextAlignment.Center` |
| `End` | `TextAlignment.Right` |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `typography.fontSize.{extraLarge…extraSmall}` | Six tailles de police |
| `typography.fontWeight.{primary,bold}` | Deux graisses |
| `typography.lineHeight.primary` | Interligne (la hauteur de la boîte de ligne = taille de police × interligne) |
| `typography.fontFamily` | Pile de polices avec empattements style chinois |
| `color.text.{primary,regular,secondary,placeholder,disabled}` | Tonalités neutres |
| `color.selected` | Tonalité de l'état sélectionné |

## Référence des cas dorés

Correspondance entre les 5 assertions de `cases/text.json` et les tests xUnit (`tests/LhTextTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `default-level-uses-base-tokens` | Le niveau par défaut consomme les jetons typographiques base | `Default_Level_Uses_Base_Tokens` |
| `large-level-uses-large-token` | Le niveau large consomme le jeton de taille large | `Large_Level_Uses_Large_Token` |
| `bold-weight-uses-bold-token` | Le gras consomme le jeton de graisse bold | `Bold_Weight_Uses_Bold_Token` |
| `secondary-tone-uses-text-secondary` | La tonalité secondaire utilise le jeton de couleur de texte secondaire | `Secondary_Tone_Uses_Text_Secondary` |
| `render-emits-single-text-command` | Il y a une et une seule commande TEXT | `Render_Emits_Single_Text_Command` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
