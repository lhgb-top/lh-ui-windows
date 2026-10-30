# مفتاح Switch

مفتاح. مسار على شكل كبسولة + المقبض دائري، يبدّله النقر؛ ويدعم التعطيل والتحميل.

> مصدر العقد: `lh-ui-core/specs/switch.json` ·
> الحالات الذهبية: `lh-ui-core/cases/switch.json` (6 حالات، تُرجمت إلى اختبارات xUnit)

## الاستخدام الأساسي

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أساسيات Switch</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };
```

## الأحجام

ثلاثة أحجام تُربط برموز `switch.width` / `switch.height`؛ وقطر المقبض = الارتفاع - 2 × `switch.gap`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أحجام Switch</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--small sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--large sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## الحالات

لا يستجيب كل من `Disabled` و `Loading` للنقر؛ وعند `Loading` يُستبدل المقبض بحلقة تحميل باللون الرئيسي.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>حالات Switch</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--checked sim-switch--disabled"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--loading"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `Change` | رد نداء تغيّر حالة المفتاح؛ لا يُطلق عند disabled أو loading | `event Action<bool>` | — |
| `Checked` | ما إذا كان مفتوحًا | `bool` | `false` |
| `Disabled` | ما إذا كان معطّلاً (لا يستجيب للنقر ويعرض نمط التعطيل) | `bool` | `false` |
| `Loading` | ما إذا كان قيد التحميل (يعرض حلقة تحميل مكان المقبض ولا يستجيب للنقر) | `bool` | `false` |
| `Size` | حجم المفتاح، يُربط برموز `switch.width` / `switch.height` | `LhSwitchSize` | `Medium` |

### LhSwitchSize

| القيمة | width | height |
| --- | --- | --- |
| `Small` | `switch.width.small` (40epx) | `switch.height.small` (22epx) |
| `Medium` | `switch.width.medium` (48epx) | `switch.height.medium` (26epx) |
| `Large` | `switch.width.large` (56epx) | `switch.height.large` (30epx) |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `switch.width.{small,medium,large}` | ثلاثة عروض للمسار |
| `switch.height.{small,medium,large}` | ثلاثة ارتفاعات للمسار |
| `switch.gap` | الفراغ بين المقبض والمسار (قطر المقبض = الارتفاع - 2 × gap) |
| `color.primary.default` | لون المسار عند الفتح |
| `color.border.base` | لون المسار عند الإغلاق |
| `color.onPrimary` | لون المقبض |
| `color.disabled.bg` / `color.text.disabled` | لونا المسار/المقبض في حالة التعطيل |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/switch.json` الست لاختبارات xUnit (`tests/LhSwitchTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | النقر يبدّل الحالة ويطلق onChange | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-turns-off` | النقر على المفتاح المفتوح يغلقه | `Tap_Checked_Turns_Off` |
| `disabled-blocks-change` | الحالة المعطّلة غير قابلة للتفاعل | `Disabled_Blocks_Change` |
| `loading-blocks-change` | حالة التحميل غير قابلة للتفاعل | `Loading_Blocks_Change` |
| `checked-uses-primary-token` | حالة الفتح تستهلك الرمز الرئيسي | `Checked_Uses_Primary_Token` |
| `unchecked-uses-border-token` | حالة الإغلاق تستهلك رمز الحد | `Unchecked_Uses_Border_Token` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
