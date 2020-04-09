using System.Collections.Generic;
using System.Linq;
namespace GreenWhale.ProjectManager
{
    /// <summary>
    /// 项目进度扩展
    /// </summary>
    public static class ProjectItemExtension
    {
        /// <summary>
        /// 进度信息
        /// </summary>
        /// <param name="projectItem"></param>
        /// <returns></returns>
        public static IReadOnlyCollection<ProcessInformation> ProcessInformations(this ProjectItem projectItem)
        {
           var list=  projectItem.ProjectSegments.OrderBy(p=>p.Order).Select(p => p.ProcessInformation).SelectMany(p => p).ToList();
           return list;
        }
    }
}
