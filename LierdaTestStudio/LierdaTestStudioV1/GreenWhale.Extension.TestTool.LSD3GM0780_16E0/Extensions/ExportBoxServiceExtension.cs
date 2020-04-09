using GreenWhale.BootLoader.Implements;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExportBoxServiceExtension
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="exportBoxService"></param>
        /// <param name="content"></param>
        public static void Log(this IExportBoxService exportBoxService,string content)
        {
            exportBoxService.Log(ConstHelper.ApplicationName, content);
        }
        /// <summary>
        /// 日志打印新的一行
        /// </summary>
        /// <param name="exportBoxService"></param>
        public static void LogNewLine(this IExportBoxService exportBoxService)
        {
            exportBoxService.Log(ConstHelper.ApplicationName, "...................................................");
        }
        /// <summary>
        /// ASCII码写日志
        /// </summary>
        /// <param name="exportBoxService"></param>
        /// <param name="content"></param>
        public static void Log(this IExportBoxService exportBoxService, byte[] content)
        {
            var text = Encoding.ASCII.GetString(content);
            exportBoxService.Log(ConstHelper.ApplicationName, text);
        }
    }
}
