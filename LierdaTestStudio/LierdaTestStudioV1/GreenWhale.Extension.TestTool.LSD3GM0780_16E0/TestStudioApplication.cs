using DevExpress.Xpf.Core;
using GreenWhale.Application.SerialPorts;
using GreenWhale.BootLoader;
using GreenWhale.BootLoader.Implements;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0;
using System;
using System.Collections.Generic;
using System.Text;
using App = System.Windows.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GreenWhale.Extension.TestTool1
{
    public  class TestStudioApplication
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="application"></param>
        /// <param name="themeName"></param>
        /// <returns></returns>
        public Task<NetCoreApplication<TApplication>> StartAsync<TApplication>(TApplication application,string themeName= Theme.VS2017BlueName) where TApplication : App
        {
           return  Task.Run(() => {
               RequestDelegate RequestDelegate=null;
               var netCoreApplication = new NetCoreApplication<TApplication>(application, new AppSetting { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory, IsMutexApplication = true });
                var service = netCoreApplication.AddThemeName(Theme.VS2017BlueName).AddVsMode().AddLSD3GM0780_16E0(async () =>
                {
                    var svc = netCoreApplication.ServicesProvider.GetService<ISerialPortContext>();
                    await RequestDelegate?.Invoke(svc);
                }, dataBase =>
                {
                    dataBase.UseSqlite("Data Source=Data.db");
                });
                var svc = netCoreApplication.BuildService();
                RequestDelegate = svc.MapSerialPort(s =>
                {
                    s.UseSerialPort();
                }).Build();
               return netCoreApplication;
            });

        }
    }
}
