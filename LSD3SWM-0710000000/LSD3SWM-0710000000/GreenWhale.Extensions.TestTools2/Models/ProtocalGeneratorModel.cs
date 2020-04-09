using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Linq;
namespace GreenWhale.Extensions.TestTools2.Models
{
    /// <summary>
    /// 协议生成模型
    /// </summary>
    public class ProtocalGeneratorModel : ViewModelBase
    {
        private byte meterType;
        private ProtocalDirection protocalDirection;
        private byte controlCode;
        private byte[] dataArea;

        /// <summary>
        /// 包头
        /// </summary>
        public byte Header => 0xAA;
        /// <summary>
        /// 表类型
        /// </summary>
        public byte MeterType
        {
            get => meterType; set
            {
                meterType = value;
                RaisePropertiesChanged(nameof(MeterType), nameof(Crc));
            }
        }
        /// <summary>
        /// 通信方向
        /// </summary>
        public ProtocalDirection ProtocalDirection
        {
            get => protocalDirection; set
            {
                protocalDirection = value;
                RaisePropertiesChanged(nameof(ProtocalDirection), nameof(Crc));
            }
        }
        /// <summary>
        /// 功能码
        /// </summary>
        public byte ControlCode
        {
            get => controlCode; set
            {
                controlCode = value;
                RaisePropertiesChanged(nameof(ControlCode), nameof(Crc));
            }
        }
        /// <summary>
        /// 数据包长度
        /// </summary>
        public byte Length => (byte)(DataArea?.Length ?? 0);
        /// <summary>
        /// 数据域
        /// </summary>
        public byte[] DataArea
        {
            get => dataArea; set
            {
                dataArea = value;
                RaisePropertiesChanged(nameof(DataArea),nameof(Length),nameof(Crc));
            }
        }
        /// <summary>
        /// 校验
        /// </summary>
        public byte Crc
        {
            get
            {
                List<byte> vs = new List<byte>();
                vs.Add(MeterType);
                vs.Add((byte)ProtocalDirection);
                vs.Add(ControlCode);
                vs.Add(Length);
                if (DataArea != null)
                {
                    vs.AddRange(DataArea);
                }
                return(byte) vs.Sum(p => p);
            }
        }
        /// <summary>
        /// 包尾
        /// </summary>
        public byte Footer => 0x55;

    }
}
