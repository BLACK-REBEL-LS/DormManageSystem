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
    public class RepairBLL
    {
        RepairDAL _repairDAL = new RepairDAL();

        public int GetDataCount(string s_time,string e_time,string bid,string caregory,string handle)
        {
            return _repairDAL.GetDataCount(s_time,e_time,bid,caregory,handle);
        }

        public DataTable GetDataTable(string starIndex,string endIndex,string s_time,string e_time,string bid,string caregory,string handle)
        {
            return _repairDAL.GetDataTable(starIndex,endIndex,s_time,e_time,bid,caregory,handle);
        }

        public DataTable RepairDownLoad(string starIndex, string endIndex, string s_time, string e_time, string bid, string caregory, string handle)
        {
            DataTable dt = new DataTable();
            dt = _repairDAL.GetDataTable(starIndex,endIndex,s_time,e_time,bid,caregory,handle);
            DataTable dd = new DataTable();
            dd.Columns.Add("序号");
            dd.Columns.Add("宿舍");
            dd.Columns.Add("类别");
            dd.Columns.Add("是否处理");
            dd.Columns.Add("报修时间");
            dd.Columns.Add("完工时间");
            dd.Columns.Add("报修人");
            dd.Columns.Add("维修人");
            dd.Columns.Add("备注");
            dd.Columns.Add("楼号");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dd.NewRow();
                    r["序号"] = dt.Rows[i]["row"].ToString();
                    r["宿舍"] = dt.Rows[i]["dorm"].ToString();
                    r["类别"] = dt.Rows[i]["category"].ToString();
                    r["是否处理"] = dt.Rows[i]["handle"].ToString();
                    r["报修时间"] = dt.Rows[i]["s_time"].ToString();
                    r["完工时间"] = dt.Rows[i]["e_time"].ToString();
                    r["报修人"] = dt.Rows[i]["rname"].ToString();
                    r["维修人"] = dt.Rows[i]["wname"].ToString();
                    r["备注"] = dt.Rows[i]["remark"].ToString();
                    r["楼号"] = dt.Rows[i]["bid"].ToString();
                    dd.Rows.Add(r);
                }
            }
            return dd;

        }
        public bool deleteRepair(string id)
        {
            return _repairDAL.deleteRepair(id);
        }

        public bool modifyRepair(RepairModel repair)
        {
            return _repairDAL.modifyRepair(repair);
        }

        public bool addRepair(RepairModel repair)
        {
            return _repairDAL.addRepair(repair);
        }

        public DataTable Category()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("category");

            DataRow r = dt.NewRow();
            r[0] = "水电";
            dt.Rows.Add(r);

            DataRow r1 = dt.NewRow();
            r1[0] = "木工";
            dt.Rows.Add(r1);

            DataRow r2 = dt.NewRow();
            r2[0] = "空调";
            dt.Rows.Add(r2);

            return dt;
        }
    }
}
