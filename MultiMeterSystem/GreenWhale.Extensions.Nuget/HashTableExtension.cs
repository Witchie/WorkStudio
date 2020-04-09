using System.Collections;
using System.Collections.Generic;
namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// 哈希表扩展
    /// </summary>
    public static class HashTableExtension
    {
        /// <summary>
        /// 转换为请求参数
        /// </summary>
        /// <param name="hashtable"></param>
        /// <returns></returns>
        public static string ToParameter(this Hashtable hashtable,bool addparamter=true)
        {
            List<string> vs = new List<string>();
            foreach (var item in hashtable.Keys)
            {
                vs.Add($"{item}={hashtable[item]}");
            }
            var content= string.Join("&",vs);
            if (addparamter)
            {
                return "?" + content;
            }
            else
            {
                return content;
            }
        }
    }
}
