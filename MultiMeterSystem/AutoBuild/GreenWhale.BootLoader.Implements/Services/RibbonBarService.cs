using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Ribbon;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// Ribbon控件服务
    /// </summary>
    internal class RibbonBarService : IRibbonBarService
    {
        /// <summary>
        /// Ribbon控件
        /// </summary>
        private readonly RibbonBar ribbonBar;
        /// <summary>
        /// Ribbon控件服务
        /// </summary>
        /// <param name="ribbonBar"></param>
        public RibbonBarService(RibbonBar ribbonBar)
        {
            this.ribbonBar = ribbonBar;
        }
        /// <summary>
        /// 加载视图页面
        /// </summary>
        /// <param name="pageViews"></param>
        public void LoadRibbonPages(IEnumerable<RibbonMenuWithPageView> pageViews)
        {
            if (pageViews is null)
            {
                throw new ArgumentNullException(nameof(pageViews));
            }
            LoadUI(pageViews);
            ribbonBar.ribboncategory.PagesSource = RibbonPages;
        }
        /// <summary>
        /// 加载UI
        /// </summary>
        /// <param name="pageViews"></param>
        private void LoadUI(IEnumerable<RibbonMenuWithPageView> pageViews)
        {
            if (pageViews is null)
            {
                throw new ArgumentNullException(nameof(pageViews));
            }

            foreach (var item in pageViews)
            {
            Page:
                var res = this.IsRibbonPageExist(item.RibbonPageCaption);
                if (res == null)
                {
                    this.AddRibbonPage(item.RibbonPageCaption);
                    goto Page;
                }
            Group:
                var pageGroup = res.IsRibbonPageGroupExist(item.RibbonPageGroupCaption);
                if (pageGroup == null)
                {
                    res.AddGroup(item.RibbonPageGroupCaption);
                    goto Group;
                }
                pageGroup.AddButton(item);
            }
        }
        /// <summary>
        /// RIBBON页面
        /// </summary>
        public ObservableCollection<RibbonPage> RibbonPages { get; set; } = new ObservableCollection<RibbonPage>();
    }
}
