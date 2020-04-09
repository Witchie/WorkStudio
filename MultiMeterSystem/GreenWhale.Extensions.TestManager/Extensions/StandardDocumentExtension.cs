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
        /// 华文新魏
        /// </summary>
        private const string Name1 = "华文新魏";
        /// <summary>
        /// 宋体
        /// </summary>
        private const string Name2 = "宋体";

        /// <summary>
        /// 添加首页
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="documentName">文档名称</param>
        /// <param name="documentEnglishName">英文名称</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="companyEnglishName">公司英文名称</param>
        /// <returns></returns>
        public static DocX WithFirstPage<TPlan>(this DocX doc, TestProject<TPlan> info) where TPlan : TestPlan
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            var par = doc.InsertParagraph().Append(info.CompanyInfo.Name).Font(Name1).Bold().FontSize(26);
            par.LineSpacingBefore = 40.5f;
            par.LineSpacingAfter = 40.5f;
            par.Alignment = Alignment.center;
            var par2 = doc.InsertParagraph().Append(info.CompanyInfo.EnglishName).FontSize(9f).Font(Name1);
            par2.Alignment = Alignment.right;
            par2.SpacingAfter(0);
            par2.WithHorizontalLine(Color.DarkRed);
            doc.InsertParagraph().Append(info.DocumentNameInfo.Name).Bold().Font(Name1).FontSize(16).SpacingBefore(50).SpacingAfter(30).Alignment = Alignment.center;
            doc.InsertParagraph().Append(info.DocumentNameInfo.EnglishName).Bold().Font(Name1).FontSize(16).SpacingBefore(30).SpacingAfter(200).Alignment = Alignment.center;
            var dataTable = doc.InsertTable(4, 2);
            dataTable.CellValue(0, 0).Append("项目名称：").WithFontSizeAlignment(Name1, 16, Alignment.right, true);
            dataTable.CellValue(1, 0).Append("项目型号：").WithFontSizeAlignment(Name1, 16, Alignment.right, true);
            dataTable.CellValue(2, 0).Append("文件版本：").WithFontSizeAlignment(Name1, 16, Alignment.right, true);
            dataTable.CellValue(3, 0).Append("提交日期：").WithFontSizeAlignment(Name1, 16, Alignment.right, true);
            dataTable.CellValue(0, 1).Append(info.ProjectTitleInfo.Name).WithFontSizeAlignment(Name1, 16, Alignment.left, true);
            dataTable.CellValue(1, 1).Append(info.ProjectTitleInfo.Model).WithFontSizeAlignment(Name1, 16, Alignment.left, true);
            dataTable.CellValue(2, 1).Append(text: $"V{info.ProjectTitleInfo.Version.ToString()}").WithFontSizeAlignment(Name1, 16, Alignment.left, true);
            dataTable.CellValue(3, 1).Append(info.ProjectTitleInfo.DateTime.Date.ToLongDateString()).WithFontSizeAlignment(Name1, 16, Alignment.left, true);
            dataTable.Alignment = Alignment.right;
            dataTable.SetColumnWidth(0, 96);
            dataTable.SetColumnWidth(1, 223);
            dataTable.SetBorderAllNone();
            doc.InsertSectionPageBreak();
            doc.WithPageHeader(a => a.First, info.CompanyInfo.CompanyLogoImage);
            doc.WithPageFooter($"公司地址:{info.CompanyInfo.Address} Tel:{info.CompanyInfo.Telephone} Fax:{info.CompanyInfo.Fax}");
            return doc;
        }

        public static Paragraph WithHorizontalLineBlack(this Paragraph paragraph, HorizontalBorderPosition position = HorizontalBorderPosition.bottom)
        {
            paragraph.InsertHorizontalLine(position, BorderStyle.Tcbs_single, 6, 1, System.Drawing.Color.Black);
            return paragraph;
        }
        /// <summary>
        /// 添加目录页
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static DocX WithMenuPage<TPlan>(this DocX doc, TestProject<TPlan> info) where TPlan : TestPlan
        {
            doc.InsertParagraph("目录").WithFontSizeAlignment("黑体", 16, Alignment.center, true).SpacingBefore(30).SpacingAfter(30);
            doc.InsertParagraph().WithFontSizeAlignment("宋体", 11, Alignment.left, false);
            doc.AddHeaders();
            doc.AddFooters();
            doc.WithPageHeader(s => s.First, info.CompanyInfo.CompanyLogoImage);
            doc.InsertSectionPageBreak();
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
            doc.Footers.First.Paragraphs[0].Append(content).Font("宋体").FontSize(9).WithHorizontalLineBlack(HorizontalBorderPosition.top).Alignment = Alignment.left;
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
        public static DocX WithPageHeader(this DocX doc, Func<Headers, Header> header, string fileName, int height = 17, int width = 482, bool DifferentFirstPage = true)
        {
            doc.AddHeaders();
            doc.DifferentFirstPage = DifferentFirstPage;
            var image = doc.AddImage(fileName);
            var picture = image.CreatePicture(height, width);
            var res = header.Invoke(doc.Headers);
            res.Paragraphs[0].AppendPicture(picture).WithHorizontalLineBlack();
            return doc;
        }
        /// <summary>
        /// 添加第二页
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static DocX WithSecondPage(this DocX doc, DocumentModifyInfo enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            doc.InsertParagraph("文件修订历史").WithFontSizeAlignment("黑体", 16, Alignment.center, true).SpacingBefore(30).SpacingAfter(30);
            var table = doc.InsertTable(16, 3);
            table.SetWidths(new float[] { 60f, 72.6f, 360.4f });
            var dd = new float[] { 60f, 72.6f, 360.4f };
            table.Rows[0].Height = 12.4;
            table.Rows[0].Background(Color.LightBlue);
            table.CellValue(0, 0).Append("文件版本").WithFontSizeAlignment(Name2, 10, Alignment.center, true);
            table.CellValue(0, 1).Append("修订日期").WithFontSizeAlignment(Name2, 10, Alignment.center, true);
            table.CellValue(0, 2).Append("修订原因").WithFontSizeAlignment(Name2, 10, Alignment.center, true);
            if (enumerable.Count() > 0)
            {
                for (int i = 0; i < enumerable.Count(); i++)
                {
                    var item = enumerable[i];
                    var row = table.Rows[i + 1];
                    row.Cells[0].Paragraphs[0].Append($"V" + item.Version).WithFontSizeAlignment(Name2, 10, Alignment.center, false);
                    row.Cells[1].Paragraphs[0].Append(item.DateTime.Date.ToLongDateString()).WithFontSizeAlignment(Name2, 10, Alignment.center, false);
                    row.Cells[2].Paragraphs[0].Append(item.Remark).WithFontSizeAlignment(Name2, 10, Alignment.left, false);
                    row.Height = 12.4;
                }

                int count = 20 - table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    table.InsertRow().Height = 12.4;
                }
            }
            else
            {
                table.CellValue(1, 0).Append("未填写");
                table.CellValue(1, 1).Append("未填写");
                table.CellValue(1, 2).Append("未填写");
            }
            doc.InsertSectionPageBreak();
            return doc;
        }
        /// <summary>
        /// 添加第二页
        /// </summary>
        /// <typeparam name="TPlan"></typeparam>
        /// <param name="doc"></param>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static DocX WithSecondPage<TPlan>(this DocX doc, TestProject<TPlan> enumerable) where TPlan : TestPlan
        {
            return doc.WithSecondPage(enumerable.DocumentModifyInfo);
        }

        /// <summary>
        /// 添加第二页
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static DocX WithSecondPageNew(this DocX doc)
        {
            doc.WithSecondPage(new DocumentModifyInfo { new ModifyInfo(new Version(1, 0), DateTime.Now, "首次创建文档") });
            doc.InsertSectionPageBreak();
            return doc;
        }
        /// <summary>
        /// 添加主要内容
        /// </summary>
        /// <typeparam name="TPlan"></typeparam>
        /// <param name="docx"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public static DocX WithMainContent<TPlan>(this DocX docx, TestProject<TPlan> project) where TPlan : TestPlan
        {
           docx.Content1(project);
           docx.Content2(project);
           docx.Content3(project);
            return docx;
        }
        /// <summary>
        /// 添加页码
        /// </summary>
        /// <param name="docx"></param>
        /// <returns></returns>
        public static DocX WithPageNumber(this DocX docx)
        {
            docx.AddFooters();
            docx.AddHeaders();
            docx.Footers.Even.PageNumbers = true;
            docx.Footers.Odd.PageNumbers = true;
            return docx;
        }
        private const int leve0 = 0;
        private const int leve1 = 1;
        private const int leve2 = 2;
        private const int leve3 = 3;
        /// <summary>
        /// 添加概述信息
        /// </summary>
        /// <typeparam name="TPlan"></typeparam>
        /// <param name="docx"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        private static void Content1<TPlan>(this DocX docx,TestProject<TPlan> project) where TPlan : TestPlan
        {
            var list1 = docx.AddInsert( "概述",new Title(1,"章"), leve0);
            docx.AddInsert("目的", new Title("1.1"), leve1);
            docx.InsertParagraph(project.DescriptionInfo.Purpose.ToString()).WithMainContent();
            docx.AddInsert("术语", new Title("1.2"), leve1);
            docx.InsertParagraph(project.DescriptionInfo.Terms?.ToString()).WithMainContent();
            docx.AddInsert("参考资料", new Title("1.3"), leve1);
            docx.InsertParagraph(project.DescriptionInfo.References?.ToString()).WithMainContent();
            docx.InsertSectionPageBreak();
        }
        /// <summary>
        /// 系统分析
        /// </summary>
        /// <typeparam name="TPlan"></typeparam>
        /// <param name="docx"></param>
        /// <param name="root"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        private static void Content2<TPlan>(this DocX docx, TestProject<TPlan> project) where TPlan : TestPlan
        {
            var list1=  docx.AddInsert("测试需求分析", new Title(2, "章"), leve0);
            var t0 = new Title(2);
            docx.AddInsert("测试目的", t0.Add(1), leve1);
            docx.InsertParagraph(project.TestAnalysis.Purpose.ToString()).WithMainContent();
            docx.AddInsert("测试对象", t0.Add(2), leve1);
            docx.InsertParagraph(project.TestAnalysis.TestObject.ToString()).WithMainContent();
            docx.AddInsert("测试范围", t0.Add(3), leve1);
            docx.AddInsert("检测项", t0.Add(3).Add(1), leve2);
            docx.InsertParagraph(project.TestAnalysis.TestRange.ToString()).WithMainContent();
            docx.AddInsert("主要检测内容", t0.Add(3).Add(2), leve2);
            docx.InsertParagraph(project.TestAnalysis.TestContent.ToString()).WithMainContent();
            docx.AddInsert("系统环境", t0.Add(4), leve1);
            docx.AddInsert("硬件环境", t0.Add(4).Add(1), leve2);
            docx.InsertParagraph(project.TestAnalysis.EnviromentInfo.Hardware.ToString()).WithMainContent();
            docx.AddInsert("软件环境", t0.Add(4).Add(2), leve2);
            docx.InsertParagraph(project.TestAnalysis.EnviromentInfo.Software.ToString()).WithMainContent();
            docx.InsertSectionPageBreak();

        }
        /// <summary>
        /// 内容3
        /// </summary>
        /// <typeparam name="TPlan"></typeparam>
        /// <param name="docx"></param>
        /// <param name="root"></param>
        /// <param name="project"></param>
        /// <param name="onInvoke"></param>
        /// <returns></returns>
        private static void Content3<TPlan>(this DocX docx, TestProject<TPlan> project,Action<TPlan,Table> onInvoke=null) where TPlan : TestPlan
        {
            var list1 = docx.AddInsert("详细内容", new Title(3, "章"), leve0);
            var t3 = new Title(3);
            var categorys= project.TestPlans.GroupBy(s => s.Category).ToArray();
            for (int i = 0; i < categorys.Length; i++)
            {
                var item = categorys[i];
                var t1 = t3.Add(i + 1);
                var name = docx.AddInsert(item.Key, t1, leve1).SpacingBefore(15);
                int j = 1;
                foreach (var item1 in item)
                {
                    
                    var cTitle= t1.Add(j);
                    var item3 = docx.AddInsert(item1.Name, cTitle, leve2).SpacingBefore(15);
                    docx.AddInsert("测试目的", cTitle.Add(1), leve3).SpacingBefore(15);
                    docx.InsertParagraph(item1.Purpose.ToString()).WithMainContent();
                    docx.AddInsert("测试步骤", cTitle.Add(2), leve3);
                    docx.InsertParagraph();
                    int step = item1.TestSteps.Count;
                    var table = docx.InsertTable(step + 1, 4);
                    table.TableCaption = $"{item1.Name}测试步骤";
                    table.SetWidths(new float[] { 25, 250, 155, 45 });
                    table.CellValue(0, 0).Append("#").Bold().Font("宋体").FontSize(9);
                    table.CellValue(0, 1).Append("操作").Bold().Font("宋体").FontSize(9);
                    table.CellValue(0, 2).Append("预期值").Bold().Font("宋体").FontSize(9);
                    table.CellValue(0, 3).Append("附件").Bold().Font("宋体").FontSize(9);
                    for (int k = 0; k < item1.TestSteps.Count; k++)
                    {
                        var current = item1.TestSteps[k];
                        table.CellValue(k + 1, 0).Append((k + 1).ToString()).Font("宋体").FontSize(9);
                        table.CellValue(k + 1, 1).Append(current.Action).Font("宋体").FontSize(9);
                        table.CellValue(k + 1, 2).Append(current.Expected).Font("宋体").FontSize(9);
                        var attach = table.CellValue(k + 1, 3).Font("宋体").FontSize(9);
                        foreach (var content in current.Attach)
                        {
                            var link = docx.AddHyperlink(content.Name, content.Link);
                            attach.InsertHyperlink(link);
                            attach.AppendLine();
                        }
                    }
                    if (onInvoke != null)
                    {
                        onInvoke.Invoke(item1, table);
                    }
                    table.AutoFit = AutoFit.Contents;
                    docx.InsertParagraph();
                    j++;
                }

            }
        }
    }

}
