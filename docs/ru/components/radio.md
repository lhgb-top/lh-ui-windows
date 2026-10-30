# Радиокнопка

Радиокнопка. Кольцо + внутренняя точка, нажатие срабатывает по всей строке; сам компонент не содержит логики группы, группу радиокнопок поддерживает хост.

> Источник контракта: `lh-ui-core/specs/radio.json` ·
> Золотые сценарии: `lh-ui-core/cases/radio.json` (6 шт., переведены в тесты xUnit)

## Базовое использование

Хост слушает `Change` и сбрасывает `Checked` остальных элементов группы в false — так получается группа радиокнопок.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio: база</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">青花瓷 (сине-белый фарфор)</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"></span>
        <span class="sim-radio__label">粉彩瓷 (famille rose)</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var radio = new LhRadio
{
    Label = "青花瓷 (сине-белый фарфор)",
    Value = "qinghua",
    Checked = true,
};
radio.Change += isChecked => { /* запускается только если не выбран и не отключён; группу поддерживает хост */ };
```

## Размер

Три размера отображаются на токены размера кольца `radio.size.*`; диаметр внутренней точки = размер × `radio.dotScale`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Размеры Radio</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--small"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Маленькая</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Средняя</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--large"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Большая</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Состояние

При `Disabled` радиокнопка не реагирует на нажатия: внешнее кольцо берёт `color.disabled.border`, точка — `color.disabled.text`;
повторное нажатие на уже выбранный элемент не запускает `Change`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Состояния Radio</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"></span>
        <span class="sim-radio__label">Отключена не выбрана</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">Отключена выбрана</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `Change` | Callback изменения состояния выбора; запускается только если элемент не выбран и не отключён (payload всегда true) | `event Action<bool>` | — |
| `Checked` | Выбрана ли радиокнопка | `bool` | `false` |
| `Disabled` | Отключена ли радиокнопка (не реагирует на нажатия и даёт отключенный стиль) | `bool` | `false` |
| `Label` | Текстовая метка (отрисовывается справа от кольца) | `string` | `""` |
| `Value` | Значение радиокнопки (идентификатор в группе; сам компонент не содержит логики группы) | `string` | `""` |
| `Size` | Размер кольца; отображается на токены `radio.size.*` | `LhRadioSize` | `Medium` |

### LhRadioSize

| Значение | Токен размера |
| --- | --- |
| `Small` | `radio.size.small` (14epx) |
| `Medium` | `radio.size.medium` (16epx) |
| `Large` | `radio.size.large` (18epx) |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `radio.size.{small,medium,large}` | Три размера кольца |
| `radio.ringWidth` | Толщина границы внешнего кольца |
| `radio.dotScale` | Коэффициент диаметра внутренней точки (размер × dotScale) |
| `radio.gap` | Расстояние между кольцом и текстом |
| `color.primary.default` | Цвет внешнего кольца и точки выбранного состояния |
| `color.border.base` | Цвет внешнего кольца невыбранного состояния |
| `color.disabled.{border,text}` | Внешнее кольцо/точка отключенного состояния |
| `color.text.{primary,disabled}` | Цвет текста метки |
| `typography.fontSize.base` / `fontFamily` | Размер шрифта и стек шрифтов метки |

## Справочник золотых сценариев

Соответствие 6 утверждений из `cases/radio.json` тестам xUnit (`tests/LhRadioTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `tap-unchecked-emits-onchange` | нажатие на невыбранную запускает onChange | `Tap_Unchecked_Emits_OnChange` |
| `tap-checked-keeps-checked` | повторное нажатие на выбранную не запускает | `Tap_Checked_Keeps_Checked` |
| `disabled-blocks-change` | отключенное состояние не интерактивно | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | выбранное состояние потребляет токены основного цвета | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | невыбранное состояние потребляет токен границы | `Unchecked_Uses_Border_Token` |
| `small-size-uses-small-token` | малый размер потребляет small-токен размера | `Small_Size_Uses_Small_Token` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
