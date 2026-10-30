# Панель навигации

Фиксированная панель в верхней части страницы. Три секции: левая / центральная / правая; поддерживает текст и значки.

> Источник контракта: `lh-ui-core/specs/navbar.json` ·
> Золотые сценарии: `lh-ui-core/cases/navbar.json` (6 шт., переведены в тесты xUnit)

## Базовое использование

Уровень `zIndex.sticky`; хост использует компонент как фиксированную панель сверху страницы (первая строка Grid или верх Canvas).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar: база</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">← Назад</span>
      <span class="sim-navbar__title">青花瓷 (сине-белый фарфор)</span>
      <span class="sim-navbar__right">Ещё</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var navbar = new LhNavbar
{
    Title = "青花瓷 (сине-белый фарфор)",
    LeftText = "Назад",
    LeftIcon = "back",
    RightText = "Ещё",
};
navbar.LeftClick += () => { };
navbar.RightClick += () => { };
```

## Значки

`LeftIcon` / `RightIcon` принимают имена значков из локальной системы значков (см. таблицу глифов в документации Icon); если значок и текст оба непусты, показываются оба;
если на стороне нет содержимого, соответствующее событие нажатия не запускается.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Значки Navbar</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">←</span>
      <span class="sim-navbar__title">Только значки</span>
      <span class="sim-navbar__right">⋯</span>
    </div>
    <div class="sim-navbar">
      <span class="sim-navbar__title">Без содержимого по краям</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Разделительная линия

Если `Bordered` равно true, снизу проводится разделительная линия `color.border.lighter`; по умолчанию включено.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Линия Navbar</span></div>
  <div class="sim-screen">
    <div class="sim-navbar sim-navbar--bordered">
      <span class="sim-navbar__title">С разделительной линией</span>
    </div>
    <div class="sim-navbar sim-navbar--borderless">
      <span class="sim-navbar__title">Без разделительной линии</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `LeftClick` | Запускается при нажатии на левую область (не запускается, если левого содержимого нет) | `event Action` | — |
| `RightClick` | Запускается при нажатии на правую область (не запускается, если правого содержимого нет) | `event Action` | — |
| `Title` | Заголовок по центру | `string` | `""` |
| `LeftText` | Текст левой кнопки; если непуст и LeftIcon, показываются оба | `string` | `""` |
| `LeftIcon` | Имя левого значка (рендерится локальной системой значков) | `string?` | `null` |
| `RightText` | Текст правой кнопки; если непуст и RightIcon, показываются оба | `string` | `""` |
| `RightIcon` | Имя правого значка (рендерится локальной системой значков) | `string?` | `null` |
| `Bordered` | Рисовать ли разделительную линию снизу | `bool` | `true` |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `navbar.height` | Высота |
| `navbar.paddingHorizontal` | Левый и правый внутренние отступы |
| `navbar.titleFontSize` | Размер шрифта заголовка и боковых текстов |
| `navbar.iconSize` | Размер боковых значков |
| `navbar.borderWidth` | Толщина разделительной линии снизу |
| `color.background.default` | Фон |
| `color.text.primary` | Цвет заголовка и боковых текстов |
| `color.border.lighter` | Цвет разделительной линии снизу |
| `zIndex.sticky` | Уровень фиксирования |
| `spacing.xs` | Расстояние между значком и текстом |
| `typography.fontFamily` | Китайский стек серифных шрифтов |

## Справочник золотых сценариев

Соответствие 6 утверждений из `cases/navbar.json` тестам xUnit (`tests/LhNavbarTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `title-renders-text` | заголовок рендерится с токеном размера шрифта | `Title_Renders_Text` |
| `leftclick-emits-onleftclick` | нажатие слева запускает onLeftClick | `LeftClick_Emits_OnLeftClick` |
| `rightclick-emits-onrightclick` | нажатие справа запускает onRightClick | `RightClick_Emits_OnRightClick` |
| `no-left-content-no-onleftclick` | при отсутствии левого содержимого не запускается | `No_Left_Content_No_OnLeftClick` |
| `bordered-uses-border-token` | разделительная линия потребляет токен border.lighter | `Bordered_Uses_Border_Token` |
| `height-uses-navbar-height-token` | высота потребляет токен navbar.height | `Height_Uses_Navbar_Height_Token` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
