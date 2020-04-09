using GreenWhale.Extensions.TestTools2.ViewModels;
using GreenWhale.RunTime.Scripts;

namespace GreenWhale.Extensions.TestTools2.Views
{
    /// <summary>
    /// 执行脚本信息错误
    /// </summary>
    public class RunningExceptionDetail: RunningException
    {
        public AddResourceDefineViewModel AddResourceDefineViewModel { get; set; }
    }
}
