using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MMICIII.Utils
{
    public class FilterTools
    {
        #region 加载映射字典
        /// <summary>
        /// 加载映射字典
        /// </summary>
        /// <param name="dicPath">字典路径</param>
        /// <returns></returns>
        public static List< mapNode> loadSofaExtendedMapDic(string dicPath)
        {
            List<mapNode> map = new List<mapNode>();
            FileStream file = new FileStream(dicPath, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string strline = reader.ReadLine();
            string []strArray;
            while ((strline = reader.ReadLine()) != null)
            {
                strArray = strline.Split(',');
                map.Add(new mapNode(strArray[0], Convert.ToDouble(strArray[1])));
            }
            return map;
        }
        #endregion

        public static bool isContainCharicter(DataTable input)
        {
            foreach(DataRow dr in input.Rows)
            {
                foreach(var col in dr.ItemArray)
                {
                    if (!CommonTools.isNumber(col.ToString()))
                    {
                        Console.WriteLine(col);
                        return true;
                    }
                }
            }

            return false;

        }


    }

    #region 字典映射类
    /// <summary>
    /// 字典映射类
    /// </summary>
    public class mapNode
    {
        public string mapKey { get; set; }
        public double mapValue { get; set; }

        public mapNode() {
        }

        public mapNode(string mapKey, double mapValue)
        {
            this.mapKey = mapKey;
            this.mapValue = mapValue;
        }
    }
    #endregion

    

}
