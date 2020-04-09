using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    public static class LSD3GMDataContextExtension
    {
        /// <summary>
        /// 获取单个表的所有测试用例
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="meterid"></param>
        /// <returns></returns>
        public static TestCategoryList GetTestCategory(this ILSD3GMDataContext dataContext,string meterid)
        {
            var data = from test in dataContext.TestCategorys
                       where test.MeterID == meterid
                       group test by test.MeterID
                       into meter
                       select new TestCategoryList
                       {
                           MeterID = meter.Key,
                           DateTime = meter.OrderByDescending(p => p.DateTime).Select(p => p.DateTime).FirstOrDefault(),
                           ResourceState = meter.All(s => s.ResourceState == ResourceState.S) == true ? ResourceState.S : ResourceState.E,
                           TestCategory = meter.ToArray()
                     };

            return data.FirstOrDefault();
        }
        /// <summary>
        /// 获取测试用例
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="dateTimeStart"></param>
        /// <param name="dateTimeEnd"></param>
        /// <returns></returns>
        public static TestCategory [] GetTestCategorys(this ILSD3GMDataContext dataContext, DateTime dateTimeStart,DateTime dateTimeEnd)
        {
            var dd = (from test in dataContext.TestCategorys
                      where test.DateTime >= dateTimeStart && test.DateTime < dateTimeEnd select test).ToArray(); 

            return dd;
        }

        /// <summary>
        /// 获取测试结果
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="dateTimeStart"></param>
        /// <param name="dateTimeEnd"></param>
        /// <returns></returns>
        public static Task<TestCategory[]> GetTestCategorysAsync(this ILSD3GMDataContext dataContext, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            return  Task.Run(() =>  
            {
               return dataContext.GetTestCategorys(dateTimeStart,dateTimeEnd);
            });
        }
        /// <summary>
        /// 判定某个表是否存在
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="meterid"></param>
        /// <returns></returns>
        public static bool IsExist(this ILSD3GMDataContext dataContext, string meterid)
        {
           return  dataContext.TestCategorys.Where(s => s.MeterID == meterid).FirstOrDefault() != null;
        }
        /// <summary>
        /// 删除表具操作记录
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="meterid"></param>
        public static void DeleteMeterLog(this ILSD3GMDataContext dataContext, string meterid)
        {
            var test=   dataContext.TestCategorys.Where(p => p.MeterID == meterid).ToArray();
            dataContext.RemoveRange(test);
            dataContext.SaveChanges();
        }
        /// <summary>
        /// 保存测试用例信息
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="dataTable"></param>
        public static string SaveTestCategory(this ILSD3GMDataContext dataContext, DataTable dataTable)
        {
            var count = dataContext.TestCategorys.Count();
            var first=  dataTable.AsEnumerable().FirstOrDefault();
            var meterid = first[ResourceStoreExtension.columeValue]?.ToString();
            var tables=   dataTable.AsEnumerable().Select(s =>
            {
                return new TestCategory
                {
                    DateTime = DateTime.Now,
                    FramID = Convert.ToInt32( s[ResourceStoreExtension.columeNumber]?.ToString()),
                    ID = 0,
                    MeterID = meterid,
                    Name = s[ResourceStoreExtension.columeName]?.ToString(),
                    ResourceState = s[ResourceStoreExtension.columeState]?.ToString().ToEnum<ResourceState>()??ResourceState.E,
                    TestValue=s[ResourceStoreExtension.columeValue]?.ToString(),
                };
            });
            dataContext.DeleteMeterLog(meterid);
            dataContext.TestCategorys.AddRange(tables);
            dataContext.SaveChanges();
            return meterid;
        }
    }
}