using System.Windows;

namespace GreenWhale.RunTime.Scripts
{
    /// <summary>
    /// 消息框服务
    /// </summary>
    public interface IMessageBox
    {
        /// <summary>
        /// 消息框
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        /// <param name="messageBox"></param>
        /// <returns></returns>
        public MessageBoxResult Show(string content, string title, MessageBoxButton messageBox);
        public MessageBoxResult Show(string content, string title, MessageBoxButton messageBox, MessageBoxImage image);
        public MessageBoxResult Show(string content, string title, MessageBoxButton messageBox, MessageBoxImage image, MessageBoxResult defaultResult);
        public MessageBoxResult Show(string content, string title);
        public MessageBoxResult Show(string content);
    }
}
