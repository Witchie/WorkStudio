using System;
using System.Windows;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 应用程序信息
    /// </summary>
    internal class ApplicationInfo : IApplicationInfo
    {
        private Window Window;
        public ApplicationInfo(Window window)
        {
            Window = window;
        }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string GetApplicationName()
        {
            return Window.Title;
        }
        /// <summary>
        /// 应用名称
        /// </summary>
        public void SetApplicationName(string value)
        {
            Window.Title = value;
        }

        /// <summary>
        /// 主窗口宽
        /// </summary>
        public double GetMainWindowWidth()
        {
            return Window.Width;
        }

        /// <summary>
        /// 主窗口宽
        /// </summary>
        public void SetMainWindowWidth(double value)
        {
            Window.Width = value;
        }

        /// <summary>
        /// 主窗口高
        /// </summary>
        public double GetMainWindowHeight()
        {
            return Window.Height;
        }

        /// <summary>
        /// 主窗口高
        /// </summary>
        public void SetMainWindowHeight(double value)
        {
            Window.Height = value;
        }
    }
}
