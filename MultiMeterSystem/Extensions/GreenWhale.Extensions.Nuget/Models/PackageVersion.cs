using Newtonsoft.Json;
namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// 包版本
    /// </summary>
    public class PackageVersion
    {
        /// <summary>
        /// 版本信息
        /// </summary>
       [JsonProperty("version")] 
        public string Version { get; set; }
        /// <summary>
        /// 下载总次数
        /// </summary>
        [JsonProperty("downloads")] 
        public ulong Downloads { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        [JsonProperty("@id")]
        public string Id { get; set; }
    }
}
