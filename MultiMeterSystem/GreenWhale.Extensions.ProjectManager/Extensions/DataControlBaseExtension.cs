using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GreenWhale.BootLoader.Implements.ProjectManager
{
    public class FilterItem
    {
        public FilterItem()
        {
        }

        public FilterItem(string name, string extension)
        {
            Name = name;
            Extension = extension;
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 扩展名(*内部自动填充)
        /// </summary>
        public string Extension { get; set; }
        public override string ToString()
        {
            if (Extension!=null&&Extension.Contains("*"))
            {
                return $"{Name}|{Extension}";
            }
            return $"{Name}|*{Extension}";
        }
    }
    /// <summary>
    /// 扩展名称
    /// </summary>
    public class FilterItems : List<FilterItem>
    {
        public override string ToString()
        {
            List<string> vs = new List<string>();
            foreach (var item in this)
            {
                vs.Add(item.ToString());
            }
            return string.Join("|", vs);
        }
    }
    /// <summary>
    /// 内部扩展
    /// </summary>
    public static class DataControlBaseExtension
    {
        /// <summary>
        /// 导出为Word格式
        /// </summary>
        /// <param name="filterItems"></param>
        /// <returns></returns>
        public static FilterItems AddWord(this FilterItems filterItems)
        {
            filterItems.Add(new FilterItem("Word文档", ".docx"));
            return filterItems;
        }
        /// <summary>
        /// 添加Excel格式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static FilterItems AddExcel(this FilterItems item)
        {
            item.Add(new FilterItem("Excel2007格式", ".xlsx"));
            return item;
        }
        /// <summary>
        /// 添加PDF格式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static FilterItems AddPdf(this FilterItems item)
        {
            item.Add(new FilterItem("Pdf格式", ".pdf"));
            return item;
        }
        public static void Export<TView>(this TView view, string intiDirectory = null) where TView : DataViewBase
        {
            var item = new FilterItems();
            item.AddExcel().AddWord().AddPdf();
            view.Export(item);
        }
        public static void Export<TView>(this TView view, FilterItems filterItems,string intiDirectory=null) where TView: DataViewBase
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filterItems.ToString();
            if (intiDirectory!=null)
            {
                saveFileDialog.FileName = intiDirectory;
            }
            if (saveFileDialog.ShowDialog()==true)
            {
                var ext = saveFileDialog.FileName.Substring(saveFileDialog.FileName.IndexOf(".")+1);
                switch (ext)
                {
                    case "xps":
                        { 
                            view.ExportToXps(saveFileDialog.FileName); 
                        }
                        break;
                    case "xlsx":
                        {
                            view.ExportToXlsx(saveFileDialog.FileName);
                        }break;
                    case "xls":
                        {
                            view.ExportToXls(saveFileDialog.FileName);
                        }break;
                    case "txt":
                        {
                            view.ExportToText(saveFileDialog.FileName);
                        }
                        break;
                    case "pdf":
                        {
                            view.ExportToPdf(saveFileDialog.FileName);
                        }
                        break;
                    case "rtf":
                        {
                            view.ExportToRtf(saveFileDialog.FileName);
                        }break;
                    case "mht":
                        {
                            view.ExportToMht(saveFileDialog.FileName);
                        }break;
                    case "png":
                        {
                            view.ExportToImage(saveFileDialog.FileName);
                        }break;
                    case "html":
                        {
                            view.ExportToHtml(saveFileDialog.FileName);
                        }break;
                    case "docx":
                        {
                            view.ExportToDocx(saveFileDialog.FileName);
                        }break;
                    case "csv":
                        {
                            view.ExportToCsv(saveFileDialog.FileName);
                        }break;
                    default:MessageBox.Show("该格式暂不支持");break;
                }
            }
        }
        /// <summary>
        /// 加载数据(自动显示Ui加载动画)
        /// </summary>
        /// <typeparam name="TGrid"></typeparam>
        /// <param name="grid"></param>
        /// <param name="onLoad"></param>
        /// <returns></returns>
        public static TGrid GridLoadData<TGrid>(this TGrid grid,Action<TGrid> onLoad,Action onBusy=null,Action onNormal=null, Action<Exception> onException = null) where TGrid: DataControlBase
        {
            try
            {
                onBusy?.Invoke();
                grid.ShowLoadingPanel = true;
                onLoad?.Invoke(grid);
                return grid;
            }
            catch (Exception exception)
            {
                onException?.Invoke(exception);
                return grid;
            }
            finally
            {
                onNormal?.Invoke();
                grid.ShowLoadingPanel = false;
            }
        }
        /// <summary>
        /// 装载数据
        /// </summary>
        /// <typeparam name="TGrid"></typeparam>
        /// <param name="grid"></param>
        /// <param name="onLoad"></param>
        /// <param name="onBusy"></param>
        /// <param name="onNormal"></param>
        /// <returns></returns>
        public static TGrid ListLoadData<TGrid>(this TGrid grid, Action<TGrid> onLoad, Action onBusy = null, Action onNormal = null,Action<Exception> onException=null) where TGrid: ListBoxEdit
        {
            try
            {
                onBusy?.Invoke();
                grid.ShowWaitIndicator = true;
                onLoad?.Invoke(grid);
                return grid;
            }
            catch (Exception exception)
            {
                onException?.Invoke(exception);
                return grid;
            }
            finally
            {
                onNormal?.Invoke();
                grid.ShowWaitIndicator = false;
            }
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Enable<T>(this object data) where T : UIElement
        {
           return  data.Set<T>(true);
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="data"></param>
        public static void Enable(this object data)
        {
            data.Enable<UIElement>();
        }
        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="data"></param>
        public static void Disable(this object data)
        {
            data.Disable<UIElement>();
        }
        /// <summary>
        /// 禁用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Disable<T>(this object data) where T : UIElement
        {
            return data.Set<T>(false);
        }
        /// <summary>
        /// 设置元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public static T Set<T>(this object data,bool isEnable) where T:UIElement
        {
            var ui= data.Get<T>();
            if (ui!=null)
            {
                ui.IsEnabled = isEnable;
            }
            return ui;
        }
        /// <summary>
        /// 获取真实类型
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        public static TElement Get<TElement>(this object uiElement)
        {
            return (TElement)uiElement;
        }
    }
}
