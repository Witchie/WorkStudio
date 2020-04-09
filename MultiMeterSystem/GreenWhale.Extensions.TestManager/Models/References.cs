using System.Collections.Generic;

namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 参考资料
    /// </summary>
    public class References : ArrayObject<string>
    {
        public References()
        {
        }

        public References(params string[] collection) : base(collection)
        {
        }

        public References(int capacity) : base(capacity)
        {
        }
    }
}
