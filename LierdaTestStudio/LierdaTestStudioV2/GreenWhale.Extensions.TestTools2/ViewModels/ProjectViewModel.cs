using DevExpress.Mvvm;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.ObjectModel;

namespace GreenWhale.Extensions.TestTools2.ViewModels
{
    /// <summary>
    /// 项目视图模型
    /// </summary>
    [ExcelExporter(Name = "协议详细信息")]
    public class ProjectViewModel : ViewModelBase
    {
        private string projectName;
        private string version;
        private DateTime dateTime;
        private string author;
        private string scriptType = "C#";

        /// <summary>
        /// 项目名称
        /// </summary>
        [ExporterHeader("项目名称")]
        public string ProjectName
        {
            get => projectName; set
            {
                projectName = value;RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 版本
        /// </summary>
        [ExporterHeader("协议版本")]
        public string Version
        {
            get => version; set
            {
                version = value; RaisePropertyChanged();
            }
        }
        /// <summary>
        ///时间
        /// </summary>
        [ExporterHeader("日期", Format = "yyyy-MM-dd")]
        public DateTime DateTime
        {
            get => dateTime; set
            {
                dateTime = value; RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 作者
        /// </summary>
        [ExporterHeader("作者")]
        public string Author
        {
            get => author; set
            {
                author = value; RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 脚本类型
        /// </summary>
        [ExporterHeader("脚本类型")]
        public string ScriptType
        {
            get => scriptType; set
            {
                scriptType = value; RaisePropertyChanged();
            }
        }         /// <summary>
                  /// 资源列表
                  /// </summary>
        public ObservableCollection<AddResourceDefineViewModel> ResourceDefineViewModels { get; private set; } = new ObservableCollection<AddResourceDefineViewModel>();

    }

}
