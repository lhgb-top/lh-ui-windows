# منطقة نص Textarea

إدخال نصي متعدد الأسطر. يحدد `Rows` عدد الأسطر المرئية مبدئيًا، ويزداد الارتفاع تلقائيًا عند تجاوز المحتوى، مع دعم عدّ الأحرف.

> مصدر العقد: `lh-ui-core/specs/textarea.json` ·
> الحالات الذهبية: `lh-ui-core/cases/textarea.json` (6 حالات، تُرجمت إلى اختبارات xUnit)

## الاستخدام الأساسي

مكوّن مُتحكم به: يستمع المضيف إلى `ValueChange` ويكتب القيمة الجديدة مرة أخرى إلى `Value`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أساسيات Textarea</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">الخزف الأزرق<br/>فاميل روز</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field sim-textarea__field--placeholder">أدخل وصفًا</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var textarea = new LhTextarea
{
    Rows = 3,
    Placeholder = "أدخل وصفًا",
};
textarea.ValueChange += value => { /* مُتحكم به: اكتب القيمة مرة أخرى إلى Value */ };
```

## عدّ الأحرف

عندما تكون `ShowCount` تساوي true يُرسم عداد الأحرف في الزاوية السفلية؛ وعند تعيين `Maxlength` يكون التنسيق
«الحالي/الأقصى»، وإلا يُعرض طول النص الحالي فقط. وعند تجاوز `Maxlength` يُقطع الإدخال الجديد.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>عدّ الأحرف</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">الخزف الأزرق</div>
        <div class="sim-textarea__count">3/10</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">الخزف الأزرق</div>
        <div class="sim-textarea__count">3</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## الحالات

لا يطلق كل من `Disabled` و `Readonly` حدث `ValueChange`، لكن `Disabled` وحده يعرض نمط التعطيل؛
وعند التركيز يأخذ الحد اللون `color.focus` + `textarea.focusRingWidth`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>حالات Textarea</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--disabled">
        <div class="sim-textarea__field">معطّل</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--focus">
        <div class="sim-textarea__field">حد التركيز</div>
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
| `Rows` | عدد الأسطر المرئية مبدئيًا؛ وينمو الارتفاع مع عدد الأسطر والمحتوى | `int` | `3` |
| `Disabled` | ما إذا كان معطّلاً (لا يستجيب للإدخال ويعرض نمط التعطيل) | `bool` | `false` |
| `Readonly` | ما إذا كان للقراءة فقط (يقبل التركيز لكن لا يقبل الإدخال) | `bool` | `false` |
| `ShowCount` | ما إذا كان سيُعرض عداد الأحرف في الزاوية السفلية | `bool` | `false` |
| `Maxlength` | الحد الأقصى لعدد الأحرف؛ يُقطع الإدخال الجديد عند تجاوزه، ويكون المقام في showCount | `int?` | `null` |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `textarea.paddingVertical` / `paddingHorizontal` | الحشو الداخلي |
| `textarea.fontSize` | حجم الخط |
| `textarea.borderRadius` / `borderWidth` | نصف قطر الزاوية وعرض الحد عند فقد التركيز |
| `textarea.focusRingWidth` / `color.focus` | عرض حد التركيز ولونه |
| `textarea.countFontSize` / `color.text.secondary` | حجم خط/لون عداد الأحرف |
| `textarea.minHeight` | الحد الأدنى للارتفاع (لا تقل نتيجة احتساب عدد الأسطر عنه) |
| `color.background.default` / `color.text.primary` | لون خلفية الحاوية/النص |
| `color.text.placeholder` | لون نص العنصر النائب |
| `color.border.base` | لون الحد عند فقد التركيز |
| `color.disabled.{bg,text}` | خلفية/نص حالة التعطيل |
| `typography.lineHeight.primary` | ارتفاع السطر (يُحتسب الارتفاع وفق rows) |
| `typography.fontFamily` | حزمة الخطوط المذيّلة ذات النمط الصيني |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/textarea.json` الست لاختبارات xUnit (`tests/LhTextareaTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `input-emits-onvaluechange` | الإدخال يطلق onValueChange | `Input_Emits_OnValueChange` |
| `disabled-blocks-input` | حالة التعطيل لا تُطلق وتعرض نمط التعطيل | `Disabled_Blocks_Input` |
| `showcount-renders-count` | «الحالي/الأقصى» يأخذ رمز countFontSize | `ShowCount_Renders_Count` |
| `showcount-without-maxlength-renders-length-only` | بدون maxlength يُعرض الطول فقط | `ShowCount_Without_Maxlength_Renders_Length_Only` |
| `focus-uses-focus-token` | حد التركيز يأخذ رمز focus | `Focus_Uses_Focus_Token` |
| `maxlength-truncates-input` | تجاوز maxlength يُقطع | `Maxlength_Truncates_Input` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
