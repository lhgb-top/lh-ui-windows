# السمات والوضع الداكن

## نموذج الرموز

المصدر الأصلي للرموز في السلالة الأصلية هو `lh-ui-core/tokens/*.json`، ويولّد codegen منتجات Windows (`src/Tokens/`، وهي منتجات مُتجاهَلة في gitignore، تُزامَن عبر `pnpm link:tokens`):

| المنتج | المحتوى | شكل الوضع الداكن |
| --- | --- | --- |
| `LhTokens` | كامل الفاتح: أحجام Button، لوحة Color، Motion، Radius، Shadow، Spacing، Typography، ZIndex | — |
| `LhDarkTokens` | الفئة الفرعية `Color` فقط (نموذج **الكائنات المتجاورة**) | الداكن يغطّي الألوان فقط، ولا تتغير الأحجام/الحركة/نصف قطر الزاوية في الداكن |
| `LhShadow` | حامل الظل (color/radius/offsetX/offsetY/spread) | — |
| `LhEasing` | تخفيف الحركة | — |

> هذا يتسق مع android / ios / harmony (كائنات متجاورة)، ويختلف عن طرف docs (دمج JSON السطحي).
> صُمّمت طبقة السمات بناءً عليه: يحمل سجل `LhColors` الألوان فقط، وتأخذ المكوّنات أحجامها مباشرة من `LhTokens`.

## LhColors

تُمدّد `LhColorsFactory.Light()` / `Dark()` الفئتين الثابتتين المولّدتين في سجلٍ ثابتٍ غير قابل للتغيير:

```csharp
public sealed record LhColors
{
    public Color Primary { get; init; }
    public Color OnPrimary { get; init; }
    // ... جميع الألوان الدلالية
}

public static class LhColorsFactory
{
    public static LhColors Light() => new()
    {
        Primary = LhTokens.Color.Primary.Default,
        // ...
    };

    public static LhColors Dark() => new()
    {
        Primary = LhDarkTokens.Color.Primary.Default,
        // ...
    };
}
```

دوال المصنع عامة: يمكن لاختبارات xUnit بناء لوحة الألوان مباشرة للتحقق (راجع `Dark_Colors_Come_From_Dark_Tokens` في كل اختبار)، ويستهلكها جانب التطبيق عبر `LhTheme`.

## LhTheme.Apply

```csharp
LhTheme.Apply(isDark);  // تبديل لوحة الألوان وإطلاق ThemeChanged
```

تشترك المكوّنات في `LhTheme.ThemeChanged` عند `Loaded` وتلغي اشتراكها عند `Unloaded`، وبعد استلام الحدث تستدعي `ApplyAppearance()` لإعادة الحساب وإعادة الرسم — **إعادة رسم السمة ساخنة دون إعادة بناء شجرة الصفحة**. وعند عدم استدعاء Apply إطلاقًا يرتد `LhTheme.Current` إلى لوحة الألوان الفاتحة.

## الحساب المسبق للداكن (مهم)

يستخدم طرف الويب `color-mix()` في CSS للتقييم اللحظي، بينما لا يوجد تسلسل CSS في الأصل، لذلك قام core بحساب تعبيرات الداكن
**مسبقًا كقيم حرفية** (ثوابت `Color` على شكل `#AARRGGBB`). النتائج (الخطة الرئيسية R-n6):

> **عند تغيير اللون الرئيسي للمسبق، لا تتبع قيم الحساب المسبق للداكن التغيير تلقائيًا**، بل يجب إعادة حسابها يدويًا وتحديث
> `lh-ui-core/tokens/color-dark.json`، وتحديث *خط أساس لغة التصميم*.

## اصطلاحات خاصة بطرف Windows

- **وحدة الأحجام هي epx**: يُعلّم `LhEpx.Px(token)` قيمة الرمز كبيكسلات فعّالة (ترتبط البيكسلات الفعّالة في WinUI
  بمعامل التكبير، وتُوحَّد عبر دالة التوسيم، فلا تظهر قيم رقمية حرفية عارية داخل المكوّنات).
- **نوع قيمة اللون هو `Windows.UI.Color`**: جميع حقول `LhColors` من النوع `Color`، ويغلّفها المكوّن في
  `SolidColorBrush`؛ ويُخرج codegen قيم `#AARRGGBB` مباشرة بصيغة `Color.FromArgb(...)`.
- **وزن الخط يحتاج إلى مطابقة**: قيم `typography.fontWeight.*` أرقام، بينما `FontWeights` في WinUI
  ثوابت معرّفة مسبقًا، وتُوحَّد عبر `LhFontWeight.FromToken(token)` (`Common/LhFontWeight.cs`).
- **الظلال عبر Elevation**: تستخدم بطاقة محتوى Dialog ولوحة Picker `Border.Elevation` +
  `ThemeShadow` للتعبير عن المستويات (لغة Fluent: أزرار مسطحة، طبقات عائمة بظلال).
- **بصرية التركيز**: يأخذ تركيز حقل الإدخال اللون `focus` + حد `input.focusRingWidth` (يقابل
  إطار التركيز عالي الرؤية FocusVisualKind في WinUI).

## مرجع سريع للرموز

يوجد جدول الرموز الكامل (فاتح/داكن/شرح) في مستودع core ضمن `dist/docs/tokens.md` (يُولَّد بعد `pnpm codegen`).
ولتعيين رموز كل مكوّن راجع [مرجع رموز Button](/ar/components/button#مرجع-الرموز).
