using System;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 单击触发事件
    /// </summary>
    public class ClickActionCommandService:CommandService
    {
        /// <summary>
        /// 单击回调
        /// </summary>
        protected readonly Action<object> clickAction;
        /// <summary>
        /// 单击触发事件
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="clickAction">单击时要调用的事件</param>
        public ClickActionCommandService(IServiceProvider serviceProvider, Action<object> clickAction) : base(serviceProvider)
        {
            this.clickAction = clickAction;
        }
        /// <summary>
        /// 单击触发事件
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            clickAction?.Invoke(parameter);
        }
    }
}
