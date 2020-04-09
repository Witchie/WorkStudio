using System.Collections.Generic;

namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 软件信息
    /// </summary>
    public class Software : ArrayObject<string>
    {
        public Software()
        {

        }

        public Software(params string[]  collection) : base(collection)
        {

        }

        public Software(int capacity) : base(capacity)
        {

        }
    }
}
