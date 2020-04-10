using System;
using System.Windows;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 单击触发工具箱
    /// </summary>
    /// <typeparam name="TToolBox"></typeparam>
    public class ClickToolBoxCommandService : CommandService 
    {
        private readonly Type page;
        private readonly PanelService ToolBoxService;
        private readonly string PageName;
        public ClickToolBoxCommandService(Type page, PanelService toolBoxService, string pageName,IServiceProvider serviceProvider):base(serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(pageName))
            {
                throw new System.ArgumentException("message", nameof(pageName));
            }

            this.page = page ?? throw new System.ArgumentNullException($"{nameof(page)}你必须先注册对应的服务");
            ToolBoxService = toolBoxService ?? throw new System.ArgumentNullException(nameof(toolBoxService));
            PageName = pageName;
        }
        /// <summary>
        /// 单击执行触发
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            var content=  ServiceProvider.GetService(page);
            ToolBoxService.CreateToolBoxPanel(new PanelInfo<FrameworkElement> { Caption = PageName, Content = content as FrameworkElement });
        }
    }
}
