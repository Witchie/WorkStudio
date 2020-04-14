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
        private readonly IPanelService panelService;
        private readonly string PageName;
        /// <summary>
        /// 单击触发工具箱
        /// </summary>
        /// <param name="page"></param>
        /// <param name="toolBoxService"></param>
        /// <param name="pageName"></param>
        /// <param name="serviceProvider"></param>
        public ClickToolBoxCommandService(Type page, IPanelService toolBoxService, string pageName,IServiceProvider serviceProvider):base(serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(pageName))
            {
                throw new System.ArgumentException("message", nameof(pageName));
            }

            this.page = page ?? throw new System.ArgumentNullException($"{nameof(page)}你必须先注册对应的服务");
            panelService = toolBoxService ?? throw new System.ArgumentNullException(nameof(toolBoxService));
            PageName = pageName;
        }
        /// <summary>
        /// 单击执行触发
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            var content=  ServiceProvider.GetService(page);
            panelService.CreateToolBoxPanel(new PanelInfo<FrameworkElement> { Caption = PageName, Content = content as FrameworkElement });
        }
    }
}
