# زر Button

زر إجراء أساسي. يُطلق إجراءً فوريًا، ويدعم النوع والحجم والتحميل والتعطيل والأيقونة وخلفية النمط الصيني.

> مصدر العقد: `lh-ui-core/specs/button.json` ·
> الحالات الذهبية: `lh-ui-core/cases/button.json` (8 حالات، تُرجمت إلى اختبارات xUnit)

## الأنواع

ستة أنواع تحدد لون الخلفية ولون النص.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أنواع Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">زر افتراضي</button>
      <button class="sim-btn sim-btn--primary">زر رئيسي</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--success">زر نجاح</button>
      <button class="sim-btn sim-btn--warning">زر تحذير</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--danger">زر خطر</button>
      <button class="sim-btn sim-btn--info">زر معلومات</button>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var button = new LhButton
{
    Type = LhButtonType.Primary,
    Text = "زر رئيسي",
};
button.Click += OnButtonClick;
```

## الأحجام

ثلاثة أحجام تُربط برموز `button.height` / `paddingHorizontal` / `fontSize` / `iconSize`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أحجام Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">زر صغير</button>
      <button class="sim-btn">زر متوسط</button>
      <button class="sim-btn sim-btn--large">زر كبير</button>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## الحالات

لا يُطلق `Click` لا في حالة التعطيل ولا أثناء التحميل؛ وعند اجتماعهما تكون **الأولوية للتعطيل** (الحالة الذهبية `disabled-precedes-loading`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>حالات Button</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">عادي</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">معطّل</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>جارٍ التحميل
      </button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">التعطيل أولاً</button>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## الأيقونات وخلفية النمط

تُحلّ أسماء الأيقونات عبر نظام الأيقونات المحلي (`LhButtonTokens.IconFor`، يدعم حاليًا search / refresh / check / close،
والأسماء غير المسجلة لا تُرسم). تستهلك خلفية النمط رمز `color.decoration`، وتُرسم كخطوط مائلة بزاوية 45°.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>الأيقونات والأنماط</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>بحث</button>
      <button class="sim-btn"><span class="sim-btn__icon"></span>افتراضي</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--pattern">نمط رئيسي</button>
      <button class="sim-btn sim-btn--pattern">نمط افتراضي</button>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## السمتان الفاتحة والداكنة

بدّل سمة الموقع من الزاوية العلوية تتغير ألوان الأزرار داخل إطار هاتف المحاكي؛ وعلى الجهاز الحقيقي يتم التبديل عبر `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أزرار داكنة</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">زر رئيسي</button>
      <button class="sim-btn">زر افتراضي</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--disabled">معطّل</button>
      <button class="sim-btn sim-btn--success">نجاح</button>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي؛ جميع قيم الفاتح/الداكن مأخوذة من لقطة رموز core</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `Click` | حدث النقر؛ لا يُطلق عندما تكون `Disabled` أو `Loading` تساوي true | `event Action` | — |
| `Type` | نوع الزر، يحدد لون الخلفية ولون النص | `LhButtonType` | `Default` |
| `Size` | حجم الزر، يُربط برموز الأحجام | `LhButtonSize` | `Medium` |
| `Disabled` | ما إذا كان الزر معطّلاً | `bool` | `false` |
| `Loading` | ما إذا كان الزر قيد التحميل (يعرض مؤشرًا ولا يطلق Click) | `bool` | `false` |
| `Icon` | اسم الأيقونة (يُحلّ عبر نظام الأيقونات المحلي) | `string?` | `null` |
| `BgPattern` | ما إذا كان سيتم رسم خلفية النمط الصيني | `bool` | `false` |
| `Text` | محتوى نص الزر (فتحة default في العقد) | `string` | `""` |

### LhButtonType

| القيمة | لون الخلفية | لون النص |
| --- | --- | --- |
| `Default` | `color.background.default` | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.onPrimary` |
| `Success` | `color.success.default` | `color.onPrimary` |
| `Warning` | `color.warning.default` | `color.onPrimary` |
| `Danger` | `color.danger.default` | `color.onPrimary` |
| `Info` | `color.info.default` | `color.onPrimary` |

### LhButtonSize

| القيمة | height | paddingHorizontal | fontSize | iconSize |
| --- | --- | --- | --- | --- |
| `Small` | `button.height.small` (32epx) | `button.paddingHorizontal.small` (16epx) | `button.fontSize.small` (13) | `button.iconSize.small` (16) |
| `Medium` | `button.height.medium` (40epx) | `button.paddingHorizontal.medium` (20epx) | `button.fontSize.medium` (14) | `button.iconSize.medium` (18) |
| `Large` | `button.height.large` (48epx) | `button.paddingHorizontal.large` (24epx) | `button.fontSize.large` (16) | `button.iconSize.large` (20) |

تجاوزات الحالة المعطّلة: الخلفية `color.disabled.bg`، النص `color.disabled.text`، الحد `color.disabled.border`.

## مرجع الرموز

الرموز التي يستهلكها الزر (مطابقة واحدًا لواحد مع `tokenRefs` في `specs/button.json`):

| الرمز | الاستخدام |
| --- | --- |
| `button.height.{small,medium,large}` | ثلاثة ارتفاعات |
| `button.paddingHorizontal.{small,medium,large}` | ثلاث حشوات أفقية |
| `button.fontSize.{small,medium,large}` | ثلاثة أحجام خط |
| `button.iconSize.{small,medium,large}` | ثلاثة أحجام أيقونات |
| `button.borderRadius` / `button.borderWidth` | نصف قطر الزاوية (يتبع `radius.base`) وعرض الحد |
| `button.fontWeight` | وزن الخط (يتبع `typography.fontWeight.primary`، ويُطابق على جانب الجهاز إلى FontWeights) |
| `color.primary.default` / `color.onPrimary` | خلفية/نص النوع الرئيسي |
| `color.{success,warning,danger,info}.default` | خلفيات الأنواع الدلالية |
| `color.background.default` / `color.text.primary` / `color.border.base` | خلفية/نص/حد النوع الافتراضي |
| `color.disabled.{bg,text,border}` | ألوان التعطيل الثلاثة |
| `color.decoration` | لون النمط |
| `spacing.xs` | مسافة الأيقونة عن النص |
| `typography.fontFamily` | حزمة الخطوط المذيّلة ذات النمط الصيني |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/button.json` الثمانية لاختبارات xUnit (`tests/LhButtonTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `click-emits-ononclick` | تسمح البوابة بالمرور (قابل للتفاعل) | `Enabled_Button_Is_Interactive` |
| `disabled-blocks-click` | الحالة المعطّلة غير قابلة للتفاعل | `Disabled_Is_Not_Interactive` |
| `loading-blocks-click` | حالة التحميل غير قابلة للتفاعل | `Loading_Is_Not_Interactive` |
| `disabled-precedes-loading` | الأولوية للتعطيل + نمط التعطيل | `Disabled_Precedes_Loading` |
| `default-type-renders-text` | النوع الافتراضي بخلفية/نص محايد | `Default_Type_Uses_Neutral_Tokens` |
| `primary-type-uses-primary-tokens` | النوع الرئيسي يستهلك الرموز الرئيسية | `Primary_Type_Uses_Primary_Tokens` |
| `small-size-uses-small-tokens` | الحجم الصغير يستهلك رموز الحجم small | `Small_Size_Uses_Small_Tokens` |
| `bgpattern-uses-decoration-token` | خلفية النمط تستهلك لون الزخرفة | `BgPattern_Uses_Decoration_Token` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
