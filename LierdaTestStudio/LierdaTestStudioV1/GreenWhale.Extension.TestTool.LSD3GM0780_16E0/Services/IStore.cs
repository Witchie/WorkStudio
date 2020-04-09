using System;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.IO.Ports;
using System.Collections.Generic;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Views;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Services
{
    public interface IStore
    {
        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="enumable"></param>
         void Save(IEnumerable<ResourceDefine<ValueModel, ValueModel>> enumable);
        /// <summary>
        /// 读取配置信息
        /// </summary>
         IEnumerable<ResourceDefine<ValueModel, ValueModel>> Read();
    }
}
