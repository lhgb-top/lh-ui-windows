# أيقونة Icon

مكوّن الأيقونات. يحلّ اسم الأيقونة إلى رمز رسومي وفق نظام الرسوم المحلي، ويدعم الحجم واللون الدلالي.

> مصدر العقد: `lh-ui-core/specs/icon.json` ·
> الحالات الذهبية: `lh-ui-core/cases/icon.json` (5 حالات، تُرجمت إلى اختبارات xUnit)

::: tip ملاحظات المنصة
يستخدم طرف Windows حاليًا رموز يونيكود مؤقتة (`LhIconTokens.GlyphFor`، تُرسم عبر TextBlock)، وينبغي استبدالها في بيئة الإنتاج بـ
`FontIcon` (أصول الرسوم المتجهة Segoe Fluent Icons)؛ وهذا يتسق مع استراتيجية العنصر النائب في button. أسماء الأيقونات المسجلة موجودة في قسم «أسماء الأيقونات» أدناه.
:::

## الأحجام

ثلاثة أحجام تُربط برمز `icon.size`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أحجام Icon</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--small">⌕</span>
      <span class="sim-icon">⌕</span>
      <span class="sim-icon sim-icon--large">⌕</span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var icon = new LhIcon
{
    Name = "search",
    Size = LhIconSize.Large,
};
```

## اللون

يحدد `Tone` لون الرمز الرسومي، ويقابله واحدًا لواحد رموز الألوان الدلالية؛ واللون الافتراضي يأخذ `color.text.primary`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>ألوان Icon</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">✓ افتراضي</span>
      <span class="sim-icon sim-icon--primary">✓ لون رئيسي</span>
      <span class="sim-icon sim-icon--success">✓ نجاح</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--warning">⚠ تحذير</span>
      <span class="sim-icon sim-icon--danger">✕ خطر</span>
      <span class="sim-icon sim-icon--info">ℹ معلومات</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--regular">✓ عادي</span>
      <span class="sim-icon sim-icon--secondary">✓ ثانوي</span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## أسماء الأيقونات

أسماء الأيقونات المسجلة حاليًا في `GlyphFor` ورموزها: `search` ⌕، `refresh` ↻، `check` ✓، `close` ✕،
`warning` ⚠، `info` ℹ، `back` ←، `more` ⋯، `star` ★، `edit` ✎، `delete` ⌫،
`plus` ＋، `minus` －، `arrow` →. الاسم غير المسجل يُستبدل بـ `◆`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أيقونات مسجلة</span></div>
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
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## السمتان الفاتحة والداكنة

بدّل سمة الموقع من الزاوية العلوية تتغير ألوان الأيقونات داخل إطار هاتف المحاكي؛ وعلى الجهاز الحقيقي يتم التبديل عبر `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أيقونات داكنة</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--primary">★</span>
      <span class="sim-icon sim-icon--success">✓</span>
      <span class="sim-icon sim-icon--warning">⚠</span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي؛ جميع قيم الفاتح/الداكن مأخوذة من لقطة رموز core</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `Name` | اسم الأيقونة (يُحلّ عبر جدول الأيقونات المحلي، وغير المسجل يُستبدل بـ `◆`) | `string` | `""` |
| `Size` | حجم الأيقونة، يُربط برموز `icon.size.*` | `LhIconSize` | `Medium` |
| `Tone` | لون الأيقونة، يُربط برموز الألوان الدلالية / ألوان النص | `LhIconTone` | `Regular` |

### LhIconSize

| القيمة | رمز الحجم |
| --- | --- |
| `Small` | `icon.size.small` (16epx) |
| `Medium` | `icon.size.medium` (18epx) |
| `Large` | `icon.size.large` (20epx) |

### LhIconTone

| القيمة | رمز اللون |
| --- | --- |
| `Default` | `color.text.primary` |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `icon.size.{small,medium,large}` | ثلاثة أحجام |
| `color.text.{primary,regular,secondary}` | ألوان محايدة |
| `color.{primary,success,warning,danger,info}.default` | ألوان دلالية |
| `typography.fontFamily` | حزمة خطوط الرموز الرسومية |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/icon.json` الخمس لاختبارات xUnit (`tests/LhIconTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `click-emits-ononclick` | تسمح البوابة بالمرور (الأيقونة قابلة للتفاعل) | `Click_Emits_OnClick` |
| `default-tone-uses-text-primary` | اللون الافتراضي يأخذ رمز لون النص الرئيسي | `Default_Tone_Uses_Text_Primary` |
| `primary-tone-uses-primary-token` | اللون الرئيسي يأخذ رمز اللون الرئيسي | `Primary_Tone_Uses_Primary_Token` |
| `small-size-uses-small-token` | الحجم الصغير يستهلك رمز الحجم small | `Small_Size_Uses_Small_Token` |
| `render-emits-icon-command` | أمر أيقونة واحد فقط | `Render_Emits_Single_Icon_Command` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
