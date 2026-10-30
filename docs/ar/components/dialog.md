# مربع حوار Dialog

مربع حوار مشروط. قناع + بطاقة محتوى متمركزة + زرا تأكيد وإلغاء، ويدعم الأنواع الدلالية والإغلاق عبر القناع.

> مصدر العقد: `lh-ui-core/specs/dialog.json` ·
> الحالات الذهبية: `lh-ui-core/cases/dialog.json` (8 حالات، تُرجمت إلى اختبارات xUnit)

## الاستخدام الأساسي

رؤية تصريحية: عندما تكون `Visible` تساوي false لا يُعرض أي محتوى؛ وبعد التأكيد/الإلغاء يُطلق `Close`،
فيضبط المضيف `Visible` إلى false بناءً عليه.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أساسيات Dialog</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">تأكيد الحذف</div>
      <div class="sim-dialog__content">لا يمكن التراجع بعد الحذف، هل تؤكد حذف هذا السجل؟</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">إلغاء</button>
        <button class="sim-btn sim-btn--danger">تأكيد</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var dialog = new LhDialog
{
    Title = "تأكيد الحذف",
    Content = "لا يمكن التراجع بعد الحذف، هل تؤكد حذف هذا السجل؟",
    Type = LhDialogType.Danger,
    ConfirmText = "تأكيد",
    CancelText = "إلغاء",
};
dialog.Confirm += () => { /* تنفيذ الحذف */ };
dialog.Close += () => dialog.Visible = false;

dialog.Visible = true;
```

## النوع الدلالي

يحدد `Type` نوع زر التأكيد (لونه)؛ بينما يبقى زر الإلغاء دائمًا من النوع default.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أنواع Dialog</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">تنبيه</div>
      <div class="sim-dialog__content">هذا مربع حوار تأكيد باللون الرئيسي</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">إلغاء</button>
        <button class="sim-btn sim-btn--primary">تأكيد</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `Confirm` | يُطلق عند التأكيد | `event Action` | — |
| `Cancel` | يُطلق عند الإلغاء | `event Action` | — |
| `Close` | يُطلق بعد التأكيد/الإلغاء أو (عند closable) عند النقر على القناع، فيضبط المضيف بناءً عليه Visible إلى false | `event Action` | — |
| `Visible` | ما إذا كان ظاهرًا؛ عندما يكون false لا يُعرض أي محتوى | `bool` | `false` |
| `Title` | العنوان | `string` | `""` |
| `Content` | المحتوى الرئيسي | `string` | `""` |
| `Type` | النوع الدلالي، يحدد نوع زر التأكيد (لونه) | `LhDialogType` | `Default` |
| `ConfirmText` | نص زر التأكيد؛ عندما يكون فارغًا لا يُعرض زر التأكيد | `string` | `"确认"` |
| `CancelText` | نص زر الإلغاء؛ عندما يكون فارغًا لا يُعرض زر الإلغاء | `string` | `"取消"` |
| `Closable` | ما إذا كان النقر على منطقة القناع يطلق Close | `bool` | `false` |

### LhDialogType

| القيمة | نوع زر التأكيد |
| --- | --- |
| `Default` | `LhButtonType.Default` |
| `Primary` | `LhButtonType.Primary` |
| `Success` | `LhButtonType.Success` |
| `Warning` | `LhButtonType.Warning` |
| `Danger` | `LhButtonType.Danger` |
| `Info` | `LhButtonType.Info` |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `dialog.width` / `dialog.borderRadius` | عرض بطاقة المحتوى ونصف قطر زاويتها |
| `dialog.paddingHorizontal` / `paddingVertical` | الحشو الداخلي لبطاقة المحتوى |
| `dialog.titleFontSize` / `contentFontSize` | حجم خط العنوان والمحتوى |
| `dialog.gap` | المسافة بين العنوان/المحتوى/منطقة الأزرار |
| `color.background.default` | لون خلفية بطاقة المحتوى |
| `color.background.overlay` | لون القناع |
| `color.text.primary` / `color.text.regular` | لون العنوان/المحتوى |
| `zIndex.modal` / `zIndex.modalBackdrop` | مستوى بطاقة المحتوى/القناع |
| `shadow.base` | ظل بطاقة المحتوى |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/dialog.json` الثمانية لاختبارات xUnit (`tests/LhDialogTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `confirm-emits-onconfirm` | التأكيد يطلق onConfirm | `Confirm_Emits_OnConfirm` |
| `cancel-emits-oncancel` | الإلغاء يطلق onCancel | `Cancel_Emits_OnCancel` |
| `confirm-also-emits-onclose` | التأكيد يطلق onClose أيضًا | `Confirm_Also_Emits_OnClose` |
| `closable-overlay-emits-onclose` | عند closable يطلق النقر على القناع onClose | `Closable_Overlay_Emits_OnClose` |
| `not-closable-blocks-overlay-close` | عند عدم closable لا يُغلق القناع | `Not_Closable_Blocks_Overlay_Close` |
| `hidden-not-rendered` | عند عدم الرؤية لا يُعرض أي محتوى | `Hidden_Not_Rendered` |
| `title-renders-text` | العنوان والمحتوى يُعرضان وفق رموز حجم الخط | `Title_Renders_Text` |
| `danger-type-uses-danger-token` | النوع danger يستهلك رمز لون الخطر | `Danger_Type_Uses_Danger_Token` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
