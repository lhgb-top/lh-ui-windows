# Local Linking

## Family layout convention

```
lh-ui/                        # Family parent directory (not a repo)
├── lh-ui-core/               # Token source of truth + codegen
├── lh-ui-android/            # Sibling repos
├── lh-ui-ios/
├── lh-ui-harmony/
└── lh-ui-windows/            # This repo
```

When the repos share one parent directory, tokens are **linked locally**; when the sibling repos are absent (a CI
single-repo clone), it **falls back to the npm package** automatically. This is structurally identical to the web
bloodline's vite alias guard (master plan D18).

## Three-level guard

`scripts/link-tokens.mjs` resolves the token artifacts by priority:

| Priority | Source | Notes |
| --- | --- | --- |
| 1 | `../lh-ui-core/dist/windows` | Local linking: change a token and it takes effect immediately |
| 2 | `node_modules/@lh-ui/core/dist/windows` | npm fallback: `@lh-ui/core`'s `files:["dist"]` ships every platform's artifacts |
| 3 | — | Errors out — without tokens there is nothing to build against |

Tokens land in `src/Tokens/` (codegen's `relativePath` is `Tokens/*.cs`); on the component side,
`using Top.Lhgb.Ui.Tokens;` points here. This directory is generated (`.gitignore`) — hand-editing is forbidden.

## Daily workflow

```bash
# 1. Change tokens (in the core repo)
cd ../lh-ui-core
vim tokens/color.json           # or spacing / typography / ...

# 2. Regenerate + verify
pnpm codegen                    # Regenerates the dist/ artifacts for every platform
pnpm verify                     # schema + references + WCAG contrast + spec consistency

# 3. Sync back to this repo
cd ../lh-ui-windows
pnpm link:tokens                # Overwrites src/Tokens/

# 4. Regression (dotnet)
dotnet test                     # xUnit golden cases
dotnet build                    # Compiling is the type check
```

## Docs-site tokens

The docs site's simulator CSS variables are also generated from tokens:

```bash
pnpm gen:sim                    # Generates docs/.vitepress/theme/sim-tokens.css
pnpm docs:dev                   # Runs gen:sim first internally; port 5186
pnpm sim:check                  # Verifies the sim CSS contains no token literals (red line)
```

## CI gate

```bash
pnpm link:tokens:check          # Checks whether tokens are synced (file-count comparison; exits 1 when out of sync)
```

## Forbidden

- ❌ Hand-editing `src/Tokens/` (generated, gitignored)
- ❌ Committing generated token files to the repo
- ❌ Writing color/spacing literals inside components (e.g. `Color.FromArgb(0xFF,0x2B,0x4C,0x7C)`, `new GridLength(40)`, or raw font-size numbers)
- ❌ Copying artifacts manually instead of running `link:tokens`

## Troubleshooting

| Symptom | Cause | Fix |
| --- | --- | --- |
| Compile error `The type or namespace 'LhTokens' does not exist` | Tokens not synced | `pnpm link:tokens` |
| Token values do not match expectations | The core repo was not re-run through codegen | `pnpm codegen` in the core repo, then link again |
| `link:tokens` reports "token artifacts not found" | Sibling repos absent and no npm package installed | Check the family layout or `pnpm i -D @lh-ui/core` |
| Dark colors are wrong | Light primary changed but the dark pre-computation was not updated | Update `color-dark.json` and re-run (R-n6) |
| Component colors are all light | The entry point never called `LhTheme.Apply` | Call it once at App / main window startup |
| `dotnet build` reports `MC1000` and other XAML errors | Components build their visual trees in code; the repo ships no XAML | Check whether a `.xaml` file was added by mistake |
