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
using GreenWhale.Extensions.TestTools2.Extensions;
using GreenWhale.Extensions.Views;
using GreenWhale.Extensions.TestStudio;
using GreenWhale.BootLoader;
using Microsoft.Extensions.DependencyInjection;
namespace LSD3SWM_0710000000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        NetCoreApplication<Application> app;

        public MainWindow()
        {
            InitializeComponent();
        }

        public IApplicationInfo applicationInfo { get; }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            app = await Task.Run(() => {
                var info = new NetCoreApplication<Application>(Application.Current, new AppSetting { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory, IsMutexApplication = true });
                info.AddApplicationInfo().AddThemeName(Theme.Office2010Blue.Name).AddVsMode().AddTestStudio().AddMainWindow(this).BuildService();
                return info;
            });
            this.Content = app.ServicesProvider.GetService<MainPage>();
            var functionUIService= app.ServicesProvider.GetService<FunctionUIService>();
            functionUIService.UseTestStudio();
            functionUIService.UseOutputBox();
            app.GetApplicationInfo().SetName(Resource.ProjectModel);
            this.Medium().Center().WindowStyle(WindowStyle.SingleBorderWindow);

        }
    }
}
