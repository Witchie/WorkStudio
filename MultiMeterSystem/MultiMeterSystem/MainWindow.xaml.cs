using DevExpress.Xpf.Core;
using GreenWhale.BootLoader;
using GreenWhale.BootLoader.Implements;
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
using Microsoft.Extensions.DependencyInjection;
using GreenWhale.BootLoader.Implements.Views;
using GreenWhale.BootLoader.Implements.ProjectManager;
namespace MultiMeterSystem
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
        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var app = new NetCoreApplication(new AppSetting(AppDomain.CurrentDomain.BaseDirectory, true));
            var service = app.AddThemeName(Theme.VS2017BlueName).AddMainWindow(this).AddVsMode().AddProjectManager().BuildService();
            this.SetName("测试应用").SetWidth(1000).SetHeight(800);
            var ui = app.GetFunctionUI();
            ui.UseOutputBox();
            ui.AddProjectManager();
            this.Content = app.GetMainPage();
        }
    }
}
