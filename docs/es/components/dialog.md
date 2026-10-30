# Diálogo

Diálogo modal. Capa de superposición + tarjeta de contenido centrada + botones de confirmar/cancelar; admite tipos semánticos y cierre mediante la capa de superposición.

> Fuente del contrato: `lh-ui-core/specs/dialog.json` ·
> Casos dorados: `lh-ui-core/cases/dialog.json` (8 casos, traducidos a pruebas xUnit)

## Uso básico

Visibilidad declarativa: cuando `Visible` es false no se renderiza nada; tras confirmar/cancelar se dispara `Close`,
y el host pone `Visible` a false en consecuencia.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog básico</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">Confirmar eliminación</div>
      <div class="sim-dialog__content">La eliminación no se puede deshacer. ¿Confirmas la eliminación de este registro?</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">Cancelar</button>
        <button class="sim-btn sim-btn--danger">Confirmar</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var dialog = new LhDialog
{
    Title = "Confirmar eliminación",
    Content = "La eliminación no se puede deshacer. ¿Confirmas la eliminación de este registro?",
    Type = LhDialogType.Danger,
    ConfirmText = "Confirmar",
    CancelText = "Cancelar",
};
dialog.Confirm += () => { /* ejecutar la eliminación */ };
dialog.Close += () => dialog.Visible = false;

dialog.Visible = true;
```

## Tipos semánticos

`Type` determina el tipo (color) del botón de confirmación; el botón de cancelación siempre es del tipo default.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tipos de Dialog</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">Aviso</div>
      <div class="sim-dialog__content">Este es un diálogo de confirmación con el color principal</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">Cancelar</button>
        <button class="sim-btn sim-btn--primary">Confirmar</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `Confirm` | Se dispara al confirmar | `event Action` | — |
| `Cancel` | Se dispara al cancelar | `event Action` | — |
| `Close` | Se dispara tras confirmar/cancelar o (si closable) al pulsar la capa de superposición; el host pone Visible a false en consecuencia | `event Action` | — |
| `Visible` | Si se muestra; cuando es false no se renderiza nada | `bool` | `false` |
| `Title` | Título | `string` | `""` |
| `Content` | Contenido del cuerpo | `string` | `""` |
| `Type` | Tipo semántico, determina el tipo (color) del botón de confirmación | `LhDialogType` | `Default` |
| `ConfirmText` | Texto del botón de confirmación; si está vacío, no se muestra el botón de confirmación | `string` | `"确认"` |
| `CancelText` | Texto del botón de cancelación; si está vacío, no se muestra el botón de cancelación | `string` | `"取消"` |
| `Closable` | Si al pulsar la zona de la capa de superposición se dispara Close | `bool` | `false` |

### LhDialogType

| Valor | Tipo del botón de confirmación |
| --- | --- |
| `Default` | `LhButtonType.Default` |
| `Primary` | `LhButtonType.Primary` |
| `Success` | `LhButtonType.Success` |
| `Warning` | `LhButtonType.Warning` |
| `Danger` | `LhButtonType.Danger` |
| `Info` | `LhButtonType.Info` |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `dialog.width` / `dialog.borderRadius` | Ancho y radio de esquina de la tarjeta de contenido |
| `dialog.paddingHorizontal` / `paddingVertical` | Relleno interno de la tarjeta de contenido |
| `dialog.titleFontSize` / `contentFontSize` | Tamaños de fuente del título y del cuerpo |
| `dialog.gap` | Espaciado entre título / contenido / zona de botones |
| `color.background.default` | Color de fondo de la tarjeta de contenido |
| `color.background.overlay` | Color de la capa de superposición |
| `color.text.primary` / `color.text.regular` | Color del título / del cuerpo |
| `zIndex.modal` / `zIndex.modalBackdrop` | Nivel de la tarjeta de contenido / de la capa de superposición |
| `shadow.base` | Sombra de la tarjeta de contenido |

## Referencia de casos dorados

Cómo se mapean las 8 aserciones de `cases/dialog.json` a las pruebas xUnit (`tests/LhDialogTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `confirm-emits-onconfirm` | La confirmación dispara onConfirm | `Confirm_Emits_OnConfirm` |
| `cancel-emits-oncancel` | La cancelación dispara onCancel | `Cancel_Emits_OnCancel` |
| `confirm-also-emits-onclose` | La confirmación también dispara onClose | `Confirm_Also_Emits_OnClose` |
| `closable-overlay-emits-onclose` | Con closable, la pulsación de la capa de superposición dispara onClose | `Closable_Overlay_Emits_OnClose` |
| `not-closable-blocks-overlay-close` | Sin closable, la capa de superposición no cierra | `Not_Closable_Blocks_Overlay_Close` |
| `hidden-not-rendered` | Cuando no es visible, no se renderiza nada | `Hidden_Not_Rendered` |
| `title-renders-text` | El título y el cuerpo se renderizan con los tokens de tamaño de fuente | `Title_Renders_Text` |
| `danger-type-uses-danger-token` | El tipo danger consume el token de color de peligro | `Danger_Type_Uses_Danger_Token` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
