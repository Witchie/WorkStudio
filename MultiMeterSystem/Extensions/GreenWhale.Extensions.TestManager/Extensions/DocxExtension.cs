using GreenWhale.Docx;
using GreenWhale.Docx.Src;
using System.Drawing;

namespace GreenWhale.Extensions.TestManager.DocxDocuments
{
    /// <summary>
    /// 标识
    /// </summary>
    public class Title
    {
        public Title(int number,string attch)
        {
            Final = $"第{number}{attch}";
        }
        public Title(int number)
        {
            Final = number.ToString();
        }
        public Title(string number)
        {
            Final = number;
        }
        public string Final { get; set; }
        public override string ToString()
        {
            return Final;
        }
        public Title Add(int numId)
        {
            return new Title(this.ToString() + "." + numId);
        }
    }
    /// <summary>
    /// 文档扩展
    /// </summary>
    public static class DocxExtension
    {
        /// <summary>
        /// 添加并插入到文档中
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="root"></param>
        /// <param name="listText"></param>
        /// <param name="level"></param>
        /// <param name="listType"></param>
        /// <param name="startNumber"></param>
        /// <param name="trackChanges"></param>
        /// <param name="continueNumbering"></param>
        /// <returns></returns>
        public static Paragraph AddInsert(this DocX doc, string listText, Title title, int level)
        {
          var par=  doc.InsertParagraph($"{title.ToString()} {listText}");
          par.Font("黑体").FontSize(14).SpacingLine(17);
          par.IndentationFirstLine = level * 14;
          return par;
        }
        /// <summary>
        /// 插入列表
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="list"></param>
        /// <param name="fontName"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List InsertList(this DocX doc,List list, string fontName, double size)
        {
          return  doc.InsertList(list, new Docx.Src.Font(fontName), size);
        }
        /// <summary>
        /// 配置页边距
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public static DocX WithPageMargin(this DocX doc, float left = 56.7f, float right = 56.7f, float top = 71f, float bottom = 72f)
        {
            doc.MarginLeft = left;
            doc.MarginRight = right;
            doc.MarginTop = top;
            doc.MarginBottom = bottom;
            return doc;
        }
        /// <summary>
        /// 添加一条横线
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Paragraph WithHorizontalLine(this Paragraph paragraph, Color color)
        {
            paragraph.InsertHorizontalLine(HorizontalBorderPosition.bottom, BorderStyle.Tcbs_single, 6, 1, color);
            return paragraph;
        }
        /// <summary>
        /// 背景颜色
        /// </summary>
        /// <param name="row"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Row Background(this Row row, Color color)
        {
            foreach (var item in row.Cells)
            {
                item.FillColor = color;
            }
            return row;
        }
        /// <summary>
        /// 配置页大小
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static DocX WithPageSize(this DocX doc, float height = 841.9f, float width = 595.3f)
        {
            doc.PageHeight = height;
            doc.PageWidth = width;
            return doc;
        }
        /// <summary>
        /// 同时配置字体，大小，对齐，粗细。
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="alignment"></param>
        /// <param name="bold"></param>
        /// <returns></returns>
        public static Paragraph WithFontSizeAlignment(this Paragraph paragraph, string name, double size, Alignment alignment, bool bold)
        {
            paragraph.Font(name).FontSize(size).Bold(bold).Alignment = alignment;
            return paragraph;
        }
        /// <summary>
        /// 配置正文样式
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static Paragraph WithMainContent(this Paragraph paragraph)
        {
            paragraph.SpacingBefore(10).SpacingAfter(10).Font("宋体").FontSize(12).IndentationFirstLine = 24;
            return paragraph;
        }
        /// <summary>
        /// 导航到指定单元格
        /// </summary>
        /// <param name="table"></param>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Cell Cell(this Table table, int row, int index)
        {
            var sigleRow = table.Rows[row];
            var cell = sigleRow.Cells[index];
            return cell;
        }
        /// <summary>
        /// 去掉所有边框
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static Table SetBorderAllNone(this Table table)
        {
            table.SetBorderAll(new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, System.Drawing.Color.Empty));
            return table;
        }
        /// <summary>
        /// 设置外置边框
        /// </summary>
        /// <param name="table"></param>
        /// <param name="border"></param>
        public static Table SetBorderOuter(this Table table, Border border)
        {
            table.SetBorder(TableBorderType.Bottom, border);
            table.SetBorder(TableBorderType.Left, border);
            table.SetBorder(TableBorderType.Right, border);
            table.SetBorder(TableBorderType.Top, border);
            return table;
        }
        /// <summary>
        /// 设置内部边框
        /// </summary>
        /// <param name="table"></param>
        /// <param name="border"></param>
        public static Table SetBorderInner(this Table table, Border border)
        {
            table.SetBorder(TableBorderType.InsideH, border);
            table.SetBorder(TableBorderType.InsideV, border);
            return table;
        }
        /// <summary>
        /// 设置所有边框
        /// </summary>
        /// <param name="table"></param>
        /// <param name="border"></param>
        public static Table SetBorderAll(this Table table, Border border)
        {
            return table.SetBorderOuter(border).SetBorderInner(border);
        }
        /// <summary>
        /// 单元格的值
        /// </summary>
        /// <param name="table"></param>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Paragraph CellValue(this Table table, int row, int index)
        {
            return table.Cell(row, index).Paragraphs[0];
        }
    }
}
