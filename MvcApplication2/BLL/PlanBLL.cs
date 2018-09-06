using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PlanBLL
    {
        PlanDAL _planDAL = new PlanDAL();

        public DataTable sdeptTable()
        {
            return _planDAL.sdeptTable();
        }

        public DataTable buildTable()
        {
            return _planDAL.buildTable();
        }

        public DataTable selectNoPlanStu(string sdept,string sex,string grade)
        {
            return _planDAL.selectNoPlanStu(sdept, sex,grade);
        }

        public DataTable selectBuild(string bid)
        {
            DataTable dt = _planDAL.selectBuild(bid);
            DataTable dd = new DataTable();
            dd.Columns.Add("one");
            dd.Columns.Add("two");
            dd.Columns.Add("three");
            dd.Columns.Add("four");
            dd.Columns.Add("five");
            dd.Columns.Add("six");
            dd.Columns.Add("seven");
            dd.Columns.Add("eight");
            dd.Columns.Add("nine");
            dd.Columns.Add("ten");
            int k = 0;
            string no, count;
            for (int i = 0; i < (dt.Rows.Count / 10); i++)
            {
                DataRow r = dd.NewRow();
                for (int j = 0; j < dd.Columns.Count; j++,k++)
                {
                     no = dt.Rows[k][0].ToString();
                     count = dt.Rows[k][1].ToString();
                    r[j] = no + "(" + count + ")";
                }
                dd.Rows.Add(r);
            }
            return dd;
        }

        public DataTable selectDormCount(string dorm)
        {
            return _planDAL.selectDormCount(dorm);
        }

      public static  Dictionary<string, int> dictionary = new Dictionary<string, int>();

        public void dormAndcount(string dorm)
        {
            DataTable dt = new DataTable();
            dt = _planDAL.selectDormCount(dorm);
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dictionary.Add(dt.Rows[i]["dno"].ToString(),int.Parse(dt.Rows[i]["ct"].ToString()));
            }
            //return dictionary;
        }

        public bool updateDorm(string bid, string dno, string sno)
        {
            return _planDAL.updateDorm(bid, dno, sno);
        }

        public DataTable selectGrage()
        {
            string year=DateTime.Now.Year.ToString();
            int y=int.Parse(year.Substring(2));
            DataTable dt = new DataTable();
            dt.Columns.Add("g");
            DataRow r = dt.NewRow();
            r[0] = y - 1;
            dt.Rows.Add(r);
            DataRow r1 = dt.NewRow();
            r1[0] = y - 2;
            dt.Rows.Add(r1);
            DataRow r2 = dt.NewRow();
            r2[0] = y - 3;
            dt.Rows.Add(r2);
            DataRow r3 = dt.NewRow();
            r3[0] = y - 4;
            dt.Rows.Add(r3);
            return dt;
        }

        public bool backStu(string grade)
        {
            return _planDAL.backStu(grade);
        }
    }
}
