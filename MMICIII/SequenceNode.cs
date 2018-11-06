using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMICIII
{
    public class SequenceNode
    {
        public int seqId { get; set; }
        public DateTime beginTime { get; set; }
        public DateTime endTime { get; set; }
        public int temperatureScore { get; set; }
        public Int64 temperatureitemid { get; set; }
        public String temperatureValue { get; set; }
        public string temperatureValueuom { get; set; }
        public string temperatureLabel { get; set; }
        public bool temperatureFlag { get; set; }


        public int ABPScore { get; set; }
        public Int64 ABPitemid { get; set; }
        public String ABPValue { get; set; }
        public string ABPValueuom { get; set; }
        public string ABPLabel { get; set; }
        public bool ABPFlag { get; set; }


        public int heartReateScore { get; set; }
        public Int64 heartReateitemid { get; set; }
        public String heartReateValue { get; set; }
        public string heartReateValueuom { get; set; }
        public string heartReateLabel { get; set; }
        public bool heartReateFlag { get; set; }


        public int respiratoryRateScore{get;set;}
        public Int64 respiratoryitemid { get; set; }
        public String respiratoryValue { get; set; }
        public string respiratoryValueuom { get; set; }
        public string respiratoryLabel { get; set; }
        public bool respiratoryFlag { get; set; }


        public int FiO2Score { get; set; }
        public Int64 FiO2itemid { get; set; }
        public String FiO2Value { get; set; }
        public string FiO2Valueuom { get; set; }
        public string FiO2Label { get; set; }
        public bool FiO2Flag { get; set; }


        public int PHScore { get; set; }
        public Int64 PHitemid { get; set; }
        public String PHValue { get; set; }
        public string PHValueuom { get; set; }
        public string PHLabel { get; set; }
        public bool PHFlag { get; set; }


        public int sodiumScore { get; set; }
        public Int64 sodiumitemid { get; set; }
        public String sodiumValue { get; set; }
        public string sodiumValueuom { get; set; }
        public string sodiumLabel { get; set; }
        public bool sodiumFlag { get; set; }


        public int potassiumSore { get; set; }
        public Int64 potassiumitemid { get; set; }
        public String potassiumValue { get; set; }
        public string potassiumValueuom { get; set; }
        public string potassiumLabel { get; set; }
        public bool potassiumFlag { get; set; }


        public int creatinineScore { get; set; }
        public Int64 creatinineitemid { get; set; }
        public String creatinineValue { get; set; }
        public string creatinineValueuom { get; set; }
        public string creatinineLabel { get; set; }
        public bool creatinineFlag { get; set; }


        public int HCTScore { get; set; }
        public Int64 HCTitemid { get; set; }
        public String HCTValue { get; set; }
        public string HCTValueuom { get; set; }
        public string HCTLabel { get; set; }
        public bool HCTFlag { get; set; }


        public int WBCScore { get; set; }
        public Int64 WBCitemid { get; set; }
        public String WBCValue { get; set; }
        public string WBCValueuom { get; set; }
        public string WBCLabel { get; set; }
        public bool WBCFlag { get; set; }


        public int GCSScore { get; set; }
        public Int64 GCSitemid { get; set; }
        public String GCSValue { get; set; }
        public string GCSValueuom { get; set; }
        public string GCSLabel { get; set; }
        public bool GCSFlag { get; set; }


        public int ageScore { get; set; }
        public Int64 ageitemid { get; set; }
        public String ageValue { get; set; }
        public string ageValueuom { get; set; }
        public string ageLabel { get; set; }
        public bool ageFlag { get; set; }


        public int HCO3Score { get; set; }
        public Int64 HCO3itemid { get; set; }
        public String HCO3Value { get; set; }
        public string HCO3Valueuom { get; set; }
        public string HCO3Label { get; set; }
        public bool HCO3Flag { get; set; }


        public int PaO2Score { get; set; }
        public Int64 PaO2itemid { get; set; }
        public String PaO2Value { get; set; }
        public string PaO2Valueuom { get; set; }
        public string PaO2Label { get; set; }
        public bool PaO2Flag { get; set; }

        public int PlateletsScore { get; set; }
        public Int64 Plateletsitemid { get; set; }
        public String PlateletsValue { get; set; }
        public string PlateletsValueuom { get; set; }
        public string PlateletsLabel { get; set; }
        public bool PlateletsFlag { get; set; }


        public int BilirubinScore { get; set; }
        public Int64 Bilirubinitemid { get; set; }
        public String BilirubinValue { get; set; }
        public string BilirubinValueuom { get; set; }
        public string BilirubinLabel { get; set; }
        public bool BilirubinFlag { get; set; }

        public int DobutamineScore { get; set; }
        public Int64 Dobutamineitemid { get; set; }
        public String DobutamineValue { get; set; }
        public string DobutamineValueuom { get; set; }
        public string DobutamineLabel { get; set; }
        public bool DobutamineFlag { get; set; }

        public int DopamineScore { get; set; }
        public Int64 Dopamineitemid { get; set; }
        public String DopamineValue { get; set; }
        public string DopamineValueuom { get; set; }
        public string DopamineLabel { get; set; }
        public bool DopamineFlag { get; set; }

        public int EpinephrineScore { get; set; }
        public Int64 Epinephrineitemid { get; set; }
        public String EpinephrineValue { get; set; }
        public string EpinephrineValueuom { get; set; }
        public string EpinephrineLabel { get; set; }
        public bool EpinephrineFlag { get; set; }

        public int NorepinephrineScore { get; set; }
        public Int64 Norepinephrineitemid { get; set; }
        public String NorepinephrineValue { get; set; }
        public string NorepinephrineValueuom { get; set; }
        public string NorepinephrineLabel { get; set; }
        public bool NorepinephrineFlag { get; set; }

        public int UrineScore { get; set; }
        public Int64 Urineitemid { get; set; }
        public String UrineValue { get; set; }
        public string UrineValueuom { get; set; }
        public string UrineLabel { get; set; }
        public bool UrineFlag { get; set; }

        public int totalScore { get; set; }
        public int itemCount { get; set; }

        public void Clear()
        {
            seqId= 0;
            
            
            temperatureScore= 0;
            temperatureitemid= 0;
            temperatureValue = string.Empty;
            temperatureValueuom =string.Empty;
            temperatureLabel = string.Empty;
            temperatureFlag = false;
            ABPScore = 0;
            ABPitemid= 0;
            ABPValue= string.Empty;
            ABPValueuom= string.Empty;
            ABPLabel= string.Empty;
            ABPFlag= false;
            heartReateScore= 0;
            heartReateitemid= 0;
            heartReateValue= string.Empty;
            heartReateValueuom = string.Empty;
            heartReateLabel= string.Empty;
            heartReateFlag= false;
            respiratoryRateScore= 0;
            respiratoryitemid= 0;
            respiratoryValue= string.Empty;
            respiratoryValueuom= string.Empty;
            respiratoryLabel= string.Empty;
            respiratoryFlag= false;
            FiO2Score= 0;
            FiO2itemid= 0;
            FiO2Value= string.Empty;
            FiO2Valueuom= string.Empty;
            FiO2Label= string.Empty;
            FiO2Flag= false;
            PHScore= 0;
            PHitemid= 0;
            PHValue= string.Empty;
            PHValueuom= string.Empty;
            PHLabel= string.Empty;
            PHFlag= false;
            sodiumScore= 0;
            sodiumitemid= 0;
            sodiumValue= string.Empty;
            sodiumValueuom= string.Empty;
            sodiumLabel= string.Empty;
            sodiumFlag= false;
            potassiumSore= 0;
            potassiumitemid= 0;
            potassiumValue = string.Empty;
            potassiumValueuom = string.Empty;
            potassiumLabel = string.Empty;
            potassiumFlag = false;
            creatinineScore = 0;
            creatinineitemid = 0;
            creatinineValue = string.Empty;
            creatinineValueuom = string.Empty;
            creatinineLabel = string.Empty;
            creatinineFlag = false;
            HCTScore = 0;
            HCTitemid = 0;
            HCTValue = string.Empty;
            HCTValueuom = string.Empty;
            HCTLabel = string.Empty;
            HCTFlag = false;
            WBCScore = 0;
            WBCitemid = 0;
            WBCValue = string.Empty;
            WBCValueuom = string.Empty;
            WBCLabel = string.Empty;
            WBCFlag = false;
            GCSScore = 0;
            GCSitemid = 0;
            GCSValue = string.Empty;
            GCSValueuom = string.Empty;
            GCSLabel = string.Empty;
            GCSFlag = false;
            ageScore = 0;
            ageitemid = 0;
            ageValue = string.Empty;
            ageValueuom = string.Empty;
            ageLabel = string.Empty;
            ageFlag = false;
            HCO3Score= 0;
            HCO3itemid= 0;
            HCO3Value= string.Empty;
            HCO3Valueuom= string.Empty;
            HCO3Label= string.Empty;
            HCO3Flag= false;

            PaO2Score = 0;
            PaO2itemid = 0;
            PaO2Value = string.Empty;
            PaO2Valueuom = string.Empty;
            PaO2Label = string.Empty;
            PaO2Flag = false;

            PlateletsScore = 0;
            Plateletsitemid = 0;
            PlateletsValue = string.Empty;
            PlateletsValueuom = string.Empty;
            PlateletsLabel = string.Empty;
            PlateletsFlag = false;


            BilirubinScore = 0;
            Bilirubinitemid = 0;
            BilirubinValue = string.Empty;
            BilirubinValueuom = string.Empty;
            BilirubinLabel = string.Empty;
            BilirubinFlag = false;

            DobutamineScore = 0;
            Dobutamineitemid = 0;
            DobutamineValue = string.Empty;
            DobutamineValueuom = string.Empty;
            DobutamineLabel = string.Empty;
            DobutamineFlag = false;

            DopamineScore = 0;
            Dopamineitemid = 0;
            DopamineValue = string.Empty;
            DopamineValueuom = string.Empty;
            DopamineLabel = string.Empty;
            DopamineFlag = false;

            EpinephrineScore = 0;
            Epinephrineitemid = 0;
            EpinephrineValue = string.Empty;
            EpinephrineValueuom = string.Empty;
            EpinephrineLabel = string.Empty;
            EpinephrineFlag = false;

            NorepinephrineScore = 0;
            Norepinephrineitemid = 0;
            NorepinephrineValue = string.Empty;
            NorepinephrineValueuom = string.Empty;
            NorepinephrineLabel = string.Empty;
            NorepinephrineFlag = false;

            UrineScore = 0;
            Urineitemid = 0;
            UrineValue = string.Empty;
            UrineValueuom = string.Empty;
            UrineLabel = string.Empty;
            UrineFlag = false;
            totalScore = 0;
            itemCount= 0;
        }
    }
}
