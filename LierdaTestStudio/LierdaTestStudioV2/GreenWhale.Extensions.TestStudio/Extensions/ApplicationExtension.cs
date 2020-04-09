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

namespace GreenWhale.Extensions.TestTools2.Extensions
{
    public static class ApplicationExtension
    {
        /// <summary>
        /// 添加测试工具
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <typeparam name="TApp"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static NetCoreApplication<TWindow, TApp> AddTestTool2<TWindow,TApp>(this NetCoreApplication<TWindow, TApp> app) where TWindow : Window where TApp : System.Windows.Application
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.ServiceBus.AddSingleton<SerialPortView>();
            app.ServiceBus.AddSingleton<ResourceDefineView>();
            app.ServiceBus.AddSingleton<TestResultView>();
            app.ServiceBus.AddTransient<ProtocalGenerator>();
            app.ServiceBus.AddSingleton<FactoryProduceView>();
            app.ServiceBus.AddSingleton<ScanPanelView>();
            app.ServiceBus.AddSingleton<IMessageTip,MessageTips>();
            app.ServiceBus.AddSingleton<ServiceBus>();
            app.ServiceBus.AddSingleton<ScriptRunner>();
            app.ServiceBus.AddSingleton<SerialPortServiceBase, SerialPortContextLocal>();
            app.ServiceBus.AddSingleton<IProjectViewServcie,ProjectViewServcie>();
            app.ServiceBus.AddSingleton<ISerialPortContext,SerialPortContext>();
            app.ServiceBus.AddSingleton<IEventBus, EventBus>();
            app.ServiceBus.AddSingleton<IDataStore, DataStore>();
            app.ServiceBus.AddTransient<IMessageBox,MessageBox>();
            app.ServiceBus.AddDbContext<ApplicationDbContext>(s =>
            {
                s.UseSqlite("dataSource=TestStudio.db");
            });
            return app;
        }
        /// <summary>
        /// 添加串口
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <typeparam name="TApp"></typeparam>
        /// <param name="coreApplication"></param>
        /// <param name="timeSpan">收到数据时 等待的时间</param>
        /// <param name="onReceiveData">收到数据时自动触发</param>
        /// <returns></returns>
        public static NetCoreApplication<TWindow, TApp> AddSerialPort<TWindow, TApp>(this NetCoreApplication<TWindow, TApp> coreApplication) where TWindow : Window where TApp : System.Windows.Application
        {
            var service = coreApplication.ServiceBus;
            SerialPort serialPort = new SerialPort();
            service.AddSingleton(serialPort);
            return coreApplication;
        }
        /// <summary>
        /// 调用应用程序管道
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IApplicationBuilder MapPipeline(this IServiceProvider service, Action<IApplicationBuilder> app)
        {
            ApplicationBuilder applicationBuilder = new ApplicationBuilder(service);
            app?.Invoke(applicationBuilder);
            return applicationBuilder;
        }
    }
}
