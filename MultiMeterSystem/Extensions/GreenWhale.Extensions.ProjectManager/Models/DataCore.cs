using System.ComponentModel.DataAnnotations;

namespace GreenWhale.ProjectManager
{
    /// <summary>
    /// 核心数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataCore<T>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public T Id { get; set; }
        /// <summary>
        /// 显示编号
        /// </summary>
        public int Order { get; set; }
    }
}
