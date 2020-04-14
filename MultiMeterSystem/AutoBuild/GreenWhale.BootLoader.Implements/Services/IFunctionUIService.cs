using System;
using System.Windows;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// RIBBONUI服务
    /// </summary>
    public interface IFunctionUIService
    {
        /// <summary>
        /// 添加单页
        /// </summary>
        /// <param name="view"></param>
        /// <param name="pageType"></param>
        void AddPage(RibbonMenuWithPageView view, Type pageType);
        /// <summary>
        /// 添加单页
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="view"></param>
        void AddPage<TPage>(RibbonMenuWithPageView view) where TPage : FrameworkElement;
        /// <summary>
        /// 添加单页
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="view"></param>
        /// <param name="serviceProvider"></param>
        void AddPage<TPage>(RibbonMenuWithPageView view, IServiceProvider serviceProvider) where TPage : FrameworkElement;
        /// <summary>
        /// 添加多页
        /// </summary>
        /// <typeparam name="TPage1"></typeparam>
        /// <typeparam name="TPage2"></typeparam>
        /// <param name="page1View"></param>
        /// <param name="page2View"></param>
        void AddPages<TPage1, TPage2>(RibbonMenuWithPageView page1View, RibbonMenuWithPageView page2View)
            where TPage1 : FrameworkElement
            where TPage2 : FrameworkElement;
        /// <summary>
        /// 添加工具箱
        /// </summary>
        /// <typeparam name="TToolBox"></typeparam>
        /// <param name="view"></param>
        void AddToolBox<TToolBox>(RibbonMenuWithPageView view) where TToolBox : FrameworkElement;
        /// <summary>
        /// 添加默认页
        /// </summary>
        /// <typeparam name="TDefaultContent"></typeparam>
        /// <param name="defaultContent"></param>
        void AddDefaultDocument<TDefaultContent>(TDefaultContent defaultContent) where TDefaultContent : FrameworkElement;
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <typeparam name="TServie"></typeparam>
        /// <param name="view"></param>
        /// <param name="servie"></param>
        void AddRibbonMenu<TServie>(RibbonMenuWithPageView view, TServie servie) where TServie : CommandService;
        /// <summary>
        /// 添加工具箱
        /// </summary>
        /// <typeparam name="TToolBoxContent"></typeparam>
        /// <param name="boxContent"></param>
        /// <param name="caption"></param>
        /// <param name="panelLocation"></param>
        void AddToolBox<TToolBoxContent>(TToolBoxContent boxContent, string caption, PanelLocation panelLocation = PanelLocation.Left) where TToolBoxContent : FrameworkElement;
       /// <summary>
       /// 添加输出框
       /// </summary>
       /// <param name="path"></param>
        void UseOutputBox(string path = "视图/视图/输出框");
    }
}