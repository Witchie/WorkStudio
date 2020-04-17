namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试结果
    /// </summary>
    public class TestResult
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 附件信息
        /// </summary>
        public ArrayObject<object> Attach { get; set; }
    }

}
