using GreenWhale.Extensions.TestTools2.Extensions;
using GreenWhale.Extensions.TestTools2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GreenWhale.Extensions.TestTools2.ViewModels
{
    /// <summary>
    /// 协议生成视图模型
    /// </summary>
    public class ProtocalGeneratorViewModel : ProtocalGeneratorModel
    {
        /// <summary>
        /// 完整数据包
        /// </summary>
        public  byte[] FullFrame
        {
            get
            {
                List<byte> vs = new List<byte>();
                vs.Add(Header);
                vs.Add(MeterType);
                vs.Add((byte)ProtocalDirection);
                vs.Add(ControlCode);
                vs.Add(Length);
                if (DataArea!=null)
                {
                    vs.AddRange(DataArea);
                }
                vs.Add(Crc);
                vs.Add(Footer);
                return vs.ToArray();
            }
        }
        /// <summary>
        /// 协议方向
        /// </summary>
        public string[] ProtocalDirections => Enum.GetNames(typeof(ProtocalDirection));
    }

}
