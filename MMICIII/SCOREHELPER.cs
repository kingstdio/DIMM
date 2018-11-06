using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MMICIII
{
    static class SCOREHELPER
    {


        #region APACHEII

        #region 获得检查项目分组
        /// <summary>
        /// get var category by item id
        /// </summary>
        /// <param name="itemid">itemid</param>
        /// <returns>item category</returns>
        private static string  apacheii_getGroup(Int64 itemid)
        {
            string sql = @"select DISTINCT item_cat from mimiciii.sup_vars_apacheii where item_id ="+itemid;
            return PGSQLHELPER.excuteSingleResult(sql);
        }
        #endregion

        #region 华氏温度转化为摄氏温度
        /// <summary>
        /// 温度转换（华氏温度转化为摄氏温度）
        /// </summary>
        /// <param name="ftemp"></param>
        /// <returns></returns>
        private static double apacheii_temp_converter_FtoC(double ftemp)
        {
            return (ftemp - 32) / 1.8;
        }
        #endregion

        #region 计算温度分值
        /// <summary>
        /// 计算温度分值
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_Temp(ChartNode chartNode)
        {

            //if(chartNode.valuenum =null)

            //转换单位
            if (chartNode.valueom.Contains("F"))
            {
                chartNode.valuenum = apacheii_temp_converter_FtoC(chartNode.valuenum);
            }

            //算分
            if (chartNode.valuenum >= 41)
                return 4;
            if (chartNode.valuenum >= 39)
                return 3;
            if (chartNode.valuenum >= 38.5)
                return 1;
            if (chartNode.valuenum >= 36)
                return 0;
            if (chartNode.valuenum >= 34)
                return 1;
            if (chartNode.valuenum >= 32)
                return 2;
            if (chartNode.valuenum >= 30)
                return 3;
            if (chartNode.valuenum <= 29.9)
                return 4;

            return 0;
        }
        #endregion

        #region ABP
        /// <summary>
        /// 计算ABP动脉血压
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_ABP(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 160)
                return 4;
            if (chartNode.valuenum >= 130)
                return 3;
            if (chartNode.valuenum >= 110)
                return 2;
            if (chartNode.valuenum >= 70)
                return 0;
            if (chartNode.valuenum >= 50)
                return 2;
            if (chartNode.valuenum <= 49)
                return 4;

            return 0;
        }
        #endregion

        #region HR
        /// <summary>
        /// Calculate Heart Rate 
        /// </summary>
        /// <param name="chartNode"> chart node</param>
        /// <returns>calculated Score </returns>
        private static int apacheii_score_calc_HeartRate(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 180)
                return 4;
            if (chartNode.valuenum >= 140)
                return 3;
            if (chartNode.valuenum >= 110)
                return 2;
            if (chartNode.valuenum >= 70)
                return 0;
            if (chartNode.valuenum >= 55)
                return 2;
            if (chartNode.valuenum >= 40)
                return 3;
            if (chartNode.valuenum <= 39)
                return 4;

            return 0;
        }
        #endregion

        #region RR
        /// <summary>
        /// 呼吸速率
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_RR(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 50)
                return 4;
            if (chartNode.valuenum >= 35)
                return 3;
            if (chartNode.valuenum >= 25)
                return 1;
            if (chartNode.valuenum >= 12)
                return 0;
            if (chartNode.valuenum >= 10)
                return 1;
            if (chartNode.valuenum >= 6)
                return 2;
            if (chartNode.valuenum <= 5)
                return 4;

            return 0;
        }
        #endregion

        #region FiO2
        /// <summary>
        /// 氧分压
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_FiO2(ChartNode chartNode)
        {
            
            if (chartNode.valuenum >= 50)
                return 4;

            //-- 整体计算问题比较多

            //if (chartNode.valuenum >= 35)
            //    return 3;
            //if (chartNode.valuenum >= 25)
            //    return 1;
            //if (chartNode.valuenum >= 12)
            //    return 0;
            //if (chartNode.valuenum >= 10)
            //    return 1;
            //if (chartNode.valuenum >= 6)
            //    return 2;
            //if (chartNode.valuenum <= 5)
            //    return 4;

            return 0;
        }
        #endregion

        #region A-aDO2
        /// <summary>
        /// 氧分压-A-aDO2
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_AaDO2(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 50)
                return 4;
            //-- 整体计算问题比较多
            return 0;
        }
        #endregion

        #region PAO2
        /// <summary>
        /// 氧分压-PAO2
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_PAO2(ChartNode chartNode)
        {
            if (chartNode.valuenum >=70)
                return 0;
            if (chartNode.valuenum >= 61)
                return 1;
            if (chartNode.valuenum >= 55)
                return 3;
            if (chartNode.valuenum < 55)
                return 4;
            //-- 整体计算问题比较多
            return 0;
        }
        #endregion

        #region PH
        /// <summary>
        /// 动脉PH
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_PH(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 7.7)
                return 4;
            if (chartNode.valuenum >= 7.6)
                return 3;
            if (chartNode.valuenum >= 7.5)
                return 1;
            if (chartNode.valuenum >= 7.33)
                return 0;
            if (chartNode.valuenum >= 7.25)
                return 2;
            if (chartNode.valuenum >= 7.15)
                return 3;
            if (chartNode.valuenum < 7.15)
                return 4;
            return 0;
        }
        #endregion

        #region Na
        /// <summary>
        /// Na
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_Na(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 180)
                return 4;
            if (chartNode.valuenum >= 160)
                return 3;
            if (chartNode.valuenum >= 155)
                return 2;
            if (chartNode.valuenum >= 150)
                return 1;
            if (chartNode.valuenum >= 130)
                return 0;
            if (chartNode.valuenum >= 120)
                return 2;
            if (chartNode.valuenum >= 111)
                return 3;
            if (chartNode.valuenum <= 110)
                return 4;
            return 0;
        }
        #endregion

        #region Ka
        /// <summary>
        /// Na
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_Ka(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 7)
                return 4;
            if (chartNode.valuenum >= 6)
                return 3;
            if (chartNode.valuenum >= 5.5)
                return 1;
            if (chartNode.valuenum >= 3.5)
                return 0;
            if (chartNode.valuenum >= 3) 
                return 1;
            if (chartNode.valuenum >= 2.5)
                return 2;
            if (chartNode.valuenum < 2.5)
                return 4;
            return 0;
        }
        #endregion

        #region 肌酸酐
        /// <summary>
        /// 肌酸酐
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_Creatinine(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 3.5)
                return 4;
            if (chartNode.valuenum >= 6)
                return 3;
            if (chartNode.valuenum >= 1.5)
                return 2;
            if (chartNode.valuenum >= 0.6)
                return 0;
            if (chartNode.valuenum < 0.6)
                return 2;
            return 0;
        }
        #endregion

        #region 血细胞比容
        /// <summary>
        /// 血细胞比容
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_Hct(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 60)
                return 4;
            if (chartNode.valuenum >= 50)
                return 2;
            if (chartNode.valuenum >= 46)
                return 1;
            if (chartNode.valuenum >= 30)
                return 0;
            if (chartNode.valuenum >= 20)
                return 2;
            if (chartNode.valuenum < 20)
                return 4;
            return 0;
        }
        #endregion

        #region 白细胞计数
        /// <summary>
        /// 白细胞计数
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_WBC(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 40)
                return 4;
            if (chartNode.valuenum >= 20)
                return 2;
            if (chartNode.valuenum >= 15)
                return 1;
            if (chartNode.valuenum >= 3)
                return 0;
            if (chartNode.valuenum >= 1)
                return 2;
            if (chartNode.valuenum < 1)
                return 4;
            return 0;
        }
        #endregion

        #region GCS昏迷指数
        /// <summary>
        /// GCS昏迷指数
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_GCS(ChartNode chartNode)
        {
            return Convert.ToInt32(chartNode.valuenum);
        }
        #endregion

        #region 年龄评分
        /// <summary>
        /// 年龄评分
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_Age(int age)
        {
            if (age >= 75)
                return 6;
            if (age >= 65)
                return 5;
            if (age >= 55)
                return 3;
            if (age >= 45)
                return 2;
            return 0;
        }
        #endregion

        #region HCO3
        /// <summary>
        /// 血清碳酸
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int apacheii_score_calc_HCO3(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 52)
                return 4;
            if (chartNode.valuenum >= 41)
                return 3;
            if (chartNode.valuenum >= 32)
                return 1;
            if (chartNode.valuenum >= 22)
                return 0;
            if (chartNode.valuenum >= 18)
                return 2;
            if (chartNode.valuenum >= 15)
                return 3;
            if (chartNode.valuenum < 15)
                return 4;
            return 0;
        }
        #endregion

        #region 计算一个周期内病人的ApacheII评分
        /// <summary>
        /// 计算一个周期内病人的ApacheII评分
        /// </summary>
        /// <param name="chartList">周期检查列表</param>
        /// <returns>评分节点</returns>
        public static SequenceNode apacheii_score(IGrouping<int, ChartNode> chartList)
        {
            SequenceNode sequenceNode = new SequenceNode();
            sequenceNode.itemCount = 0;
            
            //计算方式，同一时间段只算最后一次
            foreach (ChartNode delNode in chartList)
            {
                string itemcat = apacheii_getGroup(delNode.itemid);

                //计算温度
                if (itemcat == "Temperature")
                {
                    sequenceNode.temperatureScore = apacheii_score_calc_Temp(delNode);
                    sequenceNode.temperatureitemid = delNode.itemid;
                    sequenceNode.temperatureLabel = delNode.label.Replace(',',' ').Replace(',',' ');
                    sequenceNode.temperatureValue = delNode.value;
                    sequenceNode.temperatureValueuom = delNode.valueom;
                    sequenceNode.temperatureFlag = true;
                }

                //计算动脉压
                if (itemcat == "ABP_mean")
                {
                    sequenceNode.ABPScore = apacheii_score_calc_ABP(delNode);
                    sequenceNode.ABPitemid = delNode.itemid;
                    sequenceNode.ABPLabel = delNode.label.Replace(',',' ');
                    sequenceNode.ABPValue = delNode.value;
                    sequenceNode.ABPValueuom = delNode.valueom;
                    sequenceNode.ABPFlag = true;
                }

                //计算心率
                if (itemcat == "HR")
                {
                    sequenceNode.heartReateScore = apacheii_score_calc_HeartRate(delNode);
                    sequenceNode.heartReateitemid = delNode.itemid;
                    sequenceNode.heartReateLabel = delNode.label.Replace(',',' ');
                    sequenceNode.heartReateValue = delNode.value;
                    sequenceNode.heartReateValueuom = delNode.valueom;
                    sequenceNode.heartReateFlag = true;
                }

                //计算呼吸速率
                if (itemcat == "Respiratory rate")
                {
                    sequenceNode.respiratoryRateScore = apacheii_score_calc_RR(delNode);
                    sequenceNode.respiratoryitemid = delNode.itemid;
                    sequenceNode.respiratoryLabel = delNode.label.Replace(',',' ');
                    sequenceNode.respiratoryValue = delNode.value;
                    sequenceNode.respiratoryValueuom = delNode.valueom;
                    sequenceNode.respiratoryFlag = true;
                }

                //计算氧分压
                if (itemcat == "PAO2")
                {
                    sequenceNode.FiO2Score = apacheii_score_calc_PAO2(delNode);
                    sequenceNode.FiO2itemid = delNode.itemid;
                    sequenceNode.FiO2Label = delNode.label.Replace(',',' ');
                    sequenceNode.FiO2Value = delNode.value;
                    sequenceNode.FiO2Valueuom = delNode.valueom;
                    sequenceNode.FiO2Flag = true;
                }

                //计算PH
                if (itemcat == "Arterial_pH")
                {
                    sequenceNode.PHScore = apacheii_score_calc_PH(delNode);
                    sequenceNode.PHitemid = delNode.itemid;
                    sequenceNode.PHLabel = delNode.label.Replace(',',' ');
                    sequenceNode.PHValue = delNode.value;
                    sequenceNode.PHValueuom = delNode.valueom;
                    sequenceNode.PHFlag = true;
                }

                //计算Na
                if (itemcat == "SODIUM")
                {
                    sequenceNode.sodiumScore = apacheii_score_calc_Na(delNode);
                    sequenceNode.sodiumitemid = delNode.itemid;
                    sequenceNode.sodiumLabel = delNode.label.Replace(',',' ');
                    sequenceNode.sodiumValue = delNode.value;
                    sequenceNode.sodiumValueuom = delNode.valueom;
                    sequenceNode.sodiumFlag = true;
                }

                //计算Ka
                if (itemcat == "POTASSIUM")
                {
                    sequenceNode.potassiumSore = apacheii_score_calc_Ka(delNode);
                    sequenceNode.potassiumitemid = delNode.itemid;
                    sequenceNode.potassiumLabel = delNode.label.Replace(',',' ');
                    sequenceNode.potassiumValue = delNode.value;
                    sequenceNode.potassiumValueuom = delNode.valueom;
                    sequenceNode.potassiumFlag = true;
                }

                //计算肌酸酐
                if (itemcat == "CREATININE")
                {
                    sequenceNode.creatinineScore = apacheii_score_calc_Creatinine(delNode);
                    sequenceNode.creatinineitemid = delNode.itemid;
                    sequenceNode.creatinineLabel = delNode.label.Replace(',',' ');
                    sequenceNode.creatinineValue = delNode.value;
                    sequenceNode.creatinineValueuom = delNode.valueom;
                    sequenceNode.creatinineFlag = true;
                }

                //血细胞比容
                if (itemcat == "HCT")
                {
                    sequenceNode.HCTScore = apacheii_score_calc_Hct(delNode);
                    sequenceNode.HCTitemid = delNode.itemid;
                    sequenceNode.HCTLabel = delNode.label.Replace(',',' ');
                    sequenceNode.HCTValue = delNode.value;
                    sequenceNode.HCTValueuom = delNode.valueom;
                    sequenceNode.HCTFlag = true;
                }

                //白细胞计数
                if (itemcat == "WBC")
                {
                    sequenceNode.WBCScore = apacheii_score_calc_WBC(delNode);
                    sequenceNode.WBCitemid = delNode.itemid;
                    sequenceNode.WBCLabel = delNode.label.Replace(',',' ');
                    sequenceNode.WBCValue = delNode.value;
                    sequenceNode.WBCValueuom = delNode.valueom;
                    sequenceNode.WBCFlag = true;
                }

                //GCS昏迷指数
                if (itemcat == "GCS")
                {
                    sequenceNode.GCSScore += apacheii_score_calc_GCS(delNode);
                    sequenceNode.GCSitemid = delNode.itemid;
                    sequenceNode.GCSLabel = delNode.label.Replace(',',' ');
                    sequenceNode.GCSValue = delNode.value;
                    sequenceNode.GCSValueuom = delNode.valueom;
                    sequenceNode.GCSFlag = true;
                }

                //HCO3
                if (itemcat == "HCO3")
                {
                    sequenceNode.HCO3Score += apacheii_score_calc_HCO3(delNode);
                    sequenceNode.HCO3itemid = delNode.itemid;
                    sequenceNode.HCO3Label = delNode.label.Replace(',',' ');
                    sequenceNode.HCO3Value = delNode.value;
                    sequenceNode.HCO3Valueuom = delNode.valueom;
                    sequenceNode.HCO3Flag = true;
                }

                //统计项目计数
                sequenceNode.itemCount++;
                
            }

            PatientNode nowPatient = new PatientNode();
            nowPatient.AssignmentByChartNode(chartList.ElementAt(0));

            sequenceNode.ageScore = apacheii_score_calc_Age(nowPatient.age);
            sequenceNode.ageFlag = true;

            sequenceNode.seqId = chartList.ElementAt(0).timeSqid;

            sequenceNode.totalScore = sequenceNode.temperatureScore + sequenceNode.ABPScore + sequenceNode.heartReateScore + sequenceNode.respiratoryRateScore
                + sequenceNode.FiO2Score + sequenceNode.PHScore + sequenceNode.sodiumScore + sequenceNode.potassiumSore + sequenceNode.creatinineScore
                + sequenceNode.HCTScore + sequenceNode.WBCScore + sequenceNode.GCSScore + sequenceNode.ageScore + sequenceNode.HCO3Score;

            return sequenceNode;
        }
        #endregion

        #endregion
        
        #region 获得项目分组-Sofa
        /// <summary>
        /// 获得项目分组
        /// </summary>
        /// <param name="itemid">itemid</param>
        /// <returns>[0]:小组；[1]：大组；</returns>
        private static DataRow sofa_getGroup(Int64 itemid)
        {
            string  sql = @"select DISTINCT item_cat, item_cat_up from mimiciii.sup_vars_sofa where item_id = "+itemid;
            return PGSQLHELPER.excuteDataRow(sql);
        }
        #endregion
        
        #region 计算PaO2-Sofa
        /// <summary>
        /// 计算Pao2-Sofa
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int sofa_score_calc_PaO2(ChartNode chartNode)
        {
            //算分
            
            if (chartNode.valuenum < 100)
                return 4;
            if (chartNode.valuenum <200 )
                return 3;
            if (chartNode.valuenum < 300)
                return 2;
            if (chartNode.valuenum < 400)
                return 1;
            if (chartNode.valuenum >= 400)
                return 0;

            return 0;
        }
        #endregion

        #region 计算FiO2-Sofa
        /// <summary>
        /// 氧分压
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int sofa_score_calc_FiO2(ChartNode chartNode)
        {

            if (chartNode.valuenum < 13.3)
                return 4;
            if (chartNode.valuenum < 26.7)
                return 3;
            if (chartNode.valuenum < 40)
                return 2;
            if (chartNode.valuenum < 53.3)
                return 1;
            if (chartNode.valuenum >= 53.3)
                return 0;
            return 0;
        }
        #endregion

        #region 计算Platelets-Sofa
        /// <summary>
        /// 氧分压
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int Sofa_score_calc_Platelets(ChartNode chartNode)
        {
            if (chartNode.valuenum < 20)
                return 4;
            if (chartNode.valuenum < 50)
                return 3;
            if (chartNode.valuenum < 100)
                return 2;
            if (chartNode.valuenum < 150)
                return 1;
            if (chartNode.valuenum >= 150)
                return 0;
            return 0;
        }
        #endregion

        #region 计算Bilirubin-Sofa
        /// <summary>
        /// 胆红素
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int Sofa_score_calc_Bilirubin(ChartNode chartNode)
        {
            if (chartNode.valuenum <= 1.9)
                return 0;
            if (chartNode.valuenum < 2.0)
                return 1;
            if (chartNode.valuenum < 6.0)
                return 2;
            if (chartNode.valuenum < 12)
                return 3;

            if (chartNode.valuenum >= 12)
                return 4;
            return 0;
        }
        #endregion

        #region ABP-Sofa
        /// <summary>
        /// 计算ABP动脉血压
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int Sofa_score_calc_ABP(ChartNode chartNode)
        {
            if (chartNode.valuenum >= 70)
            {
                return 0;
            }
            else
            {
                return 1;
            }            
        }
        #endregion

        #region GCS昏迷指数 - Sofa
        /// <summary>
        /// GCS昏迷指数
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int Sofa_score_calc_GCS(ChartNode chartNode)
        {
            if (chartNode.valuenum < 6)
                return 4;
            if (chartNode.valuenum <= 9)
                return 3;
            if (chartNode.valuenum <=12)
                return 2;
            if (chartNode.valuenum <=14)
                return 1;
            return 0;
        }
        #endregion

        #region Creatinine - Sofa
        /// <summary>
        ///Creatinine 肌酸酐
        /// </summary>
        /// <param name="chartNode">项目节点</param>
        /// <returns>计算好的分数</returns>
        private static int Sofa_score_calc_Creatinine(ChartNode chartNode)
        {
            if (chartNode.valuenum <= 1.2)
                return 0;
            if (chartNode.valuenum < 1.9)
                return 1;
            if (chartNode.valuenum < 3.4)
                return 2;
            if (chartNode.valuenum < 4.9)
                return 3;
            if (chartNode.valuenum >= 5.0)
                return 4;
            return 0;
        }
        #endregion

        #region Urine - Sofa
        /// <summary>
        ///Urine 尿
        /// </summary>
        /// <param name="chartNode">数值</param>
        /// <returns>计算好的分数</returns>
        private static int Sofa_score_calc_Urine(double value)
        {
            if (value <= 0)
                return 0;
            if (value < 200)
                return 4;
            if (value < 500)
                return 3;
            return 0;
        }
        #endregion

        #region 计算一个周期内病人的SOFA评分
        /// <summary>
        /// 计算一个周期内病人的SOFA评分
        /// </summary>
        /// <param name="chartList">周期检查列表</param>
        /// <returns>评分节点</returns>
        public static SequenceNode sofa_score(IGrouping<int, ChartNode> chartList)
        {
            SequenceNode sequenceNode = new SequenceNode();
            sequenceNode.itemCount = 0;
            //计算方式，同一时间段只算最后一次

            List<string> GCS_exist_list = new List<string>();



            foreach (ChartNode delNode in chartList)
            {
                DataRow itemcatC = sofa_getGroup(delNode.itemid);

                string itemcatUp = itemcatC[1].ToString();
                string itemcat = itemcatC[0].ToString();

                //1.1 计算Pao2
                if (itemcat == "PaO2")
                {
                    sequenceNode.PaO2Score = sofa_score_calc_PaO2(delNode);
                    sequenceNode.PaO2itemid = delNode.itemid;
                    sequenceNode.PaO2Label = delNode.label.Replace(',', ' ').Replace(',', ' ');
                    sequenceNode.PaO2Value = delNode.valuenum.ToString();
                    sequenceNode.PaO2Valueuom = delNode.valueom;
                    sequenceNode.PaO2Flag = true;
                }

                //1.2 计算氧分压
                if (itemcat == "FIO2")
                {
                    sequenceNode.FiO2Score = sofa_score_calc_FiO2(delNode);
                    sequenceNode.FiO2itemid = delNode.itemid;
                    sequenceNode.FiO2Label = delNode.label.Replace(',', ' ');
                    sequenceNode.FiO2Value = delNode.valuenum.ToString();
                    sequenceNode.FiO2Valueuom = delNode.valueom;
                    sequenceNode.FiO2Flag = true;
                }


                //2 计算血小板
                if (itemcat == "Platelets")
                {
                    sequenceNode.PlateletsScore = Sofa_score_calc_Platelets(delNode);
                    sequenceNode.Plateletsitemid = delNode.itemid;
                    sequenceNode.PlateletsLabel = delNode.label.Replace(',', ' ');
                    sequenceNode.PlateletsValue = delNode.valuenum.ToString();
                    sequenceNode.PlateletsValueuom = delNode.valueom;
                    sequenceNode.PlateletsFlag = true;
                }

                //3 计算胆红素
                if (itemcat == "Bilirubin")
                {
                    sequenceNode.BilirubinScore = Sofa_score_calc_Bilirubin(delNode);
                    sequenceNode.Bilirubinitemid = delNode.itemid;
                    sequenceNode.BilirubinLabel = delNode.label.Replace(',', ' ');
                    sequenceNode.BilirubinValue = delNode.valuenum.ToString();
                    sequenceNode.BilirubinValueuom = delNode.valueom;
                    sequenceNode.BilirubinFlag = true;
                }

                //4.1 计算动脉压
                if (itemcat == "ABP_mean")
                {
                    sequenceNode.ABPScore = Sofa_score_calc_ABP(delNode);
                    sequenceNode.ABPitemid = delNode.itemid;
                    sequenceNode.ABPLabel = delNode.label.Replace(',', ' ');
                    sequenceNode.ABPValue = delNode.valuenum.ToString();
                    sequenceNode.ABPValueuom = delNode.valueom;
                    sequenceNode.ABPFlag = true;
                }

                //5 GCS昏迷指数
                if (itemcat == "GCS")
                {
                    if (!GCS_exist_list.Contains(delNode.value))
                    {
                        GCS_exist_list.Add(delNode.value);
                        sequenceNode.GCSScore += Sofa_score_calc_GCS(delNode);
                    }

                    //sequenceNode.GCSScore = Sofa_score_calc_GCS(delNode);
                    sequenceNode.GCSitemid = delNode.itemid;
                    sequenceNode.GCSLabel = delNode.label.Replace(',', ' ');
                    sequenceNode.GCSValue = delNode.valuenum.ToString();
                    sequenceNode.GCSValueuom = delNode.valueom;
                    sequenceNode.GCSFlag = true;
                }

                //6 计算Creatinine肌酸酐
                if (itemcat == "Creatinine")
                {
                    sequenceNode.creatinineScore = Sofa_score_calc_Creatinine(delNode);
                    sequenceNode.creatinineitemid = delNode.itemid;
                    sequenceNode.creatinineLabel = delNode.label.Replace(',', ' ');
                    sequenceNode.creatinineValue = delNode.valuenum.ToString();
                    sequenceNode.creatinineValueuom = delNode.valueom;
                    sequenceNode.creatinineFlag = true;
                }
               
                //if(itemcat == "Urine")
                //{
                    
                //    sql = @"";

                //    sequenceNode.UrineScore = Sofa_score_calc_Urine(600);
                //    sequenceNode.Urineitemid = delNode.itemid;
                //    sequenceNode.UrineLabel = delNode.label.Replace(',', ' ');
                //    sequenceNode.UrineValue = delNode.value;
                //    sequenceNode.UrineValueuom = delNode.valueom;
                //    sequenceNode.UrineFlag = true;
                //}
                //统计项目计数
                sequenceNode.itemCount++;

            }

            sequenceNode.seqId = chartList.ElementAt(0).timeSqid;

            sequenceNode.beginTime = chartList.ElementAt(0).charttime;
            string beginStr = sequenceNode.beginTime.ToString("yyyy/MM/dd/ HH:mm:ss");
            sequenceNode.beginTime.ToLocalTime();

            string  sql = @"select sum(value) as M from mimiciii.outputevents where icustay_id= "+ chartList.ElementAt(0).icustay_id +" and itemid in (select item_id from mimiciii.sup_vars_sofa where item_cat='Urine') and ";           
            sql+=@" charttime BETWEEN ((timestamp '"+beginStr+ "')- INTERVAL '12 HOUR') AND ((TIMESTAMP '" + beginStr + "')+ INTERVAL '12 HOUR'); ";


            
            string dbresult = PGSQLHELPER.excuteSingleResult(sql);
            double val = 0;
            if (dbresult != string.Empty)
            {
                val = Convert.ToDouble(dbresult);
                sequenceNode.UrineFlag = true;
                sequenceNode.UrineScore = Sofa_score_calc_Urine(val);
                sequenceNode.UrineValue = val.ToString();
                sequenceNode.UrineValueuom = "mL";
            }
           


            sequenceNode.totalScore = 
            sequenceNode.PaO2Score +
            sequenceNode.FiO2Score +
            sequenceNode.PlateletsScore +
            sequenceNode.BilirubinScore +
            sequenceNode.ABPScore +
            sequenceNode.DobutamineScore +
            sequenceNode.DopamineScore +
            sequenceNode.EpinephrineScore +
            sequenceNode.GCSScore +
            sequenceNode.creatinineScore +
            sequenceNode.UrineScore;

            return sequenceNode;
        }
        #endregion

 
    }
}
