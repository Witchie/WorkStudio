using GreenWhale.Application.SerialPorts;
using System.Data;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using GreenWhale.BootLoader.Implements;
using System;
using System.Threading.Tasks;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    /// <summary>
    /// 写数据
    /// </summary>
    public class LSD3GM0780_16E0Produce
    {
        /// <summary>
        /// 串口上下文
        /// </summary>
        private readonly ISerialPortContext serialPort;
        private readonly IExportBoxService exportBoxService;
            /// <summary>
            /// 数据表缓冲
            /// </summary>
        public DataTableCache DataTableCache { get; private set; }

        public LSD3GM0780_16E0Produce(ISerialPortContext serialPort, DataTableCache dataTableCache, IExportBoxService exportBoxService)
        {
            this.serialPort = serialPort;
            this.DataTableCache = dataTableCache;
            this.exportBoxService = exportBoxService;
        }
        /// <summary>
        /// 干簧管测试
        /// </summary>
        /// <param name="meterid"></param>
        public Task<bool> OnStart1(string meterid)
        {
            exportBoxService.LogNewLine();
            exportBoxService.Log($"当前表号:{meterid}");
            exportBoxService.Log(ConstHelper.BeginTestStart1);
            exportBoxService.Log(serialPort.Write(ConstHelper.Start1).Flush());
            return Task.FromResult(true);
        }
        /// <summary>
        /// 光电测试
        /// </summary>
        /// <param name="meterid"></param>
        internal Task<bool> OnStart2(string meterid)
        {
            exportBoxService.LogNewLine();
            exportBoxService.Log($"当前表号:{meterid}");
            exportBoxService.Log(ConstHelper.BeginTestStart2);
            exportBoxService.Log(serialPort.Write(ConstHelper.Start2).Flush());
            return Task.FromResult(true);
        }
    }
}
