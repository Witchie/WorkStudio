using System;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 输出对话框服务
    /// </summary>
    public class OutputBoxCommandService : CommandService
    {
        private readonly IPanelService toolBoxService;
        /// <summary>
        /// 输出对话框服务
        /// </summary>
        /// <param name="toolBoxService"></param>
        /// <param name="serviceProvider"></param>
        public OutputBoxCommandService(IPanelService toolBoxService,IServiceProvider serviceProvider):base(serviceProvider)
        {
            this.toolBoxService = toolBoxService;
        }

        /// <summary>
        /// 执行代码
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            toolBoxService.CreateOutputBox();
        }
    }
}
