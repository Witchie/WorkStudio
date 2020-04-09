using GreenWhale.ProjectManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace GreenWhale.BootLoader.Implements.ProjectManager.Views
{
    /// <summary>
    /// 项目管理数据源
    /// </summary>
    public class ProjectManagerDataSource
    {
        public ProjectManagerDataSource()
        {
            var item = new ProjectItem() { Order = 0, ProjectModel = "LSD3GM0288-21E5", ProjectName = "光电直读计数器检测板" };
            var seg = new ProjectSegment { SegmentName = "需求沟通（市场/客户）", Order = 0, Owner = "谢开勇", DateTimeDesireStart = DateTime.Parse("2019-4-17"), DateTimeDesireEnd = DateTime.Parse("2019-4-28"), DateTimeActualStart = DateTime.Parse("2019-4-17"), DateTimeActualEnd = DateTime.Parse("2019-4-28") };
            seg.ProcessInformation.Add(new ProcessInformation("2019/5/5", "下发打样订单"));
            seg.ProcessInformation.Add(new ProcessInformation("2019/5/21", "备料完成，客供料预计本周五到，最快周五上机，PM要求加快送样进度"));
            seg.ProcessInformation.Add(new ProcessInformation("2019/5/27", "客供料的SIM卡还未寄到，未贴片"));
            seg.ProcessInformation.Add(new ProcessInformation("2019/5/31", "打样完成"));
            item.ProjectSegments.Add(seg);
            DataSources.Add(item);
            DataSources.Add(new ProjectItem() { Order = 0, ProjectModel = "LSD3GM0288-21E4", ProjectName = "光电直读计数器检测板" });
            DataSources.Add(new ProjectItem() { Order = 0, ProjectModel = "LSD3GM0288-21E2", ProjectName = "光电直读计数器检测板" });

        }
        ObservableCollection<ProjectItem> DataSources = new ObservableCollection<ProjectItem>();
        /// <summary>
        /// 项目信息
        /// </summary>
        public IReadOnlyCollection<IProjectInfo> ProjectInfos(string queryParameter=null)
        {
            if (queryParameter==null)
            {
                return DataSources;
            }
            var source=  DataSources.Where(p => p.ProjectName.Contains(queryParameter) || p.ProjectModel.Contains(queryParameter)).ToArray();
            return source;
        }
        /// <summary>
        /// 根据项目型号查询
        /// </summary>
        /// <param name="projectModel"></param>
        /// <returns></returns>
        public IReadOnlyCollection<ProjectSegment> ProjectSegments(string projectModel)
        {
          var item=   DataSources.Where(p => p.ProjectModel == projectModel).FirstOrDefault();
            if (item!=null)
            {
                return  item.ProjectSegments;
            }
            else
            {
                return null;
            }
        }
    }
}
