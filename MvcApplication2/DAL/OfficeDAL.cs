using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data.SqlClient;
using System.Data;
using Model;

namespace DAL
{
    public class OfficeDAL
    {
        public int GetDataCount(string duty,string number,string bid)
        {
            SqlParameter[] para={new SqlParameter("@duty",duty),new SqlParameter("@number",number),new SqlParameter("@bid",bid)};
            string sqlstr = "";
            if (number == "" && bid == "")
            {
                sqlstr = "select distinct count(*) from office where duty=@duty";
            }
            else if (number != "" && bid == "")
            {
                sqlstr = "select distinct count(*) from office where duty=@duty and onumber=@number";
            }
            else if (number == "" && bid != "")
            {
                sqlstr = "select distinct count(*) from office where duty=@duty and bid like '%" + @bid + "%'";
            }
            else {
                sqlstr = "select distinct count(*) from office where duty=@duty and onumber=@number and bid like '%"+@bid+"%'";
            }
            
             
            DataTable dt = new DataTable();
            dt = SqlDB.FillDt(sqlstr, para);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public DataTable GetDataTable( string starIndex, string endIndex, string number,string duty,string bid)
        {
            string sqlstr = "";
            SqlParameter[] para = { 
                                 new SqlParameter("@number",number),
                                  new SqlParameter("@duty",duty),
                                  new SqlParameter("@starIndex",starIndex),
                                  new SqlParameter("@endIndex",endIndex),
                                  new SqlParameter("@bid",bid)
                                  };
            if (number == "" && bid == "" && starIndex != "" && endIndex != "")
            {
                sqlstr = @"select * from (
	select ROW_NUMBER() over(order by d.id) as row,d.*,(YEAR(getdate())-LEFT(d.datetime,4)) as age from Office d where duty=@duty) dd
where dd.row between @starIndex and @endIndex";
            }
            else if (number == "" && bid == "" && starIndex=="" && endIndex=="")
            {
                sqlstr = @"select * from (
	select ROW_NUMBER() over(order by d.id) as row,d.*,(YEAR(getdate())-LEFT(d.datetime,4)) as age from Office d where duty=@duty) dd";
            }
            else if (number != "" && bid == "" && starIndex != "" && endIndex != "")
            {
                sqlstr = @"select * from (
	select ROW_NUMBER() over(order by d.id) as row,d.*,(YEAR(getdate())-LEFT(d.datetime,4)) as age from Office d where duty=@duty and onumber=@number) dd
where dd.row between @starIndex and @endIndex";
            }
            else if (number != "" && bid == "" && starIndex == "" && endIndex == "")
            {
                sqlstr = @"select * from (
	select ROW_NUMBER() over(order by d.id) as row,d.*,(YEAR(getdate())-LEFT(d.datetime,4)) as age from Office d where duty=@duty and onumber=@number) dd";
            }
            else if (number != "" && bid != "" && starIndex != "" && endIndex != "")
            {
                sqlstr = @"select * from (
	select ROW_NUMBER() over(order by d.id) as row,d.*,(YEAR(getdate())-LEFT(d.datetime,4)) as age from Office d where duty=@duty and onumber=@number and bid like '%" + @bid + "%') dd where dd.row between @starIndex and @endIndex";
            }
            else if (number != "" && bid != "" && starIndex == "" && endIndex == "")
            {
                sqlstr = @"select * from (
	select ROW_NUMBER() over(order by d.id) as row,d.*,(YEAR(getdate())-LEFT(d.datetime,4)) as age from Office d where duty=@duty and onumber=@number and bid like '%" + @bid + "%') dd";
            }
            else if (number == "" && bid != "" && starIndex != "" && endIndex != "") 
            {
                sqlstr = @"select * from (
	select ROW_NUMBER() over(order by d.id) as row,d.*,(YEAR(getdate())-LEFT(d.datetime,4)) as age from Office d where duty=@duty and bid like '%"+@bid+"%') dd where dd.row between @starIndex and @endIndex";
            }
            else if (number == "" && bid != "" && starIndex == "" && endIndex == "")
            {
                sqlstr = @"select * from (
	select ROW_NUMBER() over(order by d.id) as row,d.*,(YEAR(getdate())-LEFT(d.datetime,4)) as age from Office d where duty=@duty and bid like '%" + @bid + "%') dd";
            }

            return SqlDB.FillDt(sqlstr, para);
        }

        public DataTable GetOneData(string number, string duty)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@number",number),
                                  new SqlParameter("@duty",duty)
                                  };
            string sqlstr = "select * from Office where onumber=@number and duty=@duty";
            return SqlDB.FillDt(sqlstr, para);
        }

        public bool addManger(OfficeModel officeModel)
        {
           // string sqlstr = @"insert into [Office]([onumber],[job],[oname],[osex],[datetime],[duty],[bid],[remark1]) 
           //                                  values(@onumber,@job,@oname,@osex,@date,@duty,@bid,@remark)";

            string sqlstr = @"insert into Office(onumber,job,oname,osex,datetime,duty,bid,tel,oarea,remark1) 
                                       values (@onumber,@job,@oname,@osex,@date,@duty,@bid,@tel,@oarea,@remark)";
            SqlParameter[] paras = { 
                                   new SqlParameter("@onumber",officeModel.onumber),
                                   new SqlParameter("@job",officeModel.job),
                                   new SqlParameter("@oname",officeModel.oname),
                                   new SqlParameter("@osex",officeModel.osex),
                                   new SqlParameter("@date",officeModel.datetime),
                                   new SqlParameter("@duty",officeModel.duty),
                                   new SqlParameter("@bid",officeModel.did),
                                   new SqlParameter("@tel",officeModel.tel),
                                   new SqlParameter("@oarea",officeModel.oarea),
                                   new SqlParameter("@remark",officeModel.remark1)
                                   };
            int i = SqlDB.executeSql(sqlstr, paras);
            if (i > 0)
                return true;
            else return false;
        }

        public bool updateManager(OfficeModel officeModel)
        {
            
            SqlParameter[] paras = { 
                                       new SqlParameter("@onumber",officeModel.onumber),
                                   new SqlParameter("@job",officeModel.job),
                                   new SqlParameter("@oname",officeModel.oname),
                                   new SqlParameter("@osex",officeModel.osex),
                                   new SqlParameter("@date",officeModel.datetime),
                                   new SqlParameter("@duty",officeModel.duty),
                                   new SqlParameter("@bid",officeModel.did),
                                   new SqlParameter("@tel",officeModel.tel),
                                   new SqlParameter("@oarea",officeModel.oarea),
                                   new SqlParameter("@remark",officeModel.remark1)
                                   };
            string sqlstr = @"update Office set job=@job,oname=@oname,osex=@osex,datetime=@date,duty=@duty,bid=@bid,tel=@tel,oarea=@oarea,remark1=@remark where onumber=@onumber";
            int i = SqlDB.executeSql(sqlstr, paras);
            if (i > 0)
            {
                return true;
            }
            else return false;
        }

        public DataTable Select_number(string number, string duty)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@number",number),
                                  new SqlParameter("@duty",duty)
                                  };
            string sqlstr = "select distinct d.*, ( YEAR(getdate())-LEFT(d.datetime,4)) as age from Office d where onumber=@number and duty=@duty";
            return SqlDB.FillDt(sqlstr, para);
        }

        public DataTable select_Build()
        {
            string sqlstr = "select * from Building order by remark1";
            return SqlDB.FillDt(sqlstr);
        }

        public bool insertBuild(string bid,string name, string area, string remark1, string remark2)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@bid",bid),
                                  new SqlParameter("@name",name),
                                  new SqlParameter("@area",area),
                                  new SqlParameter("@remark1",remark1),
                                  new SqlParameter("@remark2",remark2)
                                  };
            string sqlstr = "insert into Building(bid,buildname,barea,remark1,remark2) values (@bid,@name,@area,@remark1,@remark2)";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
                return true;
            else return false;
        }

        public bool modiftBuild(string bid,string name, string area, string remark1, string remark2)
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@bid",bid),
                                  new SqlParameter("@name",name),
                                  new SqlParameter("@area",area),
                                  new SqlParameter("@remark1",remark1),
                                  new SqlParameter("@remark2",remark2)
                                  };
            string sqlstr = "update Building set buildname=@name,barea=@area ,remark1=@remark1,remark2=@remark2 where bid=@bid";
            int i = SqlDB.executeSql(sqlstr, para);
            if (i > 0)
            {
                return true;
            }
            else return false;
        }
    }
}
