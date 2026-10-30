# Introduction

**lh-ui-windows** est l'implémentation du volet Windows de la **lignée native** de la famille lh-ui : une bibliothèque de composants UI basée sur **C# + WinUI 3** (.NET 8, Windows App SDK).

## Position dans la famille

```
                        ┌── lignée web : share (TS core + SCSS) → plus / vue2 / react / uniapp
famille lh-ui ─────────┤
                        └── lignée native : core (jetons JSON + contrats + codegen) → android / ios / harmony / windows
                                                                    ↑ ce dépôt
```

- **Centre de la compilation** : `lh-ui-core` est la source de vérité des jetons de la lignée native (`tokens/*.json`), des contrats des composants (`specs/`), des cas dorés (`cases/`) et des outils de codegen. Il génère les squelettes de jetons C# (`LhTokens` / `LhDarkTokens` / `LhShadow` / `LhEasing`), **sans générer la moindre implémentation de composant**.
- **Responsabilités de ce dépôt** : écrire à la main les composants WinUI 3, l'intégration des thèmes (`LhTheme.Apply` + événement `ThemeChanged`), la liaison des propriétés, des énumérations et des valeurs par défaut, ainsi que la traduction des cas dorés en tests xUnit.
- **Indépendance vis-à-vis de la lignée web** : aucune dépendance à `@lh-ui/share`, aucune contrainte de mise en release entre les deux. La cohérence visuelle est assurée par un alignement manuel avec la *Ligne de base du langage de conception*.

> **Historique** : ce dépôt a remplacé le 2026-10-12 le défunt `lh-ui-c` (volet embarqué C/C++) : le quatrième membre de la lignée native est passé du volet embarqué au volet Windows desktop (plan directeur D10).

## Langage de design

Thème style chinois : couleur principale en bleu 青花瓷 (porcelaine bleue et blanche) (`#2B4C7C`), fond en blanc 宣纸白 (blanc papier de riz) (`#FAF6F0`), succès en vert 松花绿 (vert pin), avertissement en 藤黄 (gamboge), danger en 朱砂 (cinabre), pile de polices avec empattements (Noto Serif SC → STSong → SimSun). Toutes les valeurs visuelles sont des jetons : aucune valeur littérale n'apparaît dans les composants. L'expression visuelle est canalisée par la retenue géométrique de Fluent (hiérarchie des arrondis et bordures fines, pilotées par les jetons).

## Pourquoi WinUI 3

- UI moderne et déclarative de Windows App SDK, mentalement isomorphe à Compose / SwiftUI / ArkUI, ce qui facilite l'alignement des contrats entre les plateformes
- Aucun runtime web, aucun pont HTML/CSS : des performances natives
- Distribution via NuGet (une fois la chaîne de publication nM6 ouverte), intégration en un geste à l'écosystème .NET

## État actuel

- **Les 15 composants P0 sont livrés** (consommation des jetons + analyse d'apparence par fonction pure `resolve` + contrôles WinUI 3 + cas dorés xUnit + application de démo + site de documentation) : button / icon / tag / cell / text / input / textarea / checkbox / radio / switch / picker / dialog / toast / loading / navbar
