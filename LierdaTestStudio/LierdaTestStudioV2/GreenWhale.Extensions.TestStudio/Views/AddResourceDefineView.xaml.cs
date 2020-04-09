using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DevExpress.CodeParser;
using DevExpress.Xpf.Core;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Services;
using GreenWhale.BootLoader.Implements;
using GreenWhale.Extensions.TestTools2.Extensions;
using GreenWhale.Extensions.TestTools2.Models;
using GreenWhale.Extensions.TestTools2.ViewModels;
using GreenWhale.Extensions.Views;
using GreenWhale.RunTime.Scripts;
using Microsoft.Win32;
using System.Linq;
using GreenWhale.Extensions.TestStudio;

namespace GreenWhale.Extensions.TestTools2.Views
{
    /// <summary>
    /// AddResourceDefineView.xaml 的交互逻辑
    /// </summary>
    public partial class AddResourceDefineView : UserControl
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ResourceDefineView resourceDefineViewModels;
        public AddResourceDefineView(IServiceProvider serviceProvider, ResourceDefineView resourceDefineViewModels)
        {
            InitializeComponent();
            r1.ReplaceService<ISyntaxHighlightService>(new CSharpSyntaxHighlightService(r1));
            this.serviceProvider = serviceProvider;
            this.resourceDefineViewModels = resourceDefineViewModels;
            r1.TextChanged += R1_TextChanged;
        }

        private void R1_TextChanged(object sender, EventArgs e)
        {
            ViewModel.ScriptModel.IsExecute = false;
        }

        private void LoadFromFile_Success_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !ViewModel.ScriptModel.IsExecute;
        }

        private void ExecuteTest_Success_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (!ViewModel.ScriptModel.IsExecute);
        }


        private  void LoadFromFile_Success_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            var currentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Examples");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "C#脚本文件|*.cs";
            openFileDialog.InitialDirectory = currentPath;
            if (openFileDialog.ShowDialog()==true)
            {
                try
                {
                    r1.LoadDocument(openFileDialog.FileName);
                }
                catch (Exception exception)
                {
                    DXMessageBox.Show(exception.Message);
                }
            }
        }
        private async void TestLoad(ModelBase modelBase,string text,string receiveContent)
        {
            if (string.IsNullOrEmpty(text))
            {
                DXMessageBox.Show(Resource.script_file_is_null_can_not_be_test);
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(receiveContent))
                {
                    DXMessageBox.Show(Resource.input_parameter_is_invalide__please_define_respond_data);
                    return;
                }
                modelBase.IsExecute = true;
                modelBase.LoadText(text);
                await  modelBase.LoadCode(serviceProvider);
                if (modelBase.RuningCore != null)
                {
                    var hex = receiveContent.ToHex();
                    var script = modelBase.RuningCore.Run(InvokeContext.Run(serviceProvider, hex));
                    var test = modelBase.RuningCore.Run(InvokeContext.Run(serviceProvider, hex, new Dictionary<string, string> { { InvokeContext.FrameValudate, InvokeContext.True } }));
                    if (script != null&&script.IsError==false&&test.IsError==false&&test.Result.State==State.FrameValidatePassed)
                    {
                        DXMessageBox.Show(script.Result.ToString(), Resource.test_pass);
                    }
                    else
                    {
                        DXMessageBox.Show(script.ErrorMessage.Message,Resource.test_failed);
                        var s= serviceProvider.GetService<IExportBoxService>();
                        s.Log(s.ToString());
                    }
                }
                else
                {
                    DXMessageBox.Show(Resource.load_script_file_is_wrong_or_can_not_be_anysis_to_script_);
                }

            }
            catch (Exception err)
            {
                DXMessageBox.Show(err.Message);

            }
            finally
            {
                modelBase.IsExecute = false;

            }

        }
        private  void ExecuteTest_Success_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            TestLoad(ViewModel.ScriptModel, r1.Text,ViewModel.ReceiveContent);
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ViewModel.IsManulCheck)
            {
                if (!string.IsNullOrEmpty(ViewModel.Description))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ViewModel.Description) &&
                ViewModel.ScriptModel.RuningCore != null && !
                string.IsNullOrEmpty(ViewModel.ReceiveContent) && !
                string.IsNullOrEmpty(ViewModel.SendContent))
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }

        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!ViewModel.ScriptModel.IsExecute)
            {
                if (isUpadate)
                {
                    ViewModel.ScriptModel.LoadText(r1.Text);
                    this.GetWindow<ThemedWindow>().Close();
                }
                else
                {
                AGAGIN:
                    var data = ViewModel.Clone();
                    var check = resourceDefineViewModels.ViewModel.ResourceDefineViewModels.Where(p => p.TestIndex == data.TestIndex).FirstOrDefault();
                    if (check != null)
                    {
                        if (DXMessageBox.Show("指定的测试序号已经存在，是否序号自动增加？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            ViewModel.TestIndex++;
                            goto AGAGIN;
                        }
                        else
                        {
                            return;
                        }
                    }
                    resourceDefineViewModels.ViewModel.ResourceDefineViewModels.Add(data);
                }

            }
            else
            {
                DXMessageBox.Show(Resource.please_run_test_before_saving);
            }
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            var check= resourceDefineViewModels?.ViewModel?.ResourceDefineViewModels?.LastOrDefault();
            if (check!=null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }
        /// <summary>
        /// 装载数据上下文
        /// </summary>
        /// <param name="addResourceDefineViewModel"></param>
        public void LoadForUpdate(AddResourceDefineViewModel addResourceDefineViewModel )
        {
            ViewModel = addResourceDefineViewModel;
            this.DataContext = ViewModel;
            isUpadate = true;
            r1.Text = addResourceDefineViewModel.ScriptModel.FileContent;
        }
        private bool isUpadate = false;
        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            var check = resourceDefineViewModels.ViewModel.ResourceDefineViewModels.LastOrDefault();
            var ddd= check.Clone();
            this.ViewModel.TestIndex = ddd.TestIndex + 1;
            this.ViewModel.Description = string.Empty;
            this.ViewModel.SendContent = string.Empty;
            this.ViewModel.ReceiveContent = string.Empty;
            this.ViewModel.ScriptModel = ddd.ScriptModel;
            this.ViewModel.IsManulCheck = ddd.IsManulCheck;
            this.ViewModel.TaskDeplay = ddd.TaskDeplay;
            this.ViewModel.IsAbnormalWork = ddd.IsAbnormalWork;
            this.ViewModel.AbnormalWorkTips = ddd.AbnormalWorkTips;
            this.r1.Text = ddd.ScriptModel.FileContent;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.Content = "手动判定";
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.Content = "自动判定";
            }
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.Content = "阻断任务";
            }
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.Content = "普通任务";
            }
        }
    }
}
