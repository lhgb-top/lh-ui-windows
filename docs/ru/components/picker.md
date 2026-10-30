# Средство выбора

Выезжающее снизу прокручиваемое колесо с одним столбцом. Поддерживает варианты, кламп индекса и панель инструментов.

> Источник контракта: `lh-ui-core/specs/picker.json` ·
> Золотые сценарии: `lh-ui-core/cases/picker.json` (8 шт., переведены в тесты xUnit)

## Базовое использование

Декларативная видимость: при `Visible` равном false не отрисовывается ничего; после подтверждения/отмены запускается `Close`,
и хост переводит `Visible` в false.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Picker: база</span></div>
  <div class="sim-screen">
    <div class="sim-picker-overlay"></div>
    <div class="sim-picker">
      <div class="sim-picker__toolbar">
        <span class="sim-picker__cancel">Отмена</span>
        <span class="sim-picker__title">Выбор</span>
        <span class="sim-picker__confirm">Подтвердить</span>
      </div>
      <div class="sim-picker__wheel">
        <span class="sim-picker__item">青花瓷 (сине-белый фарфор)</span>
        <span class="sim-picker__item sim-picker__item--active">粉彩瓷 (famille rose)</span>
        <span class="sim-picker__item">玲珑瓷 (фарфор линьлун)</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var picker = new LhPicker
{
    Options = new[] { "青花瓷 (сине-белый фарфор)", "粉彩瓷 (famille rose)", "玲珑瓷 (фарфор линьлун)" },
    Value = 1,
    Title = "Выбор",
};
picker.ValueChange += index => { /* выбранный индекс после остановки колеса */ };
picker.Confirm += index => { /* текущий выбранный индекс */ };
picker.Close += () => picker.Visible = false;

picker.Visible = true;
```

## Кламп индекса

При выходе `Value` за границы индекс клампится в диапазон вариантов: отрицательное значение становится 0, значение больше количества вариантов - 1 — последним элементом.

```csharp
Assert.Equal(0, LhPickerTokens.ClampIndex(-1, 3));   // отрицательное → 0
Assert.Equal(2, LhPickerTokens.ClampIndex(100, 3));  // выход за границу → последний элемент
```

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `ValueChange` | Запускается, когда колесо остановилось и выбранный элемент изменился | `event Action<int>` | — |
| `Confirm` | Запускается при нажатии кнопки подтверждения, несёт текущий выбранный индекс | `event Action<int>` | — |
| `Cancel` | Запускается при нажатии кнопки отмены | `event Action` | — |
| `Close` | Запускается после подтверждения/отмены; хост переводит Visible в false | `event Action` | — |
| `Visible` | Показывать ли; при false не отрисовывается ничего | `bool` | `false` |
| `Options` | Массив текстов вариантов (один столбец) | `string[]?` | `null` |
| `Value` | Индекс выбранного элемента (с 0); при выходе за границы клампится в диапазон вариантов | `int` | `0` |
| `Title` | Заголовок по центру панели инструментов | `string` | `""` |
| `ConfirmText` | Текст кнопки подтверждения | `string` | `"确认"` |
| `CancelText` | Текст кнопки отмены | `string` | `"取消"` |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `picker.height` | Высота панели |
| `picker.toolbarHeight` | Высота верхней панели инструментов (высота области колеса = высота панели - высота панели инструментов) |
| `picker.itemHeight` | Высота варианта (расстояние между центральными индикаторами такое же) |
| `picker.itemFontSize` | Размер шрифта вариантов и панели инструментов |
| `picker.borderRadius` | Радиус скругления верха панели |
| `color.background.default` | Фон панели |
| `color.background.overlay` | Цвет затемнения |
| `color.text.primary` | Цвет заголовка и вариантов |
| `color.text.secondary` | Цвет кнопки отмены |
| `color.primary.default` | Цвет кнопки подтверждения |
| `color.border.lighter` | Цвет линий центрального индикатора |
| `zIndex.modal` / `zIndex.modalBackdrop` | Уровни панели/затемнения |

## Справочник золотых сценариев

Соответствие 8 утверждений из `cases/picker.json` тестам xUnit (`tests/LhPickerTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `confirm-emits-onconfirm-with-value` | подтверждение несёт текущий выбранный индекс | `Confirm_Emits_OnConfirm_With_Value` |
| `confirm-also-emits-onclose` | подтвержение также запускает onClose | `Confirm_Also_Emits_OnClose` |
| `cancel-emits-oncancel` | отмена запускает onCancel | `Cancel_Emits_OnCancel` |
| `scroll-emits-onvaluechange` | остановка колеса запускает onValueChange | `Scroll_Emits_OnValueChange` |
| `clamp-negative-value-to-zero` | отрицательный индекс клампится в 0 | `Clamp_Negative_Value_To_Zero` |
| `clamp-overflow-value-to-last` | индекс за границей клампится в последний элемент | `Clamp_Overflow_Value_To_Last` |
| `title-renders-text` | заголовок рендерится с токеном размера шрифта | `Title_Renders_Text` |
| `hidden-not-rendered` | при скрытии не отрисовывается ничего | `Hidden_Not_Rendered` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
