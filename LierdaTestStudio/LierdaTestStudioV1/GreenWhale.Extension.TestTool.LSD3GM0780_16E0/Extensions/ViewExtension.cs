using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ViewExtension
    {
        /// <summary>
        /// 转换为窗口
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <returns></returns>
        public static TWindow SetWindow<TWindow>(this UserControl userControl) where TWindow : Window,new()
        {
            var window = new TWindow();
            window.Content = userControl;
            return window;
        }
        /// <summary>
        /// 获取父级窗口
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <returns></returns>
        public static TWindow GetWindow<TWindow>(this UserControl userControl) where TWindow : Window, new()
        {
          var parent=   LogicalTreeHelper.GetParent(userControl);
            if (parent!=null)
            {
                return   parent as TWindow;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static TWindow SetWidth<TWindow>(this TWindow userControl,double width) where TWindow : Window, new()
        {
            userControl.Width = width;
            return userControl;
        }
        /// <summary>
        /// 设置百分比
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static TWindow SetWidthPercent<TWindow>(this TWindow userControl, double width) where TWindow : Window, new()
        {
            var size = userControl.GetWindowSize();
            userControl.Width = size.Width * width;
            return userControl;
        }
        /// <summary>
        /// 设置高度百分比
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static TWindow SetHeightPercent<TWindow>(this TWindow userControl, double height) where TWindow : Window, new()
        {
            var size = userControl.GetWindowSize();
            userControl.Height = size.Height * height;
            return userControl;
        }
        /// <summary>
        /// 设置高度
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static TWindow SetHeight<TWindow>(this TWindow userControl, double height) where TWindow : Window, new()
        {
            userControl.Height = height;
            return userControl;
        }
        /// <summary>
        /// 设置默认启动位置
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static TWindow SetStartupLocation<TWindow>(this TWindow userControl, WindowStartupLocation location=WindowStartupLocation.CenterScreen) where TWindow : Window, new()
        {
            userControl.WindowStartupLocation = location;
            return userControl;
        }
        /// <summary>
        /// 重定义大小模式
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="window"></param>
        /// <param name="resizeMode"></param>
        /// <returns></returns>
        public static TWindow ResizeMode<TWindow>(this TWindow window, ResizeMode resizeMode) where TWindow : Window, new()
        {
            window.ResizeMode = resizeMode;
            return window;
        }
        /// <summary>
        /// 窗口样式
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="window"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static TWindow WindowStyle<TWindow>(this TWindow window, WindowStyle style) where TWindow : Window, new()
        {
            window.WindowStyle = style;
            return window;
        }
        /// <summary>
        /// 设置尺寸自适应
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static TWindow SetSizeToContent<TWindow>(this TWindow userControl, SizeToContent location = SizeToContent.Height) where TWindow : Window, new()
        {
            userControl.SizeToContent = location;
            return userControl;
        }
        /// <summary>
        /// 获取窗口尺寸
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <returns></returns>
        public static ScreenSize GetWindowSize<TWindow>(this TWindow userControl) where TWindow : Window, new()
        {
            return new ScreenSize(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
        }
    }
    /// <summary>
    /// 窗口尺寸
    /// </summary>
    public struct ScreenSize
    {
        public ScreenSize(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Width { get; set; }
        public double Height { get; set; }
    }
}
