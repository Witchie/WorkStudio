using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Ports;
using DevExpress.Mvvm;
using System.Diagnostics;
using GreenWhale.Extensions.TestTools2.Extensions;
using System.Threading;

namespace GreenWhale.Extensions.TestTools2.Views
{
    /// <summary>
    /// 串口上下文
    /// </summary>
    public abstract class SerialPortServiceBase:ViewModelBase
    {
        private readonly SerialPort serialPort;

        public SerialPortServiceBase(SerialPort serialPort)
        {
            this.serialPort = serialPort;
        }
        public void Open()
        {
            serialPort.Open();
            RaisePropertyChanged(nameof(IsOpen));
        }
        public void Close()
        {
            serialPort.Close();
            RaisePropertyChanged(nameof(IsOpen));
        }
        public string PortName { get=>serialPort.PortName; set=>serialPort.PortName=value; }
        /// <summary>
        /// 串口是否打开
        /// </summary>
        public bool IsOpen => serialPort.IsOpen;
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        protected void Write(byte[] buffer)
        {
            serialPort.Write(buffer,0,buffer.Length);
        }
        /// <summary>
        /// 获取缓冲数据
        /// </summary>
        /// <returns></returns>
        public async Task< byte[]> Read()
        {
            serialPort.ReadTimeout = 3000;
            return await  Task.Run(()=>{
                byte[] buffer = new byte[1024];
                try
                {
                    var length = serialPort.Read(buffer, 0, buffer.Length);
                if (length > 0)
                {
                    var data = buffer.Take(length).ToArray();
                    Debug.WriteLine("RX:" + data.ToHex());
                    return data;
                }
                else
                {
                    return Array.Empty<byte>();
                }
                }
                catch (Exception err)
                {
                    Debug.WriteLine(err.Message);
                    return Array.Empty<byte>();
                }
            });
            
        }
 
        private readonly static object _locker = new object();
        /// <summary>
        /// 收到数据休眠等待时间
        /// </summary>
        public int DeplayTime { get; set; } = 500;
        /// <summary>
        /// 发送并获取数据
        /// </summary>
        /// <param name="content"></param>
        /// <param name="timespan"></param>
        /// <param name="validateCallBack"></param>
        /// <returns></returns>
        public abstract Task<byte[]> Request(byte[] content, int timespan, Func<byte[], Task<PassModel>> validateCallBack, int tryCount = 3);
    }
}
