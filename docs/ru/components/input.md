# Поле ввода

Однострочный ввод текста. Поддерживает тип, размер, отключение, режим «только чтение», очистку и ограничение количества символов.

> Источник контракта: `lh-ui-core/specs/input.json` ·
> Золотые сценарии: `lh-ui-core/cases/input.json` (8 шт., переведены в тесты xUnit)

## Базовое использование

Контролируемый компонент: хост слушает `ValueChange` и записывает новое значение обратно в `Value`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input: база</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷 (сине-белый фарфор)</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field sim-input__field--placeholder">Введите ключевое слово</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var input = new LhInput
{
    Placeholder = "Введите ключевое слово",
};
input.ValueChange += value => { /* контролируемо: записываем значение обратно в Value */ };
```

## Размер

Три размера отображаются на токены `input.height` / `paddingHorizontal` / `fontSize`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Размеры Input</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input sim-input--small"><div class="sim-input__field">Маленькое</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">Среднее</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--large"><div class="sim-input__field">Большое</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Тип и состояние

При `Password` значение отображается маской из точек; ни `Disabled`, ни `Readonly` не запускают `ValueChange`,
но отключенный стиль применяется только для `Disabled`; в фокусе граница берёт `color.focus` + `input.focusRingWidth`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Состояния Input</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">●●●●●●●●</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--disabled"><div class="sim-input__field">Отключено</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--focus"><div class="sim-input__field">Граница фокуса</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Очистка и счётчик символов

Если `Clearable` равно true и значение непусто, показывается кнопка очистки, нажатие на которую запускает `ValueChange("")`;
при превышении `Maxlength` новый ввод усекается.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Очистка и счётчик</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷 (сине-белый фарфор)</div>
        <span class="sim-input__clear">✕</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `ValueChange` | Callback изменения значения (контролируемый); не запускается при disabled / readonly | `event Action<string>` | — |
| `Focus` | Запускается при получении фокуса | `event Action` | — |
| `Blur` | Запускается при потере фокуса | `event Action` | — |
| `Value` | Текущее текстовое значение (контролируемое) | `string` | `""` |
| `Placeholder` | Текст плейсхолдера; берёт токен `color.text.placeholder` | `string` | `""` |
| `Type` | Тип ввода; при password отображается маской из точек | `LhInputType` | `Text` |
| `Size` | Размер поля ввода; отображается на размерные токены | `LhInputSize` | `Medium` |
| `Disabled` | Отключено ли поле (не реагирует на ввод, отключенный стиль) | `bool` | `false` |
| `Readonly` | Доступно ли только чтение (может получать фокус, но не принимает ввод) | `bool` | `false` |
| `Clearable` | Показывать ли кнопку очистки | `bool` | `false` |
| `Maxlength` | Максимальное количество символов; при превышении ввод усекается | `int?` | `null` |

### LhInputSize

| Значение | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `input.height.small` (32epx) | `input.paddingHorizontal.small` (12epx) | `input.fontSize.small` (13) |
| `Medium` | `input.height.medium` (40epx) | `input.paddingHorizontal.medium` (16epx) | `input.fontSize.medium` (14) |
| `Large` | `input.height.large` (48epx) | `input.paddingHorizontal.large` (20epx) | `input.fontSize.large` (16) |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `input.height.{small,medium,large}` | Три значения высоты |
| `input.paddingHorizontal.{small,medium,large}` | Три значения горизонтального внутреннего отступа |
| `input.fontSize.{small,medium,large}` | Три значения размера шрифта |
| `input.borderRadius` / `input.borderWidth` | Радиус скругления и толщина границы вне фокуса |
| `input.focusRingWidth` / `color.focus` | Толщина и цвет границы в фокусе |
| `input.clearIconSize` | Размер значка кнопки очистки |
| `color.background.default` / `color.text.primary` | Фон контейнера / цвет текста |
| `color.text.placeholder` | Цвет текста плейсхолдера |
| `color.border.base` | Цвет границы вне фокуса |
| `color.disabled.{bg,text}` | Фон/текст отключенного состояния |
| `typography.fontFamily` | Китайский стек серифных шрифтов |

## Справочник золотых сценариев

Соответствие 8 утверждений из `cases/input.json` тестам xUnit (`tests/LhInputTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `input-emits-onvaluechange` | ввод запускает onValueChange | `Input_Emits_OnValueChange` |
| `clearable-emits-empty-value` | очистка запускает onValueChange("") | `Clearable_Emits_Empty_Value` |
| `disabled-blocks-input` | отключенное состояние не запускает и даёт отключенный стиль | `Disabled_Blocks_Input` |
| `readonly-blocks-input` | режим «только чтение» не запускает, но без отключенного стиля | `Readonly_Blocks_Input` |
| `placeholder-uses-placeholder-token` | текст плейсхолдера берёт токен плейсхолдера | `Placeholder_Uses_Placeholder_Token` |
| `focus-uses-focus-token` | граница фокуса берёт focus-токен | `Focus_Uses_Focus_Token` |
| `small-size-uses-small-tokens` | малый размер потребляет small-токены размера | `Small_Size_Uses_Small_Tokens` |
| `maxlength-truncates-input` | при превышении maxlength ввод усекается | `Maxlength_Truncates_Input` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
