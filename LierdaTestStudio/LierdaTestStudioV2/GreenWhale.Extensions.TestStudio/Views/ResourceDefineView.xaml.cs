using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using DevExpress.Xpf.Core;
using GreenWhale.Extensions.TestTools2.Extensions;
using System.Windows;
using GreenWhale.Extensions.Views;
using DevExpress.Xpf.Editors.Flyout;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GreenWhale.Extensions.TestTools2.ViewModels;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.IO;
using GreenWhale.BootLoader.Implements;
using System.Xml;
using System.Linq;
using DevExpress.Mvvm;
using Magicodes.ExporterAndImporter;
using Magicodes;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using OfficeOpenXml;
using Shouldly;
using GreenWhale.Extensions.TestTools2.Models;
using GreenWhale.Extensions.TestStudio;
using GreenWhale.RunTime.Scripts;

namespace GreenWhale.Extensions.TestTools2.Views
{

    /// <summary>
    /// ResourceDefineView.xaml 的交互逻辑
    /// </summary>
    public partial class ResourceDefineView : UserControl
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IProjectViewServcie projectViewServcie;
        public ResourceDefineView(IServiceProvider serviceProvider, IProjectViewServcie projectViewServcie)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.DataContext = ViewModel;
            this.projectViewServcie = projectViewServcie;
        }
        /// <summary>
        /// 资源列表
        /// </summary>
        public ProjectViewModel ViewModel { get; private set; }= new ProjectViewModel();
        private  void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddResourceDefineView view = new AddResourceDefineView(serviceProvider, this);
            var window = view.SetWindow<ThemedWindow>().Center().Small();
            window.AutoToggle(sender).ShowDialog();
        }


        private async void Button_Export_Click(object sender, RoutedEventArgs e)
        {
            await  projectViewServcie.WriteModelDialog(ViewModel);
        }

        private async void Button_Import_Click(object sender, RoutedEventArgs e)
        {
            await projectViewServcie.ReadModelDialog(s=>
            {
                ViewModel = s;
                this.DataContext = ViewModel;
            });
        }

        private async void Button_ExportFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel格式协议文档|*.xlsx";
            saveFileDialog.FileName = $"{ViewModel.ProjectName}_{ViewModel.Version}_{ViewModel.Author}_{ViewModel.DateTime.ToLongDateString()}.xlsx";
            if (saveFileDialog.ShowDialog()==true)
            {
                var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "ProcotalTemplate.xlsx");
                if (File.Exists(templatePath))
                {
                    IExportFileByTemplate exporter = new ExcelExporter();
                    await  exporter.ExportByTemplate(saveFileDialog.FileName,this.ViewModel,templatePath);
                    DXMessageBox.Show("导出成功");
                }
                else
                {
                    DXMessageBox.Show("导出模板不存在，请联系管理员");
                }
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgv?.SelectedItem is AddResourceDefineViewModel defineViewModel)
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
            if (DXMessageBox.Show(Resource.Are_You_Sure_Delete, Resource.Tips, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (AddResourceDefineViewModel item in dgv.SelectedItems)
                {
                    ViewModel.ResourceDefineViewModels.Remove(item);
                }
            }
        }

        private void CommandBinding_Uopdate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgv?.SelectedItems?.Count==1)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            AddResourceDefineView view = new AddResourceDefineView(serviceProvider, this);
            view.LoadForUpdate(dgv?.SelectedItem as AddResourceDefineViewModel);
            var window = view.SetWindow<ThemedWindow>().Center().Small();
            window.AutoToggle(sender).ShowDialog();
        }
    }
}
