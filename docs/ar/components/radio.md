# زر اختيار Radio

زر اختيار. حلقة + نقطة داخلية، ويغطي النقر الصف بأكمله؛ المكوّن نفسه لا يحمل منطق المجموعة، ويُترك إدارة مجموعة الاختيار الأحادي للمضيف.

> مصدر العقد: `lh-ui-core/specs/radio.json` ·
> الحالات الذهبية: `lh-ui-core/cases/radio.json` (6 حالات، تُرجمت إلى اختبارات xUnit)

## الاستخدام الأساسي

يستمع المضيف إلى `Change` ثم يضبط `Checked` للعناصر الأخرى في نفس المجموعة إلى false، فيتحقق بذلك سلوك مجموعة الاختيار الأحادي.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أساسيات Radio</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">الخزف الأزرق</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"></span>
        <span class="sim-radio__label">فاميل روز</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var radio = new LhRadio
{
    Label = "الخزف الأزرق",
    Value = "qinghua",
    Checked = true,
};
radio.Change += isChecked => { /* يُطلق فقط عند عدم التحديد وعدم التعطيل؛ المضيف يدير مجموعة الاختيار */ };
```

## الأحجام

ثلاثة أحجام تُربط برموز حجم الحلقة `radio.size.*`؛ وقطر النقطة الداخلية = الحجم × `radio.dotScale`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أحجام Radio</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--small"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">صغير</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">متوسط</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--large"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">كبير</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## الحالات

عندما يكون `Disabled` لا يستجيب للنقر، وتأخذ الحلقة الخارجية `color.disabled.border` والنقطة `color.disabled.text`؛
والنقر على عنصر محدد مسبقًا لا يطلق `Change`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>حالات Radio</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"></span>
        <span class="sim-radio__label">غير محدد معطّل</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">محدد معطّل</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `Change` | رد نداء تغيّر حالة التحديد؛ يُطلق فقط عند عدم التحديد وعدم التعطيل (الحمولة دائمًا true) | `event Action<bool>` | — |
| `Checked` | ما إذا كان محددًا | `bool` | `false` |
| `Disabled` | ما إذا كان معطّلاً (لا يستجيب للنقر ويعرض نمط التعطيل) | `bool` | `false` |
| `Label` | التسمية النصية (تُرسم إلى جانب الحلقة) | `string` | `""` |
| `Value` | قيمة الاختيار (معرّف داخل المجموعة، والمكوّن نفسه لا يحمل منطق المجموعة) | `string` | `""` |
| `Size` | حجم الحلقة، يُربط برموز `radio.size.*` | `LhRadioSize` | `Medium` |

### LhRadioSize

| القيمة | رمز الحجم |
| --- | --- |
| `Small` | `radio.size.small` (14epx) |
| `Medium` | `radio.size.medium` (16epx) |
| `Large` | `radio.size.large` (18epx) |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `radio.size.{small,medium,large}` | ثلاثة أحجام للحلقة |
| `radio.ringWidth` | عرض حد الحلقة الخارجية |
| `radio.dotScale` | نسبة قطر النقطة الداخلية (الحجم × dotScale) |
| `radio.gap` | المسافة بين الحلقة والنص |
| `color.primary.default` | لونا الحلقة الخارجية والنقطة في حالة التحديد |
| `color.border.base` | لون الحلقة الخارجية في حالة عدم التحديد |
| `color.disabled.{border,text}` | الحلقة الخارجية/النقطة في حالة التعطيل |
| `color.text.{primary,disabled}` | لون نص التسمية |
| `typography.fontSize.base` / `fontFamily` | حجم خط التسمية وحزمة الخطوط |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/radio.json` الست لاختبارات xUnit (`tests/LhRadioTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `tap-unchecked-emits-onchange` | النقر على غير المحدد يطلق onChange | `Tap_Unchecked_Emits_OnChange` |
| `tap-checked-keeps-checked` | النقر على المحدد مرة أخرى لا يطلق شيئًا | `Tap_Checked_Keeps_Checked` |
| `disabled-blocks-change` | الحالة المعطّلة غير قابلة للتفاعل | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | حالة التحديد تستهلك الرموز الرئيسية | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | الحالة غير المحددة تستهلك رمز الحد | `Unchecked_Uses_Border_Token` |
| `small-size-uses-small-token` | الحجم الصغير يستهلك رمز الحجم small | `Small_Size_Uses_Small_Token` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
