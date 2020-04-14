using GreenWhale.BootLoader.Implements.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 功能管理服务
    /// </summary>
    public class FunctionUIService : IFunctionUIService
    {
        /// <summary>
        /// 命令UI服务
        /// </summary>
        internal readonly IRibbonBarService ribbonBarService;
        internal readonly IPanelService toolBoxService;
        internal readonly OutputBoxCommandService outputBoxCommandService;
        internal readonly IServiceProvider serviceProvider;
        /// <summary>
        /// 命令UI服务
        /// </summary>
        /// <param name="ribbonBarService"></param>
        /// <param name="toolBoxService"></param>
        /// <param name="outputBoxCommandService"></param>
        /// <param name="serviceProvider"></param>
        public FunctionUIService(IRibbonBarService ribbonBarService, IPanelService toolBoxService, OutputBoxCommandService outputBoxCommandService, IServiceProvider serviceProvider)
        {
            this.ribbonBarService = ribbonBarService;
            this.toolBoxService = toolBoxService;
            this.outputBoxCommandService = outputBoxCommandService;
            this.serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 添加输出框功能
        /// </summary>
        public void UseOutputBox(string path = "视图/视图/输出框")
        {
            var box = toolBoxService.CreateOutputBox();
            box.AllowHide = true;
            box.ShowHideButton = true;
            box.AutoHideExpandState = DevExpress.Xpf.Docking.Base.AutoHideExpandState.Hidden;
            var view = new RibbonMenuWithPageView(path);
            ribbonBarService.AddRibbonMenu(view.LoadService(outputBoxCommandService));
        }
        /// <summary>
        /// 添加一个默认页面
        /// </summary>
        public void AddDefaultDocument<TDefaultContent>(TDefaultContent defaultContent) where TDefaultContent : FrameworkElement
        {
            var item = toolBoxService.CreateDocumentPanel(new DocumentInfo<TDefaultContent> { Caption = "默认页面", Content = defaultContent });
            item.Content.AllowClose = false;
            item.Content.AllowDock = false;
            item.Content.AllowDrag = false;
            item.Content.AllowFloat = false;
        }
        /// <summary>
        /// 添加工具箱
        /// </summary>
        /// <typeparam name="TToolBoxContent"></typeparam>
        /// <param name="boxContent"></param>
        /// <param name="caption">标题</param>
        /// <param name="panelLocation">面板位置</param>
        public void AddToolBox<TToolBoxContent>(TToolBoxContent boxContent, string caption, PanelLocation panelLocation = PanelLocation.Left) where TToolBoxContent : FrameworkElement
        {
            if (boxContent is null)
            {
                throw new ArgumentNullException(nameof(boxContent));
            }

            if (string.IsNullOrEmpty(caption))
            {
                throw new ArgumentException("message", nameof(caption));
            }

            toolBoxService.CreateToolBoxPanel(new PanelInfo<TToolBoxContent> { Caption = caption, Content = boxContent }, panelLocation);
        }
        /// <summary>
        /// 添加按钮并注入命令服务
        /// </summary>
        /// <param name="view"></param>
        /// <param name="servie"></param>
        public void AddRibbonMenu<TServie>(RibbonMenuWithPageView view, TServie servie) where TServie : CommandService
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (servie is null)
            {
                throw new ArgumentNullException(nameof(servie));
            }
            ribbonBarService.AddRibbonMenu(view.LoadService(servie));
        }
        /// <summary>
        /// 添加单击触发工具箱
        /// </summary>
        /// <typeparam name="TToolBox"></typeparam>
        /// <param name="view"></param>
        public void AddClickRibbonMenuWithToolBox<TToolBox>(RibbonMenuWithPageView view) where TToolBox : FrameworkElement
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            var service = new ClickToolBoxCommandService(typeof(TToolBox), toolBoxService, view.BarButtonItemName, serviceProvider);
            ribbonBarService.AddRibbonMenu(view.LoadService(service));
        }

        /// <summary>
        /// 添加单击触发弹出视图功能
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="view"></param>
        /// <param name="serviceProvider"></param>
        public void AddClickRibbonMenuWithPage<TPage>(RibbonMenuWithPageView view, IServiceProvider serviceProvider) where TPage : FrameworkElement
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            var service = new ClickPageCommandService(typeof(TPage), toolBoxService, view.BarButtonItemName, serviceProvider);
            ribbonBarService.AddRibbonMenu(view.LoadService(service));
        }
        /// <summary>
        /// 添加单击多触发
        /// </summary>
        /// <typeparam name="TPage1"></typeparam>
        /// <typeparam name="TPage2"></typeparam>
        /// <param name="page1View"></param>
        /// <param name="page2View"></param>
        public void AddClickRibbonMenuWithPages<TPage1, TPage2>(RibbonMenuWithPageView page1View, RibbonMenuWithPageView page2View) where TPage1 : FrameworkElement where TPage2 : FrameworkElement
        {
            if (page1View is null)
            {
                throw new ArgumentNullException(nameof(page1View));
            }



            if (page2View is null)
            {
                throw new ArgumentNullException(nameof(page2View));
            }

            var page2Service = new ClickToolBoxCommandService(typeof(TPage2), toolBoxService, page2View.BarButtonItemName, serviceProvider);
            var page1Service = new ClickPageCommandService<ClickToolBoxCommandService>(typeof(TPage1), toolBoxService, page1View.BarButtonItemName, page2Service, serviceProvider);
            ribbonBarService.AddRibbonMenu(page1View.LoadService(page1Service));
        }
        /// <summary>
        /// 添加单击触发弹出视图功能
        /// </summary>
        /// <param name="view"></param>
        /// <param name="pageType"></param>
        public void AddClickRibbonMenuWithPage(RibbonMenuWithPageView view, Type pageType)
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (pageType is null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }
            var service = new ClickPageCommandService(pageType, toolBoxService, view.BarButtonItemName, serviceProvider);
            ribbonBarService.AddRibbonMenu(view.LoadService(service));
        }
        /// <summary>
        /// 添加单击触发弹出视图功能
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="view"></param>
        public void AddClickRibbonMenuWithPage<TPage>(RibbonMenuWithPageView view) where TPage : FrameworkElement
        {
            AddClickRibbonMenuWithPage(view, typeof(TPage));
        }
    }
}
