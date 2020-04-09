using GreenWhale.Application.SerialPorts;
using System.Threading.Tasks;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Services
{
    public class SerialPortMiddleware:IMiddleware
    {
        public SerialPortMiddleware()
        {
            
        }
        public Task InvokeAsync(ISerialPortContext context, RequestDelegate next)
        {

            return Task.CompletedTask;
        }
    }
}
