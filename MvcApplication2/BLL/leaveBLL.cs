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
    public class leaveBLL
    {
        leaveDAL _leaveDal = new leaveDAL();
        LeaveModel _leaveMode=new LeaveModel();

        public int GetDataCount(string s_date,string e_date,string bid)
        {
            return _leaveDal.GetDataCount(s_date,e_date,bid);
        }

        public DataTable GetDataTable(string starIndex,string endIndex,string s_date,string e_date,string bid)
        {
            return _leaveDal.GetDataTable(starIndex,endIndex,s_date,e_date,bid);
        }

        public DataTable DownLoadTable(string starIndex, string endIndex, string s_date, string e_date, string bid)
        {
            DataTable dt = new DataTable();
            dt = _leaveDal.GetDataTable(starIndex, endIndex, s_date, e_date, bid);
            dt.Columns.Remove("id");
            dt.Columns.Remove("d");
            DataTable dd = new DataTable();
            dd.Columns.Add("序号");
            dd.Columns.Add("来访者");
            dd.Columns.Add("被访者");
            dd.Columns.Add("宿舍号");
            dd.Columns.Add("原因");
            dd.Columns.Add("批准人");
            dd.Columns.Add("来到时间");
            dd.Columns.Add("离开时间");
            dd.Columns.Add("联系电话");
            dd.Columns.Add("备注");
            dd.Columns.Add("楼号");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dd.NewRow();
                    r["序号"]=dt.Rows[i]["row"].ToString();
                    r["来访者"] = dt.Rows[i]["lname"].ToString();
                    r["被访者"] = dt.Rows[i]["iname"].ToString();
                    r["宿舍号"] = dt.Rows[i]["dorm"].ToString();
                    r["原因"] = dt.Rows[i]["reason"].ToString();
                    r["批准人"] = dt.Rows[i]["leader"].ToString();
                    r["来到时间"] = dt.Rows[i]["s_time"].ToString();
                    r["离开时间"] = dt.Rows[i]["e_time"].ToString();
                    r["联系电话"] = dt.Rows[i]["tel"].ToString();
                    r["备注"] = dt.Rows[i]["remark"].ToString();
                    r["楼号"] = dt.Rows[i]["bid"].ToString();
                    dd.Rows.Add(r);
                }
            }
            return dd;
        }

        public DataTable GetDownList()
        {
            return _leaveDal.GetDownList();
        }

        public bool modifyLeave(LeaveModel _leaveModel)
        {
            return _leaveDal.modifyLeave(_leaveModel);
        }

        public bool deleteLeave(string id)
        {
            return _leaveDal.deleteLeave(id);
        }

        public bool addLeave(LeaveModel leave)
        {
            return _leaveDal.addLeave(leave);
        }
    }
}
