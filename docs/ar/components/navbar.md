# شريط تنقل Navbar

شريط ثابت أعلى الصفحة. ثلاثة أجزاء: يسار / وسط / يمين، ويدعم النص والأيقونة.

> مصدر العقد: `lh-ui-core/specs/navbar.json` ·
> الحالات الذهبية: `lh-ui-core/cases/navbar.json` (6 حالات، تُرجمت إلى اختبارات xUnit)

## الاستخدام الأساسي

يأخذ المستوى `zIndex.sticky`، ويضعه المضيف شريطًا ثابتًا أعلى الصفحة (الصف الأول من Grid أو أعلى Canvas).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أساسيات Navbar</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">← رجوع</span>
      <span class="sim-navbar__title">الخزف الأزرق</span>
      <span class="sim-navbar__right">المزيد</span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

```csharp
var navbar = new LhNavbar
{
    Title = "الخزف الأزرق",
    LeftText = "رجوع",
    LeftIcon = "back",
    RightText = "المزيد",
};
navbar.LeftClick += () => { };
navbar.RightClick += () => { };
```

## الأيقونات

يأخذ كل من `LeftIcon` / `RightIcon` اسم أيقونة محليًا (راجع جدول الرموز في توثيق Icon)، وعندما يكون النص والأيقونة معًا غير فارغين يُعرض كلاهما؛
وعند عدم وجود محتوى على أحد الجانبين لا يُطلق حدث النقر المقابل.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>أيقونات Navbar</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">←</span>
      <span class="sim-navbar__title">أيقونة فقط</span>
      <span class="sim-navbar__right">⋯</span>
    </div>
    <div class="sim-navbar">
      <span class="sim-navbar__title">بلا محتوى جانبي</span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## الخط الفاصل

عندما تكون `Bordered` تساوي true يُرسم في الأسفل خط فاصل بلون `color.border.lighter`؛ وهو مفعّل افتراضيًا.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>خط فاصل Navbar</span></div>
  <div class="sim-screen">
    <div class="sim-navbar sim-navbar--bordered">
      <span class="sim-navbar__title">بخط فاصل</span>
    </div>
    <div class="sim-navbar sim-navbar--borderless">
      <span class="sim-navbar__title">بلا خط فاصل</span>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي</p>

## API

### الخصائص

| الخاصية | الوصف | النوع | الافتراضي |
| --- | --- | --- | --- |
| `LeftClick` | يُطلق عند النقر على المنطقة اليسرى (لا يُطلق عند عدم وجود محتوى أيسر) | `event Action` | — |
| `RightClick` | يُطلق عند النقر على المنطقة اليمنى (لا يُطلق عند عدم وجود محتوى أيمن) | `event Action` | — |
| `Title` | العنوان الأوسط | `string` | `""` |
| `LeftText` | نص الزر الأيسر؛ عندما يكون هو و LeftIcon معًا غير فارغين يُعرض كلاهما | `string` | `""` |
| `LeftIcon` | اسم الأيقونة اليسرى (تُرسم عبر نظام الأيقونات المحلي) | `string?` | `null` |
| `RightText` | نص الزر الأيمن؛ عندما يكون هو و RightIcon معًا غير فارغين يُعرض كلاهما | `string` | `""` |
| `RightIcon` | اسم الأيقونة اليمنى (تُرسم عبر نظام الأيقونات المحلي) | `string?` | `null` |
| `Bordered` | ما إذا كان سيُرسم خط فاصل سفلي | `bool` | `true` |

## مرجع الرموز

| الرمز | الاستخدام |
| --- | --- |
| `navbar.height` | الارتفاع |
| `navbar.paddingHorizontal` | الحشو الأفقي للجانبين |
| `navbar.titleFontSize` | حجم خط العنوان والنصين الجانبيين |
| `navbar.iconSize` | حجم الأيقونتين الجانبيتين |
| `navbar.borderWidth` | عرض الخط الفاصل السفلي |
| `color.background.default` | لون الخلفية |
| `color.text.primary` | لون العنوان والنصين الجانبيين |
| `color.border.lighter` | لون الخط الفاصل السفلي |
| `zIndex.sticky` | المستوى الثابت |
| `spacing.xs` | المسافة بين الأيقونة والنص |
| `typography.fontFamily` | حزمة الخطوط المذيّلة ذات النمط الصيني |

## مرجع الحالات الذهبية

مطابقة تأكيدات `cases/navbar.json` الست لاختبارات xUnit (`tests/LhNavbarTests.cs`):

| معرّف الحالة | التأكيد | الاختبار المحلي |
| --- | --- | --- |
| `title-renders-text` | العنوان يُعرض وفق رمز حجم الخط | `Title_Renders_Text` |
| `leftclick-emits-onleftclick` | النقر على الجانب الأيسر يطلق onLeftClick | `LeftClick_Emits_OnLeftClick` |
| `rightclick-emits-onrightclick` | النقر على الجانب الأيمن يطلق onRightClick | `RightClick_Emits_OnRightClick` |
| `no-left-content-no-onleftclick` | عند عدم وجود محتوى أيسر لا يُطلق | `No_Left_Content_No_OnLeftClick` |
| `bordered-uses-border-token` | الخط الفاصل يستهلك رمز border.lighter | `Bordered_Uses_Border_Token` |
| `height-uses-navbar-height-token` | الارتفاع يستهلك رمز navbar.height | `Height_Uses_Navbar_Height_Token` |

هناك أيضًا اختبارات مخصصة للوضع الداكن: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
التشغيل: `cd lh-ui-windows && dotnet test`.
