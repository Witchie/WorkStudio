using GreenWhale.BootLoader.Implements;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Services;
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

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Views
{
    /// <summary>
    /// SerialPortView.xaml 的交互逻辑
    /// </summary>
    public partial class SerialPortView : UserControl
    {
        private readonly IExportBoxService exportBox;
        public SerialPortView(IExportBoxService exportBox, SerialPort serialPort)
        {
            this.SerialPort = serialPort;
            InitializeComponent();
            this.DataContext = this;
            portList.ItemsSource = SerialPort.GetPortNames();
            this.exportBox = exportBox;
        }
        /// <summary>
        /// 串口
        /// </summary>
        public SerialPort SerialPort { get; private set; }
        /// <summary>
        /// 当前选中的端口名称
        /// </summary>
        public string SelectItem { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SerialPort.Open();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (SerialPort.IsOpen&&!string.IsNullOrEmpty(SelectItem))
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
            if (SerialPort.IsOpen)
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
                SerialPort.Close();
                SerialPort.PortName = SelectItem;
                SerialPort.Open();
                exportBox.Log(ConstHelper.ApplicationName, "端口已开启");

            }
            catch (Exception err)
            {
                exportBox.Log(ConstHelper.ApplicationName,err.Message);
            }
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            SerialPort.Close();
            exportBox.Log(ConstHelper.ApplicationName, "端口已关闭");

        }
    }
}
