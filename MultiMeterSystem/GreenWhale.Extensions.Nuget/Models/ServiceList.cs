using Newtonsoft.Json;
using System.Linq;
namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// 服务列表
    /// </summary>
    public class ServiceList
    {
        /// <summary>
        /// 版本
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }
        /// <summary>
        /// 服务资源
        /// </summary>
        [JsonProperty("resources")]
        public ServiceResource[] Resource { get; set; }
        public ServiceResource this[string service]
        {
            get
            {
                var obj=  Resource.Where(p => p.ServiceInfo().ServiceType.Contains(service)).FirstOrDefault();
                return obj;
            }
        }
    }
}
