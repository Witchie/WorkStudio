using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Flyout.Native;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
namespace GreenWhale.Extensions.Views
{
    public static class ViewExtension
    {
        /// <summary>
        /// 放置到浮动面板中
        /// </summary>
        /// <typeparam name="TFlyout"></typeparam>
        /// <param name="userControl"></param>
        /// <returns></returns>
        public static TFlyout SetFlyout<TFlyout>(this UserControl userControl) where TFlyout : FlyoutBase, new()
        {
            var flyout=  userControl.GetFlyout<TFlyout>();
            if (flyout==null)
            {
                TFlyout flyoutControl = new TFlyout
                {
                    Content = userControl
                };
                return flyoutControl;
            }
            else
            {
                return flyout;
            }
        }
        /// <summary>
        /// 将控件渲染到UI上
        /// </summary>
        /// <typeparam name="TFlyout"></typeparam>
        /// <param name="userControl"></param>
        /// <param name="uIElement"></param>
        /// <returns></returns>
        public static TFlyout RenderTo<TFlyout>(this TFlyout userControl,Grid uIElement) where TFlyout : FlyoutBase
        {
            Grid.SetZIndex(userControl,1000000);
            uIElement.Children.Add(userControl);
            return userControl;
        }
        /// <summary>
        /// 将控件渲染到UI上
        /// </summary>
        /// <typeparam name="TFlyout"></typeparam>
        /// <param name="userControl"></param>
        /// <param name="uIElement"></param>
        /// <returns></returns>
        public static TFlyout RenderTo<TFlyout>(this TFlyout userControl, UserControl uIElement) where TFlyout : FlyoutBase
        {
            var children=  LogicalTreeHelper.GetChildren(uIElement);
            var grid = children.OfType<Grid>().LastOrDefault();
            if (grid!=null)
            {
                userControl.RenderTo(grid);
                return userControl;
            }
            else
            {
                throw new ArgumentException("无法找到子Grid容器控件");
            }
        }
        /// <summary>
        /// 获取控件的浮动面板
        /// </summary>
        /// <typeparam name="TFlyout"></typeparam>
        /// <param name="userControl"></param>
        /// <returns></returns>
        public static TFlyout GetFlyout<TFlyout>(this UserControl userControl) where TFlyout : FlyoutBase
        {
            var parent = LogicalTreeHelper.GetParent(userControl);
            if (parent != null)
            {
                return parent as TFlyout;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 将浮动面板显示出来
        /// </summary>
        /// <typeparam name="TFlyout"></typeparam>
        /// <param name="flyout"></param>
        /// <returns></returns>
        public static TFlyout Show<TFlyout>(this TFlyout flyout) where TFlyout : FlyoutBase
        {
            flyout.IsOpen = true;
            return flyout;
        }
        public static TFlyout Hide<TFlyout>(this TFlyout flyout) where TFlyout : FlyoutBase
        {
            flyout.IsOpen = false;
            return flyout;
        }
        /// <summary>
        /// 设置吸附的对象
        /// </summary>
        /// <typeparam name="TFlyout"></typeparam>
        /// <param name="flyout"></param>
        /// <param name="uIElement"></param>
        /// <returns></returns>
        public static TFlyout SetPlacement<TFlyout>(this TFlyout flyout,UIElement uIElement) where TFlyout : FlyoutBase
        {
            flyout.PlacementTarget = uIElement;
            return flyout;
        }
        /// <summary>
        /// 设置吸附的对象
        /// </summary>
        /// <typeparam name="TFlyout"></typeparam>
        /// <param name="flyout"></param>
        /// <param name="uIElement"></param>
        /// <returns></returns>
        public static TFlyout SetPlacement<TFlyout>(this TFlyout flyout, object uIElement) where TFlyout : FlyoutBase 
        {
            return flyout.SetPlacement(uIElement as UIElement);
        }
        /// <summary>
        /// 转换为窗口
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <returns></returns>
        public static TWindow SetWindow<TWindow>(this UserControl userControl) where TWindow : Window, new()
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
        public static TWindow GetWindow<TWindow>(this UserControl userControl) where TWindow : Window
        {
            var parent = LogicalTreeHelper.GetParent(userControl);
            if (parent != null)
            {
                return parent as TWindow;
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
        public static TWindow SetWidth<TWindow>(this TWindow userControl, double width) where TWindow : Window
        {
            userControl.Width = width;
            return userControl;
        }
        /// <summary>
        /// 窗口最大化100%
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static TControl Large<TControl>(this TControl control) where TControl:FrameworkElement
        {
            var size =GetWindowSize();

            control.Width = size.Width;
            control.Height = size.Height;
            return control;
        }
        /// <summary>
        /// 中等小小80%
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static TControl Medium<TControl>(this TControl control) where TControl : FrameworkElement
        {
            var size = GetWindowSize();

            control.Width = size.Width*0.8;
            control.Height = size.Height * 0.8;
            return control;
        }
        /// <summary>
        /// 小尺寸 60%;
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static TControl Small<TControl>(this TControl control) where TControl : FrameworkElement
        {
            var size = GetWindowSize();
            control.Width = size.Width * 0.6;
            control.Height = size.Height * 0.6;
            return control;
        }
        /// <summary>
        /// 自动启用
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <param name="frameworkElement"></param>
        /// <returns></returns>
        public static TControl AutoEnable<TControl>(this TControl control,FrameworkElement frameworkElement) where TControl : Window
        {
            control.Closing += (s, e) => { frameworkElement.IsEnabled = true; };
            return control;
        }
        /// <summary>
        /// 自动翻转按钮
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static TControl AutoToggle<TControl>(this TControl control, object sender) where TControl : Window => control.AutoToggle(sender as FrameworkElement);
        /// <summary>
        /// 自动翻转按钮
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static TControl AutoToggle<TControl>(this TControl control, FrameworkElement sender) where TControl : Window
        {
            return control.AutoDisable(sender).AutoEnable(sender);
        }
        /// <summary>
        /// 自动启用
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static TControl AutoEnable<TControl>(this TControl control, object sender) where TControl : Window => control.AutoEnable(sender as FrameworkElement);
        /// <summary>
        /// 自动禁用
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <param name="frameworkElement"></param>
        /// <returns></returns>
        public static TControl AutoDisable<TControl>(this TControl control,FrameworkElement frameworkElement) where TControl : Window
        {
            frameworkElement.IsEnabled = false;
            return control;
        }
        /// <summary>
        /// 自动禁用
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <param name="frameworkElement"></param>
        /// <returns></returns>
        public static TControl AutoDisable<TControl>(this TControl control, object frameworkElement) where TControl : Window => control.AutoDisable(frameworkElement as FrameworkElement);
        /// <summary>
        /// 偏小尺寸 70%;
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static TControl SmallE<TControl>(this TControl control) where TControl : FrameworkElement
        {
            var size = GetWindowSize();
            control.Width = size.Width * 0.7;
            control.Height = size.Height * 0.7;
            return control;
        }
        /// <summary>
        /// 微小
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static TControl Tiny<TControl>(this TControl control) where TControl : FrameworkElement
        {
            var size = GetWindowSize();
            control.Width = size.Width * 0.4;
            control.Height = size.Height * 0.4;
            return control;
        }
        /// <summary>
        /// 设置百分比
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <param name="width">百分比,最大1，最小0</param>
        /// <returns></returns>
        public static TWindow SetWidthPercent<TWindow>(this TWindow userControl, double width=0.8D) where TWindow : Window
        {
            if (width>1)
            {
                throw new ArgumentException("最大不得大于1");
            }
            var size = userControl.GetWindowSize();
            userControl.Width = size.Width * width;
            return userControl;
        }
        /// <summary>
        /// 设置高度百分比
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <param name="height">百分比,最大1，最小0</param>
        /// <returns></returns>
        public static TWindow SetHeightPercent<TWindow>(this TWindow userControl, double height=0.8D) where TWindow : Window
        {
            if (height > 1)
            {
                throw new ArgumentException("最大不得大于1");
            }
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
        public static TWindow SetStartupLocation<TWindow>(this TWindow userControl, WindowStartupLocation location = WindowStartupLocation.CenterScreen) where TWindow : Window
        {
            userControl.WindowStartupLocation = location;
            return userControl;
        }
        /// <summary>
        /// 中间显示
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="userControl"></param>
        /// <returns></returns>
        public static TWindow Center<TWindow>(this TWindow userControl) where TWindow : Window
        {
            userControl.SetStartupLocation();
            return userControl;
        }
        /// <summary>
        /// 重定义大小模式
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <param name="window"></param>
        /// <param name="resizeMode"></param>
        /// <returns></returns>
        public static TWindow ResizeMode<TWindow>(this TWindow window, ResizeMode resizeMode) where TWindow : Window
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
        public static TWindow WindowStyle<TWindow>(this TWindow window, WindowStyle style) where TWindow : Window
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
        public static TWindow SetSizeToContent<TWindow>(this TWindow userControl, SizeToContent location = SizeToContent.Height) where TWindow : Window
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
        public static ScreenSize GetWindowSize<TWindow>(this TWindow userControl) where TWindow : Window
        {
            return GetWindowSize();
        }
        /// <summary>
        /// 获取窗口大小
        /// </summary>
        /// <returns></returns>
        public static ScreenSize GetWindowSize() 
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
