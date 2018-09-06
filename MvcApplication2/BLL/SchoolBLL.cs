using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SchoolBLL
    {
        SchoolDAL _schoolDAL = new SchoolDAL();

        public int GetDataCount(string schyear, string vacation)
        {
            return _schoolDAL.GetDataCount(schyear,vacation);
        }
        public DataTable GetDataTable(string starIndex,string endIndex,string schyear, string vacation)
        {
            return _schoolDAL.GetDataTable(starIndex,endIndex,schyear,vacation);
        }
        public DataTable SchoolDownLoad(string starIndex, string endIndex, string schyear, string vacation)
        {
            DataTable dt = new DataTable();
            dt = _schoolDAL.GetDataTable("", "", schyear, vacation);
            DataTable dd = new DataTable();
            dd.Columns.Add("序号");
            dd.Columns.Add("姓名");
            dd.Columns.Add("学号");
            dd.Columns.Add("开始时间");
            dd.Columns.Add("离校时间");
            dd.Columns.Add("现宿舍");
            dd.Columns.Add("原宿舍");
            dd.Columns.Add("审批人");
            dd.Columns.Add("联系电话");
            dd.Columns.Add("备注");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dd.NewRow();
                    r["序号"] = dt.Rows[i]["row"].ToString();
                    r["姓名"] = dt.Rows[i]["name"].ToString();
                    r["学号"] = dt.Rows[i]["sno"].ToString();
                    r["开始时间"] = dt.Rows[i]["s_time"].ToString();
                    r["离校时间"] = dt.Rows[i]["e_time"].ToString();
                    r["现宿舍"] = dt.Rows[i]["pdorm"].ToString();
                    r["原宿舍"] = dt.Rows[i]["odorm"].ToString();
                    r["审批人"] = dt.Rows[i]["leader"].ToString();
                    r["联系电话"] = dt.Rows[i]["tel"].ToString();
                    r["备注"] = dt.Rows[i]["remark"].ToString();
                    dd.Rows.Add(r);
                }
            }
            return dd;
        }

        public bool deleteShool(string id)
        {
            return _schoolDAL.deleteSchool(id);
        }

        public bool modifySchool(SchoolModel school)
        {
            return _schoolDAL.modifySchool(school);
        }

        public bool addSchool(SchoolModel school)
        {
            return _schoolDAL.addSchool(school);
        }

        public DataTable schYear()
        {
            DataTable year = new DataTable();
            int y = int.Parse(DateTime.Now.Year.ToString());
            year.Columns.Add("schyear");
            DataRow r1 = year.NewRow();
            DataRow r2 = year.NewRow();
            DataRow r3 = year.NewRow();
            string sy1 = (y - 1) + "-" + y + "学年";
            string sy2 = (y - 2) + "-" + (y - 1) + "学年";
            string sy3 = (y - 3) + "-" + (y - 2) + "学年";
            r1[0] = sy1;
            r2[0] = sy2;
            r3[0] = sy3;
            year.Rows.Add(r1);
            year.Rows.Add(r2);
            year.Rows.Add(r3);
            return year;
        }

        public DataTable stuSchoolTable(string sno)
        {
            return _schoolDAL.stuSchoolTable(sno);
        }

    }
}
