using System.Data;
namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 可分割
    /// </summary>
    public interface ISplit
    {
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <returns></returns>
        string[] Split();
    }
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ArrayObjectExtension
    {
        /// <summary>
        /// 转换为数据表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this ArrayObject<T> content, bool index = false, string tableName=null) where T: class, ISplit
        {
            DataTable dataTable = tableName == null ? new DataTable() : new DataTable(tableName);
            if (index)
            {
                dataTable.Columns.Add("序号", typeof(int));
            }
            int num = 1;
            foreach (var item in content)
            {
                var row = dataTable.NewRow();
                var contents = item.Split();

                if (index)
                {

                    row[0] = num++;
                    for (int i = 0; i < contents.Length; i++)
                    {
                        if (dataTable.Columns.Count != contents.Length + 1)
                        {
                            dataTable.Columns.Add(i.ToString());
                        }
                        row[i+1] = contents[i];
                    }
                }
                else
                {
                    for (int i = 0; i < contents.Length; i++)
                    {
                        if (dataTable.Columns.Count != contents.Length)
                        {
                            dataTable.Columns.Add(i.ToString());
                        }
                        row[i] = contents[i];
                    }
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
        /// <summary>
        /// 转换为数据表
        /// </summary>
        /// <param name="content"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this ArrayObject<string> content, bool index = false, string tableName = null) 
        {
            DataTable dataTable = tableName == null ? new DataTable() : new DataTable(tableName);
            if (index)
            {
                dataTable.Columns.Add("序号",typeof(int));
            }
            dataTable.Columns.Add("内容");
            int num = 1;
            foreach (var item in content)
            {
                var row= dataTable.NewRow();
                if (index)
                {
                    row[0] = num++;
                    row[1] = item;
                }
                else
                {
                    row[0] = item;
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }

}
