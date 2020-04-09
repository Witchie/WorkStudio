using GreenWhale.BootLoader.Implements;
using GreenWhale.Extensions.TestStudio;
using GreenWhale.Extensions.TestTools2.Extensions;
using GreenWhale.Extensions.TestTools2.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GreenWhale.Extensions.TestTools2.Models
{
    /// <summary>
    /// 项目视图服务
    /// </summary>
    public class ProjectViewServcie : IProjectViewServcie
    {
        private readonly IExportBoxService exportBoxService;

        public ProjectViewServcie(IExportBoxService exportBoxService)
        {
            this.exportBoxService = exportBoxService;
        }
        /// <summary>
        /// 通过文件窗口打开视图模型
        /// </summary>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        public async Task ReadModelDialog(Action<ProjectViewModel> onComplete)
        {
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            saveFileDialog.Filter = Resource.protocal_filter;
            if (saveFileDialog.ShowDialog() == true)
            {
                var box = exportBoxService;
                box.Log(Resource.BeginImport);
                var models = await ReadModel(saveFileDialog.FileName);
                box.Log( Resource.AnalysisComplete);
                onComplete?.Invoke(models);
                box.Log($"{Resource.ImportSuccess}  {saveFileDialog.FileName}");
            }
        }
        /// <summary>
        /// 将视图模型保存到文件
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <returns></returns>
        public async Task WriteModelDialog(ProjectViewModel viewmodel)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = Resource.protocal_filter;
            saveFileDialog.FileName = $"{viewmodel.ProjectName}_{viewmodel.Version}_{viewmodel.Author}_{viewmodel.DateTime.ToLongDateString()}.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                var box = exportBoxService;
                box.Log(Resource.BeiginBuild);
                await WriteModel(viewmodel, saveFileDialog.FileName);
                box.Log( $"{Resource.ExportSuccess}  {saveFileDialog.FileName}");
            }
        }
        /// <summary>
        /// 装载视图模型
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<ProjectViewModel> ReadModel(string fileName)
        {
            var models = await Task.Run(() =>
            {
                var content = File.ReadAllText(fileName);
                var text = content.XmlDeserializer<ProjectViewModel>();
                return text;
            });
            return models;
        }
        /// <summary>
        /// 将数据模型写入文件
        /// </summary>
        /// <param name="projectViewModel"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task WriteModel(ProjectViewModel projectViewModel, string fileName)
        {
            await Task.Run(() =>
            {
                var xml = projectViewModel.XmlSerialize();
                File.WriteAllText(fileName, xml);
            });
        }
    }

}
