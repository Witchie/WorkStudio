using System;

namespace GreenWhale.Extensions.TestTools2.Models
{
    public interface IEventBus
    {
        event Action<string, object?> OnNotify;
        void Notify(string notifyType, object notifyContent = null);
    }
}
