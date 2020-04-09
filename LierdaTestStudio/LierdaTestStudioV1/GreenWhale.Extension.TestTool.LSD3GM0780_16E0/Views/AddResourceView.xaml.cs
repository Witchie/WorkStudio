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
using DevExpress.Xpf.Core;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using Microsoft.Extensions.DependencyInjection;
namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Views
{
    /// <summary>
    /// AddResourceView.xaml 的交互逻辑
    /// </summary>
    public partial class AddResourceView : UserControl
    {
        private readonly ResourceStore resourceDictoaries;
        public AddResourceView(ResourceStore resourceDictoaries)
        {
            InitializeComponent();
            this.DataContext = AddResourceViewModel;
            this.resourceDictoaries= resourceDictoaries;
        }
        public AddResourceViewModel AddResourceViewModel { get; private set; } = new AddResourceViewModel();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.GetWindow<ThemedWindow>().Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var success = n2.IsChecked ?? false;
            var failed = n1.IsChecked ?? false;
            if (success)
            {
                AddResourceViewModel.Success = new ValueModel() {Number= AddResourceViewModel.ID,Caption=AddResourceViewModel.Name+":正常",KeyName=$"{ResourceState.S}-"+AddResourceViewModel.ID.ToString().PadLeft(2,'0'),ResourceState=ResourceState.S ,Value= AddResourceViewModel.SuccessValue };
            }
            else
            {
                AddResourceViewModel.Success = new ValueModel() { Number = AddResourceViewModel.ID, Caption = AddResourceViewModel.Name + ":正常", KeyName = $"{ResourceState.S}-" + AddResourceViewModel.ID.ToString().PadLeft(2, '0'), ResourceState = ResourceState.S};

            }
            if (failed)
            {
                AddResourceViewModel.Failed = new ValueModel() { Number = AddResourceViewModel.ID, Caption = AddResourceViewModel.Name + ":异常", KeyName = $"{ResourceState.E}-" + AddResourceViewModel.ID.ToString().PadLeft(2, '0'), ResourceState = ResourceState.E, Value = AddResourceViewModel.SuccessValue };
            }
            else
            {
                AddResourceViewModel.Failed = new ValueModel() { Number = AddResourceViewModel.ID, Caption = AddResourceViewModel.Name + ":异常", KeyName = $"{ResourceState.E}-" + AddResourceViewModel.ID.ToString().PadLeft(2, '0'), ResourceState = ResourceState.E };

            }
            resourceDictoaries.Add(AddResourceViewModel);
            this.GetWindow<ThemedWindow>().Close();
        }

    }
}
