using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Button;
using Top.Lhgb.Ui.Components.Dialog;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Dialog 真交互预览。方法名与 <c>docs/components/dialog.md</c> 的 section 标题经 SCENE_MAP 对应。
///
/// Dialog 是指令式浮层：预览把一个 LhDialog 实例挂进树的末层（跨预览区铺满），
/// 按钮只负责翻转 <see cref="LhDialog.Visible"/>；Confirm / Cancel / Close 都由宿主收回。
/// </summary>
internal static class DialogPreviews
{
    /// <summary>## 基础用法</summary>
    public static UIElement Basic(DocPreviewScope scope)
    {
        var dialog = new LhDialog
        {
            Title = "确认删除？",
            Content = "删除后不可恢复，请谨慎操作。",
            ConfirmText = "删除",
            CancelText = "取消",
            Closable = true,
        };
        dialog.Confirm += () =>
        {
            dialog.Visible = false;
            scope.ShowToast("Confirm：已确认删除");
        };
        dialog.Cancel += () =>
        {
            dialog.Visible = false;
            scope.ShowToast("Cancel：已取消");
        };
        dialog.Close += () => dialog.Visible = false;

        var open = new LhButton { Type = LhButtonType.Primary, Text = "打开对话框" };
        open.Click += () => dialog.Visible = true;

        return Overlay(open, dialog);
    }

    /// <summary>## 语义类型</summary>
    public static UIElement SemanticTypes(DocPreviewScope scope)
    {
        var dialog = new LhDialog
        {
            Title = "语义类型对话框",
            Content = "type 决定确认按钮的语义色：primary / success / warning / danger。",
            ConfirmText = "确认",
            CancelText = "取消",
        };
        dialog.Confirm += () =>
        {
            dialog.Visible = false;
            scope.ShowToast($"Confirm：{dialog.Type} 类型已确认");
        };
        dialog.Close += () => dialog.Visible = false;

        var types = new[] { LhDialogType.Primary, LhDialogType.Success, LhDialogType.Warning, LhDialogType.Danger };
        var labels = new[] { "主要", "成功", "警告", "危险" };

        var buttons = PreviewLayout.Row();
        for (var i = 0; i < types.Length; i++)
        {
            var type = types[i];
            var button = new LhButton { Type = LhButtonType.Primary, Text = labels[i] };
            button.Click += () =>
            {
                dialog.Type = type;
                dialog.Visible = true;
            };
            buttons.Children.Add(button);
        }

        return Overlay(buttons, dialog);
    }

    /// <summary>
    /// 把触发按钮放第一行、浮层控件放最后一行并跨行铺满，
    /// 与 demo/MainWindow 的浮层宿主挂法同构（渲染层级最高）。
    /// </summary>
    private static Grid Overlay(FrameworkElement trigger, LhDialog dialog)
    {
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        Grid.SetRow(trigger, 0);
        grid.Children.Add(trigger);

        Grid.SetRow(dialog, 0);
        Grid.SetRowSpan(dialog, 2);
        grid.Children.Add(dialog);

        return grid;
    }
}
