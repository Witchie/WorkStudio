namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// 阶段
    /// </summary>
    public enum Stage
    {
        /// <summary>
        /// 在下载站点上可用，但尚不建议用于跨范围使用
        /// </summary>
        Released,
        /// <summary>
        /// 在下载站点提供，建议用于使用
        /// </summary>
        ReleasedAndBlessed,
        /// <summary>
        /// 在下载网页上还不可见，应由合作伙伴验证
        /// </summary>
        EarlyAccessPreview
    }
}
