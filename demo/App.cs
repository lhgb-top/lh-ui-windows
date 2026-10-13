using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Common;

namespace Top.Lhgb.Ui.Demo;

/// <summary>
/// 演示应用入口（解包部署形态，WindowsPackageType=None）。
/// WinUI 3 控件的默认样式由 XamlControlsResources 提供，这里在代码里并入字典。
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
        // 注入明色板；所有 Lh 组件自动消费（暗色切换见 MainWindow 的右上角按钮）
        LhTheme.Apply(false);

        _window = new MainWindow();
        _window.Activate();
    }
}
