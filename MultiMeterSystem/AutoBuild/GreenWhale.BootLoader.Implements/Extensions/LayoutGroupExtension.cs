using DevExpress.Xpf.Docking;
using System;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 面板扩展
    /// </summary>
    public static class LayoutGroupExtension
    {
        /// <summary>
        /// 添加面板
        /// </summary>
        /// <param name="layoutGroup"></param>
        /// <param name="caption"></param>
        /// <param name="styleName"></param>
        /// <returns></returns>
        public static LayoutPanel CreateLayoutPanel(this LayoutGroup layoutGroup,string caption,Style styleName)
        {
            if (layoutGroup is null)
            {
                throw new ArgumentNullException(nameof(layoutGroup));
            }

            if (string.IsNullOrEmpty(caption))
            {
                throw new ArgumentException("message", nameof(caption));
            }

            LayoutPanel layoutPanel = new LayoutPanel();
            layoutGroup.Items.Add(layoutPanel);
            layoutPanel.Caption = caption;
            layoutPanel.Name = caption;
            layoutPanel.Style = styleName;
            return layoutPanel;
        }
        /// <summary>
        /// 添加面板
        /// </summary>
        /// <param name="layoutGroup"></param>
        /// <param name="caption">标题栏名称</param>
        /// <param name="captionImageSource">标题的icon</param>
        /// <returns></returns>
        public static LayoutGroup CreateLayoutGroup(this LayoutGroup layoutGroup,string caption,ImageSource captionImageSource=null)
        {
            if (layoutGroup is null)
            {
                throw new ArgumentNullException(nameof(layoutGroup));
            }

            if (caption is null)
            {
                throw new ArgumentNullException(nameof(caption));
            }

            LayoutGroup layoutGroup1 = new LayoutGroup();
            layoutGroup1.AllowFloat = false;
            layoutGroup1.Caption = caption;
            layoutGroup1.Name = caption;
            layoutGroup1.CaptionImage = captionImageSource;
            layoutGroup.Items.Add(layoutGroup1);
            return layoutGroup1;
        }
        /// <summary>
        /// 查询逻辑节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="frameworkElement"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static T Find<T>(this DependencyObject frameworkElement, string name) where T:class
        {
            return  LogicalTreeHelper.FindLogicalNode(frameworkElement,name) as T;
        }
        /// <summary>
        /// 查询逻辑节点
        /// </summary>
        /// <param name="frameworkElement"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static DependencyObject Find(this DependencyObject frameworkElement, string name) 
        {
            return LogicalTreeHelper.FindLogicalNode(frameworkElement, name);
        }
        /// <summary>
        /// 子节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="frameworkElement"></param>
        /// <returns></returns>
        internal static IEnumerable< T> Children<T>(this DependencyObject frameworkElement) where T : class
        {
            if (frameworkElement==null)
            {
                return default;
            }
            return LogicalTreeHelper.GetChildren(frameworkElement).OfType<T>();
        }
    }
}
