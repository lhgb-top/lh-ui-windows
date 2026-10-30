# Cellule

Cellule de ligne de liste. Icône à gauche + titre (avec étoile rouge de champ obligatoire possible) + description, valeur à droite avec flèche d'indication, séparateur en bas optionnel.

> Source du contrat : `lh-ui-core/specs/cell.json` ·
> Cas dorés : `lh-ui-core/cases/cell.json` (6 cas, traduits en tests xUnit)

## Usage de base

Le titre utilise `color.text.primary`, la valeur et la description utilisent `color.text.secondary` ; ces trois éléments sont tous pilotés par les jetons.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Cell : base</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Titre de ligne</div>
        <div class="sim-cell__label">Texte de description</div>
      </div>
      <div class="sim-cell__value">Contenu de ligne</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Cellule avec séparateur</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var cell = new LhCell
{
    Title = "Titre de ligne",
    Label = "Texte de description",
    Value = "Contenu de ligne",
};
```

## Icône et champ obligatoire

`Icon` prend un nom d'icône local (voir la table des glyphes dans la documentation de Icon) et le dessine à gauche ;
lorsque `Required` vaut true, une étoile rouge `color.danger.default` est dessinée devant le titre.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icône et champ obligatoire</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <span class="sim-cell__icon">★</span>
      <div class="sim-cell__main">
        <div class="sim-cell__title">Avec icône</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title"><span class="sim-cell__required">✱</span>Champ obligatoire</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Séparateur et clic

Lorsque `Bordered` vaut true, un séparateur `color.border.lighter` est dessiné en bas ; lorsque `Clickable` vaut true, une flèche d'indication est dessinée à droite et le clic déclenche l'événement `Click`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Séparateur et clic</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">Cliquable</div></div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">Non cliquable</div></div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var cell = new LhCell
{
    Title = "Cliquable",
    Clickable = true,
    Bordered = true,
};
cell.Click += () => { /* déclenché uniquement lorsque Clickable vaut true */ };
```

## Thèmes clair et sombre

Basculez le thème du site en haut à droite : les couleurs du texte et du séparateur dans le cadre du simulateur changent ; sur appareil réel, via `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Cellules en sombre</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Titre de ligne</div>
        <div class="sim-cell__label">Texte de description</div>
      </div>
      <div class="sim-cell__value">Contenu de ligne</div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel ; les valeurs clair/sombre proviennent toutes de l'instantané des jetons core</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `Click` | Événement de clic ; déclenché uniquement lorsque `Clickable` vaut true | `event Action` | — |
| `Title` | Titre (slot title du spec) | `string` | `""` |
| `Label` | Texte de description sous le titre (slot label) | `string` | `""` |
| `Value` | Texte de la valeur à droite (slot value) | `string` | `""` |
| `Icon` | Nom de l'icône à gauche (résolu par la table d'icônes locale) | `string?` | `null` |
| `Required` | Dessiner ou non l'étoile rouge de champ obligatoire devant le titre | `bool` | `false` |
| `Clickable` | Cliquable ou non (affiche la flèche, met le Click sous gate) | `bool` | `false` |
| `Bordered` | Dessiner ou non le séparateur en bas | `bool` | `true` |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `cell.paddingHorizontal` / `cell.paddingVertical` | Marges internes |
| `cell.gap` | Espacement vertical entre le titre et la description |
| `cell.fontSizeTitle` / `fontSizeLabel` / `fontSizeValue` | Trois tailles de police |
| `color.text.{primary,secondary,regular}` | Couleurs du titre / de la description / de la valeur / de l'icône |
| `color.danger.default` | Étoile rouge de champ obligatoire |
| `color.border.lighter` | Séparateur en bas |
| `icon.size.medium` | Dimension de l'icône à gauche |
| `spacing.{sm,xs}` | Espacement de l'icône / espacement des éléments internes |
| `typography.lineHeight.primary` | Interligne du texte |

## Référence des cas dorés

Correspondance entre les 6 assertions de `cases/cell.json` et les tests xUnit (`tests/LhCellTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `title-and-value-render` | Le titre et la valeur sont tous deux rendus | `Title_And_Value_Render` |
| `label-renders-secondary-tone` | Le texte de description utilise le jeton de couleur de texte secondaire | `Label_Renders_Secondary_Tone` |
| `required-uses-danger-token` | L'étoile rouge de champ obligatoire consomme le jeton de couleur danger | `Required_Uses_Danger_Token` |
| `bordered-emits-line-command` | La forme avec séparateur émet une commande LINE | `Bordered_Emits_Line_Command` |
| `clickable-emits-onclick` | Lorsque cliquable, le clic déclenche onClick | `Clickable_Emits_OnClick` |
| `not-clickable-no-onclick` | Lorsque non cliquable, onClick n'est pas déclenché | `Not_Clickable_No_OnClick` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
