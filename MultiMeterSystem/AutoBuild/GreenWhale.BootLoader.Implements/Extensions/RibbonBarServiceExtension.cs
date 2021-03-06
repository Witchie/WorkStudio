﻿using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// Ribbon服务扩展
    /// </summary>
    public static class RibbonBarServiceExtension
    {
        /// <summary>
        /// 添加Ribbon按钮
        /// </summary>
        /// <param name="ribbonBarService"></param>
        /// <param name="action"></param>
        public static void AddRibbonMenus(this IRibbonBarService ribbonBarService,Action<List<RibbonMenuWithPageView>> action)
        {
            List<RibbonMenuWithPageView> view = new List<RibbonMenuWithPageView>();
            action?.Invoke(view);
            ribbonBarService.LoadRibbonPages(view);
        }
        /// <summary>
        /// 添加按钮并注入命令服务
        /// </summary>
        /// <param name="ribbonBarService"></param>
        /// <param name="view"></param>
        public static void AddRibbonMenu(this IRibbonBarService ribbonBarService, RibbonMenuWithPageView view) 
        {
            ribbonBarService.AddRibbonMenus(s => 
            {
                s.Add(view);
            });
        }
        /// <summary>
        /// 添加按钮对象
        /// </summary>
        /// <param name="ribbonPageGroup"></param>
        /// <param name="buttonItem"></param>
        public static void AddButton(this RibbonPageGroup ribbonPageGroup, RibbonMenuWithPageView buttonItem) 
        {
            if (ribbonPageGroup is null)
            {
                throw new ArgumentNullException(nameof(ribbonPageGroup));
            }
            if (buttonItem is null)
            {
                throw new ArgumentNullException(nameof(buttonItem));
            }
            var item = new BarButtonItem() 
            {
                Content=buttonItem.BarButtonItemName,
                Command=buttonItem.Service.Command,
                CommandParameter =buttonItem.Service.CommandParameter,
                CommandTarget=buttonItem.Service.CommandTarget,
                Glyph=buttonItem.ImageSource,
                RibbonStyle=RibbonItemStyles.Large,
                Tag=buttonItem.TagData ,
                BarItemDisplayMode=BarItemDisplayMode.ContentAndGlyph
            };
            if (buttonItem.BarButtonItemNameDescription!=null)
            {
                item.ToolTip = buttonItem.BarButtonItemNameDescription;
            }
            ribbonPageGroup.Items.Add(item);
        }
        /// <summary>
        /// 添加Ribbon页面分组
        /// </summary>
        /// <param name="ribbonPage"></param>
        /// <param name="pageGroupName"></param>
        /// <returns></returns>
        public static RibbonPageGroup AddGroup(this RibbonPage ribbonPage, string pageGroupName)
        {
            if (ribbonPage is null)
            {
                throw new ArgumentNullException(nameof(ribbonPage));
            }

            if (string.IsNullOrEmpty(pageGroupName))
            {
                throw new ArgumentException("message", nameof(pageGroupName));
            }
            var group = new RibbonPageGroup { Caption = pageGroupName };
            ribbonPage.Groups.Add(group);
            return group;
        }
        /// <summary>
        /// 添加ribbon名称
        /// </summary>
        /// <param name="ribbonPage"></param>
        /// <param name="ribbonName">页面名称</param>
        /// <returns></returns>
        public static RibbonPage AddRibbonPage(this IRibbonBarService ribbonPage, string ribbonName)
        {
            if (ribbonPage is null)
            {
                throw new ArgumentNullException(nameof(ribbonPage));
            }

            if (string.IsNullOrEmpty(ribbonName))
            {
                throw new ArgumentException("message", nameof(ribbonName));
            }

            var ribbon = new RibbonPage() { Caption = ribbonName };
            ribbonPage.RibbonPages.Add(ribbon);
            return ribbon;
        }
        /// <summary>
        /// 判断RibbonGroup是否存在
        /// </summary>
        /// <param name="ribbonPage"></param>
        /// <param name="ribbonPageGroupName"></param>
        /// <returns></returns>
        internal static RibbonPageGroup IsRibbonPageGroupExist(this RibbonPage ribbonPage, string ribbonPageGroupName)
        {
            if (ribbonPage is null)
            {
                throw new ArgumentNullException(nameof(ribbonPage));
            }

            if (string.IsNullOrEmpty(ribbonPageGroupName))
            {
                throw new ArgumentException("message", nameof(ribbonPageGroupName));
            }

            return ribbonPage.Groups.Where(p => p.Caption == ribbonPageGroupName).FirstOrDefault();
        }
        /// <summary>
        /// 判断Ribbon页面是否存在，不存在则返回空
        /// </summary>
        /// <param name="ribbonPage"></param>
        /// <param name="ribbonName">页面名称</param>
        /// <returns></returns>
        internal static RibbonPage IsRibbonPageExist(this IRibbonBarService ribbonPage, string ribbonName)
        {
            if (ribbonPage is null)
            {
                throw new ArgumentNullException(nameof(ribbonPage));
            }

            if (string.IsNullOrEmpty(ribbonName))
            {
                throw new ArgumentException("message", nameof(ribbonName));
            }

            return ribbonPage.RibbonPages.Where(p => p.Caption?.ToString() == ribbonName).FirstOrDefault();
        }
    }
}
