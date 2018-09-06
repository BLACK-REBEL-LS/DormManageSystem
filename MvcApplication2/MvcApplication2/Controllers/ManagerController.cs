using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using Newtonsoft.Json;
using Common;

namespace MvcApplication2.Controllers
{
    public class ManagerController : Controller
    {
        //
        // GET: /Manager/
        private OfficeModel _officeModel = new OfficeModel();
        private OfficeBLL _officeBLL = new OfficeBLL();

       
        static string mbid = "";
        static string md = "";
        

        public ActionResult Index()
        {
            DataTable dt = new DataTable();

            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");//当前页
            int pageSize = 5;
            int totalCount;

            string duty = Request["duty"] ?? "1";//是否是 在岗职工
            string number = "";
            string bid = "";

            string starIndex = ((pageIndex - 1) * pageSize).ToString();
            string endIndex = (pageIndex * pageSize).ToString();
            if (Request["txt_number"] != null)
            {

                if ((Request["txt_number"] != "") && (Request["txt_bid"] != ""))
                {//职工编号 寝室楼号 都不为空

                    number = Request["txt_number"];//职工编号
                    bid = Request["txt_bid"];//寝室楼号

                    totalCount = _officeBLL.GetDataCount(duty, number, bid); //应该为 1
                    dt = _officeBLL.GetDataTable(starIndex, endIndex, number, duty, bid);
                }
                else if ((Request["txt_number"] == "") && (Request["txt_bid"] != ""))
                { //职工编号为空，寝室楼号  不为空

                    bid = Request["txt_bid"];
                    totalCount = _officeBLL.GetDataCount(duty, "", bid);//数据的总行数
                    dt = _officeBLL.GetDataTable(starIndex, endIndex, "", duty, bid);
                }
                else if ((Request["txt_number"] != "") && (Request["txt_bid"] == ""))
                {
                    //职工编号不为空，寝室楼号 为空
                    number = Request["txt_number"];
                    totalCount = _officeBLL.GetDataCount(duty, number, "");
                    dt = _officeBLL.GetDataTable(starIndex, endIndex, number, duty, "");
                }
                else
                {

                    totalCount = _officeBLL.GetDataCount(duty, "", "");
                    dt = _officeBLL.GetDataTable(starIndex, endIndex, "", duty, "");

                }
                ViewData["number"] = number;
                ViewData["bid"] = bid;
            }
            else 
            {
                totalCount = _officeBLL.GetDataCount(duty,"","");
                dt = _officeBLL.GetDataTable(starIndex,endIndex,"",duty,"");
            }

           // ma = duty;
            md = duty;
            mbid = bid;

            ViewData["Table"] = dt;//获得的表
            ViewData["pageIndex"] = pageIndex;//当前页面
            ViewData["totalCount"] = totalCount;//数据总行数
            
            
            return View();
        }

        public ActionResult AddManger()
        {
            if(Request["txt_number"] != null )
            {
                //OfficeModel _office = new OfficeModel();
                _officeModel.onumber = int.Parse(Request["txt_number"]);
                _officeModel.job = Request["txt_job"];
                _officeModel.oname = Request["txt_name"];
                _officeModel.osex = Request["selc_sex"];
                _officeModel.datetime = Request["txt_date"];
                _officeModel.duty =int.Parse( Request["selc_duty"]);
                _officeModel.did = Request["txt_bid"];

                _officeModel.tel = Request["txt_tel"];
                _officeModel.oarea = Request["txt_area"];

                _officeModel.remark1 = Request["txt_remark"];

                if (_officeBLL.addManger(_officeModel))
                {
                    return Content("ok");
                }
                else  return Content("no"); 
            }
            return View();
        }

        public ActionResult MainManager() {
            return View();
        }

        public ActionResult ModifyManger()
        {
            if (Request["txt_number"] != null)
            {
                OfficeModel _office = new OfficeModel();
                _office.onumber = int.Parse(Request["txt_number"]);
                _office.job = Request["txt_job"];
                _office.oname = Request["txt_name"];
                _office.osex = Request["selc_sex"];
                _office.datetime = Request["txt_date"];
                _office.duty = int.Parse(Request["selc_duty"]);
                _office.did = Request["txt_bid"];

                _office.tel = Request["txt_tel"];
                _office.oarea = Request["txt_area"];

                _office.remark1 = Request["txt_remark"];

                if (_officeBLL.updateManager(_office))
                {
                    return Content("ok");
                }
                else { return Content("no"); }
            }
            return View();
        }

        public ActionResult addBuild()
        {
            if (Request["txt_name"] != null)
            {
                var bid = Request["txt_bid"];
                var name = Request["txt_name"];
                var area = Request["txt_area"];
                var remark1 = Request["sex"];
                var remark2 = Request["txt_remark"];
                if (_officeBLL.insertBuild(bid,name, area, remark1, remark2))
                {
                    return Content("ok");
                }
                else
                    return Content("on");

            }
            return View();
        }

        //public ActionResult updateData(string starIndex, string endIndex, string duty)
        //{
        //    DataTable dt = new DataTable();
        //    dt = _officeBLL.GetDataTable(starIndex, endIndex, duty);
        //    return Content(JsonConvert.SerializeObject(dt).ToString());
        //}

        //public ActionResult getDataCount(string duty)
        //{
        //    return Content(_officeBLL.GetDataCount(duty).ToString());
        //}

        public ActionResult Select_number(string number, string duty)
        {
            DataTable dt = new DataTable();
            dt = _officeBLL.Select_number(number, duty);
            ViewData["dt_num"] = dt;
            return Content(JsonConvert.SerializeObject(dt).ToString());
        }

        public ActionResult Build()
        {
            DataTable dt = new DataTable();
            dt = _officeBLL.select_Build();
            ViewData["dt"] = dt;
            return View();
        }

        public ActionResult updataBuild()
        {
            DataTable dt = _officeBLL.select_Build();
            return Content(JsonConvert.SerializeObject(dt).ToString());
        }

        public ActionResult modiftBuild()
        {
            if (Request["txt_name"] != null)
            {
                var bid = Request["txt_bid"];
                var name = Request["txt_name"];
                var area = Request["txt_area"];
                var remark1 = Request["sex"];
                var remark2 = Request["txt_remark"];
                if (_officeBLL.modiftBuild(bid,name, area, remark1, remark2))
                {
                    return Content("ok");
                }
                else return Content("no");
                

            }
            return View();
        }
        ToWord toWord = new ToWord();
        public ActionResult Office_Excel()
        {
            DataTable dt = _officeBLL.OfficeDownData("","","",md,mbid);
            string title = "";
            if (md != "")
            {
                if (md == "1")
                {
                    title = "在岗职工信息";
                }
                else title = "离岗职工信息";
            }
            if (mbid != "" && md != "")
            {
                title = mbid + "楼 " + title;
            }
            string url = Server.MapPath("~//file//ManagerDoc.doc");
            if (toWord.CourseTable(dt, url, title))
            {

                
                md = "";
                mbid = "";
                return Content("ok");
            }
            else return Content("no");
        }

        //public ActionResult modifyUser(string sno)
        //{ 
        
        //}
    }
}
