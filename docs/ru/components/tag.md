# Тег

Тег для маркировки и выбора. Поддерживает шесть семантических типов, три размера, плоский (plain) вариант, капсульный вариант и возможность закрытия.

> Источник контракта: `lh-ui-core/specs/tag.json` ·
> Золотые сценарии: `lh-ui-core/cases/tag.json` (6 шт., переведены в тесты xUnit)

## Тип

Шесть типов определяют цвет фона, цвет текста и цвет границы.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Типы Tag</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag">По умолчанию</span>
      <span class="sim-tag sim-tag--primary">Основной</span>
      <span class="sim-tag sim-tag--success">Успех</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--warning">Предупреждение</span>
      <span class="sim-tag sim-tag--danger">Опасность</span>
      <span class="sim-tag sim-tag--info">Информация</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var tag = new LhTag
{
    Text = "Основной",
    Type = LhTagType.Primary,
};
```

## Плоский вариант и скругление

Если `Plain` равно true, тег переключается на светлый семантический фон + семантический текст + семантическую границу; если `Round` равно true,
радиус скругления равен половине высоты (капсула; существует наряду с токеном `tag.borderRadius`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Граница и скругление</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--plain">Плоский</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">Плоский</span>
      <span class="sim-tag sim-tag--danger sim-tag--plain">Плоский</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--round">Капсула</span>
      <span class="sim-tag sim-tag--success sim-tag--round sim-tag--plain">Плоская капсула</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Размер и закрытие

Три размера отображаются на токены `tag.height` / `paddingHorizontal` / `fontSize`; если `Closable`
равно true, справа отрисовывается значок закрытия, нажатие на который запускает событие `Close`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Размер и закрытие</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--small">Малый</span>
      <span class="sim-tag">Средний</span>
      <span class="sim-tag sim-tag--large">Большой</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">Закрываемый <span class="sim-tag__close">✕</span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var tag = new LhTag
{
    Text = "Закрываемый",
    Type = LhTagType.Primary,
    Closable = true,
};
tag.Close += () => { /* нажатие на значок закрытия */ };
```

## Светлая и тёмная темы

Переключите тему в правом верхнем углу сайта — цвета тегов в рамке телефона-симулятора изменятся; на устройстве переключение выполняется вызовом `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Тёмные теги</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">Основной</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">Плоский</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве; значения для светлой/тёмной темы взяты из снимка токенов core</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `Close` | Запускается при нажатии на значок закрытия (требуется `Closable` = true) | `event Action` | — |
| `Text` | Текст тега (слот default в spec) | `string` | `""` |
| `Type` | Семантический тип; определяет цвет фона, текста и границы | `LhTagType` | `Default` |
| `Size` | Размер тега; отображается на размерные токены | `LhTagSize` | `Medium` |
| `Plain` | Использовать ли плоский вариант со светлым фоном и границей | `bool` | `false` |
| `Round` | Использовать ли капсульный вариант (скругление равно половине высоты) | `bool` | `false` |
| `Closable` | Показывать ли значок закрытия | `bool` | `false` |

### LhTagType

| Значение | Сплошной фон | Светлый фон (plain) | Цвет текста |
| --- | --- | --- | --- |
| `Default` | `color.background.default` | Тот же | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.primary.bg` | `color.onPrimary` / `color.primary.default` |
| `Success` | `color.success.default` | `color.success.bg` | `color.onPrimary` / `color.success.default` |
| `Warning` | `color.warning.default` | `color.warning.bg` | `color.onPrimary` / `color.warning.default` |
| `Danger` | `color.danger.default` | `color.danger.bg` | `color.onPrimary` / `color.danger.default` |
| `Info` | `color.info.default` | `color.info.bg` | `color.onPrimary` / `color.info.default` |

### LhTagSize

| Значение | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `tag.height.small` (20epx) | `tag.paddingHorizontal.small` (8epx) | `tag.fontSize.small` (11) |
| `Medium` | `tag.height.medium` (24epx) | `tag.paddingHorizontal.medium` (12epx) | `tag.fontSize.medium` (12) |
| `Large` | `tag.height.large` (28epx) | `tag.paddingHorizontal.large` (16epx) | `tag.fontSize.large` (13) |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `tag.height.{small,medium,large}` | Три значения высоты |
| `tag.paddingHorizontal.{small,medium,large}` | Три значения горизонтального внутреннего отступа |
| `tag.fontSize.{small,medium,large}` | Три значения размера шрифта |
| `tag.borderRadius` / `tag.borderWidth` | Радиус скругления и толщина границы |
| `color.primary.default` / `color.onPrimary` | Фон/текст основного типа |
| `color.{success,warning,danger,info}.{default,bg}` | Сплошной/светлый фон семантических типов |
| `color.background.default` / `color.text.primary` / `color.border.base` | Три цвета типа по умолчанию |
| `spacing.xs` | Расстояние между значком закрытия и текстом |
| `typography.fontFamily` | Китайский стек серифных шрифтов |

## Справочник золотых сценариев

Соответствие 6 утверждений из `cases/tag.json` тестам xUnit (`tests/LhTagTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `default-type-renders-text` | нейтральные фон/текст/граница у типа по умолчанию | `Default_Type_Renders_Text` |
| `primary-type-uses-primary-tokens` | основной тип потребляет токены основного цвета | `Primary_Type_Uses_Primary_Tokens` |
| `plain-uses-bg-tokens` | плоский вариант потребляет токены светлого семантического фона | `Plain_Uses_Bg_Tokens` |
| `small-size-uses-small-tokens` | малый размер потребляет small-токены размера | `Small_Size_Uses_Small_Tokens` |
| `round-makes-radius-half-height` | радиус капсулы равен половине высоты | `Round_Makes_Radius_Half_Height` |
| `closable-tap-emits-onclose` | нажатие на значок закрытия запускает onClose | `Closable_Tap_Emits_OnClose` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
