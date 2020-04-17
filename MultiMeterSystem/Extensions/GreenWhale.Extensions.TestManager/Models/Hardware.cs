namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 硬件信息
    /// </summary>
    public class Hardware:ArrayObject<string>
    {
        public Hardware()
        {

        }

        public Hardware(params string[] collection) : base(collection)
        {

        }

        public Hardware(int capacity) : base(capacity)
        {

        }
    }
}
