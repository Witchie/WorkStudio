using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace GreenWhale.Extensions.TestTools2.Models
{
    /// <summary>
    /// 泛型表达式存储器
    /// </summary>
    /// <typeparam name="Tin"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public static class GenericExpressionMapper<Tin, TOut>
    {

        private static Func<Tin, TOut> func = null;

        static GenericExpressionMapper()

        {

            var parameter = Expression.Parameter(typeof(Tin), "p");

            List<MemberBinding> memberBindings = new List<MemberBinding>();

            Copy(typeof(TOut).GetProperties(), memberBindings, parameter);

            var init = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindings.ToArray());

            var lambda = Expression.Lambda<Func<Tin, TOut>>(init, parameter);

            func = lambda.Compile();

        }

        /// <summary>

        /// 深度复制

        /// </summary>

        /// <param name="tin"></param>

        /// <returns></returns>

        public static TOut Clone(Tin tin) => func.Invoke(tin);

        /// <summary>

        /// 复制信息

        /// </summary>

        /// <typeparam name="Tin"></typeparam>

        /// <param name="memberInfos">对象成员列表</param>

        /// <param name="memberBindings">成员绑定表</param>

        /// <param name="parameter">表达式参数</param>

        private static void Copy(IEnumerable<MemberInfo> memberInfos, List<MemberBinding> memberBindings, ParameterExpression parameter)

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
