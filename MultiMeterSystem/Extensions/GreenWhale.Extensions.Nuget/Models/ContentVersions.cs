using Newtonsoft.Json;

namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// 版本列表
    /// </summary>
    public class ContentVersions
    {
        /// <summary>
        /// 版本列表
        /// </summary>
        [JsonProperty("versions")]
        public string[] Versions { get; set; }
    }

}
