using System;

namespace GreenWhale.RunTime.Scripts
{
    /// <summary>
    /// 服务总线
    /// </summary>
    public class ServiceBus
    {
        public ServiceBus(IServiceProvider iServiceProvider)
        {
            IServiceProvider = iServiceProvider;
        }

        /// <summary>
        /// 服务提供者
        /// </summary>
        public IServiceProvider IServiceProvider { get; private set; }
    }
}
