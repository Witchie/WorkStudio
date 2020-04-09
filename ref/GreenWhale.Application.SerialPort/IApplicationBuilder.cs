using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace GreenWhale.Application.SerialPorts
{
    public interface IApplicationBuilder
    {
        /// <summary>
        /// 创建新的构建器
        /// </summary>
        /// <returns></returns>
        IApplicationBuilder New();
        /// <summary>
        /// 构建委托处理链
        /// </summary>
        /// <returns></returns>
        RequestDelegate Build();
        /// <summary>
        /// 添加处理中间件到委托请求链
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);
        /// <summary>
        /// 应用程序服务
        /// </summary>
        IServiceProvider ApplicationServices { get; set; }
    }
    /// <summary>
    /// 委托请求链
    /// </summary>
    /// <param name="current">当前处理中间件</param>
    /// <param name="next">下一个处理中间件</param>
    public delegate Task RequestDelegate(ISerialPortContext context);
    
}
