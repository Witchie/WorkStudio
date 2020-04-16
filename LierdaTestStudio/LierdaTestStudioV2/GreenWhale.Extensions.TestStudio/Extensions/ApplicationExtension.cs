using GreenWhale.Application.SerialPorts;
using GreenWhale.BootLoader.Implements;
using System;
using System.IO.Ports;
using System.Text;
using System.Windows;
using GreenWhale.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using GreenWhale.Extensions.TestTools2.Views;
using GreenWhale.Extensions.TestTools2.Models;
using GreenWhale.RunTime.Scripts;
using Microsoft.EntityFrameworkCore;
using GreenWhale.Extensions.TestStudio.Views;

namespace GreenWhale.Extensions.TestTools2.Extensions
{
    public static class ApplicationExtension
    {
        /// <summary>
        /// 获取GetRibbonBar
        /// </summary>
        /// <typeparam name="TApp"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static RibbonBar GetRibbonBar<TApp>(this NetCoreApplication<TApp> app)where TApp : System.Windows.Application
        {
            return   app.ServicesProvider.GetService<RibbonBar>();
        }

        /// <summary>
        /// 添加测试实验室
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <typeparam name="TApp"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static NetCoreApplication<TApp> AddTestStudio<TApp>(this NetCoreApplication<TApp> app,Action<DbContextOptionsBuilder> dataContextbuild=null  ) where TApp : System.Windows.Application
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.ServiceBus.AddSingleton<SerialPortView>();
            app.ServiceBus.AddTransient<ResourceDefineView>();
            app.ServiceBus.AddSingleton<TestResultView>();
            app.ServiceBus.AddTransient<ProtocalGenerator>();
            app.ServiceBus.AddSingleton<FactoryProduceView>();
            app.ServiceBus.AddSingleton<ScanPanelView>();
            app.ServiceBus.AddTransient<ChangedLogView>();
            app.ServiceBus.AddTransient<HelpView>();
            app.ServiceBus.AddSingleton<IMessageTip,MessageTips>();
            app.ServiceBus.AddSingleton<ServiceBus>();
            app.ServiceBus.AddSingleton<ScriptRunner>();
            app.ServiceBus.AddSingleton<SerialPortServiceBase, SerialPortContextLocal>();
            app.ServiceBus.AddSingleton<IProjectViewServcie,ProjectViewServcie>();
            app.ServiceBus.AddSingleton<ISerialPortContext,SerialPortContext>();
            app.ServiceBus.AddSingleton<IEventBus, EventBus>();
            app.ServiceBus.AddSingleton<IDataStore, DataStore>();
            app.ServiceBus.AddTransient<IMessageBox, MessageBox>();
            app.ServiceBus.AddDbContext<ApplicationDbContext>(s =>
            {
                if (dataContextbuild==null)
                {
                    s.UseSqlite("dataSource=TestStudio.db");
                }
            });
            SerialPort serialPort = new SerialPort();
            app.ServiceBus.AddSingleton(serialPort);
            return app;
        }
    }
}
