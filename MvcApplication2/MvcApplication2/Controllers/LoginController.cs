using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using System.Data;
using MvcApplication2.Models;
using Common;
namespace MvcApplication2.Controllers
{
    public class LoginController : Controller
    {
        LoginBLL _loginBLL = new LoginBLL();
        LoginModel login = new LoginModel();

        private static string usno = "";

        private FormData fdata = new FormData();
        //
        // GET: /Login/


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckLogin()
        {
            string userName = Request["name"];
            string userPwd = Request["pwd"];

            login.name = userName;
            login.pwd = userPwd;

            usno = userName;//用户输入的登陆名  编号
            return Content(_loginBLL.CheckUser(login).ToString());
        }

        public ActionResult SubmitUser()
        {
            string user = Request["txt_user"];
            string pwd = Request["txt_pwd"];
            string pwd2 = Request["txt_pwd2"];
            string sno = Request["txt_sno"];
            string tel = Request["txt_tel"];
            string count = "";
            if (pwd != pwd2) //两次输入的密码不一致
            {
                count = "-1";
                //return Content("");
            }
            else
            {
                if (_loginBLL.selectSno(sno)) //student 表里面存在此 编号
                {
                    login.name = user;
                    login.pwd = pwd;
                    login.remark1 = tel;
                    login.remark2 = sno;
                    if (_loginBLL.InsertUser(login))
                    {
                        count = "2";//成功插入
                    }
                    else count = "3";//插入失败
                }
                else count = "4";
            }
            return Content(count);
        }

        public ActionResult StuRepair()
        {
            DataTable dt = new DataTable();
            int pageSize = 5;
            int totalCount;
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            string sdate = Request["txt_s_date"] ?? "";
            string edate = Request["txt_e_date"] ?? "";

            string starIndex = ((pageIndex - 1) * pageSize).ToString();
            string endIndex = (pageIndex * pageSize).ToString();

            totalCount = _loginBLL.GetDataCount(usno, sdate,edate);
            dt = _loginBLL.GetStuRepair(usno,sdate,edate,starIndex,endIndex);

            fdata.sDate = sdate;
            fdata.eDate = edate;

            ViewData["dt"] = dt;
            ViewData["pageIndex"] = pageIndex;
            ViewData["totalCount"] = totalCount;
            return View(fdata);
        }

        public ActionResult StuRepair_Export(string stime,string etime)
        {
            DataTable dt = _loginBLL.StuRepairDownData(usno,stime, etime);
            ToWord toW = new ToWord();
            string url = Server.MapPath("~//file//StuRepairDoc.doc");
            string title = usno+"报修记录";
            toW.CourseTable(dt, url, title);
            return Content("ok");
        }

        public ActionResult StuDiscipline()
        {
            DataTable dt = new DataTable();
            int pageSize = 5;
            int totalCount;
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            string sdate = Request["txt_s_date"] ?? "";
            string edate = Request["txt_e_date"] ?? "";

            string starIndex = ((pageIndex - 1) * pageSize).ToString();
            string endIndex = (pageIndex * pageSize).ToString();

            totalCount = _loginBLL.StuDisciplineCont(usno, sdate, edate);
            dt = _loginBLL.StuDisciplineTable(usno, sdate, edate, starIndex, endIndex);

            fdata.sDate = sdate;
            fdata.eDate = edate;

            ViewData["dt"] = dt;
            ViewData["pageIndex"] = pageIndex;
            ViewData["totalCount"] = totalCount;
            return View(fdata);
        }

        public ActionResult StuDiscipline_Export(string stime,string etime)
        {
            DataTable dt = new DataTable();
            dt = _loginBLL.StuDisciplineDownData(usno, stime, etime);
            ToWord toW = new ToWord();
            string url = Server.MapPath("~//file//StuDisciplineDoc.doc");
            string title = usno + "违纪记录";
            toW.CourseTable(dt, url, title);
            return Content("ok");
        }
      
        public ActionResult StuInfo()
        {
            DataTable dt = new DataTable();
            dt = _loginBLL.GetLoginInfo(usno);//根据编号 得到所有信息；
           // ViewData["dt"] = dt;
            login.name = dt.Rows[0]["name"].ToString();
            login.remark1 = dt.Rows[0]["remark1"].ToString();
            login.remark2 = dt.Rows[0]["remark2"].ToString();
            login.remark3 = dt.Rows[0]["remark3"].ToString();
          //  login.pwd = dt.Rows[0]["pwd"].ToString();

            return View(login);
        }
        public ActionResult modifyStu()
        {
           // if (Request["txt_name"] != null)
          //  {
                login.name = Request["txt_name"];
                login.remark1 = Request["txt_tel"];
                login.remark3 = Request["txt_remark"];
                if (Request["txt_pwd"] != Request["txt_pwd2"])
                {
                    return Content("-1");
                }
                else
                {
                    login.pwd = Request["txt_pwd"];
                }
                if (_loginBLL.modifyLogin(usno, login))
                {
                    return Content("2");
                }
                else return Content("3");
           // }
        }

        SchoolBLL _schoolBLL = new SchoolBLL();

        public ActionResult StuSchool()
        {
            DataTable dt = _schoolBLL.stuSchoolTable(usno);
            ViewData["dt"] = dt;
            return View();
        }
    }
}
