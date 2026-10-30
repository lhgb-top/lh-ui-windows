# Значок

Компонент значка. Разрешает имя значка в глиф согласно локальной системе графики; поддерживает размер и семантический цвет тона.

> Источник контракта: `lh-ui-core/specs/icon.json` ·
> Золотые сценарии: `lh-ui-core/cases/icon.json` (5 шт., переведены в тесты xUnit)

::: tip Заметки о платформе
На стороне Windows сейчас используется заглушка в виде юникод-глифа (`LhIconTokens.GlyphFor`, рендеринг через TextBlock); на неё следует заменить в рабочем окружении на
`FontIcon` (векторные ассеты Segoe Fluent Icons) — это та же стратегия заглушки, что и у button. Зарегистрированные имена значков см. в разделе «Имена значков» ниже.
:::

## Размер

Три размера отображаются на токены `icon.size`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Размеры Icon</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--small">⌕</span>
      <span class="sim-icon">⌕</span>
      <span class="sim-icon sim-icon--large">⌕</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var icon = new LhIcon
{
    Name = "search",
    Size = LhIconSize.Large,
};
```

## Тон

`Tone` определяет цвет глифа и однозначно соответствует семантическим токенам цвета; тон по умолчанию берёт `color.text.primary`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Тон Icon</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">✓ По умолчанию</span>
      <span class="sim-icon sim-icon--primary">✓ Основной</span>
      <span class="sim-icon sim-icon--success">✓ Успех</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--warning">⚠ Предупреждение</span>
      <span class="sim-icon sim-icon--danger">✕ Опасность</span>
      <span class="sim-icon sim-icon--info">ℹ Информация</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--regular">✓ Обычный</span>
      <span class="sim-icon sim-icon--secondary">✓ Второстепенный</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Имена значков

Имена значков и глифы, зарегистрированные в `GlyphFor` на данный момент: `search` ⌕, `refresh` ↻, `check` ✓, `close` ✕,
`warning` ⚠, `info` ℹ, `back` ←, `more` ⋯, `star` ★, `edit` ✎, `delete` ⌫,
`plus` ＋, `minus` －, `arrow` →. Незарегистрированные имена fallback-ят на `◆`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Зарегистрированные значки</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">⌕</span><span class="sim-icon">↻</span><span class="sim-icon">✓</span>
      <span class="sim-icon">✕</span><span class="sim-icon">⚠</span><span class="sim-icon">ℹ</span>
      <span class="sim-icon">←</span><span class="sim-icon">⋯</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon">★</span><span class="sim-icon">✎</span><span class="sim-icon">⌫</span>
      <span class="sim-icon">＋</span><span class="sim-icon">－</span><span class="sim-icon">→</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## Светлая и тёмная темы

Переключите тему в правом верхнем углу сайта — цвета значков в рамке телефона-симулятора изменятся; на устройстве переключение выполняется вызовом `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Тёмные значки</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--primary">★</span>
      <span class="sim-icon sim-icon--success">✓</span>
      <span class="sim-icon sim-icon--warning">⚠</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве; значения для светлой/тёмной темы взяты из снимка токенов core</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `Name` | Имя значка (разрешается локальной таблицей значков; незарегистрированные имена fallback-ят на `◆`) | `string` | `""` |
| `Size` | Размер значка; отображается на токены `icon.size.*` | `LhIconSize` | `Medium` |
| `Tone` | Тон значка; отображается на семантические токены / токены цвета текста | `LhIconTone` | `Regular` |

### LhIconSize

| Значение | Токен размера |
| --- | --- |
| `Small` | `icon.size.small` (16epx) |
| `Medium` | `icon.size.medium` (18epx) |
| `Large` | `icon.size.large` (20epx) |

### LhIconTone

| Значение | Токен цвета |
| --- | --- |
| `Default` | `color.text.primary` |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `icon.size.{small,medium,large}` | Три размера |
| `color.text.{primary,regular,secondary}` | Нейтральные тона |
| `color.{primary,success,warning,danger,info}.default` | Семантические тона |
| `typography.fontFamily` | Стек шрифтов глифов |

## Справочник золотых сценариев

Соответствие 5 утверждений из `cases/icon.json` тестам xUnit (`tests/LhIconTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `click-emits-onclick` | гейт пропускает (значок интерактивен) | `Click_Emits_OnClick` |
| `default-tone-uses-text-primary` | тон по умолчанию берёт токен основного цвета текста | `Default_Tone_Uses_Text_Primary` |
| `primary-tone-uses-primary-token` | основной тон берёт токен основного цвета | `Primary_Tone_Uses_Primary_Token` |
| `small-size-uses-small-token` | малый размер потребляет small-токен размера | `Small_Size_Uses_Small_Token` |
| `render-emits-icon-command` | ровно одна команда значка | `Render_Emits_Single_Icon_Command` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
