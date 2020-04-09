namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Services
{
    public interface ISerialPortViewServcie
    {
        /// <summary>
        /// 缓冲大小
        /// </summary>
        int BufferSize { get; set; }
        /// <summary>
        /// 清空缓冲
        /// </summary>
        void ClearBuffer();
    }
}