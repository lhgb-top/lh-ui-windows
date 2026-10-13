using System;
using System.Threading;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;

namespace Top.Lhgb.Ui.DocsApp;

/// <summary>
/// 解包部署（WindowsPackageType=None）形态的入口点。
/// 纯代码 App（无 App.xaml）时 WinUI 3 不会自动生成 Main，需手写：
/// Application.Start 内切到 DispatcherQueue 同步上下文后再构造 App（与 demo 同款）。
/// </summary>
public static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        Application.Start(p =>
        {
            var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
            SynchronizationContext.SetSynchronizationContext(context);
            _ = new App();
        });
    }
}
