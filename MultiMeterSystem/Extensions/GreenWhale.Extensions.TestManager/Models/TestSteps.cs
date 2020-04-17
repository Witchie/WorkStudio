using System.Collections.Generic;
using System.Linq;
namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试步骤
    /// </summary>
    public class TestSteps : ArrayObject<TestStep>
    {
        public TestSteps()
        {
        }

        public TestSteps(params TestStep[] collection) : base(collection)
        {
        }

        public TestSteps(int capacity) : base(capacity)
        {
        }
        public static implicit operator TestSteps(Dictionary<string,string> dictionary)
        {
            return new TestSteps(dictionary.Select(p => new TestStep(p.Key, p.Value)).ToArray());
        }
    }
}
