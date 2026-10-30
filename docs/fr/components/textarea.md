# Zone de texte

Saisie de texte multiligne. Offre un nombre initial de lignes visibles selon `Rows`, grandit automatiquement lorsque le contenu déborde et gère le comptage de caractères.

> Source du contrat : `lh-ui-core/specs/textarea.json` ·
> Cas dorés : `lh-ui-core/cases/textarea.json` (6 cas, traduits en tests xUnit)

## Usage de base

Composant contrôlé : l'hôte écoute `ValueChange` et réécrit la nouvelle valeur dans `Value`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea : base</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷 (porcelaine bleue et blanche)<br/>粉彩瓷 (famille rose)</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field sim-textarea__field--placeholder">Saisissez une description</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

```csharp
var textarea = new LhTextarea
{
    Rows = 3,
    Placeholder = "Saisissez une description",
};
textarea.ValueChange += value => { /* contrôlé : réécrire Value */ };
```

## Comptage des caractères

Lorsque `ShowCount` vaut true, un compteur de caractères est dessiné en bas à droite ; si `Maxlength` est déclaré, le format est « courant / maximal », sinon seule la longueur courante est affichée. Lorsque `Maxlength` est dépassé, les nouvelles saisies sont tronquées.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Comptage des caractères</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷</div>
        <div class="sim-textarea__count">3/10</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷</div>
        <div class="sim-textarea__count">3</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## États

`Disabled` et `Readonly` ne déclenchent pas `ValueChange`, mais seul `Disabled` revêt l'apparence désactivée ; lors de la prise de focus, la bordure utilise `color.focus` + `textarea.focusRingWidth`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>États de Textarea</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--disabled">
        <div class="sim-textarea__field">Désactivé</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--focus">
        <div class="sim-textarea__field">Bordure de focus</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel</p>

## API

### Props

| Prop | Description | Type | Valeur par défaut |
| --- | --- | --- | --- |
| `ValueChange` | Callback de changement de valeur (contrôlé) ; non déclenché en cas de disabled / readonly | `event Action<string>` | — |
| `Focus` | Déclenché lors de la prise de focus | `event Action` | — |
| `Blur` | Déclenché lors de la perte de focus | `event Action` | — |
| `Value` | Valeur textuelle courante (contrôlée) | `string` | `""` |
| `Placeholder` | Texte d'invite de l'espace réservé, utilise le jeton `color.text.placeholder` | `string` | `""` |
| `Rows` | Nombre initial de lignes visibles ; la hauteur croît avec le nombre de lignes et le contenu | `int` | `3` |
| `Disabled` | Désactivé ou non (ne réagit pas à la saisie, revêt l'apparence désactivée) | `bool` | `false` |
| `Readonly` | Lecture seule ou non (peut recevoir le focus mais n'accepte pas la saisie) | `bool` | `false` |
| `ShowCount` | Afficher ou non le compteur de caractères en bas à droite | `bool` | `false` |
| `Maxlength` | Nombre maximal de caractères ; les saisies dépassant la limite sont tronquées et sert de dénominateur pour showCount | `int?` | `null` |

## Référence des jetons

| Jeton | Utilisation |
| --- | --- |
| `textarea.paddingVertical` / `paddingHorizontal` | Marges internes |
| `textarea.fontSize` | Taille de police |
| `textarea.borderRadius` / `borderWidth` | Arrondi et largeur de la bordure hors focus |
| `textarea.focusRingWidth` / `color.focus` | Largeur et couleur de la bordure de focus |
| `textarea.countFontSize` / `color.text.secondary` | Taille de police / couleur du compteur de caractères |
| `textarea.minHeight` | Hauteur minimale (le résultat du calcul à partir des lignes ne lui est jamais inférieur) |
| `color.background.default` / `color.text.primary` | Fond du conteneur / couleur du texte |
| `color.text.placeholder` | Couleur du texte de l'espace réservé |
| `color.border.base` | Couleur de la bordure hors focus |
| `color.disabled.{bg,text}` | Fond / texte de l'état désactivé |
| `typography.lineHeight.primary` | Interligne (la hauteur est calculée à partir de rows) |
| `typography.fontFamily` | Pile de polices avec empattements style chinois |

## Référence des cas dorés

Correspondance entre les 6 assertions de `cases/textarea.json` et les tests xUnit (`tests/LhTextareaTests.cs`) :

| case id | Assertion | Test local |
| --- | --- | --- |
| `input-emits-onvaluechange` | La saisie déclenche onValueChange | `Input_Emits_OnValueChange` |
| `disabled-blocks-input` | L'état désactivé ne déclenche pas et revêt l'apparence désactivée | `Disabled_Blocks_Input` |
| `showcount-renders-count` | Le format « courant / maximal » utilise le jeton countFontSize | `ShowCount_Renders_Count` |
| `showcount-without-maxlength-renders-length-only` | Sans maxlength, seule la longueur est affichée | `ShowCount_Without_Maxlength_Renders_Length_Only` |
| `focus-uses-focus-token` | La bordure de focus utilise le jeton focus | `Focus_Uses_Focus_Token` |
| `maxlength-truncates-input` | Au-delà de maxlength, la saisie est tronquée | `Maxlength_Truncates_Input` |

Il existe en outre des tests spécifiques au mode sombre : `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Exécution : `cd lh-ui-windows && dotnet test`.
