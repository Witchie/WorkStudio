using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using GreenWhale.Application.SerialPorts;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Views
{
    /// <summary>
    /// ProductProcess.xaml 的交互逻辑
    /// </summary>
    public partial class ProductProcessView : UserControl
    {
        private readonly ResourceStore resourceDictoarys;
        private readonly LSD3GM0780_16E0Produce lSD3GM0780_16E0Produce;
        private readonly ISerialPortContext serialPortContext;
        /// <summary>
        /// 操作耗时
        /// </summary>
        List<long> ActionCost = new List<long>();
        public ProductProcessView(ResourceStore resourceDictoarys, LSD3GM0780_16E0Produce lSD3GM0780_16E0Produce, ISerialPortContext serialPortContext)
        {
            InitializeComponent();
            this.resourceDictoarys = resourceDictoarys;
            this.DataContext = resourceDictoarys;
            this.lSD3GM0780_16E0Produce = lSD3GM0780_16E0Produce;
            this.serialPortContext= serialPortContext;
            this.DataContext = lSD3GM0780_16E0Produce;
            this.Loaded += ProductProcess_Loaded;
            DXGridDataController.DisableThreadingProblemsDetection = true;
        }


        private void ProductProcess_Loaded(object sender, RoutedEventArgs e)
        {
            lSD3GM0780_16E0Produce.DataTableCache.NewDataTable(Meterid);
        }
        /// <summary>
        /// 重新刷新加载数据源
        /// </summary>
        public void ReloadData()
        {
            grid.ItemsSource = lSD3GM0780_16E0Produce.DataTableCache.DataTables;
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.Content = "自动开始";
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.Content = "手动开始";
            }
        }
        /// <summary>
        /// 运行时状态是否显示
        /// </summary>
        /// <param name="isShow"></param>
        public  void Visible(bool isShow)
        {
            if (isShow)
            {
                stopwatch.Restart();
                costTime.Text = "计算中....";
                costTimeAverage.Text= "计算中....";
            }
            else
            {
                stopwatch.Stop();
                Press = false;
                costTime.Text = stopwatch.ElapsedMilliseconds + " ms";
                ActionCost.Add(stopwatch.ElapsedMilliseconds);
                costTimeAverage.Text= (ActionCost.Sum()/ ActionCost.Count) + " ms";
            }
            process.Visibility = isShow == true ? Visibility.Visible : Visibility.Hidden;
        }
        public bool Press
        {
            get => press.Visibility == Visibility; set
            {
                this.Dispatcher.Invoke(() => {
                    press.Visibility = value == true ? Visibility.Visible : Visibility.Hidden;
                });
            }
        }
        private void OnScan(string meterid)
        {
            this.Meterid = meterid;
            lSD3GM0780_16E0Produce.DataTableCache.NewDataTable(Meterid);
            if (light.IsChecked==true)
            {
                lSD3GM0780_16E0Produce.OnStart2(this.Meterid);

            }
            else
            {
                lSD3GM0780_16E0Produce.OnStart1(this.Meterid);
            }
            Visible(true);
        }
        public string Meterid { get;private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "生产记录|*.xlsx";
            saveFileDialog.FileName = $"{DateTime.Now.ToString("yyyyMMdd")}生产记录.xlsx";
            if (saveFileDialog.ShowDialog()==true)
            {
                tv.ExportToXlsx(saveFileDialog.FileName);
            }
        }
        Stopwatch stopwatch = new Stopwatch();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            meter.Clear();
            Visible(false);
        }

        private void meter_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (meter.Text!=null&&meter.Text.Length >= 16)
            //{
            //    Start();
            //}
        }
        private void Start()
        {
            var text = meter.Text;
            meter.Clear();
            OnScan(text);
        }
        private void meter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                Start();
            }
        }
    }
}
