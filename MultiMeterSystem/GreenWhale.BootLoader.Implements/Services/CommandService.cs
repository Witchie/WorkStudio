using System;
using System.Windows;
using System.Windows.Input;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 命令服务
    /// </summary>
    public abstract class CommandService : ICommand
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        protected  IServiceProvider ServiceProvider { get;private set; }
        protected CommandService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// 判断是否可以执行
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public virtual bool CanExecute(object parameter) => true;
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);
        public event EventHandler CanExecuteChanged;

        public ICommand Command => this;
        public object CommandParameter => null;

        public IInputElement CommandTarget => null;
    }
}
