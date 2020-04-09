using System.Collections.Generic;
namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0
{
    /// <summary>
    /// 队列扩展
    /// </summary>
    public static class QueueExtension
    {
        /// <summary>
        /// 入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="buffer"></param>
        public static void Enqueue<T>(this Queue<T> queue, IEnumerable<T> buffer)
        {
            foreach (var item in buffer)
            {
                queue.Enqueue(item);
            }
        }

    }

}
