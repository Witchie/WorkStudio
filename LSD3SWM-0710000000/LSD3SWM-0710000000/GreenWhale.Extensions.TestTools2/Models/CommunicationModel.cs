using DevExpress.Mvvm;
using GreenWhale.Extensions.TestTools2.Extensions;

namespace GreenWhale.Extensions.TestTools2.Models
{
    /// <summary>
    /// 通信模型
    /// </summary>
    public class CommunicationModel : ViewModelBase
    {
        /// <summary>
        /// 装载字节码
        /// </summary>
        /// <param name="content"></param>
        public void LoadBytes(byte[] content)
        {
            this.Content = content;
        }

        /// <summary>
        /// 装载HEX字符串
        /// </summary>
        /// <param name="content"></param>
        public void LoadHex(string content)
        {
            var temp = content.ToHex();
            this.LoadBytes(temp);
        }
        /// <summary>
        /// 数据内容
        /// </summary>
        public byte[] Content { get; private set; }
    }
}
