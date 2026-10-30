# Installation

## Prérequis

- SDK .NET 8 (framework cible `net8.0-windows10.0.19041.0`)
- Windows App SDK 1.6+ (Windows 10 1809+ ou Windows 11)
- Chaîne d'outils de compilation : Node.js >= 20 + pnpm (uniquement pour la liaison des jetons et le site de documentation ; **n'entre pas dans l'exécution**)

## Intégration via NuGet (une fois la chaîne de publication nM6 ouverte)

```bash
dotnet add package LhUi.Windows
```

```xml
<ItemGroup>
  <PackageReference Include="LhUi.Windows" Version="0.0.1" />
</ItemGroup>
```

## Intégration depuis les sources (étape actuelle)

Avec la disposition de la famille, on référence directement la bibliothèque de classe de ce dépôt (la solution contient la bibliothèque `src/LhUi.csproj` + les tests `tests/` + l'application de démonstration `demo/`) :

```
lh-ui-windows/
├── src/                  # bibliothèque LhUi (Components/ + Common/ + Tokens/)
├── tests/                # cas dorés xUnit
├── demo/                 # application de démonstration (toutes les formes + bascule clair/sombre)
└─ docs/                  # site de documentation VitePress
```

## Synchroniser les jetons

Les jetons sont des produits générés : ils doivent être synchronisés depuis `lh-ui-core` (source unique, toute modification manuelle est interdite) :

```bash
cd ../lh-ui-core && pnpm codegen   # génère les artefacts de jetons dist/windows
cd ../lh-ui-windows
pnpm install                        # installe la chaîne d'outils de compilation
pnpm link:tokens                    # synchronise les jetons vers src/Tokens/
```

`pnpm link:tokens` comporte un garde-fou d'existence à trois niveaux : `../lh-ui-core/dist/windows` local → produit npm `@lh-ui/core` → erreur.

## Vérifier l'installation

```bash
dotnet build     # compile LhUi / LhUi.Tests / LhUi.Demo (inclut la vérification des types C#)
dotnet test      # cas dorés xUnit (15 fichiers, assertent l'analyse d'apparence par la fonction pure Resolve)
```
