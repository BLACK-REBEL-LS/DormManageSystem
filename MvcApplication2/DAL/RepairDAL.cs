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
    public class RepairDAL
    {
        public int GetDataCount(string s_time,string e_time,string bid,string caregory,string handle)
        {
            string dtime = DateTime.Now.ToString("yyyy-MM-dd");
            if (e_time == "") e_time = dtime;
            SqlParameter[] para = { 
                                  new SqlParameter("@s_time",s_time),
                                  new SqlParameter("@e_time",e_time),
                                  new SqlParameter("@bid",bid),
                                  new SqlParameter("@caregory",caregory),
                                  new SqlParameter("@handle",handle),
                                  };
            string sqlstr = "";
            if (s_time != "")
            {
                if (bid != "" && caregory!="" && handle!="")
                {
                    sqlstr = @"select count(*) from Repair e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from Repair) as l
                                where l.d=e.id and l.bid=@bid and e.s_time between @s_time and @e_time and category=@caregory and handle=@handle";
                }
                else if(bid=="" && caregory!="" && handle!="")
                {
                    sqlstr = @"select count(*) from Repair e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from Repair) as l
                                where l.d=e.id and e.s_time between @s_time and @e_time and category=@caregory and handle=@handle";
                }
            }
            else if (s_time == "")
            {
                if (bid != "" && caregory != "" && handle != "")
                {
                    sqlstr = @"select count(*) from Repair e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from Repair) as l
                                where l.d=e.id and l.bid=@bid and e.s_time < @e_time and category=@caregory and handle=@handle";
                }
                else if (bid == "" && caregory != "" && handle != "")
                {
                    sqlstr = @"select count(*) from Repair e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from Repair) as l
                                where l.d=e.id and e.s_time < @e_time and category=@caregory and handle=@handle";
                }

                else if (bid == "" && caregory == "" && handle == "")
                {
                    sqlstr = @"select count(*) from Repair e,(select distinct id as d,substring(dorm,1,charindex('#',dorm)-1) as bid from Repair) as l
                                where l.d=e.id and e.s_time < @e_time";
                }
            }
            DataTable dt = SqlDB.FillDt(sqlstr,para);
            int t = int.Parse(dt.Rows[0][0].ToString());
            return t;
        }

        public DataTable GetDataTable(string starIndex,string endIndex,string s_time,string e_time,string bid,string caregory,string handle)
        {
            string dtime = DateTime.Now.ToString("yyyy-MM-dd");
            if (e_time == "") e_time = dtime;
            SqlParameter[] para = { 
                                      new SqlParameter("@starIndex",starIndex),
                                      new SqlParameter("@endIndex",endIndex),
                                  new SqlParameter("@s_time",s_time),
                                  new SqlParameter("@e_time",e_time),
                                  new SqlParameter("@bid",bid),
                                  new SqlParameter("@caregory",caregory),
                                  new SqlParameter("@handle",handle),
                                  };
            string sqlstr = "";
            if (s_time != "")
            {
                if (bid != "" && caregory != "" && handle != "" && starIndex!="" && endIndex!="")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from Repair e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from Repair)l where l.d=e.id and l.bid=@bid and e.category=@caregory and e.handle=@handle and e.s_time between @s_time and @e_time )d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid != "" && caregory != "" && handle != "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from Repair e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from Repair)l where l.d=e.id and l.bid=@bid and e.category=@caregory and e.handle=@handle and e.s_time between @s_time and @e_time )d
                                    )dd";
                }
                else if (bid == "" && caregory != "" && handle != "" && starIndex != "" && endIndex != "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from Repair e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from Repair)l where l.d=e.id and e.category=@caregory and e.handle=@handle and e.s_time between @s_time and @e_time )d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid == "" && caregory != "" && handle != "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from Repair e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from Repair)l where l.d=e.id and e.category=@caregory and e.handle=@handle and e.s_time between @s_time and @e_time )d
                                    )dd";
                }
            }
            else if (s_time == "")
            {
                if (bid != "" && caregory != "" && handle != "" && starIndex != "" && endIndex != "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from Repair e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from Repair)l where l.d=e.id and l.bid=@bid and e.category=@caregory and e.handle=@handle and e.s_time < @e_time )d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid != "" && caregory != "" && handle != "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from Repair e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from Repair)l where l.d=e.id and l.bid=@bid and e.category=@caregory and e.handle=@handle and e.s_time < @e_time )d
                                    )dd";
                }
                else if (bid == "" && caregory != "" && handle != "" && starIndex != "" && endIndex != "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from Repair e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from Repair)l where l.d=e.id and e.category=@caregory and e.handle=@handle and e.s_time < @e_time )d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid == "" && caregory != "" && handle != "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from Repair e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from Repair)l where l.d=e.id and e.category=@caregory and e.handle=@handle and e.s_time < @e_time )d
                                    )dd";
                }
                else if (bid == "" && caregory == "" && handle == "" && starIndex != "" && endIndex != "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from Repair e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from Repair)l where l.d=e.id and e.s_time < @e_time )d
                                    )dd where dd.row between @starIndex and @endIndex";
                }
                else if (bid == "" && caregory == "" && handle == "" && starIndex == "" && endIndex == "")
                {
                    sqlstr = @"select * from(
                                   select row_number() over(order by d.id)as row,d.* from(
                                            select * from Repair e,(select id d,substring(dorm,1,charindex('#',dorm)-1)as bid from Repair)l where l.d=e.id and e.s_time < @e_time )d
                                    )dd";
                }
            }
            return SqlDB.FillDt(sqlstr,para);
        }

        public bool deleteRepair(string id)
        {
            SqlParameter[] para = { new SqlParameter("@id",id)};
            string sqlstr = "delete repair where id=@id";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
                return true;
            else return false;
        }

        public bool modifyRepair(RepairModel repair)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@id",repair.id),
                                  new SqlParameter("@dorm",repair.dorm),
                                  new SqlParameter("@category",repair.category),
                                  new SqlParameter("@handle",repair.handle),
                                  new SqlParameter("@s_time",repair.s_time),
                                  new SqlParameter("@e_time",repair.e_time),
                                  new SqlParameter("@rname",repair.rname),
                                  new SqlParameter("@wname",repair.wname),
                                  new SqlParameter("@remark",repair.remark),
                                  new SqlParameter("@sno",repair.sno)
                                  };
            string sqlstr = "update repair set dorm=@dorm,category=@category,handle=@handle,s_time=@s_time,e_time=@e_time,rname=@rname,wname=@wname,remark=@remark,sno=@sno where id=@id";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
                return true;
            else return false;
        }

        public bool addRepair(RepairModel repair)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@dorm",repair.dorm),
                                  new SqlParameter("@category",repair.category),
                                  new SqlParameter("@handle",repair.handle),
                                  new SqlParameter("@s_time",repair.s_time),
                                  new SqlParameter("@e_time",repair.e_time),
                                  new SqlParameter("@rname",repair.rname),
                                  new SqlParameter("@wname",repair.wname),
                                  new SqlParameter("@remark",repair.remark),
                                  new SqlParameter("@sno",repair.sno)
                                  };
            string sqlstr = "insert into repair(dorm,category,handle,s_time,e_time,rname,wname,sno,remark) values (@dorm,@category,@handle,@s_time,@e_time,@rname,@wname,@sno,@remark)";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
                return true;
            else
                return false;
        }
    }
}
