using System.Collections.Generic;

namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试检测内容
    /// </summary>
    public class TestContent: ArrayObject<string>
    {
        public TestContent()
        {

        }

        public TestContent(params string[] collection) : base(collection)
        {

        }

        public TestContent(int capacity) : base(capacity)
        {

        }
    }
}
