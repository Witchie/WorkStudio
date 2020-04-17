namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 环境信息
    /// </summary>
    public class EnviromentInfo
    {
        /// <summary>
        /// 硬件信息
        /// </summary>
        public Hardware Hardware { get; set; } = new Hardware();
        /// <summary>
        /// 软件信息
        /// </summary>
        public Software Software { get; set; } = new Software();
    }
}
