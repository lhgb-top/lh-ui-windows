using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Components.Toast;

namespace Top.Lhgb.Ui.Docs.Model;

/// <summary>
/// 文档分组；顺序与 <c>docs/.vitepress/config.ts</c> 的侧边栏分组对齐。
/// 注册表由 <c>scripts/gen-doc-pages.mjs</c> 生成，分组顺序是真源。
/// </summary>
public enum DocGroup
{
    /// <summary>指南</summary>
    Guide,

    /// <summary>基础组件</summary>
    Basic,

    /// <summary>表单组件</summary>
    Form,

    /// <summary>反馈组件</summary>
    Feedback,

    /// <summary>导航组件</summary>
    Navigation,
}

/// <summary>通用表格行（单元格原始文本，行内反引号由构件侧渲染为等宽字形）</summary>
public sealed record DocRow(IReadOnlyList<string> Cells);

/// <summary>文档表格：Props 表 / 枚举表 / 令牌表 / 用例表 / 指南内的速查表共用</summary>
public sealed record DocTable(string Title, IReadOnlyList<string> Headers, IReadOnlyList<DocRow> Rows);

/// <summary>
/// 预览作用域：宿主能力注入。真交互预览只通过它与宿主通信（全局 Toast 双通道），
/// 不自持浮层，保持「宿主拖入即用」的零依赖。
/// </summary>
public sealed class DocPreviewScope
{
    /// <summary>显示一条全局 Toast（由宿主的 LhToast 承载，预览里点击/触发即给反馈）</summary>
    public Action<string> ShowToast { get; }

    /// <summary>切换宿主全局 Toast 的定位（演示定位能力时用）</summary>
    public Action<LhToastPosition> SetToastPosition { get; }

    public DocPreviewScope(Action<string> showToast, Action<LhToastPosition> setToastPosition)
    {
        ShowToast = showToast;
        SetToastPosition = setToastPosition;
    }
}

/// <summary>文档章节：散文行 + 可选真交互预览 + 代码片段 + 内联表格</summary>
public sealed record DocSection(
    string Title,
    IReadOnlyList<string> Prose,
    Func<DocPreviewScope, UIElement>? Preview,
    IReadOnlyList<string> Code,
    IReadOnlyList<DocTable> Tables);

/// <summary>
/// 文档条目：一个组件或一篇指南。
/// 内容由 <c>scripts/gen-doc-pages.mjs</c> 从 <c>docs/components/*.md</c> 与
/// <c>docs/guide/*.md</c> 生成（<b>禁手改</b>，漂移由脚本兜底）。
/// 终止节（API / 令牌对照 / 黄金用例对照）也作为普通章节承载，其表格落在 Tables 里。
/// </summary>
public sealed record DocEntry(
    string Id,
    string Title,
    IReadOnlyList<string> Lede,
    IReadOnlyList<string> Contract,
    DocGroup Group,
    IReadOnlyList<DocSection> Sections);

/// <summary>分组的中文标签（首页分组列表用）</summary>
public static class DocGroupLabels
{
    public static string Of(DocGroup group) => group switch
    {
        DocGroup.Guide => "指南",
        DocGroup.Basic => "基础组件",
        DocGroup.Form => "表单组件",
        DocGroup.Feedback => "反馈组件",
        DocGroup.Navigation => "导航组件",
        _ => group.ToString(),
    };
}
