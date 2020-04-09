using GreenWhale.Application.SerialPorts;
using System;
using System.Threading.Tasks;

namespace GreenWhale.Extensions.DependencyInjection
{
    /// <summary>
    /// 使用扩展
    /// </summary>
    public static class UseExtension
    {
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="app"></param>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public static IApplicationBuilder Use(this IApplicationBuilder app, Func<ISerialPortContext, Func<Task>, Task> middleware)
        {
            return app.Use(next =>
            {
                return context => 
                {
                    return middleware(context, () => next(context));
                };
            });
        }
    }
}
