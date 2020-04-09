using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Linq;
using System;
using GreenWhale.Application.SerialPorts;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0
{
    /// <summary>
    /// 串口扩展
    /// </summary>
    public static class SerialPortExtension
    {
        /// <summary>
        /// 异步读取
        /// </summary>
        /// <param name="serialPort"></param>
        /// <param name="queue"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static Task<byte[]> ReadAsync(this SerialPort serialPort, ref Queue<byte> queue, int bufferSize = 1024)
        {
            byte[] buffer = new byte[bufferSize];
            var length = serialPort.BaseStream.Read(buffer, 0, buffer.Length);
            if (length > 0)
            {
                var temp = buffer.Take(length).ToArray();
                queue.Enqueue(temp);
                return Task.FromResult(temp);
            }
            else
            {
                return Task.FromResult(new byte[] { });
            }
        }
        /// <summary>
        /// 发起请求
        /// </summary>
        /// <param name="serialPort"></param>
        /// <param name="content"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static async Task<string> Request(this SerialPort serialPort, string content, TimeSpan timeSpan)
        {
            serialPort.Write(content);
            await Task.Delay(timeSpan);
            return serialPort.ReadLine();
        }
    }

}
