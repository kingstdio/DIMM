using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMICIII
{
    public class SofaFeature
    {
        private static int[] clistPao2 = {779};
        private static int[] mlistPao2 = {220224};

        private static int[] clistFio2 = { 1040, 1206, 185, 186, 189, 190, 191, 727, 3420, 3421, 3422, 1863, 5955, 2518, 2981, 7570, 8517 };
        private static int[] mlistFio2 = { 227009, 227010, 223835, 226754 };

        private static int[] clistPlatelets = { 828, 3789, 6256, 30006, 30105 };
        private static int[] mlistPlatelets = { 225170, 227071, 227457, 225678, 226369 };

        private static int[] clistBilirubin = { 848, 803, 4948, 5750, 5885, 5901, 3765, 1527, 1538, 6158, 6193, 5045 };
        private static int[] mlistBilirubin = { 226998, 226999, 225651, 225690 };

        private static int[] clistABP = {52 };
        private static int[] mlistABP = { 220052};

        private static int[] clistDobutamine = { 5747, 30306, 30042 };
        private static int[] mlistDobutamine = {221653 };

        private static int[] clistDopamine = { 5329, 30043, 30307 };
        private static int[] mlistDopamine = { 221662 };

        private static int[] clistEpinephrine = { 3112, 30119, 30309, 30044 };
        private static int[] mlistEpinephrine = { 221289, 221906 };

        private static int[] clistNorepinephrine = { };
        private static int[] mlistNorepinephrine = { 221906 };

        private static int[] clistGCS = { 198 };
        private static int[] mlistGCS = { 220739, 227012, 223901, 226757, 227011, 227012, 227014, 220739, 228112, 223900, 223901, 226756, 226757, 226758 };

        private static int[] clistCreatinine = { 791, 3750, 1525 };
        private static int[] mlistCreatinine = { 227005, 220615, 226751, 226752 };

        private static int[] clistUrine = { 40055, 40056, 40057, 40061, 40065, 40069, 40085, 40096, 40405, 40428, 40473, 40651, 40715, 42068, 42510, 43175, 43431, 43522, 43576, 43633, 44080, 44253, 45304, 45415, 45927, 46658, 46748 };
        private static int[] mlistUrine = { 226627, 226631, 227059, 227489, 227519 };




        private static string sql = string.Empty;


        public static bool constructSofaFeatures()
        {
            sql = @"DROP TABLE IF EXISTS mimiciii.sup_vars_sofa;
                    CREATE TABLE mimiciii.sup_vars_sofa (
                      row_id SERIAL NOT NULL PRIMARY KEY,
                      item_id int4,
	                    label varchar(50),
	                    item_cat_up varchar(50),
                      item_cat varchar(50),
                      item_cat_zh varchar(50),
	                    item_unit varchar(50),
	                    item_source varchar(50)
                    );";
            PGSQLHELPER.executeScalar(sql);



            //PAO2
            string cat_f = "Pao2/FIO2";
            string cat_sub = "PaO2";
            string cat_zh = "动脉氧分压";
            foreach (int item in clistPao2)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES ("+item+",'"+cat_f+"', '"+cat_sub+"', '"+cat_zh+"', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistPao2)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }

            //FIO2
            cat_f = "Pao2/FIO2";
            cat_sub = "FIO2";
            cat_zh = "氧体积分数";
            foreach (int item in clistFio2)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistFio2)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }


            //Platelets
            cat_f = "Platelets";
            cat_sub = "Platelets";
            cat_zh = "血小板";
            foreach (int item in clistPlatelets)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistPlatelets)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }

            //Bilirubin
            cat_f = "Bilirubin";
            cat_sub = "Bilirubin";
            cat_zh = "胆红素";
            foreach (int item in clistBilirubin)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistBilirubin)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }

            //Cardiovascular
            cat_f = "Cardiovascular";
            cat_sub = "ABP_mean";
            cat_zh = "平均动脉压";
            foreach (int item in clistABP)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistABP)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }

            cat_sub = "Dobutamine";
            cat_zh = "多巴酚丁胺（强心剂）";
            foreach (int item in clistDobutamine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistDobutamine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }

            cat_sub = "Dopamine";
            cat_zh = "多巴胺";
            foreach (int item in clistDopamine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistDopamine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }

            cat_sub = "Epinephrine";
            cat_zh = "肾上腺素";
            foreach (int item in clistEpinephrine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistEpinephrine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }

            cat_sub = "Norepinephrine";
            cat_zh = "去肾上腺素";
            foreach (int item in clistNorepinephrine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistNorepinephrine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }


            //GCS
            cat_f = "GCS";
            cat_sub = "GCS";
            cat_zh = "昏迷指数";
            foreach (int item in clistGCS)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistGCS)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }

            //Creatinine
            cat_f = "Creatinine";
            cat_sub = "Creatinine";
            cat_zh = "肌酸酐";
            foreach (int item in clistCreatinine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistCreatinine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }

            //Urine
            cat_f = "Urine";
            cat_sub = "Urine";
            cat_zh = "尿";
            foreach (int item in clistUrine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }
            foreach (int item in mlistUrine)
            {
                sql = @"INSERT INTO mimiciii.sup_vars_sofa(item_id,item_cat_up,item_cat, item_cat_zh, item_unit, item_source) VALUES (" + item + ",'" + cat_f + "', '" + cat_sub + "', '" + cat_zh + "', 'mmHg', 'carevue');";
                PGSQLHELPER.executeScalar(sql);
            }

            return true;
        }

    }
}
