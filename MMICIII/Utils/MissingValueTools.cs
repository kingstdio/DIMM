using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;

namespace MMICIII.Utils
{
    #region 缺失值填充实验，线程启动参数类
    /// <summary>
    /// 缺失值填充实验，线程启动参数类
    /// </summary>
    public class MissValueThreadParam
    {
        /// <summary>
        /// ICD9 code
        /// </summary>
        public string icd9Code { get; set; }
        /// <summary>
        /// icustay table
        /// </summary>
        public DataTable subExpTable { get; set; }



        /// <summary>
        /// 时序间隔
        /// </summary>
        public int timespan { get; set; }

        public MissValueThreadParam(string icd9code, DataTable subExpTable, int timespan)
        {
            this.icd9Code = icd9code;
            this.subExpTable = subExpTable;
            this.timespan = timespan;
        }

    }
    #endregion

    #region 填充方法
    /// <summary>
    /// 填充方法
    /// </summary>
    public enum MISSINGFILLTYPE {CopyPrevious=1, WholeMean=2, KNearMean=3, SampleMean=4};
    #endregion

    #region 事件类型
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EVENTTYPE { chartEvent=1, inputEvent=2, allEvent=3 }
    #endregion


    

    public class MissingValueTools
    {
        #region 构建特征
        /// <summary>
        /// 构建特征
        /// </summary>
        private static void constructExpFeature() {
            //--chartEvent 特征
            if (GlobalVars.CHARTFETURETABLE.Rows.Count < 1)
            {
                string featurePath = setOutputDir(GlobalVars.CURRENTICDCODE)+"chartFeature.csv";
                if (!File.Exists(featurePath))
                {
                    string sql = "select itemid from mimiciii.\"sup_vars_ChartEvent_Rank\" where \"isNumber\" = '1' and itemid in (\n" +
                            "select itemid from mimiciii.chartevents where icustay_id in(\n" +
                            "				select	icustay_id from	mimiciii.icustays where	hadm_id in ( \n" +
                            "				select hadm_id from mimiciii.v_exp_death_diagnoses_details  where icd9_code = '" + GlobalVars.CURRENTICDCODE + "' and seq_num<= " + GlobalVars.DISEASERELATEDLEVEL + "\n" +
                            "				)\n" +
                            ")\n" +
                            ")order by countnum desc limit " + GlobalVars.CHARTFEATURENUM;
                    GlobalVars.CHARTFETURETABLE = PGSQLHELPER.excuteDataTable(sql);
                    DataTableTools.SaveCSV(GlobalVars.CHARTFETURETABLE, featurePath);
                }
                else
                {
                    GlobalVars.CHARTFETURETABLE = DataTableTools.OpenCSV(featurePath);
                }
               
                // string sql = "select item_id from mimiciii.sup_missing_features WHERE cat ="+ GlobalVars.CURRENTICDCODE + " and ctype='chart'";

                
                
            }

            //--inputEvent 特征

            if (GlobalVars.INPUTFETURETABLE.Rows.Count < 1 && GlobalVars.INPUTSWITCH)
            {
                string featurePath = setOutputDir(GlobalVars.CURRENTICDCODE) + "inputFeature.csv";
                if (!File.Exists(featurePath))
                {
                    string sqll = "select itemid  from mimiciii.\"v_featureRank_input\" where itemid in (\n" +
                                "select itemid from mimiciii.v_exp_input_sofa_cv where icustay_id in\n" +
                                "(" +
                                "select icustay_id from mimiciii.\"v_exp_patients_sofa_Extended\" WHERE	icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	hadm_id IN ( " +
                                " SELECT hadm_id FROM mimiciii.v_exp_death_diagnoses_details  WHERE icd9_code = '" + GlobalVars.CURRENTICDCODE + "' and seq_num<= " + GlobalVars.DISEASERELATEDLEVEL + " )  and " +
                                " icustay_id in (select icustay_id from mimiciii.v_exp_chartICU)	) and los BETWEEN " + GlobalVars.ICUSTDAYMINLENGTH + " and " + GlobalVars.ICUSTDAYMAXLENGTH +
                                " )" +
                                ")or itemid in (\n" +
                                "select itemid from mimiciii.v_exp_input_sofa_mv where icustay_id in\n" +
                                "(" +
                                "select icustay_id from mimiciii.\"v_exp_patients_sofa_Extended\" WHERE	icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	hadm_id IN ( " +
                                " SELECT hadm_id FROM mimiciii.v_exp_death_diagnoses_details  WHERE icd9_code = '" + GlobalVars.CURRENTICDCODE + "' and seq_num<= " + GlobalVars.DISEASERELATEDLEVEL + " )  " +
                                " and icustay_id in (select icustay_id from mimiciii.v_exp_chartICU)	) and los BETWEEN " + GlobalVars.ICUSTDAYMINLENGTH + " and " + GlobalVars.ICUSTDAYMAXLENGTH +
                                ")" +
                                ") order by countnum desc limit " + GlobalVars.INPUTFEATURENUM;

                    // string sqll = "select item_id from mimiciii.sup_missing_features WHERE cat =" + GlobalVars.CURRENTICDCODE + " and ctype='input'";
                    GlobalVars.INPUTFETURETABLE = PGSQLHELPER.excuteDataTable(sqll);
                    DataTableTools.SaveCSV(GlobalVars.INPUTFETURETABLE, featurePath);
                }
                else
                {
                    GlobalVars.INPUTFETURETABLE = DataTableTools.OpenCSV(featurePath);
                }
            }
        }
        #endregion

        #region 0. 获取实验数据主方法
        /// <summary>
        /// 获取实验数据主方法
        /// </summary>
        /// <param name="icd9code"></param>
        public static void constructExpData(String icd9code , string pathOut)
        {

            constructExpFeature();


            DataTable runTable = getExplist(icd9code, GlobalVars.DISEASERELATEDLEVEL, GlobalVars.ICUSTDAYMINLENGTH, GlobalVars.ICUSTDAYMAXLENGTH);
            DataSet runSet = DataTableTools.divideDataTable(runTable, GlobalVars.threadCount);

            //输出标签信息
            if (!File.Exists(pathOut + "label.csv"))
            {
                DataTable labelTable = constructLabelData(runTable);
                DataTableTools.SaveCSV(labelTable, pathOut + "label.csv");
            }

            foreach (DataTable singleTb in runSet.Tables)
            {
                Thread expThread = new Thread(new ParameterizedThreadStart(expIntergration));
                MissValueThreadParam threadParam = new MissValueThreadParam(icd9code, singleTb,  GlobalVars.TIMESPAN);
                expThread.Start(threadParam);
            }


        }
        #endregion

        #region 0.1 构建数据集标签文件
        /// <summary>
        /// 构建数据集标签文件
        /// </summary>
        /// <param name="infoTable"></param>
        /// <returns></returns>
        public static DataTable constructLabelData(DataTable infoTable)
        {
            DataTable outTable = new DataTable();
            outTable.Columns.Add(new DataColumn("id", Type.GetType("System.Int16")));
            outTable.Columns.Add(new DataColumn("icustay_id", Type.GetType("System.String")));
            outTable.Columns.Add(new DataColumn("isAlive", Type.GetType("System.Int16")));
            outTable.Columns.Add(new DataColumn("inTime",  Type.GetType("System.DateTime")));
            outTable.Columns.Add(new DataColumn("outTime", Type.GetType("System.DateTime")));
            outTable.Columns.Add(new DataColumn("missingRate", Type.GetType("System.Double")));


            int countid = 1;
            foreach(DataRow dr in infoTable.Rows)
            {              
                #region 1p1u
                //筛除多个ICUstay的，只算最后一次
                if (GlobalVars.IS1P1U)
                {
                    if (!Utils.CommonTools.isLastICU(dr["icustay_id"].ToString()))
                    {
                        continue;
                    }
                }
                #endregion


                PatientNode patientNode = new PatientNode();
                patientNode.AssigmentByIcuStayId(dr["icustay_id"].ToString());


                DataRow idr = outTable.NewRow();
                idr["id"] = countid;
                idr["icustay_id"] = patientNode.icustay_id;
                idr["isAlive"] = patientNode.isDead?0:1;
                idr["inTime"] = patientNode.icuInTime;
                idr["outTime"] = patientNode.icuOutTime;
                outTable.Rows.Add(idr);

                countid++;
          

            }

            return outTable;
        }
        #endregion

        #region 1. 获取试验用ICUstay信息
        /// <summary>
        /// 获取试验用ICUstay信息
        /// </summary>
        /// <param name="icd9"></param>
        /// <param name="icuStayMin"></param>
        /// <param name="icuStayMax"></param>
        /// <returns></returns>
        public static DataTable getExplist(String icd9, int relatedLevel, int icuStayMin=0, int icuStayMax=360 )
        {
            //住院时长压缩到10天的

            string sql = "select row_id, icustay_id, los, dbsource from mimiciii.\"v_exp_patients_sofa_Extended\" WHERE	" +
                          "icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	hadm_id IN ( SELECT hadm_id FROM mimiciii.v_exp_death_diagnoses_details  WHERE icd9_code = '" +
                          icd9 + "' and seq_num<= "+ relatedLevel + " )  and icustay_id in (select icustay_id from mimiciii.v_exp_chartICU)	) and los BETWEEN " + icuStayMin + " and " + icuStayMax + ";";

            DataTable result = PGSQLHELPER.excuteDataTable(sql);
            return result;
        }
        #endregion

        #region 2. 设置输出目录
        /// <summary>
        /// 设置输出目录
        /// </summary>
        /// <param name="icd9"></param>
        /// <returns></returns>
        public static string setOutputDir(string icd9)
        {
            
            string pathInput_out = GlobalVars.EXPFILEBASE + @"_" + (GlobalVars.ICUSTDAYMAXLENGTH * 60 / GlobalVars.TIMESPAN) + @"\" + icd9+ @"\";
            
            if (GlobalVars.FILLMISSING)
            {
                pathInput_out = pathInput_out  + GlobalVars.FillMissingTpe.ToString()+@"\";
            }
            else
            {
                pathInput_out = pathInput_out + @"original\";
            }

            if (!Directory.Exists(pathInput_out))
            {
                Directory.CreateDirectory(pathInput_out);
            }

            Console.WriteLine("output path: "+ pathInput_out);
            return pathInput_out;
        }
        #endregion

        #region 3. 读取已经计算完的文件列表
        /// <summary>
        /// 读取已经计算完的文件列表
        /// </summary>
        /// <param name="pathInput_out">文件存放目录</param>
        /// <returns></returns>
        private static List<string> getExistingFiles(string pathInput_out)
        {
            if (!Directory.Exists(pathInput_out))
            {
                return null;
            }
            var files = Directory.GetFiles(pathInput_out, "*.csv");
            List<String> existIcu = new List<string>();
            foreach (var file in files)
            {
                string str = file.Substring(file.LastIndexOf(@"\") + 1);
                string[] strarray = str.Split('_');
                str = strarray[1];
                existIcu.Add(str);
            }

            return existIcu;
        }

        private static List<string> getExistingFiles(string pathInput_out, int version)
        {
            if (!Directory.Exists(pathInput_out))
            {
                return null;
            }
            var files = Directory.GetFiles(pathInput_out, "*.csv");
            List<String> existIcu = new List<string>();
            foreach (var file in files)
            {
                string str = file.Substring(file.LastIndexOf(@"\") + 1);
                str = str.Substring(0, str.LastIndexOf("."));
                Console.WriteLine(str);
                existIcu.Add(str);
            }

            return existIcu;
        }

        #endregion

        #region 4. 判断该ICU文件是否存在
        /// <summary>
        /// 判断该ICU文件是否存在
        /// </summary>
        /// <param name="existIcu">存在文件列表</param>
        /// <param name="id">ICUstay ID</param>
        /// <returns></returns>
        private static bool isIcuStayisExists(List<String> existIcu, String id)
        {
            if (existIcu == null)
                return false;

            foreach (string pipei in existIcu)
            {
                if (pipei == id)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 5. 构建实验所需的Chart数据
        /// <summary>
        /// 构建实验所需的Chart数据
        /// </summary>
        /// <param name="dbsource">表来源</param>
        /// <param name="icustayid"></param>
        /// <param name="chartFeaturesLevel">选择前多少个</param>
        /// <returns></returns>
        private static DataTable getChartData(string icustayid, int chartFeaturesLevel)
        {
            string sql = "SELECT	itemid,charttime,\"value\",valuenum,valueuom " +
                          " FROM	mimiciii.chartevents WHERE	icustay_id = " + icustayid +
                          "	AND itemid IN ( SELECT itemid FROM mimiciii.\"sup_vars_ChartEvent_Rank\" WHERE \"isNumber\" = '1' ORDER BY countnum DESC LIMIT " + chartFeaturesLevel + " ) " +
                          "ORDER BY	charttime;";

            //手工数据源
            //string sql = "SELECT	itemid,charttime,\"value\",valuenum,valueuom " +
            //  " FROM	mimiciii.chartevents WHERE	icustay_id = " + icustayid +
            //  "	AND itemid IN ( select item_id from mimiciii.sup_missing_features WHERE cat ="+GlobalVars.CURRENTICDCODE+" and ctype='chart' ) " +
            //  "ORDER BY	charttime;";

            return PGSQLHELPER.excuteDataTable(sql);
        }
        #endregion

        #region 6. 构建ChartEvent时间序列
        /// <summary>
        /// 6. 构建ChartEvent时间序列
        /// </summary>
        /// <param name="chartTable">个人Chart信息</param>
        /// <returns></returns>
        private static DataSet buildChartSequence(DataTable chartTable, PatientNode patient)
        {
            if (chartTable.Rows.Count < 1)
            {
                return null;
            }

            DataSet ds = new DataSet();

            DateTime startTime = Convert.ToDateTime(chartTable.Rows[0]["charttime"]);
            DateTime endTime = Convert.ToDateTime(chartTable.Rows[chartTable.Rows.Count-1]["charttime"]);

            //chart数据
            DataTable resultTable = new DataTable();
            resultTable.Columns.Add(new DataColumn("seqId"));
            resultTable.Columns.Add(new DataColumn("starttime"));
            resultTable.Columns.Add(new DataColumn("endtime"));






            //#构建chartEvent的输出表格
            if (GlobalVars.CHARTFETURETABLE.Rows.Count < 1)
            {
                string sql = "select itemid from mimiciii.\"sup_vars_ChartEvent_Rank\" where \"isNumber\" = '1' and itemid in (\n" +
                            "select itemid from chartevents where icustay_id in(\n" +
                            "				SELECT	icustay_id FROM	mimiciii.icustays WHERE	hadm_id IN ( \n" +
                            "				SELECT hadm_id FROM mimiciii.v_exp_death_diagnoses_details  WHERE icd9_code = '" + GlobalVars.CURRENTICDCODE + "' and seq_num<= " + GlobalVars.DISEASERELATEDLEVEL + "\n" +
                            "				)\n" +
                            ")\n" +
                            ")ORDER BY countnum DESC LIMIT " + GlobalVars.CHARTFEATURENUM;
                GlobalVars.CHARTFETURETABLE = PGSQLHELPER.excuteDataTable(sql);
            }


            foreach (DataRow dr in GlobalVars.CHARTFETURETABLE.Rows)
            {
                resultTable.Columns.Add(new DataColumn(dr[0].ToString()));
            }

            

            //##构建序列
            int sequenceCount = (int)Math.Ceiling((endTime.Subtract(startTime).TotalMinutes)/GlobalVars.TIMESPAN);
            if (startTime.Equals(endTime))
            {
                sequenceCount = 1;
            }

            for(int i=0; i < sequenceCount; i++)
            {
                DataRow newrow = resultTable.NewRow();
                newrow["seqId"] = (i+1);
                newrow["starttime"] = startTime.AddMinutes(i * GlobalVars.TIMESPAN);
                newrow["endtime"] = startTime.AddMinutes((i + 1) * GlobalVars.TIMESPAN);
                resultTable.Rows.Add(newrow);
            }

            //添加额外辅助信息（性别年龄身高体重等。。。）
            resultTable = addBasicInfo(resultTable, patient);

           // DataTableTools.SaveCSV(resultTable, @"C:\Users\uqzshi3\Desktop\data.csv");

            //chart时间
            DataTable chartTimeTable = new DataTable();
            chartTimeTable = resultTable.Copy();

            //数据
            foreach (DataRow dr in chartTable.Rows)
            {
                DateTime chartTime = (DateTime)dr["charttime"];
                int pos = getChartSequenceIndex(resultTable, chartTime);
                if (pos != -9)
                {
                    resultTable.Rows[pos][dr["itemid"].ToString()] = dr["valuenum"];
                    chartTimeTable.Rows[pos][dr["itemid"].ToString()] = dr["charttime"];
                }
            }

            foreach(DataRow dr in chartTimeTable.Rows)
            {
                dr["gender"] = dr["starttime"];
                dr["age"] = dr["starttime"];
                dr["height"] = dr["starttime"];
                dr["weight"] = dr["starttime"];
                dr["los"] = dr["starttime"];
                dr["relatedLevel"] = dr["starttime"];
            }

            ds.Tables.Add(resultTable);
            ds.Tables.Add(chartTimeTable);


            //DataTableTools.SaveCSV(ds.Tables[0], @"C:\Users\uqzshi3\Desktop\data.csv");
            //DataTableTools.SaveCSV(ds.Tables[1], @"C:\Users\uqzshi3\Desktop\time.csv");

            return ds;
        }
        #endregion

        #region 7. 查找当前时间在Chart序列中的位置
        /// <summary>
        /// 查找当前时间在Chart序列中的位置
        /// </summary>
        /// <param name="chartSequenceTable">Chart序列</param>
        /// <param name="charttime">需要查找的时间</param>
        /// <returns></returns>
        private static int getChartSequenceIndex(DataTable chartSequenceTable, DateTime charttime)
        {
            int result = -9;
            DateTime beginTime;
            DateTime endTime;
            for(int i=0;i<chartSequenceTable.Rows.Count; i++)
            {
                beginTime = Convert.ToDateTime( chartSequenceTable.Rows[i]["starttime"]);
                endTime = Convert.ToDateTime(chartSequenceTable.Rows[i]["endtime"]);

                if(charttime.CompareTo(beginTime)>=0 && charttime.CompareTo(endTime) <= 0)
                {
                    return i;
                }
            }

            return result;
        }

        private static List<int> getChartSequenceIndex(DataTable chartSequenceTable, DateTime stime, DateTime etime)
        {
            DateTime beginTime;
            DateTime endTime;
            List<int> poslist = new List<int>();
            for (int i = 0; i < chartSequenceTable.Rows.Count; i++)
            {
                beginTime = Convert.ToDateTime(chartSequenceTable.Rows[i]["starttime"]);
                endTime = Convert.ToDateTime(chartSequenceTable.Rows[i]["endtime"]);

                if ((stime.CompareTo(endTime) <= 0 && endTime.CompareTo(etime) <= 0) ||(stime.CompareTo(beginTime)>=0&&etime.CompareTo(endTime)<=0))
                {
                    poslist.Add(i);
                }
            }

            return poslist;
        }


        #endregion

        #region 8. 缺失数据填充主方法
        /// <summary>
        /// 缺失数据填充主方法
        /// </summary>
        /// <param name="seriesTable">需要填充的数据表</param>
        /// <param name="filltype">填充方法</param>
        /// <returns></returns>
        private static DataTable fillSeries(DataTable seriesTable,  MISSINGFILLTYPE filltype, int kNear, EVENTTYPE eVENTTYPE = EVENTTYPE.allEvent)
        {
            if(filltype== MISSINGFILLTYPE.CopyPrevious)
            {
                seriesTable = copyFill(seriesTable);
            }
            if(filltype == MISSINGFILLTYPE.SampleMean)
            {
                seriesTable = sampleMeanFill(seriesTable);
            }

            if(filltype == MISSINGFILLTYPE.WholeMean)
            {
                seriesTable = totalMeanFill(seriesTable, eVENTTYPE);
            }
            if(filltype == MISSINGFILLTYPE.KNearMean)
            {
                seriesTable = kNearMeanFill(seriesTable, kNear);
            }
            

            return seriesTable;
        }
        #endregion

        #region 8.1 缺失值补全方法-照抄上一时刻的值
        /// <summary>
        /// 缺失值补全方法-照抄上一时刻的值
        /// </summary>
        /// <param name="seriesTable">需要填充的数据表</param>
        /// <returns>填充完成的数据</returns>
        private static DataTable copyFill(DataTable seriesTable)
        {
            int totalLineCount = seriesTable.Rows.Count;


            DataRow tempDr = seriesTable.NewRow();
            tempDr.ItemArray = seriesTable.Rows[0].ItemArray;

            for (int i = 1; i < totalLineCount; i++)
            {
                for (int j = 0; j < seriesTable.Columns.Count; j++)
                {
                    if (seriesTable.Rows[i][j].ToString().Trim() == "")
                    {
                        seriesTable.Rows[i][j] = tempDr[j];
                    }
                }
                tempDr.ItemArray = seriesTable.Rows[i].ItemArray;
            }

            return seriesTable;
        }
        #endregion

        #region 8.2 缺失值补全方法-样本均值
        /// <summary>
        /// 缺失值补全方法-样本均值
        /// </summary>
        /// <param name="seriesTable"></param>
        /// <returns></returns>
        private static DataTable sampleMeanFill(DataTable seriesTable)
        {
            int totalline = seriesTable.Rows.Count;
            DataRow meanRow = seriesTable.NewRow();
            DataRow coutRow = seriesTable.NewRow();

            //初始化
            for(int j=0; j < seriesTable.Columns.Count; j++)
            {
                coutRow[j] = meanRow[j] = 0;
            }

            //计算和
            for(int i=0; i < totalline; i++)
            {
                for(int j=1; j < seriesTable.Columns.Count; j++)
                {
                    if (seriesTable.Rows[i][j].ToString() != "")
                    {
                        try
                        {
                            coutRow[j] = (Convert.ToInt16(coutRow[j]) + 1);
                            meanRow[j] = (Convert.ToDouble(meanRow[j]) + Convert.ToDouble(seriesTable.Rows[i][j]));
                        }catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            //求均值
            for (int j = 0; j < seriesTable.Columns.Count; j++)
            {
                if (Convert.ToDouble(coutRow[j]) != 0)
                {
                    meanRow[j] = (Convert.ToDouble(meanRow[j]) / Convert.ToDouble(coutRow[j]));
                }
            }

            //补全回填
            for (int i = 0; i < totalline; i++)
            {
                for (int j = 1; j < seriesTable.Columns.Count; j++)
                {
                    if (seriesTable.Rows[i][j].ToString().Trim() == "")
                    {
                        if (meanRow[j].ToString().Trim() != string.Empty)
                        {
                            seriesTable.Rows[i][j] = meanRow[j];
                        }
                    }
                }
                
            }

            return seriesTable;
        }
        #endregion

        #region 8.3 缺失值补全方法-整体均值
        /// <summary>
        /// 缺失值补全方法-样本均值
        /// </summary>
        /// <param name="seriesTable"></param>
        /// <returns></returns>
        private static DataTable totalMeanFill(DataTable seriesTable, EVENTTYPE eVENTTYPE)
        {
            int totalline = seriesTable.Rows.Count;
            DataRow meanRow = seriesTable.NewRow();
            DataRow coutRow = seriesTable.NewRow();

            string sql = string.Empty;

            for (int j = 3; j < seriesTable.Columns.Count; j++)
            {
                string icdcode = seriesTable.Columns[j].ColumnName.ToString();
                if (eVENTTYPE == EVENTTYPE.chartEvent)
                {
                    sql = "select mean from mimiciii.\"sup_vars_ChartEvent_Rank\" where itemid="+ icdcode;
                }
                if (eVENTTYPE == EVENTTYPE.inputEvent)
                {
                    sql = "select mean from mimiciii.\"sup_vars_inputEvent_Rank\" where itemid=" + icdcode;
                }

                double fillMean = -9;
                if (PGSQLHELPER.excuteSingleResult(sql).ToString() != "")
                {
                    fillMean = Convert.ToDouble(PGSQLHELPER.excuteSingleResult(sql));
                }
               

                for (int i = 0; i < totalline; i++)
                {
                    if (seriesTable.Rows[i][j].ToString().Trim() == "")
                    {
                        if (fillMean != -9)
                        {
                            seriesTable.Rows[i][j] = fillMean;
                        }
                    }
                }
            }
            return seriesTable;
        }


        private static DataTable totalMeanFill(DataTable seriesTable)
        {
            int totalline = seriesTable.Rows.Count;
            DataRow meanRow = seriesTable.NewRow();
            DataRow coutRow = seriesTable.NewRow();

            string sql = string.Empty;

            for (int j = 1; j < seriesTable.Columns.Count; j++)
            {
                
                if (j < 7)
                {
                    if (seriesTable.Rows[0][j].ToString() != string.Empty)
                    {
                        double filla = Convert.ToDouble(seriesTable.Rows[0][j]);
                        for (int i = 0; i < totalline; i++)
                        {
                            if (seriesTable.Rows[i][j].ToString().Trim() == "")
                            {
                                seriesTable.Rows[i][j] = filla;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }                   
                    continue;
                }

                string icdcode = seriesTable.Columns[j].ColumnName.ToString();


                sql = "select count(itemid) from mimiciii.\"sup_vars_ChartEvent_Rank\"";

                int exi = Convert.ToInt16(PGSQLHELPER.excuteSingleResult(sql));
                if (exi != 0)
                {
                    sql = "select mean from mimiciii.\"sup_vars_ChartEvent_Rank\" where itemid=" + icdcode;
                }
                else
                {
                    sql = "select mean from mimiciii.\"sup_vars_inputEvent_Rank\" where itemid=" + icdcode;
                }
                

                double fillMean = -9;
                if (PGSQLHELPER.excuteSingleResult(sql).ToString() != "")
                {
                    fillMean = Convert.ToDouble(PGSQLHELPER.excuteSingleResult(sql));
                }


                for (int i = 0; i < totalline; i++)
                {
                    if (seriesTable.Rows[i][j].ToString().Trim() == "")
                    {
                        if (fillMean != -9)
                        {
                            seriesTable.Rows[i][j] = fillMean;
                        }
                    }
                }
            }
            return seriesTable;
        }


        #endregion

        #region 8.4 缺失值补全方法-K邻近均值
        /// <summary>
        /// 缺失值补全方法-样本均值
        /// </summary>
        /// <param name="seriesTable"></param>
        /// <returns></returns>
        private static DataTable kNearMeanFill(DataTable seriesTable, int k)
        {
            int totalRows = seriesTable.Rows.Count;
            int totalCols = seriesTable.Columns.Count;

            for(int i=0;i<totalRows; i++)
            {
                for (int j = 3; j < totalCols; j++)
                {
                    if (seriesTable.Rows[i][j].ToString().Trim() == "")
                    {
                        double mean = 0;
                        int iter = 0;

                        for(int m = k; m > 0; m--)
                        {
                            if (i - m >= 0) 
                            {
                                if (seriesTable.Rows[(i - m)][j].ToString().Trim() != "")
                                {
                                    mean = (mean + Convert.ToDouble(seriesTable.Rows[(i - m)][j]));
                                    iter++;
                                }
                            }
                        }

                        if (iter != 0)
                        {
                            seriesTable.Rows[i][j] = (mean / iter);
                        }
                    }
                }
            }

            return seriesTable;
        }
        #endregion

        #region 9. 构建InputEvent序列
        /// <summary>
        /// 构建InputEvent序列
        /// </summary>
        /// <param name="patient">病人节点</param>
        /// <param name="chartSequence">已经构建完成的ChartEvent序列</param>
        /// <returns></returns>
        private static DataSet constructInputSequence(PatientNode patient, DataTable chartSequence)
        {

            DataTable inputSequenceTable = new DataTable();
            inputSequenceTable.Columns.Add(new DataColumn("seqId"));
            inputSequenceTable.Columns.Add(new DataColumn("starttime"));
            inputSequenceTable.Columns.Add(new DataColumn("endtime"));

            // 构建input排序，sql


            //if (GlobalVars.INPUTFETURETABLE.Rows.Count < 1)
            //{
            //    string sqll = "select itemid  from mimiciii.\"v_featureRank_input\" where itemid in (\n" +
            //                    "select itemid from mimiciii.v_exp_input_sofa_cv where icustay_id in\n" +
            //                    "(" +
            //                    "select icustay_id from mimiciii.\"v_exp_patients_sofa_Extended\" WHERE	icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	hadm_id IN ( " +
            //                    " SELECT hadm_id FROM mimiciii.v_exp_death_diagnoses_details  WHERE icd9_code = '"+GlobalVars.CURRENTICDCODE+"' and seq_num<= "+GlobalVars.DISEASERELATEDLEVEL+" )  and " +
            //                    " icustay_id in (select icustay_id from mimiciii.v_exp_chartICU)	) and los BETWEEN "+GlobalVars.ICUSTDAYMINLENGTH+" and "+GlobalVars.ICUSTDAYMAXLENGTH +
            //                    " )" +
            //                    ")or itemid in (\n" +
            //                    "select itemid from mimiciii.v_exp_input_sofa_mv where icustay_id in\n" +
            //                    "(" +
            //                    "select icustay_id from mimiciii.\"v_exp_patients_sofa_Extended\" WHERE	icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	hadm_id IN ( " +
            //                    " SELECT hadm_id FROM mimiciii.v_exp_death_diagnoses_details  WHERE icd9_code = '" + GlobalVars.CURRENTICDCODE + "' and seq_num<= " + GlobalVars.DISEASERELATEDLEVEL + " )  " +
            //                    " and icustay_id in (select icustay_id from mimiciii.v_exp_chartICU)	) and los BETWEEN " + GlobalVars.ICUSTDAYMINLENGTH + " and " + GlobalVars.ICUSTDAYMAXLENGTH +
            //                    ")" +
            //                    ") order by countnum desc limit " + GlobalVars.INPUTFEATURENUM;

               
            //    GlobalVars.INPUTFETURETABLE = PGSQLHELPER.excuteDataTable(sqll);
            //}


            foreach(DataRow dr in GlobalVars.INPUTFETURETABLE.Rows)
            {
                inputSequenceTable.Columns.Add(new DataColumn(dr["itemid"].ToString()));
            }

            //构建空表（带id，起止时间）
            foreach(DataRow dr in chartSequence.Rows)
            {
                DataRow ndr = inputSequenceTable.NewRow();
                ndr["seqId"] = dr["seqId"];
                ndr["starttime"] = dr["starttime"];
                ndr["endtime"] = dr["endtime"];
                inputSequenceTable.Rows.Add(ndr);
            }

            //单个病人的inputEvent信息
            string sql = @"select * from mimiciii.v_exp_input_sofa_" + (patient.dbsource== "carevue"?"cv":"mv")+ " where amount NOTNULL and itemid in ( select itemid  from mimiciii.\"v_featureRank_input\" ORDER BY countnum desc limit " + GlobalVars.INPUTFEATURENUM + ") and icustay_id=" + patient.icustay_id;
            DataTable inputTable = PGSQLHELPER.excuteDataTable(sql);


            //chart时间
            DataTable inputTimeTable = new DataTable();
            inputTimeTable = inputSequenceTable.Copy();

            //数据
            foreach (DataRow dr in inputTable.Rows)
            {
                DateTime starttime = (DateTime)dr["starttime"];
                DateTime endtime = (DateTime)dr["endtime"];


                if (patient.dbsource == "metavision")
                {
                    List<int> pos = getChartSequenceIndex(inputSequenceTable, starttime, endtime);
                    foreach (int i in pos)
                    {
                        inputSequenceTable.Rows[i][dr["itemid"].ToString()] = dr["amount"];
                        inputTimeTable.Rows[i][dr["itemid"].ToString()] = inputSequenceTable.Rows[i]["starttime"];
                    }
                   
                }
                if (patient.dbsource == "carevue")
                {
                    int pos = getChartSequenceIndex(inputSequenceTable, starttime);
                    if (pos != -9)
                    {
                        inputSequenceTable.Rows[pos][dr["itemid"].ToString()] = dr["amount"];
                        inputTimeTable.Rows[pos][dr["itemid"].ToString()] = dr["starttime"];
                    }
                }


            }

            DataSet rsset = new DataSet();
            rsset.Tables.Add(inputSequenceTable);
            rsset.Tables.Add(inputTimeTable);

            //DataTableTools.SaveCSV(rsset.Tables[0], @"C:\Users\uqzshi3\Desktop\inputData.csv");
            //DataTableTools.SaveCSV(rsset.Tables[1], @"C:\Users\uqzshi3\Desktop\inputTime.csv");

            return rsset;
        }
        #endregion

        #region 10. 构建实验数据集主方法（集成）
        private static void expIntergration(object datasource)
        {
            MissValueThreadParam threadParam = (MissValueThreadParam)datasource;
            String sql = string.Empty;

            //设置输出文件路径
            string pathInput_out = setOutputDir(threadParam.icd9Code);
            string pathMask = pathInput_out + @"mask\";
            string pathOriData = pathInput_out + @"oriData\";
            string pathStamp = pathInput_out + @"stamp\";
            string pathInterval = pathInput_out + @"interval\";
            string pathBursty = pathInput_out + @"bursty\";
            string pathMissingRate = pathInput_out + @"mrate\";
            string pathMissingPos = pathInput_out + @"mpos\";
            string pathMissingData = pathInput_out + @"missingData\";


            FileTools.initDirectory(pathMask);
            FileTools.initDirectory(pathOriData);
            FileTools.initDirectory(pathStamp);
            FileTools.initDirectory(pathInterval);
            FileTools.initDirectory(pathBursty);
            FileTools.initDirectory(pathMissingRate);
            FileTools.initDirectory(pathMissingPos);
            FileTools.initDirectory(pathMissingData);

           


            List<string> existIcu = getExistingFiles(pathOriData,2);

            foreach (DataRow dricu in threadParam.subExpTable.Rows)
            {
                string icustayid = dricu["icustay_id"].ToString();

                #region 1. 判断文件需要构建的文件已经存在
                //判断已经在不在
                if (isIcuStayisExists(existIcu, icustayid))
                {
                    continue;
                }
                #endregion


                #region 1p1u
                //筛除多个ICUstay的，只算最后一次
                if (GlobalVars.IS1P1U)
                {
                    if (!Utils.CommonTools.isLastICU(icustayid))
                    {
                        continue;
                    }
                }
                #endregion

                //构建当前病人信息
                PatientNode patient = new PatientNode();
                patient.AssigmentByIcuStayId(icustayid);

                string fileName = patient.icustay_id + ".csv";

                #region 3. chartEvent inputEvent
                //需要获取各种诊断数据的病人ICU序列 填充到currentTrainDataTable
                DataTable chartTable = getChartData(icustayid, GlobalVars.CHARTFEATURENUM); //获取Chart信息
                if(chartTable.Rows.Count<1)
                {
                    continue;
                }


                //构建ChartEvent序列
                DataSet chartSet = buildChartSequence(chartTable , patient);

                //开始构建缺失数据集
                List<double[]> mposlist;
                DataSet missingChartSet = randomMissing(chartSet, 3, 0.1, out mposlist);

                Output.outputPosList(mposlist, pathMissingPos + fileName);


                if (GlobalVars.INPUTSWITCH)
                {
                    //构建inputEvent序列
                    DataSet inputSet = constructInputSequence(patient, chartSet.Tables[0]);
                    //将2个序列合并
                    DataSet ciSet = combineChartInput2oneTable(missingChartSet, inputSet);
                    DataSet oriSet = combineChartInput2oneTable(chartSet, inputSet);

                    //保存原始数据

                    oriSet.Tables[0].Columns.Remove("starttime");
                    oriSet.Tables[0].Columns.Remove("endtime");

                    DataTableTools.SaveCSV(oriSet.Tables[0], (pathOriData + fileName));
                }
                else
                {
                    DataTable outOrigTable = chartSet.Tables[0].Copy();
                    outOrigTable.Columns.Remove("starttime");
                    outOrigTable.Columns.Remove("endtime");
                    DataTableTools.SaveCSV(outOrigTable, (pathOriData + fileName));
                }



                



                #endregion

                DataTable MaskDataTable = new DataTable();
                DataTable IntervalTable = new DataTable();

                if (GlobalVars.OUTPUTINTERVAL)
                {
                    IntervalTable = makeTimeInterval(missingChartSet.Tables[1]);
                    IntervalTable.Columns.Remove("starttime");
                    IntervalTable.Columns.Remove("endtime");
                }

                //去除开始结束时间
                missingChartSet.Tables[0].Columns.Remove("starttime");
                missingChartSet.Tables[0].Columns.Remove("endtime");
                missingChartSet.Tables[1].Columns.Remove("starttime");
                missingChartSet.Tables[1].Columns.Remove("endtime");

                if (GlobalVars.OUTPUTMASK)
                {
                    MaskDataTable = makeMask(missingChartSet.Tables[0]);
                }

                DataTableTools.SaveCSV(missingChartSet.Tables[0], (pathMissingData + fileName));       //缺失后的数据
                DataTableTools.SaveCSV(missingChartSet.Tables[1], (pathStamp + fileName));             //时间数据
                DataTableTools.SaveCSV(MaskDataTable, (pathMask + fileName));                   //marsk数据
                DataTableTools.SaveCSV(IntervalTable, (pathInterval + fileName));               //时间间隔的数据


                //计算bursty数据
                


                DataRow[] drarray = MathTools.calSumMeanByColumns(missingChartSet.Tables[0]);
                DataRow drStd = MathTools.calStdByColum(missingChartSet.Tables[0], drarray[1]);
                DataRow burstrow = MathTools.calBursty(missingChartSet.Tables[0],drStd,drarray[1]);

                DataTable burstyTable = makeTableByRow(burstrow, missingChartSet.Tables[0]);
                DataTableTools.SaveCSV(burstyTable, (pathBursty + fileName));       //Bursty数据

                //DataTable missingRateTable = makeTableByRow(drarray[2], chartSet.Tables[0]);
                DataTable missingRateTable = MathTools.calAccumulatedMissingRateByCol(missingChartSet.Tables[0]);
                DataTableTools.SaveCSV(missingRateTable, (pathMissingRate + fileName));       //缺失率数据




                Console.WriteLine(patient.icustay_id);
            }

        }
        #endregion

        #region 11. 计算chartEvent各feature的均值与方差
        /// <summary>
        /// 计算chartEvent各feature的均值与方差
        /// </summary>
        public static void caclChartEventMeanMSE()
        {
            string sql = "select itemid from  mimiciii.\"sup_vars_ChartEvent_Rank\"";
            DataTable itemtable = PGSQLHELPER.excuteDataTable(sql);
            foreach(DataRow dr in itemtable.Rows)
            {
                sql = "UPDATE mimiciii.\"sup_vars_ChartEvent_Rank\" set mean = (select  AVG(valuenum) from mimiciii.chartevents where itemid=" + dr[0] + ")," +
                    " mse =(select STDDEV(valuenum) from mimiciii.chartevents where itemid=" + dr[0] + ") where itemid = " + dr[0] + ";";

                PGSQLHELPER.executeScalar(sql);
            }

        }
        #endregion

        #region 12. 计算inputEvent各feature的均值与方差
        /// <summary>
        /// 计算inputEvent各feature的均值与方差
        /// </summary>
        public static void caclInputEventMeanMSE()
        {
            string sql = "select itemid,dbsource from  mimiciii.\"sup_vars_inputEvent_Rank\"";
            DataTable itemtable = PGSQLHELPER.excuteDataTable(sql);
            foreach (DataRow dr in itemtable.Rows)
            {
                if (dr[1].ToString() == "carevue")
                {
                    sql = "UPDATE mimiciii.\"sup_vars_inputEvent_Rank\" set mean = (select  AVG(amount) from mimiciii.inputevents_cv where itemid=" + dr[0] + ")," +
                        " mse =(select STDDEV(amount) from mimiciii.inputevents_cv where itemid=" + dr[0] + ") where itemid = " + dr[0] + ";";
                }

                if(dr[1].ToString() == "metavision")
                {
                    sql = "UPDATE mimiciii.\"sup_vars_inputEvent_Rank\" set mean = (select  AVG(amount) from mimiciii.inputevents_mv where itemid=" + dr[0] + ")," +
                        " mse =(select STDDEV(amount) from mimiciii.inputevents_mv where itemid=" + dr[0] + ") where itemid = " + dr[0] + ";";
                }

                


                PGSQLHELPER.executeScalar(sql);
            }

        }
        #endregion

        #region 13. 构建遮罩矩阵
        /// <summary>
        /// 构建遮罩矩阵
        /// </summary>
        /// <param name="seriseTable">需要构建的序列数据</param>
        /// <returns>构建完成的矩阵</returns>
        public static DataTable makeMask(DataTable seriseTable, EVENTTYPE etype= EVENTTYPE.allEvent)
        {

            DataTable resultTable = seriseTable.Copy();
            int rowCount = resultTable.Rows.Count;
            int colCount = resultTable.Columns.Count;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 1; j < colCount; j++)
                {
                    if (resultTable.Rows[i][j].ToString().Trim() == "")
                    {
                        resultTable.Rows[i][j] = 0;
                    }
                    else
                    {
                        resultTable.Rows[i][j] = 1;
                    }
                }
            }

            return resultTable;
        }
        #endregion

        #region 14. 制作遮罩时长
        /// <summary>
        /// 制作遮罩时长工具
        /// </summary>
        /// <param name="seriesTable">序列表</param>
        /// <returns></returns>
        public static DataTable makeTimeInterval(DataTable seriesTable)
        {
            DataTable reslultTable = seriesTable.Copy();
            
            int rowCount = reslultTable.Rows.Count;
            int colCount = reslultTable.Columns.Count;

            
            for(int j = 3; j < colCount; j++)
            {
                double interval = 0;
                DateTime lastTime;
                if (seriesTable.Rows[0][j].ToString() != string.Empty)
                {
                    lastTime = Convert.ToDateTime(seriesTable.Rows[0][j]);
                }
                else
                {
                    lastTime = DateTime.MinValue;
                }

                for (int i = 0; i < rowCount; i++)
                {
                    if(seriesTable.Rows[i][j].ToString().Trim() != string.Empty)
                    {
                        lastTime = Convert.ToDateTime(seriesTable.Rows[i][j]);
                        interval = 0;
                    }
                    else
                    {
                        if (lastTime == DateTime.MinValue)
                        {
                            interval = Convert.ToDateTime(seriesTable.Rows[i]["endtime"]).Subtract(Convert.ToDateTime(seriesTable.Rows[0]["starttime"])).TotalMinutes;
                            
                        }
                        else
                        {
                            interval = Convert.ToDateTime(seriesTable.Rows[i]["endtime"]).Subtract(lastTime).TotalMinutes;
                        }
                    }

                    reslultTable.Rows[i][j] = interval;
                }
            }

            return reslultTable;
        }
        #endregion

        #region 15.计算数据的缺失率
        /// <summary>
        /// 计算数据的缺失率
        /// </summary>
        /// <param name="originSeriesTable">未填充的序列表</param>
        public static DataTable caculateMissingRate(DataTable originSeriesTable)
        {
            double missingRate = 0;
            DataRow newRow = originSeriesTable.NewRow();
            for (int j = 3; j < originSeriesTable.Columns.Count; j++)
            {
                int num = 0;
                for(int i=0; i < originSeriesTable.Rows.Count; i++)
                {
                    if (originSeriesTable.Rows[i][j].ToString().Trim() != "")
                    {
                        num++;
                    }
                }

                missingRate = (double)num / originSeriesTable.Rows.Count;
                newRow[j] = missingRate;
            }

            originSeriesTable.Rows.Add(newRow);

            return originSeriesTable;
        }
        #endregion

        #region 16.计算突发性
        /// <summary>
        /// 计算指标的突发性
        /// </summary>
        /// <param name="seriesTable"></param>
        /// <returns></returns>
        public static DataTable caclBurstness(DataTable seriesTable)
        {
            DataTable reslultTable = seriesTable.Copy();

            int rowCount = reslultTable.Rows.Count;
            int colCount = reslultTable.Columns.Count;


            //ToDo

            for (int j = 3; j < colCount; j++)
            {
                int interval = 0;
                for (int i = 0; i < rowCount; i++)
                {
                    if (seriesTable.Rows[i][j].ToString().Trim() != "")
                    {
                        interval = 0;
                    }
                    else
                    {
                        interval++;
                    }

                    reslultTable.Rows[i][j] = interval;
                }
            }

            return reslultTable;
        }
        #endregion

        #region 17. 输出关于数据集构建的相关信息
        /// <summary>
        /// 输出关于数据集构建的先关信息
        /// </summary>
        /// <param name="filePath"></param>
        public static void outputMataData(string filePath)
        {
            string outputString = "---------------DATASET DETAILED INFORMATION---------------\r\n\r\n" +
                                  "\r\nCreate Time:\t\t\t" + System.DateTime.Now.ToString() +
                                  "\r\nIcd Code:\t\t\t" + GlobalVars.CURRENTICDCODE +
                                  "\r\nTime Span:\t\t\t" + GlobalVars.TIMESPAN +
                                  "\r\nDeath Thresholds:\t\t" + GlobalVars.DEATHCOUNTDAY +
                                  "\r\nDeath Disease Related Level:\t" + GlobalVars.DISEASERELATEDLEVEL +
                                  "\r\n1p1u:\t\t\t\t" + GlobalVars.IS1P1U +
                                  "\r\nChart Features:\t\t\t" + (GlobalVars.CHARTFEATURENUM+GlobalVars.ADDITIONFILEDCOUNT) +
                                  "\r\nInput Features:\t\t\t" + GlobalVars.INPUTFEATURENUM +
                                  "\r\nMin LOS:\t\t\t" + GlobalVars.ICUSTDAYMINLENGTH +
                                  "\r\nMax LOS:\t\t\t" + GlobalVars.ICUSTDAYMAXLENGTH;

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            filePath += "readme.txt";

            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(fileStream);


            writer.Write(outputString);
            writer.Close();
            fileStream.Close();
        }
        #endregion

        #region 18. 添加一些病人相关的基础信息
        /// <summary>
        /// 添加一些病人相关的基础信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        public static DataTable addBasicInfo(DataTable dt, PatientNode patient)
        {
            DataColumn genderCol = dt.Columns.Add("gender", Type.GetType("System.String"));
            DataColumn ageCol = dt.Columns.Add("age", Type.GetType("System.String"));
            DataColumn heightCol = dt.Columns.Add("height", Type.GetType("System.String"));
            DataColumn weightCol = dt.Columns.Add("weight", Type.GetType("System.String"));
            DataColumn losCol = dt.Columns.Add("los", Type.GetType("System.String"));
            DataColumn relatedLevel = dt.Columns.Add("relatedLevel", Type.GetType("System.String"));

            int gender = patient.gender == "M" ? 1 : 0;

            String sql = @"select seq_num from mimiciii.diagnoses_icd where hadm_id in (select hadm_id from mimiciii.icustays where icustay_id='" + patient.icustay_id+"') and icd9_code ='"+GlobalVars.CURRENTICDCODE+"'";
            string rLevel = PGSQLHELPER.excuteSingleResult(sql);

            foreach (DataRow dr in dt.Rows)
            {
                dr["gender"] = gender;
                dr["age"] = patient.age;
                dr["height"] = patient.height;
                dr["weight"] = patient.weight;
                dr["los"] = patient.los;
                dr["relatedLevel"] = rLevel;
            }

            genderCol.SetOrdinal(3);
            ageCol.SetOrdinal(4);
            heightCol.SetOrdinal(5);
            weightCol.SetOrdinal(6);
            losCol.SetOrdinal(7);
            relatedLevel.SetOrdinal(8);
            return dt;
        }
        #endregion

        #region 将chartevent和inputevent两张表合并
        /// <summary>
        /// 将chartevent和inputevent两张表合并
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DataSet combineChartInput2oneTable(DataSet chart, DataSet input)
        {

            input.Tables[0].Columns[0].ColumnName = "idd";
            input.Tables[0].Columns[1].ColumnName = "st1";
            input.Tables[0].Columns[2].ColumnName = "et1";

            input.Tables[1].Columns[0].ColumnName = "idd";
            input.Tables[1].Columns[1].ColumnName = "st1";
            input.Tables[1].Columns[2].ColumnName = "et1";


            int colCount = input.Tables[0].Columns.Count;
            int ccount = chart.Tables[0].Columns.Count;

            for (int i = 0; i < colCount; i++)
            {
                DataColumn dataColumn1 = new DataColumn(input.Tables[0].Columns[i].ColumnName);
                DataColumn dataColumn2 = new DataColumn(input.Tables[0].Columns[i].ColumnName);
                chart.Tables[0].Columns.Add(dataColumn1);
                chart.Tables[1].Columns.Add(dataColumn2);
            }

            int rowCount = input.Tables[0].Rows.Count;
            
            for (int i=0; i < rowCount; i++)
            {
                int k = 0;
                for(int j = ccount; j < chart.Tables[0].Columns.Count; j++)
                {
                    chart.Tables[0].Rows[i][j] = input.Tables[0].Rows[i][k];
                    chart.Tables[1].Rows[i][j] = input.Tables[1].Rows[i][k];
                    
                    k++;
                }
                
            }

            chart.Tables[0].Columns.Remove("idd");
            chart.Tables[0].Columns.Remove("st1");
            chart.Tables[0].Columns.Remove("et1");

            chart.Tables[1].Columns.Remove("idd");
            chart.Tables[1].Columns.Remove("st1");
            chart.Tables[1].Columns.Remove("et1");

            return chart;
            
        }
        #endregion

        #region 按行还原DataTable
        /// <summary>
        /// 按行还原DataTable
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="oriTable"></param>
        /// <returns></returns>
        public static DataTable makeTableByRow(DataRow dr, DataTable oriTable)
        {
            DataTable resultable = oriTable.Copy();
            int i = 1;
            foreach(DataRow ddr in resultable.Rows)
            {
                
                ddr.ItemArray = dr.ItemArray;
                ddr[0] = i++;
            }

            return resultable;
        }
        #endregion

        #region 进行数据的随机缺失
        /// <summary>
        /// 进行数据的随机缺失
        /// </summary>
        /// <param name="origSet"></param>
        /// <param name="startCol"></param>
        /// <param name="missingRate"></param>
        /// <param name="poslist"></param>
        /// <returns></returns>
        public static DataSet randomMissing(DataSet  origSet, int startCol , double missingRate, out List<double []> poslist)
        {
            DataSet dataSet = origSet.Copy();

            int colcount = GlobalVars.CHARTFEATURENUM+startCol;
            int rowCount = origSet.Tables[0].Rows.Count;
            DataRow mrow = origSet.Tables[0].NewRow();

            int totalNum = 0;

            for(int j=startCol;j<colcount; j++)
            {
                for(int i = 0; i < rowCount; i++)
                {
                    if (origSet.Tables[0].Rows[i][j].ToString() != string.Empty)
                    {
                        totalNum++;
                    }
                }
            }

            int missNum = (int)(totalNum * missingRate);

            Random rad = new Random();
            List<double[]> positionList = new List<double[]>();
            for(int k = 0; k < missNum; k++)
            {
                while (true)
                {
                    int x = rad.Next(0, rowCount - 1);
                    int y = rad.Next(startCol, colcount - 1);

                    if (dataSet.Tables[0].Rows[x][y].ToString() != string.Empty)
                    {
                        double[] add = new double[3] { x, y, Convert.ToDouble(dataSet.Tables[0].Rows[x][y]) };
                        dataSet.Tables[0].Rows[x][y] = "";
                        dataSet.Tables[1].Rows[x][y] = "";
                        
                        positionList.Add(add);
                        break;
                    }
                }
            }
            poslist = positionList;
            return dataSet;
        }
        #endregion

        #region 计算整个清单的MAE与MRE
        /// <summary>
        /// 计算整个清单的MAE与MRE
        /// </summary>
        /// <param name="labelPath"></param>
        /// <param name="missingPath"></param>
        /// <param name="posPath"></param>
        public static void caclMAEMRE(string labelPath, string missingPath, string posPath, string savePath)
        {
            DataTable lableTable = new DataTable();
            lableTable = DataTableTools.OpenCSV(labelPath);

            DataTable resultTable = new DataTable();
            resultTable.Columns.Add(new DataColumn("icustay_id"));
            resultTable.Columns.Add(new DataColumn("mae"));
            resultTable.Columns.Add(new DataColumn("mre"));
            double[] mare = new double[2];

            string pathInput_out = setOutputDir(GlobalVars.CURRENTICDCODE);
            string fillMissingData = pathInput_out + @"sampleMeanFill\";
            FileTools.initDirectory(fillMissingData);
            foreach (DataRow dr in lableTable.Rows)
            {
                DataTable misTable = new DataTable();
                misTable = DataTableTools.OpenCSV(missingPath + dr["icustay_id"] + ".csv");


               //  misTable = sampleMeanFill(misTable); //
               // misTable = copyFill(misTable);
                misTable = totalMeanFill(misTable);
               // DataTableTools.SaveCSV(misTable, fillMissingData + dr["icustay_id"] + ".csv");



                DataTable posTable = new DataTable();
                posTable = DataTableTools.OpenCSV(posPath + dr["icustay_id"] + ".csv");

                mare = inCalMAEMRE(misTable, posTable);


                DataRow ndr = resultTable.NewRow();
                ndr["icustay_id"] = dr["icustay_id"];
                ndr["mae"] = mare[0];
                ndr["mre"] = mare[1];
                resultTable.Rows.Add(ndr);
                Console.WriteLine(dr["icustay_id"] + "," + mare[0] +"," + mare[1]);

            }

            DataTableTools.SaveCSV(resultTable, savePath + GlobalVars.CURRENTICDCODE+"_totalMean_mare.csv");

        }
        #endregion

        #region 计算单一文件的MAEmRE
        /// <summary>
        /// 计算单一文件的MAEmRE
        /// </summary>
        /// <param name="fillTable">填充后的table信息</param>
        /// <param name="posTable">原始位置信息</param>
        /// <returns>[0]:MAE;[1]:MRE</returns>
        private static double[] inCalMAEMRE(DataTable fillTable, DataTable posTable)
        {
            double mae = 0;
            double mre = 0;
            double lablesum = 0;

            foreach(DataRow dr in posTable.Rows)
            {
                int row = Convert.ToInt16(dr["rowpos"]);
                int col = Convert.ToInt16(dr["colpos"])-2;
                double value = Convert.ToDouble(dr["value"]);
                double fillvaue =0;
                if (fillTable.Rows[row][col].ToString().Trim() != string.Empty)
                {
                    fillvaue = Convert.ToDouble(fillTable.Rows[row][col]);
                }

                mae = mae + Math.Abs(fillvaue - value);
                lablesum = lablesum + Math.Abs( value);
            }


            mre = mae / lablesum;
            mae = mae / posTable.Rows.Count;
            

            return new double[2] {mae,mre };
        }
        #endregion

    }
}
