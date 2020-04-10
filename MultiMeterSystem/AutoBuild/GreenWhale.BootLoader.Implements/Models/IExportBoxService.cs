using System.Collections.Generic;

namespace GreenWhale.BootLoader.Implements
{
    public interface IExportBoxService
    {
        Dictionary<string, Queue<string>> QueueList { get; }
#pragma warning disable CA1819 // 属性不应返回数组
        string[] QueueName { get; }
#pragma warning restore CA1819 // 属性不应返回数组

        void Clear(string source);
        void Log(string source, string content);
    }
}