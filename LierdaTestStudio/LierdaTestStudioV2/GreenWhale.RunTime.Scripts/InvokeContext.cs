using System;
using System.Collections.Generic;
namespace GreenWhale.RunTime.Scripts
{
    /// <summary>
    /// 调用上下文
    /// </summary>
    public class InvokeContext
    {
        /// <summary>
        /// 服务总线
        /// </summary>
        public IServiceProvider ServiceBus { get; private set; }
        /// <summary>
        /// 调用的参数
        /// </summary>
        public byte[] SourceFrame { get; set; }
        /// <summary>
        /// 参数信息
        /// </summary>
        public Dictionary<string,string> Parameters { get; set; }
        /// <summary>
        /// 数据包验证
        /// </summary>
        public const string FrameValudate = "FrameValudate";
        public const string IsAbnormalWork = "IsAbnormalWork";
        /// <summary>
        /// 是
        /// </summary>
        public const string True = "True";
        /// <summary>
        /// 否
        /// </summary>
        public const string False = "False";
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="serviceBus"></param>
        /// <returns></returns>
        public static InvokeContext Run(IServiceProvider serviceBus, byte[] sourceFrame, Dictionary<string, string> keyValuePairs=null)
        {
            return new InvokeContext() { Parameters = keyValuePairs??new Dictionary<string, string>(), ServiceBus = serviceBus, SourceFrame = sourceFrame?? Array.Empty<byte>() };
        }
    }
}
