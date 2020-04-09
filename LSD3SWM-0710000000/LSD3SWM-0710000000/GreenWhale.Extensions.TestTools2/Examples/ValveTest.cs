using GreenWhale.BootLoader.Implements;
using GreenWhale.Extensions.TestTools2.Extensions;
using GreenWhale.RunTime.Scripts;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

public class ValveTest : RuningCore
{
    public ValveTest()
    {
    }

    /// <summary>
    /// 调用方法
    /// <code>假设输入参数AA14A1FC024F4B4D55</code>
    /// </summary>
    /// <param name="invokeContext"></param>
    /// <returns></returns>
    public override RunningResult Invoke(InvokeContext invokeContext)
    {
        var content = GetDataArea(invokeContext.SourceFrame);
        Log(invokeContext, content.ToHex());
        var ascii = ToASCII(content);
        Log(invokeContext, ascii);
        if (invokeContext.Parameters.ContainsKey("IsAbnormalWork")&& invokeContext.Parameters["IsAbnormalWork"]==InvokeContext.True)
        {
            var messageBox=  invokeContext.ServiceBus.GetService<IMessageBox>();
            var res = messageBox.Show($"电机测试是否通过?", "提示", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                return new RunningResult(State.Qualified);
            }
            else
            {
                return new RunningResult(State.Unqualified);
            }
        }
        else
        {
            if (ascii == "OK")
            {
                return new RunningResult(State.Qualified, "合格");
            }
            else if (ascii == "ER")
            {
                return new RunningResult(State.Unqualified, "不合格");
            }
            else
            {
                return new RunningResult(State.None, "无法解析");
            }
        }
    }
    private static readonly object _locker = new object();
    public byte[] GetFullFrame(byte[] buffer)
    {
        List<byte> vs = new List<byte>();
        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] == 0xAA)
            {
                vs.AddRange(buffer.Skip(i).ToArray());
            }
        }
        return vs.ToArray();
    }
    public override RunningResult FrameValidate(InvokeContext invokeContext)
    {
        lock (_locker)
        {
            try
            {
                var buffer = invokeContext.SourceFrame;
                if (buffer == null && buffer.Length < 7)
                {
                    Log(invokeContext, "...");
                    return new RunningResult(State.FrameValidateFailed);
                }
                var vs=  GetFullFrame(buffer);
                if (vs.Length < 7)
                {
                    Log(invokeContext, $"长度不足{vs.ToHex()}");
                    return new RunningResult(State.FrameValidateFailed);
                }
                int length = vs[4];
                var take = 4 + length + 2;
                var fullframe = vs.Take(take + 1).ToArray();
                if (fullframe.LastOrDefault() == 0x55)
                {
                    var crc = fullframe.Skip(1).Take(4 + length).Sum(p => p);
                    var crc1 = fullframe.Skip(1 + 4 + length).First();
                    if ((byte)crc == crc1)
                    {
                        Log(invokeContext, "校验成功");
                        return new RunningResult(State.FrameValidatePassed);
                    }
                    else
                    {
                        Log(invokeContext, "和校验不通过");
                        return new RunningResult(State.FrameValidateFailed);
                    }
                }
                else
                {
                    Log(invokeContext, "结束符号不正确");
                    return new RunningResult(State.FrameValidateFailed);
                }
            }
            catch (System.Exception err)
            {
                Log(invokeContext, err.ToString());
                return new RunningResult(State.FrameValidateFailed);
            }

        }
    }
    /// <summary>
    /// 获取数据域
    /// </summary>
    /// <param name="fullframe"></param>
    /// <returns></returns>
    private byte[] GetDataArea(byte[] fullframe)
    {
        var content = fullframe.Skip(5).Take(fullframe.Skip(4).FirstOrDefault()).ToArray();
        return content;
    }
    /// <summary>
    /// 打印日志
    /// </summary>
    /// <param name="invokeContext"></param>
    /// <param name="content"></param>
    private void Log(InvokeContext invokeContext, string content)
    {
        var svc = invokeContext?.ServiceBus?.GetService<IExportBoxService>();
        svc?.Log(content);
    }
}
