using System.Collections.ObjectModel;

namespace GreenWhale.RunTime.Scripts
{
    /// <summary>
    /// 运行结果
    /// </summary>
    public class RunningResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="state"></param>
        /// <param name="content"></param>
        public RunningResult(State state, Collection<string> content)
        {
            State = state;
            Content = content;
        }
        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="state"></param>
        /// <param name="content"></param>
        public RunningResult(State state, params string[] content)
        {
            State = state;
            Content = new Collection<string>(content);
        }
        /// <summary>
        /// 执行状态
        /// </summary>
        public State State { get;  set; }
        /// <summary>
        /// 返回的内容
        /// </summary>
        public Collection<string> Content { get;private set; }
        public override string ToString()
        {
            return $"{State}:{string.Join(";", Content)}";
        }

    }
}
