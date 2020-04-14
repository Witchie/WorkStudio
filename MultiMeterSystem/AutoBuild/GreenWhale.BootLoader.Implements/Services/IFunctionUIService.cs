using System;
using System.Windows;

namespace GreenWhale.BootLoader.Implements
{
    public interface IFunctionUIService
    {
        void AddClickRibbonMenuWithPage(RibbonMenuWithPageView view, Type pageType);
        void AddClickRibbonMenuWithPage<TPage>(RibbonMenuWithPageView view) where TPage : FrameworkElement;
        void AddClickRibbonMenuWithPage<TPage>(RibbonMenuWithPageView view, IServiceProvider serviceProvider) where TPage : FrameworkElement;
        void AddClickRibbonMenuWithPages<TPage1, TPage2>(RibbonMenuWithPageView page1View, RibbonMenuWithPageView page2View)
            where TPage1 : FrameworkElement
            where TPage2 : FrameworkElement;
        void AddClickRibbonMenuWithToolBox<TToolBox>(RibbonMenuWithPageView view) where TToolBox : FrameworkElement;
        void AddDefaultDocument<TDefaultContent>(TDefaultContent defaultContent) where TDefaultContent : FrameworkElement;
        void AddRibbonMenu<TServie>(RibbonMenuWithPageView view, TServie servie) where TServie : CommandService;
        void AddToolBox<TToolBoxContent>(TToolBoxContent boxContent, string caption, PanelLocation panelLocation = PanelLocation.Left) where TToolBoxContent : FrameworkElement;
        void UseOutputBox(string path = "视图/视图/输出框");
    }
}