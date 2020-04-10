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
        /// 判断面板是否存在
        /// </summary>
        /// <param name="layoutGroup"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        //public static T IsPanelExist<T>(this LayoutGroup layoutGroup,string caption) where T: BaseLayoutItem
        //{
        //    if (layoutGroup is null)
        //    {
        //        throw new ArgumentNullException(nameof(layoutGroup));
        //    }

        //    if (string.IsNullOrEmpty(caption))
        //    {
        //        throw new ArgumentException("message", nameof(caption));
        //    }
        //    return LogicalTreeHelper.GetChildren(layoutGroup).OfType<T>().Where(p => p.Caption?.ToString() == caption).FirstOrDefault();
        //   // return  layoutGroup.Items.Where(p => p.Caption?.ToString() == caption).FirstOrDefault() as T;
        //}
        /// <summary>
        /// 添加面板
        /// </summary>
        /// <param name="layoutGroup"></param>
        /// <param name="caption"></param>
        /// <param name="content"></param>
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
        /// <returns></returns>
        public static LayoutGroup CreateLayoutGroup(this LayoutGroup layoutGroup,string caption)
        {
            if (layoutGroup is null)
            {
                throw new ArgumentNullException(nameof(layoutGroup));
            }

            LayoutGroup layoutGroup1 = new LayoutGroup();
            layoutGroup1.AllowFloat = false;
            layoutGroup1.Caption = caption;
            layoutGroup1.Name = caption;
            layoutGroup.Items.Add(layoutGroup1);
            return layoutGroup1;
        }
        public static T Find<T>(this DependencyObject frameworkElement, string name) where T:class
        {
            return  LogicalTreeHelper.FindLogicalNode(frameworkElement,name) as T;
        }
        public static DependencyObject Find(this DependencyObject frameworkElement, string name) 
        {
            return LogicalTreeHelper.FindLogicalNode(frameworkElement, name);
        }
        public static IEnumerable< T> Children<T>(this DependencyObject frameworkElement) where T : class
        {
            if (frameworkElement==null)
            {
                return default;
            }
            return LogicalTreeHelper.GetChildren(frameworkElement).OfType<T>();
        }
    }
}
