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
namespace LSD3GM0780_16E0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        private readonly FunctionUIService functionUIService;
        public MainWindow(ApplicationInfo applicationInfo, MainPage mainPage, FunctionUIService functionUIService)
        {
            InitializeComponent();
            applicationInfo.LoadUI(this);
            this.Content = mainPage;
            this.functionUIService = functionUIService ?? throw new ArgumentNullException(nameof(functionUIService));
        }

        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            functionUIService.UseLSD3GM0780_16E0();
            functionUIService.UseOutputBox();
        }
    }
}
