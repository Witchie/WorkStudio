using System;
using System.Collections.Generic;

namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 文档修改信息
    /// </summary>
    public class ModifyInfo:ISplit
    {
        public ModifyInfo()
        {
        }

        public ModifyInfo(Version version, DateTime dateTime, string remark=null)
        {
            Version = version;
            DateTime = dateTime;
            Remark = remark;
        }

        /// <summary>
        /// 版本
        /// </summary>
        public Version Version { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public string[] Split()
        {
            List<string> vs = new List<string>();
            vs.Add($"V{Version}");
            vs.Add(DateTime.Date.ToLongDateString());
            vs.Add(Remark);
            return vs.ToArray();
        }
    }
}
