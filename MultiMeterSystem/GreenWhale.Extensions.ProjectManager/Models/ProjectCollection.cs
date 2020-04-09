using System.Collections.Generic;

namespace GreenWhale.ProjectManager
{
    /// <summary>
    /// 项目集合
    /// </summary>
    public class ProjectCollection:DataCore<int>, IProjectInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目型号
        /// </summary>
        public string ProjectModel { get; set; }
        /// <summary>
        /// 子项目列表
        /// </summary>
        public List<ProjectItem> ProjectItems { get; set; }
    }
}
