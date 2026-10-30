# Enlace local

## Convención de la distribución de la familia

```
lh-ui/                        # Directorio padre de la familia (no es un repositorio)
├── lh-ui-core/               # Fuente única de tokens + codegen
├── lh-ui-android/            # Repositorios hermanos
├── lh-ui-ios/
├── lh-ui-harmony/
└── lh-ui-windows/            # Este repositorio
```

Cuando todos los repositorios comparten directorio padre, los tokens se **enlazan localmente**; si un repositorio hermano no existe (clone de un único repositorio en CI), **se retrocede automáticamente al paquete npm**.
Es el mismo mecanismo de guardián del alias de vite del linaje web (plan maestro D18).

## Guardián de tres niveles

`scripts/link-tokens.mjs` busca los artefactos de tokens por orden de prioridad:

| Prioridad | Origen | Descripción |
| --- | --- | --- |
| 1 | `../lh-ui-core/dist/windows` | Enlace local: al cambiar un token, surte efecto de inmediato |
| 2 | `node_modules/@lh-ui/core/dist/windows` | Retroceso a npm: los `files:["dist"]` de `@lh-ui/core` incluyen los artefactos de cada extremo |
| 3 | — | Sale con error; sin tokens no se puede construir |

El destino de los tokens es `src/Tokens/` (el `relativePath` del codegen es `Tokens/*.cs`); desde el lado del componente,
`using Top.Lhgb.Ui.Tokens;` apunta a este directorio. Es un directorio de artefactos generados (`.gitignore`) y está prohibido editarlo a mano.

## Flujo diario

```bash
# 1. Cambiar los tokens (en el repositorio core)
cd ../lh-ui-core
vim tokens/color.json           # o spacing / typography / ...

# 2. Regenerar + verificar
pnpm codegen                    # Genera los artefactos de cada extremo en dist/
pnpm verify                     # schema + referencias + contraste WCAG + consistencia de contratos

# 3. Volver a este repositorio para sincronizar
cd ../lh-ui-windows
pnpm link:tokens                # Sobrescribe src/Tokens/

# 4. Regresión (dotnet)
dotnet test                     # Casos dorados xUnit
dotnet build                    # Compilar equivale a la verificación de tipos
```

## Tokens del sitio de documentación

Las variables CSS del simulador del sitio de documentación también se generan a partir de los tokens:

```bash
pnpm gen:sim                    # Genera docs/.vitepress/theme/sim-tokens.css
pnpm docs:dev                   # Internamente ejecuta primero gen:sim, puerto 5186
pnpm sim:check                  # Verifica que el CSS del simulador no contiene literales de tokens (línea roja)
```

## Guardián de CI

```bash
pnpm link:tokens:check          # Detecta si los tokens están sincronizados (comparación del número de archivos; si no, código de salida 1)
```

## Prohibiciones

- ❌ Editar a mano `src/Tokens/` (artefactos generados, gitignore)
- ❌ Confirmar los archivos de tokens generados en el repositorio
- ❌ Escribir literales de color / tamaño en los componentes (como `Color.FromArgb(0xFF,0x2B,0x4C,0x7C)`, `new GridLength(40)`, o el número del tamaño de fuente)
- ❌ Copiar los artefactos a mano saltándose `link:tokens`

## Resolución de problemas

| Síntoma | Causa | Solución |
| --- | --- | --- |
| La compilación reporta `The type or namespace 'LhTokens' does not exist` | Los tokens no están sincronizados | `pnpm link:tokens` |
| El valor de un token no es el esperado | El repositorio core no ha vuelto a ejecutar codegen | Ejecutar `pnpm codegen` en core y volver a enlazar |
| `link:tokens` reporta «no se encontraron artefactos de tokens» | Los repositorios hermanos no están y no hay paquete npm instalado | Comprobar la distribución de la familia o `pnpm i -D @lh-ui/core` |
| Los colores oscuros son incorrectos | Se cambió el color principal claro pero no se actualizó el precalculo oscuro | Actualizar `color-dark.json` y volver a ejecutar (R-n6) |
| Todos los colores del componente son claros | El punto de entrada no llamó a `LhTheme.Apply` | Llamarlo una vez al inicio de la App / ventana principal |
| `dotnet build` reporta errores XAML tipo `MC1000` | Los componentes construyen el árbol visual por código; no hay XAML en el repositorio | Comprobar si se ha añadido por error un archivo `.xaml` |
