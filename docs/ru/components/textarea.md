# Текстовая область

Многострочный ввод текста. Задаёт начальное количество видимых строк через `Rows`, автоматически растёт при переполнении, поддерживает подсчёт символов.

> Источник контракта: `lh-ui-core/specs/textarea.json` ·
> Золотые сценарии: `lh-ui-core/cases/textarea.json` (6 шт., переведены в тесты xUnit)

## Базовое использование

Контролируемый компонент: хост слушает `ValueChange` и записывает новое значение обратно в `Value`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea: база</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷<br/>粉彩瓷</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field sim-textarea__field--placeholder">Введите описание</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var textarea = new LhTextarea
{
    Rows = 3,
    Placeholder = "Введите описание",
};
textarea.ValueChange += value => { /* контролируемо: записываем значение обратно в Value */ };
```

## Подсчёт символов

Если `ShowCount` равно true, в правом нижнем углу отрисовывается счётчик символов; при заданном `Maxlength` формат «текущее/максимум»,
иначе показывается только текущая длина. При превышении `Maxlength` новый ввод усекается.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Подсчёт символов</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷</div>
        <div class="sim-textarea__count">3/10</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷</div>
        <div class="sim-textarea__count">3</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Состояние

Ни `Disabled`, ни `Readonly` не запускают `ValueChange`, но отключенный стиль применяется только для `Disabled`;
в фокусе граница берёт `color.focus` + `textarea.focusRingWidth`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Состояния Textarea</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--disabled">
        <div class="sim-textarea__field">Отключено</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--focus">
        <div class="sim-textarea__field">Граница фокуса</div>
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
| `Rows` | Начальное количество видимых строк; высота растёт вместе с количеством строк и содержимым | `int` | `3` |
| `Disabled` | Отключено ли поле (не реагирует на ввод, отключенный стиль) | `bool` | `false` |
| `Readonly` | Доступно ли только чтение (может получать фокус, но не принимает ввод) | `bool` | `false` |
| `ShowCount` | Показывать ли счётчик символов в правом нижнем углу | `bool` | `false` |
| `Maxlength` | Максимальное количество символов; при превышении ввод усекается, при showCount используется как знаменатель | `int?` | `null` |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `textarea.paddingVertical` / `paddingHorizontal` | Внутренние отступы |
| `textarea.fontSize` | Размер шрифта |
| `textarea.borderRadius` / `borderWidth` | Радиус скругления и толщина границы вне фокуса |
| `textarea.focusRingWidth` / `color.focus` | Толщина и цвет границы в фокусе |
| `textarea.countFontSize` / `color.text.secondary` | Размер шрифта / цвет счётчика символов |
| `textarea.minHeight` | Минимальная высота (результат расчёта по строкам не меньше неё) |
| `color.background.default` / `color.text.primary` | Фон контейнера / цвет текста |
| `color.text.placeholder` | Цвет текста плейсхолдера |
| `color.border.base` | Цвет границы вне фокуса |
| `color.disabled.{bg,text}` | Фон/текст отключенного состояния |
| `typography.lineHeight.primary` | Межстрочный интервал (высота рассчитывается по rows) |
| `typography.fontFamily` | Китайский стек серифных шрифтов |

## Справочник золотых сценариев

Соответствие 6 утверждений из `cases/textarea.json` тестам xUnit (`tests/LhTextareaTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `input-emits-onvaluechange` | ввод запускает onValueChange | `Input_Emits_OnValueChange` |
| `disabled-blocks-input` | отключенное состояние не запускает и даёт отключенный стиль | `Disabled_Blocks_Input` |
| `showcount-renders-count` | «текущее/максимум» берёт токен countFontSize | `ShowCount_Renders_Count` |
| `showcount-without-maxlength-renders-length-only` | без maxlength показывается только длина | `ShowCount_Without_Maxlength_Renders_Length_Only` |
| `focus-uses-focus-token` | граница фокуса берёт focus-токен | `Focus_Uses_Focus_Token` |
| `maxlength-truncates-input` | при превышении maxlength ввод усекается | `Maxlength_Truncates_Input` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
