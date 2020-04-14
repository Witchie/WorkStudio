using DevExpress.Xpf.Core;
using GreenWhale.BootLoader.Implements;
using GreenWhale.BootLoader.Implements.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0;
using GreenWhale.BootLoader;
using GreenWhale.Application.SerialPorts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;

namespace LSD3GM0780_16E0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public  RequestDelegate RequestDelegate;
        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetName(ConstHelper.ApplicationName).SetWidthPercent(0.8).SetHeightPercent(0.8);
            var netCoreApplication = new NetCoreApplication<Application>(Application.Current, new AppSetting { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory, IsMutexApplication = true });
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
            this.Content = netCoreApplication.GetMainPage();
            var functionUIService = netCoreApplication.GetFunctionUI();
            functionUIService.UseLSD3GM0780_16E0();
            functionUIService.UseOutputBox();
        }
    }
}
