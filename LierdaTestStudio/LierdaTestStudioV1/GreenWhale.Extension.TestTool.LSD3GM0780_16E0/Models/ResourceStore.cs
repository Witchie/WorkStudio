using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    /// <summary>
    /// 资源字典
    /// </summary>
    public class ResourceStore: ObservableCollection<ResourceDefine<ValueModel, ValueModel>>
    {
        public ResourceStore()
        {
           
        }
        /// <summary>
        /// 读取定义
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public ResourceDefine<ValueModel, ValueModel> Get(int num)
        {
            var dd = this.Where(predicate => predicate.OnSuccess.Number == num).FirstOrDefault();
            if (dd!=null)
            {
                return dd;
            }
            else
            {
                return null;
            }
        }

    }
}
