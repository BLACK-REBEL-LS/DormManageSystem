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
    public class PlanDAL
    {
        public DataTable sdeptTable()
        {
            string sql = "select distinct sdept from student where sdept is not null";
            return SqlDB.FillDt(sql);
        }

        public DataTable buildTable()
        {
            string sql = "select distinct bid from building";
            return SqlDB.FillDt(sql);
        }

        public DataTable selectNoPlanStu(string sdept, string sex,string grade)
        {
            string school = "1";//默认学生 为在校学生
            string dorm = "";
            SqlParameter[] para = {
                                      new SqlParameter("@school",school),
                                      new SqlParameter("@dorm",dorm),
                                  new SqlParameter("@sdept",sdept),
                                  new SqlParameter("@sex",sex),
                                  new SqlParameter("@grade",grade)
                                  };
            string sql = "";
            if (sdept == "" && sex == ""&&grade=="")
            {
                 sql = "select distinct * from student where school='1' and (dorm='' or dorm is null) order by sno";
                 return SqlDB.FillDt(sql);
            }
            else
            {
                sql = "select distinct * from student where ssex=@sex and sdept=@sdept and school=@school and sno like '%"+@grade+"%' and (dorm=@dorm or dorm is null ) order by sno";
                return SqlDB.FillDt(sql, para);
            }
            
        }

        public DataTable selectBuild(string bid)
        {
            SqlParameter[] para = {new SqlParameter("@bid",bid),
                                  new SqlParameter("@total",6)
                                  };
            string sql = "select distinct dno,(@total-dcount) as ct from Dorm where bid=@bid";
            return SqlDB.FillDt(sql, para);
        }

        public DataTable selectDormCount(string dormdt)
        {
            SqlParameter[] para = { new SqlParameter("@dormdt", dormdt) };
            string sql = "select left(@dormdt,3) as dno,substring(@dormdt,charindex('(',@dormdt)+1,1) as ct";
            return SqlDB.FillDt(sql, para);
        }

        public bool updateDorm(string bid, string dno, string sno)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@bid",bid),
                                  new SqlParameter("@dno",dno),
                                  new SqlParameter("@sno",sno),
                                  };
            
            string sqlstr = "update dorm set dcount+=1 where bid=@bid and dno=@dno";
            
            int j = SqlDB.executeSql(sqlstr, para);
            if ( j > 0 && updateStu(bid,dno,sno)>0)
            {
                return true;
            }
            else return false;
        }

        public int updateStu(string bid, string dno, string sno)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@bid",bid),
                                  new SqlParameter("@dno",dno),
                                  new SqlParameter("@sno",sno),
                                  };
            string sql = "update student set dorm=@bid+'#'+@dno where sno=@sno";
            int i = SqlDB.executeSql(sql, para);
            return i;
        }

        public bool backStu(string grade)
        {
            //SqlParameter[] para = { new SqlParameter("@school",0)};
            int j=updateBackStu(grade);
            string sql = @"update Dorm set dcount=dcount-CAST(a.people as int) from dorm,(select dorm d,COUNT(*) people from Student where sno like '"+grade+"%' and school='0' group by dorm)a where a.d=Dorm.dname";
            int i = SqlDB.executeSql(sql);
            if (i > 0 && j > 0)
            {
                return true;
            }
            else
                return false;
        }
        public int updateBackStu(string grade)
        {
            //SqlParameter[] para = {new SqlParameter("@school",0)};
            string sql = "update student set school='0' where sno like '"+grade+"%'";
            int i = SqlDB.executeSql(sql);
            return i;
        }
    }
}
