namespace GreenWhale.ProjectManager
{
    /// <summary>
    /// 进度状态
    /// </summary>
    public enum ProcessState
    {
        /// <summary>
        /// 等待
        /// </summary>
        Wating,
        /// <summary>
        /// 执行中
        /// </summary>
        Executing,
        /// <summary>
        /// 完成中
        /// </summary>
        Complete,
        /// <summary>
        /// 取消
        /// </summary>
        Cancel
    }
}
