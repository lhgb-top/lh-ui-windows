# Instalación

## Requisitos del entorno

- SDK de .NET 8 (framework de destino `net8.0-windows10.0.19041.0`)
- Windows App SDK 1.6+ (Windows 10 1809+ o Windows 11)
- Cadena de herramientas de compilación: Node.js >= 20 + pnpm (se usa únicamente para el enlace de tokens y el sitio de documentación; **nunca entra en el runtime**)

## Integración mediante NuGet (una vez que esté lista la cadena de publicación nM6)

```bash
dotnet add package LhUi.Windows
```

```xml
<ItemGroup>
  <PackageReference Include="LhUi.Windows" Version="0.0.1" />
</ItemGroup>
```

## Integración por código fuente (fase actual)

Con la distribución de la familia, se referencia directamente la biblioteca de clases de este repositorio (la solución contiene la biblioteca de clases `src/LhUi.csproj` + las pruebas `tests/` + la app de demostración `demo/`):

```
lh-ui-windows/
├── src/                  # Biblioteca LhUi (Components/ + Common/ + Tokens/)
├── tests/                # Casos dorados xUnit
├── demo/                 # App de demostración (todas las variantes + alternancia claro/oscuro)
└── docs/                 # Sitio de documentación VitePress
```

## Sincronización de tokens

Los tokens son artefactos generados y deben sincronizarse desde `lh-ui-core` (la única fuente de verdad; está prohibido editarlos a mano):

```bash
cd ../lh-ui-core && pnpm codegen   # Genera los artefactos de tokens de dist/windows
cd ../lh-ui-windows
pnpm install                        # Instala la cadena de herramientas de compilación
pnpm link:tokens                    # Sincroniza los tokens a src/Tokens/
```

`pnpm link:tokens` lleva un guardián de existencia de tres niveles: el `../lh-ui-core/dist/windows` local → el artefacto npm `@lh-ui/core` → un error.

## Verificación de la instalación

```bash
dotnet build     # Compila LhUi / LhUi.Tests / LhUi.Demo (incluye la verificación de tipos de C#)
dotnet test      # Casos dorados xUnit (15 suites, afirman la resolución del aspecto con funciones puras Resolve)
```
