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
    public  class LoginDAL
    {

        public DataTable GetOneUser(LoginModel login)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@name",login.name)
                                  };
            string sql = "select * from users where name=@name or remark2=@name";
            DataTable dt= SqlDB.FillDt(sql, para);
            if (dt.Rows.Count < 1)
                return null;
            else return dt;
        }

        public bool selectSno(string sno)
        {
            SqlParameter[] para = { new SqlParameter("@sno",sno)};
            string sql = "select count(1) from student where sno=@sno";
            int i = SqlDB.executeSql(sql, para);
            if (i > 0)
                return true;
            else return false;
        }

        public bool selectSno(string sno, string role)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@sno",sno),
                                  
                                  };
            string sql = "";
            if (role == "admin")
            {
                sql = "select count(1) from office where sno=@sno";
            }
            else if (role == "student")
            {
                sql = "select count(1) from student where sno=@sno";
            }
            int i = SqlDB.executeSql(sql, para);
            if (i > 0)
                return true;
            else return false;
        }

        public bool InsertUser(LoginModel login)
        {
            string role = "student";//注册只能是学生权限
            SqlParameter[] para = { 
                                  new SqlParameter("@name",login.name),
                                  new SqlParameter("@pwd",login.pwd),
                                  new SqlParameter("@tel",login.remark1),
                                  new SqlParameter("@sno",login.remark2),
                                  new SqlParameter("@role",role)
                                  };
            string sql = "insert into users(name,pwd,role,remark1,remark2) values (@name,@pwd,@role,@tel,@sno)";
            int i = SqlDB.executeSql(sql,para);
            if (i > 0)
                return true;
            else return false;
        }

        public DataTable GetStuRepair(string sno,string stime,string etime,string starIndex,string endIndex)
        {
            if (etime == "") etime = DateTime.Now.ToString("yyyy-MM-dd");
            SqlParameter[] para = {
                                  new SqlParameter("@sno",sno),
                                 new SqlParameter("@stime",stime),
                                 new SqlParameter("@etime",etime),
                                 new SqlParameter("@starIndex",starIndex),
                                 new SqlParameter("@endIndex",endIndex)
                                  };
            string sql = "";
            if (starIndex != "" && endIndex != "")
            {
                if (stime == "")
                {
                    sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from Repair d where d.sno=@sno and d.s_time<@etime ) dd
where dd.row between @starIndex and @endIndex";
                }
                else
                {
                    sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from Repair d where d.sno=@sno and d.s_time between @stime and @etime) dd
where dd.row between @starIndex and @endIndex";
                }
            }
            else if (starIndex == "" && endIndex == "")
            {
                if (stime != "")
                {
                    sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from Repair d where d.sno=@sno and d.s_time between @stime and @etime) dd";
                }
                else
                {
                    sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from Repair d where d.sno=@sno and d.s_time < @etime ) dd";
                }
            }
             
            return SqlDB.FillDt(sql, para);
        }

        public int GetDataCount(string sno, string stime,string etime)
        {
            if (etime == "") etime = DateTime.Now.ToString("yyyy-MM-dd");
            SqlParameter[] para = {
                                  new SqlParameter("@sno",sno),
                                  new SqlParameter("@stime",stime),
                                  new SqlParameter("@etime",etime)
                                  };

            string sql = "";
            if (stime == "")
            {
                sql = "select count(*) from repair where sno=@sno and s_time < @etime";
            }
            else
                sql = "select count(*) from repair where sno=@sno and s_time between @stime and @etime";
            DataTable dt = new DataTable();
            dt = SqlDB.FillDt(sql, para);
            int n=int.Parse(dt.Rows[0][0].ToString());
            return n;
        }

        public int GetDisciplineCount(string sno, string stime, string etime)
        {
            if (etime == "") etime = DateTime.Now.ToString("yyyy-MM-dd");
            SqlParameter[] para = {
                                  new SqlParameter("@sno",sno),
                                  new SqlParameter("@stime",stime),
                                  new SqlParameter("@etime",etime)
                                  };

            string sql = "";
            if (stime == "")
            {
                sql = "select count(*) from discipline where sno=@sno and dtime < @etime";
            }
            else
                sql = "select count(*) from discipline where sno=@sno and dtime between @stime and @etime";
            DataTable dt = new DataTable();
            dt = SqlDB.FillDt(sql, para);
            int n = int.Parse(dt.Rows[0][0].ToString());
            return n;
        }

        public DataTable GetDisciplineTable(string sno, string stime, string etime, string starIndex, string endIndex)
        {
            if (etime == "") etime = DateTime.Now.ToString("yyyy-MM-dd");
            SqlParameter[] para = {
                                  new SqlParameter("@sno",sno),
                                 new SqlParameter("@stime",stime),
                                 new SqlParameter("@etime",etime),
                                 new SqlParameter("@starIndex",starIndex),
                                 new SqlParameter("@endIndex",endIndex)
                                  };
            string sql = "";
            if (starIndex != "" && endIndex != "")
            {
                if (stime == "")
                {
                    sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from discipline d where d.sno=@sno and d.dtime<@etime ) dd
where dd.row between @starIndex and @endIndex";
                }
                else
                {
                    sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from discipline d where d.sno=@sno and d.dtime between @stime and @etime) dd
where dd.row between @starIndex and @endIndex";
                }
            }
            else if (starIndex == "" && endIndex == "")
            {
                if (stime != "")
                {
                    sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from discipline d where d.sno=@sno and d.dtime between @stime and @etime) dd";
                }
                else
                {
                    sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from discipline d where d.sno=@sno and d.dtime < @etime ) dd";
                }
            }

            return SqlDB.FillDt(sql, para);
        }

        public DataTable GetLoingInfo(string sno)
        {
            SqlParameter[] para = {new SqlParameter("@sno",sno) };
            string sql = "select * from users where remark2=@sno";
            return SqlDB.FillDt(sql, para);
        }

        public bool modifyLogin(string sno, LoginModel login)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@sno",sno),
                                  new SqlParameter("@name",login.name),
                                  new SqlParameter("@pwd",login.pwd),
                                  new SqlParameter("@tel",login.remark1),
                                  new SqlParameter("@remark",login.remark3),
                                  };
            string sql = "update users set name=@name,pwd=@pwd,remark1=@tel,remark3=@remark where remark2=@sno";
            int i = SqlDB.executeSql(sql, para);
            if (i > 0)
                return true;
            else return false;
        }

        public int userDataCount(string role, string sno)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@role",role),
                                  new SqlParameter("@sno",sno)
                                  };
            string sql = "";
            if (sno == ""&&role!="")
            {
                sql = "select count(*) from users where role=@role";
            }
            else if(sno!=""&&role!="")
                sql = "select count(*) from users where remark2=@sno and role=@role";
            else if (sno == "" && role == "")
            {
                sql = "select count(*) from users";
            }
            DataTable dt = SqlDB.FillDt(sql, para);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public DataTable userDataTable(string starIndex, string endIndex, string role, string sno)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@starIndex",starIndex),
                                  new SqlParameter("@endIndex",endIndex),
                                  new SqlParameter("@role",role),
                                  new SqlParameter("@sno",sno),
                                  };
            string sql = "";
            if (starIndex != "" || endIndex != "")
            {
                if (role != "" && sno != "")
                {
                    sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from users d where d.role=@role and d.remark2=@sno) dd where dd.row between @starIndex and @endIndex";
                }
                else if (role == "" & sno == "")
                {
                    sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from users d ) dd where dd.row between @starIndex and @endIndex";
                }
                else if (role != "" && sno == "")
                {
                    sql = @"select * from (
	select ROW_NUMBER() over(order by d.id desc) as row ,d.* from users d where d.role=@role) dd where dd.row between @starIndex and @endIndex";
                }
            }
            else
            {
                if (role != "" && sno != "")
                {
                    sql = @"select * from (select ROW_NUMBER() over(order by d.id desc) as row ,d.* from users d where d.role=@role and d.remark2=@sno) dd";
                }
                else if (role == "" & sno == "")
                {
                    sql = @"select * from (select ROW_NUMBER() over(order by d.id desc) as row ,d.* from users d ) dd";
                }
                else if (role != "" && sno == "")
                {
                    sql = @"select * from (select ROW_NUMBER() over(order by d.id desc) as row ,d.* from users d where d.role=@role) dd";
                }
            }
            return SqlDB.FillDt(sql, para);
        }
        public DataTable selectRole()
        {
            string sql = "select distinct role from users";
            return SqlDB.FillDt(sql);
        }

        public bool deleteUser(string id)
        {
            SqlParameter[] para = { new SqlParameter("@id",id)};
            string sql = "delete users where id=@id";
            int i = SqlDB.executeSql(sql, para);
            if (i > 0)
                return true;
            else return false;
        }

        public bool insertUser(LoginModel login)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@name",login.name),
                                  new SqlParameter("@pwd",login.pwd),
                                  new SqlParameter("@role",login.role),
                                  new SqlParameter("@tel",login.remark1),
                                  new SqlParameter("@sno",login.remark2),
                                  new SqlParameter("@remark",login.remark3),
                                  };
            string sql = "insert into users(name,pwd,role,remark1,remark2,remark3) values (@name,@pwd,@role,@tel,@sno,@remark)";
            int i = SqlDB.executeSql(sql, para);
            if (i > 0)
                return true;
            else return false;
        }

        public bool modifyUser(LoginModel login)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@name",login.name),
                                  new SqlParameter("@role",login.role),
                                  new SqlParameter("@tel",login.remark1),
                                  new SqlParameter("@sno",login.remark2),
                                  new SqlParameter("@remark",login.remark3),
                                  };
            string sql = "update users set name=@name,role=@role,remark1=@tel,remark3=@remark where remark2=@sno";
            int i = SqlDB.executeSql(sql, para);
            if (i > 0)
                return true;
            else return false;
        }
    }
}
