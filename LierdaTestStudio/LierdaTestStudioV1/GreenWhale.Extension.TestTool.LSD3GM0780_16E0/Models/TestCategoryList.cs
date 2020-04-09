using System;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    /// <summary> 
    /// 单个水表的所有测试项目
    /// </summary>
    public class TestCategoryList
    {
        /// <summary>
        /// 唯一ID（表号）
        /// </summary>
        public string MeterID { get; set; }
        /// <summary>
        /// 当前表测试状态
        /// </summary>
        public ResourceState ResourceState { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 测试用例
        /// </summary>
        public TestCategory[] TestCategory { get; set; }
    }
}
