# Переключатель

Переключатель. Капсульная дорожка + круглый ползунок, нажатие переключает; поддерживает отключение и состояние загрузки.

> Источник контракта: `lh-ui-core/specs/switch.json` ·
> Золотые сценарии: `lh-ui-core/cases/switch.json` (6 шт., переведены в тесты xUnit)

## Базовое использование

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch: база</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };
```

## Размер

Три размера отображаются на токены `switch.width` / `switch.height`; диаметр ползунка = высота - 2 × `switch.gap`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Размеры Switch</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--small sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--large sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Состояние

Ни `Disabled`, ни `Loading` не реагируют на нажатия; в состоянии `Loading` на месте ползунка отрисовывается кольцо загрузки основного цвета.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Состояния Switch</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--checked sim-switch--disabled"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--loading"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `Change` | Callback изменения состояния переключателя; не запускается при disabled или loading | `event Action<bool>` | — |
| `Checked` | Включён ли переключатель | `bool` | `false` |
| `Disabled` | Отключён ли переключатель (не реагирует на нажатия и даёт отключенный стиль) | `bool` | `false` |
| `Loading` | Находится ли в состоянии загрузки (на месте ползунка показывается кольцо загрузки, нажатия не воспринимаются) | `bool` | `false` |
| `Size` | Размер переключателя; отображается на токены `switch.width` / `switch.height` | `LhSwitchSize` | `Medium` |

### LhSwitchSize

| Значение | width | height |
| --- | --- | --- |
| `Small` | `switch.width.small` (40epx) | `switch.height.small` (22epx) |
| `Medium` | `switch.width.medium` (48epx) | `switch.height.medium` (26epx) |
| `Large` | `switch.width.large` (56epx) | `switch.height.large` (30epx) |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `switch.width.{small,medium,large}` | Три значения ширины дорожки |
| `switch.height.{small,medium,large}` | Три значения высоты дорожки |
| `switch.gap` | Зазор между ползунком и дорожкой (диаметр ползунка = высота - 2 × gap) |
| `color.primary.default` | Цвет включённой дорожки |
| `color.border.base` | Цвет выключенной дорожки |
| `color.onPrimary` | Цвет ползунка |
| `color.disabled.bg` / `color.text.disabled` | Цвет дорожки/ползунка в отключенном состоянии |

## Справочник золотых сценариев

Соответствие 6 утверждений из `cases/switch.json` тестам xUnit (`tests/LhSwitchTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | нажатие переключает и запускает onChange | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-turns-off` | нажатие на включённый выключает | `Tap_Checked_Turns_Off` |
| `disabled-blocks-change` | отключенное состояние не интерактивно | `Disabled_Blocks_Change` |
| `loading-blocks-change` | состояние загрузки не интерактивно | `Loading_Blocks_Change` |
| `checked-uses-primary-token` | включённое состояние потребляет токен основного цвета | `Checked_Uses_Primary_Token` |
| `unchecked-uses-border-token` | выключенное состояние потребляет токен границы | `Unchecked_Uses_Border_Token` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
