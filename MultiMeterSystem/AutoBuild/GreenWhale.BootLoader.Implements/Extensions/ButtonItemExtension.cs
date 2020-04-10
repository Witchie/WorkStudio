namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 按钮服务扩展
    /// </summary>
    public static class ButtonItemExtension
    {
        /// <summary>
        /// 加载命令服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="buttonItem"></param>
        /// <param name="command"></param>
        public static void LoadCommService<TService>(this IButtonItem buttonItem, TService command) where TService: CommandService
        {
            if (buttonItem is null)
            {
                throw new System.ArgumentNullException(nameof(buttonItem));
            }

            if (command is null)
            {
                throw new System.ArgumentNullException(nameof(command));
            }

            buttonItem.Command = command.Command;
            buttonItem.CommandParameter = command.CommandParameter;
            buttonItem.CommandTarget = command.CommandTarget;
        }
    }
}
