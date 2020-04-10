using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Ribbon;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// Ribbon控件服务
    /// </summary>
    public class RibbonBarService
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
                var res= this.IsRibbonPageExist(item.RibbonPageCaption);
                if (res==null)
                {
                    this.AddRibbonPage(item.RibbonPageCaption);
                    goto Page;
                }
                Group:
                var pageGroup=  res.IsRibbonPageGroupExist(item.RibbonPageGroupCaption);
                if (pageGroup==null)
                {
                    res.AddRibbonPageGroup(item.RibbonPageGroupCaption);
                    goto Group;
                }
                pageGroup.AddRibbonButtonItem(item);
            }
        }
        /// <summary>
        /// RIBBON页面
        /// </summary>
        internal ObservableCollection<RibbonPage> RibbonPages { get;private set; } = new ObservableCollection<RibbonPage>();
        /// <summary>
        /// 加载当前RibbonUI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentRibbonView"></param>
        public void LoadCurrentRibbon<TRibbonView>(TRibbonView currentRibbonView)  where TRibbonView: CurrentRibbonView<RibbonPage> 
        {
            if (currentRibbonView is null)
            {
                throw new ArgumentNullException(nameof(currentRibbonView));
            }
            var pages = currentRibbonView.GetRibbonPages();
            if (pages!=null&&pages.Count>0)
            {
                ribbonBar.currentribbon.PagesSource = pages;
                ribbonBar.currentribbon.IsVisible = true;
            }
            else
            {
                ribbonBar.currentribbon.IsVisible = false;
            }
        }
        /// <summary>
        /// 当前视图工具栏是否显示
        /// </summary>
        public bool CurrentRibbonIsVisible { get => ribbonBar.currentribbon.IsVisible; set => ribbonBar.currentribbon.IsVisible = value; }
    }
}
