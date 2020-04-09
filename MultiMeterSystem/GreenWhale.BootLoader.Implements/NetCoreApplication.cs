using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Threading;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// .NetCore 应用
    /// </summary>
    /// <typeparam name="TRootWindow">根窗口</typeparam>
    public class NetCoreApplication<TRootWindow,TApplication> : ApplicationBootLoader where TRootWindow:Window where TApplication:Application
    {
        private const string Caption = "发生错误，是否继续运行?";
        private const string Caption1 = "公共语言运行时错误";
        private const string Caption2 = "未处理的非CLR运行时错误";
        private const string Caption3 = "提示";
        private const string MessageBoxText = "是否查看详细错误信息?";

        /// <summary>
        ///  .NetCore 应用
        /// </summary>
        /// <param name="appSetting"></param>
        public NetCoreApplication(TApplication application,AppSetting appSetting) : base(appSetting)
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
            services.AddSingleton<TRootWindow>();
            ConfigureWiewPage(services);
            return services;
        }
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
                MessageBox.Show(e.ExceptionObject?.ToString(), Caption1);
            }
            else
            {
                MessageBox.Show(e.ExceptionObject?.ToString(), Caption2);
            }
        }

        /// <summary>
        /// 应用程序未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void AppUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var res = MessageBox.Show(e.Exception.Message, Caption, MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                e.Handled = true;
                if (MessageBox.Show(MessageBoxText, Caption3, MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                {
                    MessageBox.Show(e.Exception.ToString());
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
