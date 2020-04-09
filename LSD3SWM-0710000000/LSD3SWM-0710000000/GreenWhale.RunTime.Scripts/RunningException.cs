using System;

namespace GreenWhale.RunTime.Scripts
{
    /// <summary>
    /// 运行结果
    /// </summary>
    public class RunningException
    {

        /// <summary>
        /// 执行结果
        /// </summary>
        public RunningResult Result { get; set; }
        /// <summary>
        /// 是否出错
        /// </summary>
        public bool IsError { get; set; } = false;
        /// <summary>
        /// 错误内容
        /// </summary>
        public Exception ErrorMessage { get; set; }
    }

}
