using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMICIII
{
    public static class PGSQLHELPER
    {
        //public static string connectionString = "Server=10.33.2.149;Port=5432;User Id=uqzshi3;Password=1q2w3e4r5t;Database=mimiciii;CommandTimeout=2400;";
        //public static string connectionString = "Server=10.184.49.199;Port=5432;User Id=uqzshi3;Password=uqkingstdio2019;Database=mimic;CommandTimeout=2400;";


        //public static string connectionString = "Server=10.184.49.199;Port=5432;User Id=uqzshi3;Password=uqkingstdio2019;Database=eicu;CommandTimeout=2400;";
        //public static string connectionString = "Server=59.72.0.98;Port=5432;User Id=uqzshi3;Password=uqkingstdio2019;Database=mimic;CommandTimeout=2400;";
        public static string connectionString = "Server=192.168.1.49;Port=5432;User Id=uqzshi3;Password=uqkingstdio2019;Database=mimic;CommandTimeout=2400;";
        //public static string connectionString = "Server=10.184.50.138;Port=5432;User Id=uqzshi3;Password=uqkingstdio2019;Database=mimic;CommandTimeout=2400;";

        public static string conn2 = string.Empty;

        #region 数据库连接测试
        /// <summary>
        /// 数据库连接测试
        /// </summary>
        /// <returns></returns>
        public static bool dbTest() {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionString);
                conn.Open();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
        #endregion

        #region 执行非查询语句
        /// <summary>
        /// 执行非查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int executeScalar(String sql)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (NpgsqlCommand objCommand = new NpgsqlCommand(sql, conn))
                {
                    return Convert.ToInt32(objCommand.ExecuteScalar());
                }
            }
        }
        #endregion


        public static DataTable excuteDataTable(String sql)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (NpgsqlDataAdapter objAdapter = new NpgsqlDataAdapter(sql, conn))
                {
                    DataSet ds = new DataSet();
                    objAdapter.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }

        public static string excuteSingleResult(string sql)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (NpgsqlDataAdapter objAdapter = new NpgsqlDataAdapter(sql, conn))
                {
                    DataSet ds = new DataSet();
                    objAdapter.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
                    {
                        
                        return ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        return "0";////-------可能有问题
                    }
                }
            }
        }

        public static DataRow excuteDataRow(string sql)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (NpgsqlDataAdapter objAdapter = new NpgsqlDataAdapter(sql, conn))
                {
                    DataSet ds = new DataSet();
                    objAdapter.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }



    }
}
