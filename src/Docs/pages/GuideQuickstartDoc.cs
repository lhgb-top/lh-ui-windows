// 本文件由 scripts/gen-doc-pages.mjs 从 docs/guide/quickstart.md 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/Previews.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class GuideQuickstartDoc
{
    public static readonly DocEntry Entry = new DocEntry(
        "quickstart",
        "快速开始",
        Array.Empty<string>(),
        Array.Empty<string>(),
        DocGroup.Guide,
        new[]
        {
            new DocSection(
                "最小示例",
                new[]
                {
                    "`LhTheme.Apply(isDark)` 切换静态色板并触发 `ThemeChanged`，所有已加载的 Lh 组件自动重绘；不调用时回退明色板。"
                },
                null,
                new[]
                {
                    "using Microsoft.UI.Xaml;",
                    "using Microsoft.UI.Xaml.Controls;",
                    "using Top.Lhgb.Ui;",
                    "using Top.Lhgb.Ui.Common;",
                    "",
                    "public sealed class DemoPage : Page",
                    "{",
                    "    public DemoPage()",
                    "    {",
                    "        // 注入明色板；切暗色传 true。所有 Lh 组件自动跟随（LhTheme.ThemeChanged 热重绘）",
                    "        LhTheme.Apply(false);",
                    "",
                    "        var button = new LhButton",
                    "        {",
                    "            Type = LhButtonType.Primary,",
                    "            Text = \"主要按钮\",",
                    "        };",
                    "        button.Click += OnButtonClick;",
                    "",
                    "        Content = new StackPanel { Children = { button } };",
                    "    }",
                    "",
                    "    private void OnButtonClick() => { /* 处理点击 */ }",
                    "}"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "类型与尺寸",
                Array.Empty<string>(),
                null,
                new[]
                {
                    "// 六种类型",
                    "new LhButton { Text = \"默认\" };                                     // Default",
                    "new LhButton { Type = LhButtonType.Primary,   Text = \"主要\" };",
                    "new LhButton { Type = LhButtonType.Success,   Text = \"成功\" };",
                    "new LhButton { Type = LhButtonType.Warning,   Text = \"警告\" };",
                    "new LhButton { Type = LhButtonType.Danger,    Text = \"危险\" };",
                    "new LhButton { Type = LhButtonType.Info,      Text = \"信息\" };",
                    "",
                    "// 三种尺寸",
                    "new LhButton { Size = LhButtonSize.Small,  Text = \"小型\" };",
                    "new LhButton { Size = LhButtonSize.Medium, Text = \"中型\" };   // 默认",
                    "new LhButton { Size = LhButtonSize.Large,  Text = \"大型\" };"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "状态",
                Array.Empty<string>(),
                null,
                new[]
                {
                    "// 禁用：不响应点击（Tapped gate 拦截），呈现禁用样式",
                    "new LhButton { Disabled = true, Text = \"禁用\" };",
                    "",
                    "// 加载中：显示 ProgressRing，不触发 Click",
                    "new LhButton { Loading = true, Text = \"加载中\" };",
                    "",
                    "// 两者共存：按禁用优先处理（黄金用例 disabled-precedes-loading）",
                    "new LhButton { Disabled = true, Loading = true, Text = \"禁用优先\" };"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "图标与花纹底",
                Array.Empty<string>(),
                null,
                new[]
                {
                    "// 图标名按本地图标体系解析（LhButtonTokens.IconFor，当前支持 search / refresh / check / close，",
                    "// 未登记名不绘制）",
                    "new LhButton { Type = LhButtonType.Primary, Icon = \"search\", Text = \"搜索\" };",
                    "",
                    "// 国风花纹底纹（消费 color.decoration 令牌）",
                    "new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = \"花纹底\" };"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "明暗主题",
                Array.Empty<string>(),
                null,
                new[]
                {
                    "LhTheme.Apply(true);  // 全局切暗色；所有组件下次外观解析时自动跟随"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "表单与反馈组件",
                Array.Empty<string>(),
                null,
                new[]
                {
                    "// 受控输入：宿主监听 ValueChange 并回写 Value",
                    "var input = new LhInput { Placeholder = \"请输入关键字\" };",
                    "input.ValueChange += value => input.Value = value;",
                    "",
                    "// 复选框",
                    "var checkbox = new LhCheckbox { Label = \"青花瓷\" };",
                    "checkbox.Change += isChecked => { };",
                    "",
                    "// 开关",
                    "var sw = new LhSwitch { Checked = true };",
                    "sw.Change += isChecked => { };",
                    "",
                    "// 指令式轻提示：先放宿主控件，再用控制器驱动",
                    "var toast = new LhToast();",
                    "toast.Close += () => { };",
                    "var panel = new StackPanel { Children = { toast } };",
                    "toast.Show(\"已保存\");   // 到期自动隐藏并触发 Close"
                },
                Array.Empty<DocTable>()),
            new DocSection(
                "组件示例预览",
                new[]
                {
                    "原生 UI 无法在浏览器实时渲染，本站用**内联静态 HTML 手机框模拟器**展示外观：",
                    "> 模拟器的暗色与站点联动：右上角切换主题，手机框内颜色即变（暗色令牌由 core 预计算）。"
                },
                null,
                Array.Empty<string>(),
                Array.Empty<DocTable>()),
        });
}
