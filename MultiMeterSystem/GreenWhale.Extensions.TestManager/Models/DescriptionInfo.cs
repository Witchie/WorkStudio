namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 描述信息
    /// </summary>
    public class DescriptionInfo
    {
        /// <summary>
        /// 目的
        /// </summary>
        public Purpose Purpose { get; set; } = new Purpose();
        /// <summary>
        /// 术语
        /// </summary>
        public Terms Terms { get; set; } = new Terms();
        /// <summary>
        /// 参考资料
        /// </summary>
        public References References { get; set; } = new References();
    }
}
