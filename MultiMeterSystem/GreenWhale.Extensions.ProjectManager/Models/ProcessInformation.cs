using System;
using System.Collections.Generic;

namespace GreenWhale.ProjectManager
{
    /// <summary>
    /// 进度信息
    /// </summary>
    public class ProcessInformation:DataCore<int>
    {
        public ProcessInformation(string dateTime, string description)
        {
            DateTime = DateTime.Parse(dateTime);
            Description = description;
        }

        public ProcessInformation(DateTime dateTime, string description)
        {
            DateTime = dateTime;
            Description = description;
        }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        public override string ToString()
        {
            return $"{DateTime.ToShortDateString()}:{Description}";
        }
    }
    /// <summary>
    /// 进度信息
    /// </summary>
    public class ProcessInformations: List<ProcessInformation>
    {
        public override string ToString()
        {
            List<string> data = new List<string>();
            foreach (var item in this)
            {
                data.Add(item.ToString());
            }
            return string.Join(Environment.NewLine, data);
        }
    }
}
