namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试分析
    /// </summary>
    public class TestAnalysis
    {
        /// <summary>
        /// 测试目的
        /// </summary>
        public Purpose Purpose { get; set; } = new Purpose();
        /// <summary>
        /// 测试对象
        /// </summary>
        public TestObject TestObject { get; set; } = new TestObject();
        /// <summary>
        /// 测试范围
        /// </summary>
        public TestRange TestRange { get; set; } = new TestRange();
        /// <summary>
        /// 主要检测内容
        /// </summary>
        public TestContent TestContent { get; set; } = new TestContent();
        /// <summary>
        /// 系统环境信息
        /// </summary>
        public EnviromentInfo EnviromentInfo { get; set; } = new EnviromentInfo();
    }
}
