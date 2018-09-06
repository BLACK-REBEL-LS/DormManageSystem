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
    public class SchoolDAL
    {
        private string startime, endtime;

        public int GetDataCount(string star,string end)
        {
            string sql = "";
            if (star == "" && end == "")//页面刚开始加载的时候
            {
                //默认一个加载
                int e = int.Parse(DateTime.Now.Year.ToString());
                star = (e - 1) + "-" + "12" + "-" + "01";
                end = e + "-" + "03" + "-" + "01";
            }
            else
            {
                datetime(star, end);
                star = startime;
                end = endtime;
            }
            SqlParameter[] para = {
                                  new SqlParameter("@star",star),
                                  new SqlParameter("@end",end),
                                  };

            sql = "select count(*) from school where s_time>@star and e_time<@end";
            DataTable dt = new DataTable();
            dt = SqlDB.FillDt(sql,para);
            return int.Parse(dt.Rows[0][0].ToString());
        }
        public DataTable GetDataTable(string starIndex,string endIndex,string schyear,string vaction)
        {
            string sqlstr = "";
                if (schyear == "" && vaction == "")//默认一个时间段
                {
                    int e = int.Parse(DateTime.Now.Year.ToString());
                    schyear = (e - 1) + "-" + "12" + "-" + "01";
                    vaction = e + "-" + "03" + "-" + "01";
                }
                else
                {
                    datetime(schyear, vaction);
                    schyear = startime;
                    vaction = endtime;
                }
                SqlParameter[] para = { 
                                      new SqlParameter("@starIndex",starIndex),
                                      new SqlParameter("@endIndex",endIndex),
                                  new SqlParameter("@schyear",schyear),
                                  new SqlParameter("@vacation",vaction),
                                  };
                if (starIndex != "" && endIndex != "")
                {
                    sqlstr = @"select * from( 
	select ROW_NUMBER() over(order by d.id)as row ,d.* from School d where d.s_time>@schyear and d.e_time<@vacation
)dd where dd.row between @starIndex and @endIndex ";
                }
                else if (starIndex == "" && endIndex == "") 
                {
                    sqlstr = @"select * from( 
	select ROW_NUMBER() over(order by d.id)as row ,d.* from School d where d.s_time>@schyear and d.e_time<@vacation)dd";
                }
            
            return SqlDB.FillDt(sqlstr,para);
        }

        public bool deleteSchool(string id)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@id",id)
                                  };
            string sqlstr = "delete school where id=@id";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
                return true;
            else return false;
        }

        public bool modifySchool(SchoolModel school)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@id",school.id),
                                  new SqlParameter("@name",school.name),
                                  new SqlParameter("@sno",school.sno),
                                  new SqlParameter("@s_time",school.s_time),
                                  new SqlParameter("@e_time",school.e_time),
                                  new SqlParameter("@pdorm",school.pdorm),
                                  new SqlParameter("@odorm",school.odorm),
                                  new SqlParameter("@leader",school.leader),
                                  new SqlParameter("@tel",school.tel),
                                  new SqlParameter("@remark",school.remark),
                                  };
            string sqlstr = "update school set name=@name,sno=@sno,s_time=@s_time,e_time=@e_time,pdorm=@pdorm,odorm=@odorm,leader=@leader,tel=@tel,remark=@remark where id=@id";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
                return true;
            else return false;
        }

        public bool addSchool(SchoolModel school)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@name",school.name),
                                  new SqlParameter("@sno",school.sno),
                                  new SqlParameter("@s_time",school.s_time),
                                  new SqlParameter("@e_time",school.e_time),
                                  new SqlParameter("@pdorm",school.pdorm),
                                  new SqlParameter("@odorm",school.odorm),
                                  new SqlParameter("@leader",school.leader),
                                  new SqlParameter("@tel",school.tel),
                                  new SqlParameter("@remark",school.remark),
                                  };
            string sqlstr = "insert into school(name,sno,s_time,e_time,pdorm,odorm,leader,tel,remark) values (@name,@sno,@s_time,@e_time,@pdorm,@odorm,@leader,@tel,@remark)";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
                return true;
            else return false;
        }

        public void datetime(string schyear, string vacation)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@schyear",schyear)
                                  };
            string sqlstr = "select left(@schyear,4) as starTime,substring(@schyear,charindex('-',@schyear)+1,4) as endTime";
            DataTable time = SqlDB.FillDt(sqlstr, para);
            string starYear = time.Rows[0]["starTime"].ToString();
            string endYear = time.Rows[0]["endTime"].ToString();
            string star = "", end = "";
            if (vacation == "暑假")
            {
                star = endYear +"-"+ "06"+"-"+"01";//2018-06-01
                end = endYear + "-" + "10" + "-" + "01";//2018-10-01
            }
            else if (vacation == "寒假")
            {
                star = starYear + "-" + "12" + "-" + "01";//2017-12-01
                end = endYear + "-" + "03" + "-" + "01";//2018-03-01
            }
            //
            startime = star;
            endtime = end;
        }

        public DataTable stuSchoolTable(string sno)
        {
            SqlParameter[] para = { new SqlParameter("@sno",sno)};
            string sql = "select * from school where sno=@sno";
            return SqlDB.FillDt(sql, para);
        }
    }
}
