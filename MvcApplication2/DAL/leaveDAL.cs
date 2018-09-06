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
    public class leaveDAL
    {
        public int GetDataCount(string s_date,string e_date,string bid)
        {
            string dtime = DateTime.Now.ToString("yyyy-MM-dd");
            if (e_date == "")
            {
                e_date = dtime;//默认搜索的结束时间为 当前时间
            }
            SqlParameter[] para = { 
                                  new SqlParameter("@s_date",s_date),
                                  new SqlParameter("@e_date",e_date),
                                  new SqlParameter("@bid",bid)
                                  };
            string sqlstr = "";
            if (s_date != "")
            {
                if (bid != "")
                {
                    sqlstr = @"select count(*) from leave e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from leave) as l
                                where l.d=e.id and l.bid=@bid and e.s_time between @s_date and @e_date";
                }
                else
                {
                    sqlstr = @"select count(*) from leave e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from leave) as l
                                where l.d=e.id and e.s_time between @s_date and @e_date";
                }
            }
            else if (s_date == "")
            {
                if (bid != "")
                {
                    sqlstr = @"select count(*) from leave e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from leave) as l
                                where l.d=e.id and l.bid=@bid and e.s_time < @e_date";
                }
                else
                {
                    sqlstr = @"select count(*) from leave e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from leave) as l
                                where l.d=e.id and e.s_time < @e_date";
                }
            }

//            if (s_date != "")
//            {
//                if (e_date != "")
//                {
//                    sqlstr = @"select count(*) from leave e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from leave) as l
//                                where l.d=e.id and l.bid=@bid and e.s_time between @s_date and @e_date";
//                }
//                else if (e_date == "")
//                {
//                    sqlstr = @"select count（*） from leave e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)as l
//                                where l.d=e.id and l.bid=@bid and e.s_time =@s_date";
//                }
//            }
//            else if(s_date=="" && bid!="")
//            {
//                sqlstr = @"select count(*) from leave e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)as l
//                                where l.d=e.id and l.bid=@bid";
//            }

//            else if (bid == "")
//            {
//                sqlstr = @"select count(*) from leave e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave) as l where l.d=e.id";
//            }
            

            DataTable dt = new DataTable();
            dt = SqlDB.FillDt(sqlstr,para);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public DataTable GetDataTable(string starIndex,string endIndex,string s_date,string e_date,string bid)
        {
            if (e_date == "")
            {
                e_date = DateTime.Now.ToString("yyyy-MM-dd");
            }
            SqlParameter[] para = { 
                                      new SqlParameter("@starIndex",starIndex),
                                      new SqlParameter("@endIndex",endIndex),
                                  new SqlParameter("@s_date",s_date),
                                  new SqlParameter("@e_date",e_date),
                                  new SqlParameter("@bid",bid)
                                  };
            string sqlstr = "";
            if (s_date != "")
            {
                if (bid != "" && starIndex!="" && endIndex!="")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.bid=@bid and l.d=e.id and e.s_time between @s_date and @e_date)d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid != "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.bid=@bid and l.d=e.id and e.s_time between @s_date and @e_date)d
                                    )dd";
                }
                else if (bid == "" && starIndex != "" && endIndex != "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.d=e.id and e.s_time between @s_date and @e_date)d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid == "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.d=e.id and e.s_time between @s_date and @e_date)d
                                    )dd";
                }
                
            }
            else if (s_date == "")
            {
                if (bid != "" && starIndex != "" && endIndex != "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.bid=@bid and l.d=e.id and e.s_time < @e_date)d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid != "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.bid=@bid and l.d=e.id and e.s_time < @e_date)d
                                    )dd";
                }
                else if (bid == "" && starIndex != "" && endIndex != "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.d=e.id and e.s_time < @e_date)d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid == "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.d=e.id and e.s_time < @e_date)d
                                    )dd";
                }
            }
//            if (s_date != "")
//            {
//                if (e_date != "")
//                {
//                    //sqlstr = @"select * from leave e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from leave) as l
//                    //            where l.d=e.id and l.bid=@bid and e.s_time between @s_date and @e_date";
//                    sqlstr = @"select * from(
//                                   select row_number() over(order by d.id)as row,d.* from(
//                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.bid=@bid and l.d=e.id and e.s_time between @s_date and @e_date)d
//                                    )dd where dd.row between @starIndex and @endIndex";
//                }
//                else if (e_date == "")
//                {//只有一个日期
//                    //sqlstr = @"select * from leave e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from leave) as l
//                   //                 where l.d=e.id and l.bid=@bid and e.s_time =@s_date";
//                    sqlstr = @"select * from(
//                                   select row_number() over(order by d.id)as row,d.* from(
//                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.bid=@bid and l.d=e.id and e.s_time=@s_date)d
//                                    )dd where dd.row between @starIndex and @endIndex";
//                }
//            }
//            else if(s_date==""&& bid !="")
//            {//没有日期，楼号
////                sqlstr = @"select * from leave e,(select distinct id ad d,substring(dorm,1,charindex('#',dorm)-1) as bid from leave )as l
////                            where l.d=e.id and l.bid=@bid";
//                sqlstr = @"select * from(
//                                   select row_number() over(order by d.id)as row,d.* from(
//                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.bid=@bid and l.d=e.id)d
//                                    )dd where dd.row between @starIndex and @endIndex";
//            }
//            else if (bid == "")
//            {//一开始是没有楼号的
//               // sqlstr = @"select * from leave e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave) as l where l.d=e.id";
//                sqlstr = @"select * from(
//                                   select row_number() over(order by d.id)as row,d.* from(
//                                            select * from leave e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from leave)l where l.d=e.id)d
//                                    )dd where dd.row between @starIndex and @endIndex";
//            }
            
            DataTable dt = SqlDB.FillDt(sqlstr,para);
            return dt;
        }

        public DataTable GetDownList()
        {
            string sqlstr = "select bid from building";
            return SqlDB.FillDt(sqlstr);
        }

        public bool modifyLeave(LeaveModel _leave)
        {
            SqlParameter[] paras = { 
                                   new SqlParameter("@lname",_leave.lname),
                                   new SqlParameter("@iname",_leave.iname),
                                   new SqlParameter("@dorm",_leave.dorm),
                                   new SqlParameter("@reason",_leave.reason),
                                   new SqlParameter("@leader",_leave.leader),
                                   new SqlParameter("@s_time",_leave.s_time),
                                   new SqlParameter("@e_time",_leave.e_time),
                                   new SqlParameter("@tel",_leave.tel),
                                   new SqlParameter("@remark",_leave.remark),
                                   new SqlParameter("@id",_leave.id)
                                   };
            string sqlstr = "update leave set lname=@lname,iname=@iname,dorm=@dorm,reason=@reason,leader=@leader,s_time=@s_time,e_time=@e_time,tel=@tel,remark=@remark where id=@id";
            int i = SqlDB.executeSql(sqlstr, paras);
            if (i > 0)
                return true;
            else
                return false;
        }

        public bool deleteLeave(string id)
        {
            SqlParameter[] para = { new SqlParameter("@id",id)};
            string sqlstr = "delete leave where id=@id";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
                return true;
            else
                return false;
        }

        public bool addLeave(LeaveModel leave)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@lname",leave.lname),
                                  new SqlParameter("@iname",leave.iname),
                                  new SqlParameter("@dorm",leave.dorm),
                                  new SqlParameter("@reason",leave.reason),
                                  new SqlParameter("@leader",leave.leader),
                                  new SqlParameter("@s_time",leave.s_time),
                                  new SqlParameter("@e_time",leave.e_time),
                                  new SqlParameter("@remark",leave.remark),
                                  };
            string sqlstr = "insert into leave(lname,iname,dorm,reason,leader,s_time,e_time,remark) values (@lname,@iname,@dorm,@reason,@leader,@s_time,@e_time,@remark)";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
                return true;
            else return false;
        }
    }
}
