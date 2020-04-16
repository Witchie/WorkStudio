using System;
using System.Windows;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 单击触发执行按钮
    /// </summary>
    public class ClickPageCommandService : CommandService
    {
        private readonly Type page;
        private readonly IPanelService panelService;
        private readonly string PageName;
        public ClickPageCommandService(Type page, IPanelService toolBoxService, string pageName,IServiceProvider serviceProvider):base(serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(pageName))
            {
                throw new System.ArgumentException("message", nameof(pageName));
            }

            this.page = page;
            panelService = toolBoxService;
            PageName = pageName;
        }
        /// <summary>
        /// 单击执行触发
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            var content = ServiceProvider.GetService(page);
            panelService.CreateDocumentPanel(new DocumentInfo<FrameworkElement> {Caption= PageName, Content= (FrameworkElement)content });
        }
    }
    /// <summary>
    /// 单击双触发
    /// </summary>
    /// <typeparam name="TCommandService"></typeparam>
    public class ClickPageCommandService<TCommandService> : ClickPageCommandService where TCommandService : CommandService
    {
        private readonly TCommandService commandService;

        public ClickPageCommandService(Type page, IPanelService toolBoxService, string pageName, TCommandService commandService,IServiceProvider serviceProvider) : base(page, toolBoxService, pageName, serviceProvider)
        {
            this.commandService = commandService;
        }
        public override void Execute(object parameter)
        {
            commandService.Execute(parameter);
            base.Execute(parameter);
        }
    }
}
