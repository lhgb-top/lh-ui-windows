# Introduction

**lh-ui-windows** is the Windows implementation of the lh-ui family's **native bloodline**: a UI component library built on **C# + WinUI 3** (.NET 8, Windows App SDK).

## Position in the family

```
                        ┌── web bloodline: share (TS core + SCSS) → plus / vue2 / react / uniapp
lh-ui family (one brand)├────┤
                        └── native bloodline: core (JSON tokens + specs + codegen) → android / ios / harmony / windows
                                                                    ↑ this repo
```

- **Build-time center**: `lh-ui-core` is the native bloodline's token source of truth (`tokens/*.json`), component specs (`specs/`), golden cases (`cases/`) and codegen tooling. It generates the C# token skeletons (`LhTokens` / `LhDarkTokens` / `LhShadow` / `LhEasing`) but **generates no component implementation**.
- **This repo's responsibilities**: hand-written WinUI 3 components, theme integration (`LhTheme.Apply` + the `ThemeChanged` event), prop enums and default-value binding, and translating the golden cases into xUnit tests.
- **Independent from the web bloodline**: it does not depend on `@lh-ui/share`, and releases constrain each other in no way. Visual consistency is kept by manually aligning against the *Design Language Baseline*.

> **History**: this repo replaced the former `lh-ui-c` (C/C++ embedded end) on 2026-10-12 — the native bloodline's fourth member changed from an embedded end to the Windows desktop end (master plan D10).

## Design language

A Chinese-style theme: 青花瓷 (ceramic blue) primary (`#2B4C7C`), 宣纸白 (rice-paper white) background (`#FAF6F0`), 松花绿 (pine-flower green) for success, 藤黄 (gamboge) for warning, 朱砂 (cinnabar) for danger, and a serif font stack (Noto Serif SC → STSong → SimSun). Every visual value is a token; no literal values appear inside components. Visually it is capped by Fluent's geometric restraint (a token-driven corner-radius hierarchy and thin strokes).

## Why WinUI 3

- The Windows App SDK's modern declarative UI, mentally isomorphic with Compose / SwiftUI / ArkUI, which makes cross-platform spec alignment easy.
- No web runtime, no HTML/CSS bridging — native performance.
- NuGet distribution (once the nM6 pipeline is through), one-step integration into the .NET ecosystem.

## Current status

- **All 15 P0 components delivered** (token consumption + resolve pure-function appearance resolution + WinUI 3 controls + xUnit golden cases + a demo app + this docs site): button / icon / tag / cell / text / input / textarea / checkbox / radio / switch / picker / dialog / toast / loading / navbar.
