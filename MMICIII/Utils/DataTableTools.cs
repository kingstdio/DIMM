using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMICIII.Utils
{
    public class DataTableTools
    {

        #region Convert a List{T} to a DataTable.
        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        #endregion

        #region 将DataTable中数据写入到CSV文件中
        /// <summary>
        /// 将DataTable中数据写入到CSV文件中
        /// </summary>
        /// <param name="dt">提供保存数据的DataTable</param>
        /// <param name="fileName">CSV的文件路径</param>
        public static void SaveCSV(DataTable dt, string fileName)
        {
            if (dt == null)
            {
                return;
            }

            FileStream fs = new FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            string data = "";

            //写出列名称
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);

            //写出各行数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    data += dt.Rows[i][j].ToString();
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }

            sw.Close();
            fs.Close();
            //MessageBox.Show("CSV文件保存成功！");
        }
        #endregion

        #region 将行数相同的2个DataTable输出到同一个csv文件中
        /// <summary>
        /// 将行数相同的2个DataTable输出到同一个csv文件中
        /// </summary>
        /// <param name="t1">第一个DataTable</param>
        /// <param name="t2">第二个DataTable</param>
        /// <param name="fileName">文件名称</param>
        public static void SaveCSV_2TB(DataTable t1, DataTable t2, string fileName)
        {
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            string data = "";

            //写出列名称
            for (int i = 0; i < t1.Columns.Count; i++)
            {
                data += t1.Columns[i].ColumnName.ToString();
                if (i < t1.Columns.Count - 1)
                {
                    data += ",";
                }
            }

            data += ",";

            for (int i = 0; i < t2.Columns.Count; i++)
            {
                data += t2.Columns[i].ColumnName.ToString();
                if (i < t2.Columns.Count - 1)
                {
                    data += ",";
                }
            }

            sw.WriteLine(data);

            //写出各行数据
            for (int i = 0; i < t1.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < t1.Columns.Count; j++)
                {
                    data += t1.Rows[i][j].ToString();
                    if (j < t1.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }

                data += ",";

                for (int k = 0; k < t2.Columns.Count; k++)
                {
                    data += t2.Rows[i][k].ToString();
                    if (k < t2.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }


                sw.WriteLine(data);
            }

            sw.Close();
            fs.Close();
        }
        #endregion


        

        #region 将CSV文件的数据读取到DataTable中
        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable OpenCSV(string fileName)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;

            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                if (IsFirst == true)
                {
                    IsFirst = false;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(aryLine[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }

            sr.Close();
            fs.Close();
            return dt;
        }
        #endregion

        #region 修改数据表DataTable的数据类型
        /// <summary>
        /// 修改数据表DataTable的数据类型
        /// 某一列的类型和记录值(正确步骤：1.克隆表结构，2.修改列类型，3.修改记录值，4.返回希望的结果)
        /// </summary>
        /// <param name="argDataTable">数据表DataTable</param>
        /// <returns>数据表DataTable</returns>  
        public static DataTable UpdateDataTable(DataTable argDataTable)
        {
            DataTable dtResult = new DataTable();
            //克隆表结构
            dtResult = argDataTable.Clone();
            foreach (DataColumn col in dtResult.Columns)
            {
                if (col.ColumnName.Contains("Score") || col.ColumnName.Contains("Value"))
                {
                    //修改列类型
                    col.DataType = typeof(String);
                }

            }
            foreach (DataRow row in argDataTable.Rows)
            {
                DataRow rowNew = dtResult.NewRow();
                for(int i=0;i<argDataTable.Columns.Count;i++)
                {
                    rowNew[i] = row[i];
                }
                dtResult.Rows.Add(rowNew);
            }
            return dtResult;
        }
        #endregion



        public static DataSet divideDataTable(DataTable sourceTable, int count)
        {
            int rowCount = sourceTable.Rows.Count;

            if (rowCount < 1)
            {
                return null;
            }

            DataSet  reaultSet = new DataSet();
            int avCount = rowCount / count;
            int j = 0;
            for (int i = 0; i < count; i++)
            {
                DataTable tb = new DataTable();
                tb = sourceTable.Copy();
                tb.Rows.Clear();
                for (; j < (i + 1) * avCount; j++)
                {
                    tb.ImportRow(sourceTable.Rows[j]);
                }
                if (i == (count - 1))
                {
                    for(;j<rowCount;j++)
                    {
                        tb.ImportRow(sourceTable.Rows[j]);
                    } 
                }
                tb.TableName = i.ToString();
                reaultSet.Tables.Add(tb);
            }

            return reaultSet;
        }

        #region 从DataTable中移除指定列
        /// <summary>
        /// 从DataTable中移除指定列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static DataTable removeColsFromDataTable(DataTable dt, int[] cols)
        {
            foreach (int i in cols)
            {
                dt.Columns.RemoveAt(i);
            }

            return dt;
        }
        #endregion

    }
}
