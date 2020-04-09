using Newtonsoft.Json;

namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// NUGET下载列表
    /// </summary>
    public class NugetTools
    {
        [JsonProperty("nuget.exe")]
        public NugetTool[] Tools { get; set; }
        public static implicit operator NugetTool[](NugetTools nugetTools)
        {
            return nugetTools.Tools;
        }
    }
}
