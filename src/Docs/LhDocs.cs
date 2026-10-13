using Microsoft.UI.Xaml;

namespace Top.Lhgb.Ui.Docs;

/// <summary>
/// 文档模块公共入口。两种用法：
///
/// 1. 开窗口承载（最常用）：<c>LhDocs.Show()</c> 自建窗口并激活；
/// 2. 嵌入已有窗口：<c>var host = LhDocs.Show(myWindow);</c>，或直接
///    <c>myGrid.Children.Add(new LhDocsHost());</c>，宿主是 UserControl，拖入即用。
///
/// 内容由 <c>DocRegistry</c> 数据驱动（<c>scripts/gen-doc-pages.mjs</c> 生成，禁手改），
/// 真交互预览手写在 <c>src/Docs/previews/</c>。双形态分发对标 Android 的
/// 「library + 极薄 APK」：库内自带宿主，App 侧一行接入。
/// </summary>
public static class LhDocs
{
    /// <summary>
    /// 新开一个窗口并承载文档宿主，随后激活。
    /// 调用方无需自建窗口；适合文档站/App「关于本库」之类的独立入口。
    /// </summary>
    public static LhDocsHost Show()
    {
        var window = new Window { Title = "lh-ui-windows 文档" };
        return Show(window);
    }

    /// <summary>
    /// 把文档宿主挂进指定窗口并激活。窗口由调用方创建，
    /// 适合把文档嵌进已有 App 的某个窗口（窗口生命周期归调用方）。
    /// </summary>
    public static LhDocsHost Show(Window window)
    {
        var host = new LhDocsHost();
        window.Content = host;
        window.Activate();
        return host;
    }
}
