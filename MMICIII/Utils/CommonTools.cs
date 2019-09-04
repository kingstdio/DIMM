using System;
using System.Data;
using System.Text.RegularExpressions;

namespace MMICIII.Utils
{
    public class CommonTools
    {

        #region 判断一个字符串是不是数字
        /// <summary>
        /// 判断一个字符串是不是数字
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>True:是；False：不是</returns>
        public static bool isNumber(string str)
        {
            if (str.Trim() == "")
            {
                return true;
            }

            return Regex.IsMatch(str, @"^[+-]?/d*[.]?/d*$");
            //try
            //{
            //    Convert.ToDouble(str);
            //    return true;
            //}catch(FormatException ex)
            //{
            //    return false;
            //}
        }
        #endregion

        #region 判断一个病人是否是ICU死亡
        /// <summary>
        /// 判断一个病人是否是ICU死亡
        /// </summary>
        /// <param name="icuStayID">icustayid</param>
        /// <returns>True：是ICU死亡；False：非ICU死亡</returns>
        public static bool isICUdath(string icuStayID)
        {
            //获得最后一次chart时间
            string sql = @"select charttime  from mimiciii.chartevents where icustay_id = "+icuStayID+" ORDER BY charttime desc limit 1";
            string outIcuTime = PGSQLHELPER.excuteSingleResult(sql).Trim();

            if (outIcuTime == "" || outIcuTime=="0")//如果没有chart事件，默认非ICU死亡
            {
                return false;
            }
            
            //查询病人死亡时间
            sql = @"select dod from mimiciii.patients where subject_id =(select subject_id from mimiciii.icustays where icustay_id="+icuStayID+")";
            string deadTime = PGSQLHELPER.excuteSingleResult(sql).Trim();

            if(deadTime == "") //如果没有查询到死亡时间，则非ICU死亡
            {
                return false;
            }

            DateTime tOutICU = Convert.ToDateTime(outIcuTime);
            DateTime tDeathTime = Convert.ToDateTime(deadTime);

            //如果死亡时间减去最后一次Chart时间小于指定的GlobalVars.DEATHCOUNTDAY，则认为是ICU死亡，否则认为是非ICU死亡
            if (tDeathTime.Subtract(tOutICU).TotalDays <= GlobalVars.DEATHCOUNTDAY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 判断当前ICUstay是否是该病人的最后一次ICUstay
        /// <summary>
        /// 判断当前ICUstay是否是该病人的最后一次ICUstay
        /// </summary>
        /// <param name="icustayid">icustayid</param>
        /// <returns>是：true；否：false；</returns>
        public static bool isLastICU(string icustayid)
        {
            string sql = @"select distinct icustay_id from mimiciii.icustays where subject_id =(select subject_id from mimiciii.icustays where icustay_id=" + icustayid+") and icustay_id NOTNULL;";
            DataTable dt = PGSQLHELPER.excuteDataTable(sql);

            //如果只有一条，那么就是最后一条icustay记录
            if (dt.Rows.Count < 2)
            {
                return true;
            }
            sql = @"select charttime  from mimiciii.chartevents where icustay_id = "+icustayid+" ORDER BY charttime desc limit 1";
            DateTime currentDateTime;

            string strtime = PGSQLHELPER.excuteSingleResult(sql).Trim();
            if (strtime != "0")
            {
                currentDateTime = Convert.ToDateTime(PGSQLHELPER.excuteSingleResult(sql));
            }
            else
            {
                return false;
            }

            

            foreach (DataRow dr in dt.Rows)
            {
                sql = @"select charttime  from mimiciii.chartevents where icustay_id = " + dr[0].ToString() + " ORDER BY charttime desc limit 1";
                string datetimeStr = PGSQLHELPER.excuteSingleResult(sql).Trim();
                if (datetimeStr != "0")
                {
                    DateTime dateTime = Convert.ToDateTime(datetimeStr);
                    if (DateTime.Compare(dateTime,currentDateTime)>0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion
    }
}
