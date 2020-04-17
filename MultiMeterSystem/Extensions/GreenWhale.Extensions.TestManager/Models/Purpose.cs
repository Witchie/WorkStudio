using System.Collections.Generic;

namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试目的
    /// </summary>
    public class Purpose : ArrayObject<string>
    {
        public Purpose()
        {

        }

        public Purpose(params string[]  collection) : base(collection)
        {

        }

        public Purpose(int capacity) : base(capacity)
        {

        }
        public static implicit operator Purpose(string content)
        {
           var dd= content.Split("、");
            return new Purpose(dd);
        }
    }

}
