# Toast (notificación breve)

Capa flotante en forma de píldora color tinta, con visibilidad y temporización autogestionadas. Admite tipos semánticos, ocultación automática y posicionamiento.

> Fuente del contrato: `lh-ui-core/specs/toast.json` ·
> Casos dorados: `lh-ui-core/cases/toast.json` (6 casos, traducidos a pruebas xUnit)

## Uso básico

La interfaz imperativa (show / hide / tick del spec) se implementa en este lado como métodos con temporización interna:
`Show(text)` lo hace visible y reinicia el temporizador; `Hide()` lo oculta de inmediato pero no dispara `Close`.
La temporización la gestiona un `DispatcherTimer`: al expirar, se oculta automáticamente y se dispara `Close`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast básico</span></div>
  <div class="sim-screen">
    <div class="sim-toast">Guardado</div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var controller = new LhToastController();
var toast = new LhToast
{
    Duration = 2000,
    Position = LhToastPosition.Center,
    Controller = controller,
};
toast.Close += () => { /* se dispara tras la ocultación automática por expiración del temporizador */ };

// el host debe colocar el toast como último nodo hijo del Grid de la página (nivel zIndex.toast)
controller.Show("Guardado");
```

## Tipos semánticos

Cuando `Type` no es default, se dibuja un punto del color semántico correspondiente delante del texto (el diámetro toma el tamaño de fuente).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tipos de Toast</span></div>
  <div class="sim-screen">
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--success"></span>Guardado con éxito</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--warning"></span>Comprueba la conexión de red</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--danger"></span>Error al eliminar</div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Posicionamiento

`Position` determina la posición dentro del área disponible; en los extremos superior e inferior se deja un margen `spacing.xl` (con center no hay margen).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Posición de Toast</span></div>
  <div class="sim-screen">
    <div class="sim-toast sim-toast--top">Aviso superior</div>
    <div class="sim-toast sim-toast--bottom">Aviso inferior</div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `Close` | Se dispara al ocultarse automáticamente por expiración del temporizador; la ocultación manual con Hide() no lo dispara | `event Action` | — |
| `Type` | Tipo semántico; cuando no es default, se dibuja un punto del color semántico correspondiente delante del texto | `LhToastType` | `Default` |
| `Duration` | Tiempo hasta la ocultación automática (milisegundos) | `int` | `2000` |
| `Position` | Posición dentro del área disponible | `LhToastPosition` | `Center` |
| `Controller` | Identificador de llamada del host (opcional; registra Show/Hide tras Loaded) | `LhToastController?` | `null` |

### Métodos

| Método | Descripción |
| --- | --- |
| `Show(string text)` | Muestra el texto del aviso y reinicia el temporizador de ocultación automática |
| `Hide()` | Oculta de inmediato pero no dispara Close |

### LhToastType

| Valor | Color del punto |
| --- | --- |
| `Default` | No se dibuja ningún punto |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

### LhToastPosition

| Valor | Posición |
| --- | --- |
| `Top` | Parte superior (margen `spacing.xl` en el extremo superior) |
| `Center` | Centrado (sin margen) |
| `Bottom` | Parte inferior (margen `spacing.xl` en el extremo inferior) |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `toast.fontSize` | Tamaño de fuente del texto (también es el diámetro del punto de color) |
| `toast.paddingHorizontal` / `paddingVertical` | Relleno interno de la píldora |
| `toast.borderRadius` | Radio de esquina de la píldora |
| `toast.gap` | Espacio entre el punto de color y el texto |
| `color.ink` | Color de fondo de la píldora |
| `color.onPrimary` | Color del texto |
| `color.{success,warning,danger,info}.default` | Puntos semánticos de color |
| `zIndex.toast` | Nivel de la capa flotante |
| `spacing.xl` | Margen en los extremos superior e inferior |
| `typography.fontFamily` | Pila de fuentes serif de estilo chino |

## Referencia de casos dorados

Cómo se mapean las 6 aserciones de `cases/toast.json` a las pruebas xUnit (`tests/LhToastTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `show-makes-visible` | Tras show es visible y renderiza el texto | `Show_Makes_Visible` |
| `auto-hides-after-duration` | Al expirar el temporizador, se oculta automáticamente y dispara onClose | `Auto_Hides_After_Duration` |
| `tick-before-duration-keeps-visible` | Antes de expirar el temporizador, permanece visible | `Tick_Before_Duration_Keeps_Visible` |
| `hide-is-silent` | La ocultación manual con hide no dispara onClose | `Hide_Is_Silent` |
| `success-type-uses-success-token` | El tipo success consume los tokens semánticos y el color tinta | `Success_Type_Uses_Success_Token` |
| `hidden-emits-no-commands` | Cuando no es visible, no emite ninguna instrucción de dibujo | `Hidden_Emits_No_Commands` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
