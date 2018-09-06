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
    public class disciplineDAL
    {

        public int GetDataCount(string s_time,string e_time,string bid)
        {
            string dtime = DateTime.Now.ToString("yyyy-MM-dd");//当前时间
            if (e_time == "")//e_time是不可能为空了
            {
                e_time = dtime;
            }
            SqlParameter[] para = { 
                                  new SqlParameter("@s_time",s_time),
                                  new SqlParameter("@e_time",e_time),
                                  new SqlParameter("@bid",bid),
                                  };
            
            string sqlstr = "";
            if (s_time != "")
            {
                if (e_time != "" && bid != "")//三个都不为空
                {
                    sqlstr = @"select count(*) from Discipline e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from Discipline) as l
                                where l.d=e.id and l.bid=@bid and e.dtime between @s_time and @e_time";
                }
                else if (e_time != "" && bid == "")//bid is null
                {
                    sqlstr = @"select count(*) from Discipline e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from Discipline) as l
                                where l.d=e.id and e.dtime between @s_time and @e_time";
                }
            }
            else if (s_time == "" )
            {
                if (bid != "")
                {
                    sqlstr = @"select count(*) from Discipline e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from Discipline) as l
                                where l.d=e.id and l.bid=@bid and e.dtime <  @e_time";
                }
                else if(bid=="")
                {
                    sqlstr = @"select count(*) from Discipline e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from Discipline) as l
                                where l.d=e.id and e.dtime < @e_time";
                }
            }
      
            DataTable dt = SqlDB.FillDt(sqlstr,para);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public DataTable GetDataTable(string starIndex,string endIndex,string s_time,string e_time,string bid)
        {
            string dtime = DateTime.Now.ToString("yyyy-MM-dd");
            if (e_time == "")
            {
                e_time = dtime;
            }
            SqlParameter[] para = { 
                                  new SqlParameter("@s_time",s_time),
                                  new SqlParameter("@e_time",e_time),
                                  new SqlParameter("@bid",bid),
                                  new SqlParameter("@starIndex",starIndex),
                                  new SqlParameter("@endIndex",endIndex)
                                  };
            string sqlstr = "";
            if (s_time != "")
            {
                if (bid != "" && starIndex!="" && endIndex!="")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from discipline e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from discipline)l where l.bid=@bid and l.d=e.id and e.dtime between @s_time and @e_time)d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid != "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from discipline e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from discipline)l where l.bid=@bid and l.d=e.id and e.dtime between @s_time and @e_time)d
                                    )dd";
                }
                else if (bid == "" && starIndex != "" && endIndex != "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from discipline e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from discipline)l where l.d=e.id and e.dtime between @s_time and @e_time)d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid == "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from discipline e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from discipline)l where l.d=e.id and e.dtime between @s_time and @e_time)d
                                    )dd";
                }
            }
            else if (s_time == "")
            {
                if (bid != "" && starIndex != "" && endIndex != "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from discipline e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from discipline)l where l.bid=@bid and l.d=e.id and e.dtime< @e_time)d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid != "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from discipline e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from discipline)l where l.bid=@bid and l.d=e.id and e.dtime< @e_time)d
                                    )dd";
                }
                else if (bid == "" && starIndex != "" && endIndex != "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from discipline e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from discipline)l where l.d=e.id and e.dtime< @e_time)d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid == "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from discipline e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from discipline)l where l.d=e.id and e.dtime< @e_time)d
                                    )dd";
                }
            }

//            
            return SqlDB.FillDt(sqlstr,para);
        }

        public bool modifyDiscipline(disciplineModel discipline)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@id",discipline.id),
                                  new SqlParameter("@dname",discipline.dname),
                                  new SqlParameter("@dorm",discipline.dorm),
                                  new SqlParameter("@reason",discipline.reason),
                                  new SqlParameter("@punish",discipline.punish),
                                  new SqlParameter("@leader",discipline.leader),
                                  new SqlParameter("@dtime",discipline.dtime),
                                  new SqlParameter("@remark",discipline.remark),
                                  new SqlParameter("@sno",discipline.sno)
                                  };
            string sqlstr = "update discipline set dname=@dname,dorm=@dorm,reason=@reason,punish=@punish,leader=@leader,dtime=@dtime,remark=@remark,sno=@sno where id=@id";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
            {
                return true;
            }
            else
                return false;
        
        }

        public bool deleteDiscipline(string id)
        {
            SqlParameter[] para = {new SqlParameter("@id",id) };
            string sqlstr = "delete discipline where id=@id";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
                return true;
            else return false;
        }

        public bool addDisicipline(disciplineModel discipline)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@dname",discipline.dname),
                                  new SqlParameter("@dorm",discipline.dorm),
                                  new SqlParameter("@reason",discipline.reason),
                                  new SqlParameter("@punish",discipline.punish),
                                  new SqlParameter("@leader",discipline.leader),
                                  new SqlParameter("@dtime",discipline.dtime),
                                  new SqlParameter("@remark",discipline.remark),
                                  new SqlParameter("@sno",discipline.sno)
                                  };
            string sqlstr = "insert into discipline(dname,dorm,reason,punish,leader,dtime,remark,sno) values (@dname,@dorm,@reason,@punish,@leader,@dtime,@remark,@sno)";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
