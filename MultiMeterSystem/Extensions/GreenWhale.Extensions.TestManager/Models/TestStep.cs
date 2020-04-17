namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试步骤
    /// </summary>
    public class TestStep
    {
        public TestStep()
        {
        }

        public TestStep(string action, string expected)
        {
            Action = action;
            Expected = expected;
        }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 预期值
        /// </summary>
        public string Expected { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public Attach Attach { get; set; } = new Attach();
    }

}
