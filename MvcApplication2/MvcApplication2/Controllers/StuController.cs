using BLL;
using Common;
using MvcApplication2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class StuController : Controller
    {
        public StuBLL _stuBll=new StuBLL();

        FormData fData = new FormData();
        //
        // GET: /Stu/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ModifyStu()
        {
            return View();
        }

        public ActionResult mod_Stu()
        {
            if (Request["txt_no"] != null)
            {
                var sno = Request["txt_no"];
                var sname = Request["txt_name"];
                var ssex = Request["txt_sex"];
                var sdept = Request["txt_dept"];
                var dorm = Request["txt_dorm"];
                var s_time = Request["txt_s_time"];
                var e_time = Request["txt_e_time"];
                var m_time = Request["txt_m_time"];
                if(_stuBll.ModifyStu(sno,sname,ssex,sdept,dorm,s_time,e_time,m_time))
                {
                return Content("ok");
                }
                else 
                    return Content("no");
            }
            return View();
        }

        public ActionResult SelectStuAll(string starIndex,string endIndex,string school)
        { 
            DataTable dt=_stuBll.selectStuAll(starIndex,endIndex,school);
            return Content(JsonConvert.SerializeObject(dt).ToString());
        }
        //导出数据
        public ActionResult Stu_ExportData(string school)
        {
            DataTable dt = new DataTable();
            dt = _stuBll.StuDownLoad(school);
            string url = Server.MapPath("~//file//StuDoc.doc");
            string title = "";
            if (school == "0")
            {
                title = "离校学生信息";
            }
            else
                title = "在校学生信息";

            ToWord toWord = new ToWord();
            toWord.CourseTable(dt,url,title);
            return Content("ok");
        }

        public ActionResult pageAll(string school)
        {
            return Content(_stuBll.pageAll(school));
        }

        public ActionResult Select_Sno(string sno,string school)
        {
            DataTable dt = new DataTable();
            dt = _stuBll.select_Son(sno,school);
            return Content(JsonConvert.SerializeObject(dt).ToString());
        }

        public ActionResult ModifyStudent(string sno,string sdept,string dorm, string e_time,string m_time)
        {
            //if (_stuBll.ModifyStu(sno, sdept, dorm, e_time, m_time))
               // return Content("OK");
            //else return Content("NO");

            return View();
        
        }

        public ActionResult addStu()
        { 
            if(Request["txt_no"]!=null)
            {
                var sno = Request["txt_no"];
                var name = Request["txt_name"];
                var sex = Request["selc_sex"];
                var dept = Request["txt_dept"];
                var dorm = Request["txt_dorm"];
                var s_time = Request["txt_s_time"];
                var e_time = Request["txt_e_time"];
                var m_time = Request["txt_m_time"];
                var school = Request["selc_sc"];
                if (_stuBll.AddStu(sno, name, sex, dept, dorm, s_time, e_time, m_time,school))
                {
                    return Content("ok");
                }
                else
                    return Content("no");
            
            }
            return View();
        }

        public ActionResult TryStu()
        {
            DataTable dt = new DataTable();
            DataTable dd = _stuBll.selectGrade();//年级
            DataTable db = _stuBll.selectDept();//系别

            int pageSize = 5;
            int totalCount;
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");//当前页
            string school = Request["txt_id"] ?? "1";//是否在校
            string grade = Request["txt_s_date"] ?? dd.Rows[0][0].ToString();//年级
            string dept = Request["txt_e_date"] ?? db.Rows[0][0].ToString();//系别
            string sno = Request["seno"] ?? "";//学号

            string starIndex = ((pageIndex - 1) * pageSize).ToString();
            string endIndex = (pageIndex * pageSize).ToString();

            totalCount = int.Parse(_stuBll.GetDataCount(grade,dept,sno,school));
            dt = _stuBll.GetDataTable(starIndex, endIndex, grade, dept, sno, school);

            fData.sDate = grade;
            fData.eDate = dept;
            fData.Bid = school;

            ViewData["dt"] = dt;
            ViewData["gra"] = dd;
            ViewData["dep"] = db;
            ViewData["pageIndex"] = pageIndex;
            ViewData["totalCount"] = totalCount;
            return View(fData);
        }

        public ActionResult TryStu_Export(string grade,string dept,string sch)
        {
            DataTable dt = new DataTable();
            ToWord toW = new ToWord();
            dt = _stuBll.TryStuExport(grade, dept, sch);
            string school="";
            if (sch == "1")
                school = "在校学生信息";
            else school = "离校学生信息";
            string title = grade + "级" + dept + "学院"+school;
            string url = Server.MapPath("~//file//TryStuDoc.doc");
            toW.CourseTable(dt,url,title);
            return Content("ok");
        }

        public ActionResult SeeStudent()
        {
            return View();
        }
        public ActionResult GetUserInfo(string name)
        {
            DataTable dt = new DataTable();
            dt = _stuBll.GetUserInfo(name);
            return Content(JsonConvert.SerializeObject(dt).ToString());
        }

        //public ActionResult StuRepair()
        //{
            
        //    return View();
        //}
    }
}
