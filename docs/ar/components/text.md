# نص Text

مكوّن النص. يعرض فقرة نص واحدة وفق المستوى ووزن الخط واللون والمحاذاة، وتأتي جميع قيم التنضيد من رموز typography.

> مصدر العقد: `lh-ui-core/specs/text.json` ·
> الحالات الذهبية: `lh-ui-core/cases/text.json` (5 حالات، تُرجمت إلى اختبارات xUnit)

## المستويات

ستة مستويات تُربط برموز `typography.fontSize.*`؛ والمستوى الافتراضي هو `base`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>مستويات Text</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--extra-large">الخزف الأزرق extraLarge</div>
    <div class="sim-text sim-text--large">الخزف الأزرق large</div>
    <div class="sim-text sim-text--medium">الخزف الأزرق medium</div>
    <div class="sim-text">الخزف الأزرق base</div>
    <div class="sim-text sim-text--small">الخزف الأزرق small</div>
    <div class="sim-text sim-text--extra-small">الخزف الأزرق extraSmall</div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var text = new LhText
{
    Text = "الخزف الأزرق",
    Level = LhTextLevel.Large,
};
```

## وزن الخط واللون

يُربط `Weight` برموز `typography.fontWeight.*`؛ ويُربط `Tone` بـ `color.text.*` (يأخذ selected القيمة `color.selected`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>وزن الخط واللون</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--bold">عريض bold</div>
    <div class="sim-text sim-text--secondary">وصف ثانوي secondary</div>
    <div class="sim-text sim-text--placeholder">نص العنصر النائب placeholder</div>
    <div class="sim-text sim-text--selected">حالة التحديد selected</div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## المحاذاة

يحدد `Align`تموضع النص أفقياً ضمن عرض صندوق السطر (وليس رمز تصميم، إذ يُربط بـ `TextAlignment` في WinUI).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>المحاذاة</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--start">محاذاة إلى البداية start</div>
    <div class="sim-text sim-text--center">توسيط center</div>
    <div class="sim-text sim-text--end">محاذاة إلى النهاية end</div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## السمتان الفاتحة والداكنة

بدّل سمة الموقع من الزاوية العلوية يتغير لون النص داخل إطار هاتف المحاكي؛ وعلى الجهاز الحقيقي يتم التبديل عبر `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>نص داكن</span></div>
  <div class="sim-screen">
    <div class="sim-text">نص عادي regular</div>
    <div class="sim-text sim-text--secondary">وصف ثانوي secondary</div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي؛ جميع قيم الفاتح/الداكن مأخوذة من لقطة رموز core</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `Text` | محتوى النص (فتحة default في العقد) | `string` | `""` |
| `Level` | مستوى حجم الخط، يُربط بـ `typography.fontSize.*` | `LhTextLevel` | `Base` |
| `Weight` | وزن الخط، يُربط بـ `typography.fontWeight.*` | `LhTextWeight` | `Primary` |
| `Tone` | لون النص، يُربط بـ `color.text.*` / `color.selected` | `LhTextTone` | `Regular` |
| `Align` | المحاذاة الأفقية (تُربط بـ `TextAlignment`) | `LhTextAlign` | `Start` |

### LhTextLevel

| القيمة | رمز حجم الخط |
| --- | --- |
| `ExtraLarge` | `typography.fontSize.extraLarge` (20epx) |
| `Large` | `typography.fontSize.large` (18epx) |
| `Medium` | `typography.fontSize.medium` (16epx) |
| `Base` | `typography.fontSize.base` (14epx) |
| `Small` | `typography.fontSize.small` (13epx) |
| `ExtraSmall` | `typography.fontSize.extraSmall` (12epx) |

### LhTextWeight

| القيمة | رمز وزن الخط |
| --- | --- |
| `Primary` | `typography.fontWeight.primary` (400، يُطابق على جانب الجهاز إلى Normal) |
| `Bold` | `typography.fontWeight.bold` (600، يُطابق على جانب الجهاز إلى SemiBold) |

### LhTextTone

| القيمة | رمز اللون |
| --- | --- |
| `Primary` | `color.text.primary` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |
| `Placeholder` | `color.text.placeholder` |
| `Disabled` | `color.text.disabled` |
| `Selected` | `color.selected` |

### LhTextAlign

| القيمة | مطابقة WinUI |
| --- | --- |
| `Start` | `TextAlignment.Left` |
| `Center` | `TextAlignment.Center` |
| `End` | `TextAlignment.Right` |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `typography.fontSize.{extraLarge…extraSmall}` | ستة أحجام خط |
| `typography.fontWeight.{primary,bold}` | مستويان لوزن الخط |
| `typography.lineHeight.primary` | ارتفاع السطر (ارتفاع صندوق السطر = حجم الخط × ارتفاع السطر) |
| `typography.fontFamily` | حزمة الخطوط المذيّلة ذات النمط الصيني |
| `color.text.{primary,regular,secondary,placeholder,disabled}` | ألوان محايدة |
| `color.selected` | لون حالة التحديد |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/text.json` الخمس لاختبارات xUnit (`tests/LhTextTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `default-level-uses-base-tokens` | المستوى الافتراضي يستهلك رموز التنضيد base | `Default_Level_Uses_Base_Tokens` |
| `large-level-uses-large-token` | المستوى الكبير يستهلك رمز حجم الخط large | `Large_Level_Uses_Large_Token` |
| `bold-weight-uses-bold-token` | الخط العريض يستهلك رمز وزن الخط bold | `Bold_Weight_Uses_Bold_Token` |
| `secondary-tone-uses-text-secondary` | اللون الثانوي يأخذ رمز لون النص الثانوي | `Secondary_Tone_Uses_Text_Secondary` |
| `render-emits-single-text-command` | أمر TEXT واحد فقط | `Render_Emits_Single_Text_Command` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
