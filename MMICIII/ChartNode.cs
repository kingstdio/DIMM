using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMICIII
{
    public class ChartNode
    {
        public Int64 row_id { get; set; }
        public Int64 subject_id { get; set; }
        public Int64 hadm_id { get; set; }
        public Int64 icustay_id { get; set; }
        public Int64 itemid { get; set; }
        public DateTime charttime { get; set; }
        public DateTime storetime { get; set; }
        public int cgid { get; set; }
        public string value { get; set; }
        public double valuenum { get; set; }
        public string valueom { get; set; }
        public int warning { get; set; }
        public int error { get; set; }
        public string restultstatus { get; set; }
        public string stoped { get; set; }
        public string label { get; set; }
        public string item_unit { get; set; }
        public bool f_unit { get; set; }
        public int timeSqid { get; set; }
    }
}
