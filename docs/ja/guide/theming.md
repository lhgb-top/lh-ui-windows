# テーマとダークモード

## トークンモデル

ネイティブ血脈のトークン真源は `lh-ui-core/tokens/*.json` にあり、codegen が Windows 向けの成果物を生成します（`src/Tokens/`、生成物は gitignore 対象で、`pnpm link:tokens` が同期します）：

| 成果物 | 内容 | ダーク形態 |
| --- | --- | --- |
| `LhTokens` | 明色の全量：Button のサイズ、Color パレット、Motion、Radius、Shadow、Spacing、Typography、ZIndex | — |
| `LhDarkTokens` | `Color` サブクラスのみ（**並列オブジェクト**モデル） | ダークは色だけを上書きし、サイズ / モーション / 角丸は暗くしても変化しない |
| `LhShadow` | シャドウのキャリア（color/radius/offsetX/offsetY/spread） | — |
| `LhEasing` | モーションのイージング | — |

> これは android / ios / harmony と一致する（並列オブジェクト）形態であり、docs 端（浅いマージの JSON）とは異なります。
> テーマレイヤーはこれに従って設計されています：`LhColors` レコードは色だけを担い、サイズはコンポーネントが直接 `LhTokens` から取ります。

## LhColors

`LhColorsFactory.Light()` / `Dark()` は、生成された二つの静的クラスを一つのイミュータブルなレコードに平坦化します：

```csharp
public sealed record LhColors
{
    public Color Primary { get; init; }
    public Color OnPrimary { get; init; }
    // ... すべてのセマンティックカラー
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

ファクトリメソッドは公開されています：xUnit 単体テストはパレットを直接構築してアサーションでき（各テストの `Dark_Colors_Come_From_Dark_Tokens` を参照）、アプリケーション側は `LhTheme` を介して消費します。

## LhTheme.Apply

```csharp
LhTheme.Apply(isDark);  // パレットを切り替えて ThemeChanged を発火
```

コンポーネントは `Loaded` で `LhTheme.ThemeChanged` を購読し、`Unloaded` で解除します。イベントを受け取ると `ApplyAppearance()` を呼んで再計算・再描画します——**ページツリーを再構築せずにテーマがホットリフレッシュされます**。一度も Apply していない場合、`LhTheme.Current` は明色パレットにフォールバックします。

## ダークの事前計算（重要）

Web 血脈のダークは CSS の `color-mix()` で動的に評価しますが、ネイティブには CSS のカスケードがないため、core はダークの式を
**リテラルに事前計算**しています（`#AARRGGBB` 形式の `Color` 定数）。帰結（マスタープラン R-n6）：

> **プリセットのメインカラーを変更すると、事前計算されたダーク値は自動では追従しません。** 手作業で再計算して
> `lh-ui-core/tokens/color-dark.json` を更新し、『デザイン言語ベースライン』を刷新しなければなりません。

## Windows 端に固有の約束

- **サイズ単位は epx**：`LhEpx.Px(token)` がトークンの数値を有効ピクセルとしてマークします（WinUI の有効ピクセルは
  スケーリング係数に依存するため、必ずマーク関数を経由します。コンポーネント内に裸の数値リテラルは現れません）。
- **色値の型は `Windows.UI.Color`**：`LhColors` のフィールドはすべて `Color` で、ブラシはコンポーネントが
  `SolidColorBrush` に包みます。`#AARRGGBB` は codegen が直接 `Color.FromArgb(...)` として出力します。
- **フォントの太さはマッピングが必要**：トークンの `typography.fontWeight.*` は数値で、WinUI の `FontWeights` は
  定義済みの定数です。必ず `LhFontWeight.FromToken(token)` を経由します（`Common/LhFontWeight.cs`）。
- **シャドウは Elevation で表現**：Dialog の内容カード / Picker のパネルは `Border.Elevation` +
  `ThemeShadow` で階層を表します（Fluent の言語：ボタンはフラット、浮層にはシャドウ）。
- **フォーカスの見た目**：入力フィールドのフォーカスは `focus` 色 + `input.focusRingWidth` の枠線で表現します
  （WinUI の FocusVisualKind 高可視フォーカス枠に対応）。

## トークン早見表

完全なトークン表（明 / 暗 / 説明）は core リポジトリの `dist/docs/tokens.md`（`pnpm codegen` 後に生成）を参照してください。
コンポーネント別のトークンマッピングは [Button トークン对照](/ja/components/button#トークン对照)を参照してください。
