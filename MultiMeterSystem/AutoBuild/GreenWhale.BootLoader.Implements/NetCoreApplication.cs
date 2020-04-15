using DevExpress.Xpf.Core;
using GreenWhale.BootLoader.Properties;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Threading;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// NETCore 应用
    /// </summary>
    public class NetCoreApplication : NetCoreApplication<Application>
    {
        /// <summary>
        /// NETCore 应用
        /// </summary>
        /// <param name="appSetting"></param>
        public NetCoreApplication(AppSetting appSetting=null) : base(Application.Current, appSetting??new AppSetting(AppContext.BaseDirectory,true))
        {

        }
    }
    /// <summary>
    ///  .NetCore 应用
    /// </summary>
    /// <typeparam name="TApplication">根应用程序</typeparam>
    public class NetCoreApplication<TApplication> : ApplicationBootLoader where TApplication:Application
    {

        /// <summary>
        /// .NetCore 应用
        /// </summary>
        /// <param name="application">根应用</param>
        /// <param name="appSetting">系统配置</param>
        public NetCoreApplication(TApplication application, AppSetting appSetting) : base(appSetting)
        {
            CurrentApplication = application;
        }
        /// <summary>
        /// 当前应用程序
        /// </summary>
        public TApplication CurrentApplication { get;private set; }
        /// <summary>
        /// 配置应用实践
        /// </summary>
        public event ServiceHandle ConfigureApplicationEvent;
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected virtual IServiceCollection ConfigureApplication(IServiceCollection services)
        {
            ConfigureApplicationEvent?.Invoke(services);
            services.AddSingleton(CurrentApplication);
            ConfigureWiewPage(services);
            return services;
        }
        /// <summary>
        /// 编译完成后触发
        /// </summary>
        protected override void OnBuildComplect()
        {
            var app = Application.Current;
            AppDomain.CurrentDomain.UnhandledException += UnhandledDomainException;
            app.DispatcherUnhandledException += AppUnhandledException;
            base.OnBuildComplect();
        }
        /// <summary>
        /// 未处理的应用程序领域错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void UnhandledDomainException(object sender, UnhandledExceptionEventArgs e)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (e.IsTerminating)
            {
                MessageBox.Show(e.ExceptionObject?.ToString(), Resource.clr_running_time_exception);
            }
            else
            {
                MessageBox.Show(e.ExceptionObject?.ToString(), Resource.unhandle_clr_runing_time_exception);
            }
        }

        /// <summary>
        /// 应用程序未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void AppUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            var res = DXMessageBox.Show(e.Exception.Message, Resource.on_error_will_continue, MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                e.Handled = true;
                if (DXMessageBox.Show(Resource.do_you_see_detail_exception, Resource.message_tips, MessageBoxButton.YesNo,MessageBoxImage.Error)==MessageBoxResult.Yes)
                {
                    DXMessageBox.Show(e.Exception.ToString());
                }
            }
        }
        /// <summary>
        /// 添加视图页面事件
        /// </summary>
        public event ServiceHandle ConfigureWiewPageHandler;
        /// <summary>
        /// 添加视图页
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected virtual IServiceCollection ConfigureWiewPage(IServiceCollection services) 
        {
            ConfigureWiewPageHandler?.Invoke(services);
            return services;
        }
        /// <summary>
        /// 配置UI对象
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override IServiceCollection ConfigureUIElement(IServiceCollection services)
        {
            return ConfigureApplication(services);
        }
    }
}
