using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace GreenWhale.Extensions.TestTools2.Extensions
{
    /// <summary>
    /// 字节处理扩展
    /// </summary>
    public static class BytesExtension
    {
        /// <summary>
        /// 转换HEX字符串
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        public static string ToHex(this byte[] vs)
        {
            if (vs is null)
            {
                return string.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in vs)
            {
                stringBuilder.Append(Convert.ToString(item, 16).PadLeft(2,'0'));
            }
            return stringBuilder.ToString().ToUpper();
        }
        
        /// <summary>
        /// 转换为字节集合
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] ToHex(this string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    return Array.Empty<byte>();
                }
                var match = Regex.Match(content, "[0-9a-fA-F]{0,}");
                content = match.Value;
                List<byte> vs = new List<byte>();

                for (int i = 0; i < content.Length; i += 2)
                {
                    vs.Add(Convert.ToByte(content.Substring(i, 2), 16));
                }
                return vs.ToArray();
            }
            catch (Exception)
            {
                return Array.Empty<byte>();
            }

        }
    }
}
