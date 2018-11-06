using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMICIII.Utils
{
    public class MergeList
    {
        public static void mergeList(ref List<SequenceNode> sofaList, List<SequenceNode> apacheList)
        {
            int i = 0;
           foreach(SequenceNode sofaNode in sofaList)
            {
                for (; i < apacheList.Count; i++)
                {
                    if (sofaNode.seqId == apacheList[i].seqId)
                    {
                        sofaNode.ABPitemid = apacheList[i].ABPitemid;
                        sofaNode.ABPFlag = apacheList[i].ABPFlag;
                        sofaNode.ABPValue = apacheList[i].ABPValue;
                        sofaNode.ABPLabel = apacheList[i].ABPLabel;

                        sofaNode.PHitemid = apacheList[i].PHitemid;
                        sofaNode.PHFlag = apacheList[i].PHFlag;
                        sofaNode.PHValue = apacheList[i].PHValue;
                        sofaNode.PHLabel = apacheList[i].PHLabel;

                        sofaNode.HCO3itemid = apacheList[i].HCO3itemid;
                        sofaNode.HCO3Flag = apacheList[i].HCO3Flag;
                        sofaNode.HCO3Value = apacheList[i].HCO3Value;
                        sofaNode.HCO3Label = apacheList[i].HCO3Label;

                        sofaNode.HCTitemid = apacheList[i].HCTitemid;
                        sofaNode.HCTFlag = apacheList[i].HCTFlag;
                        sofaNode.HCTValue = apacheList[i].HCTValue;
                        sofaNode.HCTLabel = apacheList[i].HCTLabel;

                        sofaNode.heartReateitemid= apacheList[i].heartReateitemid;
                        sofaNode.heartReateFlag = apacheList[i].heartReateFlag;
                        sofaNode.heartReateValue = apacheList[i].heartReateValue;
                        sofaNode.heartReateLabel = apacheList[i].heartReateLabel;

                        sofaNode.potassiumitemid = apacheList[i].potassiumitemid;
                        sofaNode.potassiumFlag = apacheList[i].potassiumFlag;
                        sofaNode.potassiumValue = apacheList[i].potassiumValue;
                        sofaNode.potassiumLabel = apacheList[i].potassiumLabel;

                        sofaNode.respiratoryitemid = apacheList[i].respiratoryitemid;
                        sofaNode.respiratoryFlag = apacheList[i].respiratoryFlag;
                        sofaNode.respiratoryValue = apacheList[i].respiratoryValue;
                        sofaNode.respiratoryLabel = apacheList[i].respiratoryLabel;

                        sofaNode.sodiumitemid = apacheList[i].sodiumitemid;
                        sofaNode.sodiumFlag = apacheList[i].sodiumFlag;
                        sofaNode.sodiumValue = apacheList[i].sodiumValue;
                        sofaNode.sodiumLabel = apacheList[i].sodiumLabel;

                        sofaNode.temperatureitemid = apacheList[i].temperatureitemid;
                        sofaNode.temperatureFlag = apacheList[i].temperatureFlag;
                        sofaNode.temperatureValue = apacheList[i].temperatureValue;
                        sofaNode.temperatureLabel = apacheList[i].temperatureLabel;

                        sofaNode.WBCitemid = apacheList[i].WBCitemid;
                        sofaNode.WBCFlag = apacheList[i].WBCFlag;
                        sofaNode.WBCValue = apacheList[i].WBCValue;
                        sofaNode.WBCLabel = apacheList[i].WBCLabel;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
