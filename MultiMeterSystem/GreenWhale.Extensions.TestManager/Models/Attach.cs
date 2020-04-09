namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 附件信息
    /// </summary>
    public class Attach : ArrayObject<AttachContent>
    {
        public Attach()
        {
        }

        public Attach(params AttachContent[] collection) : base(collection)
        {
        }

        public Attach(int capacity) : base(capacity)
        {
        }
        public override string ToString()
        {
            if (this.Count==0)
            {
                return "无";
            }
            return base.ToString();
        }
    }
    /// <summary>
    /// 附件内容
    /// </summary>
    public class AttachContent
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }
    }
}
