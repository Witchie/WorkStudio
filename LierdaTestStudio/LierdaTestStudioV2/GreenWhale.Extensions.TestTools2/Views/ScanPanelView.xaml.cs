using GreenWhale.Extensions.TestTools2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Threading.Tasks;
using GreenWhale.RunTime.Scripts;
using GreenWhale.Extensions.TestTools2.Extensions;
using GreenWhale.BootLoader.Implements;
using GreenWhale.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using GreenWhale.Extensions.TestTools2.ViewModels;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GreenWhale.Extensions.TestTools2.Views
{

    /// <summary>
    /// ScanPanelView.xaml 的交互逻辑
    /// </summary>
    public partial class ScanPanelView : UserControl,INotifyPropertyChanged, IMessageTip
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IExportBoxService exportBoxService;
        private readonly IDataStore dataStore;
        private readonly IEventBus eventBus;
        private readonly IMessageBox messageBox;
        private ObservableCollection<SerialPortWork> serialPortWorks;

        public SerialPortServiceBase serialPortContext { get; private set; }
        public ScanPanelView(IProjectViewServcie projectViewServcie, SerialPortServiceBase serialPortContext, IServiceProvider serviceProvider, IExportBoxService exportBoxService, IDataStore dataStore, IEventBus eventBus, IMessageBox messageBox)
        {
            InitializeComponent();
            this.ProjectViewServcie = projectViewServcie;
            this.serialPortContext = serialPortContext;
            this.serviceProvider = serviceProvider;
            this.exportBoxService = exportBoxService;
            this.DataContext = this;
            this.dataStore = dataStore;
            this.eventBus = eventBus;
            this.messageBox = messageBox;
            dispatcher.Interval = TimeSpan.FromSeconds(2);
            dispatcher.Tick += Dispatcher_Tick;
            dispatcher.Start();
        }

        private void Dispatcher_Tick(object sender, EventArgs e)
        {
                meterid.Focus();
        }

        DispatcherTimer dispatcher = new DispatcherTimer();
        private async void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var text = meterid.Text.Trim();
                Reset();
                meterid.Clear();
                IsBusy = true;

                Stopwatch = Stopwatch.StartNew();
                await Start(text);
                Stopwatch.Stop();
                var time = Stopwatch.ElapsedMilliseconds;
                exportBoxService.Log($"耗时:{TimeSpan.FromMilliseconds(time)}");

                IsBusy = false;
            }
        }
        public Stopwatch Stopwatch { get; private set; } = new Stopwatch();
        private async Task Start(string text)
        {
            IsCancel = false;
            if (string.IsNullOrEmpty(text))
            {
                exportBoxService.Log($"设备ID号码不得为空");
                Reset();
                return;
            }
            if (SerialPortWorks==null||SerialPortWorks?.Count == 0)
            {
                exportBoxService.Log($"请先加载任务,当前任务为空");
                Reset();
                return;
            }
            if (!serialPortContext.IsOpen)
            {
                exportBoxService.Log($"请先开启串口");
                Reset();
                return;
            }
            foreach (var item in SerialPortWorks)
            {
            AGAIN:
                var exception = await item.WorkContent?.Invoke();
                item.State = (exception.Result?.State ?? State.FrameValidateFailed).Description();
                if (exception.IsError == true)
                {
                    if (!IsCancel)
                    {
                        if (messageBox.Show($"出现错误，是否重试上一次的任务：{exception.ErrorMessage}", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            goto AGAIN;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    if (exception.Result != null)
                    {
                        if (exception.Result.State == State.None)
                        {
                            exportBoxService.Log($"解析脚本返回未知！任务名称：{exception.AddResourceDefineViewModel.Description}");

                            if (messageBox.Show($"{exception.ErrorMessage}", "解析出错!是否重试上一次的任务?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                goto AGAIN;
                            }
                            else
                            {
                                exportBoxService.Log($"因解析脚本返回未知状态任务被强制停止");
                                return;
                            }
                        }
                        else if (exception.Result.State == State.Unqualified)
                        {
                            exportBoxService.Log($"测试不通过！任务名称：{exception.AddResourceDefineViewModel.Description}");
                            await TestStop(text);
                            break;
                        }
                    }
                }
            }
            await TestComplete(text);

        }
        private async Task TestComplete(string deviceId)
        {
            exportBoxService.Log($"测试任务完成");
            var data = SerialPortWorks.ToArray();

            await dataStore.Post(data, deviceId);
            eventBus.Notify("factory.roduce.view.update", data);
            IsBusy = false;
            IsCancel = true;

        }
        private  Task TestStop(string deviceId)
        {
            exportBoxService.Log($"测试任务被迫停止");
            IsBusy = false;
            IsCancel = true;
            return Task.CompletedTask;
        }
        public bool IsBusy
        {
            get => psb.IsVisible; set
            {
                psb.Dispatcher.Invoke(() =>
                {
                    psb.Visibility = value == true ? Visibility.Visible : Visibility.Collapsed;
                });
            }
        }

        public IProjectViewServcie ProjectViewServcie { get; }
        public bool IsCancel { get; set; } = false;

        /// <summary>
        /// 重置窗体
        /// </summary>
        private  void Reset()
        {
            this.Dispatcher.Invoke(() =>
            {
                IsCancel = true;
                meterid.Clear();
                IsBusy = false;
                meterid.Focus();
                if (ProjectViewModel != null)
                {
                    ReGenerator(ProjectViewModel);
                }
                CloseTip();
            });

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        public ObservableCollection<SerialPortWork> SerialPortWorks
        {
            get => serialPortWorks; private set
            {
                serialPortWorks = value;
                Changed();
            }
        }
        public ProjectViewModel ProjectViewModel { get; set; }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ProjectViewServcie.ReadModelDialog(s =>
            {
                ProjectViewModel = s.Clone();
                ReGenerator(s);
            });
        }
        private void ReGenerator(ProjectViewModel projectViewModel)
        {
            SerialPortWorks = new ObservableCollection<SerialPortWork>();
            var works = projectViewModel.ResourceDefineViewModels.OrderBy(p => p.TestIndex).ToArray();
            foreach (var item in works)
            {
                Func<Task<RunningExceptionDetail>> task = new Func<Task<RunningExceptionDetail>>(async () =>
                {
                    if (!item.IsManulCheck)
                    {
                        try
                        {
                            if (IsCancel)
                            {
                                return new RunningExceptionDetail() { IsError = true };
                            }
                            var data = item;
                            exportBoxService.Log($"TX\t{data.SendContent}");
                            if (data.IsAbnormalWork)
                            {
                                ShowTip(data.AbnormalWorkTips);
                            }
                            var respond = await serialPortContext.Request(data.SendContent.ToHex(), item.TaskDeplay, async (source) =>
                            {
                                try
                                {
                                    if (source.Length == 0)
                                    {
                                        return new PassModel(null, false);
                                    }
                                    await data.ScriptModel.LoadCode(serviceProvider);
                                    var result = data.ScriptModel.RuningCore.Run(InvokeContext.Run(serviceProvider, source, new Dictionary<string, string>
                                    {
                                        {InvokeContext.FrameValudate,InvokeContext.True }
                                    }));
                                    if (result?.Result?.State == State.FrameValidatePassed)
                                    {
                                        return new PassModel(null, true);
                                    }
                                    return new PassModel(null, false);
                                }
                                catch (Exception)
                                {
                                    return new PassModel(null, false);
                                }

                            }, 5);
                            if (respond == null || respond?.Length == 0)
                            {
                                messageBox.Show("设备无应答，测试被迫停止");
                                return new RunningExceptionDetail() { IsError = true, AddResourceDefineViewModel = item };
                            }
                            exportBoxService.Log($"RX\t{respond.ToHex()}");
                            await data.ScriptModel.LoadCode(serviceProvider);
                            RunningException exp = null;
                            if (data.IsAbnormalWork)
                            {
                                exp =  data.ScriptModel.RuningCore.Run(InvokeContext.Run(serviceProvider, respond, new Dictionary<string, string> { { "IsAbnormalWork", InvokeContext.True } }));
                            }
                            else
                            {
                                exp =  data.ScriptModel.RuningCore.Run(InvokeContext.Run(serviceProvider, respond, new Dictionary<string, string> { { "IsAbnormalWork", InvokeContext.False } }));
                            }
                            var detail = exp.Clone<RunningException, RunningExceptionDetail>();
                            detail.AddResourceDefineViewModel = item;
                            CloseTip();
                            return detail;
                        }
                        catch (Exception err)
                        {
                            return new RunningExceptionDetail() { IsError = true, ErrorMessage = err, AddResourceDefineViewModel = item };
                        }

                    }
                    else
                    {
                        var res = messageBox.Show($"{item.Description}是否通过?回车默认通过", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                        var result = res == MessageBoxResult.Yes ? State.Qualified : State.Unqualified;
                        return new RunningExceptionDetail() { IsError = false, AddResourceDefineViewModel = item, Result = new RunningResult(result) };

                    }
                });
                var work = new SerialPortWork(task, item.Description, State.None.Description(), item.TestIndex);
                SerialPortWorks.Add(work);
            }
            this.DataContext = this;
        }
        private void Changed([CallerMemberName] string name=null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public async void ShowTip(string content,TimeSpan? timeSpan=null)
        {
           await this.Dispatcher.InvokeAsync(async () => {
                tip.Visibility = Visibility.Visible;
                tip.Text = content;
                await Task.Delay(timeSpan ?? TimeSpan.FromSeconds(10));
                CloseTip();
            });

        }

        public async void CloseTip()
        {
            await this.Dispatcher.InvokeAsync(() => {
                tip.Visibility = Visibility.Collapsed;
            });
        }
    }
    /// <summary>
    /// 消息提示框
    /// </summary>
    public interface IMessageTip
    {
        /// <summary>
        /// 显示提示
        /// </summary>
        /// <param name="content"></param>
        void ShowTip(string content, TimeSpan? timeSpan=null);
        /// <summary>
        /// 关闭提示
        /// </summary>
        void CloseTip();
    }
}
