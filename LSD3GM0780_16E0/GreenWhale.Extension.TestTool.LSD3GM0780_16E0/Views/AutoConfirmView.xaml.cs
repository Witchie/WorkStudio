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
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using DevExpress.Xpf.Core;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Views
{
   /// <summary>
   /// 自动判定结果窗口
   /// </summary>
    public partial class AutoConfirmView : UserControl
    {
        public AutoConfirmView()
        {
            InitializeComponent();
        }
        private DataTable dataTable;
        private Action Save;
        public void LoadDataTable(DataTable cacheDataTable,Action onSave)
        {
            this.dataTable = cacheDataTable;
            //var names = cacheDataTable.ColumnNames().ToArray();
            //  grid.ColumnsSource = names;
            grid.ItemsSource = dataTable;
            this.Save = onSave;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem is DataRowView dataRow)
            {
                dataRow[2] = ResourceState.S;
            }
            grid.ItemsSource = dataTable;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem is DataRowView dataRow)
            {
                dataRow[2] = ResourceState.E;
            }
            grid.ItemsSource = dataTable;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (DataRow item in dataTable.Rows)
            {
                item[2] = ResourceState.S;
            }
            grid.ItemsSource = dataTable;
            Button_Click_3(sender,e);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (DataRow item in dataTable.Rows)
            {
                var res = item[2]?.ToString();
                if (string.IsNullOrEmpty(res))
                {
                    count++;
                }
                var state=(ResourceState)Enum.Parse(typeof(ResourceState), res);
                if (state == ResourceState.N)
                {
                    count++;
                }

            }
            if (count == 0)
            {
                Save?.Invoke();
                this.GetWindow<ThemedWindow>().Close();
            }
            else
            {
                MessageBox.Show("存在尚未判定的数据，请先判定");
            }
        }
    }
}
