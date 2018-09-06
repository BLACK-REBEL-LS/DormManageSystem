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
    public class disciplineBLL
    {
        disciplineDAL _disciplineDAl = new disciplineDAL();
        disciplineModel _discipline = new disciplineModel();

        public int GetDataCount(string s_time,string e_time,string bid)
        {
            return _disciplineDAl.GetDataCount(s_time,e_time,bid);
        }

        public DataTable GetDataTable(string starIndex,string endIndex,string s_time,string e_time,string bid)
        {
            return _disciplineDAl.GetDataTable(starIndex,endIndex,s_time,e_time,bid);
        }

        public DataTable DisciplineDownLoad(string starIndex, string endIndex, string s_time, string e_time, string bid)
        {
            DataTable dt = new DataTable();
            dt = _disciplineDAl.GetDataTable(starIndex, endIndex, s_time, e_time, bid);
            DataTable dd = new DataTable();
            dd.Columns.Add("序号");
            dd.Columns.Add("姓名");
            dd.Columns.Add("宿舍");
            dd.Columns.Add("原因");
            dd.Columns.Add("处罚方式");
            dd.Columns.Add("审批人");
            dd.Columns.Add("时间");
            dd.Columns.Add("备注");
            dd.Columns.Add("楼号");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dd.NewRow();
                    r["序号"] = dt.Rows[i]["row"].ToString();
                    r["姓名"] = dt.Rows[i]["dname"].ToString();
                    r["宿舍"] = dt.Rows[i]["dorm"].ToString();
                    r["原因"] = dt.Rows[i]["reason"].ToString();
                    r["处罚方式"] = dt.Rows[i]["punish"].ToString();
                    r["审批人"] = dt.Rows[i]["leader"].ToString();
                    r["时间"] = dt.Rows[i]["dtime"].ToString();
                    r["备注"] = dt.Rows[i]["remark"].ToString();
                    r["楼号"] = dt.Rows[i]["bid"].ToString();
                    dd.Rows.Add(r);
                }
            }
            return dd;
        }

        public bool modifyDiscipline(disciplineModel discipline)
        { 
        return _disciplineDAl.modifyDiscipline(discipline);
        }

        public bool deleteDiscipline(string id)
        {
            return _disciplineDAl.deleteDiscipline(id);
        }

        public bool addDiscipline(disciplineModel discipline)
        {
            return _disciplineDAl.addDisicipline(discipline);
        }
    }
}
