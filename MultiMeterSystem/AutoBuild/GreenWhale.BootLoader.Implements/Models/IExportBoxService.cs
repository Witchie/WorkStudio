using System.Collections.Generic;

namespace GreenWhale.BootLoader.Implements
{
    public interface IExportBoxService
    {
        /// <summary>
        /// 消息队列列表
        /// </summary>
        Dictionary<string, Queue<string>> QueueList { get; }
        /// <summary>
        /// 队列名称
        /// </summary>
        string[] QueueName { get; }
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="source"></param>
        void Clear(string source);
        /// <summary>
        /// 清空所有
        /// </summary>
        void Clear();
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="content"></param>
        void Log(string source, string content);
    }
}