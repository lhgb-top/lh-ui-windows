# حقل إدخال Input

إدخال نصي أحادي السطر. يدعم النوع والحجم والتعطيل والقراءة فقط والإلغاء والحد الأقصى لعدد الأحرف.

> مصدر العقد: `lh-ui-core/specs/input.json` ·
> الحالات الذهبية: `lh-ui-core/cases/input.json` (8 حالات، تُرجمت إلى اختبارات xUnit)

## الاستخدام الأساسي

مكوّن مُتحكم به: يستمع المضيف إلى `ValueChange` ويكتب القيمة الجديدة مرة أخرى إلى `Value`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أساسيات Input</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">الخزف الأزرق</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field sim-input__field--placeholder">أدخل كلمة مفتاحية</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var input = new LhInput
{
    Placeholder = "أدخل كلمة مفتاحية",
};
input.ValueChange += value => { /* مُتحكم به: اكتب القيمة مرة أخرى إلى Value */ };
```

## الأحجام

ثلاثة أحجام تُربط برموز `input.height` / `paddingHorizontal` / `fontSize`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أحجام Input</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input sim-input--small"><div class="sim-input__field">صغير</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">متوسط</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--large"><div class="sim-input__field">كبير</div></div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## النوع والحالة

يعرض `Password` القيمة كنقاط قناعية؛ ولا يطلق كل من `Disabled` و `Readonly` حدث `ValueChange`،
لكن `Disabled` وحده يعرض نمط التعطيل؛ وعند التركيز يأخذ الحد اللون `color.focus` + `input.focusRingWidth`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>حالات Input</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">●●●●●●●●</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--disabled"><div class="sim-input__field">معطّل</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--focus"><div class="sim-input__field">حد التركيز</div></div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## الإلغاء وعدد الأحرف

عندما تكون `Clearable` تساوي true والقيمة غير فارغة يُعرض زر الإلغاء، ويطلق النقر عليه `ValueChange("")`؛
وعند تجاوز `Maxlength` يُقطع الإدخال الجديد.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>الإلغاء وعدد الأحرف</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">الخزف الأزرق</div>
        <span class="sim-input__clear">✕</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `ValueChange` | رد نداء تغيّر القيمة (مُتحكم به)؛ لا يُطلق عند disabled / readonly | `event Action<string>` | — |
| `Focus` | يُطلق عند اكتساب التركيز | `event Action` | — |
| `Blur` | يُطلق عند فقد التركيز | `event Action` | — |
| `Value` | قيمة النص الحالية (مُتحكم بها) | `string` | `""` |
| `Placeholder` | نص العنصر النائب، يأخذ رمز `color.text.placeholder` | `string` | `""` |
| `Type` | نوع الإدخال؛ يُعرض كنقاط قناعية عند password | `LhInputType` | `Text` |
| `Size` | حجم حقل الإدخال، يُربط برموز الأحجام | `LhInputSize` | `Medium` |
| `Disabled` | ما إذا كان معطّلاً (لا يستجيب للإدخال ويعرض نمط التعطيل) | `bool` | `false` |
| `Readonly` | ما إذا كان للقراءة فقط (يقبل التركيز لكن لا يقبل الإدخال) | `bool` | `false` |
| `Clearable` | ما إذا كان سيُعرض زر الإلغاء | `bool` | `false` |
| `Maxlength` | الحد الأقصى لعدد الأحرف؛ يُقطع الإدخال الجديد عند تجاوزه | `int?` | `null` |

### LhInputSize

| القيمة | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `input.height.small` (32epx) | `input.paddingHorizontal.small` (12epx) | `input.fontSize.small` (13) |
| `Medium` | `input.height.medium` (40epx) | `input.paddingHorizontal.medium` (16epx) | `input.fontSize.medium` (14) |
| `Large` | `input.height.large` (48epx) | `input.paddingHorizontal.large` (20epx) | `input.fontSize.large` (16) |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `input.height.{small,medium,large}` | ثلاثة ارتفاعات |
| `input.paddingHorizontal.{small,medium,large}` | ثلاث حشوات أفقية |
| `input.fontSize.{small,medium,large}` | ثلاثة أحجام خط |
| `input.borderRadius` / `input.borderWidth` | نصف قطر الزاوية وعرض الحد عند فقد التركيز |
| `input.focusRingWidth` / `color.focus` | عرض حد التركيز ولونه |
| `input.clearIconSize` | حجم أيقونة زر الإلغاء |
| `color.background.default` / `color.text.primary` | لون خلفية الحاوية/النص |
| `color.text.placeholder` | لون نص العنصر النائب |
| `color.border.base` | لون الحد عند فقد التركيز |
| `color.disabled.{bg,text}` | خلفية/نص حالة التعطيل |
| `typography.fontFamily` | حزمة الخطوط المذيّلة ذات النمط الصيني |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/input.json` الثمانية لاختبارات xUnit (`tests/LhInputTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `input-emits-onvaluechange` | الإدخال يطلق onValueChange | `Input_Emits_OnValueChange` |
| `clearable-emits-empty-value` | الإلغاء يطلق onValueChange("") | `Clearable_Emits_Empty_Value` |
| `disabled-blocks-input` | حالة التعطيل لا تُطلق وتعرض نمط التعطيل | `Disabled_Blocks_Input` |
| `readonly-blocks-input` | القراءة فقط لا تُطلق لكنها لا تعرض نمط التعطيل | `Readonly_Blocks_Input` |
| `placeholder-uses-placeholder-token` | نص العنصر النائب يأخذ رمز العنصر النائب | `Placeholder_Uses_Placeholder_Token` |
| `focus-uses-focus-token` | حد التركيز يأخذ رمز focus | `Focus_Uses_Focus_Token` |
| `small-size-uses-small-tokens` | الحجم الصغير يستهلك رموز الحجم small | `Small_Size_Uses_Small_Tokens` |
| `maxlength-truncates-input` | تجاوز maxlength يُقطع | `Maxlength_Truncates_Input` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
