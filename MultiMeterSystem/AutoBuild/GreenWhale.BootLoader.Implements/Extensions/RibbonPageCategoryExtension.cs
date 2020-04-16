using System;
using System.Linq;
using DevExpress.Xpf.Ribbon;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// RibbonPage分组扩展
    /// </summary>
    public static class RibbonPageCategoryExtension
    {
        /// <summary>
        /// 第一个或者默认
        /// </summary>
        /// <param name="ribbonPageCategory"></param>
        /// <param name="captionName"></param>
        /// <returns></returns>
        public static RibbonPage Add(this RibbonPageCategory ribbonPageCategory,string captionName)
        {
            if (ribbonPageCategory is null)
            {
                throw new ArgumentNullException(nameof(ribbonPageCategory));
            }

            if (string.IsNullOrEmpty(captionName))
            {
                throw new ArgumentException("message", nameof(captionName));
            }
            Again:
                var ribbonPage = ribbonPageCategory.Pages.Where(p => p.Caption?.ToString() == captionName).FirstOrDefault();
                if (ribbonPage == null)
                {
                     var page = DefaultRibbonPage(captionName);
                    ribbonPageCategory.Pages.Add(page);
                    goto Again;
                }
            return ribbonPage;
        }
        /// <summary>
        /// 默认的RibbonPage
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        private static RibbonPage DefaultRibbonPage(string caption)
        {
            RibbonPage ribbonPage = new RibbonPage();
            ribbonPage.Caption = caption;
            return ribbonPage;
        }
        /// <summary>
        /// 默认页分组
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        private static RibbonPageGroup DefaultRibbonPageCategory(string caption)
        {
            RibbonPageGroup ribbonPage = new RibbonPageGroup();
            ribbonPage.Caption = caption;
            return ribbonPage;
        }
        /// <summary>
        /// 添加一个分组
        /// </summary>
        /// <param name="ribbonPage"></param>
        /// <param name="captionName"></param>
        /// <param name="commandService">命令服务</param>
        /// <returns></returns>
        public static RibbonPageGroup Add(this RibbonPage ribbonPage, string captionName,CommandService commandService=null)
        {
            if (ribbonPage is null)
            {
                throw new ArgumentNullException(nameof(ribbonPage));
            }

            if (string.IsNullOrEmpty(captionName))
            {
                throw new ArgumentException("message", nameof(captionName));
            }

        Again:
            var group = ribbonPage.Groups.Where(p => p.Caption == captionName).FirstOrDefault();
            if (group == null)
            {
                var page = DefaultRibbonPageCategory(captionName);
                if (commandService!=null)
                {
                    page.ShowCaptionButton = true;
                    page.CaptionButtonCommand = commandService.Command;
                    page.CaptionButtonCommandParameter = commandService.CommandParameter;
                    page.CaptionButtonCommandTarget = commandService.CommandTarget;
                }
                else
                {
                    page.ShowCaptionButton = false;
                }
                ribbonPage.Groups.Add(page);
                goto Again;
            }
            
            return group;
        }
        /// <summary>
        /// 获取分组
        /// </summary>
        /// <param name="ribbonPage"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static RibbonPageGroup GetGroup(this RibbonPage ribbonPage,string groupName)
        {
            if (ribbonPage is null)
            {
                throw new ArgumentNullException(nameof(ribbonPage));
            }

            if (string.IsNullOrEmpty(groupName))
            {
                throw new ArgumentException("message", nameof(groupName));
            }

            return  ribbonPage.Groups.Where(p => p.Caption == groupName).FirstOrDefault();
        }
    }
}
