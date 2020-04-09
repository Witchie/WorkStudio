namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    /// <summary>
    /// 资源定义
    /// </summary>
    /// <typeparam name="Success"></typeparam>
    /// <typeparam name="Failed"></typeparam>
    public class ResourceDefine<Success,Failed> where Success:ModelBase where Failed:ModelBase
    {
        /// <summary>
        /// 成功的定义
        /// </summary>
        public Success OnSuccess { get; set; }
        /// <summary>
        /// 失败的定义
        /// </summary>
        public Failed OnFailed { get; set; }
        /// <summary>
        /// 功能描述
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 协议编号
        /// </summary>
        public int Number { get; set; }
    }
}
