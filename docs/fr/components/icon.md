# Icône

Composant d'icône. Résout un nom d'icône en glyphe via le système graphique local et le rend ; gère la dimension et la tonalité sémantique.

> Source du contrat : `lh-ui-core/specs/icon.json` ·
> Cas dorés : `lh-ui-core/cases/icon.json` (5 cas, traduits en tests xUnit)

::: tip Notes de plateforme
Le volet Windows utilise actuellement un glyphe unicode de remplacement (`LhIconTokens.GlyphFor`, rendu par un TextBlock) ; en production, il convient de le remplacer par un `FontIcon` (vecteurs Segoe Fluent Icons). Cette stratégie de remplacement est cohérente avec celle du bouton. Les noms d'icônes enregistrés figurent dans la section « Noms d'icônes » ci-dessous.
:::

## Dimensions

Trois dimensions mappées sur le jeton `icon.size`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dimensions de Icon</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--small">⌕</span>
      <span class="sim-icon">⌕</span>
      <span class="sim-icon sim-icon--large">⌕</span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var icon = new LhIcon
{
    Name = "search",
    Size = LhIconSize.Large,
};
```

## Tonalité

`Tone` détermine la couleur du glyphe et correspond un à un aux jetons de couleurs sémantiques ; la tonalité par défaut utilise `color.text.primary`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tonalité de Icon</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">✓ Par défaut</span>
      <span class="sim-icon sim-icon--primary">✓ Couleur principale</span>
      <span class="sim-icon sim-icon--success">✓ Succès</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--warning">⚠ Avertissement</span>
      <span class="sim-icon sim-icon--danger">✕ Danger</span>
      <span class="sim-icon sim-icon--info">ℹ Information</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--regular">✓ Régulier</span>
      <span class="sim-icon sim-icon--secondary">✓ Secondaire</span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Noms d'icônes

Noms d'icônes actuellement enregistrés dans `GlyphFor` et leurs glyphes : `search` ⌕, `refresh` ↻, `check` ✓, `close` ✕,
`warning` ⚠, `info` ℹ, `back` ←, `more` ⋯, `star` ★, `edit` ✎, `delete` ⌫,
`plus` ＋, `minus` －, `arrow` →. Les noms non enregistrés sont remplacés par le glyphe de repli `◆`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icônes enregistrées</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">⌕</span><span class="sim-icon">↻</span><span class="sim-icon">✓</span>
      <span class="sim-icon">✕</span><span class="sim-icon">⚠</span><span class="sim-icon">ℹ</span>
      <span class="sim-icon">←</span><span class="sim-icon">⋯</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon">★</span><span class="sim-icon">✎</span><span class="sim-icon">⌫</span>
      <span class="sim-icon">＋</span><span class="sim-icon">－</span><span class="sim-icon">→</span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Thèmes clair et sombre

Basculez le thème du site en haut à droite : les couleurs des icônes dans le cadre du simulateur changent ; sur appareil réel, via `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icônes en sombre</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--primary">★</span>
      <span class="sim-icon sim-icon--success">✓</span>
      <span class="sim-icon sim-icon--warning">⚠</span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel ; les valeurs clair/sombre proviennent toutes de l'instantané des jetons core</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `Name` | Nom de l'icône (résolu par la table d'icônes locale, les noms non enregistrés sont remplacés par `◆`) | `string` | `""` |
| `Size` | Dimension de l'icône, mappée sur les jetons `icon.size.*` | `LhIconSize` | `Medium` |
| `Tone` | Tonalité de l'icône, mappée sur les jetons de couleurs sémantiques / de texte | `LhIconTone` | `Regular` |

### LhIconSize

| Valeur | Jeton de dimension |
| --- | --- |
| `Small` | `icon.size.small` (16epx) |
| `Medium` | `icon.size.medium` (18epx) |
| `Large` | `icon.size.large` (20epx) |

### LhIconTone

| Valeur | Jeton de couleur |
| --- | --- |
| `Default` | `color.text.primary` |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `icon.size.{small,medium,large}` | Trois dimensions |
| `color.text.{primary,regular,secondary}` | Tonalités neutres |
| `color.{primary,success,warning,danger,info}.default` | Tonalités sémantiques |
| `typography.fontFamily` | Pile de polices des glyphes |

## Référence des cas dorés

Correspondance entre les 5 assertions de `cases/icon.json` et les tests xUnit (`tests/LhIconTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `click-emits-onclick` | Les gate laissent passer (l'icône est interactive) | `Click_Emits_OnClick` |
| `default-tone-uses-text-primary` | La tonalité par défaut utilise le jeton de couleur de texte principal | `Default_Tone_Uses_Text_Primary` |
| `primary-tone-uses-primary-token` | La tonalité principale utilise le jeton de la couleur principale | `Primary_Tone_Uses_Primary_Token` |
| `small-size-uses-small-token` | La petite taille consomme les jetons de dimension small | `Small_Size_Uses_Small_Token` |
| `render-emits-icon-command` | Il y a une et une seule commande d'icône | `Render_Emits_Single_Icon_Command` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
