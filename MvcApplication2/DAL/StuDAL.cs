using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StuDAL
    {

        public DataTable selectStuAll(string starIndex,string endIndex,string schoool)
        {
            //String str = "select * from Student where school ='1'";
            SqlParameter[] paras = { 
                                   new SqlParameter("@starIndex",starIndex),
                                   new SqlParameter("@endIndex",endIndex),
                                   new SqlParameter("@school",schoool),
                                   };
            string sql = "";
            if (starIndex != "" && endIndex != "")
            {
                sql = @"select * from (
                                select ROW_NUMBER() over(order by d.sno asc) as row ,d.* from Student d where d.school=@school) dd
                                where dd.row between @starIndex and @endIndex";
            }
            else
                sql = "select * from (select ROW_NUMBER() over(order by d.sno asc) as row ,d.* from Student d) dd";
            return SqlDB.FillDt(sql,paras);
        }

        public string pageAll(string school)
        {
            string str = "select distinct COUNT(*) total from Student where school=@school";
            SqlParameter[] para = {new SqlParameter("@school",school) };
            DataTable dt= SqlDB.FillDt(str,para);
            string res=dt.Rows[0][0].ToString();
            return res;
        }


        public DataTable select_Sno(string sno,string school)
        {
            string sqlstr = "select * from Student where sno=@sno and school=@school";
            SqlParameter[] para = { 
                                  new SqlParameter("@sno",sno),
                                  new SqlParameter("@school",school)
                                  };
            return SqlDB.FillDt(sqlstr, para);
        }

        public bool ModifyStu(string sno,string sname,string ssex,string sdept ,string dorm ,string s_time,string e_time,string m_time)
        {
            SqlParameter[] para = { 
                                    new SqlParameter("@sname",sname),
                                    new SqlParameter("@ssex",ssex),

                                  new SqlParameter("@sdept",sdept),
                                  new SqlParameter("@dorm",dorm),
                                  new SqlParameter("@s_time",s_time),
                                  new SqlParameter("@e_time",e_time),
                                  new SqlParameter("@m_time",m_time),
                                  new SqlParameter("@sno",sno)
                                  };
            string sqlstr = "update Student set sname=@sname,ssex=@ssex, sdept=@sdept,dorm=@dorm,s_time=@s_time,e_time=@e_time,m_time=@m_time where sno=@sno";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0) return true;
            else return false;
        }


        public bool AddStu(string sno, string sname, string ssex, string sdept, string dorm, string s_time, string e_time, string m_time,string school)
        {
            int i;
            SqlParameter[] paras ={
                                  new SqlParameter("@sno",sno),
                                  new SqlParameter("@sname",sname),
                                  new SqlParameter("@ssex",ssex),
                                  new SqlParameter("@sdept",sdept),
                                  new SqlParameter("@dorm",dorm),
                                  new SqlParameter("@s_time",s_time),
                                  new SqlParameter("@e_time",e_time),
                                  new SqlParameter("@m_time",m_time),
                                  new SqlParameter("@school",school)
                                  };
            string sqlstr = "insert into Student(sno,sname,ssex,sdept,dorm,school,s_time,e_time,m_time) values (@sno,@sname,@ssex,@sdept,@dorm,@school,@s_time,@e_time,@m_time)";
            try
            {
               i = SqlDB.executeSql(sqlstr, paras);
            }
            catch (Exception ex)
            {
                return false;
            }
            if (i > 0)
                return true;
            else return false;
        }


        //TryStu
        public DataTable selectDept()
        {
            string sql = "select distinct sdept from Student where sdept is not null";
            return SqlDB.FillDt(sql);
        }

        public string GetDataCount(string grade,string dept,string sno,string sch)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@grade",grade),
                                  new SqlParameter("@dept",dept),
                                  new SqlParameter("@sno",sno),
                                  new SqlParameter("@sch",sch),
                                  };
            string sql = "";
            //if (sch == "") sch = "1";//当 是否在校 为空时 默认一个值  学生在校
            if (sno != "")//查找时 学号不为空，精确查找
            {
                sql = "select count(*) from student where sno=@sno";
            }
            else//模糊查找
            {
                //在controller里面默认了 系别，年级，不可能为null了
                //if (grade == "" && dept == "")//当程序刚开始加载时  下拉框里面的东西都为空
                //{
                //    sql = "select distinct * from student where school=@sch";
                //}
                sql = "select distinct COUNT(*) from Student where school=@sch and sdept=@dept and sno like '" + @grade + "%'";
            }
            string i = SqlDB.FillDt(sql, para).Rows[0][0].ToString();
            return i;
        }

        public DataTable GetDataTable(string starIndex, string endIndex, string grade, string dept, string sno, string sch)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@starIndex",starIndex),
                                  new SqlParameter("@endIndex",endIndex),
                                  new SqlParameter("@grade",grade),
                                  new SqlParameter("@dept",dept),
                                  new SqlParameter("@sno",sno),
                                  new SqlParameter("@sch",sch),
                                  };
            string sql = "";
            if (sno != "")//精确查找
            {
                sql = "select * from student where sno=@sno";
            }
            else
            {
                if (starIndex == "" && endIndex == "")//导出数据需要 分页为空
                {
                    sql = @"select * from (
        select ROW_NUMBER() over(order by d.sno asc) as row,d.* from Student d where d.school=@sch and d.sdept=@dept and d.sno like '%"+@grade+"%')dd";
                }
                else
                {
                    sql = @"select * from (
        select ROW_NUMBER() over(order by d.sno asc) as row,d.* from Student d where d.school=@sch and d.sdept=@dept and d.sno like '%" + @grade + "%')dd where dd.row between @starIndex and @endIndex";
                }
            }
            return SqlDB.FillDt(sql, para);
        }

        public DataTable GetUserInfo(string name)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@name",name),
                                  };
            string sql = "select * from student where sno=@name";
            return SqlDB.FillDt(sql, para);
        }
    }
}
