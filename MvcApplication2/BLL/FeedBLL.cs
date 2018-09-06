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
    public class FeedBLL
    {
        FeedDAL _feedDAL = new FeedDAL();

        public int GetDataCount(string s_time,string e_time)
        {
            DataTable dt = new DataTable();
            dt = _feedDAL.GetDataCount(s_time,e_time);
            int count = int.Parse(dt.Rows[0][0].ToString());
            return count;
        }

        public DataTable GetDataTable(string starIndex,string endIndex,string s_time,string e_time)
        {
            return _feedDAL.GetDataTable(starIndex,endIndex,s_time,e_time);
        }
        public DataTable FeedDownLoad(string starIndex, string endIndex, string s_time, string e_time)
        {
            DataTable dt = new DataTable();
            dt = _feedDAL.GetDataTable("", "", s_time, e_time);
            DataTable dd = new DataTable();
            dd.Columns.Add("序号");
            dd.Columns.Add("上报时间");
            dd.Columns.Add("描述");
            dd.Columns.Add("回复");
            dd.Columns.Add("反馈时间");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dd.NewRow();
                    r["序号"] = dt.Rows[i]["row"].ToString();
                    r["上报时间"] = dt.Rows[i]["time"].ToString();
                    r["描述"] = dt.Rows[i]["remark"].ToString();
                    r["回复"] = dt.Rows[i]["remark2"].ToString();
                    r["反馈时间"] = dt.Rows[i]["remark3"].ToString();
                    dd.Rows.Add(r);
                }
            }
            return dd;
        }

        public bool deletFeed(string id)
        {
            return _feedDAL.deleteFeed(id);
        }

        public bool modifyFeed(FeedModel feed)
        {
            return _feedDAL.modifyFeed(feed);
        }
    }
}
