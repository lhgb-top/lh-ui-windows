# Sélecteur

Roue à colonne unique surgissant du bas. Gère les options, le clamp d'index et la barre d'outils.

> Source du contrat : `lh-ui-core/specs/picker.json` ·
> Cas dorés : `lh-ui-core/cases/picker.json` (8 cas, traduits en tests xUnit)

## Usage de base

Visibilité déclarative : lorsque `Visible` vaut false, rien n'est rendu ; après confirmation ou annulation, `Close` est déclenché et l'hôte passe alors `Visible` à false.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Picker : base</span></div>
  <div class="sim-screen">
    <div class="sim-picker-overlay"></div>
    <div class="sim-picker">
      <div class="sim-picker__toolbar">
        <span class="sim-picker__cancel">Annuler</span>
        <span class="sim-picker__title">Sélectionner</span>
        <span class="sim-picker__confirm">Confirmer</span>
      </div>
      <div class="sim-picker__wheel">
        <span class="sim-picker__item">青花瓷 (porcelaine bleue et blanche)</span>
        <span class="sim-picker__item sim-picker__item--active">粉彩瓷 (famille rose)</span>
        <span class="sim-picker__item">玲珑瓷 (porcelaine linglong)</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var picker = new LhPicker
{
    Options = new[] { "青花瓷", "粉彩瓷", "玲珑瓷" },
    Value = 1,
    Title = "Sélectionner",
};
picker.ValueChange += index => { /* index sélectionné après l'arrêt de la roue */ };
picker.Confirm += index => { /* index actuellement sélectionné */ };
picker.Close += () => picker.Visible = false;

picker.Visible = true;
```

## Clamp d'index

Lorsque `Value` est hors limites, il est ramené (clamp) dans la plage des options : les valeurs négatives prennent 0 ; les valeurs supérieures au nombre d'options - 1 prennent le dernier élément.

```csharp
Assert.Equal(0, LhPickerTokens.ClampIndex(-1, 3));   // une valeur négative prend 0
Assert.Equal(2, LhPickerTokens.ClampIndex(100, 3));  // hors limites : prend le dernier élément
```

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `ValueChange` | Déclenché lorsque la roue s'arrête et que l'option sélectionnée change | `event Action<int>` | — |
| `Confirm` | Déclenché lors du clic sur le bouton de confirmation, porte l'index sélectionné | `event Action<int>` | — |
| `Cancel` | Déclenché lors du clic sur le bouton d'annulation | `event Action` | — |
| `Close` | Déclenché après confirmation / annulation, l'hôte passe alors Visible à false | `event Action` | — |
| `Visible` | Affiché ou non ; lorsque false, rien n'est rendu | `bool` | `false` |
| `Options` | Tableau des textes des options (colonne unique) | `string[]?` | `null` |
| `Value` | Index de l'option sélectionnée (à partir de 0) ; ramené dans la plage des options hors limites | `int` | `0` |
| `Title` | Titre au milieu de la barre d'outils | `string` | `""` |
| `ConfirmText` | Texte du bouton de confirmation | `string` | `"确认"` |
| `CancelText` | Texte du bouton d'annulation | `string` | `"取消"` |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `picker.height` | Hauteur du panneau |
| `picker.toolbarHeight` | Hauteur de la barre d'outils supérieure (la hauteur de la zone de la roue = hauteur du panneau - hauteur de la barre d'outils) |
| `picker.itemHeight` | Hauteur d'une option (l'espacement de l'indicateur central est identique) |
| `picker.itemFontSize` | Taille de police des options et de la barre d'outils |
| `picker.borderRadius` | Arrondi supérieur du panneau |
| `color.background.default` | Couleur de fond du panneau |
| `color.background.overlay` | Couleur du calque de surdimensionnement |
| `color.text.primary` | Couleur du titre et des options |
| `color.text.secondary` | Couleur du bouton d'annulation |
| `color.primary.default` | Couleur du bouton de confirmation |
| `color.border.lighter` | Couleur des lignes de l'indicateur central |
| `zIndex.modal` / `zIndex.modalBackdrop` | Ordre d'empilement du panneau / du calque |

## Référence des cas dorés

Correspondance entre les 8 assertions de `cases/picker.json` et les tests xUnit (`tests/LhPickerTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `confirm-emits-onconfirm-with-value` | La confirmation porte l'index actuellement sélectionné | `Confirm_Emits_OnConfirm_With_Value` |
| `confirm-also-emits-onclose` | La confirmation déclenche également onClose | `Confirm_Also_Emits_OnClose` |
| `cancel-emits-oncancel` | L'annulation déclenche onCancel | `Cancel_Emits_OnCancel` |
| `scroll-emits-onvaluechange` | L'arrêt de la roue déclenche onValueChange | `Scroll_Emits_OnValueChange` |
| `clamp-negative-value-to-zero` | Un index négatif est ramené à 0 | `Clamp_Negative_Value_To_Zero` |
| `clamp-overflow-value-to-last` | Un index hors limites est ramené au dernier élément | `Clamp_Overflow_Value_To_Last` |
| `title-renders-text` | Le titre est rendu selon le jeton de taille de police | `Title_Renders_Text` |
| `hidden-not-rendered` | Lorsque invisible, rien n'est rendu | `Hidden_Not_Rendered` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
