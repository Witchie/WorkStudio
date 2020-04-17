using Newtonsoft.Json;
namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// NUGET包列表
    /// </summary>
    public class NugetPackets
    {
        /// <summary>
        /// 总数
        /// </summary>
        [JsonProperty("totalHits")]
        public int TotalHits { get; set; }
        /// <summary>
        /// 包列表
        /// </summary>
        [JsonProperty("data")]
        public NugetPacket[] Data { get; set; }
    }
}
