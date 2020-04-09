using GreenWhale.Extensions.TestTools2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Linq;
using GreenWhale.BootLoader.Implements;
using GreenWhale.Extensions.TestTools2.Extensions;
using Microsoft.Win32;
using GreenWhale.RunTime.Scripts;

namespace GreenWhale.Extensions.TestTools2.Views
{
    /// <summary>
    /// FactoryProduceView.xaml 的交互逻辑
    /// </summary>
    public partial class FactoryProduceView : UserControl
    {
        private readonly IDataStore dataStore;
        private readonly IEventBus eventBus;
        private readonly IExportBoxService exportBoxService;
        private readonly IMessageBox messageBox;
        public FactoryProduceView(IDataStore dataStore, IEventBus eventBus, IExportBoxService exportBoxService, IMessageBox messageBox)
        {
            InitializeComponent();
            dtEnd.DateTime = DateTime.Now.AddDays(1).Date;
            dtStart.DateTime = DateTime.Now.AddDays(-2).Date;
            this.exportBoxService = exportBoxService;
            this.dataStore = dataStore;
            this.eventBus = eventBus;
            eventBus.OnNotify += EventBus_OnNotify;
            this.messageBox = messageBox;
        }

        private async void EventBus_OnNotify(string arg1, object arg2)
        {
            if (arg1== "factory.roduce.view.update")
            {
               await  Reload();
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dtEnd.DateTime>=dtStart.DateTime)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }
        private async Task Reload()
        {
            await this.Dispatcher.InvokeAsync(async () =>
             {
                 try
                 {
                     dxg.ShowLoadingPanel = true;
                     var dataTable = await dataStore.Get(dtStart.DateTime, dtEnd.DateTime);
                     dxg.ItemsSource = dataTable;
                 }
                 catch (Exception err)
                 {
                     exportBoxService.Log(err.ToString());
                     messageBox.Show(err.Message);
                 }
                 finally
                 {
                     dxg.ShowLoadingPanel = false;
                 }

             });

        }
        private async void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           await  Reload();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "EXCEL文件|*.xlsx";
            saveFileDialog.FileName = $"生产日志_{DateTime.Now.ToString("yyyyMMdd")}.xlsx";
            if (saveFileDialog.ShowDialog()==true)
            {
                tv.ExportToXlsx(saveFileDialog.FileName);
            }
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dxg?.SelectedItem!=null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private async void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            if (dxg.SelectedItem is DataRowView data)
            {
                var deviceId = data.Row.Field<string>("设备ID");

                var gg = messageBox.Show("确定要删除设备记录吗？","提示",MessageBoxButton.YesNo);
                if (gg==MessageBoxResult.Yes)
                {
                    await dataStore.Delete(deviceId);
                    eventBus.Notify("factory.roduce.view.update");
                }
            }
        }
    }

}
