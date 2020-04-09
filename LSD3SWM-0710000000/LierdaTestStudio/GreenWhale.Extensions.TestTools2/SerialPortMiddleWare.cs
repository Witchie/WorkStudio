using GreenWhale.Application.SerialPorts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GreenWhale.Extensions.TestTools2
{
    /// <summary>
    /// 串口中间件
    /// </summary>
    public class SerialPortMiddleWare:IMiddleware
    {
        public Task InvokeAsync(ISerialPortContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
