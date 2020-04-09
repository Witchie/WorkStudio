using Microsoft.Extensions.DependencyInjection;
namespace GreenWhale.RunTime.Scripts
{
    public static class InvokeContextExtension
    {
        public static IMessageBox MessageBox(this InvokeContext invokeContext)
        {
            var messageBox = invokeContext.ServiceBus.GetService<IMessageBox>();
            return messageBox;
        }
    }
}
