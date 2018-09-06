using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FeedDAL
    {
        public DataTable GetDataCount(string s_time,string e_time)
        {
            string dtime = DateTime.Now.ToString("yyyy-MM-dd");
            if (e_time == "")
                e_time = dtime;
            SqlParameter[] para = { 
                                  new SqlParameter("@s_time",s_time),
                                  new SqlParameter("@e_time",e_time)
                                  };
            string sql = "";
            if (s_time == "")
            {
                sql = "select distinct count(*) from feedback where time<@e_time";
            }
            else
            {
                sql = "select distinct count(*) from feedback where time between @s_time and @e_time";
            }
            return SqlDB.FillDt(sql,para);
        }

        public DataTable GetDataTable(string starIndex,string endIndex,string s_time,string e_time)
        {
            string dtime = DateTime.Now.ToString("yyyy-MM-dd");
            if (e_time == "")
                e_time = dtime;
            SqlParameter[] para = {
                                  new SqlParameter("@starIndex",starIndex),
                                  new SqlParameter("@endIndex",endIndex),
                                  new SqlParameter("@s_time",s_time),
                                  new SqlParameter("@e_time",e_time)
                                  };
            string sql = "";
            if (s_time == "" && starIndex != "" && endIndex != "")
            {
                sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from Feedback d where time <@e_time ) dd
where dd.row between @starIndex and @endIndex";
            }
            else if (s_time == "" && starIndex == "" && endIndex == "")
            {
                sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from Feedback d where time <@e_time ) dd";
            }
            else if (s_time != "" && starIndex != "" && endIndex != "")
            {
                sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from Feedback d where time between @s_time and @e_time) dd
where dd.row between @starIndex and @endIndex";
            }
            else if (s_time != "" && starIndex == "" && endIndex == "")
            {
                sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from Feedback d where time between @s_time and @e_time) dd";
            }
            return SqlDB.FillDt(sql,para);
        }

        public bool deleteFeed(string id)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@id",id),
                                  };
            string sql = "delete feedback where id=@id";
            int i = SqlDB.executeSql(sql, para);
            if (i > 0)
            {
                return true;
            }
            else return false;
        }

        public bool modifyFeed(FeedModel feed)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@id",feed.id),
                                  new SqlParameter("@time",feed.time),
                                  new SqlParameter("@remark",feed.remark),
                                  new SqlParameter("@remark2",feed.remark2),
                                  new SqlParameter("@remark3",feed.remark3),
                                  };
            string sql = "update feedback set time=@time,remark=@remark,remark2=@remark2,remark3=@remark3 where id=@id";
            int i = SqlDB.executeSql(sql, para);
            if (i > 0)
                return true;
            else return false;
        }
    }
}
