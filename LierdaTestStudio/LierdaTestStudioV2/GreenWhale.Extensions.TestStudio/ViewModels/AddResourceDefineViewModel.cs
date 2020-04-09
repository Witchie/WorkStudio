using DevExpress.Mvvm;
using GreenWhale.Extensions.TestTools2.Models;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System.Xml.Serialization;

namespace GreenWhale.Extensions.TestTools2.ViewModels
{
    /// <summary>
    /// 添加资源视图模型
    /// </summary>
    [ExcelExporter(Name = "协议详细信息")]
    public class AddResourceDefineViewModel : ViewModelBase
    {
        private int testIndex;
        private string description;
        private string sendContent;
        private string receiveContent;
        private ModelBase scriptModel = new ModelBase();
        private int taskDeplay=1000;

        /// <summary>
        /// 测试序号
        /// </summary>
        [XmlElement]
        [ExporterHeader("测试序号")]
        public int TestIndex
        {
            get => testIndex; set
            {
                testIndex = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 描述信息
        /// </summary>
        [XmlElement]
        [ExporterHeader("描述信息")]
        public string Description
        {
            get => description; set
            {
                description = value; RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 要发送的数据
        /// </summary>
        [XmlElement]
        [ExporterHeader("要发送的数据",IsAutoFit =true)]
        public string SendContent
        {
            get => sendContent; set
            {
                sendContent = value; RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 接收到的数据
        /// </summary>
        [XmlElement]
        [ExporterHeader("接收到的数据", IsAutoFit = true)]
        public string ReceiveContent
        {
            get => receiveContent; set
            {
                receiveContent = value; RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 是否手动判定结果
        /// </summary>
        [XmlAttribute]
        [ExporterHeader("是否手动判定")]
        public bool IsManulCheck { get; set; } = false;
        /// <summary>
        /// 任务延时
        /// </summary>
        [XmlAttribute]
        [ExporterHeader("接收到的数据")]
        public int TaskDeplay
        {
            get => taskDeplay; set
            {
                taskDeplay = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 是否普通任务
        /// </summary>
        [XmlAttribute]
        [ExporterHeader("阻断任务")]
        public bool IsAbnormalWork { get; set; }
        /// <summary>
        /// 阻断提醒
        /// </summary>
        [XmlAttribute]
        [ExporterHeader("阻断提醒")]
        public string AbnormalWorkTips { get; set; }
        [XmlElement]
        [ExporterHeader("脚本内容")]
        public ModelBase ScriptModel
        {
            get => scriptModel; set
            {
                scriptModel = value; RaisePropertyChanged();
            }
        }
    }

}
