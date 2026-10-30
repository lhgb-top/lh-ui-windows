# Toast (короткое уведомление)

Пилюля цвета туши с собственным управлением видимостью и таймером. Поддерживает семантические типы, автоматическое скрытие и позиционирование.

> Источник контракта: `lh-ui-core/specs/toast.json` ·
> Золотые сценарии: `lh-ui-core/cases/toast.json` (6 шт., переведены в тесты xUnit)

## Базовое использование

Императивный интерфейс (show / hide / tick из spec) реализован на этой стороне как методы + внутренний таймер:
`Show(text)` делает уведомление видимым и сбрасывает таймер, `Hide()` скрывает немедленно, но не запускает `Close`.
Таймер держится на `DispatcherTimer`; по истечении уведомление скрывается автоматически и запускает `Close`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast: база</span></div>
  <div class="sim-screen">
    <div class="sim-toast">Сохранено</div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var controller = new LhToastController();
var toast = new LhToast
{
    Duration = 2000,
    Position = LhToastPosition.Center,
    Controller = controller,
};
toast.Close += () => { /* запускается после автоматического скрытия по истечении таймера */ };

// хост должен поместить toast последним дочерним узлом в Grid страницы (уровень zIndex.toast)
controller.Show("Сохранено");
```

## Семантический тип

При `Type`, отличном от default, перед текстом отрисовывается точка соответствующего семантического цвета (диаметр равен размеру шрифта).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Типы Toast</span></div>
  <div class="sim-screen">
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--success"></span>Сохранено успешно</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--warning"></span>Проверьте подключение к сети</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--danger"></span>Ошибка удаления</div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Позиционирование

`Position` определяет положение в доступной области; у верхнего и нижнего краёв оставляется отступ `spacing.xl` (при center отступа нет).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Позиции Toast</span></div>
  <div class="sim-screen">
    <div class="sim-toast sim-toast--top">Уведомление сверху</div>
    <div class="sim-toast sim-toast--bottom">Уведомление снизу</div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `Close` | Запускается при автоматическом скрытии по истечении таймера; ручное скрытие через Hide() не запускает | `event Action` | — |
| `Type` | Семантический тип; при значении, отличном от default, перед текстом отрисовывается точка соответствующего семантического цвета | `LhToastType` | `Default` |
| `Duration` | Период автоматического скрытия (миллисекунды) | `int` | `2000` |
| `Position` | Положение в доступной области | `LhToastPosition` | `Center` |
| `Controller` | Хэндл для вызовов со стороны хоста (опционально; после Loaded регистрирует Show/Hide) | `LhToastController?` | `null` |

### Методы

| Метод | Описание |
| --- | --- |
| `Show(string text)` | Показывает текст уведомления и сбрасывает таймер автоматического скрытия |
| `Hide()` | Скрывает немедленно, но не запускает Close |

### LhToastType

| Значение | Цвет точки |
| --- | --- |
| `Default` | Точка не отрисовывается |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

### LhToastPosition

| Значение | Позиционирование |
| --- | --- |
| `Top` | Сверху (отступ `spacing.xl` от верхнего края) |
| `Center` | По центру (без отступов) |
| `Bottom` | Снизу (отступ `spacing.xl` от нижнего края) |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `toast.fontSize` | Размер шрифта текста (он же — диаметр точки) |
| `toast.paddingHorizontal` / `paddingVertical` | Внутренние отступы капсулы |
| `toast.borderRadius` | Радиус скругления капсулы |
| `toast.gap` | Расстояние между точкой и текстом |
| `color.ink` | Фон капсулы |
| `color.onPrimary` | Цвет текста |
| `color.{success,warning,danger,info}.default` | Семантические точки |
| `zIndex.toast` | Уровень всплывающего слоя |
| `spacing.xl` | Отступы у верхнего и нижнего краёв |
| `typography.fontFamily` | Китайский стек серифных шрифтов |

## Справочник золотых сценариев

Соответствие 6 утверждений из `cases/toast.json` тестам xUnit (`tests/LhToastTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `show-makes-visible` | после show становится видимым и рендерит текст | `Show_Makes_Visible` |
| `auto-hides-after-duration` | по истечении таймера скрывается автоматически и запускает onClose | `Auto_Hides_After_Duration` |
| `tick-before-duration-keeps-visible` | до истечения таймера остаётся видимым | `Tick_Before_Duration_Keeps_Visible` |
| `hide-is-silent` | ручное скрытие через hide не запускает onClose | `Hide_Is_Silent` |
| `success-type-uses-success-token` | тип success потребляет семантический цвет и токены цвета туши | `Success_Type_Uses_Success_Token` |
| `hidden-emits-no-commands` | при скрытии не испускается ни одной команды отрисовки | `Hidden_Emits_No_Commands` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
