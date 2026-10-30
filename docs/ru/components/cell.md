# Ячейка

Ячейка строки списка. Слева значок + заголовок (с опциональной красной звездой обязательности) + пояснение, справа значение и стрелка-индикатор, снизу опциональная разделительная линия.

> Источник контракта: `lh-ui-core/specs/cell.json` ·
> Золотые сценарии: `lh-ui-core/cases/cell.json` (6 шт., переведены в тесты xUnit)

## Базовое использование

Заголовок берёт `color.text.primary`, значение и пояснение — `color.text.secondary`; все три цвета токен-ориентированы.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Cell: база</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Заголовок строки</div>
        <div class="sim-cell__label">Поясняющий текст</div>
      </div>
      <div class="sim-cell__value">Содержимое строки</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Ячейка с линией</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var cell = new LhCell
{
    Title = "Заголовок строки",
    Label = "Поясняющий текст",
    Value = "Содержимое строки",
};
```

## Значок и обязательность

`Icon` принимает имя значка из локальной системы значков (см. таблицу глифов в документации Icon) и отрисовывается слева;
если `Required` равно true, перед заголовком рисуется красная звезда цвета `color.danger.default`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Значок и обязательность</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <span class="sim-cell__icon">★</span>
      <div class="sim-cell__main">
        <div class="sim-cell__title">Со значком</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title"><span class="sim-cell__required">✱</span>Обязательное поле</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Разделительная линия и нажатие

Если `Bordered` равно true, снизу проводится разделительная линия `color.border.lighter`; если `Clickable` равно true,
справа отрисовывается стрелка-индикатор, а нажатие запускает событие `Click`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Линия и нажатие</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">Кликабельная</div></div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">Некликабельная</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var cell = new LhCell
{
    Title = "Кликабельная",
    Clickable = true,
    Bordered = true,
};
cell.Click += () => { /* запускается только когда Clickable равно true */ };
```

## Светлая и тёмная темы

Переключите тему в правом верхнем углу сайта — цвет текста и разделительной линии в рамке телефона-симулятора изменится; на устройстве переключение выполняется вызовом `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Тёмная ячейка</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">Заголовок строки</div>
        <div class="sim-cell__label">Поясняющий текст</div>
      </div>
      <div class="sim-cell__value">Содержимое строки</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве; значения для светлой/тёмной темы взяты из снимка токенов core</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `Click` | Событие нажатия; запускается только если `Clickable` равно true | `event Action` | — |
| `Title` | Заголовок (слот title в spec) | `string` | `""` |
| `Label` | Поясняющий текст под заголовком (слот label) | `string` | `""` |
| `Value` | Текст значения справа (слот value) | `string` | `""` |
| `Icon` | Имя значка слева (разрешается локальной таблицей значков) | `string?` | `null` |
| `Required` | Рисовать ли красную звезду обязательности перед заголовком | `bool` | `false` |
| `Clickable` | Кликабельна ли ячейка (показ/скрытие стрелки, гейт Click) | `bool` | `false` |
| `Bordered` | Рисовать ли разделительную линию снизу | `bool` | `true` |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `cell.paddingHorizontal` / `cell.paddingVertical` | Внутренние отступы |
| `cell.gap` | Вертикальное расстояние между заголовком и пояснением |
| `cell.fontSizeTitle` / `fontSizeLabel` / `fontSizeValue` | Три размера шрифта |
| `color.text.{primary,secondary,regular}` | Цвета заголовка/пояснения/значения/значка |
| `color.danger.default` | Красная звезда обязательности |
| `color.border.lighter` | Разделительная линия снизу |
| `icon.size.medium` | Размер левого значка |
| `spacing.{sm,xs}` | Отступ значка / отступы между внутренними элементами |
| `typography.lineHeight.primary` | Межстрочный интервал текста |

## Справочник золотых сценариев

Соответствие 6 утверждений из `cases/cell.json` тестам xUnit (`tests/LhCellTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `title-and-value-render` | заголовок и значение отрендерены | `Title_And_Value_Render` |
| `label-renders-secondary-tone` | поясняющий текст берёт токен второстепенного цвета текста | `Label_Renders_Secondary_Tone` |
| `required-uses-danger-token` | красная звезда обязательности потребляет токен опасности | `Required_Uses_Danger_Token` |
| `bordered-emits-line-command` | вариант с линией испускает команду LINE | `Bordered_Emits_Line_Command` |
| `clickable-emits-onclick` | при кликабельности нажатие запускает onClick | `Clickable_Emits_OnClick` |
| `not-clickable-no-onclick` | при некликабельности onClick не запускается | `Not_Clickable_No_OnClick` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
