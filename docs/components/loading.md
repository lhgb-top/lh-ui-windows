# Loading 加载指示器

环形旋转加载指示器。支持尺寸与语义色调。

> 契约源：`lh-ui-core/specs/loading.json` ·
> 黄金用例：`lh-ui-core/cases/loading.json`（5 条，已翻译为 xUnit 测试）

::: tip 端侧落点说明
旋转由 WinUI 的 `ProgressRing` 平台动画承担（朴素调性：只发几何，动画由平台补齐）——
令牌层只决定直径、描边宽与环色。
:::

## 尺寸

三档尺寸映射到 `loading.size.*` 直径令牌。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Loading 尺寸</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--small"></span>
      <span class="sim-loading"></span>
      <span class="sim-loading sim-loading--large"></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

```csharp
var loading = new LhLoading
{
    Size = LhLoadingSize.Large,
    Tone = LhLoadingTone.Primary,
};
```

## 色调

`Tone` 决定环色，与语义色令牌一一对应。

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Loading 色调</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--primary"></span>
      <span class="sim-loading sim-loading--success"></span>
      <span class="sim-loading sim-loading--warning"></span>
    </div>
    <div class="sim-row">
      <span class="sim-loading sim-loading--danger"></span>
      <span class="sim-loading sim-loading--info"></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 模拟预览（Windows / WinUI 3 外观），非真机渲染</p>

## API

### 属性

| 属性 | 说明 | 类型 | 默认值 |
| --- | --- | --- | --- |
| `Size` | 指示器直径，映射到 `loading.size.*` 令牌 | `LhLoadingSize` | `Medium` |
| `Tone` | 环色，取对应语义色令牌 | `LhLoadingTone` | `Primary` |

### LhLoadingSize

| 值 | 直径令牌 |
| --- | --- |
| `Small` | `loading.size.small`（20epx） |
| `Medium` | `loading.size.medium`（28epx） |
| `Large` | `loading.size.large`（36epx） |

### LhLoadingTone

| 值 | 环色令牌 |
| --- | --- |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

## 令牌对照

| 令牌 | 用途 |
| --- | --- |
| `loading.size.{small,medium,large}` | 三档直径 |
| `loading.strokeWidth` | 加载环描边宽 |
| `color.{primary,success,warning,danger,info}.default` | 五种环色 |

## 黄金用例对照

`cases/loading.json` 的 5 条断言与 xUnit 测试的对应关系（`tests/LhLoadingTests.cs`）：

| case id | 断言 | 本地测试 |
| --- | --- | --- |
| `medium-size-uses-medium-token` | 中号消费 medium 直径令牌 | `Medium_Size_Uses_Medium_Token` |
| `large-size-uses-large-token` | 大号消费 large 直径令牌 | `Large_Size_Uses_Large_Token` |
| `primary-tone-uses-primary-token` | 主色调取主色令牌 | `Primary_Tone_Uses_Primary_Token` |
| `success-tone-uses-success-token` | 成功色调取成功色令牌 | `Success_Tone_Uses_Success_Token` |
| `render-emits-ring-command` | 有且仅有一条 RING 指令 | `Render_Emits_Ring_Command` |

另有暗色专项：`Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`。
运行：`cd lh-ui-windows && dotnet test`。
