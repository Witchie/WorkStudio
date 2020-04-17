namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试计划
    /// </summary>
    public class TestPlan
    {
        public TestPlan(string category="测试内容")
        {
            Category = category;
        }

        /// <summary>
        /// 测试目的
        /// </summary>
        public Purpose Purpose { get; set; } = new Purpose();
        /// <summary>
        /// 测试步骤
        /// </summary>
        public TestSteps TestSteps { get; set; } = new TestSteps();
        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 测试名称
        /// </summary>
        public string Name { get; set; }
    }

}
