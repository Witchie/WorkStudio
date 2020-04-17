namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试对象
    /// </summary>
    public class TestObject : ArrayObject<string>
    {
        public TestObject()
        {
        }

        public TestObject(params string[] collection) : base(collection)
        {
        }

        public TestObject(int capacity) : base(capacity)
        {
        }
    }

}
