using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;

namespace GreenWhale.BootLoader
{
    /// <summary>
    /// 应用启动器
    /// </summary>
    public class ApplicationBootLoader
    {
        private readonly AppSetting appSetting;
        /// <summary>
        /// 服务总线 导入空间<see cref="Microsoft.Extensions.DependencyInjection"/>
        /// </summary>
        public ServiceCollection ServiceBus { get;private set; } = new ServiceCollection();
        /// <summary>
        /// 应用启动器
        /// </summary>
        /// <param name="appSetting"></param>
        public ApplicationBootLoader(AppSetting appSetting)
        {
            this.appSetting = appSetting;
        }
        /// <summary>
        /// 构建服务,所有处理事件，必须在构建之前注册
        /// </summary>
        public IServiceProvider BuildService()
        {
            if (appSetting.IsMutexApplication)
            {
                MutexApplication();
            }
            ConfigureUIElement(ConfigureService(ConfigureSelfService(ServiceBus)));
            ServicesProvider = ServiceBus.BuildServiceProvider();
            OnBuildComplect();
            return ServicesProvider;
        }
        /// <summary>
        /// 当整个应用程序注入框架启动后
        /// </summary>
        protected virtual void OnBuildComplect() { }
        /// <summary>
        /// 线程互斥应用
        /// </summary>
        private void MutexApplication()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    process.Kill();
                }
            }
        }
        /// <summary>
        /// 配置框架服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection ConfigureSelfService(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder().SetBasePath(appSetting.BaseDirectory);
            if (File.Exists(AppSettingFile()))
            {
                builder = builder.AddJsonFile(AppSettingFile(), false, true);
            }
            var configuration = builder.Build();
            services.AddSingleton(configuration);
            return services;
        }
        /// <summary>
        /// 服务总线
        /// </summary>
        public IServiceProvider ServicesProvider { get;private set; }
        /// <summary>
        /// 调试JSON文件
        /// </summary>
        /// <returns></returns>
        protected virtual string AppSettingFile() => "appsettings.json";
        /// <summary>
        /// 配置服务事件
        /// </summary>
        public event ServiceHandle ConfigureServiceEvent;
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services">服务总线</param>
        protected virtual IServiceCollection ConfigureService(IServiceCollection services) 
        {
            ConfigureServiceEvent?.Invoke(services);
            return services;
        }
        /// <summary>
        /// 配置UI对象
        /// </summary>
        public event ServiceHandle ConfigureUIElementEvent;
        /// <summary>
        /// 配置UI对象
        /// </summary>
        /// <param name="services">服务总线</param>
        protected virtual IServiceCollection ConfigureUIElement(IServiceCollection services) 
        {
            ConfigureUIElementEvent?.Invoke(services);
            return services;
        }
    }
    /// <summary>
    /// 服务委托
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public delegate IServiceCollection ServiceHandle(IServiceCollection services);
}
