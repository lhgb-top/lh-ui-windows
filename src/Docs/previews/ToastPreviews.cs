using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Button;
using Top.Lhgb.Ui.Components.Toast;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Toast 真交互预览。方法名与 <c>docs/components/toast.md</c> 的 section 标题经 SCENE_MAP 对应。
///
/// 预览内嵌一个 LhToast 实例作为末层浮层；同时也演示宿主通道
/// <see cref="LhToastController"/>：把作用域的全局 Toast 接到按钮上（真机里由宿主统一持有）。
/// </summary>
internal static class ToastPreviews
{
    /// <summary>## 基础用法</summary>
    public static UIElement Basic(DocPreviewScope scope)
    {
        var toast = new LhToast { Duration = 2000 };

        var show = new LhButton { Type = LhButtonType.Primary, Text = "显示 Toast" };
        show.Click += () => toast.Show("这是一条轻提示");

        var hide = new LhButton { Type = LhButtonType.Default, Text = "立即隐藏" };
        hide.Click += () => toast.Hide();

        toast.Close += () => scope.ShowToast("计时到期，Close 事件触发");

        return Overlay(PreviewLayout.Row(show, hide), toast);
    }

    /// <summary>## 语义类型</summary>
    public static UIElement SemanticTypes(DocPreviewScope scope)
    {
        var toast = new LhToast();

        var types = new[] { LhToastType.Default, LhToastType.Success, LhToastType.Warning, LhToastType.Danger, LhToastType.Info };
        var labels = new[] { "默认", "成功", "警告", "危险", "信息" };
        var messages = new[] { "默认提示", "操作成功", "请注意风险", "操作失败", "普通信息" };

        var triggers = new LhButton[types.Length];
        for (var i = 0; i < types.Length; i++)
        {
            var type = types[i];
            var message = messages[i];
            triggers[i] = new LhButton { Type = LhButtonType.Default, Text = labels[i] };
            triggers[i].Click += () =>
            {
                toast.Type = type;
                toast.Show(message);
            };
        }

        // 五个按钮分两行排（WinUI 3 无原生 WrapPanel，用两行 Row 代替）
        var buttons = PreviewLayout.Column(
            PreviewLayout.Row(triggers[0], triggers[1], triggers[2]),
            PreviewLayout.Row(triggers[3], triggers[4]));

        return Overlay(buttons, toast);
    }

    /// <summary>## 定位</summary>
    public static UIElement Position(DocPreviewScope scope)
    {
        var toast = new LhToast();

        var top = new LhButton { Type = LhButtonType.Default, Text = "Top 顶部" };
        top.Click += () =>
        {
            toast.Position = LhToastPosition.Top;
            toast.Show("顶部定位");
        };

        var center = new LhButton { Type = LhButtonType.Default, Text = "Center 居中（默认）" };
        center.Click += () =>
        {
            toast.Position = LhToastPosition.Center;
            toast.Show("居中定位");
        };

        var bottom = new LhButton { Type = LhButtonType.Default, Text = "Bottom 底部" };
        bottom.Click += () =>
        {
            toast.Position = LhToastPosition.Bottom;
            scope.SetToastPosition(LhToastPosition.Bottom);
            toast.Show("底部定位");
        };

        return Overlay(PreviewLayout.Row(top, center, bottom), toast);
    }

    /// <summary>触发按钮在第一行，Toast 浮层跨行铺满挂末层（渲染层级最高）</summary>
    private static Grid Overlay(FrameworkElement trigger, LhToast toast)
    {
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        Grid.SetRow(trigger, 0);
        grid.Children.Add(trigger);

        Grid.SetRow(toast, 0);
        Grid.SetRowSpan(toast, 2);
        grid.Children.Add(toast);

        return grid;
    }
}
