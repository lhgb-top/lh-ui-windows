// 本文件由 scripts/gen-doc-pages.mjs 生成 —— 禁手改（分组顺序对齐 docs 侧边栏）。

using System.Collections.Generic;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Pages;

namespace Top.Lhgb.Ui.Docs;

/// <summary>
/// 文档注册表：数据驱动的页面清单。顺序与 <c>docs/.vitepress/config.ts</c> 的侧边栏一致。
/// </summary>
public static class DocRegistry
{
    public static readonly IReadOnlyList<DocEntry> All = new[]
    {
        GuideIntroductionDoc.Entry,
        GuideInstallationDoc.Entry,
        GuideQuickstartDoc.Entry,
        GuideThemingDoc.Entry,
        GuideLocalLinkingDoc.Entry,
        ButtonDoc.Entry,
        IconDoc.Entry,
        TagDoc.Entry,
        CellDoc.Entry,
        TextDoc.Entry,
        InputDoc.Entry,
        TextareaDoc.Entry,
        CheckboxDoc.Entry,
        RadioDoc.Entry,
        SwitchDoc.Entry,
        PickerDoc.Entry,
        DialogDoc.Entry,
        ToastDoc.Entry,
        LoadingDoc.Entry,
        NavbarDoc.Entry,
    };

    /// <summary>按 id 取条目；首页与详情页的导航都走它</summary>
    public static DocEntry? ById(string id)
    {
        foreach (var entry in All)
        {
            if (entry.Id == id) return entry;
        }
        return null;
    }
}
