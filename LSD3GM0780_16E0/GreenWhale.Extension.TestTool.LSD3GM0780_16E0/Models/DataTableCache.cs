using System.Collections.Generic;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using System.Data;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Views;
using DevExpress.Xpf.Core;
using System;
using System.Windows.Threading;
using System.Windows;
using System.Threading.Tasks;
using GreenWhale.BootLoader.Implements;
using GreenWhale.BootLoader.Services;
namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0
{
    public class DataTableCache : ViewModelBase
    {
        private readonly ResourceStore resourceStore;
        public DataTable DataTables { get; private set; }= new DataTable();
        private DataTable dataTable;
        private readonly IServiceProvider autoConfirmView;
        private readonly Dispatcher Dispatcher;
        private readonly IExportBoxService exportBoxService;
        private readonly ILSD3GMDataContext dataContext;

        public DataTableCache(ResourceStore resourceStore, IServiceProvider autoConfirmView, Dispatcher dispatcher,IExportBoxService exportBoxService, ILSD3GMDataContext dataContext)
        {
            this.resourceStore = resourceStore;
            this.autoConfirmView = autoConfirmView;
            Dispatcher = dispatcher;
            this.exportBoxService = exportBoxService;
            this.dataContext = dataContext;
        }
        /// <summary>
        /// 当前缓冲的数据表
        /// </summary>
        public DataTable CacheDataTable
        {
            get => dataTable; private set
            {
                dataTable = value;
                RaisePropertyChanged();
            }
        }
        private DataTable GenerateColumns(DataTable dataTable)
        {
            if (dataTable.Columns.Count == 0)
            {
                var dt = CacheDataTable.Rotate(1);
                dataTable.Columns.AddRange(dt);
            }
            return dataTable;
        }
        private readonly object _locker = new object();
        /// <summary>
        /// 将测试记录保存起来
        /// </summary>
        public void SaveDataTable()
        {
            try
            {
                lock (_locker)
                {
                    var res = CacheDataTable.RoateValue();
                    var view1 = autoConfirmView.GetService<ProductProcessView>();
                    view1.ReloadData();
                    var meterid=   dataContext.SaveTestCategory(CacheDataTable);
                    ClearOld(meterid);
                    GenerateColumns(DataTables).Rows.Add(res);
                    view1.Visible(false);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
 
        }
        /// <summary>
        /// 设置普通模式
        /// </summary>
        /// <param name="normalModel"></param>
        public void SetNormalMode(NormalModel normalModel)
        {
            this.Dispatcher.Invoke(() => {
                var id = normalModel.Number;
                var row = FindRow(id);
                if (row != null)
                {
                    row[2] = normalModel.ResourceState;
                }
                ErrorStop(normalModel);
                var view1 = autoConfirmView.GetService<ProductProcessView>();
                view1.Press = false;
            });

        }
        private void ErrorStop(ModelBase modelBase)
        {

            if (modelBase.ResourceState == ResourceState.E)
            {
                Dispatcher.Invoke(() =>
                {
                    SaveDataTable();
                    var view1 = autoConfirmView.GetService<ProductProcessView>();
                    view1.Press = false;
                    exportBoxService.Log($"发现硬件错误测试被迫停止");

                });
               // NewDataTable(string.Empty);
            }
        }
        private DataRow FindRow(int id)
        {
            if (CacheDataTable==null|| CacheDataTable.Rows==null)
            {
                return null;
            }
            foreach (DataRow item in CacheDataTable.Rows)
            {
                if (item[0].ToString()==id.ToString())
                {
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// 设置值模式
        /// </summary>
        /// <param name="normalModel"></param>
        public void SetValueMode(ValueModel normalModel)
        {
            var id = normalModel.Number;
            var row = FindRow(id);
            if (row != null)
            {
                row[2] = normalModel.ResourceState;
                row[3] = normalModel.Value.Value;
            }
            ErrorStop(normalModel);
            var view1 = autoConfirmView.GetService<ProductProcessView>();
            view1.Press = false;
        }
        /// <summary>
        /// 设置表号响应成功
        /// </summary>
        public  void SetStartSuccess()
        {
            var row = FindRow(0);
            if (row != null)
            {
                row[2] = ResourceState.S;
            }
            Task.Run(async () =>
            {
                await Task.Delay(6000);

                var view1 = autoConfirmView.GetService<ProductProcessView>();

                view1.Press = true;
            });

        }
        /// <summary>
        /// 设置测试成功
        /// </summary>
        public void SetSuccess()
        {
            Dispatcher.Invoke(() =>
            {
                var view1 = autoConfirmView.GetService<ProductProcessView>();
                view1.Visible(false);
                var view = autoConfirmView.GetService<AutoConfirmView>();
                view.LoadDataTable(CacheDataTable,()=> 
                {
                    SaveDataTable();
                });
                view.SetWindow<ThemedWindow>().SetSizeToContent(SizeToContent.WidthAndHeight).ResizeMode(ResizeMode.NoResize).WindowStyle(WindowStyle.ToolWindow).SetStartupLocation(WindowStartupLocation.CenterScreen).ShowDialog();
            });
        }
        private void ClearOld(string meterid)
        {
            foreach (DataRow item in DataTables.Rows)
            {
                var id = item["表号"]?.ToString();
                if (id == meterid)
                {
                    DataTables.Rows.Remove(item);
                    exportBoxService.Log($"重复测试:{meterid},原数据已被删除");
                    var view1 = autoConfirmView.GetService<ProductProcessView>();
                    view1.ReloadData();
                    if (dataContext.IsExist(meterid))
                    {
                        dataContext.DeleteMeterLog(meterid);
                        exportBoxService.Log($"重复测试:{meterid},原数据已被删除");
                    }
                    break;
                }
            }
        }
        /// <summary>
        /// 创建一个新的表
        /// </summary>
        public void NewDataTable(string meterid)
        {
            ClearOld(meterid);
            var data = resourceStore.DataTable(s =>
            {
                var row = s.NewRow();
                row[0] = "0";
                row[1] = "表号";
                row[3] = meterid;
                row[2] = ResourceState.N;
                s.Rows.Add(row);
            });
            CacheDataTable = data;
        }
    }
}
