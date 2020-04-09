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
        private readonly FunctionUIService functionUIService;
        public MainWindow(ApplicationInfo applicationInfo, MainPage mainPage, FunctionUIService functionUIService)
        {
            if (applicationInfo is null)
            {
                throw new ArgumentNullException(nameof(applicationInfo));
            }

            if (mainPage is null)
            {
                throw new ArgumentNullException(nameof(mainPage));
            }

            InitializeComponent();
            applicationInfo.LoadUI(this);
            this.Content = mainPage;
            this.functionUIService = functionUIService ?? throw new ArgumentNullException(nameof(functionUIService));
        }
        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
          //  functionUIService.AddDefaultDocument(new WebBrowser() { Source = new Uri("https://www.lierda.com/") });
             functionUIService.UseOutputBox();
            functionUIService.AddProjectManager();
        }
    }
}
