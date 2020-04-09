using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Services;
using System.Collections.Generic;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    /// <summary>
    /// 数据库存储 
    /// </summary>
    public class DataBaseStore : IStore
    {
        public void Save(IEnumerable<ResourceDefine<ValueModel, ValueModel>> enumable)
        {

        }          

        public IEnumerable<ResourceDefine<ValueModel, ValueModel>> Read()
        {
            return null;
        }
    }
}
