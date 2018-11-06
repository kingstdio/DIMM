using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using MMICIII.Utils;

namespace MMICIII
{
    public partial class F_missing : Form
    {

        #region Fileds

        #endregion

        #region init

        public F_missing()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            //创建基础目录
            if (!Directory.Exists(GlobalVars.EXPFILEBASE))
            {
                Directory.CreateDirectory(GlobalVars.EXPFILEBASE);
            }

            //初始化ICD
            GlobalVars.CURRENTICDCODE  = tb_icd9.Text.Trim();

            //设置状态栏
            tss_baselocation.Text = GlobalVars.EXPFILEBASE;
            tss_icd9.Text = GlobalVars.CURRENTICDCODE;

            //初始化选择参数
            tb_timespan.Text = GlobalVars.TIMESPAN.ToString();
            tb_threads.Text = GlobalVars.threadCount.ToString();
            tb_deathThresholds.Text = GlobalVars.DEATHCOUNTDAY.ToString();
            tb_minstay.Text = GlobalVars.ICUSTDAYMINLENGTH.ToString();
            tb_maxstay.Text = GlobalVars.ICUSTDAYMAXLENGTH.ToString();
            sw_1p1u.Value = GlobalVars.IS1P1U;
            sw_fill.Value = GlobalVars.FILLMISSING;
            tb_chartFC.Text = GlobalVars.CHARTFEATURENUM.ToString();
            tb_inputFC.Text = GlobalVars.INPUTFEATURENUM.ToString();

            cmb_filleype.DataSource = System.Enum.GetNames(typeof(MISSINGFILLTYPE));

            GlobalVars.FillMissingTpe = MISSINGFILLTYPE.CopyPrevious;

            tb_relatedLevel.Text = GlobalVars.DISEASERELATEDLEVEL.ToString();
            tb_knear.Text = GlobalVars.KNEAR.ToString();
            if (GlobalVars.FILLMISSING)
            {
                tb_knear.Enabled = true;
                cmb_filleype.Enabled = true;
            }
            else
            {
                tb_knear.Enabled = false;
                cmb_filleype.Enabled = false;
            }

            sw_marsk.Value = GlobalVars.OUTPUTMASK;
            sw_interval.Value = GlobalVars.OUTPUTINTERVAL;
        }
        #endregion

        #region 绑定界面值改变
        private void tb_icd9_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.CURRENTICDCODE = tb_icd9.Text.Trim();
        }

        private void tb_timespan_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.TIMESPAN = Convert.ToInt16(tb_timespan.Text.Trim());
        }

        private void tb_deathThresholds_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.DEATHCOUNTDAY = Convert.ToInt16(tb_deathThresholds.Text.Trim());
        }

        private void tb_threads_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.threadCount = Convert.ToInt16(tb_threads.Text.Trim());
        }

        private void sw_1p1u_ValueChanged(object sender, EventArgs e)
        {
            GlobalVars.IS1P1U = sw_1p1u.Value;
        }

        private void tb_minstay_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.ICUSTDAYMINLENGTH = Convert.ToInt16(tb_minstay.Text.Trim()); 
        }

        private void tb_maxstay_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.ICUSTDAYMAXLENGTH = Convert.ToInt16(tb_maxstay.Text.Trim());
        }

        private void sw_fill_ValueChanged(object sender, EventArgs e)
        {
            GlobalVars.FILLMISSING = sw_fill.Value;
            if (GlobalVars.FILLMISSING)
            {
                tb_knear.Enabled = true;
                cmb_filleype.Enabled = true;
            }
            else
            {
                tb_knear.Enabled = false;
                cmb_filleype.Enabled = false;
            }
        }

        private void tb_chartFC_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.CHARTFEATURENUM = Convert.ToInt16(tb_chartFC.Text.Trim());
        }

        private void tb_inputFC_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.INPUTFEATURENUM = Convert.ToInt16(tb_inputFC.Text.Trim());
        }

        private void cmb_filleype_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVars.FillMissingTpe = (MISSINGFILLTYPE)Enum.Parse(typeof(MISSINGFILLTYPE), cmb_filleype.SelectedItem.ToString(), false);
            if(GlobalVars.FillMissingTpe == MISSINGFILLTYPE.KNearMean)
            {
                tb_knear.Enabled = true;
            }
            else
            {
                tb_knear.Enabled = false;
            }
        }

        private void tb_relatedLevel_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.DISEASERELATEDLEVEL = Convert.ToInt16(tb_relatedLevel.Text);
        }
        private void tb_knear_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.KNEAR = Convert.ToInt16(tb_knear.Text.Trim());
        }
        private void sw_marsk_ValueChanged(object sender, EventArgs e)
        {
            GlobalVars.OUTPUTMASK = sw_marsk.Value;
        }

        private void swInterval_ValueChanged(object sender, EventArgs e)
        {
            GlobalVars.OUTPUTINTERVAL = sw_interval.Value;
        }
        #endregion

        private void bt_pd1_Click(object sender, EventArgs e)
        {
            string pathInput_out = GlobalVars.EXPFILEBASE + @"_" +
                (GlobalVars.ICUSTDAYMAXLENGTH * 60 / GlobalVars.TIMESPAN) +
                @"\" + GlobalVars.CURRENTICDCODE + @"\";
            MissingValueTools.outputMataData(pathInput_out);//输出mata信息
            MissingValueTools.constructExpData(GlobalVars.CURRENTICDCODE);
        }

        #region 计算ChartEvent各项Feature的均值与方差
        /// <summary>
        /// 计算ChartEvent各项Feature的均值与方差
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_calmean_Click(object sender, EventArgs e)
        {
            //MissingValueTools.caclChartEventMeanMSE();
            MissingValueTools.caclInputEventMeanMSE();

        }

        #endregion

   
    }
}
