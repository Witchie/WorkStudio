using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GreenWhale.BootLoader.Implements.Views
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : UserControl
    {
        private readonly GlobalLayout globalLayout;
        private readonly RibbonBar ribbonBar;
        public MainPage()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// 命令UI服务
        /// </summary>
        public IFunctionUIService FunctionUIService { get;private set; }
        public MainPage(IFunctionUIService commandUIService, GlobalLayout globalLayout, RibbonBar ribbonBar):this()
        {
            this.FunctionUIService = commandUIService;
            this.globalLayout = globalLayout;
            this.ribbonBar = ribbonBar;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.layout.Content == null)
            {
                this.layout.Content = globalLayout;
            }
            if (this.title.Content == null)
            {
                this.title.Content = ribbonBar;
            }
        }
    }
}
