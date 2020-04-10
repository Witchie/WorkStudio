namespace GreenWhale.BootLoader
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// 当前应用程序目录
        /// </summary>
        public string BaseDirectory { get; set; }
        /// <summary>
        /// 是否为线程互斥应用
        /// </summary>
        public bool IsMutexApplication { get; set; }

    }
}
