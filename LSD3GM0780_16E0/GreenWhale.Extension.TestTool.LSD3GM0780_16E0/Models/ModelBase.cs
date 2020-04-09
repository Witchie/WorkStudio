namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    /// <summary>
    /// 基础模型
    /// </summary>
    public abstract class ModelBase
    {
        /// <summary>
        /// 值信息
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 资源状态
        /// </summary>
        public ResourceState ResourceState { get; set; } = ResourceState.N;
        /// <summary>
        /// 键名称
        /// </summary>
        public string KeyName { get; set; }
        /// <summary>
        /// 描述名称
        /// </summary>
        public string Caption { get; set; }
    }
}
