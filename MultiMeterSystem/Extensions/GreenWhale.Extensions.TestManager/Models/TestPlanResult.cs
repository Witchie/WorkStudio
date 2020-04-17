namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试计划带结果
    /// </summary>
    public class TestPlanResult: TestPlan
    {
        /// <summary>
        /// 测试结论
        /// </summary>
        public TestConclusion TestConclusion { get; set; } = new TestConclusion();
    }

}
