# Cell 儲存格

清單的列單元。左側圖示 + 標題（可帶必填紅星）+ 說明，右側值與指示箭頭，底部可選分隔線。

> 契約源：`lh-ui-core/specs/cell.json` ·
> 黃金用例：`lh-ui-core/cases/cell.json`（6 條，已翻譯為 xUnit 測試）

## 基礎用法

標題取 `color.text.primary`，值與說明取 `color.text.secondary`，三者都由令牌驅動。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Cell 基礎</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">列標題</div>
        <div class="sim-cell__label">說明文字</div>
      </div>
      <div class="sim-cell__value">列內容</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">帶線儲存格</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var cell = new LhCell
{
    Title = "列標題",
    Label = "說明文字",
    Value = "列內容",
};
```

## 圖示與必填

`Icon` 傳入本機圖示名（見 Icon 文件的字形表），在左側繪製；
`Required` 為 true 時標題前繪製 `color.danger.default` 的紅星。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>圖示與必填</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <span class="sim-cell__icon">★</span>
      <div class="sim-cell__main">
        <div class="sim-cell__title">帶圖示</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title"><span class="sim-cell__required">✱</span>必填項</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

## 分隔線與點選

`Bordered` 為 true 時底部繪製 `color.border.lighter` 分隔線；`Clickable` 為 true 時
右側繪製指示箭頭且點選觸發 `Click` 事件。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>分隔線與點選</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">可點</div></div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">不可點</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染</p>

```csharp
var cell = new LhCell
{
    Title = "可點",
    Clickable = true,
    Bordered = true,
};
cell.Click += () => { /* 僅 Clickable 為 true 時觸發 */ };
```

## 明暗主題

切換站台右上角主題，模擬器手機框內的文字色與分隔線色即變；真機透過 `LhTheme.Apply(true)` 切換。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>暗色儲存格</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">列標題</div>
        <div class="sim-cell__label">說明文字</div>
      </div>
      <div class="sim-cell__value">列內容</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模擬預覽（Windows / WinUI 3 外觀），非真機渲染；明/暗值全部取自 core 令牌快照</p>

## API

### 屬性

| 屬性 | 說明 | 型別 | 預設值 |
| --- | --- | --- | --- |
| `Click` | 點選事件；僅 `Clickable` 為 true 時觸發 | `event Action` | — |
| `Title` | 標題（spec 的 title 插槽） | `string` | `""` |
| `Label` | 標題下說明文字（label 插槽） | `string` | `""` |
| `Value` | 右側值文字（value 插槽） | `string` | `""` |
| `Icon` | 左側圖示名（本機圖示表解析） | `string?` | `null` |
| `Required` | 是否在標題前繪製必填紅星 | `bool` | `false` |
| `Clickable` | 是否可點選（顯隱箭頭、gate Click） | `bool` | `false` |
| `Bordered` | 是否繪製底部分隔線 | `bool` | `true` |

## 令牌對照

| 令牌 | 用途 |
| --- | --- |
| `cell.paddingHorizontal` / `cell.paddingVertical` | 內邊距 |
| `cell.gap` | 標題與說明的垂直間距 |
| `cell.fontSizeTitle` / `fontSizeLabel` / `fontSizeValue` | 三檔字級 |
| `color.text.{primary,secondary,regular}` | 標題/說明/值/圖示色 |
| `color.danger.default` | 必填紅星 |
| `color.border.lighter` | 底部分隔線 |
| `icon.size.medium` | 左側圖示尺寸 |
| `spacing.{sm,xs}` | 圖示間距 / 內部元素間距 |
| `typography.lineHeight.primary` | 文字行高 |

## 黃金用例對照

`cases/cell.json` 的 6 條斷言與 xUnit 測試的對應關係（`tests/LhCellTests.cs`）：

| case id | 斷言 | 本地測試 |
| --- | --- | --- |
| `title-and-value-render` | 標題與值都被渲染 | `Title_And_Value_Render` |
| `label-renders-secondary-tone` | 說明文字取次要文字色令牌 | `Label_Renders_Secondary_Tone` |
| `required-uses-danger-token` | 必填紅星消費危險色令牌 | `Required_Uses_Danger_Token` |
| `bordered-emits-line-command` | 分隔線形態發出 LINE 指令 | `Bordered_Emits_Line_Command` |
| `clickable-emits-onclick` | 可點選時點選觸發 onClick | `Clickable_Emits_OnClick` |
| `not-clickable-no-onclick` | 非可點選時不觸發 onClick | `Not_Clickable_No_OnClick` |

另有暗色專項：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
執行：`cd lh-ui-windows && dotnet test`。
