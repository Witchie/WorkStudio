using GreenWhale.BootLoader.Implements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DevExpress;
using DevExpress.Xpf.Core;
using Microsoft.Extensions.DependencyInjection;
using GreenWhale.BootLoader;
using GreenWhale.ProjectManager;
using GreenWhale.BootLoader.Implements.ProjectManager;
namespace MultiMeterSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NetCoreApplication<MainWindow,App> netCoreApplication;

        public App()
        {
            this.netCoreApplication = new NetCoreApplication<MainWindow, App>(this,new AppSetting { BaseDirectory=AppDomain.CurrentDomain.BaseDirectory,IsMutexApplication=true });
            var service = netCoreApplication.AddApplicationInfo().AddThemeName(Theme.VS2017BlueName).AddVsMode().AddProjectManager();
            service.BuildService();
            var window = netCoreApplication.MainWindow();
            netCoreApplication.UseApplicationInfo().SetName("测试应用").SetWidth(1000).SetHeight(800);
            window.Show();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            throw new NotImplementedException();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
