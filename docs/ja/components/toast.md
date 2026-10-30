# Toast 通知

可視性とタイミングを自前で持ち、墨色のカプセルとして重なるオーバーレイです。セマンティックタイプ、自動非表示、位置指定をサポートします。

> 契約元：`lh-ui-core/specs/toast.json` ·
> ゴールデンケース：`lh-ui-core/cases/toast.json`（6 件、xUnit テストに翻訳済み）

## 基本的な使い方

命令的インターフェース（spec の show / hide / tick）はエンド側ではメソッド + 内部タイマーに実装されています。
`Show(text)` は可視にしタイマーをリセット、`Hide()` は Close を発火せず即座に非表示にします。
タイマーは `DispatcherTimer` が担い、期限が来ると自動で非表示になり `Close` を発火します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-toast">保存しました</div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

```csharp
var controller = new LhToastController();
var toast = new LhToast
{
    Duration = 2000,
    Position = LhToastPosition.Center,
    Controller = controller,
};
toast.Close += () => { /* タイマーの期限で自動非表示になった後に発火 */ };

// ホストは toast をページ Grid の最後の子ノードに置く必要があります（階層 zIndex.toast）
controller.Show("保存しました");
```

## セマンティックタイプ

`Type` が default 以外のときはテキストの前に、対応するセマンティック色の円点（直径はフォントサイズと同じ）を描画します。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast タイプ</span></div>
  <div class="sim-screen">
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--success"></span>保存に成功しました</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--warning"></span>ネットワーク接続を確認してください</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--danger"></span>削除に失敗しました</div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## 位置指定

`Position` が利用可能領域内での位置を決定します。上下の両端に `spacing.xl` の余白を取り（center のときは余白なし）ます。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast 位置</span></div>
  <div class="sim-screen">
    <div class="sim-toast sim-toast--top">上部の通知</div>
    <div class="sim-toast sim-toast--bottom">下部の通知</div>
  </div>
</div>
<p class="sim-note">HTML シミュレーションプレビュー（Windows / WinUI 3 の見た目）。実機レンダリングではありません</p>

## API

### プロパティ

| プロパティ | 説明 | 型 | デフォルト値 |
| --- | --- | --- | --- |
| `Close` | タイマーの期限で自動非表示になったときに発火。Hide() による明示的な非表示では発火しない | `event Action` | — |
| `Type` | セマンティックタイプ。default 以外のときテキストの前に対応するセマンティック色の円点を描画 | `LhToastType` | `Default` |
| `Duration` | 自動非表示までの時間（ミリ秒） | `int` | `2000` |
| `Position` | 利用可能領域内での位置 | `LhToastPosition` | `Center` |
| `Controller` | ホストが呼び出すハンドル（任意。Loaded 後に Show / Hide を登録） | `LhToastController?` | `null` |

### メソッド

| メソッド | 説明 |
| --- | --- |
| `Show(string text)` | 通知テキストを表示し、自動非表示のタイマーをリセットする |
| `Hide()` | 即座に非表示にするが Close は発火しない |

### LhToastType

| 値 | 円点の色 |
| --- | --- |
| `Default` | 円点を描画しない |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

### LhToastPosition

| 値 | 位置 |
| --- | --- |
| `Top` | 上部（上端に `spacing.xl` の余白） |
| `Center` | 中央（余白なし） |
| `Bottom` | 下部（下端に `spacing.xl` の余白） |

## トークン对照

| トークン | 用途 |
| --- | --- |
| `toast.fontSize` | テキストのフォントサイズ（色付き円点の直径も同じ値） |
| `toast.paddingHorizontal` / `paddingVertical` | カプセルのパディング |
| `toast.borderRadius` | カプセルの角丸 |
| `toast.gap` | 円点とテキストの間隔 |
| `color.ink` | カプセルの背景色 |
| `color.onPrimary` | テキスト色 |
| `color.{success,warning,danger,info}.default` | セマンティックな円点の色 |
| `zIndex.toast` | オーバーレイのレイヤー順 |
| `spacing.xl` | 上下両端の余白 |
| `typography.fontFamily` | 中華風セリフフォントスタック |

## ゴールデンケース对照

`cases/toast.json` の 6 件のアサーションと xUnit テストの対応関係（`tests/LhToastTests.cs`）：

| case id | アサーション | ローカルテスト |
| --- | --- | --- |
| `show-makes-visible` | show 後は可視になりテキストを描画 | `Show_Makes_Visible` |
| `auto-hides-after-duration` | タイマーの期限で自動非表示になり onClose を発火 | `Auto_Hides_After_Duration` |
| `tick-before-duration-keeps-visible` | 期限前は可視を保つ | `Tick_Before_Duration_Keeps_Visible` |
| `hide-is-silent` | hide による明示的な非表示は onClose を発火しない | `Hide_Is_Silent` |
| `success-type-uses-success-token` | success タイプがセマンティック色と墨色トークンを消費 | `Success_Type_Uses_Success_Token` |
| `hidden-emits-no-commands` | 非表示のときは描画コマンドを一切発行しない | `Hidden_Emits_No_Commands` |

さらにダーク専用のテストがあります：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
実行：`cd lh-ui-windows && dotnet test`。
