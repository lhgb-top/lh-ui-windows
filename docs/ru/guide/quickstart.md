# Быстрый старт

## Минимальный пример

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui;
using Top.Lhgb.Ui.Common;

public sealed class DemoPage : Page
{
    public DemoPage()
    {
        // применяем светлую палитру; для тёмной передайте true. Все Lh-компоненты
        // следуют автоматически (горячая перерисовка через LhTheme.ThemeChanged)
        LhTheme.Apply(false);

        var button = new LhButton
        {
            Type = LhButtonType.Primary,
            Text = "Основная",
        };
        button.Click += OnButtonClick;

        Content = new StackPanel { Children = { button } };
    }

    private void OnButtonClick() => { /* обработка нажатия */ }
}
```

`LhTheme.Apply(isDark)` переключает статическую палитру и запускает `ThemeChanged`, перерисовывая все уже загруженные Lh-компоненты; если вызова не было, используется светлая палитра.

## Тип и размер

```csharp
// шесть типов
new LhButton { Text = "По умолчанию" };                               // Default
new LhButton { Type = LhButtonType.Primary,   Text = "Основная" };
new LhButton { Type = LhButtonType.Success,   Text = "Успех" };
new LhButton { Type = LhButtonType.Warning,   Text = "Предупреждение" };
new LhButton { Type = LhButtonType.Danger,    Text = "Опасность" };
new LhButton { Type = LhButtonType.Info,      Text = "Информация" };

// три размера
new LhButton { Size = LhButtonSize.Small,  Text = "Маленькая" };
new LhButton { Size = LhButtonSize.Medium, Text = "Средняя" };   // по умолчанию
new LhButton { Size = LhButtonSize.Large,  Text = "Большая" };
```

## Состояние

```csharp
// отключение: не реагирует на нажатия (перехватывается гейтом Tapped), отключенный стиль
new LhButton { Disabled = true, Text = "Отключена" };

// загрузка: показывает ProgressRing, не запускает Click
new LhButton { Loading = true, Text = "Загрузка" };

// оба состояния: отключение имеет приоритет (золотой сценарий disabled-precedes-loading)
new LhButton { Disabled = true, Loading = true, Text = "Отключение приоритетнее" };
```

## Значки и узорный фон

```csharp
// имя значка разрешается локальной системой значков (LhButtonTokens.IconFor; сейчас
// поддерживаются search / refresh / check / close, незарегистрированные имена не отрисовываются)
new LhButton { Type = LhButtonType.Primary, Icon = "search", Text = "Поиск" };

// узорный китайский фон (потребляет токен color.decoration)
new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = "Узорный фон" };
```

## Светлая и тёмная темы

```csharp
LhTheme.Apply(true);  // глобально переключает тёмную тему; все компоненты следуют
                      // автоматически при следующем расчёте внешнего вида
```

## Компоненты форм и обратной связи

```csharp
// контролируемый ввод: хост слушает ValueChange и записывает значение обратно
var input = new LhInput { Placeholder = "Введите ключевое слово" };
input.ValueChange += value => input.Value = value;

// флажок
var checkbox = new LhCheckbox { Label = "青花瓷 (сине-белый фарфор)" };
checkbox.Change += isChecked => { };

// переключатель
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };

// императивное короткое уведомление: сначала размещается хост-элемент,
// затем им управляют через контроллер
var toast = new LhToast();
toast.Close += () => { };
var panel = new StackPanel { Children = { toast } };
toast.Show("Сохранено");   // по истечении скрывается автоматически и запускает Close
```

## Предпросмотр примеров компонентов

Нативный UI не может рендериться в браузере в реальном времени, поэтому этот сайт показывает внешний вид через **встроенный статический HTML-симулятор в рамке телефона**:

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>lh-ui-windows</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">По умолчанию</button>
      <button class="sim-btn sim-btn--primary">Основная</button>
      <button class="sim-btn sim-btn--success">Успех</button>
      <button class="sim-btn sim-btn--warning">Предупреждение</button>
      <button class="sim-btn sim-btn--danger">Опасность</button>
      <button class="sim-btn sim-btn--info">Информация</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">Маленькая</button>
      <button class="sim-btn sim-btn--primary sim-btn--large">Большая</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">Отключена</button>
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>Загрузка
      </button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>Поиск</button>
      <button class="sim-btn sim-btn--primary sim-btn--pattern">Узорный фон</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве; значения стилей взяты из снимка токенов core, общего с устройством источника и значений. Внешний вид на устройстве определяется демо-приложением и снимками экранов устройства.</p>

> Тёмная тема симулятора связана с сайтом: переключите тему в правом верхнем углу, и цвета в рамке телефона изменятся (тёмные токены предвычислены в core).
