using System;

namespace GreenWhale.Extensions.TestTools2.Models
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public class EventBus : IEventBus
    {
        public event Action<string, object> OnNotify;

        public void Notify(string notifyType, object notifyContent = null)
        {
            OnNotify?.Invoke(notifyType, notifyContent);
        }
    }
}
