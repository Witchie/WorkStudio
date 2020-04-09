using System;
using System.ComponentModel.DataAnnotations;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    /// <summary>
    /// 测试用例
    /// </summary>
    public class TestCategory
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 协议包ID
        /// </summary>
        public int FramID { get; set; }
        /// <summary>
        /// 组ID
        /// </summary>
        public string MeterID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 资源状态
        /// </summary>
        public ResourceState ResourceState { get; set; }
        /// <summary>
        /// 测试值
        /// </summary>
        public string TestValue { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
