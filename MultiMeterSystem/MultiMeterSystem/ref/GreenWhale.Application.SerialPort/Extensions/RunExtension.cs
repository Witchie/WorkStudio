using GreenWhale.Application.SerialPorts;
using System;

namespace GreenWhale.Extensions.DependencyInjection
{
    /// <summary>
    /// Run扩展
    /// </summary>
    public static class RunExtension
    {
        /// <summary>
        /// 添加Run扩展
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="handler"></param>
        public static void Run(this IApplicationBuilder applicationBuilder, RequestDelegate handler)
        {
            if (applicationBuilder is null)
            {
                throw new ArgumentNullException(nameof(applicationBuilder));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            applicationBuilder.Use((s) => handler);
        }
    }
}
