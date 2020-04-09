using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Views;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Services
{
    public class SerialPortViewServcie : ISerialPortViewServcie
    {
        private readonly SerialPortView serialPortView;
        /// <summary>
        /// 串口视图服务
        /// </summary>
        /// <param name="serialPortView"></param>
        public SerialPortViewServcie(SerialPortView serialPortView)
        {
            this.serialPortView = serialPortView;
        }
        /// <summary>
        /// 接收缓冲
        /// </summary>
        private Queue<byte> ReceiveBuffer = new Queue<byte>();
        /// <summary>
        /// 缓冲大小
        /// </summary>
        public int BufferSize { get; set; } = 100;
        /// <summary>
        /// 清空缓冲
        /// </summary>
        public void ClearBuffer()
        {
            ReceiveBuffer.Clear();
        }
    }
}
