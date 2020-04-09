using System;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;
using GreenWhale.Extensions.TestTools2.Extensions;
using System.Collections.Generic;

namespace GreenWhale.Extensions.TestTools2.Views
{
    /// <summary>
    /// /串口上下文
    /// </summary>
    public class SerialPortContextLocal: SerialPortServiceBase
    {
        public SerialPortContextLocal(SerialPort serialPort) : base(serialPort)
        {
        }
        public override async Task<byte[]> Request(byte[] content,int timespan,Func<byte[], Task<PassModel>> validateCallBack,int tryCount=3)
        {
            Write(content);
            List<byte> vs = new List<byte>();
        Label:
            await Task.Delay(timespan);
            var buffer =await Read();
            if (buffer!=null)
            {
                vs.AddRange(buffer);

            }
            if (vs == null|| vs.Count==0)
            {
                if (--tryCount > 0)
                {
                    goto Label;
                }
                else
                {
                    return Array.Empty<byte>();
                }
            }
            var res=await validateCallBack?.Invoke(vs.ToArray());
            if (res!=null&&res.IsPassed==true)
            {
                Debug.WriteLine("PASS");
                return vs.ToArray();
            }
            else
            {
                if (--tryCount>0)
                {
                    Debug.WriteLine("RETARY");
                    goto Label;
                }
            }
            Debug.WriteLine($"PASS:{vs.ToArray().ToHex()}");
            return vs.ToArray();
        }
    }
    public class PassModel
    {
        public PassModel(byte[] frame, bool isPassed)
        {
            Frame = frame;
            IsPassed = isPassed;
        }

        public byte[] Frame { get; set; }
        public bool IsPassed { get; set; }
    }
}
