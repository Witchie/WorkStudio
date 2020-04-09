using System.IO.Ports;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    public class ConstHelper
    {
        public const string ApplicationName = "生产测试系统";
        /// <summary>
        /// 测试成功
        /// </summary>
        public const string OnSuccess = "<Succes>";
        /// <summary>
        /// 测试OK
        /// </summary>
        public const string OnOK = "<OK>";
        /// <summary>
        /// 干簧管测试
        /// </summary>
        public const string Start1 = "<START1>";
        /// <summary>
        /// 光电测试
        /// </summary>
        public const string Start2 = "<START2>";
        /// <summary>
        /// 开始测试干簧管
        /// </summary>
        public const string BeginTestStart1 = "开始测试干簧管";
        /// <summary>
        /// 开始测试光电
        /// </summary>
        public const string BeginTestStart2= "开始测试光电";
    }
}
