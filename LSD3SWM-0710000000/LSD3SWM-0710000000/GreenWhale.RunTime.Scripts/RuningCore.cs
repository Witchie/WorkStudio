using System.Collections;
using System.Text;
using System.Linq;
namespace GreenWhale.RunTime.Scripts
{
    /// <summary>
    /// 运行服务总线
    /// </summary>
    public abstract class RuningCore: RunningBase
    {
        /// <summary>
        /// 转换为ASCII
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected string ToASCII(byte[] content)
        {
            return  Encoding.ASCII.GetString(content);
        }
        public byte[] Take(byte[] source,int startIndex, int Length)
        {
            return source.Skip(startIndex).Take(Length).ToArray() ;
        }
    }
}
