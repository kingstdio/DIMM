using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMICIII.Utils
{
    public class ChartTools
    {
        #region 构建chart列表
        /// <summary>
        /// 构建chart列表
        /// </summary>
        /// <param name="trainDataTable">查询到的ChartTable</param>
        /// <returns>构建完成的ChartList</returns>
        public static List<ChartNode> buildChartList(DataTable trainDataTable)
        {
            int count = 0;
            List<ChartNode> chartList = new List<ChartNode>();
            foreach (DataRow dr in trainDataTable.Rows)
            {
                ChartNode chartNode = new ChartNode();

                chartNode.row_id = Convert.ToInt64(dr[0]);
                chartNode.subject_id = Convert.ToInt64(dr[1]);
                chartNode.hadm_id = Convert.ToInt64(dr[2]);
                chartNode.icustay_id = Convert.ToInt64(dr[3]);
                chartNode.itemid = Convert.ToInt64(dr[4]);


                if (dr[5].ToString() != string.Empty)
                {
                    chartNode.charttime = Convert.ToDateTime(dr[5]);
                }

                if (dr[6].ToString() != string.Empty)
                {
                    chartNode.storetime = Convert.ToDateTime(dr[6]);
                }

                if (dr[7].ToString() != string.Empty)
                {
                    chartNode.cgid = Convert.ToInt32(dr[7]);
                }

                if (dr[8].ToString() != string.Empty)
                {
                    chartNode.value = dr[8].ToString();
                }

                if (dr[9].ToString() != String.Empty)
                {
                    chartNode.valuenum = Convert.ToDouble(dr[9]);
                }

                chartNode.valueom = dr[10].ToString();

                if (dr[11].ToString() != String.Empty)
                {
                    chartNode.warning = Convert.ToInt32(dr[11]);
                }

                if (dr[12].ToString() != String.Empty)
                {
                    chartNode.error = Convert.ToInt32(dr[12]);
                }

                chartNode.restultstatus = dr[13].ToString();
                chartNode.stoped = dr[14].ToString();
                chartNode.label = dr[15].ToString();
                chartNode.item_unit = dr[16].ToString();
                chartList.Add(chartNode);
                count++;
            }

            return chartList;
        }
        #endregion
    }
}
