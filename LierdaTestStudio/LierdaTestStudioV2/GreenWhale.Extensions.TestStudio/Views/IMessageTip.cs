using System;
using System.Threading.Tasks;

namespace GreenWhale.Extensions.TestTools2.Views
{
    /// <summary>
    /// 消息提示框
    /// </summary>
    public interface IMessageTip
    {
        /// <summary>
        /// 显示提示
        /// </summary>
        /// <param name="content"></param>
        void ShowTip(string content, TimeSpan? timeSpan=null);
        /// <summary>
        /// 关闭提示
        /// </summary>
        Task CloseTip();
    }
}
