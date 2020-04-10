using System;
using System.Windows;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 应用程序信息
    /// </summary>
    public class ApplicationInfo
    {
        public ApplicationInfo()
        {
        }
        private Window Window;

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
                throw new Exception($"请先调用{nameof(ApplicationInfo.LoadUI)}");
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
                throw new Exception($"请先调用{nameof(ApplicationInfo.LoadUI)}");
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
                throw new Exception($"请先调用{nameof(ApplicationInfo.LoadUI)}");
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
                throw new Exception($"请先调用{nameof(ApplicationInfo.LoadUI)}");
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
                throw new Exception($"请先调用{nameof(ApplicationInfo.LoadUI)}");
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
                throw new Exception($"请先调用{nameof(ApplicationInfo.LoadUI)}");
            }
            Window.Height = value;
        }
    }
}
