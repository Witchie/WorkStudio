using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    public static class ResourceStoreExtension
    {
        public static DataTable DataTable(this ResourceStore resourceDefines,Action<DataTable> onDataTable=null)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn(columeNumber), new DataColumn(columeName), new DataColumn(columeState), new DataColumn(columeValue) });
            onDataTable?.Invoke(dataTable);
            foreach (var item in resourceDefines)
            {
                var row = dataTable.NewRow();
                row[columeNumber] = item.Number;
                row[columeName] = item.Name;
                row[columeState] = ResourceState.N;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
        public  const string columeNumber = "序号";
        public const string columeName = "名称";
        public const string columeState = "状态";
        public const string columeValue = "值";
        public const string groupValue = "分组ID";
        public static DataTable ToDataTable(this TestCategory[] testCategories)
        {
           var dic=  testCategories.ToDictionary();
            var first = dic.FirstOrDefault();
            DataTable dt = new DataTable();

            if (first.Value==null)
            {
                return dt;
            }
           var names =first.Value.ColumnName().Select(p=>new DataColumn(p)).ToArray();
            dt.Columns.AddRange(names);
            foreach (var item in dic)
            {
                var rows= item.Value.ToDataRow();
                dt.Rows.Add(rows);
            }
            return dt;
        }
        public static TestResult[] ToDataRow(this TestCategory[] testCategories)
        {
            return testCategories.OrderBy(p=>p.FramID).Select(p => new TestResult {State=p.ResourceState,Value=p.TestValue }).ToArray();
        }
        /// <summary>
        /// 转换为数据表
        /// </summary>
        /// <param name="testCategories"></param>
        /// <returns></returns>
        public static Dictionary<string, TestCategory[]> ToDictionary(this TestCategory[] testCategories)
        {
            var data=  testCategories.GroupBy(p => p.MeterID).ToDictionary(s=>s.Key,s=>s.OrderBy(p=>p.FramID).ToArray());
            return data;
        }
        /// <summary>
        /// 获取列名称
        /// </summary>
        /// <param name="testCategories"></param>
        /// <returns></returns>
        public static IEnumerable<string> ColumnName(this TestCategory[] testCategories)
        {
            var names = testCategories.OrderBy(p=>p.FramID).Select(p=>p.Name);
            return names;
        }
    }
    public class TestResult
    {
        public ResourceState State { get; set; }
        public string Value { get; set; }
        public override string ToString()
        {
 return $"【{State}】{Value}";    
        }
    }
}
