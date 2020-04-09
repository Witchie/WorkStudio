using System;
using System.Threading.Tasks;
using System.Windows;

namespace GreenWhale.RunTime.Scripts
{
    /// <summary>
    /// 运行时服务总线
    /// </summary>
    public abstract class RunningBase
    {
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public RunningException Run(InvokeContext invokeContext)
        {
            try
            {
                RunningResult context=null;
                if (invokeContext.SourceFrame == null || invokeContext.SourceFrame.Length == 0)
                {
                    return new RunningException() { IsError = true, Result = new RunningResult(State.FrameValidateFailed) };
                }
                if (invokeContext.Parameters.ContainsKey(InvokeContext.FrameValudate))
                {
                    if (invokeContext.Parameters[InvokeContext.FrameValudate]==InvokeContext.True)
                    {
                        context= FrameValidate(invokeContext);
                    }
                }
                else
                {
                    context = Invoke(invokeContext);

                }
                return new RunningException() { ErrorMessage = null, IsError = false, Result = context };
            }
            catch (System.Exception err)
            {
                return new RunningException() {ErrorMessage= err,IsError=true };
            }
        }
        public abstract RunningResult FrameValidate(InvokeContext invokeContext);
        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="invokeContext"></param>
        /// <returns></returns>
        public abstract RunningResult Invoke(InvokeContext invokeContext);
    }
}
