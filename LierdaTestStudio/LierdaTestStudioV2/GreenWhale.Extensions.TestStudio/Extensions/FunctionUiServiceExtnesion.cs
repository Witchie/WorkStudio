using GreenWhale.BootLoader.Implements;
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
    }
}
