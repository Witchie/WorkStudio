using DevExpress.Xpf.Core;
using GreenWhale.BootLoader;
using GreenWhale.BootLoader.Implements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0;
using GreenWhale.Application.SerialPorts;
using GreenWhale.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;

namespace LSD3GM0780_16E0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NetCoreApplication<MainWindow, App> netCoreApplication;
        public static RequestDelegate RequestDelegate;
        public App()
        {
            this.netCoreApplication = new NetCoreApplication<MainWindow, App>(this, new AppSetting { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory, IsMutexApplication = true });
            var service = netCoreApplication.AddApplicationInfo().AddThemeName(Theme.VS2017BlueName).AddVsMode().AddLSD3GM0780_16E0(async ()=> 
            {
               var svc=  netCoreApplication.ServicesProvider.GetService<ISerialPortContext>();
               await  RequestDelegate?.Invoke(svc);
            },dataBase=> 
            {
                dataBase.UseSqlite("Data Source=Data.db");
            });
            var svc = netCoreApplication.BuildService();
            RequestDelegate = svc.MapSerialPort(s=> 
            {
                s.UseSerialPort();
            }).Build();
            var window = netCoreApplication.MainWindow();
            netCoreApplication.UseApplicationInfo().SetName(ConstHelper.ApplicationName).SetWidth(1000).SetHeight(700);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }
    }

}
