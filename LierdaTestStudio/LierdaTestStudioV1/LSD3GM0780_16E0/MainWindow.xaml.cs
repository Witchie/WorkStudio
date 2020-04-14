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
using GreenWhale.Extension.TestTool1;

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
        RequestDelegate RequestDelegate;
        private async void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetName(ConstHelper.ApplicationName).SetWidthPercent(0.8).SetHeightPercent(0.8);
            TestStudioApplication testStudio = new TestStudioApplication();
            var netCoreApplication=await  testStudio.StartAsync(App.Current);
            this.Content = netCoreApplication.GetMainPage();
            var functionUIService = netCoreApplication.GetFunctionUI();
            functionUIService.UseLSD3GM0780_16E0();
            functionUIService.UseOutputBox();
        }
    }
}
