---
layout: home

hero:
  name: lh-ui-windows
  text: سلالة أصلية · طرف Windows
  tagline: مكتبة مكوّنات بلغة C# + WinUI 3 (.NET 8 / Windows App SDK)، مدفوعة بالرموز ومطبّقة للعقود ومحمية بالحالات الذهبية
  actions:
    - theme: brand
      text: البدء السريع
      link: /ar/guide/quickstart
    - theme: alt
      text: نظرة على المكوّنات
      link: /ar/components/button

features:
  - title: مدفوعة بالرموز
    details: لا توجد قيم ألوان أو مسافات مشفّرة ثابتة داخل المكوّنات، بل تستهلك جميعها رموز LhTokens / LhDarkTokens التي يولّدها codegen الخاص بـ core، وتغيير الرمز يسري فورًا على كل الأطراف.
  - title: اتساق العقود
    details: تتطابق تسمية الخصائص حرفًا حرفًا مع lh-ui-core/specs، وتتشارك السلوك مع طرفي Android / iOS / HarmonyOS وسلالة الويب.
  - title: حماية الحالات الذهبية
    details: تُترجم تأكيدات سلوك cases/*.json إلى اختبارات xUnit، بانحدار كامل بأمر dotnet test واحد.
  - title: سمتان فاتحة وداكنة
    details: تبديل بسطر واحد عبر LhTheme.Apply (لوحة ألوان ثابتة + إعادة رسم ساخنة عبر حدث ThemeChanged)، ويحسب core رموز الداكن مسبقًا دون أي مزج ألوان في وقت التشغيل.
  - title: لغة تصميم بالنمط الصيني
    details: لون رئيسي 青花瓷 (الخزف الأزرق)، خلفية 宣纸白 (أبيض ورق الأرز)، لمسات 藤黄 (أصفر عشبي) و朱砂 (زنجفر)، حزمة خطوط مذيّلة، بنفس المصدر ونفس القيم كسلالة الويب.
  - title: WinUI 3 أصلي
    details: واجهة مكتوبة يدويًا بـ C# + WinUI 3 خالص، دون أي وقت تشغيل ويب JS، على رعاية نظام تصميم Fluent.
---
