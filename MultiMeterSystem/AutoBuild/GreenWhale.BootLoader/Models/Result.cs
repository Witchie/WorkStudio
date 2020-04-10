using System;
using System.Linq;

namespace GreenWhale.BootLoader.Services
{
    /// <summary>
    /// 执行结果
    /// </summary>
    public class Result
    {
        public Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

    }
    /// <summary>
    /// 错误结果
    /// </summary>
    public class ExceptionResult: Result
    {
        /// <summary>
        /// 错误结果
        /// </summary>
        /// <param name="exception"></param>
        public ExceptionResult(params Exception[] exception) : base(false, exception.FirstOrDefault()?.Message)
        {
            Exceptions = exception;
        }
        /// <summary>
        /// 错误消息
        /// </summary>
        public Exception[] Exceptions { get; set; }
    }
}
