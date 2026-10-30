# Загрузка

Кольцевой вращающийся индикатор загрузки. Поддерживает размер и семантический тон.

> Источник контракта: `lh-ui-core/specs/loading.json` ·
> Золотые сценарии: `lh-ui-core/cases/loading.json` (5 шт., переведены в тесты xUnit)

::: tip Заметки о платформе
Вращение обеспечивается платформенной анимацией `ProgressRing` из WinUI (сдержанная подача: передаётся только геометрия, анимацию дополняет платформа) —
слой токенов определяет лишь диаметр, толщину границы и цвет кольца.
:::

## Размер

Три размера отображаются на токены диаметра `loading.size.*`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Размеры Loading</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--small"></span>
      <span class="sim-loading"></span>
      <span class="sim-loading sim-loading--large"></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var loading = new LhLoading
{
    Size = LhLoadingSize.Large,
    Tone = LhLoadingTone.Primary,
};
```

## Тон

`Tone` определяет цвет кольца и однозначно соответствует семантическим токенам цвета.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Тон Loading</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--primary"></span>
      <span class="sim-loading sim-loading--success"></span>
      <span class="sim-loading sim-loading--warning"></span>
    </div>
    <div class="sim-row">
      <span class="sim-loading sim-loading--danger"></span>
      <span class="sim-loading sim-loading--info"></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `Size` | Диаметр индикатора; отображается на токены `loading.size.*` | `LhLoadingSize` | `Medium` |
| `Tone` | Цвет кольца; берёт соответствующий семантический токен цвета | `LhLoadingTone` | `Primary` |

### LhLoadingSize

| Значение | Токен диаметра |
| --- | --- |
| `Small` | `loading.size.small` (20epx) |
| `Medium` | `loading.size.medium` (28epx) |
| `Large` | `loading.size.large` (36epx) |

### LhLoadingTone

| Значение | Токен цвета кольца |
| --- | --- |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `loading.size.{small,medium,large}` | Три значения диаметра |
| `loading.strokeWidth` | Толщина границы кольца загрузки |
| `color.{primary,success,warning,danger,info}.default` | Пять цветов кольца |

## Справочник золотых сценариев

Соответствие 5 утверждений из `cases/loading.json` тестам xUnit (`tests/LhLoadingTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `medium-size-uses-medium-token` | средний размер потребляет medium-токен диаметра | `Medium_Size_Uses_Medium_Token` |
| `large-size-uses-large-token` | большой размер потребляет large-токен диаметра | `Large_Size_Uses_Large_Token` |
| `primary-tone-uses-primary-token` | основной тон берёт токен основного цвета | `Primary_Tone_Uses_Primary_Token` |
| `success-tone-uses-success-token` | тон успеха берёт токен цвета успеха | `Success_Tone_Uses_Success_Token` |
| `render-emits-ring-command` | ровно одна команда RING | `Render_Emits_Ring_Command` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
