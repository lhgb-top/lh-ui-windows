# خلية Cell

وحدة صف في القائمة. أيقونة على جهة + عنوان (قد يصاحبه نجم أحمر للحقول الإلزامية) + وصف، وقيمة على الجهة الأخرى مع سهم إرشادي، وخط فاصل اختياري في الأسفل.

> مصدر العقد: `lh-ui-core/specs/cell.json` ·
> الحالات الذهبية: `lh-ui-core/cases/cell.json` (6 حالات، تُرجمت إلى اختبارات xUnit)

## الاستخدام الأساسي

يأخذ العنوان `color.text.primary`، وتأخذ القيمة والوصف `color.text.secondary`، وجميعها مدفوعة بالرموز.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أساسيات Cell</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">عنوان الصف</div>
        <div class="sim-cell__label">نص توضيحي</div>
      </div>
      <div class="sim-cell__value">محتوى الصف</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">خلية بخط فاصل</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var cell = new LhCell
{
    Title = "عنوان الصف",
    Label = "نص توضيحي",
    Value = "محتوى الصف",
};
```

## الأيقونة والحقول الإلزامية

يأخذ `Icon` اسم أيقونة محليًا (راجع جدول الرموز في توثيق Icon) ويُرسم على الجهة الأخرى؛
وعندما تكون `Required` تساوي true يُرسم أمام العنوان نجم أحمر بلون `color.danger.default`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>الأيقونة والإلزامية</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <span class="sim-cell__icon">★</span>
      <div class="sim-cell__main">
        <div class="sim-cell__title">بأيقونة</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title"><span class="sim-cell__required">✱</span>حقل إلزامي</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## الخط الفاصل والنقر

عندما تكون `Bordered` تساوي true يُرسم في الأسفل خط فاصل بلون `color.border.lighter`؛ وعندما تكون `Clickable`
تساوي true يُرسم سهم إرشادي على الجهة الأخرى ويطلق النقر حدث `Click`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>الفاصل والنقر</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">قابل للنقر</div></div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">غير قابل للنقر</div></div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var cell = new LhCell
{
    Title = "قابل للنقر",
    Clickable = true,
    Bordered = true,
};
cell.Click += () => { /* يُطلق فقط عندما تكون Clickable تساوي true */ };
```

## السمتان الفاتحة والداكنة

بدّل سمة الموقع من الزاوية العلوية تتغير ألوان النص وألوان الخط الفاصل داخل إطار هاتف المحاكي؛ وعلى الجهاز الحقيقي يتم التبديل عبر `LhTheme.Apply(true)`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>خلية داكنة</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">عنوان الصف</div>
        <div class="sim-cell__label">نص توضيحي</div>
      </div>
      <div class="sim-cell__value">محتوى الصف</div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي؛ جميع قيم الفاتح/الداكن مأخوذة من لقطة رموز core</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `Click` | حدث النقر؛ يُطلق فقط عندما تكون `Clickable` تساوي true | `event Action` | — |
| `Title` | العنوان (فتحة title في العقد) | `string` | `""` |
| `Label` | النص التوضيحي تحت العنوان (فتحة label) | `string` | `""` |
| `Value` | نص القيمة على الجهة الأخرى (فتحة value) | `string` | `""` |
| `Icon` | اسم الأيقونة على الجهة الأخرى (تُحلّ عبر جدول الأيقونات المحلي) | `string?` | `null` |
| `Required` | ما إذا كان سيُرسم نجم إلزامي أحمر أمام العنوان | `bool` | `false` |
| `Clickable` | ما إذا كان قابلًا للنقر (يُظهر/يُخفي السهم ويبوّب Click) | `bool` | `false` |
| `Bordered` | ما إذا كان سيُرسم خط فاصل في الأسفل | `bool` | `true` |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `cell.paddingHorizontal` / `cell.paddingVertical` | الحشو الداخلي |
| `cell.gap` | المسافة العمودية بين العنوان والوصف |
| `cell.fontSizeTitle` / `fontSizeLabel` / `fontSizeValue` | ثلاثة أحجام خط |
| `color.text.{primary,secondary,regular}` | لون العنوان/الوصف/القيمة/الأيقونة |
| `color.danger.default` | النجم الإلزامي الأحمر |
| `color.border.lighter` | الخط الفاصل السفلي |
| `icon.size.medium` | حجم الأيقونة الجانبية |
| `spacing.{sm,xs}` | مسافة الأيقونة / مسافة العناصر الداخلية |
| `typography.lineHeight.primary` | ارتفاع سطر النص |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/cell.json` الست لاختبارات xUnit (`tests/LhCellTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `title-and-value-render` | يُعرض العنوان والقيمة معًا | `Title_And_Value_Render` |
| `label-renders-secondary-tone` | النص التوضيحي يأخذ رمز لون النص الثانوي | `Label_Renders_Secondary_Tone` |
| `required-uses-danger-token` | النجم الإلزامي يستهلك رمز لون الخطر | `Required_Uses_Danger_Token` |
| `bordered-emits-line-command` | شكل الخط الفاصل يُصدر أمر LINE | `Bordered_Emits_Line_Command` |
| `clickable-emits-onclick` | عندما يكون قابلًا للنقر يطلق النقر onClick | `Clickable_Emits_OnClick` |
| `not-clickable-no-onclick` | عندما لا يكون قابلًا للنقر لا يطلق onClick | `Not_Clickable_No_OnClick` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
