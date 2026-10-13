using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Components.Button;
using Top.Lhgb.Ui.Components.Picker;
using Top.Lhgb.Ui.Docs.Model;

namespace Top.Lhgb.Ui.Docs.Previews;

/// <summary>
/// Picker 真交互预览。方法名与 <c>docs/components/picker.md</c> 的 section 标题经 SCENE_MAP 对应。
///
/// Picker 是指令式浮层：预览把一个 LhPicker 实例挂进树的末层（跨预览区铺满），
/// 按钮只负责翻转 <see cref="LhPicker.Visible"/>；Close 事件里由宿主收回。
/// </summary>
internal static class PickerPreviews
{
    private static readonly string[] Provinces =
    {
        "北京市", "上海市", "广东省", "浙江省", "江苏省", "四川省",
    };

    /// <summary>## 基础用法</summary>
    public static UIElement Basic(DocPreviewScope scope)
    {
        var picker = new LhPicker
        {
            Title = "请选择省份",
            ConfirmText = "确定",
            CancelText = "取消",
            Options = Provinces,
        };
        picker.Confirm += index => scope.ShowToast($"Confirm：{Provinces[index]}（索引 {index}）");
        picker.Cancel += () => scope.ShowToast("Cancel：取消了选择");
        picker.Close += () => picker.Visible = false;

        var open = new LhButton { Type = LhButtonType.Primary, Text = "打开 Picker" };
        open.Click += () => picker.Visible = true;

        return Overlay(open, picker);
    }

    /// <summary>## 索引钳制</summary>
    public static UIElement IndexClamp(DocPreviewScope scope)
    {
        var picker = new LhPicker
        {
            Title = "Value 越界自动钳制",
            Options = Provinces,
            // 黄金用例 value-clamp：Value 超出 options 长度时解析回 0，不越界崩溃
            Value = 99,
        };
        picker.Confirm += index => scope.ShowToast($"Confirm：钳制到索引 {index}（{Provinces[index]}）");
        picker.Close += () => picker.Visible = false;

        var open = new LhButton { Type = LhButtonType.Primary, Text = "Value = 99 打开 Picker" };
        open.Click += () => picker.Visible = true;

        return Overlay(open, picker);
    }

    /// <summary>
    /// 把触发按钮放第一行、浮层控件放最后一行并跨行铺满，
    /// 与 demo/MainWindow 的浮层宿主挂法同构（渲染层级最高）。
    /// </summary>
    private static Grid Overlay(FrameworkElement trigger, LhPicker picker)
    {
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        Grid.SetRow(trigger, 0);
        grid.Children.Add(trigger);

        Grid.SetRow(picker, 0);
        Grid.SetRowSpan(picker, 2);
        grid.Children.Add(picker);

        return grid;
    }
}
