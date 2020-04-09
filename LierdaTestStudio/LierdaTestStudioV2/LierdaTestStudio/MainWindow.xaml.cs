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
namespace LSD3SWM_0710000000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        private readonly FunctionUIService functionUIService;
        public MainWindow(FunctionUIService functionUIService, ApplicationInfo applicationInfo,MainPage mainPage)
        {
            InitializeComponent();
            this.functionUIService = functionUIService;
            applicationInfo.LoadUI(this);
            this.Content = mainPage;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            functionUIService.UseTestTool2();
            functionUIService.UseOutputBox();
        }
    }
}
