using System;
using System.Collections.Generic;

namespace GreenWhale.ProjectManager
{
    /// <summary>
    /// 项目片段
    /// </summary>
    public class ProjectSegment: DataCore<int>
    {
        /// <summary>
        /// 事宜名称
        /// </summary>
        public string SegmentName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime DateTimeDesireStart { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime DateTimeDesireEnd { get; set; }
        /// <summary>
        /// 计划耗时
        /// </summary>
        public double TimeSpanDesire => (DateTimeDesireEnd - DateTimeDesireStart).TotalDays;
        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime DateTimeActualStart { get; set; }
        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime DateTimeActualEnd { get; set; }
        /// <summary>
        /// 实际耗时
        /// </summary>
        public double TimeSpanActual => (DateTimeActualEnd - DateTimeActualStart).TotalDays;
        /// <summary>
        /// 项目进度信息
        /// </summary>
        public ProcessInformations ProcessInformation { get; set; } = new ProcessInformations();
        /// <summary>
        /// 进度状态
        /// </summary>
        public ProcessState ProcessItemState { get; set; } = ProcessState.Wating;
    }
}
