using System.Linq;

namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// 服务资源信息
    /// </summary>
    public class ServiceInfo
    {
        public ServiceInfo(string serviceType)
        {
           var data=  serviceType.Split('/');
            ServiceType = data.FirstOrDefault();
            Version = data.Skip(1).FirstOrDefault();
        }
        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Version))
            {
                return ServiceType;
            }
            return $"{ServiceType}/{Version}";
        }
    }
}
