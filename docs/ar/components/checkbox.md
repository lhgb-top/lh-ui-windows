# مربع اختيار Checkbox

مربع اختيار. يُرسم عند التحديد علامة صح، وعند الحالة غير محددة خط أفقي، ويغطي النقر الصف بأكمله (المربع + منطقة التسمية).

> مصدر العقد: `lh-ui-core/specs/checkbox.json` ·
> الحالات الذهبية: `lh-ui-core/cases/checkbox.json` (6 حالات، تُرجمت إلى اختبارات xUnit)

## الاستخدام الأساسي

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أساسيات Checkbox</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box"></span>
        <span class="sim-checkbox__label">غير محدد</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">محدد</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">─</span>
        <span class="sim-checkbox__label">غير محدد</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var checkbox = new LhCheckbox
{
    Label = "الخزف الأزرق",
    Checked = true,
};
checkbox.Change += isChecked => { };
```

## الأحجام

ثلاثة أحجام تُربط برموز حجم المربع `checkbox.size.*`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أحجام Checkbox</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--small sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">صغير</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">متوسط</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--large sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">كبير</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## الحالات

عندما يكون `Disabled` لا يستجيب للنقر، وتأخذ الخلفية/الحد/المؤشر/النص جميعها رموز `color.disabled.*`؛
ويمثل `Indeterminate` الحالة غير محددة (خط أفقي)، ويتحول بالنقر إلى محدد.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>حالات Checkbox</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked sim-checkbox__box--disabled">✓</span>
        <span class="sim-checkbox__label">محدد معطّل</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--disabled"></span>
        <span class="sim-checkbox__label">غير محدد معطّل</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `Change` | رد نداء تغيّر حالة التحديد؛ لا يُطلق عند disabled | `event Action<bool>` | — |
| `Checked` | ما إذا كان محددًا | `bool` | `false` |
| `Indeterminate` | ما إذا كانت الحالة غير محددة (مؤشر خط أفقي)؛ يتحول بالنقر إلى محدد | `bool` | `false` |
| `Disabled` | ما إذا كان معطّلاً (لا يستجيب للنقر ويعرض نمط التعطيل) | `bool` | `false` |
| `Label` | التسمية النصية (تُرسم إلى جانب المربع) | `string` | `""` |
| `Size` | حجم المربع، يُربط برموز `checkbox.size.*` | `LhCheckboxSize` | `Medium` |

### LhCheckboxSize

| القيمة | رمز الحجم |
| --- | --- |
| `Small` | `checkbox.size.small` (14epx) |
| `Medium` | `checkbox.size.medium` (16epx) |
| `Large` | `checkbox.size.large` (18epx) |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `checkbox.size.{small,medium,large}` | ثلاثة أحجام للمربع |
| `checkbox.borderRadius` / `checkbox.borderWidth` | نصف قطر زاوية المربع وعرض الحد |
| `checkbox.checkWidth` | عرض حد علامة الصح/الخط الأفقي |
| `checkbox.gap` | المسافة بين المربع والنص |
| `color.primary.default` / `color.onPrimary` | لونا خلفية ومؤشر حالتي التحديد وغير محدد |
| `color.border.base` | لون حد الحالة غير المحددة |
| `color.disabled.{bg,border,text}` | خلفية/حد/مؤشر حالة التعطيل |
| `color.text.{primary,disabled}` | لون نص التسمية |
| `typography.fontSize.base` / `fontFamily` | حجم خط التسمية وحزمة الخطوط |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/checkbox.json` الست لاختبارات xUnit (`tests/LhCheckboxTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | النقر على غير المحدد يحدده | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-unchecks` | النقر على المحدد يلغي تحديده | `Tap_Checked_Unchecks` |
| `disabled-blocks-change` | الحالة المعطّلة غير قابلة للتفاعل | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | حالة التحديد تستهلك الرموز الرئيسية | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | الحالة غير المحددة تستهلك رمز الحد | `Unchecked_Uses_Border_Token` |
| `indeterminate-becomes-checked` | الحالة غير محددة تتحول بالنقر إلى محدد | `Indeterminate_Becomes_Checked` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
