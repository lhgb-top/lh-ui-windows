# Темы и тёмный режим

## Модель токенов

Единый источник токенов нативного направления — `lh-ui-core/tokens/*.json`; codegen генерирует Windows-артефакты (`src/Tokens/`; генерируемое содержимое gitignore'ится, синхронизируется через `pnpm link:tokens`):

| Артефакт | Содержимое | Тёмная форма |
| --- | --- | --- |
| `LhTokens` | Полный набор светлой темы: размеры Button, палитра Color, Motion, Radius, Shadow, Spacing, Typography, ZIndex | — |
| `LhDarkTokens` | Только подкатегория `Color` (модель **параллельных объектов**) | Тёмная тема переопределяет только цвет; размеры, анимация, скругления в тёмной теме не меняются |
| `LhShadow` | Носитель тени (color/radius/offsetX/offsetY/spread) | — |
| `LhEasing` | Анимационные easing'и | — |

> Это согласуется с android / ios / harmony (параллельные объекты) и отличается от формы на стороне docs (поверхностное слияние JSON).
> Слой тем на этом спроектирован так: запись `LhColors` несёт только цвета, а размеры компоненты берут напрямую из `LhTokens`.

## LhColors

`LhColorsFactory.Light()` / `Dark()` разворачивают два сгенерированных статических класса в одну неизменяемую запись:

```csharp
public sealed record LhColors
{
    public Color Primary { get; init; }
    public Color OnPrimary { get; init; }
    // ... все семантические цвета
}

public static class LhColorsFactory
{
    public static LhColors Light() => new()
    {
        Primary = LhTokens.Color.Primary.Default,
        // ...
    };

    public static LhColors Dark() => new()
    {
        Primary = LhDarkTokens.Color.Primary.Default,
        // ...
    };
}
```

Фабричные методы открыты: xUnit-тесты могут напрямую конструировать палитры для утверждений (см. `Dark_Colors_Come_From_Dark_Tokens` в соответствующих тестах); прикладной слой потребляет их через `LhTheme`.

## LhTheme.Apply

```csharp
LhTheme.Apply(isDark);  // переключает палитру и запускает ThemeChanged
```

Компоненты подписываются на `LhTheme.ThemeChanged` в `Loaded` и отписываются в `Unloaded`, а при получении события вызывают `ApplyAppearance()` для перерасчёта и перерисовки — **горячая перерисовка темы, дерево страницы перестраивать не нужно**. Если Apply ни разу не вызывался, `LhTheme.Current` fallback-ит на светлую палитру.

## Предвычисление тёмной темы (важно)

В направлении web тёмная тема вычисляется динамически через CSS `color-mix()`, а в нативе нет CSS-каскада, поэтому core уже
**предвычислил тёмные выражения до литералов** (константы `Color` в формате `#AARRGGBB`). Следствие (генеральный план, R-n6):

> **При изменении основного цвета пресета предвычисленные тёмные значения не следуют за ним автоматически** — их необходимо
> пересчитать вручную и обновить `lh-ui-core/tokens/color-dark.json`, а также обновить *Базовый стандарт языка дизайна*.

## Специфичные для Windows соглашения

- **Единица размеров — epx**: `LhEpx.Px(token)` помечает числовое значение токена как эффективные пиксели
  (эффективные пиксели WinUI связаны с коэффициентом масштабирования; всё проходит через функцию-маркер,
  голых числовых литералов в компонентах нет).
- **Тип цвета — `Windows.UI.Color`**: все поля `LhColors` имеют тип `Color`, кисти компоненты оборачивают в
  `SolidColorBrush`; `#AARRGGBB` codegen выводит напрямую как `Color.FromArgb(...)`.
- **Насыщенность шрифта требует отображения**: `typography.fontWeight.*` в токенах — числа, а `FontWeights` в WinUI —
  предопределённые константы; всё проходит через `LhFontWeight.FromToken(token)` (`Common/LhFontWeight.cs`).
- **Тени через Elevation**: карточка содержимого Dialog / панель Picker выражают уровень через `Border.Elevation` +
  `ThemeShadow` (язык Fluent: кнопки плоские, всплывающие слои с тенью).
- **Фокус-визуал**: фокус поля ввода обозначается границей цвета `focus` + `input.focusRingWidth`
  (соответствует высокозаметной рамке фокуса FocusVisualKind в WinUI).

## Справочник токенов

Полная таблица токенов (светлая/тёмная/описание) находится в репозитории core: `dist/docs/tokens.md` (генерируется после `pnpm codegen`).
Отображение токенов на уровне компонентов см. в [справочнике токенов Button](/ru/components/button#справочник-токенов).
