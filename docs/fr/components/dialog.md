# Boîte de dialogue

Boîte de dialogue modale. Calque de surdimensionnement + carte de contenu centrée + boutons de confirmation / annulation ; gère les types sémantiques et la fermeture par le calque.

> Source du contrat : `lh-ui-core/specs/dialog.json` ·
> Cas dorés : `lh-ui-core/cases/dialog.json` (8 cas, traduits en tests xUnit)

## Usage de base

Visibilité déclarative : lorsque `Visible` vaut false, rien n'est rendu ; après confirmation ou annulation, `Close` est déclenché et l'hôte passe alors `Visible` à false.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog : base</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">Confirmation de suppression</div>
      <div class="sim-dialog__content">La suppression est irréversible. Confirmez-vous la suppression de cet enregistrement ?</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">Annuler</button>
        <button class="sim-btn sim-btn--danger">Confirmer</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var dialog = new LhDialog
{
    Title = "Confirmation de suppression",
    Content = "La suppression est irréversible. Confirmez-vous la suppression de cet enregistrement ?",
    Type = LhDialogType.Danger,
    ConfirmText = "Confirmer",
    CancelText = "Annuler",
};
dialog.Confirm += () => { /* exécuter la suppression */ };
dialog.Close += () => dialog.Visible = false;

dialog.Visible = true;
```

## Types sémantiques

`Type` détermine le type (la couleur) du bouton de confirmation ; le bouton d'annulation est toujours de type default.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Types de Dialog</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">Information</div>
      <div class="sim-dialog__content">Ceci est une boîte de dialogue de confirmation en couleur principale</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">Annuler</button>
        <button class="sim-btn sim-btn--primary">Confirmer</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `Confirm` | Déclenché lors de la confirmation | `event Action` | — |
| `Cancel` | Déclenché lors de l'annulation | `event Action` | — |
| `Close` | Déclenché après confirmation / annulation ou (si closable) lors d'un clic sur le calque, l'hôte passe alors Visible à false | `event Action` | — |
| `Visible` | Affichée ou non ; lorsque false, rien n'est rendu | `bool` | `false` |
| `Title` | Titre | `string` | `""` |
| `Content` | Contenu du corps | `string` | `""` |
| `Type` | Type sémantique, détermine le type (la couleur) du bouton de confirmation | `LhDialogType` | `Default` |
| `ConfirmText` | Texte du bouton de confirmation ; si vide, le bouton de confirmation n'est pas affiché | `string` | `"确认"` |
| `CancelText` | Texte du bouton d'annulation ; si vide, le bouton d'annulation n'est pas affiché | `string` | `"取消"` |
| `Closable` | Un clic sur la zone du calque déclenche ou non Close | `bool` | `false` |

### LhDialogType

| Valeur | Type du bouton de confirmation |
| --- | --- |
| `Default` | `LhButtonType.Default` |
| `Primary` | `LhButtonType.Primary` |
| `Success` | `LhButtonType.Success` |
| `Warning` | `LhButtonType.Warning` |
| `Danger` | `LhButtonType.Danger` |
| `Info` | `LhButtonType.Info` |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `dialog.width` / `dialog.borderRadius` | Largeur et arrondi de la carte de contenu |
| `dialog.paddingHorizontal` / `paddingVertical` | Marges internes de la carte de contenu |
| `dialog.titleFontSize` / `contentFontSize` | Tailles de police du titre et du corps |
| `dialog.gap` | Espacement entre les zones titre / contenu / boutons |
| `color.background.default` | Couleur de fond de la carte de contenu |
| `color.background.overlay` | Couleur du calque de surdimensionnement |
| `color.text.primary` / `color.text.regular` | Couleurs du titre / du corps |
| `zIndex.modal` / `zIndex.modalBackdrop` | Ordre d'empilement de la carte / du calque |
| `shadow.base` | Ombre portée de la carte de contenu |

## Référence des cas dorés

Correspondance entre les 8 assertions de `cases/dialog.json` et les tests xUnit (`tests/LhDialogTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `confirm-emits-onconfirm` | La confirmation déclenche onConfirm | `Confirm_Emits_OnConfirm` |
| `cancel-emits-oncancel` | L'annulation déclenche onCancel | `Cancel_Emits_OnCancel` |
| `confirm-also-emits-onclose` | La confirmation déclenche également onClose | `Confirm_Also_Emits_OnClose` |
| `closable-overlay-emits-onclose` | Si closable, un clic sur le calque déclenche onClose | `Closable_Overlay_Emits_OnClose` |
| `not-closable-blocks-overlay-close` | Si non closable, le calque ne ferme pas la boîte | `Not_Closable_Blocks_Overlay_Close` |
| `hidden-not-rendered` | Lorsque invisible, rien n'est rendu | `Hidden_Not_Rendered` |
| `title-renders-text` | Le titre et le corps sont rendus selon les jetons de taille de police | `Title_Renders_Text` |
| `danger-type-uses-danger-token` | Le type danger consomme le jeton de couleur danger | `Danger_Type_Uses_Danger_Token` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
