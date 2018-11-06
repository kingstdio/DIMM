using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Data;
using System.Text.RegularExpressions;

namespace MMICIII.Utils
{
    public class SofaTools
    {

        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

        #region 获取某个ICUstay的所有Chartevent信息
        /// <summary>
        /// 获取icustay信息
        /// </summary>
        /// <param name="icustay_id"></param>
        /// <returns></returns>
        private static DataTable getICUStdayInfo(Int64 icustay_id)
        {
            string sql = @"select chartevents.* , d_items.label, item_unit 
                            from mimiciii.chartevents, mimiciii.d_items, mimiciii.sup_vars_sofa
                            where chartevents.itemid=d_items.itemid and chartevents.itemid=sup_vars_sofa.item_id and icustay_id =" + icustay_id;
            sql+=@" and chartevents.itemid in (select item_id from mimiciii.sup_vars_sofa) ORDER BY charttime;";
            DataTable dt = PGSQLHELPER.excuteDataTable(sql);
            return dt;
        }
        #endregion

        #region 构建sofa评分所需要的ICU病人信息
        /// <summary>
        /// 构建sofa评分所需要的ICU病人信息
        /// </summary>
        /// <returns></returns>
        public static bool constructSofaExpICustay()
        {
            DataTable pdata = loadSofaPatient();
            string sql = string.Empty;
            foreach (DataRow drow in pdata.Rows)
            {
                DataTable dt = getICUStdayInfo(Convert.ToInt64(drow[0]));
                double stayTime = icustayTime(dt);
                if (stayTime >= GlobalVars.ICUSTDAYMINLENGTH && stayTime<=GlobalVars.ICUSTDAYMAXLENGTH)
                {
                   string icustay_id =  drow[0].ToString();//icustay_id;
                   sql = @"INSERT INTO mimiciii.sup_exp_icustay_sofa(icustay_id, staytime) VALUES (" + icustay_id+" ,  "+stayTime.ToString("0.00")+" )";
                    PGSQLHELPER.executeScalar(sql);
                }
                
                Console.WriteLine(drow[1]);
                
            }
            return true;
        }
        #endregion

        #region 加载实验病人数据
        /// <summary>
        /// 加载实验病人数据
        /// </summary>
        /// <returns></returns>
        private static DataTable loadSofaPatient()
        {
            //string sql = @"select distinct icustay_id,subject_id  from mimiciii.chartevents where subject_id in (select subject_id from mimiciii.v_exp_patients) and icustay_id NOTNULL;";
            //DataTable pdata = PGSQLHELPER.excuteDataTable(sql);

            DataTable pdata = Utils.DataTableTools.OpenCSV(@"./data/exp_subject.csv");
            return pdata;
        }
        #endregion

        #region 判断一个ICUStay是否大于 GlobalVars.ICUSTDAYFLENGTH
        /// <summary>
        /// 判断一个ICUStay是否大于 GlobalVars.ICUSTDAYFLENGTH
        /// </summary>
        /// <param name="table">ICUstay</param>
        /// <returns>大于：true；小于：false</returns>
        private static double icustayTime( DataTable table)
        {
            if (table == null || table.Rows.Count<10)
            {
                return 0;
            }
            DateTime start =Convert.ToDateTime(table.Rows[0]["charttime"]);
            DateTime end = Convert.ToDateTime(table.Rows[table.Rows.Count - 1]["charttime"]);
            return end.Subtract(start).TotalHours;

        }
        #endregion

        #region 映射非数值数据
        /// <summary>
        /// 映射非数值数据
        /// </summary>
        /// <param name="input">需要映射的数据</param>
        /// <returns>返回映射后的数值</returns>
        public static string mapNoneNumerData(string input)
        {

            if (input.Trim() == "")
            {
                return "0";
            }

            if (CommonTools.isNumber(input))
            {
                return input;
            }
            else
            {
                foreach (var item in GlobalVars.sofaMapDic)
                {
                    if (input.Trim().ToLower() == item.mapKey.ToLower())
                    {
                        return item.mapValue.ToString();
                    }
                }
                return input; 
            }
       
        }
        #endregion

        #region 根据ICUstayId返回chart的全路径
        /// <summary>
        /// 根据ICUstayId返回chart的全路径
        /// </summary>
        /// <param name="icuStayid"></param>
        /// <param name="fileList">文件列表</param>
        /// <returns>文件全路劲</returns>
        public static string getChartFileFullPath(string icuStayid, string[] fileList)
        {
            foreach(String str in fileList)
            {
                if (str.Contains(icuStayid))
                {
                    return str;
                }
            }
            return string.Empty;
        }
        #endregion

        /// <summary>
        /// 根据ICUstayId返回chart的全文件名
        /// </summary>
        /// <param name="icuStayid">icuStayid</param>
        /// <param name="fileList">fileList</param>
        /// <returns></returns>
        public static string getChartFileFullName(string icuStayid, string[] fileList)
        {
            foreach (String str in fileList)
            {
                if (str.Contains(icuStayid))
                {
                    return str.Substring(str.LastIndexOf(@"\",str.Length));
                }
            }
            return string.Empty;
        }


        /// <summary>
        /// 移除无用的列
        /// </summary>
        /// <returns></returns>
        public static DataTable removeUseLessCols(DataTable dt)
        {
            int[] removeCols = {
                135,134,133,131,
                129,128,127,125,
                123,122,121,119,
                117,116,115,113,
                111,110,109,107,
                105,104,103,101,
                99,98,97,95,
                93,92,91,89,
                87,86,85,83,
                81,80,79,77,
                75,74,73,71,
                69,68,67,65,
                63,62,61,59,
                57,56,55,53,
                51,50,49,47,
                45,44,43,41,
                39,38,37,35,
                33,32,31,29,
                27,26,25,23,
                21,20,19,17,
                15,14,13,11,
                9,8,7,5
            };

            dt = DataTableTools.removeColsFromDataTable(dt, removeCols);
            
            return dt;
        }

        /// <summary>
        /// 将sofa的列全部移除
        /// </summary>
        /// <returns></returns>
        public static DataTable removeUseLessCols2(DataTable dt)
        {
            for(int i = 135; i >= 3; i--)
            {
                dt.Columns.RemoveAt(i);
            }

            return dt;
        }

    }
}
