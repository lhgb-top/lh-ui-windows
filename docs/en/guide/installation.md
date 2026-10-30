# Installation

## Requirements

- .NET 8 SDK (`net8.0-windows10.0.19041.0` target framework)
- Windows App SDK 1.6+ (Windows 10 1809+ or Windows 11)
- Build-time toolchain: Node.js >= 20 + pnpm (used only for token linking and the docs site; **never enters the runtime**)

## Adding via NuGet (once the nM6 publishing pipeline is through)

```bash
dotnet add package LhUi.Windows
```

```xml
<ItemGroup>
  <PackageReference Include="LhUi.Windows" Version="0.0.1" />
</ItemGroup>
```

## Source integration (current stage)

With the family layout, reference this repo's class library directly (the solution contains the `src/LhUi.csproj` class library + `tests/` + a `demo/` app):

```
lh-ui-windows/
├── src/                  # LhUi class library (Components/ + Common/ + Tokens/)
├── tests/                # xUnit golden cases
├── demo/                 # Demo app (all variants + light/dark toggle)
└── docs/                 # VitePress docs site
```

## Syncing tokens

Tokens are generated artifacts and must be synced from `lh-ui-core` (the single source of truth — hand-editing is forbidden):

```bash
cd ../lh-ui-core && pnpm codegen   # Generates the dist/windows token artifacts
cd ../lh-ui-windows
pnpm install                        # Installs the build-time toolchain
pnpm link:tokens                    # Syncs tokens into src/Tokens/
```

`pnpm link:tokens` carries a three-level existence guard: local `../lh-ui-core/dist/windows` → the npm `@lh-ui/core` artifact → an error.

## Verifying the installation

```bash
dotnet build     # Compiles LhUi / LhUi.Tests / LhUi.Demo (C# type checking included)
dotnet test      # xUnit golden cases (15 suites, asserting the Resolve pure-function appearance resolution)
```
