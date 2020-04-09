using GreenWhale.BootLoader.Implements;
using GreenWhale.Extensions.TestTools2.Extensions;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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
    /// SerialPortView.xaml 的交互逻辑
    /// </summary>
    public partial class SerialPortView : UserControl
    {
        private readonly IExportBoxService exportBox;
        /// <summary>
        /// 串口编辑视图
        /// </summary>
        /// <param name="exportBox"></param>
        /// <param name="serialPort">串口对象</param>
        public SerialPortView(IExportBoxService exportBox, SerialPortServiceBase serialPort)
        {
            InitializeComponent();
            this.SerialPortServiceBase = serialPort;
            this.DataContext = this;           
            portList.ItemsSource = SerialPort.GetPortNames();
            this.exportBox = exportBox;
        }
        /// <summary>
        /// 串口
        /// </summary>
        public SerialPortServiceBase SerialPortServiceBase { get; private set; }
        /// <summary>
        /// 当前选中的端口名称
        /// </summary>
        public string SelectItem { get; set; }
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SerialPortServiceBase.Open();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (SerialPortServiceBase?.IsOpen==true && !string.IsNullOrEmpty(SelectItem))
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (SerialPortServiceBase?.IsOpen==true)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                SerialPortServiceBase.Close();
                SerialPortServiceBase.PortName = SelectItem;
                SerialPortServiceBase.Open();
                exportBox.Log("端口已开启");

            }
            catch (Exception err)
            {
                exportBox.Log(err.Message);
            }
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            SerialPortServiceBase.Close();
            exportBox.Log("端口已关闭");

        }
    }
}
