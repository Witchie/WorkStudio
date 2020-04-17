using System.Collections.Generic;
using System.Linq;
namespace GreenWhale.ProjectManager
{
    /// <summary>
    /// 单个项目
    /// </summary>
    public class ProjectItem:DataCore<int>, IProjectInfo
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目型号
        /// </summary>
        public string ProjectModel { get; set; }
        /// <summary>
        /// 项目片段列表
        /// </summary>
        public List<ProjectSegment> ProjectSegments { get; set; } = new List<ProjectSegment>();
        /// <summary>
        /// 项目状态
        /// </summary>
        public ProcessState ProcessState => ProjectSegments.Any(s => s.ProcessItemState == ProcessState.Wating || s.ProcessItemState == ProcessState.Executing || s.ProcessItemState == ProcessState.Wating) ? ProcessState.Executing : ProcessState.Complete;
    }
}
