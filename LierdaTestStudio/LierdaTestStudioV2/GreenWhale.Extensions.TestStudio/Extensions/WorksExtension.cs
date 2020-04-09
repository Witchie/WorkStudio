using GreenWhale.Application.SerialPorts;
using System.Collections.Generic;
using GreenWhale.Extensions.TestTools2.Views;
using System.Linq;

namespace GreenWhale.Extensions.TestTools2.Extensions
{
    public static class WorksExtension
    {
        /// <summary>
        /// 转换成行
        /// </summary>
        /// <param name="works"></param>
        /// <returns></returns>
        public static WorkResult[] ToRows(this Works[] works,params string[] attact)
        {
            List<string> vs = new List<string>();
            var result = works.OrderBy(p => p.TestIndex).Select(p => new WorkResult { State = p.State, WorkName = p.WorkName, TestResult = p.TestResult });
            List<WorkResult> res = new List<WorkResult>();
            res.AddRange(attact.Select(p => new WorkResult { IsDefineValue = true, State = p }));
            res.AddRange(result);
            return res.ToArray();
        }
    }
}
