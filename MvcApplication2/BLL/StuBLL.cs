using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StuBLL
    {
        public StuDAL _stuDAL = new StuDAL();

        public DataTable selectStuAll(string starIndex,string endIndex,string school)
        {
            return _stuDAL.selectStuAll(starIndex,endIndex,school);
        }

        public DataTable StuDownLoad(string school)
        {
            DataTable dt = new DataTable();
            dt = _stuDAL.selectStuAll("", "", school);
            DataTable dd = new DataTable();
            dd.Columns.Add("序号");
            dd.Columns.Add("学号");
            dd.Columns.Add("姓名");
            dd.Columns.Add("性别");
            dd.Columns.Add("系别");
            dd.Columns.Add("宿舍");
            dd.Columns.Add("入学时间");
            dd.Columns.Add("拟毕业时间");
            dd.Columns.Add("修改时间");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dd.NewRow();
                    r["序号"] = dt.Rows[i]["row"].ToString();
                    r["学号"] = dt.Rows[i]["sno"].ToString();
                    r["姓名"] = dt.Rows[i]["sname"].ToString();
                    r["性别"] = dt.Rows[i]["ssex"].ToString();
                    r["系别"] = dt.Rows[i]["sdept"].ToString();
                    r["宿舍"] = dt.Rows[i]["dorm"].ToString();
                    r["入学时间"] = dt.Rows[i]["s_time"].ToString();
                    r["拟毕业时间"] = dt.Rows[i]["e_time"].ToString();
                    r["修改时间"] = dt.Rows[i]["m_time"].ToString();
                    dd.Rows.Add(r);
                }
            }
            return dd;
        }

        public string pageAll(string school)
        {
            return _stuDAL.pageAll(school);
        }

        public DataTable select_Son(string sno,string school)
        {
            return _stuDAL.select_Sno(sno,school);
        }

        public bool ModifyStu(string sno,string sname,string ssex,string sdept, string dorm,string s_time, string e_time, string m_time)
        {
            return _stuDAL.ModifyStu(sno,sname,ssex,sdept, dorm,s_time, e_time, m_time);
        }

        public bool AddStu(string sno, string sname, string ssex, string sdept, string dorm, string s_time, string e_time, string m_time,string school)
        {
            return _stuDAL.AddStu(sno, sname, ssex, sdept, dorm, s_time, e_time, m_time,school);
        }

        //TryStu

       
        public DataTable selectGrade()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("g");
            string dtime = DateTime.Now.Year.ToString();//2018
            int y = int.Parse(dtime.Substring(2));//18
           // dt.Rows.Add(y);//18
            dt.Rows.Add(y - 1);//17
            dt.Rows.Add(y - 2);//16
            dt.Rows.Add(y - 3);//15
            dt.Rows.Add(y - 4);//14
            return dt;
        }
        public DataTable selectDept()
        {
            DataTable dt = new DataTable();
            dt = _stuDAL.selectDept();
            return dt;
        }

        public string GetDataCount(string grade,string dept,string sno,string sch)
        {
            return _stuDAL.GetDataCount(grade, dept, sno, sch);
        }

        public DataTable GetDataTable(string starIndex, string endIndex, string grade, string dept, string sno, string sch)
        {
            return _stuDAL.GetDataTable(starIndex,endIndex,grade,dept,sno,sch);
        }

        public DataTable TryStuExport(string grade, string dept, string sch)
        {
            DataTable dt = _stuDAL.GetDataTable("", "", grade, dept, "", sch);
            DataTable dd = new DataTable();
            dd.Columns.Add("序号");
            dd.Columns.Add("学号");
            dd.Columns.Add("姓名");
            dd.Columns.Add("性别");
            dd.Columns.Add("系别");
            dd.Columns.Add("宿舍");
            dd.Columns.Add("入学时间");
            dd.Columns.Add("拟毕业时间");
            dd.Columns.Add("修改时间");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dd.NewRow();
                    r["序号"] = dt.Rows[i]["row"].ToString();
                    r["学号"] = dt.Rows[i]["sno"].ToString();
                    r["姓名"] = dt.Rows[i]["sname"].ToString();
                    r["性别"] = dt.Rows[i]["ssex"].ToString();
                    r["系别"] = dt.Rows[i]["sdept"].ToString();
                    r["宿舍"] = dt.Rows[i]["dorm"].ToString();
                    r["入学时间"] = dt.Rows[i]["s_time"].ToString();
                    r["拟毕业时间"] = dt.Rows[i]["e_time"].ToString();
                    r["修改时间"] = dt.Rows[i]["m_time"].ToString();
                    dd.Rows.Add(r);
                }
            }
            return dd;
        }

        public DataTable GetUserInfo(string name)
        {
            return _stuDAL.GetUserInfo(name);
        }
    }
}
