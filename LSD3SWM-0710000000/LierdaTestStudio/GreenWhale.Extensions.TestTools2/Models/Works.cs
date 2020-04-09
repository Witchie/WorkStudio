using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
namespace GreenWhale.Extensions.TestTools2.Views
{
    /// <summary>
    /// 数据表
    /// </summary>
    public class Works
    {
        [Key]
        public int ID { get; set; }
        public string DeviceId { get; set; }
        public DateTime DateTime { get; set; }
        public string State { get; set; }
        public string WorkName { get; set; }
        public int TestIndex { get; set; }
        public string TestResult { get; set; }
    }
}
