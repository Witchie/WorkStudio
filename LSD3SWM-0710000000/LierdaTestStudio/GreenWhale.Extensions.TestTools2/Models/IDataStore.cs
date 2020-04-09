using System;
using System.Data;
using System.Threading.Tasks;
namespace GreenWhale.Extensions.TestTools2.Views
{
    public interface IDataStore
    {
        Task<DataTable> Get(DateTime dtStart, DateTime dtEnd);
        Task Post(SerialPortWork[] serialPortWork, string devcieId);
        /// <summary>
        /// 删除设备的记录
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task<bool> Delete(string deviceId);
    }
}
