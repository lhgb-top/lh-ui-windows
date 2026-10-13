/**
 * LhUi 聚合导出（类库入口）。
 *
 * 只做聚合，不含任何实现：控件体在 Components/<Comp>/Lh<Comp>.cs，外观解析在
 * 各 Lh<Comp>Tokens.cs，主题工具在 Common/。core codegen 不生成 UI（AGENTS.md 红线 3），
 * 这里的每一行都是手写 WinUI 3。
 *
 * 血脉隔离（红线 4）：本文件不引入任何 @lh-ui/share / web 血脉符号。
 */

// 基础组件
global using Top.Lhgb.Ui.Components.Button;
global using Top.Lhgb.Ui.Components.Icon;
global using Top.Lhgb.Ui.Components.Tag;
global using Top.Lhgb.Ui.Components.Cell;
global using Top.Lhgb.Ui.Components.Text;

// 表单组件
global using Top.Lhgb.Ui.Components.Input;
global using Top.Lhgb.Ui.Components.Textarea;
global using Top.Lhgb.Ui.Components.Checkbox;
global using Top.Lhgb.Ui.Components.Radio;
global using Top.Lhgb.Ui.Components.Switch;
global using Top.Lhgb.Ui.Components.Picker;

// 反馈组件
global using Top.Lhgb.Ui.Components.Dialog;
global using Top.Lhgb.Ui.Components.Toast;
global using Top.Lhgb.Ui.Components.Loading;

// 导航组件
global using Top.Lhgb.Ui.Components.Navbar;

// 主题
global using Top.Lhgb.Ui.Common;

// 令牌（core codegen 产物的命名空间引用：唯一外部令牌来源，红线 1/2）
// 暗色令牌是并列对象模型——LhDarkTokens 只含 Color 子类，尺寸/动效/圆角暗色不覆盖（D18）。
// 需要暗色尺寸时取明色对象的值（组件已如此实现，见各 Lh<Comp>Tokens.cs）。
global using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui;

/// <summary>lh-ui-windows 类库入口。聚合导出见文件顶部的 global using。</summary>
public static class LhUi
{
    /// <summary>库的语义化版本（与 package.json / changesets 同步）</summary>
    public const string Version = "0.0.1";
}
