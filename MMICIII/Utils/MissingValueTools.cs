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
        #region 0. 获取实验数据主方法
        /// <summary>
        /// 获取实验数据主方法
        /// </summary>
        /// <param name="icd9code"></param>
        public static void constructExpData(String icd9code)
        {

            DataSet runSet = DataTableTools.divideDataTable(getExplist(icd9code,GlobalVars.DISEASERELATEDLEVEL,GlobalVars.ICUSTDAYMINLENGTH,GlobalVars.ICUSTDAYMAXLENGTH), GlobalVars.threadCount);

            foreach (DataTable singleTb in runSet.Tables)
            {
                Thread expThread = new Thread(new ParameterizedThreadStart(expIntergration));
                MissValueThreadParam threadParam = new MissValueThreadParam(icd9code, singleTb,  GlobalVars.TIMESPAN);
                expThread.Start(threadParam);
            }
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
                          "icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	subject_id IN ( SELECT subject_id FROM mimiciii.v_exp_death_diagnoses_details  WHERE icd9_code like '" +
                          icd9 + "%' and seq_num<= "+ relatedLevel + " ) 	) and los BETWEEN " + icuStayMin + " and " + icuStayMax + ";";

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
        private static string setOutputDir(string icd9)
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
                          " FROM	mimiciii.chartevents WHERE	icustay_id = "+icustayid +
                          "	AND itemid IN ( SELECT itemid FROM mimiciii.\"sup_vars_ChartEvent_Rank\" WHERE \"isNumber\" = '1' ORDER BY countnum DESC LIMIT "+chartFeaturesLevel+" ) " +
                          "ORDER BY	charttime;";

            return PGSQLHELPER.excuteDataTable(sql);
        }
        #endregion

        #region 6. 构建ChartEvent时间序列
        /// <summary>
        /// 6. 构建ChartEvent时间序列
        /// </summary>
        /// <param name="chartTable">个人Chart信息</param>
        /// <returns></returns>
        private static DataTable buildChartSequence(DataTable chartTable)
        {
            if (chartTable.Rows.Count < 1)
            {
                return null;
            }
               

            DateTime startTime = Convert.ToDateTime(chartTable.Rows[0]["charttime"]);
            DateTime endTime = Convert.ToDateTime(chartTable.Rows[chartTable.Rows.Count-1]["charttime"]);

            DataTable resultTable = new DataTable();
            resultTable.Columns.Add(new DataColumn("seqId"));
            resultTable.Columns.Add(new DataColumn("starttime"));
            resultTable.Columns.Add(new DataColumn("endtime"));

            string sql = "SELECT itemid FROM mimiciii.\"sup_vars_ChartEvent_Rank\" WHERE \"isNumber\" = '1' ORDER BY countnum DESC LIMIT "+GlobalVars.CHARTFEATURENUM;
            DataTable chartFeatureFiled = PGSQLHELPER.excuteDataTable(sql);
            //#构建chartEvent的输出表格
            foreach(DataRow dr in chartFeatureFiled.Rows)
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

            //数据
            foreach(DataRow dr in chartTable.Rows)
            {
                DateTime chartTime = (DateTime)dr["charttime"];
                int pos = getChartSequenceIndex(resultTable, chartTime);
                resultTable.Rows[pos][dr["itemid"].ToString()] = dr["valuenum"];
            }
            return resultTable;
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
            int result = 0;
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
                for(int j=3; j < seriesTable.Columns.Count; j++)
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
                for (int j = 3; j < seriesTable.Columns.Count; j++)
                {
                    if (seriesTable.Rows[i][j].ToString().Trim() == "")
                    {
                        if (meanRow[j].ToString() != "0")
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
        private static DataTable constructInputSequence(PatientNode patient, DataTable chartSequence)
        {

            DataTable inputSequenceTable = new DataTable();
            inputSequenceTable.Columns.Add(new DataColumn("seqId"));
            inputSequenceTable.Columns.Add(new DataColumn("starttime"));
            inputSequenceTable.Columns.Add(new DataColumn("endtime"));

            // 构建input排序，sql
            string sql = "select itemid,dbsource  from mimiciii.\"v_featureRank_input\" ORDER BY countnum desc limit " + GlobalVars.INPUTFEATURENUM;

            DataTable inputFeaturesTable = PGSQLHELPER.excuteDataTable(sql);
            foreach(DataRow dr in inputFeaturesTable.Rows)
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
            sql = @"select * from mimiciii.v_exp_input_sofa_" + (patient.dbsource== "carevue"?"cv":"mv")+ " where amount NOTNULL and itemid in ( select itemid  from mimiciii.\"v_featureRank_input\" ORDER BY countnum desc limit " + GlobalVars.INPUTFEATURENUM + ") and icustay_id=" + patient.icustay_id;
            DataTable inputTable = PGSQLHELPER.excuteDataTable(sql);

            foreach (DataRow dr in inputTable.Rows)
            {
                int pos = getChartSequenceIndex(inputSequenceTable, Convert.ToDateTime(dr["starttime"]));
                inputSequenceTable.Rows[pos][dr["itemid"].ToString()] = dr["amount"];
            }


            return inputSequenceTable;
        }
        #endregion

        #region 10. 构建实验数据集主方法
        private static void expIntergration(object datasource)
        {
            MissValueThreadParam threadParam = (MissValueThreadParam)datasource;
            String sql = string.Empty;

            //设置输出文件路径
            string pathInput_out = setOutputDir(threadParam.icd9Code);
            string pathMask = pathInput_out + @"maksk\";
            string pathData = pathInput_out + @"data\";
            if (GlobalVars.OUTPUTMASK)
            {
                
                if (!Directory.Exists(pathMask))
                {
                    Directory.CreateDirectory(pathMask);
                }
            }
            string pathInterval = pathInput_out + @"interval\";
            if (GlobalVars.OUTPUTINTERVAL)
            {
                

                if (!Directory.Exists(pathInterval))
                {
                    Directory.CreateDirectory(pathInterval);
                }
            }

            List<string> existIcu = getExistingFiles(pathData);


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

                #region 2. 1p1u
                //筛除多个ICUstay的，只算最后一次
                if (GlobalVars.IS1P1U) {
                    if (!Utils.CommonTools.isLastICU(dricu["icustay_id"].ToString()))
                    {
                        continue;
                    }
                }
                #endregion

                #region 3. chartEvent
                //需要获取各种诊断数据的病人ICU序列 填充到currentTrainDataTable
                DataTable chartTable = getChartData(icustayid, GlobalVars.CHARTFEATURENUM); //获取Chart信息

                if(chartTable.Rows.Count<1)
                {
                    continue;
                }

                //构建ChartEvent序列
                DataTable chartSequenceTable = buildChartSequence(chartTable); //构建ChartEvent序列
                #endregion

                #region 4. inputEvent
                //生产当前病人信息
                PatientNode patient = new PatientNode();
                patient.AssigmentByIcuStayId(icustayid);
                
                //构建inputEvent序列
                DataTable inputSequenceTable = constructInputSequence(patient, chartSequenceTable);
                #endregion


                

                string fileName = ((patient.isDead) ? "0_" : "1_") + patient.icustay_id + "_" + patient.gender + "_" + patient.age + ".csv";

                if (GlobalVars.OUTPUTMASK)
                {
                    DataTable chartMaskDataTable = makeMask(chartSequenceTable);
                    DataTable inputMaskDataTable = makeMask(inputSequenceTable);

                    DataTableTools.SaveCSV_2TB(chartMaskDataTable, inputMaskDataTable, (pathMask + fileName));

                }

                if (GlobalVars.OUTPUTINTERVAL)
                {
                    DataTable chartIntervalTable = makeTimeInterval(chartSequenceTable);
                    DataTable inputIntervalTable = makeTimeInterval(inputSequenceTable);

                    DataTableTools.SaveCSV_2TB(chartIntervalTable, inputIntervalTable, (pathInterval + fileName));
                }

                //计算缺失率

                chartSequenceTable = caculateMissingRate(chartSequenceTable);
                inputSequenceTable = caculateMissingRate(inputSequenceTable);


                //填充缺失值
                if (GlobalVars.FILLMISSING)
                {
                    chartSequenceTable = fillSeries(chartSequenceTable, GlobalVars.FillMissingTpe,GlobalVars.KNEAR, EVENTTYPE.chartEvent); //chartEvent缺失值填充
                    inputSequenceTable = fillSeries(inputSequenceTable, GlobalVars.FillMissingTpe, GlobalVars.KNEAR, EVENTTYPE.inputEvent);//填充inputEvent
                }
                
                if (!Directory.Exists(pathData))
                {
                    Directory.CreateDirectory(pathData);
                }
                DataTableTools.SaveCSV_2TB(chartSequenceTable, inputSequenceTable, ( pathData+ fileName));


                


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
        public static DataTable makeMask(DataTable seriseTable)
        {

            DataTable resultTable = seriseTable.Copy();
            int rowCount = resultTable.Rows.Count;
            int colCount = resultTable.Columns.Count;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 3; j < colCount; j++)
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
                int interval = 0;
                for (int i = 0; i < rowCount; i++)
                {
                    if(seriesTable.Rows[i][j].ToString().Trim() != "")
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

        public static void outputMataData(string filePath)
        {
            string outputString = "---------------DATASET DETAILED INFORMATION---------------\r\n\r\n" +
                                  "\r\nIcd Code:\t\t\t" + GlobalVars.CURRENTICDCODE +
                                  "\r\nTime Span:\t\t\t" + GlobalVars.TIMESPAN +
                                  "\r\nDeath Thresholds:\t\t" + GlobalVars.DEATHCOUNTDAY +
                                  "\r\nDeath Disease Related Level:\t" + GlobalVars.DISEASERELATEDLEVEL +
                                  "\r\n1p1u:\t\t\t\t" + GlobalVars.IS1P1U +
                                  "\r\nChart Features:\t\t\t" + GlobalVars.CHARTFEATURENUM +
                                  "\r\nInput Features:\t\t\t" + GlobalVars.INPUTFEATURENUM +
                                  "\r\nMin LOS:\t\t\t" + GlobalVars.ICUSTDAYMINLENGTH +
                                  "\r\nMax LOS:\t\t\t" + GlobalVars.ICUSTDAYMAXLENGTH;

                                  ;
            ;

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
    }
}
