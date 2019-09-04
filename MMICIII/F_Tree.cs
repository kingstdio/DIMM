using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMICIII
{
    public partial class F_Tree : Form
    {
        public static string sql = string.Empty;

        string[] selectedLevelString = new string[10];
        DataTable[] fillDt = new DataTable[10];




        public F_Tree()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            sql = "select DISTINCT level1 FROM eicu_crd.\"sup_diagnosisPath\" ORDER BY level1;";
            DataTable level1dt =  PGSQLHELPER.excuteDataTable(sql);
            cmb_level1.DataSource = level1dt;
            cmb_level1.DisplayMember = "level1";
            cmb_level1.ValueMember = "level1";
            cmb_level1.SelectedIndex = 0;
        }

        private void cmb_level1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sql = "select distinct level2 FROM eicu_crd.\"sup_diagnosisPath\" where level1='"+cmb_level1.Text+"' ORDER BY level2;";
            DataTable level2dt = PGSQLHELPER.excuteDataTable(sql);
            cmb_level2.DataSource = level2dt;
            cmb_level2.DisplayMember = "level2";
            cmb_level2.ValueMember = "level2";
        }

        private void bt_show_Click(object sender, EventArgs e)
        {

        }

        private void getSteletedLevelString()
        {
            selectedLevelString[1] = cmb_level1.Text.Replace("'","''");
            selectedLevelString[2] = cmb_level2.Text.Replace("'", "''");
            selectedLevelString[3] = cmb_level3.Text.Replace("'", "''");
            selectedLevelString[4] = cmb_level4.Text.Replace("'", "''");
            selectedLevelString[5] = cmb_level5.Text.Replace("'", "''");
            selectedLevelString[6] = cmb_level6.Text.Replace("'", "''");
            selectedLevelString[7] = cmb_level7.Text.Replace("'", "''");
            selectedLevelString[8] = cmb_level8.Text.Replace("'", "''");
        }


        private void getFillTable()
        {
            sql = "select DISTINCT level1 FROM eicu_crd.\"sup_diagnosisPath\" ORDER BY level1;";
            fillDt[1] = PGSQLHELPER.excuteDataTable(sql);
            cmb_level1.DataSource = fillDt[1];
            cmb_level1.DisplayMember = "level1";
            cmb_level1.ValueMember = "level1";
            cmb_level1.SelectedIndex = 0;


            sql = "select distinct level2 FROM eicu_crd.\"sup_diagnosisPath\" where level1='" + cmb_level1.Text + "' ORDER BY level2;";
            fillDt[2] = PGSQLHELPER.excuteDataTable(sql);
            cmb_level2.DataSource = fillDt[2];
            cmb_level2.DisplayMember = "level2";
            cmb_level2.ValueMember = "level2";


        }

        private void cmb_level2_SelectedIndexChanged(object sender, EventArgs e)
        {
            getSteletedLevelString();

            sql = "select distinct level3 FROM eicu_crd.\"sup_diagnosisPath\" where level1='"+selectedLevelString[1]+"' and level2='"+selectedLevelString[2]+"' ORDER BY level3;";
            fillDt[3] = PGSQLHELPER.excuteDataTable(sql);
            cmb_level3.DataSource = fillDt[3];
            cmb_level3.DisplayMember = "level3";
            cmb_level3.ValueMember = "level3";
            cmb_level3.SelectedIndex = 0;
        }



        private void outputDataTable(DataTable dt)
        {
            rtb_out.Clear();
            foreach(DataRow dr in dt.Rows)
            {
                rtb_out.AppendText(dr[0].ToString());
                rtb_out.AppendText("\r\n");
            }
        }

        private void cmb_level3_SelectedIndexChanged(object sender, EventArgs e)
        {
            getSteletedLevelString();
            sql = "select distinct level4 FROM eicu_crd.\"sup_diagnosisPath\" where level1='" + selectedLevelString[1] + "' and level2='" + selectedLevelString[2] + "' and level3='"+selectedLevelString[3]+"' ORDER BY level4;";
            fillDt[4] = PGSQLHELPER.excuteDataTable(sql);
            cmb_level4.DataSource = fillDt[4];
            cmb_level4.DisplayMember = "level4";
            cmb_level4.ValueMember = "level4";
            outputDataTable(fillDt[4]);
        }
    }
}
