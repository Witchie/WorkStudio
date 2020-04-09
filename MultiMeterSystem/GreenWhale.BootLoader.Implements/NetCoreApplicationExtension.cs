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
        /// 获取应用程序信息
        /// </summary>
        /// <typeparam name="TRootWindow"></typeparam>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="coreApplication"></param>
        /// <returns></returns>
        public static ApplicationInfo UseApplicationInfo<TRootWindow, TApplication>(this NetCoreApplication<TRootWindow, TApplication> coreApplication) where TRootWindow : Window where TApplication : Application
        {
            if (coreApplication is null)
            {
                throw new ArgumentNullException(nameof(coreApplication));
            }
            var info = coreApplication.ServicesProvider.GetService<ApplicationInfo>();
            return info;
        }
        /// <summary>
        /// 获取主窗口
        /// </summary>
        /// <typeparam name="TRootWindow"></typeparam>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="coreApplication"></param>
        /// <returns></returns>
        public static TRootWindow MainWindow<TRootWindow, TApplication>(this NetCoreApplication<TRootWindow, TApplication> coreApplication) where TRootWindow : Window where TApplication : Application
        {
            if (coreApplication is null)
            {
                throw new System.ArgumentNullException(nameof(coreApplication));
            }

            var window = coreApplication.ServicesProvider.GetService<TRootWindow>();
            return window;
        }
        /// <summary>
        /// 设置主题名称 见 <see cref="Theme"/>
        /// </summary>
        /// <typeparam name="TRootWindow"></typeparam>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="coreApplication"></param>
        public static NetCoreApplication<TRootWindow, TApplication> AddThemeName<TRootWindow, TApplication>(this NetCoreApplication<TRootWindow, TApplication> coreApplication, string themeName) where TRootWindow : Window where TApplication : Application
        {
            ApplicationThemeHelper.ApplicationThemeName = themeName;
            return coreApplication;
        }
        /// <summary>
        /// 添加并配置应用程序信息
        /// </summary>
        /// <typeparam name="TRootWindow"></typeparam>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="coreApplication"></param>
        /// <param name="applicationInfo"></param>
        /// <returns></returns>
        public static NetCoreApplication<TRootWindow, TApplication> AddApplicationInfo<TRootWindow, TApplication>(this NetCoreApplication<TRootWindow, TApplication> coreApplication) where TRootWindow : Window where TApplication : Application
        {
            if (coreApplication is null)
            {
                throw new ArgumentNullException(nameof(coreApplication));
            }
            var services = coreApplication.ServiceBus;
            services.AddSingleton<ApplicationInfo>();
            return coreApplication;
        }
        /// <summary>
        /// 设置为VS模式
        /// </summary>
        /// <param name="coreApplication"></param>
        public static NetCoreApplication<TRootWindow, TApplication> AddVsMode<TRootWindow, TApplication>(this NetCoreApplication<TRootWindow, TApplication> coreApplication) where TRootWindow : Window where TApplication : Application
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
