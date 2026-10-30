# Área de texto

Entrada de texto multilínea. Ofrece un número inicial de líneas visibles según `Rows`, crece automáticamente cuando el contenido las supera y admite recuento de caracteres.

> Fuente del contrato: `lh-ui-core/specs/textarea.json` ·
> Casos dorados: `lh-ui-core/cases/textarea.json` (6 casos, traducidos a pruebas xUnit)

## Uso básico

Componente controlado: el host escucha `ValueChange` y reescribe el nuevo valor en `Value`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea básica</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷 (porcelana azul y blanco)<br/>粉彩瓷 (famille rose)</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field sim-textarea__field--placeholder">Introduce una descripción</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var textarea = new LhTextarea
{
    Rows = 3,
    Placeholder = "Introduce una descripción",
};
textarea.ValueChange += value => { /* controlado: reescribe Value */ };
```

## Recuento de caracteres

Cuando `ShowCount` es true, se dibuja un recuento de caracteres en la esquina inferior derecha; si se declara `Maxlength`, el formato es «actual/máximo»,
en caso contrario solo se muestra la longitud actual. Al superar `Maxlength`, se trunca la nueva entrada.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Recuento de caracteres</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷 (porcelana azul y blanco)</div>
        <div class="sim-textarea__count">3/10</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷 (porcelana azul y blanco)</div>
        <div class="sim-textarea__count">3</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Estados

Tanto `Disabled` como `Readonly` no disparan `ValueChange`, pero solo `Disabled` presenta el estilo deshabilitado;
al recibir el foco, el borde toma `color.focus` + `textarea.focusRingWidth`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Estados de Textarea</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--disabled">
        <div class="sim-textarea__field">Deshabilitado</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--focus">
        <div class="sim-textarea__field">Borde de foco</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `ValueChange` | Devolución de llamada de cambio de valor (controlado); no se dispara con disabled / readonly | `event Action<string>` | — |
| `Focus` | Se dispara al recibir el foco | `event Action` | — |
| `Blur` | Se dispara al perder el foco | `event Action` | — |
| `Value` | Valor de texto actual (controlado) | `string` | `""` |
| `Placeholder` | Texto del marcador de posición, toma el token `color.text.placeholder` | `string` | `""` |
| `Rows` | Número inicial de líneas visibles; la altura crece con el número de líneas y el contenido | `int` | `3` |
| `Disabled` | Si está deshabilitado (no responde a la entrada y presenta el estilo deshabilitado) | `bool` | `false` |
| `Readonly` | Si es de solo lectura (puede recibir el foco pero no acepta entrada) | `bool` | `false` |
| `ShowCount` | Si se muestra el recuento de caracteres en la esquina inferior derecha | `bool` | `false` |
| `Maxlength` | Número máximo de caracteres; al superarlo se trunca la nueva entrada y, con showCount, actúa como denominador | `int?` | `null` |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `textarea.paddingVertical` / `paddingHorizontal` | Relleno interno |
| `textarea.fontSize` | Tamaño de fuente |
| `textarea.borderRadius` / `borderWidth` | Radio de esquina y ancho del borde sin foco |
| `textarea.focusRingWidth` / `color.focus` | Ancho y color del borde con foco |
| `textarea.countFontSize` / `color.text.secondary` | Tamaño de fuente / color del recuento de caracteres |
| `textarea.minHeight` | Altura mínima (el resultado del cálculo por líneas no es inferior a ella) |
| `color.background.default` / `color.text.primary` | Fondo del contenedor / color del texto |
| `color.text.placeholder` | Color del texto del marcador de posición |
| `color.border.base` | Color del borde sin foco |
| `color.disabled.{bg,text}` | Fondo / texto del estado deshabilitado |
| `typography.lineHeight.primary` | Altura de línea (para calcular la altura a partir de rows) |
| `typography.fontFamily` | Pila de fuentes serif de estilo chino |

## Referencia de casos dorados

Cómo se mapean las 6 aserciones de `cases/textarea.json` a las pruebas xUnit (`tests/LhTextareaTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `input-emits-onvaluechange` | La entrada dispara onValueChange | `Input_Emits_OnValueChange` |
| `disabled-blocks-input` | El estado deshabilitado no dispara y presenta el estilo deshabilitado | `Disabled_Blocks_Input` |
| `showcount-renders-count` | El formato «actual/máximo» toma el token countFontSize | `ShowCount_Renders_Count` |
| `showcount-without-maxlength-renders-length-only` | Sin maxlength, solo se muestra la longitud | `ShowCount_Without_Maxlength_Renders_Length_Only` |
| `focus-uses-focus-token` | El borde con foco toma el token focus | `Focus_Uses_Focus_Token` |
| `maxlength-truncates-input` | Al superar maxlength se trunca | `Maxlength_Truncates_Input` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
