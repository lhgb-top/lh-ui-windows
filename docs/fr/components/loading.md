# Chargement

Indicateur de chargement rotatif en anneau. Gère la dimension et la tonalité sémantique.

> Source du contrat : `lh-ui-core/specs/loading.json` ·
> Cas dorés : `lh-ui-core/cases/loading.json` (5 cas, traduits en tests xUnit)

::: tip Notes de plateforme
La rotation est assurée par l'animation de la plateforme `ProgressRing` de WinUI (tonalité épurée : seules les valeurs géométriques sont émises, l'animation est complétée par la plateforme) ; la couche de jetons ne détermine que le diamètre, la largeur du trait et la couleur de l'anneau.
:::

## Dimensions

Trois dimensions mappées sur les jetons de diamètre `loading.size.*`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dimensions de Loading</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--small"></span>
      <span class="sim-loading"></span>
      <span class="sim-loading sim-loading--large"></span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var loading = new LhLoading
{
    Size = LhLoadingSize.Large,
    Tone = LhLoadingTone.Primary,
};
```

## Tonalité

`Tone` détermine la couleur de l'anneau et correspond un à un aux jetons de couleurs sémantiques.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tonalité de Loading</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--primary"></span>
      <span class="sim-loading sim-loading--success"></span>
      <span class="sim-loading sim-loading--warning"></span>
    </div>
    <div class="sim-row">
      <span class="sim-loading sim-loading--danger"></span>
      <span class="sim-loading sim-loading--info"></span>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `Size` | Diamètre de l'indicateur, mappé sur les jetons `loading.size.*` | `LhLoadingSize` | `Medium` |
| `Tone` | Couleur de l'anneau, utilise le jeton de couleur sémantique correspondant | `LhLoadingTone` | `Primary` |

### LhLoadingSize

| Valeur | Jeton de diamètre |
| --- | --- |
| `Small` | `loading.size.small` (20epx) |
| `Medium` | `loading.size.medium` (28epx) |
| `Large` | `loading.size.large` (36epx) |

### LhLoadingTone

| Valeur | Jeton de couleur de l'anneau |
| --- | --- |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `loading.size.{small,medium,large}` | Trois diamètres |
| `loading.strokeWidth` | Largeur du trait de l'anneau de chargement |
| `color.{primary,success,warning,danger,info}.default` | Cinq couleurs d'anneau |

## Référence des cas dorés

Correspondance entre les 5 assertions de `cases/loading.json` et les tests xUnit (`tests/LhLoadingTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `medium-size-uses-medium-token` | La taille moyenne consomme le jeton de diamètre medium | `Medium_Size_Uses_Medium_Token` |
| `large-size-uses-large-token` | La grande taille consomme le jeton de diamètre large | `Large_Size_Uses_Large_Token` |
| `primary-tone-uses-primary-token` | La tonalité principale utilise le jeton de la couleur principale | `Primary_Tone_Uses_Primary_Token` |
| `success-tone-uses-success-token` | La tonalité succès utilise le jeton de la couleur succès | `Success_Tone_Uses_Success_Token` |
| `render-emits-ring-command` | Il y a une et une seule commande RING | `Render_Emits_Ring_Command` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
