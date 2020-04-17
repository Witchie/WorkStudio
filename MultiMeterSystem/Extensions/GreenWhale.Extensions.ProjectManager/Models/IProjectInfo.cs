namespace GreenWhale.ProjectManager
{
    /// <summary>
    /// 项目信息
    /// </summary>
    public interface IProjectInfo
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        string ProjectName { get; set; }
        /// <summary>
        /// 项目型号
        /// </summary>
        string ProjectModel { get; set; }
    }
}
