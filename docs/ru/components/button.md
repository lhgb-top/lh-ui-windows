# Кнопка

Базовая кнопка действия. Запускает немедленное действие; поддерживает тип, размер, состояние загрузки, отключение, значки и узорный китайский фон (花纹底).

> Источник контракта: `lh-ui-core/specs/button.json` ·
> Золотые сценарии: `lh-ui-core/cases/button.json` (8 шт., переведены в тесты xUnit)

## Тип

Шесть типов определяют цвет фона и цвет текста.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Типы Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">По умолчанию</button>
      <button class="sim-btn sim-btn--primary">Основная</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--success">Успех</button>
      <button class="sim-btn sim-btn--warning">Предупреждение</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--danger">Опасность</button>
      <button class="sim-btn sim-btn--info">Информация</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var button = new LhButton
{
    Type = LhButtonType.Primary,
    Text = "Основная",
};
button.Click += OnButtonClick;
```

## Размер

Три размера отображаются на токены `button.height` / `paddingHorizontal` / `fontSize` / `iconSize`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Размеры Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">Маленькая</button>
      <button class="sim-btn">Средняя</button>
      <button class="sim-btn sim-btn--large">Большая</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Состояние

Отключенная кнопка и кнопка в состоянии загрузки не запускают `Click`; если оба состояния заданы одновременно, **отключение имеет приоритет** (золотой сценарий `disabled-precedes-loading`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Состояния Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">Обычная</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Отключена</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>Загрузка
      </button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Отключение приоритетнее</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Значки и узорный фон

Имя значка разрешается локальной системой значков (`LhButtonTokens.IconFor`; сейчас поддерживаются search / refresh / check / close, незарегистрированные имена не отрисовываются). Узорный фон потребляет токен `color.decoration` и накладывается диагональной штриховкой под 45°.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Значки и узор</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>Поиск</button>
      <button class="sim-btn"><span class="sim-btn__icon"></span>По умолчанию</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--pattern">Основной узорный фон</button>
      <button class="sim-btn sim-btn--pattern">Узорный фон по умолчанию</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Светлая и тёмная темы

Переключите тему в правом верхнем углу сайта — цвета кнопок в рамке телефона-симулятора изменятся; на устройстве переключение выполняется вызовом `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Тёмные кнопки</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">Основная</button>
      <button class="sim-btn">По умолчанию</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Отключена</button>
      <button class="sim-btn sim-btn--success">Успех</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве; значения для светлой/тёмной темы взяты из снимка токенов core</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `Click` | Событие нажатия; не запускается, если `Disabled` или `Loading` равно true | `event Action` | — |
| `Type` | Тип кнопки; определяет цвет фона и текста | `LhButtonType` | `Default` |
| `Size` | Размер кнопки; отображается на размерные токены | `LhButtonSize` | `Medium` |
| `Disabled` | Отключена ли кнопка | `bool` | `false` |
| `Loading` | Находится ли кнопка в состоянии загрузки (показывает индикатор и не запускает Click) | `bool` | `false` |
| `Icon` | Имя значка (разрешается локальной системой значков) | `string?` | `null` |
| `BgPattern` | Рисовать ли узорный китайский фон | `bool` | `false` |
| `Text` | Текст кнопки (слот default в spec) | `string` | `""` |

### LhButtonType

| Значение | Фон | Цвет текста |
| --- | --- | --- |
| `Default` | `color.background.default` | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.onPrimary` |
| `Success` | `color.success.default` | `color.onPrimary` |
| `Warning` | `color.warning.default` | `color.onPrimary` |
| `Danger` | `color.danger.default` | `color.onPrimary` |
| `Info` | `color.info.default` | `color.onPrimary` |

### LhButtonSize

| Значение | height | paddingHorizontal | fontSize | iconSize |
| --- | --- | --- | --- | --- |
| `Small` | `button.height.small` (32epx) | `button.paddingHorizontal.small` (16epx) | `button.fontSize.small` (13) | `button.iconSize.small` (16) |
| `Medium` | `button.height.medium` (40epx) | `button.paddingHorizontal.medium` (20epx) | `button.fontSize.medium` (14) | `button.iconSize.medium` (18) |
| `Large` | `button.height.large` (48epx) | `button.paddingHorizontal.large` (24epx) | `button.fontSize.large` (16) | `button.iconSize.large` (20) |

Переопределение отключенного состояния: фон `color.disabled.bg`, текст `color.disabled.text`, граница `color.disabled.border`.

## Справочник токенов

Токены, потребляемые кнопкой (построчно соответствуют `tokenRefs` из `specs/button.json`):

| Токен | Назначение |
| --- | --- |
| `button.height.{small,medium,large}` | Три значения высоты |
| `button.paddingHorizontal.{small,medium,large}` | Три значения горизонтального внутреннего отступа |
| `button.fontSize.{small,medium,large}` | Три значения размера шрифта |
| `button.iconSize.{small,medium,large}` | Три размера значков |
| `button.borderRadius` / `button.borderWidth` | Радиус скругления (следует за `radius.base`) и толщина границы |
| `button.fontWeight` | Насыщенность шрифта (следует за `typography.fontWeight.primary`, на устройстве отображается на FontWeights) |
| `color.primary.default` / `color.onPrimary` | Фон/текст основного типа |
| `color.{success,warning,danger,info}.default` | Фоны семантических типов |
| `color.background.default` / `color.text.primary` / `color.border.base` | Фон/текст/граница типа по умолчанию |
| `color.disabled.{bg,text,border}` | Три цвета отключенного состояния |
| `color.decoration` | Цвет узорного фона |
| `spacing.xs` | Расстояние между значком и текстом |
| `typography.fontFamily` | Китайский стек серифных шрифтов |

## Справочник золотых сценариев

Соответствие 8 утверждений из `cases/button.json` тестам xUnit (`tests/LhButtonTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `click-emits-onclick` | гейт пропускает (интерактивна) | `Enabled_Button_Is_Interactive` |
| `disabled-blocks-click` | отключенное состояние не интерактивно | `Disabled_Is_Not_Interactive` |
| `loading-blocks-click` | состояние загрузки не интерактивно | `Loading_Is_Not_Interactive` |
| `disabled-precedes-loading` | отключение приоритетнее + стиль отключения | `Disabled_Precedes_Loading` |
| `default-type-renders-text` | нейтральные фон/текст у типа по умолчанию | `Default_Type_Uses_Neutral_Tokens` |
| `primary-type-uses-primary-tokens` | основной тип потребляет основные токены | `Primary_Type_Uses_Primary_Tokens` |
| `small-size-uses-small-tokens` | малый размер потребляет small-токены размера | `Small_Size_Uses_Small_Tokens` |
| `bgpattern-uses-decoration-token` | узорный фон потребляет декоративный цвет | `BgPattern_Uses_Decoration_Token` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
