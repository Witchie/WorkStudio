using System;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    public static class EnumExtension
    {
        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string content)
        {
            return  (T)Enum.Parse(typeof(T), content);
        }
    }
}
