# Liaison locale

## Disposition de la famille

```
lh-ui/                        # répertoire parent de la famille (pas un dépôt)
├── lh-ui-core/               # source de vérité des jetons + codegen
├── lh-ui-android/            # dépôts frères
├── lh-ui-ios/
├── lh-ui-harmony/
└── lh-ui-windows/            # ce dépôt
```

Lorsque les dépôts partagent un même répertoire parent, les jetons sont **directement reliés localement** ; lorsqu'un dépôt frère est absent (clone mono-dépôt en CI), **retour automatique au paquet npm**.
Ce mécanisme est isomorphe au garde-fou alias vite de la lignée web (plan directeur D18).

## Garde-fou à trois niveaux

`scripts/link-tokens.mjs` recherche les artefacts de jetons par ordre de priorité :

| Priorité | Source | Description |
| --- | --- | --- |
| 1 | `../lh-ui-core/dist/windows` | Liaison locale directe : modifier un jeton prend effet immédiatement |
| 2 | `node_modules/@lh-ui/core/dist/windows` | Retour npm : les `files:["dist"]` de `@lh-ui/core` contiennent les artefacts de chaque plateforme |
| 3 | — | Erreur et arrêt : sans jeton, impossible de construire |

Les jetons atterrissent dans `src/Tokens/` (le `relativePath` du codegen est `Tokens/*.cs`) ; côté composant, `using Top.Lhgb.Ui.Tokens;` pointe vers ce répertoire. Il s'agit d'un produit généré (`.gitignore`), toute modification manuelle est interdite.

## Flux quotidien

```bash
# 1. Modifier les jetons (dans le dépôt core)
cd ../lh-ui-core
vim tokens/color.json           # ou spacing / typography / ...

# 2. Régénérer + vérifier
pnpm codegen                    # génère les artefacts dist/ de chaque plateforme
pnpm verify                     # schema + références + contraste WCAG + cohérence des contrats

# 3. Revenir dans ce dépôt pour synchroniser
cd ../lh-ui-windows
pnpm link:tokens                # écrase src/Tokens/

# 4. Régression (dotnet)
dotnet test                     # cas dorés xUnit
dotnet build                    # la compilation constitue la vérification des types
```

## Jetons du site de documentation

Les variables CSS du simulateur du site de documentation sont également générées à partir des jetons :

```bash
pnpm gen:sim                    # génère docs/.vitepress/theme/sim-tokens.css
pnpm docs:dev                   # lance gen:sim en interne au préalable, port 5186
pnpm sim:check                  # vérifie l'absence de littéraux de jetons dans le CSS sim (ligne rouge)
```

## Gate CI

```bash
pnpm link:tokens:check          # détecte si les jetons ont bien été synchronisés (comparaison du nombre de fichiers, code de sortie 1 si non synchronisé)
```

## Interdictions

- ❌ Modifier manuellement `src/Tokens/` (produit généré, gitignore)
- ❌ Committer les fichiers de jetons générés dans le dépôt
- ❌ Écrire des littéraux de couleur / d'espacement dans les composants (ex. `Color.FromArgb(0xFF,0x2B,0x4C,0x7C)`, `new GridLength(40)`, une taille de police en chiffres)
- ❌ Contourner `link:tokens` en copiant manuellement les artefacts

## Diagnostic

| Symptôme | Cause | Solution |
| --- | --- | --- |
| La compilation signale `The type or namespace 'LhTokens' does not exist` | Jetons non synchronisés | `pnpm link:tokens` |
| Les valeurs des jetons ne correspondent pas aux attentes | Le codegen du dépôt core n'a pas été relancé | Relancer `pnpm codegen` dans core, puis linkage |
| `link:tokens` signale « artefacts de jetons introuvables » | Dépôt frère absent et paquet npm non installé | Vérifier la disposition de la famille ou `pnpm i -D @lh-ui/core` |
| Les couleurs sombres sont incorrectes | La couleur principale claire a été modifiée sans mettre à jour le précalcul sombre | Mettre à jour `color-dark.json` et relancer (R-n6) |
| Toutes les couleurs des composants sont claires | `LhTheme.Apply` n'est pas appelé à l'entrée | Appeler une fois au démarrage de l'App / de la fenêtre principale |
| `dotnet build` signale des erreurs XAML telles que `MC1000` | Les composants construisent tous leur arbre visuel en code, aucun XAML dans le dépôt | Vérifier qu'un fichier `.xaml` n'a pas été ajouté par erreur |
