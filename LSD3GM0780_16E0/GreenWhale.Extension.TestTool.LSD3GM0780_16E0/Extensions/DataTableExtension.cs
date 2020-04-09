using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataTableExtension
    {
        /// <summary>
        /// 转置Table
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="columnName">需要去重的行名</param>
        /// <returns></returns>
        public static DataColumn[] Rotate(this DataTable dataTable, int columnNameindex,params string[] columnName)
        {
            var rows = dataTable.GetRows(columnName).ToColumns(columnNameindex);
            return rows;
        }

        /// <summary>
        /// 获取所有列值
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="columnNameindex"></param>
        /// <returns></returns>
        public static object[] RoateValue(this DataTable dataTable)
        {
            List<object> list = new List<object>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow item = dataTable.Rows[i];
                var obj = item[3];
                var res = item[2]?.ToString();
                if (string.IsNullOrEmpty(res))
                {
                    throw new Exception("请先判定所有项");
                }
                if (i == 0)
                {
                    list.Add(obj);
                }
                else
                {
                    var state = res.ToEnum<ResourceState>();
                    list.Add($"【{state}】{obj}");
                }

            }
            return list.ToArray();
        }
        /// <summary>
        /// 获取所有列名
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static IEnumerable<string> ColumnNames(this DataTable dataTable)
        {
            foreach (DataColumn item in dataTable.Columns)
            {
               yield return item.ColumnName;    
            }
        }
        /// <summary>
        /// 获取不重复的列
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static DataRow[] GetRows(this DataTable dataTable, params string[] columnName)
        {
            var rows = dataTable.DefaultView.ToTable(true, columnName).AsEnumerable().ToArray();
            return rows;
        }
        /// <summary>
        /// 转换为行名称
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DataColumn ToColumn(this DataRow dataRow,int index)
        {
            DataColumn dataColumn = new DataColumn();
            dataColumn.ColumnName = dataRow[index].ToString();
            dataColumn.Caption = dataRow[index].ToString();
            dataColumn.DataType = typeof(string);
            dataColumn.DefaultValue = string.Empty;
            return dataColumn;
        }
        /// <summary>
        /// 转换列名称
        /// </summary>
        /// <param name="dataRows"></param>
        /// <returns></returns>
        public static DataColumn[] ToColumns(this DataRow[] dataRows,int index)
        {
            DataColumn[] dataColumns = new DataColumn[dataRows.Length];
            for (int i = 0; i < dataRows.Length; i++)
            {
                var row = dataRows[i];
                dataColumns[i] = row.ToColumn(index);
            }
            return dataColumns;
        }
    }
}
