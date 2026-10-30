# Toast (notification)

Couche flottante en pilule couleur encre, qui gère elle-même sa visibilité et sa temporisation. Gère les types sémantiques, le masquage automatique et le positionnement.

> Source du contrat : `lh-ui-core/specs/toast.json` ·
> Cas dorés : `lh-ui-core/cases/toast.json` (6 cas, traduits en tests xUnit)

## Usage de base

L'interface impérative (show / hide / tick du spec) se traduit côté appareil par des méthodes + une temporisation interne : `Show(text)` rend l'élément visible et réinitialise la temporisation, `Hide()` le masque immédiatement sans déclencher `Close`. La temporisation est portée par un `DispatcherTimer` ; à son expiration, l'élément est masqué automatiquement et `Close` est déclenché.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast : base</span></div>
  <div class="sim-screen">
    <div class="sim-toast">Enregistré</div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var controller = new LhToastController();
var toast = new LhToast
{
    Duration = 2000,
    Position = LhToastPosition.Center,
    Controller = controller,
};
toast.Close += () => { /* déclenché après le masquage automatique à l'expiration de la temporisation */ };

// l'hôte doit placer le toast en dernier enfant du Grid de la page (ordre d'empilement zIndex.toast)
controller.Show("Enregistré");
```

## Types sémantiques

Lorsque `Type` est différent de default, un point de la couleur sémantique correspondante est dessiné devant le texte (son diamètre vaut la taille de police).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Types de Toast</span></div>
  <div class="sim-screen">
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--success"></span>Enregistrement réussi</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--warning"></span>Vérifiez la connexion réseau</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--danger"></span>Échec de la suppression</div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## Positionnement

`Position` détermine le positionnement dans la zone disponible ; une marge `spacing.xl` est laissée aux extrémités haute et basse (aucune marge en position center).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Positionnement de Toast</span></div>
  <div class="sim-screen">
    <div class="sim-toast sim-toast--top">Indication en haut</div>
    <div class="sim-toast sim-toast--bottom">Indication en bas</div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `Close` | Déclenché lors du masquage automatique à l'expiration de la temporisation ; non déclenché par un masquage volontaire via Hide() | `event Action` | — |
| `Type` | Type sémantique ; lorsque différent de default, un point de la couleur sémantique correspondante est dessiné devant le texte | `LhToastType` | `Default` |
| `Duration` | Délai avant masquage automatique (en millisecondes) | `int` | `2000` |
| `Position` | Positionnement dans la zone disponible | `LhToastPosition` | `Center` |
| `Controller` | Handle appelé par l'hôte (optionnel ; enregistre Show/Hide après Loaded) | `LhToastController?` | `null` |

### Méthodes

| Méthode | Description |
| --- | --- |
| `Show(string text)` | Affiche le texte de l'indication et réinitialise la temporisation de masquage automatique |
| `Hide()` | Masque immédiatement sans déclencher Close |

### LhToastType

| Valeur | Couleur du point |
| --- | --- |
| `Default` | Aucun point dessiné |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

### LhToastPosition

| Valeur | Positionnement |
| --- | --- |
| `Top` | En haut (marge `spacing.xl` à l'extrémité supérieure) |
| `Center` | Au centre (sans marge) |
| `Bottom` | En bas (marge `spacing.xl` à l'extrémité inférieure) |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `toast.fontSize` | Taille de police du texte (correspond également au diamètre du point coloré) |
| `toast.paddingHorizontal` / `paddingVertical` | Marges internes de la pilule |
| `toast.borderRadius` | Arrondi de la pilule |
| `toast.gap` | Espacement entre le point coloré et le texte |
| `color.ink` | Couleur de fond de la pilule |
| `color.onPrimary` | Couleur du texte |
| `color.{success,warning,danger,info}.default` | Points sémantiques |
| `zIndex.toast` | Ordre d'empilement de la couche flottante |
| `spacing.xl` | Marges aux extrémités haute et basse |
| `typography.fontFamily` | Pile de polices avec empattements style chinois |

## Référence des cas dorés

Correspondance entre les 6 assertions de `cases/toast.json` et les tests xUnit (`tests/LhToastTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `show-makes-visible` | Après show, l'élément est visible et rend le texte | `Show_Makes_Visible` |
| `auto-hides-after-duration` | À l'expiration de la temporisation, masquage automatique et déclenchement de onClose | `Auto_Hides_After_Duration` |
| `tick-before-duration-keeps-visible` | Avant l'expiration de la temporisation, l'élément reste visible | `Tick_Before_Duration_Keeps_Visible` |
| `hide-is-silent` | Un masquage volontaire via hide ne déclenche pas onClose | `Hide_Is_Silent` |
| `success-type-uses-success-token` | Le type success consomme les jetons sémantiques et le jeton couleur encre | `Success_Type_Uses_Success_Token` |
| `hidden-emits-no-commands` | Lorsque invisible, aucune commande de dessin n'est émise | `Hidden_Emits_No_Commands` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
