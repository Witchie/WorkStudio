namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试范围
    /// </summary>
    public class TestRange : ArrayObject<string>
    {
        public TestRange()
        {
        }

        public TestRange(params string[] collection) : base(collection)
        {
        }

        public TestRange(int capacity) : base(capacity)
        {
        }
    }
}
