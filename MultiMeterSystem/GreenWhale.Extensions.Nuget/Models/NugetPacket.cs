using Newtonsoft.Json;
namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// NUEGT包
    /// </summary>
    public class NugetPacket
    {
        /// <summary>
        /// 注册索引的绝对地址
        /// </summary>
        [JsonProperty("registration")]
        public string Registration { get; set; }
        /// <summary>
        /// ID
        /// </summary>
       [JsonProperty("id")]
        public string ID { get; set; }
        /// <summary>
        /// 包类型
        /// </summary>
        [JsonProperty("@type")]
        public string PackageType { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// 汇总
        /// </summary>
        [JsonProperty("summary")] 
        public string Summary { get; set; }
        /// <summary>
        /// 包类型
        /// </summary>
        [JsonProperty("packageTypes")]
        public PackageType[] PackageTypes { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")] 
        public string Title { get; set; }
        /// <summary>
        /// 许可证地址
        /// </summary>
        [JsonProperty("licenseUrl")]
        public string LicenseUrl { get; set; }
        /// <summary>
        /// 项目地址
        /// </summary>
        [JsonProperty("projectUrl")]
        public string ProjectUrl { get; set; }
        /// <summary>
        /// 图标地址
        /// </summary>
        [JsonProperty("iconUrl")]
        public string IconUrl { get; set; }
        /// <summary>
        /// 标记信息
        /// </summary>
        [JsonProperty("tags")]
        public string[] Tags { get; set; }
        /// <summary>
        /// 作者信息
        /// </summary>
        [JsonProperty("authors")]
        public string[] Authors { get; set; }
        /// <summary>
        /// 下载总次数
        /// </summary>
        [JsonProperty("totalDownloads")]
        public int TotalDownloads { get; set; }
        /// <summary>
        /// 验证信息
        /// </summary>
        [JsonProperty("verified")]
        public bool Verified { get; set; }
        /// <summary>
        /// 版本信息
        /// </summary>
        [JsonProperty("versions")]
        public PackageVersion[] Versions { get; set; }
    }
    /// <summary>
    /// 包类型
    /// </summary>
    public class PackageType
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
