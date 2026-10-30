# وسم Tag

وسم يُستخدم للتمييز والاختيار. يدعم ستة أنواع دلالية، وثلاثة أحجام، وحالة الحد، وحالة الزوايا الدائرية، وإمكانية الإغلاق.

> مصدر العقد: `lh-ui-core/specs/tag.json` ·
> الحالات الذهبية: `lh-ui-core/cases/tag.json` (6 حالات، تُرجمت إلى اختبارات xUnit)

## الأنواع

ستة أنواع تحدد لون الخلفية ولون النص ولون الحد.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أنواع Tag</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag">افتراضي</span>
      <span class="sim-tag sim-tag--primary">رئيسي</span>
      <span class="sim-tag sim-tag--success">نجاح</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--warning">تحذير</span>
      <span class="sim-tag sim-tag--danger">خطر</span>
      <span class="sim-tag sim-tag--info">معلومات</span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var tag = new LhTag
{
    Text = "رئيسي",
    Type = LhTagType.Primary,
};
```

## الحد والزوايا الدائرية

عندما تكون `Plain` تساوي true يُتبدّل الشكل إلى خلفية دلالية فاتحة + نص دلالي + حد دلالي؛ وعندما تكون `Round`
تساوي true يأخذ نصف قطر الزاوية نصف الارتفاع (شكل كبسولة، يقابل رمز `tag.borderRadius`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>الحد والزوايا الدائرية</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--plain">حد</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">حد</span>
      <span class="sim-tag sim-tag--danger sim-tag--plain">حد</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--round">زوايا دائرية</span>
      <span class="sim-tag sim-tag--success sim-tag--round sim-tag--plain">حد دائري</span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## الحجم والإغلاق

ثلاثة أحجام تُربط برموز `tag.height` / `paddingHorizontal` / `fontSize`؛ وعندما تكون `Closable`
تساوي true يُرسم رمز الإغلاق على الجانب الآخر، ويطلق النقر عليه حدث `Close`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>الحجم والإغلاق</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--small">صغير</span>
      <span class="sim-tag">متوسط</span>
      <span class="sim-tag sim-tag--large">كبير</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">قابل للإغلاق <span class="sim-tag__close">✕</span></span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var tag = new LhTag
{
    Text = "قابل للإغلاق",
    Type = LhTagType.Primary,
    Closable = true,
};
tag.Close += () => { /* النقر على رمز الإغلاق */ };
```

## السمتان الفاتحة والداكنة

بدّل سمة الموقع من الزاوية العلوية تتغير ألوان الوسوم داخل إطار هاتف المحاكي؛ وعلى الجهاز الحقيقي يتم التبديل عبر `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>وسوم داكنة</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">رئيسي</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">حد</span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي؛ جميع قيم الفاتح/الداكن مأخوذة من لقطة رموز core</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `Close` | يُطلق عند النقر على رمز الإغلاق (يتطلب `Closable` تساوي true) | `event Action` | — |
| `Text` | محتوى نص الوسم (فتحة default في العقد) | `string` | `""` |
| `Type` | النوع الدلالي، يحدد لون الخلفية ولون النص ولون الحد | `LhTagType` | `Default` |
| `Size` | حجم الوسم، يُربط برموز الأحجام | `LhTagSize` | `Medium` |
| `Plain` | ما إذا كان الشكل ذا خلفية فاتحة وحد | `bool` | `false` |
| `Round` | ما إذا كان الشكل كبسولة (نصف قطر الزاوية نصف الارتفاع) | `bool` | `false` |
| `Closable` | ما إذا كان سيُعرض رمز الإغلاق | `bool` | `false` |

### LhTagType

| القيمة | خلفية مصمتة | خلفية فاتحة (plain) | لون النص |
| --- | --- | --- | --- |
| `Default` | `color.background.default` | نفس ما سبق | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.primary.bg` | `color.onPrimary` / `color.primary.default` |
| `Success` | `color.success.default` | `color.success.bg` | `color.onPrimary` / `color.success.default` |
| `Warning` | `color.warning.default` | `color.warning.bg` | `color.onPrimary` / `color.warning.default` |
| `Danger` | `color.danger.default` | `color.danger.bg` | `color.onPrimary` / `color.danger.default` |
| `Info` | `color.info.default` | `color.info.bg` | `color.onPrimary` / `color.info.default` |

### LhTagSize

| القيمة | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `tag.height.small` (20epx) | `tag.paddingHorizontal.small` (8epx) | `tag.fontSize.small` (11) |
| `Medium` | `tag.height.medium` (24epx) | `tag.paddingHorizontal.medium` (12epx) | `tag.fontSize.medium` (12) |
| `Large` | `tag.height.large` (28epx) | `tag.paddingHorizontal.large` (16epx) | `tag.fontSize.large` (13) |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `tag.height.{small,medium,large}` | ثلاثة ارتفاعات |
| `tag.paddingHorizontal.{small,medium,large}` | ثلاث حشوات أفقية |
| `tag.fontSize.{small,medium,large}` | ثلاثة أحجام خط |
| `tag.borderRadius` / `tag.borderWidth` | نصف قطر الزاوية وعرض الحد |
| `color.primary.default` / `color.onPrimary` | خلفية/نص النوع الرئيسي |
| `color.{success,warning,danger,info}.{default,bg}` | خلفيات الأنواع الدلالية المصمتة/الفاتحة |
| `color.background.default` / `color.text.primary` / `color.border.base` | ألوان النوع الافتراضي الثلاثة |
| `spacing.xs` | مسافة رمز الإغلاق عن النص |
| `typography.fontFamily` | حزمة الخطوط المذيّلة ذات النمط الصيني |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/tag.json` الست لاختبارات xUnit (`tests/LhTagTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `default-type-renders-text` | النوع الافتراضي بخلفية/نص/حد محايد | `Default_Type_Renders_Text` |
| `primary-type-uses-primary-tokens` | النوع الرئيسي يستهلك الرموز الرئيسية | `Primary_Type_Uses_Primary_Tokens` |
| `plain-uses-bg-tokens` | الشكل الفاتح يستهلك رموز الخلفيات الدلالية الفاتحة | `Plain_Uses_Bg_Tokens` |
| `small-size-uses-small-tokens` | الحجم الصغير يستهلك رموز الحجم small | `Small_Size_Uses_Small_Tokens` |
| `round-makes-radius-half-height` | نصف قطر الشكل الكبسولي نصف الارتفاع | `Round_Makes_Radius_Half_Height` |
| `closable-tap-emits-onclose` | النقر على رمز الإغلاق يطلق onClose | `Closable_Tap_Emits_OnClose` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
