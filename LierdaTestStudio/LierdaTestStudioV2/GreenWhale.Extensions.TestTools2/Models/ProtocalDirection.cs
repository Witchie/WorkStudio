using System;
using System.ComponentModel;
using System.Text;

namespace GreenWhale.Extensions.TestTools2.Models
{
    /// <summary>
    /// 通信方向
    /// </summary>
    public enum ProtocalDirection:byte
    {   
        /// <summary>
        /// 主机
        /// </summary>
        [Description("主机")]
        Master = 0x11,
        /// <summary>
        /// 从机
        /// </summary>
        [Description("从机")]
        Slave = 0xA1,
    }
}
