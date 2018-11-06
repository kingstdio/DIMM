using MMICIII.Utils;
using System;
using System.Collections.Generic;
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
        public static int TIMESPAN = 20;

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
        public static int threadCount = 16;


        /// <summary>
        /// 出ICU死亡天数阈值
        /// </summary>
        public static int DEATHCOUNTDAY = 30;

        /// <summary>
        /// 实验数据的基础目录
        /// </summary>
        public static string EXPFILEBASE = @"D:\MIMICIII\MISSING";


        /// <summary>
        /// chartEvent所用的指标排序取前多少个
        /// </summary>
        public static int CHARTFEATURENUM = 100;

        /// <summary>
        /// inputEvent所用的指标排序取前多少个
        /// </summary>
        public static int INPUTFEATURENUM = 100;

        /// <summary>
        /// 是否填充缺失值
        /// </summary>
        public static bool FILLMISSING = false;

        /// <summary>
        /// 是否是1p1U
        /// </summary>
        public static bool IS1P1U = false;

        /// <summary>
        /// 填充缺失值的方式
        /// </summary>
        public static MISSINGFILLTYPE FillMissingTpe;

        /// <summary>
        /// 疾病相关性级别
        /// </summary>
        public static int DISEASERELATEDLEVEL = 1;

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

    }
}
