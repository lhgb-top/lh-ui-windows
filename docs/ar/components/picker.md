# منتقي Picker

منتقي بعجلة عمود واحد ينبثق من الأسفل. يدعم العناصر المرشحة وتقييد الفهرس وشريط الأدوات.

> مصدر العقد: `lh-ui-core/specs/picker.json` ·
> الحالات الذهبية: `lh-ui-core/cases/picker.json` (8 حالات، تُرجمت إلى اختبارات xUnit)

## الاستخدام الأساسي

رؤية تصريحية: عندما تكون `Visible` تساوي false لا يُعرض أي محتوى؛ وبعد التأكيد/الإلغاء يُطلق `Close`،
فيضبط المضيف `Visible` إلى false بناءً عليه.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أساسيات Picker</span></div>
  <div class="sim-screen">
    <div class="sim-picker-overlay"></div>
    <div class="sim-picker">
      <div class="sim-picker__toolbar">
        <span class="sim-picker__cancel">إلغاء</span>
        <span class="sim-picker__title">اختيار</span>
        <span class="sim-picker__confirm">تأكيد</span>
      </div>
      <div class="sim-picker__wheel">
        <span class="sim-picker__item">الخزف الأزرق</span>
        <span class="sim-picker__item sim-picker__item--active">فاميل روز</span>
        <span class="sim-picker__item">خزف لينغلونغ</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var picker = new LhPicker
{
    Options = new[] { "الخزف الأزرق", "فاميل روز", "خزف لينغلونغ" },
    Value = 1,
    Title = "اختيار",
};
picker.ValueChange += index => { /* فهرس العنصر المحدد بعد توقف العجلة */ };
picker.Confirm += index => { /* الفهرس المحدد حاليًا */ };
picker.Close += () => picker.Visible = false;

picker.Visible = true;
```

## تقييد الفهرس

عند خروج `Value` عن النطاق يُقىّد إلى نطاق العناصر المرشحة: القيم السالبة تُعاير إلى 0، وما يزيد عن عدد العناصر - 1 يُعاير إلى العنصر الأخير.

```csharp
Assert.Equal(0, LhPickerTokens.ClampIndex(-1, 3));   // القيم السالبة تُعاير إلى 0
Assert.Equal(2, LhPickerTokens.ClampIndex(100, 3));  // تجاوز النطاق يُعاير إلى العنصر الأخير
```

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `ValueChange` | يُطلق عند توقف العجلة وتغيّر العنصر المحدد | `event Action<int>` | — |
| `Confirm` | يُطلق عند النقر على زر التأكيد، ويحمل الفهرس المحدد حاليًا | `event Action<int>` | — |
| `Cancel` | يُطلق عند النقر على زر الإلغاء | `event Action` | — |
| `Close` | يُطلق بعد التأكيد/الإلغاء، فيضبط المضيف بناءً عليه Visible إلى false | `event Action` | — |
| `Visible` | ما إذا كان ظاهرًا؛ عندما يكون false لا يُعرض أي محتوى | `bool` | `false` |
| `Options` | مصفوفة نصوص العناصر المرشحة (عمود واحد) | `string[]?` | `null` |
| `Value` | فهرس العنصر المحدد (ابتداءً من 0)؛ يُقىّد إلى نطاق العناصر المرشحة عند تجاوز النطاق | `int` | `0` |
| `Title` | العنوان في منتصف شريط الأدوات | `string` | `""` |
| `ConfirmText` | نص زر التأكيد | `string` | `"确认"` |
| `CancelText` | نص زر الإلغاء | `string` | `"取消"` |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `picker.height` | ارتفاع اللوحة |
| `picker.toolbarHeight` | ارتفاع شريط الأدوات العلوي (ارتفاع منطقة العجلة = ارتفاع اللوحة - ارتفاع شريط الأدوات) |
| `picker.itemHeight` | ارتفاع العنصر المرشح (مسافة المؤشر المركزي نفسها) |
| `picker.itemFontSize` | حجم خط العناصر المرشحة وشريط الأدوات |
| `picker.borderRadius` | نصف قطر الزاوية العلوية للوحة |
| `color.background.default` | لون خلفية اللوحة |
| `color.background.overlay` | لون القناع |
| `color.text.primary` | لون العنوان والعناصر المرشحة |
| `color.text.secondary` | لون زر الإلغاء |
| `color.primary.default` | لون زر التأكيد |
| `color.border.lighter` | لون خط المؤشر المركزي |
| `zIndex.modal` / `zIndex.modalBackdrop` | مستوى اللوحة/القناع |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/picker.json` الثمانية لاختبارات xUnit (`tests/LhPickerTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `confirm-emits-onconfirm-with-value` | التأكيد يحمل الفهرس المحدد حاليًا | `Confirm_Emits_OnConfirm_With_Value` |
| `confirm-also-emits-onclose` | التأكيد يطلق onClose أيضًا | `Confirm_Also_Emits_OnClose` |
| `cancel-emits-oncancel` | الإلغاء يطلق onCancel | `Cancel_Emits_OnCancel` |
| `scroll-emits-onvaluechange` | توقف العجلة يطلق onValueChange | `Scroll_Emits_OnValueChange` |
| `clamp-negative-value-to-zero` | الفهرس السالب يُقىّد إلى 0 | `Clamp_Negative_Value_To_Zero` |
| `clamp-overflow-value-to-last` | الفهرس خارج النطاق يُقىّد إلى العنصر الأخير | `Clamp_Overflow_Value_To_Last` |
| `title-renders-text` | العنوان يُعرض وفق رمز حجم الخط | `Title_Renders_Text` |
| `hidden-not-rendered` | عند عدم الرؤية لا يُعرض أي محتوى | `Hidden_Not_Rendered` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
