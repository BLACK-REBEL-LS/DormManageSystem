using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RepairModel
    {
        public string id { get; set; }

        public string dorm { get; set; }

        public string category { get; set; }

        public int handle { get; set; }

        public string s_time { get; set; }
        public string e_time { get; set; }
        public string rname { get; set; }
        public string wname { get; set; }
        public string remark { get; set; }
        public string sno { get; set; }
    }
}
