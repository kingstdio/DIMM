using MMICIII.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MMICIII
{
    public enum SCORESYSTEMTYPE {APACHEII,SOFA,SOFAEXTENDED}
    public partial class F_main : Form
    {
        private string filePath = Application.StartupPath + @"\data\APACHE II Train-1.csv";
        private string sql = string.Empty;
        private int threadCounter = 0;//计数器
        //private DataTable currentTrainDataTable;
        //private SequenceNode fillNode = new SequenceNode();
        List<ChartNode> chartList_Bt_use = new List<ChartNode>();
        List<SequenceNode> squenceList_Bt_use = new List<SequenceNode>();
        List<string> removeFileList = new List<string>();

        /// <summary>
        /// 需要划分训练集与测试机的基础目录
        /// </summary>
        private string divide_base = string.Empty;

        private string divide_file = string.Empty;

        private SCORESYSTEMTYPE sCORESYSTEMTYPE;

        #region 初始化
        public F_main()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            comb_scoreSystemType.SelectedIndex = 0;
            sCORESYSTEMTYPE = SCORESYSTEMTYPE.APACHEII;
            GlobalVars.sofaMapDic = Utils.FilterTools.loadSofaExtendedMapDic(@"./data/sofadic.csv");
        }
        #endregion


    

        #region 计算分数
        private void tb_calcScore_Click(object sender, EventArgs e)
        {
            rtb_outBox.AppendText("Begin to Calculating Score \r\n");
            squenceList_Bt_use.Clear();
            IEnumerable<IGrouping<int, ChartNode>> query = from chartNode in chartList_Bt_use group chartNode by chartNode.timeSqid;

            if(sCORESYSTEMTYPE== SCORESYSTEMTYPE.APACHEII)
            {
                foreach (IGrouping<int, ChartNode> chartGropu in query)
                {
                    squenceList_Bt_use.Add(SCOREHELPER.apacheii_score(chartGropu));
                }
            }

            if(sCORESYSTEMTYPE == SCORESYSTEMTYPE.SOFA)
            {
                foreach (IGrouping<int, ChartNode> chartGropu in query)
                {
                    squenceList_Bt_use.Add(SCOREHELPER.sofa_score(chartGropu));
                }
            }
            rtb_outBox.AppendText("Calculating Finished \r\n");
            lb_score.Text = "评分完毕";
        }
        #endregion

        #region 输出结果

        private void bt_viewResult_Click(object sender, EventArgs e)
        {
            ouputSquence();
        }

        public void ouputSquence()
        {
            rtb_outBox.Clear();


            rtb_outBox.AppendText("seqId \t totalScore \t itemCount");
            rtb_outBox.AppendText("\r\n");
            foreach (SequenceNode node in squenceList_Bt_use)
            {
                string strLine = node.seqId + " \t ";
                strLine += node.totalScore + " \t " + node.itemCount;
                rtb_outBox.AppendText(strLine);
                rtb_outBox.AppendText("\r\n");
            }
        }
        #endregion


        #region 评分补全
        private void bt_fill_Click(object sender, EventArgs e)
        {
            rtb_outBox.AppendText("Filling Score... \r\n");
            Utils.FillScore.fillScoreList(ref squenceList_Bt_use);
            rtb_outBox.AppendText("Done \r\n");
        }
        #endregion

        #region 将计算结果写入CSV
        /// <summary>
        /// 将计算结果写入CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_writeCSV_Click(object sender, EventArgs e)
        {
            switch(sCORESYSTEMTYPE)
            {
                case SCORESYSTEMTYPE.APACHEII:
                    {
                        Utils.Output.CsvWrite(@"C:\Users\uqzshi3\Desktop\"+GlobalVars.currentPatient.icustay_id+"_APACHEII_SCORE.csv", squenceList_Bt_use, SCORESYSTEMTYPE.APACHEII);
                        break;
                    }
                case SCORESYSTEMTYPE.SOFA:
                    {
                        Utils.Output.CsvWrite(@"C:\Users\uqzshi3\Desktop\"+GlobalVars.currentPatient.icustay_id+"_SOFA_SCORE.csv", squenceList_Bt_use, SCORESYSTEMTYPE.SOFA);
                        break;
                    }
            }
            rtb_outBox.AppendText("Writing CSV Completed \r\n");
        }
        #endregion

        #region 构建SOFAfeature
        /// <summary>
        /// 构建SOFAfeature
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_constructSofaFeature_Click(object sender, EventArgs e)
        {
            SofaFeature.constructSofaFeatures();
            MessageBox.Show("SOFA特征构建完毕", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region 构建评分系统所需要的数据
        private void tb_conApacheII_Click(object sender, EventArgs e)
        {
            rtb_outBox.AppendText("Begin to build Sample Data \r\n");
            String icustayid = tb_icustayid.Text.Trim();
            DataTable currentTrainDataTable = new DataTable();
            switch (sCORESYSTEMTYPE)
            {
                case SCORESYSTEMTYPE.APACHEII:
                    {
                        rtb_outBox.AppendText("Using APACHEII Scoring System \r\n");
                        currentTrainDataTable = getExpData("mimiciii.sup_vars_apacheii",icustayid);
                        tss_scoreSystem.Text = "APACHEII";
                        break;
                    }
                case SCORESYSTEMTYPE.SOFA:
                    {
                        rtb_outBox.AppendText("Using SOFA Scoring System \r\n");
                        currentTrainDataTable = getExpData("mimiciii.sup_vars_sofa",icustayid);
                        tss_scoreSystem.Text = "SOFA";                    
                        break;
                    }
            }
            rtb_outBox.AppendText("Building Chart List \r\n");

            chartList_Bt_use =  ChartTools.buildChartList(currentTrainDataTable);

            rtb_outBox.AppendText("Divide sequence time span："+GlobalVars.TIMESPAN+"minutes \r\n");
            DateTime startTime = chartList_Bt_use[0].charttime;

            foreach (ChartNode node in chartList_Bt_use)
            {
                node.timeSqid = (int)(node.charttime.Subtract(startTime).TotalMinutes / GlobalVars.TIMESPAN) + 1;
            }

            lb_divided.Text = "切分完毕共：" + chartList_Bt_use[chartList_Bt_use.Count - 1].timeSqid + "个序列";
            rtb_outBox.AppendText("Done Sequence item count:"+ chartList_Bt_use[chartList_Bt_use.Count - 1].timeSqid + " \r\n");
        }

        #region 构建实验所需的Chart数据
        /// <summary>
        /// 构建实验所需的Chart数据
        /// </summary>
        /// <param name="dbsource">表来源</param>
        /// <param name="icustayid"></param>
        /// <returns></returns>
        private DataTable getExpData(String dbsource, string icustayid)
        {

            sql = @"select chartevents.* , d_items.label, item_unit 
                    from mimiciii.chartevents, mimiciii.d_items, " + dbsource + "  where chartevents.itemid=d_items.itemid and chartevents.itemid=" + dbsource + ".item_id and icustay_id =" + icustayid 
                  + " and chartevents.itemid in (select item_id from " + dbsource + ") ORDER BY charttime;";
            return PGSQLHELPER.excuteDataTable(sql);
        }
        #endregion

        #endregion

        #region  选择评分系统
        private void comb_scoreSystemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string scoreSystemStr = comb_scoreSystemType.Text;
            switch (scoreSystemStr)
            {
                case "APACHEII":sCORESYSTEMTYPE = SCORESYSTEMTYPE.APACHEII;break;
                case "SOFA": sCORESYSTEMTYPE = SCORESYSTEMTYPE.SOFA; break;
                default: sCORESYSTEMTYPE = SCORESYSTEMTYPE.APACHEII; break;
            }
        }
        #endregion

        private bool isIcuStayisExists(List<String> existIcu, String id)
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

        

        private int  getIndex(DataTable datatable, string seqId)
        {
            for(int i=0; i<datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i]["seqId"].ToString() == seqId)
                {
                    return i;
                }
            }

            return 0;
        }

        private void bt_cunstructICUStay_Click(object sender, EventArgs e)
        {
            Utils.SofaTools.constructSofaExpICustay();
        }

        #region 补全序列按钮
        private void bt_completion_Click(object sender, EventArgs e)
        {
            string path = @"D:\MIMICIII\SOFAEXTENDED_SCORE\";
            var files = Directory.GetFiles(path, "*.csv");
            int count = 0;
            foreach(var file in files)
            {
                DataTable scoreTable = Utils.DataTableTools.OpenCSV(file);
                dgv_main.DataSource = scoreTable;
                int lostRow = 0;
                int cid = 0;
                int nid = 0;
                if (scoreTable != null)
                {
                    DataRow tempRow = scoreTable.NewRow();
                    for (int i = 0; i < scoreTable.Rows.Count; i++)
                    {
                        if (scoreTable.Rows[i][0].ToString() == string.Empty)
                        {
                            break;
                        }
                        cid = Convert.ToInt32( scoreTable.Rows[i][0]);
                        //nid = cid + 1;

                        if(i+1< scoreTable.Rows.Count)
                        {
                            nid = Convert.ToInt32(scoreTable.Rows[i+1][0]);
                        }
                        else
                        {
                            break;
                        }
                        if ((nid - cid) != 1)
                        {
                            lostRow = nid - cid - 1;
                            for (int j = 0; j < lostRow; j++)//补全
                            {
                                DataRow dr = scoreTable.NewRow();
                                dr.ItemArray = scoreTable.Rows[i].ItemArray;  
                                dr[0] = i + j + 2;
                                scoreTable.Rows.InsertAt(dr,cid+j);
                            }
                            i = i +lostRow;
                        }
                    }
                    string fileName = file.Replace("SOFAEXTENDED_SCORE", "SOFAEXTENDED_SCORE2");



                    Utils.DataTableTools.SaveCSV(scoreTable, fileName);
                    count++;
                }
                Console.WriteLine(count);
                //dgv_main.DataSource = scoreTable;
            }
        }
        #endregion

        private void bt_autosofa_Click(object sender, EventArgs e)
        {
            //选出患诊断为对应疾病的病人的ICU信息
            //sql = "select row_id, icustay_id, los as staytime, dbsource from mimiciii.\"v_exp_patients_sofa_Extended\" WHERE" +
            //    "	icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	subject_id IN ( SELECT subject_id FROM mimiciii.v_exp_death_diagnoses_details " +
            //    " WHERE icd9_code = '" + tb_icd9.Text.Trim() + "' ) 	);";


            ////住院时长压缩到10天的,带相关性的
            //sql = "select row_id, icustay_id, los as staytime, dbsource from mimiciii.\"v_exp_patients_sofa_Extended\" WHERE" +
            //      "	icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	subject_id IN ( SELECT subject_id FROM mimiciii.v_exp_death_diagnoses_details " +
            //      " WHERE icd9_code = '" + tb_icd9.Text.Trim() + "' and seq_num<=2) 	) and los<=" + GlobalVars.ICUSTDAYMAXLENGTH + ";";

            //住院时长压缩到10天的
            //sql = "select row_id, icustay_id, los as staytime, dbsource from mimiciii.\"v_exp_patients_sofa_Extended\" WHERE" +
            //      "	icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	subject_id IN ( SELECT subject_id FROM mimiciii.v_exp_death_diagnoses_details " +
            //      " WHERE icd9_code = '" + tb_icd9.Text.Trim() + "' ) 	) and los<=" + GlobalVars.ICUSTDAYMAXLENGTH + ";";

            //1p1u带level的
            //sql = "select row_id, icustay_id, los as staytime, dbsource from mimiciii.\"v_exp_patients_sofa_Extended\" WHERE	icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	subject_id IN ( SELECT subject_id FROM mimiciii.v_exp_death_diagnoses_details  WHERE icd9_code = '41401' and seq_num<=1 ) 	) and los<=10;";


            //模糊的

            //sql = "select row_id, icustay_id, los as staytime, dbsource from mimiciii.\"v_exp_patients_sofa_Extended\" WHERE" +
            //  "	icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	subject_id IN ( SELECT subject_id FROM mimiciii.v_exp_death_diagnoses_details " +
            //  " WHERE icd9_code like '" + tb_icd9.Text.Trim() + "%' ) 	) and los<=" + GlobalVars.ICUSTDAYMAXLENGTH + ";";


            //淋巴肿瘤

           sql="select row_id, icustay_id, los as staytime, dbsource from mimiciii.\"v_exp_patients_sofa_Extended\" WHERE	icustay_id IN ( SELECT	icustay_id FROM	mimiciii.icustays WHERE	subject_id IN ( SELECT subject_id FROM mimiciii.v_exp_death_diagnoses_details  WHERE icd9_code LIKE'200%' \n" +
                "	OR icd9_code LIKE'201%'\n" +
                "	OR icd9_code LIKE'202%'\n" +
                "	OR icd9_code LIKE'203%'\n" +
                "	OR icd9_code LIKE'204%'\n" +
                "	OR icd9_code LIKE'205%'\n" +
                "	OR icd9_code LIKE'206%'\n" +
                "	OR icd9_code LIKE'207%'\n" +
                "	OR icd9_code LIKE'208%' ) 	) ;";

            DataTable dataTableS = PGSQLHELPER.excuteDataTable(sql);

            int threadCount = 16; //跑的线程数

            DataSet runSet = DataTableTools.divideDataTable(dataTableS, threadCount);
            foreach (DataTable singleTb in runSet.Tables)
            {
                Thread ausofaThread = new Thread(new ParameterizedThreadStart(doAutoSofa_sub_Thread));
                ausofaThread.Start(singleTb);
            }

        }

        #region 全自动SofaExtended评分



        //主算法


        private void doAutoSofa_sub_Thread(object datasource)
        {
            DataTable delTable = (DataTable)datasource;
            String sql = string.Empty;

            //inputEvent输出文件路径
            string pathInput_out = @"E:\MIMICIII\SOFA_SCORE\" + tb_icd9.Text.Trim() + @"_720_1p1u_3aiFinal\";

            if (!Directory.Exists(pathInput_out))
            {
                Directory.CreateDirectory(pathInput_out);
            }





            #region 读取已经计算完的文件列表
            var files = Directory.GetFiles(pathInput_out, "*.csv");
            List<String> existIcu = new List<string>();
            foreach (var file in files)
            {
                string str = file.Substring(file.LastIndexOf(@"\") + 1);
                string[] strarray = str.Split('_');
                str = strarray[1];
                existIcu.Add(str);
            }
            #endregion


            foreach (DataRow dricu in delTable.Rows)
            {

                #region 1. 判断文件需要构建的文件已经存在
                //判断已经在不在
                if (isIcuStayisExists(existIcu, dricu["icustay_id"].ToString()))
                {
                    continue;
                }
                #endregion

                //##########################
                //筛除多个ICUstay的，只算最后一次

                if (!Utils.CommonTools.isLastICU(dricu["icustay_id"].ToString()))
                {
                    continue;
                }
                //##########################


                List<SequenceNode> squenceList = new List<SequenceNode>(); //sofa序列节点信息

                //需要获取各种诊断数据的病人ICU序列 填充到currentTrainDataTable
                DataTable currentTrainDataTable = getExpData("mimiciii.sup_vars_sofa", dricu["icustay_id"].ToString());


                //构建ChartEvent信息列表
                List<ChartNode> chartList = ChartTools.buildChartList(currentTrainDataTable);

                if (chartList.Count < 1)
                {
                    continue;
                }



                //当前病人的信息
                PatientNode nowPatient = new PatientNode();
                nowPatient.AssignmentByChartNode(chartList[0]);


                DateTime startTime = chartList[0].charttime;
                foreach (ChartNode node in chartList)
                {
                    //给每个结点添加所属结点信息
                    node.timeSqid = (int)(node.charttime.Subtract(startTime).TotalMinutes / GlobalVars.TIMESPAN) + 1;
                }

                //
                squenceList.Clear();
                IEnumerable<IGrouping<int, ChartNode>> query = from chartNode in chartList group chartNode by chartNode.timeSqid;
                foreach (IGrouping<int, ChartNode> chartGropu in query)
                {
                    squenceList.Add(SCOREHELPER.sofa_score(chartGropu));
                }
                Utils.FillScore.fillScoreList(ref squenceList);

                //给序列增加增加时间(开始于结束时间)
                DateTime astartTime = chartList[0].charttime;
                foreach (SequenceNode node in squenceList)
                {
                    node.beginTime = astartTime.AddMinutes((node.seqId - 1) * GlobalVars.TIMESPAN);
                    node.endTime = astartTime.AddMinutes(node.seqId*GlobalVars.TIMESPAN);
                }


                DataTable daat = Utils.DataTableTools.ToDataTable(squenceList);


                #region 填充缺失序列开始

                int totalLineCount = Convert.ToInt16(daat.Rows[daat.Rows.Count - 1][0]);



                for (int i = 0; i < totalLineCount; i++)
                {
                    int realNum = Convert.ToInt16(daat.Rows[i][0]);

                    if ((i + 1) != realNum)
                    {
                        daat.Rows.InsertAt(daat.NewRow(), (i));
                        daat.Rows[(i)][0] = (i + 1);
                    }
                }

                //Utils.DataTableTools.SaveCSV(daat, @"C:\Users\uqzshi3\Desktop\aa.csv");
                #endregion 填充缺失序列结束



                #region  整理标准sofa评分表格

                

                //调整下列顺序
                daat.Columns["seqId"].SetOrdinal(2);
                //将总分提前
                daat.Columns["totalScore"].SetOrdinal(3);
                //去掉分数合成项数
                daat.Columns.Remove("itemCount");
               // Utils.DataTableTools.SaveCSV(daat, @"C:\Users\uqzshi3\Desktop\daat1.csv");
                
                daat = SofaTools.removeUseLessCols2(daat); //去除不输出的列

               // Utils.DataTableTools.SaveCSV(daat, @"C:\Users\uqzshi3\Desktop\daat.csv");
                //               sql = @"select distinct d_item_id from mimiciii.v_exp_feature_mortality";

                //--全序排列feature
                sql = @"select itemid from mimiciii.sup_feature_mortality_chartevent;";


                //增加多出来数据的列
                DataTable fildTable = PGSQLHELPER.excuteDataTable(sql);
                foreach (DataRow dr in fildTable.Rows)
                {
                    daat.Columns.Add(new DataColumn(dr["itemid"].ToString()));
                }
                #endregion

           

                #region 补全时间

                DateTime beginTime = DateTime.Now;
                DateTime endTime = DateTime.Now;

                foreach (DataRow drow in daat.Rows)
                {

                    ///填充时间
                    if (drow["beginTime"].ToString() != "")
                    {
                        beginTime = Convert.ToDateTime(drow["beginTime"]);
                        endTime = Convert.ToDateTime(drow["endTime"]);
                    }
                    else
                    {
                        beginTime = endTime;
                        endTime = endTime.AddMinutes(GlobalVars.TIMESPAN);
                        drow["beginTime"] = beginTime;
                        drow["endTime"] = endTime;
                    }
                }
                #endregion



                //填充缺失序列

                #region 查询所有补充Feature的诊断结果


                //添加了372多feature
                //sql = @"select chartevents.itemid,chartevents.charttime,chartevents.valuenum
                //  from mimiciii.chartevents, mimiciii.d_items, mimiciii.sup_vars_sofa_extended  where 
                //  chartevents.itemid=d_items.itemid and valuenum NOTNULL
                //  and chartevents.itemid=mimiciii.sup_vars_sofa_extended.item_id 
                //  and icustay_id = " + nowPatient.icustay_id + " and chartevents.itemid in (select d_item_id from mimiciii.v_exp_feature_mortality)  ORDER BY charttime;";

                //--新feature，排序后的
                sql = @"select ct.itemid, ct.charttime, ct.valuenum from mimiciii.chartevents ct  where valuenum NOTNULL and icustay_id = "+nowPatient.icustay_id+" and ct.itemid in (select itemid from mimiciii.sup_feature_mortality_chartevent ) ORDER BY ct.charttime;";

                DataTable dt_extendedSofaChartItem = PGSQLHELPER.excuteDataTable(sql);



                #endregion

                #region 给诊断结果分配序列号，按照诊断时间
                if (dt_extendedSofaChartItem != null)
                {
                    dt_extendedSofaChartItem.Columns.Add(new DataColumn("seqId"));

                    foreach (DataRow dr in dt_extendedSofaChartItem.Rows)
                    {
                        DateTime charTime = Convert.ToDateTime(dr["charttime"]);

                        if (DateTime.Compare(charTime, Convert.ToDateTime(daat.Rows[0]["beginTime"])) < 0)
                        {
                            dr["seqId"] = 1;
                            daat.Rows[0][dr["itemid"].ToString()] = dr["valuenum"];
                        }
                        else
                        {
                            foreach (DataRow drTime in daat.Rows)
                            {
                                DateTime atime = Convert.ToDateTime(drTime["beginTime"]);
                                DateTime btime = Convert.ToDateTime(drTime["endTime"]);
                                if (DateTime.Compare(charTime, atime) >= 0 && DateTime.Compare(charTime, btime) <= 0)
                                {
                                    dr["seqId"] = drTime["seqId"];

                                    drTime[dr["itemid"].ToString()] = dr["valuenum"];
                                    break;
                                }
                            }
                        }
                    }
                }
                #endregion

              //  Utils.DataTableTools.SaveCSV(dt_extendedSofaChartItem, @"C:\Users\uqzshi3\Desktop\dt_extendedSofaChartItem.csv");
               // Utils.DataTableTools.SaveCSV(daat, @"C:\Users\uqzshi3\Desktop\daat.csv");



                #region 填充补充数据，外加字符映射

                //填充查询出的数据到输出大表格
                foreach (DataRow row in dt_extendedSofaChartItem.Rows)
                {
                    int rowId = getIndex(daat, row["seqId"].ToString());
                    daat.Rows[rowId][row["itemid"].ToString()] = row["valuenum"].ToString();
                }




                //填充数据，采用照抄上一时刻的数据
                DataRow tempRow = daat.NewRow();

                foreach (DataRow drow in daat.Rows)
                {
                    //填充剩余列

                    for (int j = 3; j < daat.Columns.Count; j++)
                    {
                        if (drow[j].ToString() != string.Empty)
                        {
                            tempRow[j] = drow[j];
                        }
                        else
                        {
                            drow[j] = tempRow[j];
                        }
                        //数据映射
                        // drow[j] = Utils.SofaTools.mapNoneNumerData(drow[j].ToString());
                    }
                }




                //缺失值补全结束

                #endregion

                Console.WriteLine("{0}\t", dricu[0]);


                

                string fileName = ((nowPatient.isDead) ? "0_" : "1_") + nowPatient.icustay_id + "_" + nowPatient.gender + "_" + nowPatient.age + "_SOFAEXTENDED.csv";

                //第一波不输出了
                // Utils.DataTableTools.SaveCSV(daat, path + fileName);


                #region  输出inputEvent合并信息
                //--可以输出第二波了，带输入信息的资料


                //确定数据源
                sql = @"select dbsource from mimiciii.icustays where icustay_id=" + nowPatient.icustay_id;
                string inputSource = PGSQLHELPER.excuteSingleResult(sql);
                

                if (inputSource == "carevue")
                {
                    inputSource = "cv";
                }
                else if (inputSource == "metavision")
                {
                    inputSource = "mv";
                }
                else
                {
                    string sql1 = @"select count(row_id) from mimiciii.inputevents_cv where icustay_id="+nowPatient.icustay_id;
                    string sql2 = @"select count(row_id) from mimiciii.inputevents_mv where icustay_id=" + nowPatient.icustay_id;
                    int rs1 = Convert.ToInt16(PGSQLHELPER.excuteSingleResult(sql1));
                    int rs2 = Convert.ToInt16(PGSQLHELPER.excuteSingleResult(sql2));
                    if (rs1 > rs2)
                    {
                        inputSource = "cv";
                    }
                    else
                    {
                        inputSource = "mv";
                    }
                }



                sql = @"select itemid from mimiciii.sup_exp_sofa_input_item";
                DataTable inputItemDt = PGSQLHELPER.excuteDataTable(sql);

                //生成输出表格
                DataTable inputEventDataTable = new DataTable();
                //添加序列号列
                inputEventDataTable.Columns.Add(new DataColumn("seqId", typeof(string)));

                foreach (DataRow dr in inputItemDt.Rows)
                {
                    inputEventDataTable.Columns.Add(new DataColumn(dr["itemid"].ToString(), typeof(string)));
                }

                
              

                sql = @"select * from mimiciii.v_exp_input_sofa_" + inputSource + " where itemid in (select itemid from mimiciii.sup_exp_sofa_input_item) and icustay_id=" + nowPatient.icustay_id + ";";
                //选出给药信息
                DataTable inputlist = PGSQLHELPER.excuteDataTable(sql);
                //给药加序列号列
                inputlist.Columns.Add(new DataColumn("seqId", typeof(string)));
                inputlist.Columns["seqId"].SetOrdinal(0);


                //给输出table添加行
                for (int i = 0; i < daat.Rows.Count; i++)
                {
                    DataRow drinputOut = inputEventDataTable.NewRow();
                    drinputOut["seqId"] = i + 1;
                    inputEventDataTable.Rows.Add(drinputOut);
                }

                //给给药添加序列号
                foreach (DataRow drmv in inputlist.Rows)
                {
                    drmv["seqId"] = getInputSeqId(Convert.ToDateTime(drmv["starttime"]), daat);

                    //将数据填充到横向展开表格
                    if (drmv["seqId"].ToString() != "-9")
                    {
                        inputEventDataTable.Rows[Convert.ToInt16(drmv["seqId"]) - 1][drmv["itemid"].ToString()] = drmv["amount"];
                    }
                }

                DataTableTools.SaveCSV_2TB(daat, inputEventDataTable, pathInput_out + fileName);

                #endregion

            }
            threadCounter++;
            if (threadCounter == 16)
            {
                MessageBox.Show("数据构建完成");
            }
        }


        
        #endregion

        #region  检查数据中是否包含字符
        private void bt_checkSF_Click(object sender, EventArgs e)
        {

            removeFileList.Clear();

            string path = @"D:\MIMICIII\SOFA_SCORE\" + tb_icd9.Text.Trim() + @"_mix\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var files = Directory.GetFiles(path, "*.csv");
            List<String> existIcu = new List<string>();
            foreach (var file in files)
            {
                try
                {
                    FileStream fileStream = new FileStream(file, FileMode.Open);
                    StreamReader streamReader = new StreamReader(fileStream);
                    String strline = streamReader.ReadLine();
                    while ((strline = streamReader.ReadLine()) != null)
                    {
                        string[] strArray = strline.Split(',');

                        for (int i = 2; i < strArray.Length; i++)
                        {
                            try
                            {
                                if (strArray[i].ToString().Trim() != string.Empty)
                                {
                                    double fl = Convert.ToDouble(strArray[i]);
                                }
                            }
                            catch (FormatException ex)
                            {

                                Console.WriteLine(strArray[i] + "----" + file);
                                if (!isInRemovelist(file))
                                {
                                    removeFileList.Add(file);
                                }

                            }
                        }

                    }
                    streamReader.Close();
                    fileStream.Close();
                }catch(IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            MessageBox.Show("complete!");
        }
        #endregion

        #region 清除包含字符串的文件

        /// <summary>
        /// 判断当前文件是否在移除文件列表里
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool isInRemovelist(string file)
        {
            foreach(string item in removeFileList)
            {
                if (file == item)
                {
                    return true;
                }
            }
            return false;
        }


        private void bt_removef_Click(object sender, EventArgs e)
        {
            foreach(string fileName in removeFileList)
            {
                FileInfo file = new FileInfo(fileName);
                try
                {
                    file.Delete();
                    Console.WriteLine("success remove:" + fileName);
                }catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion

        #region 填充icuStay的数据源，是cv还是mv
        /// <summary>
        /// 填充icuStay的数据源，是cv还是mv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_comDBsource_Click(object sender, EventArgs e)
        {
            sql = @"select icustay_id from mimiciii.sup_exp_icustay_sofa;";
            DataTable icuStaySofaTable = PGSQLHELPER.excuteDataTable(sql);

            int counter = 0;
            foreach (DataRow dr in icuStaySofaTable.Rows)
            {
                Thread updateDbsourceThred = new Thread(new ParameterizedThreadStart(updatedbsouce));
                updateDbsourceThred.Start(dr[0]);

                counter++;
                if (counter % 1000 == 0)
                {
                    Console.WriteLine(counter);
                }

            }

            Console.WriteLine("###################FINSH###############");
        }

        private void updatedbsouce(object obj)
        {

            string id = Convert.ToString(obj);

            sql = @"select count(row_id) from mimiciii.inputevents_cv where icustay_id =" + id + ";";
            int countcv = Convert.ToInt16(PGSQLHELPER.excuteSingleResult(sql));

            sql = @"select count(row_id) from mimiciii.inputevents_mv where icustay_id =" + id + ";";
            int countmv = Convert.ToInt16(PGSQLHELPER.excuteSingleResult(sql));

            string dbsource = string.Empty;

            //在cv里
            if (countcv > countmv)
            {
                dbsource = "cv";
            }
            else
            {
                dbsource = "mv";
            }


            sql = @"update mimiciii.sup_exp_icustay_sofa set dbsource='" + dbsource + "' where icustay_id=" + id + "";
            PGSQLHELPER.executeScalar(sql);
        }




        #endregion

        #region 构建输入数据inputEvent
        /// <summary>
        /// 构建输入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_input_Click(object sender, EventArgs e)
        {
            Thread inputEventThread1 = new Thread(new ParameterizedThreadStart(inputEdo));
            Thread inputEventThread2 = new Thread(new ParameterizedThreadStart(inputEdo));
            inputEventThread1.Start("cv");
            inputEventThread2.Start("mv");
        }

        #region 判断当前文件是否存在
        /// <summary>
        /// 判断当前文件是否存在
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="icustayId"></param>
        /// <returns></returns>
        private bool isFileExistInList(string[] fileList, string icustayId)
        {
            foreach (string str in fileList)
            {
                if (str.Contains(icustayId))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region 获取给药所在的时间序列id
        /// <summary>
        /// 获取给药所在的时间序列id
        /// </summary>
        /// <param name="dateTime">给药时间</param>
        /// <param name="chartTable">诊断序列表</param>
        /// <returns></returns>
        public string getInputSeqId(DateTime dateTime, DataTable chartTable)
        {
            foreach (DataRow dr in chartTable.Rows)
            {
                DateTime startTime = Convert.ToDateTime(dr["beginTime"]);
                DateTime endTime = Convert.ToDateTime(dr["endTime"]);

                if(dateTime.CompareTo(startTime)>0 && dateTime.CompareTo(endTime) < 0)
                {
                    return dr["seqId"].ToString();
                }

            }

            return "-9";
        }
        #endregion

        private void inputEdo(object obj)
        {
            string inputSource = (string)obj;


            //读取inputEvent的Itemid列表
            String inputItemFilePath = @"./data/exp_sofa_input_item.csv";
            DataTable inputItemDt = DataTableTools.OpenCSV(inputItemFilePath);

            //生成输出表格
            DataTable inputEventDataTable = new DataTable();
            //添加序列号列
            inputEventDataTable.Columns.Add(new DataColumn("seqId", typeof(string)));

            foreach (DataRow dr in inputItemDt.Rows)
            {
                inputEventDataTable.Columns.Add(new DataColumn(dr[1].ToString(), typeof(string)));
            }

            //读取Chart文件列表
            string path = @"D:\MIMICIII\SOFA_SCORE\" + tb_icd9.Text.Trim() + @"\";
            string[] filesChartList = Directory.GetFiles(path, "*.csv");

            //输出文件路径
            string pathInput_out = @"D:\MIMICIII\SOFA_SCORE\" + tb_icd9.Text.Trim() + @"_mix\";
            if (!Directory.Exists(pathInput_out))
            {
                Directory.CreateDirectory(pathInput_out);
            }

            string[] existInput_outFile = Directory.GetFiles(pathInput_out, "*.csv");


            //读取icu病人列表

            sql = @"select * from mimiciii.sup_exp_icustay_sofa where dbsource='" + inputSource + @"' and icustay_id IN (
                    SELECT	icustay_id FROM	mimiciii.icustays WHERE	subject_id IN ( 
                    SELECT subject_id FROM mimiciii.v_exp_death_diagnoses_details WHERE icd9_code = '" + tb_icd9.Text + "' )  );";

            DataTable icuMVTable = PGSQLHELPER.excuteDataTable(sql);

            foreach (DataRow dr in icuMVTable.Rows)
            {

                string nowIcuStayId = dr["icustay_id"].ToString();

                //判断输出文件是否存在
                if (isFileExistInList(existInput_outFile, dr["icustay_id"].ToString()))
                {
                    continue;
                }


                sql = @"select * from mimiciii.v_exp_input_sofa_" + inputSource + " where itemid in (select itemid from mimiciii.sup_exp_sofa_input_item) and icustay_id=" + dr["icustay_id"] + ";";
                //选出给药信息
                DataTable inputlist = PGSQLHELPER.excuteDataTable(sql);
                //给药加序列号列
                inputlist.Columns.Add(new DataColumn("seqId", typeof(string)));
                inputlist.Columns["seqId"].SetOrdinal(0);

                ///从文件中读取进来的ChartTable
                ///

                DataTable chartTableRin = DataTableTools.OpenCSV(SofaTools.getChartFileFullPath(dr["icustay_id"].ToString(), filesChartList));



                //给输出table添加行
                for (int i = 0; i < chartTableRin.Rows.Count; i++)
                {
                    DataRow drinputOut = inputEventDataTable.NewRow();
                    drinputOut["seqId"] = i + 1;
                    inputEventDataTable.Rows.Add(drinputOut);
                }

                //给给药添加序列号
                foreach (DataRow drmv in inputlist.Rows)
                {
                    drmv["seqId"] = getInputSeqId(Convert.ToDateTime(drmv["starttime"]), chartTableRin);

                    //将数据填充到横向展开表格
                    if (drmv["seqId"].ToString() != "-9")
                    {
                        inputEventDataTable.Rows[Convert.ToInt16(drmv["seqId"]) - 1][drmv["itemid"].ToString()] = drmv["amount"];
                    }
                }

                // pathInput_out += SofaTools.getChartFileFullName(dr["icustay_id"].ToString(), filesChartList);

                DataTableTools.SaveCSV_2TB(chartTableRin, inputEventDataTable, pathInput_out + SofaTools.getChartFileFullName(dr["icustay_id"].ToString(), filesChartList));

            }

            MessageBox.Show(inputSource + " complete");

        }



        #endregion

        private void bt_ausofaTest_Click(object sender, EventArgs e)
        {
            string icd9code = tb_icd9Test.Text;
            string icustay = tb_icustayidTest.Text;
            
            string sql = @"SELECT * FROM	mimiciii.sup_exp_icustay_sofa WHERE	icustay_id ="+icustay;

            DataTable dataT = PGSQLHELPER.excuteDataTable(sql);


            Thread ausofaThread = new Thread(new ParameterizedThreadStart(doAutoSofa_sub_Thread));
            ausofaThread.Start(dataT);

        }


        #region 分析死亡模型实验结果文件
        private void slider1_ValueChanged(object sender, EventArgs e)
        {
            label8.Text = slider1.Value.ToString();
        }

        List<string[]> icustaylist = new List<string[]>();//实验结果加载进来的json文件FP，FN

        private void bt_load_json_Click(object sender, EventArgs e)
        {
            opf_load_json.ShowDialog();
            string [] filejson = opf_load_json.FileNames;
            if (filejson.Length == 0)
            {
                return;
            }
            FileStream jfilestream = new FileStream(filejson[0],FileMode.Open);
            StreamReader reader = new StreamReader(jfilestream);
            string readin = reader.ReadToEnd();


            string regString = @"[1|0]_[0-9]*_[F|M]_[0-9]*";
            Regex regex = new Regex(regString);
            
            foreach(Match item in regex.Matches(readin))
            {
                icustaylist.Add(item.ToString().Split('_'));
            }

            MessageBox.Show("加载成功","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        #region 分析病序情况按钮
        private void bt_dorder_Click(object sender, EventArgs e)
        {
            string idcollect = string.Empty;
            foreach (string[] item in icustaylist)
            {
                idcollect += (item[1] + " , ");
            }
            idcollect = idcollect.Substring(0, idcollect.Length - 2);

            string sql = @"SELECT
	                    diagnoses_icd.subject_id,icustay_id,icd9_code,seq_num,los
                    FROM
	                    mimiciii.diagnoses_icd,
	                    mimiciii.icustays 
                    WHERE
	                    diagnoses_icd.subject_id IN (
                    SELECT
	                    subject_id 
                    FROM
	                   mimiciii.icustays 
                    WHERE
	                    icustay_id IN ( " + idcollect+" )) AND icd9_code = '"+tb_icd9.Text+"'  AND diagnoses_icd.subject_id = icustays.subject_id order by icustay_id;";
            DataTable anatable = PGSQLHELPER.excuteDataTable(sql);
            anatable.Columns.Add(new DataColumn("diag_count"));
            anatable.Columns.Add(new DataColumn("isdead"));
            foreach(DataRow dr in anatable.Rows)
            {
                sql = @"select count(seq_num) from mimiciii.diagnoses_icd where subject_id ='" + dr["subject_id"]+"';";
                dr["diag_count"] = PGSQLHELPER.excuteSingleResult(sql);
                sql = @"select expire_flag from mimiciii.patients where subject_id='" + dr["subject_id"] + "';";
                dr["isdead"] = PGSQLHELPER.excuteSingleResult(sql);
            }
            dgv_ana.DataSource = anatable;
        }
        #endregion

        #region 分析是否在出错的序列里
        private void bt_cInOrNot_Click(object sender, EventArgs e)
        {
            int pri = slider1.Value;
            int diacount = slider2.Value;

            for (pri = 1; pri < 40; pri++) {
                for (diacount = 1; diacount < 541; diacount++)
                {
                    string sql = @"SELECT icustay_id FROM mimiciii.icustays WHERE subject_id IN (" +
            "	SELECT	subject_id FROM	mimiciii.diagnoses_icd WHERE	subject_id IN ( " +
            "			SELECT subject_id FROM	mimiciii.icustays WHERE	icustay_id IN ( " +
            "				SELECT icustay_id FROM mimiciii.v_exp_diagnoses_ana GROUP BY icustay_id HAVING COUNT ( seq_num ) <= " + diacount + " ))" +
            "	AND icd9_code = '41401' and seq_num <=" + pri + ");";

                    List<string> output = new List<string>();

                    DataTable dt = PGSQLHELPER.excuteDataTable(sql);
                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (string[] str in icustaylist)
                        {
                            if (dr[0].ToString() == str[1].ToString())
                            {
                                if (!isstrInlist(output, str[1]))
                                {
                                    output.Add(str[1]);
                                }
                            }
                        }
                    }
                    double result = 1.0 - (double)output.Count / (double)dt.Rows.Count;
                    Console.WriteLine(pri + "," +diacount+","+ result);
                }

            }

            MessageBox.Show("计算完毕:");
        }
        #endregion

        private bool isstrInlist(List<string> list, string str)
        {
            foreach(string lstr in list)
            {
                if(str==lstr)
                {
                    return true;
                }
            }
            return false;
        }

        private void slider2_ValueChanged(object sender, EventArgs e)
        {
            label9.Text = slider2.Value.ToString();
        }
        #endregion


        #region 找预测错误相同的
        private void bt_findsame_Click(object sender, EventArgs e)
        {
            opf_load_json.ShowDialog();
            string regString = @"[1|0]_[0-9]*_[F|M]_[0-9]*_SOFAEXTENDED";
            Regex regex = new Regex(regString);
            List<string[]> readinlist = new List<string[]>();
            string[] filejson = opf_load_json.FileNames;
            if (filejson.Length == 0)
            {
                return;
            }

            foreach (string str in filejson)
            {
                string fileName = Path.GetFileName(str);
                FileStream jfilestream = new FileStream(str, FileMode.Open);
                StreamReader reader = new StreamReader(jfilestream);
                string readin = reader.ReadToEnd();

                foreach (Match item in regex.Matches(readin))
                {
                    string[] itemArray = item.ToString().Split('_');
                    itemArray[4] = fileName;
                    readinlist.Add(itemArray);
                }
                
            }

            foreach (string[] item in readinlist)
            {
                Console.WriteLine(item[0] +","+ item[1] + "," + item[2] + "," + item[3] + "," + item[4]);
                
            }

            

            DataTable countTable = new DataTable();
            countTable.Columns.Add(new DataColumn("isDead"));
            countTable.Columns.Add(new DataColumn("icustayid"));
            countTable.Columns.Add(new DataColumn("gender"));
            countTable.Columns.Add(new DataColumn("age"));
            countTable.Columns.Add(new DataColumn("file"));
            countTable.Columns.Add(new DataColumn("appearTime", typeof(int)));
            countTable.Columns.Add(new DataColumn("appearFile"));

            for (int i = 0; i < readinlist.Count; i++)
            {
                int pos = isincountlist(countTable, readinlist[i][1]);
                if (pos<0)
                {
                    DataRow dr = countTable.NewRow();
                    dr["isDead"] = readinlist[i][0];
                    dr["icustayid"] = readinlist[i][1];
                    dr["gender"] = readinlist[i][2];
                    dr["age"] = readinlist[i][3];
                    dr["file"] = readinlist[i][4];
                    dr["appearTime"] =0;
                    string aa = dr["appearFile"].ToString();
                    string bb = readinlist[i][4].ToString();
                    
                        dr["appearFile"] = dr["appearFile"] + "|" + readinlist[i][4];
                    

                    countTable.Rows.Add(dr);

                    for (int j = i + 1; j < readinlist.Count; j++)
                    {
                        if(readinlist[i][1]== readinlist[j][1])
                        {
                            dr["appearTime"] = (int)dr["appearTime"]+1;
                            if (!dr["appearFile"].ToString().Contains(readinlist[i][4].ToString()))
                            {
                                dr["appearFile"] = dr["appearFile"] + "|" + readinlist[i][4];
                            }
                        }
                    }
                }
                

            }

            dgv_ana.DataSource = countTable;
            
        }
        #endregion

        #region 判断一个字符串是否在一个DataTable里
        /// <summary>
        /// 判断一个字符串是否在一个DataTable里
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private int isincountlist(DataTable dataTable, string str)
        {
            for(int i=0;i<dataTable.Rows.Count; i++)
            {
                if (str == dataTable.Rows[i][1].ToString())
                {
                    return i;
                }
            }

            return -9;
        }
        #endregion

        #region 根据测试文件列表划分训练集测试集
        private void bt_tt_Click(object sender, EventArgs e)
        {

            Directory.CreateDirectory(divide_base + @"\test\");
            string [] filesin= Directory.GetFiles(divide_base);

            
            FileStream fileStream = new FileStream(divide_file, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            string rfileName = reader.ReadLine();
            
            while (rfileName!=null)
            {
                foreach (string strline in filesin)
                {
                    if (strline.Contains(rfileName))
                    {
                        string fileName = divide_base + @"\test\" + rfileName;
                        if (File.Exists(strline))
                        {
                            File.Move(strline, fileName);
                        }
                    }
                }

                rfileName = reader.ReadLine();
            }
        }
        #endregion

        #region 统计住院时长情况
        private void bt_saveLos_Click(object sender, EventArgs e)
        {
            double timeSpan = 1;
            double start = 0;
            double end = 174;

            double count = (end - start) / timeSpan;
            string sql = string.Empty;

            DataTable staticLosTable = new DataTable();
            staticLosTable.Columns.Add(new DataColumn("time", typeof(int)));
            staticLosTable.Columns.Add(new DataColumn("count", typeof(int)));



            for(int i=0;i<count; i++)
            {
                sql = @"select count(row_id) from mimiciii.icustays where los BETWEEN " + start+" and "+(start+timeSpan)+";";
                start = start + timeSpan;

                int result = Convert.ToInt16( PGSQLHELPER.excuteSingleResult(sql));
                DataRow dr = staticLosTable.NewRow();
                dr["time"] = start;
                dr["count"] = result;
                staticLosTable.Rows.Add(dr);
                Console.WriteLine(start+","+result);
            }


            
            chart1.Series["s1"].Points.DataBind(staticLosTable.AsEnumerable(),"time","count","");
            chart1.Series["s1"].ChartType = SeriesChartType.Line;
            //chart1.Series["s1"].IsValueShownAsLabel = true;
            
        }
        #endregion

        #region 选择基础目录按钮
        private void bt_chooseBaseDataset_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            divide_base = fbd.SelectedPath;
            lb_divided.Text = divide_base;
            fbd.Dispose();
        }
        #endregion

        #region 选择划分文件按钮
        private void bt_dividedFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.ShowDialog();
            divide_file = opf.FileName;
            tss_scoreSystem.Text = divide_file;
            opf.Dispose();
        }
        #endregion
    }
}
