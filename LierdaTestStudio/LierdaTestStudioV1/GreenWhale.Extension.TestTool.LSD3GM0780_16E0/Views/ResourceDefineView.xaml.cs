using System;
using System.Collections.Generic;
using System.IO;
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
using DevExpress.Xpf.Core;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Newtonsoft.Json;
namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Views
{
    /// <summary>
    /// ResourceDefineView.xaml 的交互逻辑
    /// </summary>
    public partial class ResourceDefineView : UserControl
    {
        private readonly IServiceProvider serviceProvider;
        public ResourceStore ResourceStore { get; private set; }
        private readonly IStore Store;
        public ResourceDefineView(IServiceProvider serviceProvider, ResourceStore resourceStore, IStore Store)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.ResourceStore = resourceStore;
            this.DataContext = this;
            this.Store = Store;
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgv?.SelectedItem !=null)
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
            var view=  serviceProvider.GetService<AddResourceView>();
            view.AddResourceViewModel.ID = ResourceStore.Count + 1;
            var dd=   view.SetWindow<ThemedWindow>().SetWidth(400).SetSizeToContent(SizeToContent.Height).WindowStyle(WindowStyle.ToolWindow).SetStartupLocation(WindowStartupLocation.CenterScreen).ShowDialog();
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            if (dgv?.SelectedItem is ResourceDefine<ValueModel, ValueModel> model)
            {
                ResourceStore.Remove(model);
            }
        }

        private void CommandBinding_CanExecute_2(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute= ResourceStore!=null&& ResourceStore.Count > 0; ;
        }

        private void CommandBinding_Executed_2(object sender, ExecutedRoutedEventArgs e)
        {
            Store.Save(ResourceStore);
        }

        private void CommandBinding_Executed_3(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            saveFileDialog.Filter = "协议文件|*.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                var text = JsonConvert.DeserializeObject<ResourceStore>(File.ReadAllText(saveFileDialog.FileName));
                ResourceStore.Clear();
                foreach (var item in text)
                {
                    ResourceStore.Add(item);
                }
            }

        }

        private void CommandBinding_Executed_4(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "协议文件|*.json";
            saveFileDialog.FileName = ConstHelper.ApplicationName + ".json";
            if (saveFileDialog.ShowDialog()==true)
            {
                var text=  JsonConvert.SerializeObject(ResourceStore,Formatting.Indented);
                File.WriteAllText(saveFileDialog.FileName,text);
            }
        }

        private void CommandBinding_CanExecute_3(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_5(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "协议文件|*.xlsx";
            saveFileDialog.FileName = ConstHelper.ApplicationName + ".xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                tv.ExportToXlsx(saveFileDialog.FileName);
            }
        }

        private void CommandBinding_CanExecute_4(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_6(object sender, ExecutedRoutedEventArgs e)
        {
            var data = Store.Read();
            if (data!=null)
            {
                ResourceStore.Clear();
                foreach (var item in data)
                {
                    ResourceStore.Add(item);
                }
            }
            else
            {
                MessageBox.Show("存储区不存在或不支持");
            }
        }
    }
}
