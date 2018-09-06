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
    public class LoginBLL
    {
        public LoginDAL _loginDAL = new LoginDAL();

        public int CheckUser(LoginModel login)
        {
            DataTable dt = _loginDAL.GetOneUser(login);//根据用户名或学号、编号 找出一条结果
            int count = 0;//用户不存在
            if (dt != null)
            {
                string str = dt.Rows[0]["pwd"].ToString();


                if (str.Equals(login.pwd))//密码一致
                {
                    if (dt.Rows[0]["role"].ToString() == "admin")
                    {
                        count = 3;//管理者
                    }
                    else
                    {
                        count = 4;//学生
                    }
                }
                else
                {
                    count = 2;//密码错误
                }
            }
            return count;
        }
        public bool selectSno(string sno)
        {
            return _loginDAL.selectSno(sno);
        }
        public bool selectSno(string sno, string role)
        {
            return _loginDAL.selectSno(sno, role);
        }

        public bool InsertUser(LoginModel login)
        {
            return _loginDAL.InsertUser(login);
        }

        public DataTable GetStuRepair(string sno,string stime,string etime,string starIndex,string endIndex)
        {
            return _loginDAL.GetStuRepair(sno,stime,etime,starIndex,endIndex);
        }

        public int GetDataCount(string sno, string stime,string etime)
        {

            return _loginDAL.GetDataCount(sno, stime,etime);
        }

        public DataTable StuRepairDownData(string sno,string stime,string etime)
        {
            DataTable dt = new DataTable();
            dt = _loginDAL.GetStuRepair(sno,stime,etime,"","");
            DataTable dd = new DataTable();
            dd.Columns.Add("序号");
            dd.Columns.Add("宿舍");
            dd.Columns.Add("类别");
            dd.Columns.Add("是否处理");
            dd.Columns.Add("报修时间");
            dd.Columns.Add("完成时间");
            dd.Columns.Add("报修人"); 
            dd.Columns.Add("报修人学号");
            dd.Columns.Add("维修人");
            dd.Columns.Add("备注");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow r = dd.NewRow();
                r["序号"] = dt.Rows[i]["row"].ToString();
                r["宿舍"] = dt.Rows[i]["dorm"].ToString();
                r["类别"] = dt.Rows[i]["category"].ToString();
                r["是否处理"] = dt.Rows[i]["handle"].ToString();
                r["报修时间"] = dt.Rows[i]["s_time"].ToString();
                r["完成时间"] = dt.Rows[i]["e_time"].ToString();
                r["报修人"] = dt.Rows[i]["rname"].ToString();
                r["报修人学号"] = dt.Rows[i]["sno"].ToString();
                r["维修人"] = dt.Rows[i]["wname"].ToString();
                r["备注"] = dt.Rows[i]["remark"].ToString();
                dd.Rows.Add(r);
            }
            return dd;
        }

        public int StuDisciplineCont(string sno, string stime, string etime)
        {
            return _loginDAL.GetDisciplineCount(sno, stime, etime);
        }
        public DataTable StuDisciplineTable(string sno, string stime, string etime, string starIndex, string endIndex)
        {
            return _loginDAL.GetDisciplineTable(sno, stime, etime, starIndex, endIndex);
        }

        public DataTable StuDisciplineDownData(string sno, string stime, string etime)
        {
            DataTable dt = new DataTable();
            DataTable dd = new DataTable();
            dt = _loginDAL.GetDisciplineTable(sno, stime, etime, "", "");
            dd.Columns.Add("序号");
            dd.Columns.Add("姓名");
            dd.Columns.Add("学号");
            dd.Columns.Add("宿舍");
            dd.Columns.Add("原因");
            dd.Columns.Add("处罚");
            dd.Columns.Add("审批人");
            dd.Columns.Add("时间");
            dd.Columns.Add("备注");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow r = dd.NewRow();
                r["序号"] = dt.Rows[i]["row"].ToString();
                r["姓名"] = dt.Rows[i]["dname"].ToString();
                r["学号"] = dt.Rows[i]["sno"].ToString();
                r["宿舍"] = dt.Rows[i]["dorm"].ToString();
                r["原因"] = dt.Rows[i]["reason"].ToString();
                r["处罚"] = dt.Rows[i]["punish"].ToString();
                r["审批人"] = dt.Rows[i]["leader"].ToString();
                r["时间"] = dt.Rows[i]["dtime"].ToString();
                r["备注"] = dt.Rows[i]["remark"].ToString();
                dd.Rows.Add(r);
            }
            return dd;
        }

        public DataTable GetLoginInfo(string sno)
        {
            return _loginDAL.GetLoingInfo(sno);
        }
        public bool modifyLogin(string sno, LoginModel login)
        {
            return _loginDAL.modifyLogin(sno,login);
        }

        public int userDataCount(string role, string sno)
        {
            return _loginDAL.userDataCount(role, sno);
        }
        public DataTable userDataTable(string starIndex, string endIndex, string role, string sno)
        {
            return _loginDAL.userDataTable(starIndex, endIndex, role,sno);
        }

        public DataTable selectRoel()
        {
            return _loginDAL.selectRole();
        }

        public bool deleteUser(string id)
        {
            return _loginDAL.deleteUser(id);
        }
        public bool insertUser(LoginModel login)
        {
            return _loginDAL.insertUser(login);
        }
        public bool modifyUser(LoginModel login)
        {
            return _loginDAL.modifyUser(login);
        }

        public DataTable userDownData(string role)
        {
            DataTable dt = new DataTable();
            dt = _loginDAL.userDataTable("", "", role, "");
            DataTable dd = new DataTable();
            dd.Columns.Add("序号");
            dd.Columns.Add("昵称");
            dd.Columns.Add("编号");
            dd.Columns.Add("权限");
            dd.Columns.Add("联系电话");
            dd.Columns.Add("备注");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow r = dd.NewRow();
                r["序号"] = dt.Rows[i]["row"].ToString();
                r["昵称"] = dt.Rows[i]["name"].ToString();
                r["编号"] = dt.Rows[i]["remark2"].ToString();
                r["权限"] = dt.Rows[i]["role"].ToString();
                r["联系电话"] = dt.Rows[i]["remark1"].ToString();
                r["备注"] = dt.Rows[i]["remark3"].ToString();
                dd.Rows.Add(r);
            }
            return dd;
        }
    }

}
