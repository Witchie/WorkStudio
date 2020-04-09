using System;
using System.Collections.Generic;

namespace GreenWhale.Application.SerialPorts
{
    public interface ISerialPortContext
    {
        /// <summary>
        /// 串口是否开启
        /// </summary>
        bool IsOpen { get; }
        /// <summary>
        /// 出队列
        /// </summary>
        Queue<byte> OutBuffer { get; }
        /// <summary>
        /// 服务总线
        /// </summary>
        IServiceProvider RequestServices { get; }
        /// <summary>
        /// 写入队列
        /// </summary>
        /// <returns></returns>
        byte[] Flush();
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <returns></returns>
        TContent Read<TContent>() where TContent : RequestContentBase, new();
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        void Write(byte[] buffer);
        /// <summary>
        /// 清空队列
        /// </summary>
        void Clear();
        /// <summary>
        /// 输入缓冲消息
        /// </summary>
        string InBufferMessage { get; }
    }
}