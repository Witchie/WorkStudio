using GreenWhale.Extensions.TestTools2.Models;
using System.Text;

namespace GreenWhale.Extensions.TestTools2.ViewModels
{
    /// <summary>
    /// 发送模型
    /// </summary>
    public class SendModel: CommunicationModel
    {
        /// <summary>
        /// 加载ASCII码
        /// </summary>
        /// <param name="ascii"></param>
        public void LoadAscii(string ascii)
        {
          var temp= Encoding.ASCII.GetBytes(ascii);
            LoadBytes(temp);
        }

    }
}
