using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMICIII.Utils
{
    public class FillScore
    {

        public static bool fillScoreList(ref List<SequenceNode> squenceList)
        {
            SequenceNode fillNode = new SequenceNode();
            foreach (SequenceNode node in squenceList)
            {

                //1.temperature
                if (node.temperatureFlag)
                {
                    fillNode.temperatureScore = node.temperatureScore;
                   
                }
                else
                {
                    node.temperatureScore = fillNode.temperatureScore;
                    node.temperatureValue = fillNode.temperatureValue;
                }

                //2.ABP
                if (node.ABPFlag)
                {
                    fillNode.ABPScore = node.ABPScore;
                    fillNode.ABPValue = node.ABPValue;
                }
                else
                {
                    node.ABPScore = fillNode.ABPScore;
                    node.ABPValue = fillNode.ABPValue;
                }

                //3.heart reate
                if (node.heartReateFlag)
                {
                    fillNode.heartReateScore = node.heartReateScore;
                    fillNode.heartReateValue = node.heartReateValue;
                }
                else
                {
                    node.heartReateScore = fillNode.heartReateScore;
                    node.heartReateValue = fillNode.heartReateValue;
                }

                //4. respiratory rate
                if (node.respiratoryFlag)
                {
                    fillNode.respiratoryRateScore = node.respiratoryRateScore;
                    fillNode.respiratoryValue = node.respiratoryValue;
                }
                else
                {
                    node.respiratoryRateScore = fillNode.respiratoryRateScore;
                    node.respiratoryValue = fillNode.respiratoryValue;
                }

                //5.FIO2
                if (node.FiO2Flag)
                {
                    fillNode.FiO2Score = node.FiO2Score;
                    fillNode.FiO2Value = node.FiO2Value;
                }
                else
                {
                    node.FiO2Score = fillNode.FiO2Score;
                    node.FiO2Value = fillNode.FiO2Value;
                }

                //6.PH
                if (node.PHFlag)
                {
                    fillNode.PHScore = node.PHScore;
                    fillNode.PHValue = node.PHValue;
                }
                else
                {
                    node.PHScore = fillNode.PHScore;
                    node.PHValue = fillNode.PHValue;
                }

                //7.sodium
                if (node.sodiumFlag)
                {
                    fillNode.sodiumScore = node.sodiumScore;
                    fillNode.sodiumValue = node.sodiumValue;
                }
                else
                {
                    node.sodiumScore = fillNode.sodiumScore;
                    node.sodiumValue = fillNode.sodiumValue;
                }

                //8.potassium
                if (node.potassiumFlag)
                {
                    fillNode.potassiumSore = node.potassiumSore;
                    fillNode.potassiumValue = node.potassiumValue;
                }
                else
                {
                    node.potassiumSore = fillNode.potassiumSore;
                    node.potassiumValue = fillNode.potassiumValue;
                }

                //9.creatinine
                if (node.creatinineFlag)
                {
                    fillNode.creatinineScore = node.creatinineScore;
                    fillNode.creatinineScore = node.creatinineScore;
                }
                else
                {
                    node.creatinineScore = fillNode.creatinineScore;
                    node.creatinineScore = fillNode.creatinineScore;
                }

                //10.HCT
                if (node.HCTFlag)
                {
                    fillNode.HCTScore = node.HCTScore;
                    fillNode.HCTValue = node.HCTValue;
                }
                else
                {
                    node.HCTScore = fillNode.HCTScore;
                    node.HCTValue = fillNode.HCTValue;
                }

                //11.WBC
                if (node.WBCFlag)
                {
                    fillNode.WBCScore = node.WBCScore;
                    fillNode.WBCValue = node.WBCValue;
                }
                else
                {
                    node.WBCScore = fillNode.WBCScore;
                    node.WBCValue = fillNode.WBCValue;
                }

                //12.GCS
                if (node.GCSFlag)
                {
                    fillNode.GCSScore = node.GCSScore;
                    fillNode.GCSValue = node.GCSValue;
                }
                else
                {
                    node.GCSScore = fillNode.GCSScore;
                    node.GCSValue = fillNode.GCSValue;
                }

                //13.age
                if (node.ageFlag)
                {
                    fillNode.ageScore = node.ageScore;
                    fillNode.ageValue = node.ageValue;
                }
                else
                {
                    node.ageScore = fillNode.ageScore;
                    node.ageValue = fillNode.ageValue;
                }

                //14.HCO3
                if (node.HCO3Flag)
                {
                    fillNode.HCO3Score = node.HCO3Score;
                    fillNode.HCO3Value = node.HCO3Value;
                }
                else
                {
                    node.HCO3Score = fillNode.HCO3Score;
                    node.HCO3Value = fillNode.HCO3Value;
                }

                //15. sup PaO2
                if (node.PaO2Flag)
                {
                    fillNode.PaO2Score = node.PaO2Score;
                    fillNode.PaO2Value = node.PaO2Value;
                }
                else
                {
                    node.PaO2Score = fillNode.PaO2Score;
                    node.PaO2Value = fillNode.PaO2Value;
                }

                //16. sup Platelets
                if (node.PlateletsFlag)
                {
                    fillNode.PlateletsScore = node.PlateletsScore;
                    fillNode.PlateletsValue = node.PlateletsValue;
                }
                else
                {
                    node.PlateletsScore = fillNode.PlateletsScore;
                    node.PlateletsValue = fillNode.PlateletsValue;
                }

                //17. sup Bilirubin
                if (node.BilirubinFlag)
                {
                    fillNode.BilirubinScore = node.BilirubinScore;
                    fillNode.BilirubinValue = node.BilirubinValue;
                }
                else
                {
                    node.BilirubinScore = fillNode.BilirubinScore;
                    node.BilirubinValue = fillNode.BilirubinValue;
                }

                //18. sup Dobutamine
                if (node.DobutamineFlag)
                {
                    fillNode.DobutamineScore = node.DobutamineScore;
                    fillNode.DobutamineValue = node.DobutamineValue;
                }
                else
                {
                    node.DobutamineScore = fillNode.DobutamineScore;
                    node.DobutamineValue = fillNode.DobutamineValue;
                }

                //19. sup Dopamine
                if (node.DopamineFlag)
                {
                    fillNode.DopamineScore = node.DopamineScore;
                    fillNode.DopamineValue = node.DopamineValue;
                }
                else
                {
                    node.DopamineScore = fillNode.DopamineScore;
                    node.DopamineValue = fillNode.DopamineValue;
                }

                //20. sup Epinephrine
                if (node.EpinephrineFlag)
                {
                    fillNode.EpinephrineScore = node.EpinephrineScore;
                    fillNode.EpinephrineValue = node.EpinephrineValue;
                }
                else
                {
                    node.EpinephrineScore = fillNode.EpinephrineScore;
                    node.EpinephrineValue = fillNode.EpinephrineValue;
                }

                //21. sup Norepinephrine
                if (node.NorepinephrineFlag)
                {
                    fillNode.NorepinephrineScore = node.NorepinephrineScore;
                    fillNode.NorepinephrineValue = node.NorepinephrineValue;
                }
                else
                {
                    node.NorepinephrineScore = fillNode.NorepinephrineScore;
                    node.NorepinephrineValue = fillNode.NorepinephrineValue;
                }

                //22.sup Urine
                if (node.UrineFlag)
                {
                    fillNode.UrineScore = node.UrineScore;
                    fillNode.UrineValue = node.UrineValue;
                }
                else
                {
                    node.UrineScore = fillNode.UrineScore;
                    node.UrineValue = fillNode.UrineValue;
                }

                node.totalScore = node.temperatureScore + node.ABPScore + node.heartReateScore + node.respiratoryRateScore
                + node.FiO2Score + node.PHScore + node.sodiumScore + node.potassiumSore + node.creatinineScore
                + node.HCTScore + node.WBCScore + node.GCSScore + node.ageScore + node.HCO3Score +
                node.PaO2Score +
                node.PlateletsScore +
                node.BilirubinScore +
                node.DobutamineScore +
                node.DopamineScore +
                node.EpinephrineScore +
                node.NorepinephrineScore +
                node.UrineScore;

            }

            return true;
        }
    }
}
