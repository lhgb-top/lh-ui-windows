# Thèmes et mode sombre

## Modèle de jetons

La source de vérité des jetons de la lignée native se trouve dans `lh-ui-core/tokens/*.json` ; le codegen génère les artefacts Windows (`src/Tokens/`, produits gitignore, synchronisés via `pnpm link:tokens`) :

| Artefact | Contenu | Forme sombre |
| --- | --- | --- |
| `LhTokens` | Jeu complet clair : dimensions Button, palette Color, Motion, Radius, Shadow, Spacing, Typography, ZIndex | — |
| `LhDarkTokens` | Uniquement la sous-classe `Color` (modèle à **objets parallèles**) | Le sombre ne couvre que les couleurs ; dimensions / animations / arrondis sont inchangés en sombre |
| `LhShadow` | Support d'ombre (color/radius/offsetX/offsetY/spread) | — |
| `LhEasing` | Easing des animations | — |

> Ce modèle est identique sur android / ios / harmony (objets parallèles) et diffère du volet docs (fusion superficielle de JSON).
> La couche de thème est conçue en conséquence : l'enregistrement `LhColors` ne porte que les couleurs, les dimensions étant lues directement dans `LhTokens` par les composants.

## LhColors

`LhColorsFactory.Light()` / `Dark()` aplatissement les deux classes statiques générées en un enregistrement immuable :

```csharp
public sealed record LhColors
{
    public Color Primary { get; init; }
    public Color OnPrimary { get; init; }
    // ... toutes les couleurs sémantiques
}

public static class LhColorsFactory
{
    public static LhColors Light() => new()
    {
        Primary = LhTokens.Color.Primary.Default,
        // ...
    };

    public static LhColors Dark() => new()
    {
        Primary = LhDarkTokens.Color.Primary.Default,
        // ...
    };
}
```

Les méthodes de fabrique sont publiques : les tests unitaires xUnit peuvent construire directement les palettes pour faire des assertions (voir `Dark_Colors_Come_From_Dark_Tokens` dans chaque test) ; côté application, la consommation se fait via `LhTheme`.

## LhTheme.Apply

```csharp
LhTheme.Apply(isDark);  // bascule la palette et déclenche ThemeChanged
```

Les composants s'abonnent à `LhTheme.ThemeChanged` lors du `Loaded` et s'en désabonnent lors du `Unloaded` ; à la réception de l'événement, ils appellent `ApplyAppearance()` pour recalculer et redessiner — **redessin à chaud du thème, sans reconstruire l'arbre de la page**. Si Apply n'a jamais été appelé, `LhTheme.Current` revient à la palette claire.

## Précalcul du sombre (important)

La lignée web évalue le sombre dynamiquement avec la fonction CSS `color-mix()` ; le natif ne disposant pas de cascade CSS, core a donc **précalculé les expressions sombres en valeurs littérales** (constantes `Color` au format `#AARRGGBB`). Conséquence (plan directeur R-n6) :

> **Lorsque vous modifiez la couleur principale d'un préréglage, les valeurs sombres précalculées ne la suivent pas automatiquement** : il faut les recalculer manuellement et mettre à jour
> `lh-ui-core/tokens/color-dark.json`, puis rafraîchir la *Ligne de base du langage de conception*.

## Conventions spécifiques au volet Windows

- **L'unité de dimension est l'epx** : `LhEpx.Px(token)` marque la valeur du jeton comme des pixels effectifs (les pixels effectifs de WinUI dépendent du facteur d'échelle ; on passe systématiquement par la fonction de marquage, aucun littéral numérique brut dans les composants).
- **Le type des couleurs est `Windows.UI.Color`** : tous les champs de `LhColors` sont des `Color`, les pinceaux sont enveloppés en `SolidColorBrush` par les composants ; les `#AARRGGBB` sont directement émis par le codegen sous forme de `Color.FromArgb(...)`.
- **La graisse nécessite un mappage** : les jetons `typography.fontWeight.*` sont des nombres, tandis que les `FontWeights` de WinUI sont des constantes prédéfinies ; on passe systématiquement par `LhFontWeight.FromToken(token)` (`Common/LhFontWeight.cs`).
- **L'ombre portée passe par Elevation** : la carte de contenu de Dialog / le panneau de Picker expriment la profondeur via `Border.Elevation` + `ThemeShadow` (langage Fluent : boutons plats, couches flottantes avec ombre portée).
- **Visuel de focus** : la prise de focus du champ de saisie utilise la couleur `focus` + une bordure `input.focusRingWidth` (correspond au cadre de focus haute visibilité FocusVisualKind de WinUI).

## Aide-mémoire des jetons

Le tableau complet des jetons (clair / sombre / description) se trouve dans `dist/docs/tokens.md` du dépôt core (généré après `pnpm codegen`).
Le mappage des jetons au niveau du composant figure dans [Référence des jetons de Button](/fr/components/button#reference-des-jetons).
