# Текст

Текстовый компонент. Рендерит один фрагмент текста по уровню, насыщенности шрифта, тону и выравниванию; все типографические значения берутся из токенов typography.

> Источник контракта: `lh-ui-core/specs/text.json` ·
> Золотые сценарии: `lh-ui-core/cases/text.json` (5 шт., переведены в тесты xUnit)

## Уровень

Шесть уровней отображаются на токены `typography.fontSize.*`; уровень по умолчанию — `base`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Уровни Text</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--extra-large">青花瓷 (сине-белый фарфор) extraLarge</div>
    <div class="sim-text sim-text--large">青花瓷 (сине-белый фарфор) large</div>
    <div class="sim-text sim-text--medium">青花瓷 (сине-белый фарфор) medium</div>
    <div class="sim-text">青花瓷 (сине-белый фарфор) base</div>
    <div class="sim-text sim-text--small">青花瓷 (сине-белый фарфор) small</div>
    <div class="sim-text sim-text--extra-small">青花瓷 (сине-белый фарфор) extraSmall</div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var text = new LhText
{
    Text = "青花瓷 (сине-белый фарфор)",
    Level = LhTextLevel.Large,
};
```

## Насыщенность шрифта и тон

`Weight` отображается на токены `typography.fontWeight.*`; `Tone` отображается на `color.text.*` (для selected берётся `color.selected`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Насыщенность и тон</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--bold">Полужирный bold</div>
    <div class="sim-text sim-text--secondary">Второстепенное пояснение secondary</div>
    <div class="sim-text sim-text--placeholder">Плейсхолдер placeholder</div>
    <div class="sim-text sim-text--selected">Выбранное состояние selected</div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Выравнивание

`Align` определяет горизонтальное положение текста в пределах ширины строкового блока (не дизайн-токен; отображается на `TextAlignment` из WinUI).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Выравнивание</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--start">По левому краю start</div>
    <div class="sim-text sim-text--center">По центру center</div>
    <div class="sim-text sim-text--end">По правому краю end</div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Светлая и тёмная темы

Переключите тему в правом верхнем углу сайта — цвет текста в рамке телефона-симулятора изменится; на устройстве переключение выполняется вызовом `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Тёмный текст</span></div>
  <div class="sim-screen">
    <div class="sim-text">Обычный текст regular</div>
    <div class="sim-text sim-text--secondary">Второстепенное пояснение secondary</div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве; значения для светлой/тёмной темы взяты из снимка токенов core</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `Text` | Текстовое содержимое (слот default в spec) | `string` | `""` |
| `Level` | Уровень размера шрифта; отображается на `typography.fontSize.*` | `LhTextLevel` | `Base` |
| `Weight` | Насыщенность шрифта; отображается на `typography.fontWeight.*` | `LhTextWeight` | `Primary` |
| `Tone` | Тон текста; отображается на `color.text.*` / `color.selected` | `LhTextTone` | `Regular` |
| `Align` | Горизонтальное выравнивание (отображается на `TextAlignment`) | `LhTextAlign` | `Start` |

### LhTextLevel

| Значение | Токен размера шрифта |
| --- | --- |
| `ExtraLarge` | `typography.fontSize.extraLarge` (20epx) |
| `Large` | `typography.fontSize.large` (18epx) |
| `Medium` | `typography.fontSize.medium` (16epx) |
| `Base` | `typography.fontSize.base` (14epx) |
| `Small` | `typography.fontSize.small` (13epx) |
| `ExtraSmall` | `typography.fontSize.extraSmall` (12epx) |

### LhTextWeight

| Значение | Токен насыщенности шрифта |
| --- | --- |
| `Primary` | `typography.fontWeight.primary` (400; на устройстве отображается на Normal) |
| `Bold` | `typography.fontWeight.bold` (600; на устройстве отображается на SemiBold) |

### LhTextTone

| Значение | Токен цвета |
| --- | --- |
| `Primary` | `color.text.primary` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |
| `Placeholder` | `color.text.placeholder` |
| `Disabled` | `color.text.disabled` |
| `Selected` | `color.selected` |

### LhTextAlign

| Значение | Отображение на WinUI |
| --- | --- |
| `Start` | `TextAlignment.Left` |
| `Center` | `TextAlignment.Center` |
| `End` | `TextAlignment.Right` |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `typography.fontSize.{extraLarge…extraSmall}` | Шесть размеров шрифта |
| `typography.fontWeight.{primary,bold}` | Две насыщенности шрифта |
| `typography.lineHeight.primary` | Межстрочный интервал (высота строки = размер шрифта × межстрочный интервал) |
| `typography.fontFamily` | Китайский стек серифных шрифтов |
| `color.text.{primary,regular,secondary,placeholder,disabled}` | Нейтральные тона |
| `color.selected` | Тон выбранного состояния |

## Справочник золотых сценариев

Соответствие 5 утверждений из `cases/text.json` тестам xUnit (`tests/LhTextTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `default-level-uses-base-tokens` | уровень по умолчанию потребляет базовые типографические токены | `Default_Level_Uses_Base_Tokens` |
| `large-level-uses-large-token` | уровень large потребляет токен размера large | `Large_Level_Uses_Large_Token` |
| `bold-weight-uses-bold-token` | полужирный потребляет токен насыщенности bold | `Bold_Weight_Uses_Bold_Token` |
| `secondary-tone-uses-text-secondary` | второстепенный тон берёт токен второстепенного цвета текста | `Secondary_Tone_Uses_Text_Secondary` |
| `render-emits-single-text-command` | ровно одна команда TEXT | `Render_Emits_Single_Text_Command` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
