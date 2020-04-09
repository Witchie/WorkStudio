namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试结论
    /// </summary>
    public class TestConclusion
    {
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsPassed { get; set; }
        /// <summary>
        /// 测试结果
        /// </summary>
        public TestResults TestResults { get; set; }
    }

}
