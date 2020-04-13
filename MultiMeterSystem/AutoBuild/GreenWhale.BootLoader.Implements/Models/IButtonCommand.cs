using System.Windows;
using System.Windows.Input;
namespace GreenWhale.BootLoader.Implements
{
    public interface IButtonCommand
    {
        /// <summary>
        /// 命令
        /// </summary>
        ICommand Command { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        object CommandParameter { get; set; }
        /// <summary>
        /// 对象
        /// </summary>
        IInputElement CommandTarget { get; set; }
    }
}
