using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Top.Lhgb.Ui.Common;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Tokens;

namespace Top.Lhgb.Ui.Docs.Widgets;

/// <summary>
/// 文档明暗取色别名：docs 令牌的暗色只覆盖颜色（尺寸回退明色），统一在此解析，
/// 与 Android <c>DocWidgets.kt</c> 的私有属性别名同构。构件全文不出现色值/尺寸字面量。
/// </summary>
internal static class DocPalette
{
    public static Windows.UI.Color CardBackground => LhTheme.IsDark ? LhDarkTokens.Docs.Card.Background : LhTokens.Docs.Card.Background;
    public static Windows.UI.Color CardBorder => LhTheme.IsDark ? LhDarkTokens.Docs.Card.BorderColor : LhTokens.Docs.Card.BorderColor;
    public static double CardRadius => LhTokens.Docs.Card.BorderRadius;
    public static double CardBorderWidth => LhTokens.Docs.Card.BorderWidth;
    public static double CardPadding => LhTokens.Docs.Card.Padding;

    public static Windows.UI.Color CodeBackground => LhTheme.IsDark ? LhDarkTokens.Docs.Code.Background : LhTokens.Docs.Code.Background;
    public static Windows.UI.Color CodeText => LhTheme.IsDark ? LhDarkTokens.Docs.Code.TextColor : LhTokens.Docs.Code.TextColor;
    public static double CodeRadius => LhTokens.Docs.Code.BorderRadius;
    public static string CodeFontFamily => LhTokens.Docs.Code.FontFamily;
    public static double CodeFontSize => LhTokens.Docs.Code.FontSize;
    public static double CodePaddingX => LhTokens.Docs.Code.PaddingX;
    public static double CodePaddingY => LhTokens.Docs.Code.PaddingY;

    public static Windows.UI.Color HeadingColor => LhTheme.IsDark ? LhDarkTokens.Docs.Heading.Color : LhTokens.Docs.Heading.Color;
    public static double HeadingFontSize(int level) => level switch
    {
        1 => LhTokens.Docs.Heading.FontSizeH1,
        2 => LhTokens.Docs.Heading.FontSizeH2,
        _ => LhTokens.Docs.Heading.FontSizeH3,
    };

    public static Windows.UI.Color ProseText => LhTheme.IsDark ? LhDarkTokens.Docs.Prose.TextColor : LhTokens.Docs.Prose.TextColor;
    public static Windows.UI.Color ProseSecondary => LhTheme.IsDark ? LhDarkTokens.Docs.Prose.SecondaryColor : LhTokens.Docs.Prose.SecondaryColor;
    public static double ProseFontSize => LhTokens.Docs.Prose.FontSize;

    public static Windows.UI.Color TableBorder => LhTheme.IsDark ? LhDarkTokens.Docs.Table.BorderColor : LhTokens.Docs.Table.BorderColor;
    public static Windows.UI.Color TableHeaderBg => LhTheme.IsDark ? LhDarkTokens.Docs.Table.HeaderBg : LhTokens.Docs.Table.HeaderBg;
    public static Windows.UI.Color TableHeaderText => LhTheme.IsDark ? LhDarkTokens.Docs.Table.HeaderTextColor : LhTokens.Docs.Table.HeaderTextColor;
    public static double TableBorderWidth => LhTokens.Docs.Table.BorderWidth;
    public static double CellPaddingX => LhTokens.Docs.Table.CellPaddingX;
    public static double CellPaddingY => LhTokens.Docs.Table.CellPaddingY;
    public static double TableFontSize => LhTokens.Docs.Table.FontSize;
}

/// <summary>
/// 文档基础构件：标题 / 正文 / 分隔线 / 卡片 / 代码块 / 表格。
/// 全部消费 <see cref="DocPalette"/>（即 <c>LhTokens.Docs.*</c> + <c>LhTheme</c>），零字面量。
/// </summary>
internal static class DocWidgets
{
    /// <summary>把 md 行内反引号 <c>`token`</c> 渲染为等宽字形，其余为正文字体</summary>
    private static IReadOnlyList<Inline> BuildInlines(string text, double fontSize, Windows.UI.Color color)
    {
        var inlines = new List<Inline>();
        // 以反引号分段：奇数段是代码，偶数段是正文（未闭合的尾段按正文处理）
        var parts = text.Split('`');
        for (var i = 0; i < parts.Length; i++)
        {
            if (parts[i].Length == 0) continue;
            var isCode = i % 2 == 1;
            inlines.Add(new Run
            {
                Text = parts[i],
                FontSize = isCode ? DocPalette.CodeFontSize : fontSize,
                Foreground = new SolidColorBrush(isCode ? DocPalette.CodeText : color),
                // 正文段不指定字体族（回退到系统默认）；FontFamily 无无参构造，须显式传空
                FontFamily = isCode ? new FontFamily(DocPalette.CodeFontFamily) : new FontFamily(string.Empty),
            });
        }
        return inlines;
    }

    private static void AddInlines(TextBlock block, string text, double fontSize, Windows.UI.Color color)
    {
        foreach (var inline in BuildInlines(text, fontSize, color)) block.Inlines.Add(inline);
    }

    /// <summary>标题（level 1/2/3 对应 H1/H2/H3 字号令牌）</summary>
    public static TextBlock Heading(string text, int level)
    {
        var block = new TextBlock
        {
            FontSize = DocPalette.HeadingFontSize(level),
            FontWeight = Microsoft.UI.Text.FontWeights.SemiBold,
            Foreground = new SolidColorBrush(DocPalette.HeadingColor),
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, level == 1 ? 0 : DocPalette.CardPadding, 0, DocPalette.CellPaddingY),
        };
        AddInlines(block, text, DocPalette.HeadingFontSize(level), DocPalette.HeadingColor);
        return block;
    }

    /// <summary>正文（行内反引号渲染为等宽）</summary>
    public static TextBlock Prose(string text, bool secondary = false)
    {
        var color = secondary ? DocPalette.ProseSecondary : DocPalette.ProseText;
        var block = new TextBlock
        {
            FontSize = DocPalette.ProseFontSize,
            Foreground = new SolidColorBrush(color),
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 0, DocPalette.CellPaddingY),
        };
        AddInlines(block, text, DocPalette.ProseFontSize, color);
        return block;
    }

    /// <summary>分隔线（取表格分割线令牌）</summary>
    public static Rectangle Divider() => new()
    {
        Height = DocPalette.TableBorderWidth,
        Fill = new SolidColorBrush(DocPalette.TableBorder),
        Margin = new Thickness(0, DocPalette.CellPaddingY, 0, DocPalette.CellPaddingY),
        HorizontalAlignment = HorizontalAlignment.Stretch,
    };

    /// <summary>卡片：包裹预览框或成组内容</summary>
    public static Border Card(UIElement content)
    {
        var panel = new StackPanel();
        panel.Children.Add(content);
        return new Border
        {
            Background = new SolidColorBrush(DocPalette.CardBackground),
            BorderBrush = new SolidColorBrush(DocPalette.CardBorder),
            BorderThickness = new Thickness(DocPalette.CardBorderWidth),
            CornerRadius = new CornerRadius(DocPalette.CardRadius),
            Padding = new Thickness(DocPalette.CardPadding),
            Child = panel,
            Margin = new Thickness(0, 0, 0, DocPalette.CellPaddingY),
        };
    }

    /// <summary>代码块：等宽字体栈 + 逐行保留缩进</summary>
    public static Border CodeBlock(IReadOnlyList<string> lines)
    {
        var panel = new StackPanel();
        foreach (var line in lines)
        {
            panel.Children.Add(new TextBlock
            {
                Text = line.Length == 0 ? " " : line,
                FontFamily = new FontFamily(DocPalette.CodeFontFamily),
                FontSize = DocPalette.CodeFontSize,
                Foreground = new SolidColorBrush(DocPalette.CodeText),
                TextWrapping = TextWrapping.NoWrap,
            });
        }
        return new Border
        {
            Background = new SolidColorBrush(DocPalette.CodeBackground),
            CornerRadius = new CornerRadius(DocPalette.CodeRadius),
            Padding = new Thickness(DocPalette.CodePaddingX, DocPalette.CodePaddingY, DocPalette.CodePaddingX, DocPalette.CodePaddingY),
            Child = panel,
            Margin = new Thickness(0, 0, 0, DocPalette.CellPaddingY),
            HorizontalAlignment = HorizontalAlignment.Left,
        };
    }

    /// <summary>表格：表头行底色 + 分割线，列宽按内容自适应（WinUI Grid 星号列）</summary>
    public static Grid Table(DocTable table)
    {
        var grid = new Grid();
        var border = new SolidColorBrush(DocPalette.TableBorder);
        var borderWidth = DocPalette.TableBorderWidth;

        for (var i = 0; i < table.Headers.Count; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
        }

        // 表头
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        for (var c = 0; c < table.Headers.Count; c++)
        {
            var cell = new Border
            {
                Background = new SolidColorBrush(DocPalette.TableHeaderBg),
                BorderBrush = border,
                BorderThickness = new Thickness(borderWidth, borderWidth, c == table.Headers.Count - 1 ? borderWidth : 0, borderWidth),
                Padding = new Thickness(DocPalette.CellPaddingX, DocPalette.CellPaddingY, DocPalette.CellPaddingX, DocPalette.CellPaddingY),
            };
            var header = new TextBlock
            {
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold,
                FontSize = DocPalette.TableFontSize,
                Foreground = new SolidColorBrush(DocPalette.TableHeaderText),
                TextWrapping = TextWrapping.Wrap,
            };
            header.Inlines.Add(new Run
            {
                Text = table.Headers[c],
                FontSize = DocPalette.TableFontSize,
                Foreground = new SolidColorBrush(DocPalette.TableHeaderText),
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold,
            });
            cell.Child = header;
            Grid.SetRow(cell, 0);
            Grid.SetColumn(cell, c);
            grid.Children.Add(cell);
        }

        // 数据行
        for (var r = 0; r < table.Rows.Count; r++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            var isLast = r == table.Rows.Count - 1;
            for (var c = 0; c < table.Headers.Count; c++)
            {
                var text = c < table.Rows[r].Cells.Count ? table.Rows[r].Cells[c] : string.Empty;
                var cell = new Border
                {
                    BorderBrush = border,
                    BorderThickness = new Thickness(c == 0 ? borderWidth : 0, 0, c == table.Headers.Count - 1 ? borderWidth : 0, isLast ? borderWidth : 0),
                    Padding = new Thickness(DocPalette.CellPaddingX, DocPalette.CellPaddingY, DocPalette.CellPaddingX, DocPalette.CellPaddingY),
                    Background = new SolidColorBrush(DocPalette.CardBackground),
                };
                var body = new TextBlock
                {
                    FontSize = DocPalette.TableFontSize,
                    Foreground = new SolidColorBrush(DocPalette.ProseText),
                    TextWrapping = TextWrapping.Wrap,
                };
                body.Inlines.Add(new Run
                {
                    Text = text,
                    FontSize = DocPalette.TableFontSize,
                    Foreground = new SolidColorBrush(DocPalette.ProseText),
                });
                cell.Child = body;
                Grid.SetRow(cell, r + 1);
                Grid.SetColumn(cell, c);
                grid.Children.Add(cell);
            }
        }
        return grid;
    }
}
