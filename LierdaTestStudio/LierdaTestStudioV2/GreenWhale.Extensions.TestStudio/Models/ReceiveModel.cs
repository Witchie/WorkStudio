using GreenWhale.Extensions.TestTools2.Extensions;
using GreenWhale.Extensions.TestTools2.Models;
using System.Text;

namespace GreenWhale.Extensions.TestTools2.ViewModels
{
    /// <summary>
    /// 接收模型
    /// </summary>
    public class ReceiveModel: CommunicationModel
    {
        /// <summary>
        /// 转换为ASCII码s
        /// </summary>
        /// <returns></returns>
        public string ToAscii()
        {
           return Encoding.ASCII.GetString(Content);
        }
        /// <summary>
        /// 转换为HEX字符串
        /// </summary>
        /// <returns></returns>
        public string ToHex()
        {
            return Content.ToHex();
        }
    }
}
