namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试计划
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TestPlansCore<T> : ArrayObject<T> where T : TestPlan
    {
        public TestPlansCore()
        {
        }

        public TestPlansCore(params T[] collection) : base(collection)
        {
        }

        public TestPlansCore(int capacity) : base(capacity)
        {
        }
    }

}
