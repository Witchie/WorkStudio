using Newtonsoft.Json;

namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// 服务资源
    /// </summary>
    public class ServiceResource
    {
        /// <summary>
        /// 地址信息
        /// </summary>
        [JsonProperty("@id")]
        public string ID { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        [JsonProperty("@type")]
        public string ServiceType { get; set; }
        /// <summary>
        /// 注释文件
        /// </summary>
        [JsonProperty("comment")]
        public string Commment { get; set; }
        public override string ToString()
        {
            return ServiceType;
        }
    }
}
