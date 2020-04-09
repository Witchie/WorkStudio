namespace GreenWhale.RunTime.Scripts
{
    /// <summary>
    /// 执行状态
    /// </summary>
    public enum State
    {
        /// <summary>
        /// 合格
        /// </summary>
        Qualified,
        /// <summary>
        /// 不合格
        /// </summary>
        Unqualified,
        /// <summary>
        /// 未知状态
        /// </summary>
        None,
        /// <summary>
        /// 数据包格式有效
        /// </summary>
        FrameValidatePassed,
        /// <summary>
        /// 数据包格式无效
        /// </summary>
        FrameValidateFailed,
    }
}
