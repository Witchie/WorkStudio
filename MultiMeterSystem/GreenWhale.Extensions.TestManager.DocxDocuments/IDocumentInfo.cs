using System;
namespace GreenWhale.Extensions.TestManager.DocxDocuments
{
    /// <summary>
    /// 文档信息接口
    /// </summary>
    public interface IDocumentInfo
    {
        /// <summary>
        /// 文档名称
        /// </summary>
        string DocumentName { get; set; }
        /// <summary>
        /// 文档英文名称
        /// </summary>
        string DocumentEnglishName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        string CompanyName { get; set; }
        /// <summary>
        /// 公司英文名称
        /// </summary>
        string CompanyEnglishName { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        string ProjectName { get; set; }
        /// <summary>
        /// 项目版本
        /// </summary>
        string ProjectVersion { get; set; }
        /// <summary>
        /// 提交日期
        /// </summary>
        string ProjectDateTime { get; set; }
    }
}
