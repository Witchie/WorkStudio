using DevExpress.Xpf.Grid;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// TestResultView.xaml 的交互逻辑
    /// </summary>
    public partial class TestResultView : UserControl
    {
        private readonly ILSD3GMDataContext dataContext;
        public TestResultView()
        {
            InitializeComponent();
            this.dtStart.DateTime = DateTime.Now.Date.AddDays(-1);
            this.dtEnd.DateTime = DateTime.Now.Date.AddDays(1);
        }

        public TestResultView(ILSD3GMDataContext dataContext):this()
        {
            this.dataContext = dataContext;
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dtEnd!=null&& dtStart!=null&&dtEnd.DateTime>dtStart.DateTime)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private async void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try                                              
            {
                DataTable dataTable1 = new DataTable();
                dgv.ShowLoadingPanel = true;
                var list =await dataContext.GetTestCategorysAsync(dtStart.DateTime, dtEnd.DateTime);
                var table= list.ToDataTable();
                dgv.ItemsSource = table;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                dgv.ShowLoadingPanel = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "生产记录|*.xlsx";
            saveFileDialog.FileName = $"生产记录.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                tv.ExportToXlsx(saveFileDialog.FileName);
            }
        }
    }


}
