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
using GreenWhale.ProjectManager;
using Microsoft.Extensions.DependencyInjection;
namespace GreenWhale.BootLoader.Implements.ProjectManager.Views
{
    /// <summary>
    /// ProjectItemEditorView.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectItemEditorView : UserControl
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ProjectItemEditorViewService projectItemEditorViewService;
        private readonly ProjectManagerDataSource projectManagerDataSource;
        public ProjectItemEditorView(IServiceProvider serviceProvider, ProjectListEditorViewService projectListEditorViewService, ProjectManagerDataSource projectManagerDataSource)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            projectItemEditorViewService = new ProjectItemEditorViewService(this, projectListEditorViewService);
            this.projectManagerDataSource = projectManagerDataSource;
        }
        /// <summary>
        /// 加载数据上下文
        /// </summary>
        /// <param name="projectInfo"></param>
        public void LoadDataContext(IProjectInfo projectInfo)
        {
           this.projectInfo.DataContext = projectInfo;
        }
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.projectInfo.DataContext is IProjectInfo projectInfo)
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
            if (this.projectInfo.DataContext is IProjectInfo projectInfo)
            {
                dxg.GridLoadData(s =>
                {
                    s.ItemsSource = projectManagerDataSource.ProjectSegments(projectInfo.ProjectModel);
                },()=>
                { 
                    sender.Disable();
                },()=>
                {
                    sender.Enable(); 
                });
            }
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dxg!=null&&dxg.ItemsSource!=null)
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
            tv.Export();
        }
    }
}
