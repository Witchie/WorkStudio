namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class CompanyInfo: IName
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 公司英文名称
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 公司图标文件
        /// </summary>
        public string CompanyLogoImage { get; set; }
    }
}
