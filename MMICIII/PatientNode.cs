﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMICIII
{
    public class PatientNode
    {
        /// <summary>
        /// 病人编号
        /// </summary>
        public Int64 subject_id { get; set; }

        /// <summary>
        /// ICU编号
        /// </summary>
        public Int64 icustay_id { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public double weight { get; set; }


        /// <summary>
        /// 身高
        /// </summary>
        public double height { get; set; }

        /// <summary>
        /// 是否死亡
        /// </summary>
        public bool isDead { get; set; }
        /// <summary>
        /// 进ICU时间
        /// </summary>
        public DateTime icuInTime { get; set; }

        /// <summary>
        /// 出ICU时间
        /// </summary>
        public DateTime icuOutTime { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public string dbsource { get; set; }

        /// <summary>
        /// ICU停留时长
        /// </summary>
        public double los { get; set; }


        public PatientNode()
        {
        }

        public PatientNode(Int64 subject_id, int age, string gender, double weight, double height, Int64 icustay_id)
        {
            this.subject_id = subject_id;
            this.age = age;
            this.gender = gender;
            this.weight = weight;
            this.icustay_id = icustay_id;
            this.height = height;
        }

        public void Clear()
        {
            this.subject_id =0;
            this.age = 0;
            this.gender = string.Empty;
            this.weight = 0;
            this.icustay_id = 0;
            this.height = 0;
        }

        #region 通过chartNode填充病人信息
        /// <summary>
        /// 通过chartNode填充病人信息
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool AssignmentByChartNode(ChartNode node)
        {
            this.subject_id = node.subject_id;
            this.icustay_id = node.icustay_id;
            string sql = @"select * from mimiciii.patients where subject_id =" + subject_id;
            DataRow drow = PGSQLHELPER.excuteDataRow(sql);

            this.age = CalculateAge(Convert.ToDateTime(drow[3]), node.charttime);
            string isDeadStr = drow[4].ToString().Trim();

            this.isDead = Utils.CommonTools.isICUdath(node.icustay_id.ToString());

            
            this.gender = drow[2].ToString();
            return true;
        }

        /// <summary>
        /// 根据ICUstayid 获得当前病人情况
        /// </summary>
        /// <param name="icustayid"></param>
        /// <returns></returns>
        public bool AssigmentByIcuStayId(string icustayid)
        {
            this.icustay_id = Convert.ToInt64(icustayid);
            string sql = "select ICU.subject_id, ICU.icustay_id, ICU.intime, ICU.dbsource , ICU.los*24 as los , P.gender, (((ICU.intime)::date - (P.dob)::date) / 365) as age from mimiciii.icustays ICU, mimiciii.patients P\n" +
                           "where ICU.subject_id=P.subject_id and icustay_id="+icustayid;
            DataRow dr = PGSQLHELPER.excuteDataRow(sql);

            this.subject_id = Convert.ToInt64(dr["subject_id"]);
            this.age = (int)dr["age"];
            this.gender = dr["gender"].ToString();
            this.isDead = Utils.CommonTools.isICUdath(icustayid);
            this.icuInTime = Convert.ToDateTime(dr["intime"]);
            this.dbsource = dr["dbsource"].ToString();

            this.los = Convert.ToDouble(dr["los"]);

            //体重
            sql = @"select * from mimiciii.weightfirstday where icustay_id ='"+icustayid+"'";
            dr = PGSQLHELPER.excuteDataRow(sql);
            if (dr == null)
            {
                this.weight = 0;
            }
            else
            {
                foreach (var cell in dr.ItemArray)
                {
                    if (cell.ToString() != string.Empty)
                    {
                        this.weight = Convert.ToDouble(cell);
                    }
                    else
                    {
                        this.weight = 0;
                    }
                }
            }

            //身高
            sql = @"select height from mimiciii.heightfirstday where icustay_id ='" + icustayid + "'";
            string hh = PGSQLHELPER.excuteSingleResult(sql);

            if (hh != string.Empty)
            {
                this.height = Convert.ToDouble(hh);
            }
            else
            {
                this.height = 0;
            }

            //进出ICU时间
            sql = @"select charttime from mimiciii.chartevents where icustay_id ='"+icustayid+"' ORDER BY charttime limit 1;";
            string resultDate = PGSQLHELPER.excuteSingleResult(sql);
            if (resultDate != string.Empty && resultDate!="0")
                this.icuInTime = Convert.ToDateTime( resultDate);

            sql = @"select charttime from mimiciii.chartevents where icustay_id ='" + icustayid + "' ORDER BY charttime desc limit 1;";
            resultDate = PGSQLHELPER.excuteSingleResult(sql);
            if (resultDate != string.Empty && resultDate != "0")
                this.icuOutTime = Convert.ToDateTime(resultDate);



            return true;
        }


        private int CalculateAge(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return age;
        }
        #endregion
    }
}
