using CSScriptLib;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevExpress.CodeParser.CSharp;
using DevExpress.CodeParser;
using DevExpress.CodeParser.CodeStyle;
using GreenWhale.RunTime.Scripts;

namespace GreenWhale.Extensions.TestTools2.Models
{
    public  class ScriptRunner
    {
        /// <summary>
        /// 加载代码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public T LoadCode<T>(string script,params object[] args) where T: RuningCore
        {
            if (string.IsNullOrEmpty(script))
            {
                throw new System.ArgumentException("message", nameof(script));
            }

            return   CSScript.Evaluator.LoadCode<T>(script, args);
        }
        /// <summary>
        /// 加载代码
        /// </summary>
        /// <param name="script"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public RuningCore LoadCode(string script, params object[] args) => LoadCode<RuningCore>(script,args);
        /// <summary>
        /// 从文件加载脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public T LoadFile<T>(string fileName,params object[] args) where T : RuningCore
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new System.ArgumentException("message", nameof(fileName));
            }

            return  CSScript.Evaluator.LoadFile<T>(fileName, args);
        }
        /// <summary>
        /// 从文件加载脚本
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public RuningCore LoadFile(string fileName, params object[] args) => LoadFile<RuningCore>(fileName, args);
        /// <summary>
        /// 装载方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodCode"></param>
        /// <returns></returns>
        public T LoadMethod<T>(string methodCode) where T: RunningResult
        {
            if (string.IsNullOrEmpty(methodCode))
            {
                throw new System.ArgumentException("message", nameof(methodCode));
            }

            return  CSScript.Evaluator.LoadMethod<T>(methodCode);
        }
        /// <summary>
        /// 装载方法
        /// </summary>
        /// <param name="methodCode"></param>
        /// <returns></returns>
        public RunningResult LoadMethod(string methodCode) => LoadMethod<RunningResult>(methodCode);
    }
}
