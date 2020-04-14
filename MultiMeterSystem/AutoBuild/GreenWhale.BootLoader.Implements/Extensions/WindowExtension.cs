using System;
using System.Windows;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 应用程序信息扩展
    /// </summary>
    public static class WindowExtension
    {
        /// <summary>
        /// 设置应用程序名称
        /// </summary>
        /// <param name="info"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TWindow SetName<TWindow>(this TWindow info,string name) where TWindow : Window
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            info.Title=name;
            return info;
        }
        /// <summary>
        /// 设置应用程序宽度
        /// </summary>
        /// <param name="info"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static TWindow SetWidth<TWindow>(this TWindow info, double width) where TWindow: Window
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            info.Width=width;
            return info;
        }
        /// <summary>
        /// 设置应用程序高度
        /// </summary>
        /// <param name="info"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static TWindow SetHeight<TWindow>(this TWindow info, double height) where TWindow : Window
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            info.Height=height;
            return info;
        }
    }
}
