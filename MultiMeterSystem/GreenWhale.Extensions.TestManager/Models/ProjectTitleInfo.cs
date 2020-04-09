using System;

namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 项目标题信息
    /// </summary>
    public class ProjectTitleInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public Version Version { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
