# Toast (تنبيه)

عبارة حبرية الشكل تدير رؤيتها وتوقيتها بنفسها. تدعم الأنواع الدلالية والإخفاء التلقائي والتموضع.

> مصدر العقد: `lh-ui-core/specs/toast.json` ·
> الحالات الذهبية: `lh-ui-core/cases/toast.json` (6 حالات، تُرجمت إلى اختبارات xUnit)

## الاستخدام الأساسي

تتحول الواجهة الحتمية (show / hide / tick في العقد) على جانب المنصة إلى دوال وتوقيت داخلي:
تجعل `Show(text)` العبارة ظاهرة وتعيد ضبط التوقيت، بينما تخفيها `Hide()` فورًا دون إطلاق `Close`.
يحمل التوقيت `DispatcherTimer`، وعند انتهائه تُخفى العبارة تلقائيًا ويُطلق `Close`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أساسيات Toast</span></div>
  <div class="sim-screen">
    <div class="sim-toast">تم الحفظ</div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var controller = new LhToastController();
var toast = new LhToast
{
    Duration = 2000,
    Position = LhToastPosition.Center,
    Controller = controller,
};
toast.Close += () => { /* يُطلق بعد الإخفاء التلقائي عند انتهاء التوقيت */ };

// يجب على المضيف تركيب toast كآخر عنصر فرعي في Grid للصفحة (المستوى zIndex.toast)
controller.Show("تم الحفظ");
```

## النوع الدلالي

عندما يكون `Type` غير default يُرسم أمام النص نقطة باللون الدلالي المقابل (قطرها يساوي حجم الخط).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أنواع Toast</span></div>
  <div class="sim-screen">
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--success"></span>تم الحفظ بنجاح</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--warning"></span>يرجى فحص اتصال الشبكة</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--danger"></span>فشل الحذف</div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## التموضع

يحدد `Position` موضع العبارة داخل المنطقة المتاحة؛ ويُترك هامش `spacing.xl` عند الطرفين العلوي والسفلي (وعند center لا يُترك هامش).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>تموضع Toast</span></div>
  <div class="sim-screen">
    <div class="sim-toast sim-toast--top">تنبيه علوي</div>
    <div class="sim-toast sim-toast--bottom">تنبيه سفلي</div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `Close` | يُطلق عند الإخفاء التلقائي بعد انتهاء التوقيت؛ لا يُطلق عند الإخفاء اليدوي بـ Hide() | `event Action` | — |
| `Type` | النوع الدلالي؛ عندما يكون غير default يُرسم أمام النص نقطة باللون الدلالي المقابل | `LhToastType` | `Default` |
| `Duration` | مدة الإخفاء التلقائي (بالمللي ثانية) | `int` | `2000` |
| `Position` | التموضع داخل المنطقة المتاحة | `LhToastPosition` | `Center` |
| `Controller` | مقبض استدعاء المضيف (اختياري؛ يسجل Show/Hide بعد Loaded) | `LhToastController?` | `null` |

### الدوال

| الدالة | الوصف |
| --- | --- |
| `Show(string text)` | يعرض نص التنبيه ويعيد ضبط توقيت الإخفاء التلقائي |
| `Hide()` | يخفي فورًا لكنه لا يطلق Close |

### LhToastType

| القيمة | لون النقطة |
| --- | --- |
| `Default` | لا تُرسم نقطة |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

### LhToastPosition

| القيمة | التموضع |
| --- | --- |
| `Top` | أعلى (هامش `spacing.xl` عند الطرف العلوي) |
| `Center` | الوسط (بدون هامش) |
| `Bottom` | أسفل (هامش `spacing.xl` عند الطرف السفلي) |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `toast.fontSize` | حجم خط النص (وهو أيضًا قطر نقطة اللون) |
| `toast.paddingHorizontal` / `paddingVertical` | الحشو الداخلي للكبسولة |
| `toast.borderRadius` | نصف قطر زاوية الكبسولة |
| `toast.gap` | المسافة بين نقطة اللون والنص |
| `color.ink` | لون خلفية الكبسولة |
| `color.onPrimary` | لون النص |
| `color.{success,warning,danger,info}.default` | نقاط الألوان الدلالية |
| `zIndex.toast` | مستوى العبارة العائمة |
| `spacing.xl` | الهامشان العلوي والسفلي |
| `typography.fontFamily` | حزمة الخطوط المذيّلة ذات النمط الصيني |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/toast.json` الست لاختبارات xUnit (`tests/LhToastTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `show-makes-visible` | بعد show تصبح ظاهرة ويُعرض النص | `Show_Makes_Visible` |
| `auto-hides-after-duration` | انتهاء التوقيت يخفيها تلقائيًا ويطلق onClose | `Auto_Hides_After_Duration` |
| `tick-before-duration-keeps-visible` | قبل انتهاء التوقيت تبقى ظاهرة | `Tick_Before_Duration_Keeps_Visible` |
| `hide-is-silent` | الإخفاء اليدوي بـ hide لا يطلق onClose | `Hide_Is_Silent` |
| `success-type-uses-success-token` | النوع success يستهلك رموز اللون الدلالي ولون الحبر | `Success_Type_Uses_Success_Token` |
| `hidden-emits-no-commands` | عند عدم الرؤية لا تُصدر أي أوامر رسم | `Hidden_Emits_No_Commands` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
