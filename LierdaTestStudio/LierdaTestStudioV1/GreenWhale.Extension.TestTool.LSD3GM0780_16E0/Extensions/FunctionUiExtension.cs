using GreenWhale.BootLoader.Implements;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Views;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0
{
    public static class FunctionUiExtension
    {
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="functionUIService"></param>
        public static void UseLSD3GM0780_16E0(this FunctionUIService functionUIService)
        {
            functionUIService.AddClickRibbonMenuWithToolBox<SerialPortView>(new RibbonMenuWithPageView("生产中心/系统配置/串口配置:配置串口端口和比特率"));
            functionUIService.AddClickRibbonMenuWithPage<ResourceDefineView>(new RibbonMenuWithPageView("生产中心/协议管理/协议编辑器:配置协议字典信息"));
            functionUIService.AddClickRibbonMenuWithPage<TestResultView>(new RibbonMenuWithPageView("生产中心/生产记录/测试记录查询:精确导出测试记录"));
            functionUIService.AddClickRibbonMenuWithPage<ProductProcessView>(new RibbonMenuWithPageView("生产中心/工厂生产/生产测试:扫码生成生产记录"));
        }
    }
}
