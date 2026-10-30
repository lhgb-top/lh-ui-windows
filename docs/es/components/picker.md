# Selector

Rueda de una sola columna que aparece en la parte inferior. Admite opciones, clamp de índice y barra de herramientas.

> Fuente del contrato: `lh-ui-core/specs/picker.json` ·
> Casos dorados: `lh-ui-core/cases/picker.json` (8 casos, traducidos a pruebas xUnit)

## Uso básico

Visibilidad declarativa: cuando `Visible` es false no se renderiza nada; tras confirmar/cancelar se dispara `Close`,
y el host pone `Visible` a false en consecuencia.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Picker básico</span></div>
  <div class="sim-screen">
    <div class="sim-picker-overlay"></div>
    <div class="sim-picker">
      <div class="sim-picker__toolbar">
        <span class="sim-picker__cancel">Cancelar</span>
        <span class="sim-picker__title">Seleccionar</span>
        <span class="sim-picker__confirm">Confirmar</span>
      </div>
      <div class="sim-picker__wheel">
        <span class="sim-picker__item">青花瓷 (porcelana azul y blanco)</span>
        <span class="sim-picker__item sim-picker__item--active">粉彩瓷 (famille rose)</span>
        <span class="sim-picker__item">玲珑瓷 (porcelana linglong)</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var picker = new LhPicker
{
    Options = new[] { "青花瓷", "粉彩瓷", "玲珑瓷" },
    Value = 1,
    Title = "Seleccionar",
};
picker.ValueChange += index => { /* índice seleccionado tras detenerse la rueda */ };
picker.Confirm += index => { /* índice actualmente seleccionado */ };
picker.Close += () => picker.Visible = false;

picker.Visible = true;
```

## Clamp de índice

Cuando `Value` está fuera de rango, se aplica un clamp al rango de las opciones: los valores negativos toman 0 y los mayores que el número de opciones - 1 toman el último elemento.

```csharp
Assert.Equal(0, LhPickerTokens.ClampIndex(-1, 3));   // los negativos toman 0
Assert.Equal(2, LhPickerTokens.ClampIndex(100, 3));  // fuera de rango: toma el último elemento
```

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `ValueChange` | Se dispara cuando la rueda se detiene y el elemento seleccionado cambia | `event Action<int>` | — |
| `Confirm` | Se dispara al pulsar el botón de confirmación, con el índice actualmente seleccionado | `event Action<int>` | — |
| `Cancel` | Se dispara al pulsar el botón de cancelación | `event Action` | — |
| `Close` | Se dispara tras confirmar/cancelar; el host pone Visible a false en consecuencia | `event Action` | — |
| `Visible` | Si se muestra; cuando es false no se renderiza nada | `bool` | `false` |
| `Options` | Array de textos de las opciones (una sola columna) | `string[]?` | `null` |
| `Value` | Índice del elemento seleccionado (base 0); si está fuera de rango, se aplica un clamp al rango de las opciones | `int` | `0` |
| `Title` | Título en el centro de la barra de herramientas | `string` | `""` |
| `ConfirmText` | Texto del botón de confirmación | `string` | `"确认"` |
| `CancelText` | Texto del botón de cancelación | `string` | `"取消"` |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `picker.height` | Altura del panel |
| `picker.toolbarHeight` | Altura de la barra de herramientas superior (la altura de la zona de la rueda = altura del panel - altura de la barra) |
| `picker.itemHeight` | Altura de las opciones (el espaciado del indicador central es el mismo) |
| `picker.itemFontSize` | Tamaño de fuente de las opciones y de la barra de herramientas |
| `picker.borderRadius` | Radio de esquina superior del panel |
| `color.background.default` | Color de fondo del panel |
| `color.background.overlay` | Color de la capa de superposición |
| `color.text.primary` | Color del título y de las opciones |
| `color.text.secondary` | Color del botón de cancelación |
| `color.primary.default` | Color del botón de confirmación |
| `color.border.lighter` | Color de las líneas del indicador central |
| `zIndex.modal` / `zIndex.modalBackdrop` | Nivel del panel / de la capa de superposición |

## Referencia de casos dorados

Cómo se mapean las 8 aserciones de `cases/picker.json` a las pruebas xUnit (`tests/LhPickerTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `confirm-emits-onconfirm-with-value` | La confirmación lleva el índice actualmente seleccionado | `Confirm_Emits_OnConfirm_With_Value` |
| `confirm-also-emits-onclose` | La confirmación también dispara onClose | `Confirm_Also_Emits_OnClose` |
| `cancel-emits-oncancel` | La cancelación dispara onCancel | `Cancel_Emits_OnCancel` |
| `scroll-emits-onvaluechange` | La detención de la rueda dispara onValueChange | `Scroll_Emits_OnValueChange` |
| `clamp-negative-value-to-zero` | Un índice negativo se ajusta a 0 | `Clamp_Negative_Value_To_Zero` |
| `clamp-overflow-value-to-last` | Un índice fuera de rango se ajusta al último elemento | `Clamp_Overflow_Value_To_Last` |
| `title-renders-text` | El título se renderiza con el token de tamaño de fuente | `Title_Renders_Text` |
| `hidden-not-rendered` | Cuando no es visible, no se renderiza nada | `Hidden_Not_Rendered` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
