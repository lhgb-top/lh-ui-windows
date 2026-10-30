# Barra de navegación

Barra fija en la parte superior de la página. Tres zonas: izquierda / centro / derecha; admite texto e iconos.

> Fuente del contrato: `lh-ui-core/specs/navbar.json` ·
> Casos dorados: `lh-ui-core/cases/navbar.json` (6 casos, traducidos a pruebas xUnit)

## Uso básico

El nivel es `zIndex.sticky`; el host la coloca como barra fija en la parte superior de la página (primera fila del Grid o parte superior del Canvas).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar básica</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">← Volver</span>
      <span class="sim-navbar__title">青花瓷 (porcelana azul y blanco)</span>
      <span class="sim-navbar__right">Más</span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

```csharp
var navbar = new LhNavbar
{
    Title = "青花瓷 (porcelana azul y blanco)",
    LeftText = "Volver",
    LeftIcon = "back",
    RightText = "Más",
};
navbar.LeftClick += () => { };
navbar.RightClick += () => { };
```

## Iconos

`LeftIcon` / `RightIcon` reciben un nombre de icono local (véase la tabla de glifos de la documentación de Icon); cuando ambos (icono y texto) están presentes, se muestran ambos;
si una zona no tiene contenido, no se dispara el evento de pulsación correspondiente.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Iconos de Navbar</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">←</span>
      <span class="sim-navbar__title">Solo iconos</span>
      <span class="sim-navbar__right">⋯</span>
    </div>
    <div class="sim-navbar">
      <span class="sim-navbar__title">Sin contenido en los laterales</span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## Línea de separación

Cuando `Bordered` es true, se dibuja una línea de separación `color.border.lighter` en la parte inferior; está activada por defecto.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Línea de separación de Navbar</span></div>
  <div class="sim-screen">
    <div class="sim-navbar sim-navbar--bordered">
      <span class="sim-navbar__title">Con separador</span>
    </div>
    <div class="sim-navbar sim-navbar--borderless">
      <span class="sim-navbar__title">Sin separador</span>
    </div>
  </div>
</div>
<p class="sim-note">Simulación HTML (apariencia de Windows / WinUI 3), no es un renderizado en dispositivo</p>

## API

### Props

| Prop | Descripción | Tipo | Predeterminado |
| --- | --- | --- | --- |
| `LeftClick` | Se dispara al pulsar la zona izquierda (no se dispara si no hay contenido izquierdo) | `event Action` | — |
| `RightClick` | Se dispara al pulsar la zona derecha (no se dispara si no hay contenido derecho) | `event Action` | — |
| `Title` | Título central | `string` | `""` |
| `LeftText` | Texto del botón izquierdo; cuando junto a LeftIcon ambos están presentes, se muestran ambos | `string` | `""` |
| `LeftIcon` | Nombre del icono izquierdo (renderizado por el sistema de iconos local) | `string?` | `null` |
| `RightText` | Texto del botón derecho; cuando junto a RightIcon ambos están presentes, se muestran ambos | `string` | `""` |
| `RightIcon` | Nombre del icono derecho (renderizado por el sistema de iconos local) | `string?` | `null` |
| `Bordered` | Si se dibuja la línea de separación inferior | `bool` | `true` |

## Referencia de tokens

| Token | Uso |
| --- | --- |
| `navbar.height` | Altura |
| `navbar.paddingHorizontal` | Relleno horizontal izquierdo/derecho |
| `navbar.titleFontSize` | Tamaño de fuente del título y de los textos laterales |
| `navbar.iconSize` | Tamaño de los iconos laterales |
| `navbar.borderWidth` | Ancho de la línea de separación inferior |
| `color.background.default` | Color de fondo |
| `color.text.primary` | Color del título y de los textos laterales |
| `color.border.lighter` | Color de la línea de separación inferior |
| `zIndex.sticky` | Nivel de la barra fija |
| `spacing.xs` | Espacio entre el icono y el texto |
| `typography.fontFamily` | Pila de fuentes serif de estilo chino |

## Referencia de casos dorados

Cómo se mapean las 6 aserciones de `cases/navbar.json` a las pruebas xUnit (`tests/LhNavbarTests.cs`):

| case id | Aserción | Test local |
| --- | --- | --- |
| `title-renders-text` | El título se renderiza con el token de tamaño de fuente | `Title_Renders_Text` |
| `leftclick-emits-onleftclick` | La pulsación de la zona izquierda dispara onLeftClick | `LeftClick_Emits_OnLeftClick` |
| `rightclick-emits-onrightclick` | La pulsación de la zona derecha dispara onRightClick | `RightClick_Emits_OnRightClick` |
| `no-left-content-no-onleftclick` | Sin contenido izquierdo, no se dispara | `No_Left_Content_No_OnLeftClick` |
| `bordered-uses-border-token` | La línea de separación consume el token border.lighter | `Bordered_Uses_Border_Token` |
| `height-uses-navbar-height-token` | La altura consume el token navbar.height | `Height_Uses_Navbar_Height_Token` |

Además hay pruebas específicas del modo oscuro: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Ejecución: `cd lh-ui-windows && dotnet test`.
