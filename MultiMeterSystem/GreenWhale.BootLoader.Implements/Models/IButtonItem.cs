using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 按钮对象
    /// </summary>
    public interface IButtonItem: IButtonCommand
    {
        /// <summary>
        /// 视图简称
        /// </summary>
        string BarButtonItemName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        ImageSource ImageSource { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        string BarButtonItemNameDescription { get; set; }

        /// <summary>
        /// 寄存数据
        /// </summary>
        object TagData { get; set; }

    }
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
