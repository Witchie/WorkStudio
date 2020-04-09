using GreenWhale.BootLoader.Implements;
using GreenWhale.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace GreenWhale.Application.SerialPorts
{
    /// <summary>
    /// 串口上下文
    /// </summary>
    public class SerialPortContext : ISerialPortContext
    {

        /// <summary>
        /// 串口
        /// </summary>
        private readonly SerialPort SerialPort;
        public SerialPortContext(SerialPort serialPort)
        {
            SerialPort = serialPort;
            SerialPort.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            ReadIn();
        }

        /// <summary>
        /// Gets or sets the <see cref="IServiceProvider"/> that provides access to the request's service container.
        /// </summary>
        public IServiceProvider RequestServices { get; private set; }
        /// <summary>
        /// 出队列
        /// </summary>
        public Queue<byte> OutBuffer { get; private set; } = new Queue<byte>();
        private Queue<byte> InBuffer = new Queue<byte>();
        /// <summary>
        /// 输入缓冲消息
        /// </summary>
        public string InBufferMessage => Read<RequestContent>();
        /// <summary>
        /// 写队列
        /// </summary>
        /// <param name="buffer"></param>
        public void Write(byte[] buffer)
        {
            OutBuffer.Enqueue(buffer);
        }
        /// <summary>
        /// 判断串口是否开启
        /// </summary>
        public bool IsOpen => SerialPort.IsOpen;
        /// <summary>
        /// 将数据全部写出
        /// </summary>
        public byte[] Flush()
        {
            var buffer = OutBuffer.ToArray();
            if (!SerialPort.IsOpen)
            {
                SerialPort.Open();
            }
            SerialPort.Write(buffer, 0, buffer.Length);
            Clear();
            return buffer;
        }
        private readonly static object _Locker = new object();
        private void ReadIn(int size = 1024)
        {
            try
            {
                lock (_Locker)
                {
                    byte[] buffer = new byte[size];
                    var length = this.SerialPort.Read(buffer, 0, buffer.Length);
                    InBuffer.Enqueue((buffer.Take(length).ToArray()));
                }
            }
            catch (Exception err)
            {
                    
            }
        }
        /// <summary>
        /// 获取请求的内容
        /// </summary>
        /// <param name="serialPort"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public TContent Read<TContent>() where TContent: RequestContentBase,new()
        {
            var message= new TContent();
            message.LoadContent(InBuffer.ToArray());
            return message;
        }
        /// <summary>
        /// 清空缓冲
        /// </summary>
        public void Clear()
        {
            OutBuffer.Clear();
            InBuffer.Clear();
        }
    }
}
