using System;
using System.Collections.Generic;
using System.Linq;
namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 互转对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayObject<T> : List<T> where T : class
    {
        public ArrayObject()
        {
        }

        public ArrayObject(params T[] collection) : base(collection)
        {
        }

        public ArrayObject(int capacity) : base(capacity)
        {
        }
        /// <summary>
        /// 是否允许空
        /// </summary>
        protected virtual bool IsAllowNone { get;} = true;
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="test"></param>
        public static implicit operator string(ArrayObject<T> test)
        {
            if (test.Count==0)
            {
                if (test.IsAllowNone)
                {
                    return "无";
                }
            }
            List<T> content = test;
            return string.Join(Environment.NewLine, content);
        }
        /// <summary>
        /// 转换为测试对象
        /// </summary>
        /// <param name="content"></param>
        public static explicit operator ArrayObject<T>(string content)
        {
            var text = content.Split(Environment.NewLine);
            var test = new ArrayObject<T>();
            foreach (var item in text)
            {
                test.Add(item as T);
            }
            return test;
        }
        public override string ToString()
        {
            if (this.Count==0)
            {
                return string.Empty;
            }
            return string.Join('、', this.ToList()) + "。";
        }
    }

}
