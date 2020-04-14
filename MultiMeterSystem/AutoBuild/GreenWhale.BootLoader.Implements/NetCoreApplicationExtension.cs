using DevExpress.Xpf.Core;
using GreenWhale.BootLoader.Implements.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 应用扩展
    /// </summary>
    public static class NetCoreApplicationExtension
    {
        /// <summary>
        /// 获取主窗口
        /// </summary>
        /// <typeparam name="TRootWindow"></typeparam>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="coreApplication"></param>
        /// <returns></returns>
        public static TRootWindow GetMainWindow<TApplication, TRootWindow>(this NetCoreApplication<TApplication> coreApplication)  where TApplication : Application where TRootWindow:Window
        {
            if (coreApplication is null)
            {
                throw new System.ArgumentNullException(nameof(coreApplication));
            }

            var window = coreApplication.ServicesProvider.GetService<TRootWindow>();
            return window;
        }
        /// <summary>
        /// 获取主页面
        /// </summary>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="netCoreApplication"></param>
        /// <returns></returns>
        public static MainPage GetMainPage<TApplication>(this NetCoreApplication<TApplication> netCoreApplication) where TApplication : Application
        {
            if (netCoreApplication is null)
            {
                throw new ArgumentNullException(nameof(netCoreApplication));
            }
            return  netCoreApplication.ServicesProvider.GetService<MainPage>();
        }
        /// <summary>
        /// 获取输出框
        /// </summary>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="netCoreApplication"></param>
        /// <returns></returns>
        public static IExportBoxService GetExportBox<TApplication>(this NetCoreApplication<TApplication> netCoreApplication) where TApplication : Application
        {
            if (netCoreApplication is null)
            {
                throw new ArgumentNullException(nameof(netCoreApplication));
            }
            return netCoreApplication.ServicesProvider.GetService<IExportBoxService>();
        }
        /// <summary>
        /// 获取功能UI
        /// </summary>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="netCoreApplication"></param>
        /// <returns></returns>
        public static FunctionUIService GetFunctionUI<TApplication>(this NetCoreApplication<TApplication> netCoreApplication) where TApplication : Application
        {
            if (netCoreApplication is null)
            {
                throw new ArgumentNullException(nameof(netCoreApplication));
            }
            return netCoreApplication.ServicesProvider.GetService<FunctionUIService>();
        }
        /// <summary>
        /// 设置主题名称 见 <see cref="Theme"/>
        /// </summary>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="coreApplication"></param>
        /// <param name="themeName">主题名称</param>
        public static NetCoreApplication<TApplication> AddThemeName< TApplication>(this NetCoreApplication<TApplication> coreApplication, string themeName) where TApplication : Application
        {
            ApplicationThemeHelper.ApplicationThemeName = themeName;
            return coreApplication;
        }
        /// <summary>
        /// 添加并配置应用程序信息
        /// </summary>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="coreApplication"></param>
        /// <returns></returns>
        [Obsolete("该功能已经取消，请直接操作Window",true)]
        public static NetCoreApplication<TApplication> AddApplicationInfo< TApplication>(this NetCoreApplication<TApplication> coreApplication) where TApplication : Application
        {
            return coreApplication;
        }
        /// <summary>
        /// 添加主窗口
        /// </summary>
        /// <typeparam name="TApplication"></typeparam>
        /// <typeparam name="TMainWindow"></typeparam>
        /// <param name="coreApplication"></param>
        /// <param name="window"></param>
        /// <returns></returns>
        public static NetCoreApplication<TApplication> AddMainWindow<TApplication,TMainWindow>(this NetCoreApplication<TApplication> coreApplication,TMainWindow window=default) where TMainWindow:Window where TApplication : Application
        {
            if (coreApplication is null)
            {
                throw new ArgumentNullException(nameof(coreApplication));
            }
            var services = coreApplication.ServiceBus;
            if (window==null)
            {
                services.AddSingleton<TMainWindow>();
            }
            else
            {
                services.AddSingleton(window);
            }
            return coreApplication;
        }
        /// <summary>
        /// 设置为VS模式
        /// </summary>
        /// <param name="coreApplication"></param>
        public static NetCoreApplication<TApplication> AddVsMode< TApplication>(this NetCoreApplication<TApplication> coreApplication)where TApplication : Application
        {
            if (coreApplication is null)
            {
                throw new ArgumentNullException(nameof(coreApplication));
            }
            var services = coreApplication.ServiceBus;
            services.AddSingleton<RibbonBarService>();
            services.AddTransient<PanelService>();
            services.AddSingleton<IExportBoxService, ExportBoxService>();
            services.AddSingleton<FunctionUIService>();
            services.AddTransient<OutputBoxCommandService>();
            services.AddSingleton<RibbonBar>();
            services.AddSingleton<ExportBox>();
            services.AddSingleton<GlobalLayout>();
            services.AddSingleton<MainPage>();
            return coreApplication;
        }
       
    }
}
