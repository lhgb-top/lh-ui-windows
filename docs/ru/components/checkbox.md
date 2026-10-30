# Флажок

Флажок. В выбранном состоянии рисует галочку, в неопределённом — горизонтальную черту; нажатие срабатывает по всей строке (квадрат + область метки).

> Источник контракта: `lh-ui-core/specs/checkbox.json` ·
> Золотые сценарии: `lh-ui-core/cases/checkbox.json` (6 шт., переведены в тесты xUnit)

## Базовое использование

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox: база</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box"></span>
        <span class="sim-checkbox__label">Не выбран</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Выбран</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">─</span>
        <span class="sim-checkbox__label">Неопределённое</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var checkbox = new LhCheckbox
{
    Label = "青花瓷 (сине-белый фарфор)",
    Checked = true,
};
checkbox.Change += isChecked => { };
```

## Размер

Три размера отображаются на токены размера квадрата `checkbox.size.*`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Размеры Checkbox</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--small sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Маленький</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Средний</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--large sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">Большой</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Состояние

При `Disabled` флажок не реагирует на нажатия, а фон, граница, индикатор и текст берут токены `color.disabled.*`;
`Indeterminate` — неопределённое состояние (черта), после нажатия становится выбранным.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Состояния Checkbox</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked sim-checkbox__box--disabled">✓</span>
        <span class="sim-checkbox__label">Отключён выбранный</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--disabled"></span>
        <span class="sim-checkbox__label">Отключён не выбранный</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `Change` | Callback изменения состояния выбора; не запускается при disabled | `event Action<bool>` | — |
| `Checked` | Выбран ли флажок | `bool` | `false` |
| `Indeterminate` | Находится ли в неопределённом состоянии (индикатор-черта); после нажатия становится выбранным | `bool` | `false` |
| `Disabled` | Отключён ли флажок (не реагирует на нажатия и даёт отключенный стиль) | `bool` | `false` |
| `Label` | Текстовая метка (отрисовывается справа от квадрата) | `string` | `""` |
| `Size` | Размер квадрата; отображается на токены `checkbox.size.*` | `LhCheckboxSize` | `Medium` |

### LhCheckboxSize

| Значение | Токен размера |
| --- | --- |
| `Small` | `checkbox.size.small` (14epx) |
| `Medium` | `checkbox.size.medium` (16epx) |
| `Large` | `checkbox.size.large` (18epx) |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `checkbox.size.{small,medium,large}` | Три размера квадрата |
| `checkbox.borderRadius` / `checkbox.borderWidth` | Радиус скругления и толщина границы квадрата |
| `checkbox.checkWidth` | Толщина границы галочки / черты |
| `checkbox.gap` | Расстояние между квадратом и текстом |
| `color.primary.default` / `color.onPrimary` | Фон и цвет индикатора выбранного / неопределённого состояния |
| `color.border.base` | Цвет границы невыбранного состояния |
| `color.disabled.{bg,border,text}` | Фон/граница/индикатор отключенного состояния |
| `color.text.{primary,disabled}` | Цвет текста метки |
| `typography.fontSize.base` / `fontFamily` | Размер шрифта и стек шрифтов метки |

## Справочник золотых сценариев

Соответствие 6 утверждений из `cases/checkbox.json` тестам xUnit (`tests/LhCheckboxTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | невыбранный после нажатия становится выбранным | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-unchecks` | выбранный после нажатия снимает выбор | `Tap_Checked_Unchecks` |
| `disabled-blocks-change` | отключенное состояние не интерактивно | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | выбранное состояние потребляет токены основного цвета | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | невыбранное состояние потребляет токен границы | `Unchecked_Uses_Border_Token` |
| `indeterminate-becomes-checked` | неопределённое состояние после нажатия становится выбранным | `Indeterminate_Becomes_Checked` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
