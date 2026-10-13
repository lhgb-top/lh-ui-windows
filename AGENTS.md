# lh-ui-windows —— AGENTS.md

> `@lh-ui/windows` · C# + WinUI 3（.NET 8，Windows App SDK）· **原生血脉**成员之一。
> 权威规范：本文件 > 家族根 `AGENTS.md` > 总纲 `docs/多端组件库规划.md`。

## 一、定位与血脉

- **原生血脉**：`lh-ui-core`（JSON 令牌 + 契约 + 黄金用例 + codegen）→ android / ios / harmony / windows。
- 本仓是 Windows 端 UI 层：**C# + WinUI 3 手写 UI**，运行时零 JS、零 npm 依赖（npm 只服务构建期工具链）。
- 与 web 血脉（`@lh-ui/share` → plus/vue2/react/uniapp）**互相独立、不得关联**（总纲 D13）。本仓不依赖
  `@lh-ui/share`，web 仓也不依赖本仓。视觉一致性靠《设计语言基线.md》人工对齐。
- 唯一外部令牌来源：core codegen 生成的 `LhTokens` / `LhDarkTokens` / `LhShadow` / `LhEasing`
  （`src/Tokens/` 目录，生成物）。本仓取代已于 2026-10-12 废弃的 `lh-ui-c`（C/C++ 嵌入式端）。

## 二、目录职责

```
lh-ui-windows/
├── lh-ui-windows.sln          # 解决方案：LhUi 类库 + LhUi.Tests + LhUi.Demo + LhUi.DocsApp
├── package.json               # 构建期工具脚本入口（非运行时）
├── scripts/
│   ├── link-tokens.mjs        # 令牌本地连接（三级存在性守卫 + 逐文件字节比对，D18）
│   ├── gen-sim-tokens.mjs     # 文档模拟器令牌 CSS 生成（D19）
│   ├── gen-doc-pages.mjs      # 原生文档页面生成（md → src/Docs/pages，D21）
│   └── check-sim-vars.mjs     # 模拟器 var(--lh-*) 变量名守卫（CI 门禁）
├── src/                       # LhUi 类库（net8.0-windows10.0.19041.0，UseWinUI）
│   ├── LhUi.csproj            #   类库工程
│   ├── LhUi.cs                #   聚合导出（只导出不实现）
│   ├── Tokens/                #   【生成物，gitignore】link-tokens 落点：LhTokens/LhDarkTokens/LhShadow/LhEasing
│   ├── Common/                #   LhColors 色板 + LhTheme 主题工具 + LhEpx 尺寸标记 + LhColor 构造
│   ├── Components/<Comp>/     #   每个组件两文件，职责固定：
│   │   ├── Lh<Comp>Tokens.cs  #     枚举 + Lh<Comp>Appearance 记录 + Lh<Comp>Tokens.Resolve 纯函数
│   │   └── Lh<Comp>.cs        #     WinUI 3 控件（UserControl，属性与 spec 逐字对齐，只画外观）
│   └── Docs/                  #   原生文档模块（纯 C# 代码后端，零 .xaml）：
│       ├── LhDocs.cs          #     公共入口：Show() 开窗口 / Show(window) 嵌入
│       ├── LhDocsHost.cs      #     宿主：分组侧栏 + 手写返回栈 + LhNavbar dogfood + 暗色 + 全局 LhToast
│       ├── DocRegistry.cs     #     【生成物】页面清单（顺序对齐 config.ts 侧边栏）
│       ├── model/             #     DocEntry / DocSection / DocTable / DocPreviewScope
│       ├── widgets/           #     DocWidgets（标题/正文/卡片/代码块/表格）+ DocPreviewFrame（手机框）
│       ├── pages/             #     【生成物，禁手改】gen-doc-pages.mjs 产出
│       └── previews/          #     手写真 WinUI 3 交互预览（SCENE_MAP：md 的 ## 标题 → 方法名）
├── tests/                     # xUnit 黄金用例（LhUi.Tests.csproj，每组件一份 Lh<Comp>Tests.cs）
├── demo/                      # WinUI 演示应用（LhUi.Demo.csproj，15 组件分栏 + 明暗切换）
├── docsapp/                   # 极薄 WinExe（LhUi.DocsApp.csproj）：入口点 + 承载 LhDocsHost（D21）
└── docs/                      # VitePress 文档站（内联静态 HTML 模拟器，D19）
    ├── .vitepress/config.ts   #   数据驱动侧边栏；DOCS_BASE 显式读 env；search 放顶层 themeConfig
    └── .vitepress/theme/      #   style.css + sim/base.css + sim/windows.css + sim-tokens.css（生成物）
```

**已落地组件**（15 个，按 `LhUi.cs` 的分组）：

- 基础：`button` / `icon` / `tag` / `cell` / `text`
- 表单：`input` / `textarea` / `checkbox` / `radio` / `switch` / `picker`
- 反馈：`dialog` / `toast` / `loading`
- 导航：`navbar`

P0 全 15 组件已齐备，无 spec / 实现缺口。text 只消费全局排版与颜色令牌（零组件级令牌）。

## 三、红线

1. **令牌唯一源**：组件实现禁止出现任何色值 / 间距 / 圆角 / 字号字面量（`Color.FromArgb(...)`、
   `new Thickness(16,0,16,0)`、字号数字等），只能引用 `LhTokens.Button.Height.Medium` /
   `LhColors` 工厂。违例即 drift。尺寸令牌经 `LhEpx.Px(...)` 消费（`Common/LhEpx.cs`），
   它在运行时是恒等函数，存在的意义就是把「这里消费的是令牌数值」写在调用点上。
2. **core 生成的令牌文件禁止手改**：`src/Tokens/` 是生成物（gitignore），改令牌去
   `lh-ui-core/tokens/*.json` → `pnpm codegen` → 本仓 `pnpm link:tokens`。
3. **只生成骨架不生成 UI**：core 不会、也不应生成任何控件实现；本仓的 WinUI 组件全部手写。
4. **血脉隔离**：禁止引用任何 `@lh-ui/share` / web 仓符号。
4b. **颜色构造统一走 `LhColor`，禁止 `Microsoft.UI.Colors.*`**：后者是 WinRT 静态类，取属性
   要走激活工厂，在测试宿主（非打包上下文，无 WinUI 注册表项）里抛 `REGDB_E_CLASSNOTREG`
   （0x80040154），见 bugfix-log A-06 附 3。`Windows.UI.Color` 是纯值类型结构体，`FromArgb`
   不涉及激活，任何上下文都能用；透明色等常量取 `Common/LhColor.cs` 的 `LhColor.Transparent`。
5. **黄金用例对齐**：组件行为必须满足 `lh-ui-core/cases/<comp>.json`，并人工翻译为 xUnit 测试
   （断言 `Resolve` 纯函数的外观解析 + 状态 gate）。
6. **命名与 spec 对齐**（R-n5）：属性名严格沿用 `lh-ui-core/specs/<comp>.json` 的驼峰命名
   （`Type` / `Size` / `Disabled` / `Loading` / `Icon` / `BgPattern`），C# 侧 PascalCase，
   不得自创 `Variant` / `Dimension` 等。spec 的 `onClick` 事件落 `event Action Click`。
7. **新增组件必须同时更新三个聚合点**：`src/LhUi.cs`（聚合导出组件 + 枚举 + Tokens 类；
   测试与 demo 都经项目引用 `Top.Lhgb.Ui` 命名空间导入，漏导出符号会直接编译失败）、
   `tests/Lh<Comp>Tests.cs`（黄金用例翻译）、
   `docs/.vitepress/config.ts` 的 `componentGroups`（侧边栏数据驱动，加一条才有文档入口）。
   另需同步：demo 的分栏演示、`docs/components/<comp>.md`、
   `docs/.vitepress/theme/sim/windows.css` 的 `.sim-<comp>` 样式、
   `src/Docs/previews/<Comp>Previews.cs`（真交互预览，方法名按 SCENE_MAP 对齐 md 的 `##` 标题）。
8. **`src/Docs/pages/` 与 `DocRegistry.cs` 是生成物，禁手改**：由 `scripts/gen-doc-pages.mjs`
   从 `docs/components/*.md` + `docs/guide/*.md` 产出（每个文件首行带「禁手改」头）。
   改文档改 md 再跑 `pnpm gen:docs`；`previews/` 才是手写区。生成物里若发现需要改的内容，
   说明 md 或 SCENE_MAP 有缺口，改源头而非改产物（与红线 2 同一原则）。

## 四、令牌消费机制（D18）

core 的 codegen 产物是 `LhTokens`（明）与 `LhDarkTokens`（暗）两个静态类。

- **暗色模型是并列对象**：`LhDarkTokens` 只含 `Color` 子类，尺寸类令牌暗色不覆盖
  （与 android / ios / harmony 一致；docs 端是浅合并）。主题层据此设计：`LhColors` 只承载
  颜色，尺寸直接取 `LhTokens`（尺寸无暗色变体，暗色取明色对象的值）。
- **本地连接**：`pnpm link:tokens`（脚本带三级守卫：本地 `../lh-ui-core/dist/windows` →
  npm `@lh-ui/core/dist/windows` → 报错）。`--check` 是 CI 门禁，做**文件名集合 + 逐文件字节**
  比对。禁止把生成的令牌文件提交入库。
- **回退形态**：家族布局不存在时（CI 单仓 clone），`@lh-ui/core` npm 包的 `files:["dist"]`
  含各端产物。NuGet 令牌包回退在 nM6 打通后接入。

## 五、命名规范（总纲 D20）

- **平台包名一律反域名**：MSIX 包标识 / 应用标识为 `top.lhgb.ui.windows.demo`；
  命名空间一律 `Top.Lhgb.Ui.*`（`Top.Lhgb.Ui.Components.Button`、`Top.Lhgb.Ui.Common`、
  `Top.Lhgb.Ui.Tokens`）。npm 的 `@lh-ui/*` scope 不受反域名约束。
- **自有标识符一律 LH 前缀**：类 / 枚举 / 记录 / 组件 `Lh*`（`LhButton`、`LhButtonType`、
  `LhButtonAppearance`），工具方法 `Lh*`（`LhEpx.Px`）；框架约定的模板名（`MainWindow`、
  `App`）除外。resolve 纯函数与外观记录遵循 `Lh<Comp>Tokens` / `Lh<Comp>Appearance` 命名，
  `<Comp>` 用 spec 文件名的大驼峰形式。
- **令牌成员 PascalCase**：codegen 的 `csMemberName` 把 `paddingHorizontal` → `PaddingHorizontal`、
  `default` → `Default`；分组名同理（`color` → `Color`）。引用旧产物若见小写成员即过期产物。

## 六、平台 gotcha

- **单位**：尺寸是 effective pixels（逻辑像素），令牌数值是裸 double，直接用作
  `Width` / `Height` / `Thickness` / `FontSize`。暗色对象不含尺寸令牌，取尺寸时始终走明色对象。
- **时长单位是毫秒**（int）：`LhTokens.Motion.Duration.Base = 300`（iOS 是秒）。
  组件侧 `TimeSpan.FromMilliseconds(...)`；`DispatcherTimer.Interval` 同样换算。
- **色值表达（两件事，勿混）**：
  ① 结构体是 `Windows.UI.Color`，令牌由 codegen 产出 `Windows.UI.Color.FromArgb(alpha, r, g, b)`
  字面量（`src/Tokens/LhTokens.cs` 全文如此）。`FromArgb` 是值类型工厂，不涉及激活，任何上下文都能用。
  ② `Microsoft.UI.Colors` 是 WinRT **静态类**，取其属性（如 `Microsoft.UI.Colors.Transparent`）要走激活
  工厂，测试宿主里会抛 `REGDB_E_CLASSNOTREG`（0x80040154，bugfix-log A-06 附 3）——红线 4b 禁止用它。
  需要透明色等常量时取 `Common/LhColor.cs` 的 `LhColor.Transparent`（值类型构造，等价但安全）。
  注意 `Windows.UI.Color` 与 `Microsoft.UI.Color` 是同一个结构体的两个命名空间别名，可互换；
  而 `Colors` 静态表（`Microsoft.UI.Colors`）才是危险的那一个。
- **贝塞尔与投影是生成记录**：`LhEasing`（X1/Y1/X2/Y2）与 `LhShadow`（Color/OffsetX/OffsetY/BlurRadius）
  是 `Top.Lhgb.Ui.Tokens` 命名空间下的 `record struct`，组件侧按需转 `SplineEase` /
  `DropShadow`；投影的 `spread` WinUI 不支持，已忽略并注释。
- **圆角**：`Radius.Circle` 是 0.5 分数（不是长度），端侧取 `Min(宽, 高) / 2` 换算，
  不能当 epx 用。胶囊形态同理取高度一半。
- **主题消费模式**：组件不直接持有色板，而是订阅 `LhTheme.ThemeChanged`（`Loaded` 订阅、
  `Unloaded` 退订），事件触发时调 `ApplyAppearance()` 重算并重绘（主题热重绘）。
  颜色走 `LhTheme.Current`（`LhColors`），尺寸直接取 `LhTokens`。未 `Apply` 时回退明色板。
- **指令式组件在声明式 UI 中的落点**：spec 的 show/hide 类指令（toast / dialog / picker）
  实现为控件 public 方法 + `Lh<Comp>Controller` 句柄：控件在 `Loaded` 把方法注册到宿主传入的
  控制器上，宿主即可在不持有控件实例的情况下驱动。计时模型（tick/hide）由 `DispatcherTimer` 承载。
- **依赖属性改外观**：所有影响外观的属性注册时带 `PropertyChangedCallback`，回调里调
  `ApplyAppearance()`；回调签名固定 `static void(DependencyObject, DependencyPropertyChangedEventArgs)`。
- **图标是字形占位**：`LhIconTokens.GlyphFor` 把图标名映射为 unicode 字形、用 `TextBlock` 渲染
  （与 button 的占位策略一致）。生产环境应换 `FontIcon`（Segoe Fluent Icons），此处保持务实占位。
- **模拟器变量名保留驼峰**：`gen-sim-tokens.mjs` 生成的 CSS 变量名沿用令牌点路径的驼峰
  （`--lh-button-paddingHorizontal-medium`），手写 CSS 按 kebab 习惯写会**静默失配**（变量未定义、
  样式悄悄失效）。`pnpm sim:check` 把这类失配拦在 CI。
- **三个示例现场，各有分工**（D21）：
  ① **原生文档应用**（`src/Docs/` + `docsapp/`，`pnpm docs:app`）——**真交互主场**：页面正文
  由 md 生成（`pnpm gen:docs`），预览是真实 WinUI 3 控件树（`previews/` 手写），宿主 dogfood
  自家 LhNavbar / LhToast 并带手写返回栈与明暗热重绘。它是唯一能同时验证「文档-组件-令牌」
  三层一致性的运行时，改动组件后优先来这里点一遍。
  ② demo 演示应用（`demo/`）——组件示例的速查分栏，覆盖全部 15 个组件与明暗热重绘。
  ③ 文档站 HTML 模拟器（`docs/components/*.md` 每组件一页，`pnpm docs:dev`）——网页端静态模拟，
  给未装 .NET 的读者看外观。
  三处现场与组件实现同步，新增组件时按红线 7 一并补齐。
- **docs 模块零 .xaml 是硬约束**：`src/Docs/` 全部是 C# 代码后端（控件树手写，无 XAML / 无 .resw）。
  一旦引入 .xaml，PRI 生成链（本仓在缺 VS 工作负载时已由 `AppxGeneratePriEnabled=false` 关掉，
  见 `src/LhUi.csproj` 的环境守卫）就会要求 XAML 编译产物，破坏已打通的纯命令行构建。
  要加文档构件，写代码后端，不写 XAML。
- **生成器的 C# 字符串转义用 `\"` 不用 `""`**：`gen-doc-pages.mjs` 把 md 文本写进**常规**
  字符串字面量（非 `@"..."` verbatim），双引号必须转义为 `\"`。写成 `""` 会被 Roslyn 切成
  两个相邻字面量，在数组初始化器里直接 CS1003（此坑已修复并写进 `esc()` 注释，勿回退）。
- **测试宿主是非打包上下文**：`dotnet test` 跑的是普通控制台宿主，没有 WinUI 3 的注册表注册项，
  因此 (a) WinRT 静态取色（`Microsoft.UI.Colors.*`）会抛 `REGDB_E_CLASSNOTREG`（红线 4b）、
  (b) WindowsAppSDK 的 targets 把 `CopyLocalLockFileAssemblies` 置为 `false`，导致 testhost
  依赖的 `Microsoft.TestPlatform.*` 不落盘，启动即 `FileNotFoundException`。tests 工程已显式
  开回 `<CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>`；测试只断言
  `Tokens.Resolve(...)` 纯函数与 `LhColors` 工厂，不实例化需要 XAML 树的控件，保持宿主无关。
- **纯代码 App（无 App.xaml）要手写 Main**：WindowsPackageType=None 且无 App.xaml 时 WinUI 3
  不会自动生成入口点，需 `demo/Program.cs` 的 `[STAThread] static Main`，在 `Application.Start`
  回调里先切到 `DispatcherQueueSynchronizationContext` 再构造 `App`。另：`Window` 没有
  `Loaded` 事件（那是 `FrameworkElement` 的），窗口级初始化直接写在构造函数里。

## 七、命令速查

| 任务 | 命令 | 说明 |
| --- | --- | --- |
| 同步令牌 | `pnpm link:tokens` | 从 core dist 同步到 `src/Tokens/`（改令牌后必跑） |
| 令牌同步校验 | `pnpm link:tokens:check` | CI 门禁：文件名集合 + 逐文件字节比对 |
| 生成模拟器令牌 | `pnpm gen:sim` | 生成 `docs/.vitepress/theme/sim-tokens.css`（gitignore） |
| 生成原生文档页面 | `pnpm gen:docs` | md → `src/Docs/pages/*.cs` + `DocRegistry.cs`（禁手改，见红线 8） |
| 模拟器变量名校验 | `pnpm sim:check` | CI 门禁：模拟器引用的 `var(--lh-*)` 必须有定义 |
| 构建 | `dotnet build` | 编译四个工程（含 C# 类型检查） |
| 单测 | `dotnet test` | xUnit 黄金用例（15 份，断言 Resolve 纯函数） |
| 原生文档应用 | `pnpm docs:app` | `dotnet run --project docsapp`，类库内 LhDocsHost 的极薄 WinExe |
| 文档 dev | `pnpm docs:dev` | 先 gen:sim 再起 VitePress（端口 5186） |
| 文档构建 | `pnpm docs:build` | 部署前跑，产物在 docs/.vitepress/dist |
| 文档预览 | `pnpm docs:preview` | 预览构建产物 |
| 发版 | `pnpm changeset` / `pnpm changeset:version` | changesets 流程（.changeset/ 目录） |

## 八、Read before touching sensitive areas

- 改 `Common/LhColors.cs`：先读 core 的 `dist/windows/Tokens/LhDarkTokens.cs`，确认暗色覆盖范围
  （只 Color，不含尺寸），否则主题层取尺寸会拿到错误对象。`LhColors` 里每新增一个色字段，
  明/暗两个工厂都要同步加。
- 改任一 `Lh<Comp>Tokens.cs`：先读 `../lh-ui-core/specs/<comp>.json` 的 behaviors 与 tokenRefs，
  令牌绑定必须逐条对齐；黄金用例（`cases/<comp>.json`，已翻译为 `tests/Lh<Comp>Tests.cs`）
  会断言这些映射。
- 改 `scripts/*.mjs`：守卫逻辑改动影响各原生仓统一机制，先读 core 的
  `scripts/resolve-local-tokens.mjs` 与总纲 D18。
- 改 `docs/.vitepress/config.ts`：注意 `DOCS_BASE` 必须显式读 env（Vite 内置 env 加载在
  config 求值之后），且 `search` 必须放顶层 `themeConfig`（放 locale 下搜索 UI 不渲染）。
- 加新组件：先在 core 仓写 `specs/<comp>.json` + `cases/<comp>.json`（codegen 不生成 UI），
  再本仓建 `src/Components/<Comp>/`（`Lh<Comp>Tokens.cs` + `Lh<Comp>.cs` 两文件），翻译黄金用例为
  `tests/Lh<Comp>Tests.cs`，最后按红线 7 同步全部聚合点。
