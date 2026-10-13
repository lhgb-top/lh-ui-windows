# 安装

## 环境要求

- .NET 8 SDK（`net8.0-windows10.0.19041.0` 目标框架）
- Windows App SDK 1.6+（Windows 10 1809+ 或 Windows 11）
- 构建期工具链：Node.js >= 20 + pnpm（仅令牌连接与文档站使用，**不进入运行时**）

## NuGet 引入（nM6 发包链路打通后）

```bash
dotnet add package LhUi.Windows
```

```xml
<ItemGroup>
  <PackageReference Include="LhUi.Windows" Version="0.0.1" />
</ItemGroup>
```

## 源码集成（当前阶段）

家族布局下直接引用本仓类库（解决方案含 `src/LhUi.csproj` 类库 + `tests/` 测试 + `demo/` 演示应用）：

```
lh-ui-windows/
├── src/                  # LhUi 类库（Components/ + Common/ + Tokens/）
├── tests/                # xUnit 黄金用例
├── demo/                 # 演示应用（全形态展示 + 明暗切换）
└── docs/                 # VitePress 文档站
```

## 同步令牌

令牌是生成物，必须从 `lh-ui-core` 同步（唯一真源，禁止手改）：

```bash
cd ../lh-ui-core && pnpm codegen   # 生成 dist/windows 令牌产物
cd ../lh-ui-windows
pnpm install                        # 安装构建期工具链
pnpm link:tokens                    # 同步令牌到 src/Tokens/
```

`pnpm link:tokens` 带三级存在性守卫：本地 `../lh-ui-core/dist/windows` → npm `@lh-ui/core` 产物 → 报错。

## 验证安装

```bash
dotnet build     # 编译 LhUi / LhUi.Tests / LhUi.Demo（含 C# 类型检查）
dotnet test      # xUnit 黄金用例（15 份，断言 Resolve 纯函数外观解析）
```
