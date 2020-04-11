using System;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 应用程序信息扩展
    /// </summary>
    public static class ApplicationInfoExtension
    {
        /// <summary>
        /// 设置应用程序名称
        /// </summary>
        /// <param name="info"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IApplicationInfo SetName(this IApplicationInfo info,string name)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            info.SetApplicationName(name);
            return info;
        }
        /// <summary>
        /// 设置应用程序宽度
        /// </summary>
        /// <param name="info"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static IApplicationInfo SetWidth(this IApplicationInfo info, double width)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            info.SetMainWindowWidth(width);
            return info;
        }
        /// <summary>
        /// 设置应用程序高度
        /// </summary>
        /// <param name="info"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static IApplicationInfo SetHeight(this IApplicationInfo info, double height)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            info.SetMainWindowHeight(height);
            return info;
        }
    }
}
