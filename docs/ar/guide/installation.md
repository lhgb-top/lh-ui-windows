# التثبيت

## متطلبات البيئة

- .NET 8 SDK (إطار العمل المستهدف `net8.0-windows10.0.19041.0`)
- Windows App SDK 1.6+ (Windows 10 1809+ أو Windows 11)
- أداة وقت البناء: Node.js >= 20 + pnpm (تُستخدم فقط لربط الرموز وموقع التوثيق، **ولا تدخل وقت التشغيل**)

## الإضافة عبر NuGet (بعد إتمام سلسلة النشر nM6)

```bash
dotnet add package LhUi.Windows
```

```xml
<ItemGroup>
  <PackageReference Include="LhUi.Windows" Version="0.0.1" />
</ItemGroup>
```

## التكامل من المصدر (المرحلة الحالية)

في تخطيط العائلة يتم الإشارة مباشرة إلى مكتبة هذا المستودع (يحتوي الحل على مكتبة `src/LhUi.csproj` + اختبارات `tests/` + تطبيق عرض `demo/`):

```
lh-ui-windows/
├── src/                  # مكتبة LhUi (Components/ + Common/ + Tokens/)
├── tests/                # حالات xUnit الذهبية
├── demo/                 # تطبيق العرض (عرض كل الأشكال + تبديل الفاتح/الداكن)
└── docs/                 # موقع توثيق VitePress
```

## مزامنة الرموز

الرموز منتجات مولّدة، ويجب مزامنتها من `lh-ui-core` (المصدر الأصلي الوحيد، يُمنع التعديل اليدوي):

```bash
cd ../lh-ui-core && pnpm codegen   # توليد منتجات رموز dist/windows
cd ../lh-ui-windows
pnpm install                        # تثبيت أداة وقت البناء
pnpm link:tokens                    # مزامنة الرموز إلى src/Tokens/
```

يجلب `pnpm link:tokens` حارس وجود من ثلاث مستويات: `../lh-ui-core/dist/windows` محليًا → منتج npm الخاص بـ `@lh-ui/core` → خطأ.

## التحقق من التثبيت

```bash
dotnet build     # ترجمة LhUi / LhUi.Tests / LhUi.Demo (يتضمن فحص أنواع C#)
dotnet test      # حالات xUnit الذهبية (15 ملفًا، تؤكد تحليل المظهر بدوال Resolve النقية)
```
