using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace GreenWhale.Extensions.TestTools2.Models
{
    /// <summary>
    /// 克隆扩展
    /// </summary>
    public static class CloneExtension

    {

        private static Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
        /// <summary>
        /// 克隆复制当前对象
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TIn Clone<TIn>(this TIn data) => data.Clone<TIn, TIn>();
        /// <summary>
        /// 克隆服务对象
        /// </summary>
        /// <typeparam name="Tin"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="tin"></param>
        /// <returns></returns>
        public static TOut Clone<Tin, TOut>(this Tin tin)

        {

            var key = $"funckey_{typeof(Tin).FullName}_{typeof(TOut).FullName}";

            if (!keyValuePairs.ContainsKey(key))

            {

                var parameter = Expression.Parameter(typeof(Tin), "p");

                List<MemberBinding> memberBindings = new List<MemberBinding>();

                Copy<Tin>(typeof(TOut).GetProperties(), memberBindings, parameter);

                var init = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindings.ToArray());

                var lambda = Expression.Lambda<Func<Tin, TOut>>(init, parameter);

                keyValuePairs[key] = lambda.Compile();

            }

            return ((Func<Tin, TOut>)keyValuePairs[key]).Invoke(tin);

        }

        /// <summary>
        /// 复制信息
        /// </summary>
        /// <typeparam name="Tin"></typeparam>
        /// <param name="memberInfos">对象成员列表</param>
        /// <param name="memberBindings">成员绑定表</param>
        /// <param name="parameter">表达式参数</param>
        private static void Copy<Tin>(IEnumerable<MemberInfo> memberInfos, List<MemberBinding> memberBindings, ParameterExpression parameter)

        {

            var propertys = typeof(Tin);

            foreach (var item in memberInfos)

            {

                if (item is PropertyInfo property1 && property1.CanWrite == false)

                {

                    continue;

                }

                var gg = propertys.GetProperty(item.Name);

                if (gg == null)

                {

                    continue;

                }

                var property = Expression.Property(parameter, gg);

                var member = Expression.Bind(item, property);

                memberBindings.Add(member);
            }

        }

    }

}
