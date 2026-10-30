# Inicio rápido

## Ejemplo mínimo

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui;
using Top.Lhgb.Ui.Common;

public sealed class DemoPage : Page
{
    public DemoPage()
    {
        // Inyecta la paleta clara; para cambiar a oscura, pasa true. Todos los componentes Lh
        // la siguen automáticamente (repintado en caliente mediante LhTheme.ThemeChanged)
        LhTheme.Apply(false);

        var button = new LhButton
        {
            Type = LhButtonType.Primary,
            Text = "Botón principal",
        };
        button.Click += OnButtonClick;

        Content = new StackPanel { Children = { button } };
    }

    private void OnButtonClick() => { /* procesar la pulsación */ }
}
```

`LhTheme.Apply(isDark)` cambia la paleta estática y dispara `ThemeChanged`; todos los componentes Lh ya cargados se repintan automáticamente; si no se llama, se retrocede a la paleta clara.

## Tipos y tamaños

```csharp
// Seis tipos
new LhButton { Text = "Predeterminado" };                                     // Default
new LhButton { Type = LhButtonType.Primary,   Text = "Principal" };
new LhButton { Type = LhButtonType.Success,   Text = "Éxito" };
new LhButton { Type = LhButtonType.Warning,   Text = "Advertencia" };
new LhButton { Type = LhButtonType.Danger,    Text = "Peligro" };
new LhButton { Type = LhButtonType.Info,      Text = "Información" };

// Tres tamaños
new LhButton { Size = LhButtonSize.Small,  Text = "Pequeño" };
new LhButton { Size = LhButtonSize.Medium, Text = "Mediano" };   // predeterminado
new LhButton { Size = LhButtonSize.Large,  Text = "Grande" };
```

## Estados

```csharp
// Deshabilitado: no responde a la pulsación (lo bloquea la barrera Tapped) y presenta el estilo deshabilitado
new LhButton { Disabled = true, Text = "Deshabilitado" };

// Cargando: muestra un ProgressRing, no dispara Click
new LhButton { Loading = true, Text = "Cargando" };

// Ambos a la vez: la deshabilitación tiene prioridad (caso dorado disabled-precedes-loading)
new LhButton { Disabled = true, Loading = true, Text = "Deshabilitado prioritario" };
```

## Icono y fondo de patrón

```csharp
// El nombre del icono se resuelve contra el sistema de iconos local (LhButtonTokens.IconFor, admite
// actualmente search / refresh / check / close; los nombres no registrados no se dibujan)
new LhButton { Type = LhButtonType.Primary, Icon = "search", Text = "Buscar" };

// Fondo de patrón chino (consume el token color.decoration)
new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = "Fondo de patrón" };
```

## Temas claro y oscuro

```csharp
LhTheme.Apply(true);  // cambia globalmente a oscuro; todos los componentes lo siguen automáticamente en la próxima resolución del aspecto
```

## Componentes de formulario y de retroalimentación

```csharp
// Entrada controlada: el host escucha ValueChange y reescribe Value
var input = new LhInput { Placeholder = "Introduce una palabra clave" };
input.ValueChange += value => input.Value = value;

// Casilla de verificación
var checkbox = new LhCheckbox { Label = "青花瓷 (porcelana azul y blanco)" };
checkbox.Change += isChecked => { };

// Interruptor
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };

// Aviso breve imperativo: primero se coloca el control host y luego se acciona mediante el controlador
var toast = new LhToast();
toast.Close += () => { };
var panel = new StackPanel { Children = { toast } };
toast.Show("Guardado");   // al expirar, se oculta automáticamente y se dispara Close
```

## Vista previa de los ejemplos de componentes

La UI nativa no se puede renderizar en tiempo real en el navegador, por lo que este sitio muestra el aspecto mediante un **simulador de marco de teléfono en HTML estático en línea**:

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>lh-ui-windows</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">Botón predeterminado</button>
      <button class="sim-btn sim-btn--primary">Botón principal</button>
      <button class="sim-btn sim-btn--success">Éxito</button>
      <button class="sim-btn sim-btn--warning">Advertencia</button>
      <button class="sim-btn sim-btn--danger">Peligro</button>
      <button class="sim-btn sim-btn--info">Información</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">Pequeño</button>
      <button class="sim-btn sim-btn--primary sim-btn--large">Grande</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Deshabilitado</button>
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>Cargando
      </button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>Buscar</button>
      <button class="sim-btn sim-btn--primary sim-btn--pattern">Fondo de patrón</button>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo; los valores de estilo provienen todos de la instantánea de tokens de core y comparten origen y valores con el dispositivo. El aspecto en el dispositivo real es el que muestran la app de demostración y las capturas de pantalla del dispositivo.</p>

> El modo oscuro del simulador está sincronizado con el sitio: cambia el tema en la esquina superior derecha y los colores dentro del marco del teléfono cambian (los tokens oscuros están precalculados por core).
