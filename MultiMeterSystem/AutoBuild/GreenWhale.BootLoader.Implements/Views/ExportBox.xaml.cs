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
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.ComponentModel;
using GreenWhale.BootLoader.Implements.Properties;
using Microsoft.Extensions.DependencyInjection;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// ExportBox.xaml 的交互逻辑
    /// </summary>
    public partial class ExportBox : UserControl,INotifyPropertyChanged
    {
        private string currentSource;
        private IServiceProvider serviceProvider;
        /// <summary>
        /// 输出框
        /// </summary>
        public ExportBox(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.DataContext = this;
            IsAutoScroll = true;
            _autoWrap.IsChecked = true;
            this.serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 当前数据源
        /// </summary>
        public string CurrentSource
        {
            get
            {
                if (currentSource == null && _exportSource.ItemsSource != null)
                {
                    _exportSource.SelectedItem = _exportSource.ItemsSource.Cast<string>().FirstOrDefault();
                }
                return currentSource;
            }

            set
            {
                currentSource = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(CurrentSource)));
            }
        }
        /// <summary>
        /// 是否自动滚动
        /// </summary>
        public bool IsAutoScroll { get; set; }
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="content"></param>
        public void Log(string content)
        {
            _logBox.AppendText($"【{DateTime.Now.ToLongTimeString()}】{content}{Environment.NewLine}");
            if (IsAutoScroll)
            {
                _logBox.ScrollToEnd();
            }
        }
        /// <summary>
        /// 清空日志
        /// </summary>
        public void Clear()
        {
            this._logBox.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void _autoWrap_Checked(object sender, RoutedEventArgs e)
        {
            _logBox.TextWrapping = TextWrapping.Wrap;
            _autoWrap.Content =Resource.new_line;
        }

        private void _autoWrap_Unchecked(object sender, RoutedEventArgs e)
        {
            _logBox.TextWrapping = TextWrapping.NoWrap;
            _autoWrap.Content = Resource.not_new_line;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var box=  serviceProvider.GetService<IExportBoxService>();
            box.Clear();
        }
    }

}
