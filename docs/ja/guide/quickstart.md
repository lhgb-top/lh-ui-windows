# クイックスタート

## 最小サンプル

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui;
using Top.Lhgb.Ui.Common;

public sealed class DemoPage : Page
{
    public DemoPage()
    {
        // 明色パレットを適用；ダークに切り替えるときは true を渡す。すべての Lh コンポーネントが自動的に追随（LhTheme.ThemeChanged で再描画）
        LhTheme.Apply(false);

        var button = new LhButton
        {
            Type = LhButtonType.Primary,
            Text = "プライマリボタン",
        };
        button.Click += OnButtonClick;

        Content = new StackPanel { Children = { button } };
    }

    private void OnButtonClick() => { /* クリックを処理 */ }
}
```

`LhTheme.Apply(isDark)` は静的なカラーパレットを切り替えて `ThemeChanged` を発火し、読み込み済みのすべての Lh コンポーネントが自動的に再描画されます。呼び出さない場合は明色パレットにフォールバックします。

## タイプとサイズ

```csharp
// 6 種類のタイプ
new LhButton { Text = "デフォルト" };                                     // Default
new LhButton { Type = LhButtonType.Primary,   Text = "プライマリ" };
new LhButton { Type = LhButtonType.Success,   Text = "成功" };
new LhButton { Type = LhButtonType.Warning,   Text = "警告" };
new LhButton { Type = LhButtonType.Danger,    Text = "危険" };
new LhButton { Type = LhButtonType.Info,      Text = "情報" };

// 3 種類のサイズ
new LhButton { Size = LhButtonSize.Small,  Text = "小" };
new LhButton { Size = LhButtonSize.Medium, Text = "中" };   // デフォルト
new LhButton { Size = LhButtonSize.Large,  Text = "大" };
```

## 状態

```csharp
// 無効：クリックに応答せず（Tapped gate が傍受）、無効スタイルを表示
new LhButton { Disabled = true, Text = "無効" };

// 読み込み中：ProgressRing を表示し、Click を発火しない
new LhButton { Loading = true, Text = "読み込み中" };

// 両方指定：無効が優先される（ゴールデンケース disabled-precedes-loading）
new LhButton { Disabled = true, Loading = true, Text = "無効が優先" };
```

## アイコンと紋様背景

```csharp
// アイコン名はローカルのアイコン体系で解決される（LhButtonTokens.IconFor、現在は search / refresh / check / close をサポート、
// 未登録名は描画されない）
new LhButton { Type = LhButtonType.Primary, Icon = "search", Text = "検索" };

// 中華風の紋様背景（color.decoration トークンを消費）
new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = "紋様背景" };
```

## ライト / ダークテーマ

```csharp
LhTheme.Apply(true);  // 全体をダークに切り替え；すべてのコンポーネントが次回の外観解析で自動的に追随
```

## フォームとフィードバックのコンポーネント

```csharp
// コントロールされる入力：ホストが ValueChange を監視して Value に書き戻す
var input = new LhInput { Placeholder = "キーワードを入力してください" };
input.ValueChange += value => input.Value = value;

// チェックボックス
var checkbox = new LhCheckbox { Label = "青花瓷" };
checkbox.Change += isChecked => { };

// スイッチ
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };

// 命令的な通知：まずホスト側のコントロールを置き、コントローラーで駆動する
var toast = new LhToast();
toast.Close += () => { };
var panel = new StackPanel { Children = { toast } };
toast.Show("保存しました");   // 期限が来ると自動非表示になり Close を発火
```

## コンポーネントサンプルのプレビュー

ネイティブ UI はブラウザでリアルタイムレンダリングできないため、本サイトでは**インラインの静的 HTML 携帯フレームシミュレーター**で外観を示します：

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>lh-ui-windows</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">デフォルトボタン</button>
      <button class="sim-btn sim-btn--primary">プライマリボタン</button>
      <button class="sim-btn sim-btn--success">成功</button>
      <button class="sim-btn sim-btn--warning">警告</button>
      <button class="sim-btn sim-btn--danger">危険</button>
      <button class="sim-btn sim-btn--info">情報</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">小</button>
      <button class="sim-btn sim-btn--primary sim-btn--large">大</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">無効</button>
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>読み込み中
      </button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>検索</button>
      <button class="sim-btn sim-btn--primary sim-btn--pattern">紋様背景</button>
    </div>
  </div>
</div>
<p class="sim-note">Windows / WinUI 3 見た目の HTML シミュレーションプレビューであり、実機レンダリングではありません。スタイル値はすべて core のトークンスナップショットから取っており、実機と同じソース・同じ値です。実機の外観は demo デモアプリとデバイスのスクリーンショットを基準にしてください。</p>

> シミュレーターのダーク表示はサイトと連動します：右上のテーマを切り替えると、携帯フレーム内の色が即座に変わります（ダークトークンは core が事前計算）。
