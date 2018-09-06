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
    public class OfficeBLL
    {
        private OfficeDAL _officDAL = new OfficeDAL();
        private OfficeModel _officeMode = new OfficeModel();

        public int GetDataCount(string duty,string number,string bid)
        {
            return _officDAL.GetDataCount(duty, number,bid);
        }

        public DataTable GetDataTable(string starIndex, string endIndex,string name ,string duty,string bid)
        {
            return _officDAL.GetDataTable(starIndex, endIndex,name, duty,bid);
        }
        public DataTable OfficeDownData(string starIndex,string endIndex,string nam,string duty,string bid)
        {
            DataTable dt = new DataTable();
            dt = _officDAL.GetDataTable(starIndex, endIndex, nam, duty, bid);
            DataTable dd = new DataTable();
            dd.Columns.Add("序号");
            dd.Columns.Add("编号");
            dd.Columns.Add("工作");
            dd.Columns.Add("姓名");
            dd.Columns.Add("性别");
            dd.Columns.Add("年龄");
            dd.Columns.Add("是否在岗");
            dd.Columns.Add("工作区域");
            dd.Columns.Add("联系电话");
            dd.Columns.Add("住址");
            dd.Columns.Add("备注");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dd.NewRow();
                    r["序号"] = dt.Rows[i]["row"].ToString();
                    r["编号"] = dt.Rows[i]["onumber"].ToString();
                    r["工作"] = dt.Rows[i]["job"].ToString();
                    r["姓名"] = dt.Rows[i]["oname"].ToString();
                    r["性别"] = dt.Rows[i]["osex"].ToString();
                    r["年龄"] = dt.Rows[i]["age"].ToString();
                    r["是否在岗"] = dt.Rows[i]["duty"].ToString();
                    r["工作区域"] = dt.Rows[i]["bid"].ToString();
                    r["联系电话"] = dt.Rows[i]["tel"].ToString();
                    r["住址"] = dt.Rows[i]["oarea"].ToString();
                    r["备注"] = dt.Rows[i]["remark1"].ToString();
                    dd.Rows.Add(r);
                }
            }
            return dd;
        }

        public DataTable GetOneData(string number, string duty)
        {
            return _officDAL.GetOneData(number,duty);
        }

        public bool addManger(OfficeModel officeModel)
        {
            return _officDAL.addManger(officeModel);
        }

        public bool updateManager(OfficeModel offic)
        {
            return _officDAL.updateManager(offic);
        }

        public DataTable Select_number(string number, string duty)
        {
            return _officDAL.Select_number(number, duty);
        }

        public DataTable select_Build()
        {
            return _officDAL.select_Build();
        }

        public bool insertBuild(string bid,string name,string area,string remark1,string remark2)
        {
            return _officDAL.insertBuild(bid,name, area, remark1, remark2);
        }

        public bool modiftBuild(string bid,string name, string area, string remark1, string remark2)
        {
            return _officDAL.modiftBuild(bid,name, area, remark1, remark2);
        }
    }
}
