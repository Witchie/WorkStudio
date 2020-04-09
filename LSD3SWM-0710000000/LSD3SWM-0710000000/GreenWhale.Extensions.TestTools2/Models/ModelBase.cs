using DevExpress.Mvvm;
using GreenWhale.RunTime.Scripts;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace GreenWhale.Extensions.TestTools2.Models
{
    /// <summary>
    /// 模型基础父类
    /// </summary>
    public class ModelBase:ViewModelBase
    {
        /// <summary>
        /// 是否在执行
        /// </summary>
        [XmlIgnore]
        public bool IsExecute { get; set; } = false;
        /// <summary>
        /// 代码的内容信息
        /// </summary>
        [XmlElement]
        public string FileContent { get; set; }
        public override string ToString()
        {
            return FileContent??string.Empty;
        }
        /// <summary>
        /// 运行时
        /// </summary>
        [XmlIgnore]
        public RuningCore RuningCore { get;private set; }
        /// <summary>
        /// 装载代码
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public async Task<RuningCore> LoadCode(IServiceProvider serviceProvider)
        {
            RuningCore= await Task.Run(() => {
                var core = serviceProvider.GetService<ScriptRunner>().LoadCode<RuningCore>(FileContent);
                return core;
            });
            return RuningCore;
        }
        /// <summary>
        /// 装载文件到内存
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task LoadFile(string fileName)
        {
            var text=await  File.ReadAllTextAsync(fileName);
            FileContent = text;
        }
        public void LoadText(string filecontent)
        {
            FileContent = filecontent;
        }
    }
}
