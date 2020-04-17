namespace GreenWhale.BootLoader.Extensions.Nuget
{
    public static class ServiceResourceExtension
    {
        /// <summary>
        /// 获取服务资源信息
        /// </summary>
        /// <param name="serviceResource"></param>
        /// <returns></returns>
        public static ServiceInfo ServiceInfo(this ServiceResource serviceResource)
        {
            return new ServiceInfo(serviceResource.ServiceType);
        }
    }
}
