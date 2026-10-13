using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Docs;

namespace Top.Lhgb.Ui.DocsApp;

/// <summary>
/// 文档应用入口（解包部署形态，WindowsPackageType=None）。
///
/// 极薄宿主：只负责注入明色板与并入 WinUI 默认控件字典，然后把
/// <see cref="LhDocsHost"/> 挂进窗口——所有文档内容、真交互预览、
/// 返回栈、明暗切换都在类库内的宿主里（双形态分发的「极薄 App」一侧）。
/// </summary>
public sealed class App : Application
{
    private Window? _window;

    public App()
    {
        Resources.MergedDictionaries.Add(new XamlControlsResources());
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        // 注入明色板；所有 Lh 组件自动消费（暗色切换见宿主顶栏右侧按钮）
        LhTheme.Apply(false);

        _window = new Window { Title = "lh-ui-windows 文档" };
        _window.Content = new LhDocsHost();
        _window.Activate();
    }
}
