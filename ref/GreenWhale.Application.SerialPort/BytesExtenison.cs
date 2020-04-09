using System;

namespace GreenWhale.Application.SerialPorts
{
    public static class BytesExtenison
    {
        /// <summary>
        /// 转换为字节码
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        public static string ToArray(this byte[] vs)
        {
           return BitConverter.ToString(vs).Replace("-",string.Empty);
        }
    }
}
