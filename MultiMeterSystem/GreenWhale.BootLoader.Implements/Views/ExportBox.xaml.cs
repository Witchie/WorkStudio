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

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// ExportBox.xaml 的交互逻辑
    /// </summary>
    public partial class ExportBox : UserControl,INotifyPropertyChanged
    {
        private string currentSource;

        public ExportBox()
        {
            InitializeComponent();
            this.DataContext = this;
            IsAutoScroll = true;
            _autoWrap.IsChecked = true;
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
        public bool IsAutoScroll { get; set; }
        public void Log(string content)
        {
            _logBox.AppendText($"【{DateTime.Now.ToLongTimeString()}】{content}{Environment.NewLine}");
            if (IsAutoScroll)
            {
                _logBox.ScrollToEnd();
            }
        }
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
            _autoWrap.Content = "换行";
        }

        private void _autoWrap_Unchecked(object sender, RoutedEventArgs e)
        {
            _logBox.TextWrapping = TextWrapping.NoWrap;
            _autoWrap.Content = "不换行";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
