using GreenWhale.BootLoader.Implements;
using GreenWhale.Extensions.TestTools2.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
namespace GreenWhale.Extensions.TestTools2.Views
{
    /// <summary>
    /// 数据存储
    /// </summary>
    public class DataStore : IDataStore
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IExportBoxService exportBoxService;
        public DataStore(ApplicationDbContext applicationDbContext, IExportBoxService exportBoxService)
        {
            this.applicationDbContext = applicationDbContext;
            this.exportBoxService = exportBoxService;
        }

        public async Task<DataTable> Get(DateTime dtStart, DateTime dtEnd)
        {
            if (dtStart > dtEnd)
            {
                return new DataTable();
            }
            var works =await applicationDbContext.Works.Where(p => p.DateTime >= dtStart && p.DateTime <= dtEnd).ToArrayAsync();
            if (works.Count() == 0)
            {
                return new DataTable();
            }
            var dictionary= works.GroupBy(p => p.DeviceId).ToDictionary(p => p.Key, v => v.OrderBy(p => p.TestIndex).ToArray());
            if (dictionary.Count==0)
            {
                return new DataTable();
            }
            var columns = dictionary.OrderByDescending(p=>p.Value.Count()).FirstOrDefault().Value.Select(p =>new {p.TestIndex,p.WorkName }).OrderBy(p => p.TestIndex).Select(p=>p.WorkName).Distinct().ToArray();
            var dataColumns=   columns.Select(p => new DataColumn(p)).ToArray();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("设备ID");
            dataTable.Columns.Add("生产时间");
            dataTable.Columns.Add("全部通过");
            dataTable.Columns.AddRange(dataColumns);
            foreach (var item in dictionary)
            {
                var rows = item.Value;
                var row=   dataTable.NewRow();
                row.ItemArray = rows.ToRows(item.Key,rows.FirstOrDefault().DateTime.ToLongDateString(),rows.Any(p=>p.State!="通过")? "异常" : "通过");
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        public async Task Post(SerialPortWork[] serialPortWork, string devcieId)
        {
            try
            {
                if (serialPortWork is null)
                {
                    throw new ArgumentNullException(nameof(serialPortWork));
                }

                if (string.IsNullOrWhiteSpace(devcieId))
                {
                    throw new ArgumentException("message", nameof(devcieId));
                }

                var works = serialPortWork.Select(p => new Works() { DeviceId = devcieId, DateTime = DateTime.Now, State = p.State, WorkName = p.WorkName, TestIndex = p.TestIndex, TestResult = p.TestResult,ID=0 });
                var devcies=await   applicationDbContext.Works.Where(p => p.DeviceId == devcieId).ToArrayAsync();
                applicationDbContext.RemoveRange(devcies);
                await applicationDbContext.AddRangeAsync(works);
                await applicationDbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                exportBoxService.Log(err.ToString());
            }

        }

        public async Task<bool> Delete(string deviceId)
        {
            var devcies = await applicationDbContext.Works.Where(p => p.DeviceId == deviceId).ToArrayAsync();
            applicationDbContext.RemoveRange(devcies);
            var gg= await applicationDbContext.SaveChangesAsync();
            if (gg > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
