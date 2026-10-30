# البدء السريع

## الحد الأدنى من المثال

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui;
using Top.Lhgb.Ui.Common;

public sealed class DemoPage : Page
{
    public DemoPage()
    {
        // ضخ لوحة الألوان الفاتحة؛ لتفعيل الداكن مرّر true. تتبعها جميعًا مكوّنات Lh تلقائيًا (إعادة رسم حية عبر LhTheme.ThemeChanged)
        LhTheme.Apply(false);

        var button = new LhButton
        {
            Type = LhButtonType.Primary,
            Text = "زر رئيسي",
        };
        button.Click += OnButtonClick;

        Content = new StackPanel { Children = { button } };
    }

    private void OnButtonClick() => { /* معالجة النقر */ }
}
```

يبدّل `LhTheme.Apply(isDark)` لوحة الألوان الثابتة ويطلق `ThemeChanged`، فيُعاد رسم جميع مكوّنات Lh المحمّلة تلقائيًا؛ وعند عدم استدعائه يُرتد إلى لوحة الألوان الفاتحة.

## النوع والحجم

```csharp
// ستة أنواع
new LhButton { Text = "افتراضي" };                                     // Default
new LhButton { Type = LhButtonType.Primary,   Text = "رئيسي" };
new LhButton { Type = LhButtonType.Success,   Text = "نجاح" };
new LhButton { Type = LhButtonType.Warning,   Text = "تحذير" };
new LhButton { Type = LhButtonType.Danger,    Text = "خطر" };
new LhButton { Type = LhButtonType.Info,      Text = "معلومات" };

// ثلاثة أحجام
new LhButton { Size = LhButtonSize.Small,  Text = "صغير" };
new LhButton { Size = LhButtonSize.Medium, Text = "متوسط" };   // الافتراضي
new LhButton { Size = LhButtonSize.Large,  Text = "كبير" };
```

## الحالات

```csharp
// التعطيل: لا يستجيب للنقر (يعترضه بوابة Tapped)، ويعرض نمط التعطيل
new LhButton { Disabled = true, Text = "معطّل" };

// التحميل: يعرض ProgressRing ولا يطلق Click
new LhButton { Loading = true, Text = "جارٍ التحميل" };

// اجتماع الحالتين معًا: الأولوية للتعطيل (الحالة الذهبية disabled-precedes-loading)
new LhButton { Disabled = true, Loading = true, Text = "التعطيل أولاً" };
```

## الأيقونة وخلفية النمط

```csharp
// يُحلّ اسم الأيقونة عبر نظام الأيقونات المحلي (LhButtonTokens.IconFor، يدعم حاليًا search / refresh / check / close،
// والأسماء غير المسجلة لا تُرسم)
new LhButton { Type = LhButtonType.Primary, Icon = "search", Text = "بحث" };

// خلفية النمط الصيني (تستهلك رمز color.decoration)
new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = "خلفية نمط" };
```

## السمتان الفاتحة والداكنة

```csharp
LhTheme.Apply(true);  // تفعيل الوضع الداكن عموميًا؛ تتبعه جميع المكوّنات تلقائيًا عند تحليل المظهر التالي
```

## مكوّنات النماذج والتغذية الراجعة

```csharp
// إدخال مُتحكم به: يستمع المضيف إلى ValueChange ويكتب القيمة مرة أخرى إلى Value
var input = new LhInput { Placeholder = "أدخل كلمة مفتاحية" };
input.ValueChange += value => input.Value = value;

// مربع اختيار
var checkbox = new LhCheckbox { Label = "الخزف الأزرق" };
checkbox.Change += isChecked => { };

// مفتاح
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };

// تنبيه حتمي: ضع عنصر المضيف أولًا، ثم استخدم المتحكم لتشغيله
var toast = new LhToast();
toast.Close += () => { };
var panel = new StackPanel { Children = { toast } };
toast.Show("تم الحفظ");   // يُخفى تلقائيًا عند انتهاء المدة ويطلق Close
```

## معاينة أمثلة المكوّنات

لا يمكن لواجهة المستخدم الأصلية أن تُعرض لحظيًا في المتصفح، لذا يستخدم هذا الموقع **محاكي إطارات هاتف HTML ثابت مضمّن** لعرض المظهر:

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>lh-ui-windows</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">زر افتراضي</button>
      <button class="sim-btn sim-btn--primary">زر رئيسي</button>
      <button class="sim-btn sim-btn--success">نجاح</button>
      <button class="sim-btn sim-btn--warning">تحذير</button>
      <button class="sim-btn sim-btn--danger">خطر</button>
      <button class="sim-btn sim-btn--info">معلومات</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">صغير</button>
      <button class="sim-btn sim-btn--primary sim-btn--large">كبير</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">معطّل</button>
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>جارٍ التحميل
      </button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>بحث</button>
      <button class="sim-btn sim-btn--primary sim-btn--pattern">خلفية نمط</button>
    </div>
  </div>
</div>
<p class="sim-note">معاينة محاكاة HTML (مظهر Windows / WinUI 3)، ليست رسمًا على جهاز حقيقي؛ جميع قيم الأنماط مأخوذة من لقطة رموز core، بنفس المصدر ونفس القيم كالجهاز الحقيقي. يُعتمد على تطبيق العرض التوضيحي ولقطات الجهاز فيما يخص المظهر على الجهاز الحقيقي.</p>

> المحاكي الداكن مرتبط بالموقع: بدّل السمة من الزاوية العلوية تتغير الألوان داخل إطار الهاتف (يحسب core رموز الوضع الداكن مسبقًا).
