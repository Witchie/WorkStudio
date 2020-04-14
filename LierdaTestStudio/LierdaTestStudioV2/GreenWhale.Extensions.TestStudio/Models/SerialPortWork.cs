using System;
using System.Threading.Tasks;
using GreenWhale.RunTime.Scripts;
using DevExpress.Mvvm;

namespace GreenWhale.Extensions.TestTools2.Views
{
    /// <summary>
    /// 串口任务
    /// </summary>
    public class SerialPortWork : ViewModelBase
    {
        private string workName;
        private string state;
        private Func<Task<RunningExceptionDetail>> workContent;
        private int testIndex;
        private string testResult;

        public SerialPortWork(Func<Task<RunningExceptionDetail>> workContent, string workName, string state, int testIndex)
        {
            WorkContent = workContent;
            WorkName = workName;
            State = state;
            TestIndex = testIndex;
        }
        public int TestIndex
        {
            get => testIndex; set
            {
                testIndex = value; RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 任务内容
        /// </summary>
        public Func<Task<RunningExceptionDetail>> WorkContent
        {
            get => workContent; set
            {
                workContent = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string WorkName
        {
            get => workName; set
            {
                workName = value; RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 任务状态
        /// </summary>
        public string State
        {
            get => state; set
            {
                state = value; RaisePropertyChanged();
            }
        }
        public string TestResult
        {
            get => testResult; set
            {
                testResult = value; RaisePropertyChanged();
            }
        }
    }
}
