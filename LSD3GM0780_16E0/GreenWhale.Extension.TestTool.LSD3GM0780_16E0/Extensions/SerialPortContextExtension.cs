using GreenWhale.Application.SerialPorts;
using GreenWhale.BootLoader.Implements;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using System.Text;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0
{
    public static class SerialPortContextExtension
    {
        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="serialPortContext"></param>
        /// <param name="content"></param>
        public static ISerialPortContext Write(this ISerialPortContext serialPortContext,string content)
        {
            serialPortContext.Write(Encoding.ASCII.GetBytes(content));
            return serialPortContext;
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="portContext"></param>
        /// <param name="exportBoxService"></param>
        /// <returns></returns>
        public static IExportBoxService WithLog(this byte[] portContext, IExportBoxService exportBoxService)
        {
            var text = Encoding.ASCII.GetString(portContext);
            exportBoxService.Log(ConstHelper.ApplicationName, text);
            return exportBoxService;
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="serialPortContext"></param>
        /// <returns></returns>
        public static RequestContent Read(this ISerialPortContext serialPortContext)
        {
            var context = serialPortContext.Read<RequestContent>();
            return context;
        }
    }

}
