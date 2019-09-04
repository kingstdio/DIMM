using MMICIII.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMICIII
{
    public static class GlobalVars
    {
        /// <summary>
        /// currentPatient Infomation Node
        /// </summary>
        public static PatientNode currentPatient = new PatientNode();

        /// <summary>
        /// Current Disease ICD Code information
        /// </summary>
        public static String CURRENTICDCODE = string.Empty;

        /// <summary>
        /// timeSpan minutes
        /// </summary>
        public static int TIMESPAN = 30;

        /// <summary>
        /// ICU呆的最短时间，单位小时
        /// </summary>
        public static int ICUSTDAYMINLENGTH = 1;


        /// <summary>
        /// ICU呆的最长时间，单位小时
        /// </summary>
        public static int ICUSTDAYMAXLENGTH = 240;


        public static List<mapNode> sofaMapDic = new List<mapNode>();

        /// <summary>
        /// 计算Features跑的线程个数
        /// </summary>
        public static int threadCount = 1;


        /// <summary>
        /// 出ICU死亡天数阈值
        /// </summary>
        public static int DEATHCOUNTDAY = 30;

        /// <summary>
        /// 实验数据的基础目录
        /// </summary>
        public static string EXPFILEBASE = @"E:\MIMICIII\MISSING_IJCAI";


        /// <summary>
        /// chartEvent所用的指标排序取前多少个
        /// </summary>
        public static int CHARTFEATURENUM = 230;

        /// <summary>
        /// inputEvent所用的指标排序取前多少个
        /// </summary>
        public static int INPUTFEATURENUM = 200;

        /// <summary>
        /// 是否填充缺失值
        /// </summary>
        public static bool FILLMISSING = false;

        /// <summary>
        /// 是否是1p1U
        /// </summary>
        public static bool IS1P1U = true;

        /// <summary>
        /// 填充缺失值的方式
        /// </summary>
        public static MISSINGFILLTYPE FillMissingTpe;

        /// <summary>
        /// 疾病相关性级别
        /// </summary>
        public static int DISEASERELATEDLEVEL = 15;

        /// <summary>
        /// 补全K邻近的K值
        /// </summary>
        public static int KNEAR = 2;


        /// <summary>
        /// 是否输出遮罩
        /// </summary>
        public static bool OUTPUTMASK = true;


        /// <summary>
        /// 是否输出时序掩码
        /// </summary>
        public static bool OUTPUTINTERVAL = true;

        /// <summary>
        /// 额外添加的独立特征
        /// </summary>
        public static int ADDITIONFILEDCOUNT = 6;


        /// <summary>
        /// 是否构建的是缺失数据
        /// </summary>
        public static bool ISMISSINGSET = true;

        /// <summary>
        /// 数据缺失率
        /// </summary>
        public static double MISSINGRATE = 0.1;

        /// <summary>
        /// chartEvent的feature列表
        /// </summary>
        public static DataTable CHARTFETURETABLE = new DataTable();


       /// <summary>
       /// inputevent的feature列表
       /// </summary>
        public static DataTable INPUTFETURETABLE = new DataTable();


        /// <summary>
        /// 是否包含inputevent的数据
        /// </summary>
        public static bool INPUTSWITCH = false;


    }
}
