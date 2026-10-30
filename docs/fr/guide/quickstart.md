# Démarrage rapide

## Exemple minimal

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui;
using Top.Lhgb.Ui.Common;

public sealed class DemoPage : Page
{
    public DemoPage()
    {
        // injecte la palette claire ; passez true pour le mode sombre. Tous les composants Lh suivent automatiquement (redessin à chaud via LhTheme.ThemeChanged)
        LhTheme.Apply(false);

        var button = new LhButton
        {
            Type = LhButtonType.Primary,
            Text = "Bouton principal",
        };
        button.Click += OnButtonClick;

        Content = new StackPanel { Children = { button } };
    }

    private void OnButtonClick() => { /* traiter le clic */ }
}
```

`LhTheme.Apply(isDark)` bascule la palette statique et déclenche `ThemeChanged` : tous les composants Lh déjà chargés sont automatiquement redessinés ; sans appel, la palette claire est utilisée par défaut.

## Types et dimensions

```csharp
// Six types
new LhButton { Text = "Par défaut" };                                     // Default
new LhButton { Type = LhButtonType.Primary,   Text = "Principal" };
new LhButton { Type = LhButtonType.Success,   Text = "Succès" };
new LhButton { Type = LhButtonType.Warning,   Text = "Avertissement" };
new LhButton { Type = LhButtonType.Danger,    Text = "Danger" };
new LhButton { Type = LhButtonType.Info,      Text = "Information" };

// Trois dimensions
new LhButton { Size = LhButtonSize.Small,  Text = "Petit" };
new LhButton { Size = LhButtonSize.Medium, Text = "Moyen" };   // par défaut
new LhButton { Size = LhButtonSize.Large,  Text = "Grand" };
```

## États

```csharp
// Désactivé : ne réagit pas au clic (intercepté par le gate Tapped) et revêt l'apparence désactivée
new LhButton { Disabled = true, Text = "Désactivé" };

// En chargement : affiche un ProgressRing, ne déclenche pas Click
new LhButton { Loading = true, Text = "Chargement" };

// Cumul des deux : le désactivé prime (cas doré disabled-precedes-loading)
new LhButton { Disabled = true, Loading = true, Text = "Désactivé prioritaire" };
```

## Icône et motif décoratif

```csharp
// Le nom de l'icône est résolu par le système d'icônes local (LhButtonTokens.IconFor, actuellement search / refresh / check / close,
// les noms non enregistrés ne sont pas dessinés)
new LhButton { Type = LhButtonType.Primary, Icon = "search", Text = "Rechercher" };

// Motif décoratif style chinois (consomme le jeton color.decoration)
new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = "Fond à motif" };
```

## Thèmes clair et sombre

```csharp
LhTheme.Apply(true);  // bascule globalement en mode sombre ; tous les composants suivent à la prochaine analyse d'apparence
```

## Composants de formulaire et de retour

```csharp
// Saisie contrôlée : l'hôte écoute ValueChange et réécrit Value
var input = new LhInput { Placeholder = "Saisissez un mot-clé" };
input.ValueChange += value => input.Value = value;

// Case à cocher
var checkbox = new LhCheckbox { Label = "青花瓷 (porcelaine bleue et blanche)" };
checkbox.Change += isChecked => { };

// Interrupteur
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };

// Notification impérative : placez d'abord le contrôle hôte, puis pilotez-le avec le contrôleur
var toast = new LhToast();
toast.Close += () => { };
var panel = new StackPanel { Children = { toast } };
toast.Show("Enregistré");   // masquée automatiquement à l'expiration et déclenche Close
```

## Aperçu des exemples de composants

L'UI native ne pouvant être rendue en temps réel dans le navigateur, ce site présente son apparence à l'aide d'un **simulateur en cadre de téléphone HTML statique en ligne** :

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>lh-ui-windows</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">Bouton par défaut</button>
      <button class="sim-btn sim-btn--primary">Bouton principal</button>
      <button class="sim-btn sim-btn--success">Succès</button>
      <button class="sim-btn sim-btn--warning">Avertissement</button>
      <button class="sim-btn sim-btn--danger">Danger</button>
      <button class="sim-btn sim-btn--info">Information</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">Petit</button>
      <button class="sim-btn sim-btn--primary sim-btn--large">Grand</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Désactivé</button>
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>Chargement
      </button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>Rechercher</button>
      <button class="sim-btn sim-btn--primary sim-btn--pattern">Fond à motif</button>
    </div>
  </div>
</div>
<p class="sim-note">Aperçu HTML simulé (apparence de Windows / WinUI 3), pas un rendu sur appareil réel ; les valeurs de style proviennent toutes de l'instantané des jetons core, mêmes sources et mêmes valeurs que sur appareil réel. L'apparence sur appareil réel fait foi, à voir dans l'application de démonstration et les captures d'écran.</p>

> Le mode sombre du simulateur est lié au site : basculez le thème en haut à droite et les couleurs dans le cadre du téléphone changent (les jetons sombres sont précalculés par core).
