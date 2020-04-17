using System.Linq;
using System.Text;
namespace GreenWhale.Application.SerialPorts
{
    /// <summary>
    /// 消息内容
    /// </summary>
    public class RequestContentBase
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public RequestContentBase()
        {
        }
        /// <summary>
        /// 加载数据内容
        /// </summary>
        /// <param name="array"></param>
        public void LoadContent(byte[] array)
        {
            this.Bytes = array;
        }
        /// <summary>
        /// 加载数据内容
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(string content)
        {
            this.Bytes = Encoding.ASCII.GetBytes(content);
        }
        /// <summary>
        /// 字节数据
        /// </summary>
        public byte[] Bytes { get;private set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content => Encoding.ASCII.GetString(Bytes).Split('\n').FirstOrDefault();
        /// <summary>
        /// 消息内容
        /// </summary>
        /// <param name="content"></param>
        public static implicit operator RequestContentBase(string content)
        {
            var message= new RequestContentBase();
            message.LoadContent(content);
            return message;
        }
        public static implicit operator RequestContentBase(byte[] content)
        {
            var message = new RequestContentBase();
            message.LoadContent(content);
            return message;
        }
        public static implicit operator string(RequestContentBase content)
        {
            return content.Content;
        }
    }
}
