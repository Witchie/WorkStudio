using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Threading;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 日志消息队列
    /// </summary>
    public class ExportBoxService : IExportBoxService
    {
        private ExportBox exportBox;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        /// <summary>
        /// 日志消息队列
        /// </summary>
        /// <param name="exportBox"></param>
        public ExportBoxService(ExportBox exportBox)
        {
            this.exportBox = exportBox;
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
            dispatcherTimer.Start();
        }

        private async void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            await  exportBox.Dispatcher.InvokeAsync(() =>
            {
                var res = TextLog(exportBox.CurrentSource);
                exportBox._exportSource.ItemsSource = QueueName;
                var count = res.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var text = res.Dequeue();
                        exportBox.Log(text);
                    }
                }
            });

        }
        /// <summary>
        /// 日志信息i
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private Queue<string> TextLog(string source)
        {
            if (!QueueName.Contains(source))
            {
                return new Queue<string>();
            }
            return QueueList[source];
        }
        /// <summary>
        /// 队列名称列表
        /// </summary>
        public string[] QueueName => QueueList.Keys.Select(p => p).ToArray();
        /// <summary>
        /// 日志列表
        /// </summary>
        public Dictionary<string, Queue<string>> QueueList { get; private set; } = new Dictionary<string, Queue<string>>();
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="content"></param>
        public void Log(string source, string content)
        {
            if (!QueueName.Contains(source))
            {
                QueueList.Add(source, new Queue<string>());
            }
            QueueList[source].Enqueue(content);
        }
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="source"></param>
        public void Clear(string source)
        {
            if (QueueName.Contains(source))
            {
                QueueList[source].Clear();
            }
            exportBox.Clear();
        }
        /// <summary>
        /// 清空所有
        /// </summary>
        public void Clear()
        {
            QueueList.Clear();
            exportBox.Clear();
        }
    }
}
