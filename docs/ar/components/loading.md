# تحميل Loading

مؤشر تحميل دائري دوّار. يدعم الحجم واللون الدلالي.

> مصدر العقد: `lh-ui-core/specs/loading.json` ·
> الحالات الذهبية: `lh-ui-core/cases/loading.json` (5 حالات، تُرجمت إلى اختبارات xUnit)

::: tip ملاحظات المنصة
يتولى `ProgressRing` الخاص بـ WinUI رسم الدوران كحركة منصة (نهج بسيط: تُرسل الهندسة فقط وتُكمل المنصة الحركة) —
وتحدد طبقة الرموز القطر وعرض الحد ولون الحلقة فقط.
:::

## الأحجام

ثلاثة أحجام تُربط برموز القطر `loading.size.*`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أحجام Loading</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--small"></span>
      <span class="sim-loading"></span>
      <span class="sim-loading sim-loading--large"></span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var loading = new LhLoading
{
    Size = LhLoadingSize.Large,
    Tone = LhLoadingTone.Primary,
};
```

## اللون

يحدد `Tone` لون الحلقة، ويقابله واحدًا لواحد رموز الألوان الدلالية.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>ألوان Loading</span></div>
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
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `Size` | قطر المؤشر، يُربط برموز `loading.size.*` | `LhLoadingSize` | `Medium` |
| `Tone` | لون الحلقة، يأخذ رمز اللون الدلالي المقابل | `LhLoadingTone` | `Primary` |

### LhLoadingSize

| القيمة | رمز القطر |
| --- | --- |
| `Small` | `loading.size.small` (20epx) |
| `Medium` | `loading.size.medium` (28epx) |
| `Large` | `loading.size.large` (36epx) |

### LhLoadingTone

| القيمة | رمز لون الحلقة |
| --- | --- |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `loading.size.{small,medium,large}` | ثلاثة أقطار |
| `loading.strokeWidth` | عرض حد حلقة التحميل |
| `color.{primary,success,warning,danger,info}.default` | خمسة ألوان للحلقة |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/loading.json` الخمس لاختبارات xUnit (`tests/LhLoadingTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `medium-size-uses-medium-token` | الحجم المتوسط يستهلك رمز القطر medium | `Medium_Size_Uses_Medium_Token` |
| `large-size-uses-large-token` | الحجم الكبير يستهلك رمز القطر large | `Large_Size_Uses_Large_Token` |
| `primary-tone-uses-primary-token` | اللون الرئيسي يأخذ رمز اللون الرئيسي | `Primary_Tone_Uses_Primary_Token` |
| `success-tone-uses-success-token` | لون النجاح يأخذ رمز لون النجاح | `Success_Tone_Uses_Success_Token` |
| `render-emits-ring-command` | أمر RING واحد فقط | `Render_Emits_Ring_Command` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
