using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMICIII.Utils
{
    public class MathTools
    {

        #region 判断是不是跳过的列
        /// <summary>
        /// 判断是不是跳过的列
        /// </summary>
        /// <param name="col"></param>
        /// <param name="colArray"></param>
        /// <returns></returns>
        private static bool isSkipCol(int col, int[] colArray)
        {
            foreach (int item in colArray)
            {
                if (col == item)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region 按DataTable的列计算均值与和
        /// <summary>
        /// 按DataTable的列计算均值与和缺失率 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataRow[] calSumMeanByColumns(DataTable dt, int [] skipCol)
        {
            DataRow[] result = new DataRow[3]; 
            DataRow totalDr = dt.NewRow();
            DataRow meanDr = dt.NewRow();
            DataRow mssingRateDr = dt.NewRow();
            int colcount = dt.Columns.Count;
            int rowcount = dt.Rows.Count;
            int[] numCount = new Int32[colcount];

            for(int j=0; j < colcount; j++)
            {
                if (isSkipCol(j, skipCol))
                {
                    continue;
                }

                double sum = 0;
                double cellValue=0;
                int vrowCount = 0;
                for(int i = 0; i < rowcount; i++)
                {
                    if (dt.Rows[i][j].ToString() != string.Empty)
                    {
                        cellValue = Convert.ToDouble(dt.Rows[i][j]);
                        sum += cellValue;
                        vrowCount++;
                    }
                }
                totalDr[j] = sum;
                if (vrowCount != 0)
                {
                    meanDr[j] = sum / vrowCount;
                    mssingRateDr[j] = 1- (double)vrowCount / rowcount;
                }
            }

            result[0] = totalDr;
            result[1] = meanDr;
            result[2] = mssingRateDr;



            return result;
        }

        public static DataRow[] calSumMeanByColumns(DataTable dt)
        {
            DataRow[] result = new DataRow[3];
            DataRow totalDr = dt.NewRow();
            DataRow meanDr = dt.NewRow();
            DataRow mssingRateDr = dt.NewRow();
            int colcount = dt.Columns.Count;
            int rowcount = dt.Rows.Count;
            int[] numCount = new Int32[colcount];

            for (int j = 0; j < colcount; j++)
            {
                double sum = 0;
                double cellValue = 0;
                int vrowCount = 0;
                for (int i = 0; i < rowcount; i++)
                {
                    if (dt.Rows[i][j].ToString() != string.Empty)
                    {
                        cellValue = Convert.ToDouble(dt.Rows[i][j]);
                        sum += cellValue;
                        vrowCount++;
                    }
                }
                totalDr[j] = sum;
                if (vrowCount != 0)
                {
                    meanDr[j] = sum / vrowCount;
                    mssingRateDr[j] = 1 - (double)vrowCount / rowcount;
                }
            }

            result[0] = totalDr;
            result[1] = meanDr;
            result[2] = mssingRateDr;



            return result;
        }

        #endregion

        #region 按列计算标准差
        /// <summary>
        /// 按列计算标准差
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="mean"></param>
        /// <returns></returns>
        public static DataRow calStdByColum(DataTable dt, DataRow mean, int [] skipCols)
        {
            DataRow stdDr = dt.NewRow();
            int rowcount = dt.Rows.Count;
            int colcount = dt.Columns.Count;
            for(int i = 0; i < colcount; i++)
            {
                if (isSkipCol(i, skipCols))
                {
                    continue;
                }

                double stdev = 0;
                int count = 0;
                for (int j = 0; j < rowcount; j++)
                {
                    double meanl = 0;
                    double xi = 0;
                    if (dt.Rows[j][i].ToString() != string.Empty)
                    {
                        xi = Convert.ToDouble(dt.Rows[j][i]);
                        meanl = Convert.ToDouble(mean[i]);
                        count++;
                        stdev = stdev + Math.Pow(xi - meanl, 2);
                    }
                }
                if (count != 0)
                {
                    stdDr[i] = Math.Sqrt(stdev /count);
                }
            }

            return stdDr;
        }

        public static DataRow calStdByColum(DataTable dt, DataRow mean)
        {
            DataRow stdDr = dt.NewRow();
            int rowcount = dt.Rows.Count;
            int colcount = dt.Columns.Count;
            for (int i = 0; i < colcount; i++)
            {
                double stdev = 0;
                int count = 0;
                for (int j = 0; j < rowcount; j++)
                {
                    double meanl = 0;
                    double xi = 0;
                    if (dt.Rows[j][i].ToString() != string.Empty)
                    {
                        xi = Convert.ToDouble(dt.Rows[j][i]);
                        meanl = Convert.ToDouble(mean[i]);
                        count++;
                        stdev = stdev + Math.Pow(xi - meanl, 2);
                    }
                }
                if (count != 0)
                {
                    stdDr[i] = Math.Sqrt(stdev / count);
                }
            }

            return stdDr;
        }

        #endregion

        #region 计算突发性
        /// <summary>
        /// 计算突发性
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="delta">标准差</param>
        /// <param name="miu">均值</param>
        /// <returns></returns>
        public static DataRow calBursty(DataTable orig, DataRow delta, DataRow miu)
        {
            DataRow result = orig.NewRow();
            int col = delta.ItemArray.Count();
            for(int i = 0; i < col; i++)
            {
                if (miu[i].ToString() != string.Empty)
                {
                    double amiu = Convert.ToDouble(miu[i]);
                    double adelta = Convert.ToDouble(delta[i]);
                    if ((amiu + adelta) != 0)
                    {
                        result[i] = (adelta - amiu) / (amiu + adelta);
                    }
                }

            }
            return result;
        }


        public static DataRow calBursty(DataTable orig)
        {

            DataRow delta, miu;
            DataRow[] drarray = MathTools.calSumMeanByColumns(orig);
            miu = drarray[1];
            delta = MathTools.calStdByColum(orig, miu);

            DataRow result = orig.NewRow();
            int col = delta.ItemArray.Count();
            for (int i = 0; i < col; i++)
            {
                if (miu[i].ToString() != string.Empty)
                {
                    double amiu = Convert.ToDouble(miu[i]);
                    double adelta = Convert.ToDouble(delta[i]);
                    result[i] = (adelta - amiu) / (amiu + adelta);
                }
            }
            return result;
        }

        #endregion


        #region 按列结算指标的累积缺失率
        /// <summary>
        /// 按列结算指标的累积缺失率
        /// </summary>
        /// <param name="originTable">需要计算的DataTable</param>
        /// <returns></returns>
        public static DataTable calAccumulatedMissingRateByCol(DataTable originTable)
        {
            DataTable mrTable = originTable.Copy();
            int colCount = originTable.Columns.Count;
            int rowCount = originTable.Rows.Count;

            for(int j = 1; j < colCount; j++)
            {
                double mrate = 0;
                double ecount = 0;
                for(int i = 0; i < rowCount; i++)
                {
                    if (originTable.Rows[i][j].ToString() != string.Empty)
                    {
                        ecount++;
                    }

                    mrate = ecount / (i + 1);
                    mrTable.Rows[i][j] = 1 - mrate;
                }
            }


            return mrTable;
        }
        #endregion
    }
}
