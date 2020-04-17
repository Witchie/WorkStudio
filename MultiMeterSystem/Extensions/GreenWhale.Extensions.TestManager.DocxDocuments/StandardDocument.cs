using GreenWhale.Docx;
using GreenWhale.Docx.Src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
namespace GreenWhale.Extensions.TestManager.DocxDocuments
{
    public static class StandardDocumentExtension
    {
        /// <summary>
        /// 配置页边距
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public static DocX WithPageMargin(this DocX doc,float left=56.7f, float right= 56.7f, float top=71f, float bottom=72f)
        {
            doc.MarginLeft = left;
            doc.MarginRight = right;
            doc.MarginTop = top;
            doc.MarginBottom = bottom;
            return doc;
        }
        /// <summary>
        /// 配置页大小
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static DocX WithPageSize(this DocX doc,float height=841.9f,float width=595.3f)
        {
            doc.PageHeight = height;
            doc.PageWidth = width;
            return doc;
        }
        /// <summary>
        /// 添加页眉
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static DocX WithPageHeader(this DocX doc,string fileName, int height=17,int width=482)
        {
            doc.AddHeaders();
            doc.DifferentFirstPage = true;
            var image = doc.AddImage(fileName);
            var picture=  image.CreatePicture(height, width);
            doc.Headers.First.Paragraphs[0].AppendPicture(picture).WithHorizontalLineBlack();
            return doc;
        }
        /// <summary>
        /// 设置页脚
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static DocX WithPageFooter(this DocX doc, string content)
        {
            doc.AddFooters();
            doc.Footers.First.Paragraphs[0].Append(content).Font("宋体").FontSize(9).WithHorizontalLineBlack(HorizontalBorderPosition.top).Alignment=Alignment.left;
            return doc;
        }
        /// <summary>
        /// 华文新魏
        /// </summary>
        private const string Name1 = "华文新魏";
        public static Paragraph WithHorizontalLineBlack(this Paragraph paragraph, HorizontalBorderPosition position= HorizontalBorderPosition.bottom)
        {
            paragraph.InsertHorizontalLine(position, BorderStyle.Tcbs_single,6,1,System.Drawing.Color.Black);
            return paragraph;
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
        /// 添加首页
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="documentName">文档名称</param>
        /// <param name="documentEnglishName">英文名称</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="companyEnglishName">公司英文名称</param>
        /// <returns></returns>
        public static DocX WithFirstPage(this DocX doc, DocumentInfo info)
        {
           var par=  doc.InsertParagraph().Append(info.CompanyName).Font(Name1).Bold().FontSize(26);
            par.LineSpacingBefore = 40.5f;
            par.LineSpacingAfter = 40.5f;
            par.Alignment = Alignment.center;
            var par2 = doc.InsertParagraph().Append(info.CompanyEnglishName).FontSize(9f).Font(Name1);
            par2.Alignment = Alignment.right;
            par2.SpacingAfter(0);
            par2.WithHorizontalLine(Color.DarkRed);
            doc.InsertParagraph().Append(info.DocumentName).Bold().Font(Name1).FontSize(16).SpacingBefore(50).SpacingAfter(30).Alignment=Alignment.center;
            doc.InsertParagraph().Append(info.DocumentEnglishName).Bold().Font(Name1).FontSize(16).SpacingBefore(30).SpacingAfter(200).Alignment = Alignment.center;
            var dataTable= doc.InsertTable(4, 2);
            dataTable.CellValue(0, 0).Append("项目名称：").WithFontSizeAlignment(Name1, 16, Alignment.right, true);
            dataTable.CellValue(1, 0).Append("项目型号：").WithFontSizeAlignment(Name1, 16, Alignment.right, true);
            dataTable.CellValue(2, 0).Append("文件版本：").WithFontSizeAlignment(Name1, 16, Alignment.right, true);
            dataTable.CellValue(3, 0).Append("提交日期：").WithFontSizeAlignment(Name1, 16, Alignment.right, true);
            dataTable.CellValue(0, 1).Append(info.ProjectName).WithFontSizeAlignment(Name1, 16, Alignment.left, true);
            dataTable.CellValue(1, 1).Append(info.ProjectModel).WithFontSizeAlignment(Name1, 16, Alignment.left, true);
            dataTable.CellValue(2, 1).Append(info.ProjectVersion).WithFontSizeAlignment(Name1, 16, Alignment.left, true);
            dataTable.CellValue(3, 1).Append(info.ProjectDateTime).WithFontSizeAlignment(Name1, 16, Alignment.left, true);
            dataTable.Alignment = Alignment.right;
            dataTable.SetColumnWidth(0, 96);
            dataTable.SetColumnWidth(1, 223);
            dataTable.SetBorderAllNone();    
            doc.InsertSectionPageBreak();
            doc.WithPageHeader(info.CompanyLogoImage);
            doc.WithPageFooter(info.DocumentFooter);
            return doc;
        }
        /// <summary>
        /// 添加第二页
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static DocX WithSecondPage(this DocX doc, IEnumerable<DocumentModifyInfo> enumerable)
        {
            doc.InsertParagraph("文件修订历史").WithFontSizeAlignment("黑体", 16, Alignment.center, true).SpacingBefore(30).SpacingAfter(30);
            var table= doc.InsertTable(10,3);
            table.CellValue(0, 0).Append("文件版本");
            table.CellValue(0, 1).Append("修订日期");
            table.CellValue(0, 2).Append("修订原因");
            if (enumerable.Count()>0)
            {
                foreach (var item in enumerable)
                {
                   var row=  table.InsertRow();
                    row.Cells[0].Paragraphs[0].Append($"V" + item.Version);
                    row.Cells[1].Paragraphs[0].Append(item.ModifyDateTime.Date.ToLongDateString());
                    row.Cells[2].Paragraphs[0].Append(item.Remark);
                }
            }
            else
            {
                table.CellValue(1, 0).Append("未填写");
                table.CellValue(1, 1).Append("未填写");
                table.CellValue(1, 2).Append("未填写");
            }
            return doc;
        }
        /// <summary>
        /// 添加第二页
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static DocX WithSecondPageNew(this DocX doc)
        {
            doc.WithSecondPage(new DocumentModifyInfo[] { new DocumentModifyInfo {Version=new Version(1,0),ModifyDateTime=DateTime.Now,Remark="首次创建文档" } });
            doc.InsertSectionPageBreak();
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
        public static Paragraph WithFontSizeAlignment(this Paragraph paragraph,string name,double size,Alignment alignment,bool bold)
        {
            paragraph.Font(name).FontSize(size).Bold(bold).Alignment = alignment;
            return paragraph;
        }
        /// <summary>
        /// 导航到指定单元格
        /// </summary>
        /// <param name="table"></param>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Cell Cell(this Table table,int row,int index)
        {
          var sigleRow=   table.Rows[row];
          var cell=  sigleRow.Cells[index];
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
        public static Table SetBorderOuter(this Table table,Border border)
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
          return  table.SetBorderOuter(border).SetBorderInner(border);
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
            var sigleRow = table.Rows[row];
            var cell = sigleRow.Cells[index].Paragraphs[0];
            return cell;
        }
    }
    /// <summary>
    /// 修改信息
    /// </summary>
    public class DocumentModifyInfo
    {
        /// <summary>
        /// 版本
        /// </summary>
        public Version Version { get; set; }
        /// <summary>
        /// 修订日期
        /// </summary>
        public DateTime ModifyDateTime { get; set; }
        /// <summary>
        /// 修订原因
        /// </summary>
        public string Remark { get; set; }
    }
    /// <summary>
    /// 文档信息
    /// </summary>
    public class DocumentInfo
    {
        /// <summary>
        /// 文档名称
        /// </summary>
        public string DocumentName { get; set; } = "研发测试方案";
        /// <summary>
        /// 文档英文名称
        /// </summary>
        public string DocumentEnglishName { get ; set ; } = "R&D Test Scheme";
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; } = "利尔达科技集团股份有限公司";
        /// <summary>
        /// 公司英文名称
        /// </summary>
        public string CompanyEnglishName { get; set; } = "LIERDA SCIENCE & TECHNOLOGY GROUP CO., LTD";
        /// <summary>
        /// 公司LOGO图片
        /// </summary>

        public string CompanyLogoImage { get; set; } = @"C:\Users\WXF\Desktop\header.png";
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = "ZR水表生产测试上位机";
        /// <summary>
        /// 项目型号
        /// </summary>
        public string ProjectVersion { get; set; } = "LSD3SW-00201010";
        /// <summary>
        /// 项目版本
        /// </summary>
        public string ProjectModel { get; set; } = "V01";
        /// <summary>
        /// 提交日期
        /// </summary>
        public string ProjectDateTime { get; set; } = "2019-11-20";
        /// <summary>
        /// 文档落脚
        /// </summary>
        public string DocumentFooter { get; set; } = "";
    }
}
