using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Components.Button;
using Top.Lhgb.Ui.Components.Cell;
using Top.Lhgb.Ui.Components.Checkbox;
using Top.Lhgb.Ui.Components.Dialog;
using Top.Lhgb.Ui.Components.Icon;
using Top.Lhgb.Ui.Components.Input;
using Top.Lhgb.Ui.Components.Loading;
using Top.Lhgb.Ui.Components.Navbar;
using Top.Lhgb.Ui.Components.Picker;
using Top.Lhgb.Ui.Components.Radio;
using Top.Lhgb.Ui.Components.Switch;
using Top.Lhgb.Ui.Components.Tag;
using Top.Lhgb.Ui.Components.Text;
using Top.Lhgb.Ui.Components.Textarea;
using Top.Lhgb.Ui.Components.Toast;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Demo;

/// <summary>
/// 演示分栏的公共接口：把宿主的指令式能力（toast / dialog / picker）注入各分栏。
/// </summary>
public sealed class DemoContext
{
    /// <summary>显示一条轻提示（type 决定圆点语义色）</summary>
    public required Action<string, LhToastType> ShowToast { get; init; }

    /// <summary>打开对话框（title / content / type 决定确认按钮颜色）</summary>
    public required Action<string, string, LhDialogType> ShowDialog { get; init; }

    /// <summary>打开选择器；确认时回调选中索引</summary>
    public required Action<string[], Action<int>> ShowPicker { get; init; }
}

/// <summary>
/// 15 个 P0 组件的演示分栏。每个分栏是一组真实控件，交互全部接到 DemoContext。
/// 布局尺寸一律走令牌（LhEpx.Px + LhTokens.*），不出现裸尺寸字面量。
/// </summary>
public static class DemoSections
{
    /// <summary>分栏名（与 ListView 绑定，顺序即索引）</summary>
    public static readonly string[] Names =
    {
        "Button 按钮",
        "Icon 图标",
        "Tag 标签",
        "Cell 单元格",
        "Text 文本",
        "Input 输入框",
        "Textarea 多行输入",
        "Checkbox 复选框",
        "Radio 单选框",
        "Switch 开关",
        "Loading 加载",
        "Dialog 对话框",
        "Toast 轻提示",
        "Picker 选择器",
        "Navbar 导航栏",
    };

    public static FrameworkElement Build(int index, DemoContext ctx) => index switch
    {
        0 => ButtonSection(ctx),
        1 => IconSection(),
        2 => TagSection(ctx),
        3 => CellSection(ctx),
        4 => TextSection(),
        5 => InputSection(ctx),
        6 => TextareaSection(ctx),
        7 => CheckboxSection(ctx),
        8 => RadioSection(ctx),
        9 => SwitchSection(ctx),
        10 => LoadingSection(),
        11 => DialogSection(ctx),
        12 => ToastSection(ctx),
        13 => PickerSection(ctx),
        _ => NavbarSection(ctx),
    };

    // —— 布局原语 ——

    private static StackPanel Section(string title, string desc, params FrameworkElement[] blocks)
    {
        var panel = new StackPanel { Spacing = LhEpx.Px(LhTokens.Spacing.Md) };
        panel.Children.Add(new LhText
        {
            Text = title,
            Level = LhTextLevel.ExtraLarge,
            Weight = LhTextWeight.Bold,
        });
        panel.Children.Add(new LhText { Text = desc, Tone = LhTextTone.Secondary });
        foreach (var block in blocks) panel.Children.Add(block);
        return panel;
    }

    /// <summary>横向行：容纳按钮/标签/图标等定宽控件</summary>
    private static StackPanel Row(params FrameworkElement[] items)
    {
        var panel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = LhEpx.Px(LhTokens.Spacing.Sm),
        };
        foreach (var item in items) panel.Children.Add(item);
        return panel;
    }

    /// <summary>纵向行：容纳输入框/单元格等需铺满宽度的控件</summary>
    private static StackPanel Column(params FrameworkElement[] items)
    {
        var panel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Spacing = LhEpx.Px(LhTokens.Spacing.Sm),
        };
        foreach (var item in items) panel.Children.Add(item);
        return panel;
    }

    private static LhButton Primary(string text) => new()
    {
        Type = LhButtonType.Primary,
        Text = text,
    };

    // —— 1. Button ——

    private static FrameworkElement ButtonSection(DemoContext ctx)
    {
        FrameworkElement Clickable(LhButton button)
        {
            button.Click += () => ctx.ShowToast($"点击了「{button.Text}」", LhToastType.Default);
            return button;
        }

        return Section(
            "Button 按钮",
            "六种类型 × 三种尺寸；禁用与加载中不触发 Click，两者共存按禁用优先。",
            Row(
                Clickable(new LhButton { Text = "默认" }),
                Clickable(Primary("主要")),
                Clickable(new LhButton { Type = LhButtonType.Success, Text = "成功" })),
            Row(
                Clickable(new LhButton { Type = LhButtonType.Warning, Text = "警告" }),
                Clickable(new LhButton { Type = LhButtonType.Danger, Text = "危险" }),
                Clickable(new LhButton { Type = LhButtonType.Info, Text = "信息" })),
            Row(
                Clickable(new LhButton { Size = LhButtonSize.Small, Text = "小型" }),
                Clickable(new LhButton { Size = LhButtonSize.Medium, Text = "中型" }),
                Clickable(new LhButton { Size = LhButtonSize.Large, Text = "大型" })),
            Row(
                new LhButton { Type = LhButtonType.Primary, Disabled = true, Text = "禁用" },
                new LhButton { Type = LhButtonType.Primary, Loading = true, Text = "加载中" },
                new LhButton { Type = LhButtonType.Primary, Disabled = true, Loading = true, Text = "禁用优先" }),
            Row(
                Clickable(new LhButton { Type = LhButtonType.Primary, Icon = "search", Text = "搜索" }),
                Clickable(new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = "花纹底" })));
    }

    // —— 2. Icon ——

    private static FrameworkElement IconSection()
    {
        return Section(
            "Icon 图标",
            "尺寸映射 icon.size.*；色调映射语义色。字形由 LhIconTokens.GlyphFor 解析，未登记名回退 ◆。",
            Row(
                new LhIcon { Name = "search", Size = LhIconSize.Small },
                new LhIcon { Name = "search", Size = LhIconSize.Medium },
                new LhIcon { Name = "search", Size = LhIconSize.Large }),
            Row(
                new LhIcon { Name = "check", Tone = LhIconTone.Success },
                new LhIcon { Name = "warning", Tone = LhIconTone.Warning },
                new LhIcon { Name = "close", Tone = LhIconTone.Danger },
                new LhIcon { Name = "info", Tone = LhIconTone.Info }),
            Row(
                new LhIcon { Name = "star", Tone = LhIconTone.Regular },
                new LhIcon { Name = "edit", Tone = LhIconTone.Secondary },
                new LhIcon { Name = "refresh", Tone = LhIconTone.Primary }));
    }

    // —— 3. Tag ——

    private static FrameworkElement TagSection(DemoContext ctx)
    {
        FrameworkElement Closable(LhTag tag)
        {
            tag.Closable = true;
            tag.Close += () => ctx.ShowToast($"关闭了「{tag.Text}」", LhToastType.Info);
            return tag;
        }

        return Section(
            "Tag 标签",
            "实心与描边（plain）两态；round 圆角取高度一半；closable 的关闭符吞掉点击事件。",
            Row(
                new LhTag { Text = "默认" },
                new LhTag { Type = LhTagType.Primary, Text = "主要" },
                new LhTag { Type = LhTagType.Success, Text = "成功" }),
            Row(
                new LhTag { Type = LhTagType.Warning, Text = "警告" },
                new LhTag { Type = LhTagType.Danger, Text = "危险" },
                new LhTag { Type = LhTagType.Info, Text = "信息" }),
            Row(
                new LhTag { Type = LhTagType.Primary, Plain = true, Text = "描边主要" },
                new LhTag { Type = LhTagType.Danger, Plain = true, Text = "描边危险" }),
            Row(
                new LhTag { Type = LhTagType.Primary, Round = true, Text = "圆角" },
                Closable(new LhTag { Type = LhTagType.Primary, Text = "可关闭" }),
                new LhTag { Size = LhTagSize.Small, Text = "小型" },
                new LhTag { Size = LhTagSize.Large, Text = "大型" }));
    }

    // —— 4. Cell ——

    private static FrameworkElement CellSection(DemoContext ctx)
    {
        var clickable = new LhCell
        {
            Title = "青花瓷",
            Label = "釉下彩，景德镇窑",
            Value = "已鉴定",
            Icon = "star",
            Clickable = true,
            Bordered = true,
        };
        clickable.Click += () => ctx.ShowToast("点击了单元格", LhToastType.Info);

        var required = new LhCell
        {
            Title = "藏品名称",
            Required = true,
            Value = "请填写",
            Bordered = true,
        };

        var plain = new LhCell { Title = "无分隔线单元格", Value = "仅标题" };

        return Section(
            "Cell 单元格",
            "单元格为纵向堆叠的分隔线风格；bordered 控制底部分隔线，clickable 打开点击反馈。",
            Column(clickable, required, plain));
    }

    // —— 5. Text ——

    private static FrameworkElement TextSection()
    {
        return Section(
            "Text 文本",
            "level 映射 typography.fontSize.*；tone 映射语义文字色；align 映射对齐。",
            Column(
                new LhText { Text = "extraLarge 一级标题", Level = LhTextLevel.ExtraLarge },
                new LhText { Text = "large 二级标题", Level = LhTextLevel.Large },
                new LhText { Text = "medium 三级标题", Level = LhTextLevel.Medium },
                new LhText { Text = "base 正文（默认）" },
                new LhText { Text = "small 说明文字", Level = LhTextLevel.Small },
                new LhText { Text = "extraSmall 辅助文字", Level = LhTextLevel.ExtraSmall }),
            Column(
                new LhText { Text = "加粗 bold", Weight = LhTextWeight.Bold },
                new LhText { Text = "常规 regular", Tone = LhTextTone.Regular },
                new LhText { Text = "次要 secondary", Tone = LhTextTone.Secondary },
                new LhText { Text = "占位 placeholder", Tone = LhTextTone.Placeholder },
                new LhText { Text = "选中 selected", Tone = LhTextTone.Selected }),
            Column(
                new LhText { Text = "左对齐 start", Align = LhTextAlign.Start },
                new LhText { Text = "居中 center", Align = LhTextAlign.Center },
                new LhText { Text = "右对齐 end", Align = LhTextAlign.End }));
    }

    // —— 6. Input ——

    private static FrameworkElement InputSection(DemoContext ctx)
    {
        var basic = new LhInput { Placeholder = "请输入关键字", Clearable = true };
        basic.ValueChange += value => ctx.ShowToast($"输入：{value}", LhToastType.Default);

        var password = new LhInput
        {
            Placeholder = "请输入密码",
            Type = LhInputType.Password,
            Value = "lh-ui",
        };

        var limited = new LhInput { Placeholder = "最多 6 个字", Maxlength = 6 };

        return Section(
            "Input 输入框",
            "受控组件：监听 ValueChange 回写 Value。Password 掩码显示；Clearable 出现清除符；Maxlength 截断。",
            Column(basic, password, limited),
            Column(
                new LhInput { Size = LhInputSize.Small, Placeholder = "小型输入框" },
                new LhInput { Size = LhInputSize.Medium, Placeholder = "中型输入框（默认）" },
                new LhInput { Size = LhInputSize.Large, Placeholder = "大型输入框" }),
            Column(
                new LhInput { Placeholder = "禁用输入框", Disabled = true },
                new LhInput { Placeholder = "只读输入框", Readonly = true, Value = "只读值" }));
    }

    // —— 7. Textarea ——

    private static FrameworkElement TextareaSection(DemoContext ctx)
    {
        var basic = new LhTextarea
        {
            Placeholder = "请输入藏品描述",
            Rows = 3,
            ShowCount = true,
            Maxlength = 10,
        };
        basic.ValueChange += value => ctx.ShowToast($"已输入 {value.Length} 字", LhToastType.Default);

        return Section(
            "Textarea 多行输入",
            "Rows 决定最小高度；ShowCount 显示字数统计，声明 Maxlength 时格式为「当前/最大」。",
            Column(basic),
            Column(
                new LhTextarea { Placeholder = "禁用多行输入", Rows = 2, Disabled = true },
                new LhTextarea { Placeholder = "只读多行输入", Rows = 2, Readonly = true, Value = "只读值" }));
    }

    // —— 8. Checkbox ——

    private static FrameworkElement CheckboxSection(DemoContext ctx)
    {
        FrameworkElement Labeled(LhCheckbox checkbox, string label)
        {
            checkbox.Label = label;
            checkbox.Change += isChecked => ctx.ShowToast($"{label}：{(isChecked ? "已选中" : "已取消")}", LhToastType.Default);
            return checkbox;
        }

        return Section(
            "Checkbox 复选框",
            "点击在 选中/未选中 间切换；Indeterminate 半选态点击后转全选并清除半选。",
            Row(
                Labeled(new LhCheckbox { Checked = true }, "青花瓷"),
                Labeled(new LhCheckbox { Indeterminate = true }, "粉彩瓷"),
                Labeled(new LhCheckbox { Disabled = true, Checked = true }, "汝窑（禁用）")),
            Row(
                Labeled(new LhCheckbox { Size = LhCheckboxSize.Small, Checked = true }, "小型"),
                Labeled(new LhCheckbox { Size = LhCheckboxSize.Medium, Checked = true }, "中型"),
                Labeled(new LhCheckbox { Size = LhCheckboxSize.Large, Checked = true }, "大型")));
    }

    // —— 9. Radio ——

    private static FrameworkElement RadioSection(DemoContext ctx)
    {
        var r1 = new LhRadio { Label = "青花瓷", Checked = true };
        var r2 = new LhRadio { Label = "粉彩瓷" };
        var r3 = new LhRadio { Label = "汝窑" };
        var group = new[] { r1, r2, r3 };

        foreach (var radio in group)
        {
            radio.Change += isChecked =>
            {
                if (!isChecked) return;
                foreach (var other in group) other.Checked = ReferenceEquals(other, radio);
                ctx.ShowToast($"选择了 {radio.Label}", LhToastType.Default);
            };
        }

        return Section(
            "Radio 单选框",
            "单选由宿主维护互斥（监听 Change 置兄弟为未选中）；选中圆点直径取 dotScale。",
            Row(r1, r2, r3),
            Row(
                new LhRadio { Label = "小型", Size = LhRadioSize.Small, Checked = true },
                new LhRadio { Label = "大型", Size = LhRadioSize.Large },
                new LhRadio { Label = "禁用", Disabled = true, Checked = true }));
    }

    // —— 10. Switch ——

    private static FrameworkElement SwitchSection(DemoContext ctx)
    {
        FrameworkElement Labeled(LhSwitch sw, string label)
        {
            sw.Change += isChecked => ctx.ShowToast($"{label}：{(isChecked ? "开" : "关")}", LhToastType.Default);
            return sw;
        }

        return Section(
            "Switch 开关",
            "滑块直径 = 高度 − 2 × gap；Loading 时改画加载环且不响应点击。",
            Row(
                Labeled(new LhSwitch { Checked = true }, "已开启"),
                Labeled(new LhSwitch(), "已关闭"),
                new LhSwitch { Checked = true, Disabled = true },
                new LhSwitch { Checked = true, Loading = true }),
            Row(
                new LhSwitch { Size = LhSwitchSize.Small, Checked = true },
                new LhSwitch { Size = LhSwitchSize.Medium, Checked = true },
                new LhSwitch { Size = LhSwitchSize.Large, Checked = true }));
    }

    // —— 11. Loading ——

    private static FrameworkElement LoadingSection()
    {
        return Section(
            "Loading 加载",
            "ProgressRing 环形旋转；size 映射直径，tone 映射环色。",
            Row(
                new LhLoading { Size = LhLoadingSize.Small },
                new LhLoading { Size = LhLoadingSize.Medium },
                new LhLoading { Size = LhLoadingSize.Large }),
            Row(
                new LhLoading { Tone = LhLoadingTone.Primary },
                new LhLoading { Tone = LhLoadingTone.Success },
                new LhLoading { Tone = LhLoadingTone.Warning },
                new LhLoading { Tone = LhLoadingTone.Danger },
                new LhLoading { Tone = LhLoadingTone.Info }));
    }

    // —— 12. Dialog ——

    private static FrameworkElement DialogSection(DemoContext ctx)
    {
        FrameworkElement Opener(LhButton button, LhDialogType type, string title, string content)
        {
            button.Click += () => ctx.ShowDialog(title, content, type);
            return button;
        }

        return Section(
            "Dialog 对话框",
            "确认按钮颜色随 type 变化；点击遮罩（closable）或按钮触发 Close，宿主据此隐藏。",
            Row(
                Opener(new LhButton { Text = "默认对话框" }, LhDialogType.Default, "提示", "这是一个默认对话框。"),
                Opener(Primary("主要对话框"), LhDialogType.Primary, "确认操作", "确认要继续吗？")),
            Row(
                Opener(new LhButton { Type = LhButtonType.Danger, Text = "危险对话框" },
                    LhDialogType.Danger, "删除确认", "删除后不可恢复，确认删除？"),
                Opener(new LhButton { Type = LhButtonType.Info, Text = "信息对话框" },
                    LhDialogType.Info, "说明", "lh-ui-windows 的对话框组件。")));
    }

    // —— 13. Toast ——

    private static FrameworkElement ToastSection(DemoContext ctx)
    {
        FrameworkElement Opener(LhButton button, LhToastType type, string text)
        {
            button.Click += () => ctx.ShowToast(text, type);
            return button;
        }

        return Section(
            "Toast 轻提示",
            "Show(text) 显示并重置自动隐藏计时（默认 2000ms），到期触发 Close；非 default 类型带语义色圆点。",
            Row(
                Opener(new LhButton { Text = "默认提示" }, LhToastType.Default, "这是一条提示"),
                Opener(Primary("成功提示"), LhToastType.Success, "保存成功")),
            Row(
                Opener(new LhButton { Type = LhButtonType.Warning, Text = "警告提示" }, LhToastType.Warning, "请检查输入"),
                Opener(new LhButton { Type = LhButtonType.Danger, Text = "危险提示" }, LhToastType.Danger, "操作失败"),
                Opener(new LhButton { Type = LhButtonType.Info, Text = "信息提示" }, LhToastType.Info, "已更新")));
    }

    // —— 14. Picker ——

    private static FrameworkElement PickerSection(DemoContext ctx)
    {
        var options = new[] { "青花瓷", "粉彩瓷", "汝窑", "哥窑", "定窑" };

        var opener = Primary("打开选择器");
        opener.Click += () => ctx.ShowPicker(options, index =>
            ctx.ShowToast($"已选择：{options[index]}", LhToastType.Success));

        return Section(
            "Picker 选择器",
            "底部滚轮选择器：上下两条指示线之间为选中区；确认携带选中索引，取消不携带。",
            Row(opener));
    }

    // —— 15. Navbar ——

    private static FrameworkElement NavbarSection(DemoContext ctx)
    {
        var bordered = new LhNavbar
        {
            Title = "藏品详情",
            LeftIcon = "back",
            LeftText = "返回",
            RightText = "编辑",
            Bordered = true,
        };
        bordered.LeftClick += () => ctx.ShowToast("点击了返回", LhToastType.Info);
        bordered.RightClick += () => ctx.ShowToast("点击了编辑", LhToastType.Info);

        var borderless = new LhNavbar
        {
            Title = "无分隔线导航栏",
            RightIcon = "more",
            Bordered = false,
        };
        borderless.RightClick += () => ctx.ShowToast("点击了更多", LhToastType.Info);

        return Section(
            "Navbar 导航栏",
            "三栏布局：左侧返回区 / 居中标题 / 右侧操作区；bordered 控制底部分隔线。",
            Column(bordered, borderless));
    }
}
