using GreenWhale.BootLoader.Implements;
using GreenWhale.Extensions.TestStudio;

namespace GreenWhale.Extensions.TestTools2.Extensions
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
            exportBoxService.Log(Resource.ApplicationName, content);
        }
    }
}
