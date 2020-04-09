using GreenWhale.BootLoader.Implements;
using System;
using System.Text;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Views;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Services;
using System.IO.Ports;
using GreenWhale.Application.SerialPorts;
using GreenWhale.Extensions.DependencyInjection;
using System.Threading.Tasks;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using DevExpress.Mvvm;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0
{

    public static class LSD3GM0780_16E0Extension
    {
        /// <summary>
        /// 添加请求
        /// </summary>
        /// <typeparam name="TRootWindow"></typeparam>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="coreApplication"></param>
        /// <param name="onReceiveData"></param>
        /// <returns></returns>
        public static NetCoreApplication<TRootWindow, TApplication> AddLSD3GM0780_16E0<TRootWindow, TApplication>(this NetCoreApplication<TRootWindow, TApplication> coreApplication, Action onReceiveData, Action<DbContextOptionsBuilder> optionsAction) where TRootWindow : Window where TApplication :System.Windows.Application
        {
            if (coreApplication is null)
            {
                throw new ArgumentNullException(nameof(coreApplication));
            }
            var service = coreApplication.ServiceBus;
            SerialPort serialPort = new SerialPort();
            service.AddSingleton(System.Windows.Application.Current.Dispatcher);
            service.AddSingleton<IStore, DataBaseStore>();
            service.AddSingleton<SerialPort>(serialPort);
            service.AddSingleton<ResourceStore>();
            //串口编辑视图
            service.AddSingleton<SerialPortView>();
            service.AddSingleton<ISerialPortViewServcie,SerialPortViewServcie>();
            //通用资源管理视图
            service.AddSingleton<ResourceDefineView>();
            service.AddSingleton<ResourceDefineViewService>();
            service.AddSingleton<ResourceDefineViewService>();
            //串口处理中间件
            service.AddTransient<SerialPortMiddleware>();
            //自动判定窗口
            service.AddTransient<AutoConfirmView>();
            service.AddSingleton<ResourceStore>();
            //生产
            service.AddSingleton<ProductProcessView>();
            service.AddSingleton<ISerialPortContext, SerialPortContext>();
            service.AddSingleton<LSD3GM0780_16E0Produce>();
            service.AddSingleton<DataTableCache>();
            service.AddDbContext<ILSD3GMDataContext,LSD3GMDataContext>(optionsAction);
            serialPort.DataReceived += async(s, e) => 
            {
                await  Task.Delay(300);
                onReceiveData.Invoke();
            };
            //测试结果视图
            service.AddTransient<TestResultView>();
            return coreApplication;
        }

        /// <summary>
        /// 调用应用程序管道
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IApplicationBuilder MapSerialPort(this IServiceProvider service,Action<IApplicationBuilder> app)
        {
            Application.SerialPorts.ApplicationBuilder applicationBuilder = new Application.SerialPorts.ApplicationBuilder(service);
            app?.Invoke(applicationBuilder);
            return applicationBuilder;
        }
        /// <summary>
        /// 调用串口中间件
        /// </summary>
        /// <param name="applicationBuilder"></param>     
        /// <returns></returns>
        public static IApplicationBuilder UseSerialPort(this IApplicationBuilder applicationBuilder)
        {
            var res=  applicationBuilder.ApplicationServices.GetRequiredService<ResourceStore>();
            var log = applicationBuilder.ApplicationServices.GetService<IExportBoxService>();
            var cache = applicationBuilder.ApplicationServices.GetService<DataTableCache>();
            applicationBuilder.Use(async (current,next)=> 
            {
                try
                {
                    var context = current.Read();
                    if (context.Content!=null&&context.Content.IndexOf("<")>=0)
                    {
                        int start = context.Content.IndexOf("<");
                        int end = context.Content.IndexOf(">");
                        var text= context.Content.Substring(start,(end-start)+1);
                        log.Log("RX " + context.Content);
                        RequestContent con = text;
                        var result = con.Get(current,res, normalMode =>
                        {
                            cache.SetNormalMode(normalMode);
                            current.Clear();
                            current.Write(ConstHelper.OnOK).Flush().WithLog(log);
                        }, valueModel =>
                        {
                            cache.SetValueMode(valueModel);
                            current.Clear();

                            current.Write(ConstHelper.OnOK).Flush().WithLog(log);
                        }, log);
                        if (text.Contains(ConstHelper.OnOK))
                        {
                            current.Clear();
                            cache.SetStartSuccess();
                            return;
                        }
                        if (text.Contains(ConstHelper.OnSuccess))
                        {
                            current.Clear();
                            current.Write(ConstHelper.OnOK).Flush().WithLog(log);
                            cache.SetSuccess();
                            return;
                        }
                    }

                    await next();

                }
                catch (Exception err)
                {
                    log.Log(err.Message);
                }

            });
            return applicationBuilder;
        }
    }
}
