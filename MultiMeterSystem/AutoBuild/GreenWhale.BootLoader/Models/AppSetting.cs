namespace GreenWhale.BootLoader
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public class AppSetting
    {
        public AppSetting()
        {
        }
        /// <summary>
        /// 应用程序配置
        /// </summary>
        /// <param name="baseDirectory">根目录配置</param>
        /// <param name="isMutexApplication"> 是否为进程互斥应用</param>
        public AppSetting(string baseDirectory, bool isMutexApplication)
        {
            BaseDirectory = baseDirectory;
            IsMutexApplication = isMutexApplication;
        }

        /// <summary>
        /// 当前应用程序目录
        /// </summary>
        public string BaseDirectory { get; set; }
        /// <summary>
        /// 是否为进程互斥应用
        /// </summary>
        public bool IsMutexApplication { get; set; }

    }
}
