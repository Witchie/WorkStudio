using GreenWhale.Extensions.TestTools2.Extensions;
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

namespace GreenWhale.Extensions.TestTools2.Views
{
    /// <summary>
    /// ProtocalGenerator.xaml 的交互逻辑
    /// </summary>
    public partial class ProtocalGenerator : UserControl
    {
        public ProtocalGenerator()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(viewModel.FullFrame.ToHex());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txb.Text = viewModel.FullFrame.ToHex();
        }
    }
}
