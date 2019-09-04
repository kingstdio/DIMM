using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMICIII.Utils
{
    public class Output
    {

        #region Sofa的输出字段
        /// <summary>
        /// Sofa 的输出字段
        /// </summary>
        private static string []  outPutFiled_Sofa = {
            "PaO2",
            "FiO2",
            "Platelets",
            "Bilirubin",
            "ABP_mean",
            "GCS",
            "Creatinine",
            "Urine"
        };
        #endregion

        #region Sofa扩展输出字段
        /// <summary>
        /// Sofa 扩展输出字段
        /// </summary>
        private static string[] outPutFiled_Sofa_Extended = {
            "temperature",
            "ABP",
            "heartReate",
            "respiratory",
            "PH",
            "sodium",
            "potassium"
        };
        #endregion

        #region ApacheII的输出字段
        /// <summary>
        /// ApacheII的输出字段
        /// </summary>
        private static string[] outPutFiled_Apacheii = {
            "temperature",
            "ABP",
            "heartReate",
            "respiratory",
            "FiO2",
            "PH",
            "sodium",
            "potassium",
            "creatinine",
            "HCT",
            "WBC",
            "GCS",
            "HCO3"
        };
        #endregion

        #region 写CSV文件
        /// <summary>
        /// 写CSV文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="squenceList">序列列表</param>
        /// <param name="stype">评分系统</param>
        public static void CsvWrite(String filePath , List<SequenceNode> squenceList, SCORESYSTEMTYPE stype)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(fileStream);

            if (stype == SCORESYSTEMTYPE.APACHEII)
            {
                string title = "seqId,totalScore";

                foreach (string item in outPutFiled_Apacheii)
                {
                    title += ("," + item + "Score" + "," + item + "Flag" + "," + item + "itemid" + "," + item + "Value" + "," + item + "Label");
                }
                title += ",age";
                writer.WriteLine(title);
                foreach (SequenceNode node in squenceList)
                {
                    string strLine = LineConstructor(SCORESYSTEMTYPE.APACHEII, node);
                    writer.WriteLine(strLine);
                }
            }
            if (stype == SCORESYSTEMTYPE.SOFA)
            {
                string title = "seqId,totalScore";
                foreach (string item in outPutFiled_Sofa)
                {
                    title += ("," + item + "Score" + "," + item + "Flag" + "," + item + "itemid" + "," + item + "Value" + "," + item + "Label");
                }



                writer.WriteLine(title);
                foreach (SequenceNode node in squenceList)
                {
                    string strLine = LineConstructor(SCORESYSTEMTYPE.SOFA, node);
                    writer.WriteLine(strLine);
                }
            }

            if(stype == SCORESYSTEMTYPE.SOFAEXTENDED)
            {
                string title = "seqId,totalScore";
                foreach (string item in outPutFiled_Sofa)
                {
                    title += ("," + item + "Score" + "," + item + "Flag" + "," + item + "itemid" + "," + item + "Value" + "," + item + "Label");
                }

                foreach(string item in outPutFiled_Sofa_Extended)
                {
                    title += (","+ item + "Flag" + "," + item + "itemid" + "," + item + "Value" + "," + item + "Label");
                }


                writer.WriteLine(title);
                foreach (SequenceNode node in squenceList)
                {
                    string strLine = LineConstructor(SCORESYSTEMTYPE.SOFAEXTENDED, node);
                    writer.WriteLine(strLine);
                }
            }
            writer.Close();
            fileStream.Close();
        }
        #endregion

        #region 构建每一行输出项
        /// <summary>
        /// 构建每一行输出项
        /// </summary>
        /// <param name="type">评分系统</param>
        /// <param name="node">序列节点</param>
        /// <returns></returns>
        private static string LineConstructor(SCORESYSTEMTYPE type, SequenceNode node)
        {
            string strLine = node.seqId + ",";

            if(type== SCORESYSTEMTYPE.SOFA)
            {
                strLine += node.totalScore + ",";
                strLine += node.PaO2Score + "," + (node.PaO2Flag == true ? "1" : "0") + "," + node.PaO2itemid + "," + node.PaO2Value + "," + node.PaO2Label + ",";
                strLine += node.FiO2Score + "," + (node.FiO2Flag == true ? "1" : "0") + "," + node.FiO2itemid + "," + node.FiO2Value + "," + node.FiO2Label + ",";
                strLine += node.PlateletsScore + "," + (node.PlateletsFlag == true ? "1" : "0") + "," + node.Plateletsitemid + "," + node.PlateletsValue + ","  + node.PlateletsLabel + ",";
                strLine += node.BilirubinScore + "," + (node.BilirubinFlag == true ? "1" : "0") + "," + node.Bilirubinitemid + "," + node.BilirubinValue + ","  + node.BilirubinLabel + ",";
                strLine += node.ABPScore + "," + (node.ABPFlag == true ? "1" : "0") + "," + node.ABPitemid + "," + node.ABPValue + ","  + node.ABPLabel + ",";
                strLine += node.GCSScore + "," + (node.GCSFlag == true ? "1" : "0") + "," + node.GCSitemid + "," + node.GCSValue + ","  + node.GCSLabel + ",";
                strLine += node.creatinineScore + "," + (node.creatinineFlag == true ? "1" : "0") + "," + node.creatinineitemid + "," + node.creatinineValue + ","  + node.creatinineLabel + ",";
                strLine += node.UrineScore + "," + (node.UrineFlag == true ? "1" : "0") + "," + node.Urineitemid + "," + node.UrineValue + ","  + node.UrineLabel ;               
            }

            if (type == SCORESYSTEMTYPE.SOFAEXTENDED)
            {
                strLine += node.totalScore + ",";
                strLine += node.PaO2Score + "," + (node.PaO2Flag == true ? "1" : "0") + "," + node.PaO2itemid + "," + node.PaO2Value + "," + node.PaO2Label + ",";
                strLine += node.FiO2Score + "," + (node.FiO2Flag == true ? "1" : "0") + "," + node.FiO2itemid + "," + node.FiO2Value + "," + node.FiO2Label + ",";
                strLine += node.PlateletsScore + "," + (node.PlateletsFlag == true ? "1" : "0") + "," + node.Plateletsitemid + "," + node.PlateletsValue + "," + node.PlateletsLabel + ",";
                strLine += node.BilirubinScore + "," + (node.BilirubinFlag == true ? "1" : "0") + "," + node.Bilirubinitemid + "," + node.BilirubinValue + "," + node.BilirubinLabel + ",";
                strLine += node.ABPScore + "," + (node.ABPFlag == true ? "1" : "0") + "," + node.ABPitemid + "," + node.ABPValue + "," + node.ABPLabel + ",";
                strLine += node.GCSScore + "," + (node.GCSFlag == true ? "1" : "0") + "," + node.GCSitemid + "," + node.GCSValue + "," + node.GCSLabel + ",";
                strLine += node.creatinineScore + "," + (node.creatinineFlag == true ? "1" : "0") + "," + node.creatinineitemid + "," + node.creatinineValue + "," + node.creatinineLabel + ",";
                strLine += node.UrineScore + "," + (node.UrineFlag == true ? "1" : "0") + "," + node.Urineitemid + "," + node.UrineValue + "," + node.UrineLabel +",";

                strLine += (node.temperatureFlag == true ? "1" : "0") + "," + node.temperatureitemid + "," + node.temperatureValue + "," + node.temperatureLabel + ",";
                strLine += (node.ABPFlag == true ? "1" : "0") + "," + node.ABPitemid + "," + node.ABPValue + "," + node.ABPLabel + ",";
                strLine += (node.heartReateFlag == true ? "1" : "0") + "," + node.heartReateitemid + "," + node.heartReateValue + "," + node.heartReateLabel + ",";
                strLine += (node.respiratoryFlag == true ? "1" : "0") + "," + node.respiratoryitemid + "," + node.respiratoryValue + "," + node.respiratoryLabel + ",";
                strLine += (node.PHFlag == true ? "1" : "0") + "," + node.PHitemid + "," + node.PHValue + "," + node.PHLabel + ",";
                strLine += (node.sodiumFlag == true ? "1" : "0") + "," + node.sodiumitemid + "," + node.sodiumValue + "," + node.sodiumLabel + ",";
                strLine += (node.potassiumFlag == true ? "1" : "0") + "," + node.potassiumitemid + "," + node.potassiumValue + "," + node.potassiumLabel ;
            }

            if (type== SCORESYSTEMTYPE.APACHEII)
            {
                strLine += node.totalScore + ",";
                strLine += node.temperatureScore + "," + (node.temperatureFlag == true ? "1" : "0") + "," + node.temperatureitemid + "," + node.temperatureValue + ","  + node.temperatureLabel + ",";
                strLine += node.ABPScore + "," + (node.ABPFlag == true ? "1" : "0") + "," + node.ABPitemid + "," + node.ABPValue + "," + node.ABPLabel + ",";
                strLine += node.heartReateScore + "," + (node.heartReateFlag == true ? "1" : "0") + "," + node.heartReateitemid + "," + node.heartReateValue + ","  + node.heartReateLabel + ",";
                strLine += node.respiratoryRateScore + "," + (node.respiratoryFlag == true ? "1" : "0") + "," + node.respiratoryitemid + "," + node.respiratoryValue + ","  + node.respiratoryLabel + ",";
                strLine += node.FiO2Score + "," + (node.FiO2Flag == true ? "1" : "0") + "," + node.FiO2itemid + "," + node.FiO2Value + "," + node.FiO2Label + ",";
                strLine += node.PHScore + "," + (node.PHFlag == true ? "1" : "0") + "," + node.PHitemid + "," + node.PHValue + ","  + node.PHLabel + ",";
                strLine += node.sodiumScore + "," + (node.sodiumFlag == true ? "1" : "0") + "," + node.sodiumitemid + "," + node.sodiumValue + "," + node.sodiumLabel + ",";
                strLine += node.potassiumSore + "," + (node.potassiumFlag == true ? "1" : "0") + "," + node.potassiumitemid + "," + node.potassiumValue + ","  + node.potassiumLabel + ",";
                strLine += node.creatinineScore + "," + (node.creatinineFlag == true ? "1" : "0") + "," + node.creatinineitemid + "," + node.creatinineValue + ","  + node.creatinineLabel + ",";
                strLine += node.HCTScore + "," + (node.HCTFlag == true ? "1" : "0") + "," + node.HCTitemid + "," + node.HCTValue + ","  + node.HCTLabel + ",";
                strLine += node.WBCScore + "," + (node.WBCFlag == true ? "1" : "0") + "," + node.WBCitemid + "," + node.WBCValue + ","  + node.WBCLabel + ",";
                strLine += node.GCSScore + "," + (node.GCSFlag == true ? "1" : "0") + "," + node.GCSitemid + "," + node.GCSValue + ","  + node.GCSLabel + ",";
                strLine += node.HCO3Score + "," + (node.HCO3Flag == true ? "1" : "0") + "," + node.HCO3itemid + "," + node.HCO3Value + "," + node.HCO3Label ;
                strLine += node.ageScore + ",";
            }

            return strLine;
        }
        #endregion

        #region 输出缺失值的缺失信息
        /// <summary>
        /// 输出缺失值的缺失信息
        /// </summary>
        /// <param name="list">位置列表</param>
        /// <param name="filePath">文件路径</param>
        public static void outputPosList(List<double []> list, string filePath )
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(fileStream);
            string title = "rowpos,colpos,value";
            writer.WriteLine(title);
            foreach(double []  item in list)
            {
                string line = item[0] + "," + item[1] + "," + item[2];
                writer.WriteLine(line);
            }

            writer.Close();
            fileStream.Close();

        }
        #endregion
    }
}
