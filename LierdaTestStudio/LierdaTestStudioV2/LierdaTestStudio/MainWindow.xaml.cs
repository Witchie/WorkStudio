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

        public MainWindow()
        {
            InitializeComponent();
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            waiting.DeferedVisibility = true;
            this.Medium().Center().WindowStyle(WindowStyle.SingleBorderWindow).SetName(Resource.ProjectModel);
            TestStudioApplication testStudio = new TestStudioApplication();
            var app=await  testStudio.StartAsync(Application.Current, this);
            this.Content = app.GetMainPage();
            var ui = app.GetFunctionUI();
            ui.UseTestStudio();
            ui.UseOutputBox();
            waiting.DeferedVisibility = false;
        }
    }
}
