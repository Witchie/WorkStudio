using Newtonsoft.Json;
using System;

namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// NUGET工具列表
    /// </summary>
    public class NugetTool
    {
        /// <summary>
        /// 版本信息
        /// </summary>
        [JsonProperty("version")] 
        public string Version { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        [JsonProperty("url")] 
        public string DownLoadUrl { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        [JsonProperty("stage")] 
        public Stage Stage { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        [JsonProperty("uploaded")] 
        public DateTime UploadDateTime { get; set; }
        public override string ToString()
        {
            return $"{Version}\t{Stage}\t{UploadDateTime}\t{DownLoadUrl}";
        }
    }
}
