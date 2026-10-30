# Диалог

Модальный диалог. Затемнение + центрированная карточка содержимого + кнопки подтверждения/отмены; поддерживает семантические типы и закрытие по затемнению.

> Источник контракта: `lh-ui-core/specs/dialog.json` ·
> Золотые сценарии: `lh-ui-core/cases/dialog.json` (8 шт., переведены в тесты xUnit)

## Базовое использование

Декларативная видимость: при `Visible` равном false не отрисовывается ничего; после подтверждения/отмены запускается `Close`,
и хост переводит `Visible` в false.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog: база</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">Подтверждение удаления</div>
      <div class="sim-dialog__content">После удаления восстановление невозможно. Подтвердить удаление этой записи?</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">Отмена</button>
        <button class="sim-btn sim-btn--danger">Подтвердить</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

```csharp
var dialog = new LhDialog
{
    Title = "Подтверждение удаления",
    Content = "После удаления восстановление невозможно. Подтвердить удаление этой записи?",
    Type = LhDialogType.Danger,
    ConfirmText = "Подтвердить",
    CancelText = "Отмена",
};
dialog.Confirm += () => { /* выполнить удаление */ };
dialog.Close += () => dialog.Visible = false;

dialog.Visible = true;
```

## Семантический тип

`Type` определяет тип (цвет) кнопки подтверждения; кнопка отмены всегда имеет тип default.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Типы Dialog</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">Подсказка</div>
      <div class="sim-dialog__content">Это диалог подтверждения основного цвета</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">Отмена</button>
        <button class="sim-btn sim-btn--primary">Подтвердить</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML-превью симуляции (внешний вид Windows / WinUI 3), не рендеринг на устройстве</p>

## API

### Свойства

| Свойство | Описание | Тип | По умолчанию |
| --- | --- | --- | --- |
| `Confirm` | Запускается при подтверждении | `event Action` | — |
| `Cancel` | Запускается при отмене | `event Action` | — |
| `Close` | Запускается после подтверждения/отмены или (при closable) при нажатии на затемнение; хост переводит Visible в false | `event Action` | — |
| `Visible` | Показывать ли; при false не отрисовывается ничего | `bool` | `false` |
| `Title` | Заголовок | `string` | `""` |
| `Content` | Содержимое основного текста | `string` | `""` |
| `Type` | Семантический тип; определяет тип (цвет) кнопки подтверждения | `LhDialogType` | `Default` |
| `ConfirmText` | Текст кнопки подтверждения; при пустом значении кнопка не показывается | `string` | `"确认"` |
| `CancelText` | Текст кнопки отмены; при пустом значении кнопка не показывается | `string` | `"取消"` |
| `Closable` | Запускать ли Close при нажатии на область затемнения | `bool` | `false` |

### LhDialogType

| Значение | Тип кнопки подтверждения |
| --- | --- |
| `Default` | `LhButtonType.Default` |
| `Primary` | `LhButtonType.Primary` |
| `Success` | `LhButtonType.Success` |
| `Warning` | `LhButtonType.Warning` |
| `Danger` | `LhButtonType.Danger` |
| `Info` | `LhButtonType.Info` |

## Справочник токенов

| Токен | Назначение |
| --- | --- |
| `dialog.width` / `dialog.borderRadius` | Ширина карточки содержимого и радиус скругления |
| `dialog.paddingHorizontal` / `paddingVertical` | Внутренние отступы карточки содержимого |
| `dialog.titleFontSize` / `contentFontSize` | Размеры шрифта заголовка и основного текста |
| `dialog.gap` | Расстояния между заголовком/содержимым/кнопками |
| `color.background.default` | Фон карточки содержимого |
| `color.background.overlay` | Цвет затемнения |
| `color.text.primary` / `color.text.regular` | Цвета заголовка/основного текста |
| `zIndex.modal` / `zIndex.modalBackdrop` | Уровни карточки/затемнения |
| `shadow.base` | Тень карточки содержимого |

## Справочник золотых сценариев

Соответствие 8 утверждений из `cases/dialog.json` тестам xUnit (`tests/LhDialogTests.cs`):

| case id | Утверждение | Локальный тест |
| --- | --- | --- |
| `confirm-emits-onconfirm` | подтверждение запускает onConfirm | `Confirm_Emits_OnConfirm` |
| `cancel-emits-oncancel` | отмена запускает onCancel | `Cancel_Emits_OnCancel` |
| `confirm-also-emits-onclose` | подтверждение также запускает onClose | `Confirm_Also_Emits_OnClose` |
| `closable-overlay-emits-onclose` | при closable нажатие на затемнение запускает onClose | `Closable_Overlay_Emits_OnClose` |
| `not-closable-blocks-overlay-close` | при отсутствии closable затемнение не закрывает диалог | `Not_Closable_Blocks_Overlay_Close` |
| `hidden-not-rendered` | при скрытии не отрисовывается ничего | `Hidden_Not_Rendered` |
| `title-renders-text` | заголовок и основной текст рендерятся с токенами размера шрифта | `Title_Renders_Text` |
| `danger-type-uses-danger-token` | тип danger потребляет токен опасности | `Danger_Type_Uses_Danger_Token` |

Кроме того, есть специализированные тесты тёмной темы: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
Запуск: `cd lh-ui-windows && dotnet test`.
