using System;
using System.Windows;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 应用程序信息
    /// </summary>
    internal class ApplicationInfo : IApplicationInfo
    {
        /// <summary>
        /// 应用程序信息
        /// </summary>
        public ApplicationInfo()
        {
        }
        private Window Window;
        /// <summary>
        /// 装在UI
        /// </summary>
        /// <param name="window"></param>
        public void LoadUI(Window window)
        {
            this.Window = window;
        }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string GetApplicationName()
        {
            if (Window == null)
            {
                throw new Exception($"请先调用{nameof(IApplicationInfo.LoadUI)}");
            }
            return Window.Title;
        }
        /// <summary>
        /// 应用名称
        /// </summary>
        public void SetApplicationName(string value)
        {
            if (Window == null)
            {
                throw new Exception($"请先调用{nameof(IApplicationInfo.LoadUI)}");
            }
            Window.Title = value;
        }

        /// <summary>
        /// 主窗口宽
        /// </summary>
        public double GetMainWindowWidth()
        {
            if (Window == null)
            {
                throw new Exception($"请先调用{nameof(IApplicationInfo.LoadUI)}");
            }
            return Window.Width;
        }

        /// <summary>
        /// 主窗口宽
        /// </summary>
        public void SetMainWindowWidth(double value)
        {
            if (Window == null)
            {
                throw new Exception($"请先调用{nameof(IApplicationInfo.LoadUI)}");
            }
            Window.Width = value;
        }

        /// <summary>
        /// 主窗口高
        /// </summary>
        public double GetMainWindowHeight()
        {
            if (Window == null)
            {
                throw new Exception($"请先调用{nameof(IApplicationInfo.LoadUI)}");
            }
            return Window.Height;
        }

        /// <summary>
        /// 主窗口高
        /// </summary>
        public void SetMainWindowHeight(double value)
        {
            if (Window == null)
            {
                throw new Exception($"请先调用{nameof(IApplicationInfo.LoadUI)}");
            }
            Window.Height = value;
        }
    }
}
