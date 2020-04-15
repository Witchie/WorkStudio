using GreenWhale.BootLoader.Implements;
using GreenWhale.Extensions.TestStudio.Views;
using GreenWhale.Extensions.TestTools2.Views;

namespace GreenWhale.Extensions.TestTools2.Extensions
{
    public static class FunctionUiServiceExtnesion
    {
        /// <summary>
        /// 使用测试工具
        /// </summary>
        /// <param name="functionUIService"></param>
        public static IFunctionUIService UseTestStudio(this IFunctionUIService functionUIService)
        {
            functionUIService.AddToolBox<SerialPortView>(new RibbonMenuWithPageView("生产中心/系统配置/串口配置:配置串口端口和比特率"));
            functionUIService.AddPage<ResourceDefineView>(new RibbonMenuWithPageView("生产中心/协议管理/协议编辑器:应用协议编辑"));
            functionUIService.AddPage<ProtocalGenerator>(new RibbonMenuWithPageView("生产中心/协议管理/数据包生成器:生成数据包"));
            //functionUIService.AddClickRibbonMenuWithPage<FactoryProduceView>(new RibbonMenuWithPageView("生产中心/生产记录/测试记录查询:查询生产测试信息"));
            functionUIService.AddPages<FactoryProduceView, ScanPanelView>(new RibbonMenuWithPageView("生产中心/工厂生产/生产测试:生产并测试表具"), new RibbonMenuWithPageView("生产中心/工厂生产/扫描面板:扫描二维码"));
            return functionUIService;
        }
        /// <summary>
        /// 使用更新日志
        /// </summary>
        /// <param name="functionUIService"></param>
        /// <returns></returns>
        public static IFunctionUIService UseChangeLog(this IFunctionUIService functionUIService)
        {
            functionUIService.AddPage<ChangedLogView>(new RibbonMenuWithPageView("关于/帮助/更新日志:查看各个版本的更新内容"));
            return functionUIService;
        }
        public static IFunctionUIService UseHelp(this IFunctionUIService functionUIService)
        {
            functionUIService.AddPage<HelpView>(new RibbonMenuWithPageView("关于/帮助/帮助文档:查看使用帮助文档"));
            return functionUIService;
        }
    }
}
