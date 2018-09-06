using BLL;
using Model;
using MvcApplication2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Common;
namespace MvcApplication2.Controllers
{
    public class AdministorController : Controller
    {
        leaveBLL _leaveBLL = new leaveBLL();
        LeaveModel _leaveModel = new LeaveModel();

        disciplineBLL _disciplineBLL = new disciplineBLL();
        disciplineModel _discipline = new disciplineModel();

        FormData fdata = new FormData();

        RepairBLL _repairBLL = new RepairBLL();
        RepairModel _repair = new RepairModel();

        SchoolBLL _schoolBLL = new SchoolBLL();
        SchoolModel _school = new SchoolModel();

        PlanBLL _planBLL = new PlanBLL();

       private static List<stuModel> _stu=new List<stuModel>();//学生的集合
       private static List<dormMode> _dorm = new List<dormMode>();//宿舍的集合

       FeedBLL _feedBLL = new FeedBLL();
       FeedModel _feed = new FeedModel();

       LoginBLL _loginBLL = new LoginBLL();
       LoginModel login = new LoginModel();

       static string stime = "";
       static string etime = "";
       static string sbid = "";
       static string scate = "";
       static string sha = "";

        //
        // GET: /Administor/


        public ActionResult Index()
        {
            return View();
        }
        #region 辅助功能

        public ActionResult OutPeopleCome()
        {
            DataTable dt = new DataTable();

            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");//当前页
            int pageSize = 5;
            int totalCount;
            string s_time = Request["txt_s_date"] ?? "";
            string e_time = Request["txt_e_date"] ?? "";
            string bid = Request["txt_id"] ?? "";

            string starIndex = ((pageIndex - 1) * pageSize).ToString();
            string endIndex = (pageIndex * pageSize).ToString();

            totalCount = _leaveBLL.GetDataCount(s_time, e_time, bid);
            dt = _leaveBLL.GetDataTable(starIndex, endIndex, s_time, e_time, bid);

            fdata.sDate = s_time;
            fdata.eDate = e_time;
            fdata.Bid = bid;

            stime = s_time;
            etime = e_time;
            sbid = bid;

            ViewData["dt"] = dt;
            ViewData["pageIndex"] = pageIndex;
            ViewData["totalCount"] = totalCount;


            return View(fdata);
        }

        public ActionResult ModifyOutPeole()
        {
            if (Request["txt_lname"] != null)
            {
                _leaveModel.lname = Request["txt_lname"];
               _leaveModel.iname= Request["txt_iname"];
                _leaveModel.dorm = Request["txt_dorm"];
                _leaveModel.reason = Request["txt_reason"];
                _leaveModel.leader = Request["txt_leader"];
                _leaveModel.s_time = Request["txt_s_time"];
                 _leaveModel.e_time = Request["txt_e_time"];
                _leaveModel.tel = Request["txt_tel"];
                _leaveModel.remark = Request["txt_remark"];
                _leaveModel.id = int.Parse(Request["txt_id"]);
                if(_leaveBLL.modifyLeave(_leaveModel)){
                    return Content("ok");
                   
                }
                else 
                    return Content("no");
            }
            return View();
        }

        public ActionResult addLeave()
        {
            if(Request["txt_lname"]!=null)
            {
                _leaveModel.lname=Request["txt_lname"];
                _leaveModel.iname = Request["txt_iname"];
                _leaveModel.dorm = Request["txt_dorm"];
                _leaveModel.reason = Request["txt_reason"];
                _leaveModel.leader = Request["txt_leader"];
                _leaveModel.s_time = Request["txt_s_time"];
                _leaveModel.e_time = Request["txt_e_time"];
                _leaveModel.tel = Request["txt_tel"];
                _leaveModel.remark = Request["txt_remark"];
                if(_leaveBLL.addLeave(_leaveModel))
                {
                    return Content("ok");
                }
                else return Content("no");
            }
            return View();
        }

        public ActionResult deleteLeave(string id)
        {
            if (_leaveBLL.deleteLeave(id))
            {

                return Content("true");
            }
            else
            {
                return Content("false");
            }
        }
        
        public ActionResult Discipline()
        {
            DataTable dt = new DataTable();      
            int pageSize = 5;
            int totalCount;
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");//当前页
            string s_time = Request["txt_s_date"] ?? "";
            string e_time =Request["txt_e_date"] ?? "";
            string bid = Request["txt_id"] ?? "";
            
            string starIndex = ((pageIndex - 1) * pageSize).ToString();
            string endIndex = (pageIndex * pageSize).ToString();

            totalCount = _disciplineBLL.GetDataCount(s_time, e_time, bid);
            dt = _disciplineBLL.GetDataTable(starIndex, endIndex, s_time, e_time, bid);

            fdata.sDate = s_time;
            fdata.eDate = e_time;
            fdata.Bid = bid;

            stime = s_time;
            etime = e_time;
            sbid = bid;

            ViewData["dt"] = dt;
            ViewData["pageIndex"] = pageIndex;
            ViewData["totalCount"] = totalCount;

           // ViewData["FormData"] = fdata;

            return View(fdata);
        }

        public ActionResult modifyDiscipline()
        {
            if (Request["txt_lname"] != null)
            {
                _discipline.dname = Request["txt_lname"];
                _discipline.id = Request["txt_id"];
                _discipline.dorm = Request["txt_dorm"];
                _discipline.reason = Request["txt_reason"];
                _discipline.punish = Request["txt_punish"];
                _discipline.leader = Request["txt_leader"];
                _discipline.dtime = Request["txt_s_time"];
                _discipline.remark = Request["txt_remark"];
                _discipline.sno = Request["txt_sno"];
                if (_disciplineBLL.modifyDiscipline(_discipline))
                {
                    return Content("ok");
                }
                else
                    return Content("no");
            }
            return View();
        }

        public ActionResult deleteDiscipline(string id)
        {
            if (_disciplineBLL.deleteDiscipline(id))
                return Content("true");
            else return Content("false");
        }

        public ActionResult addDiscipline()
        {
            if (Request["txt_lname"] != null)
            {
                _discipline.dname = Request["txt_lname"];
                _discipline.dorm = Request["txt_dorm"];
                _discipline.reason = Request["txt_reason"];
                _discipline.punish = Request["txt_punish"];
                _discipline.leader = Request["txt_leader"];
                _discipline.dtime = Request["txt_s_time"];
                _discipline.remark = Request["txt_remark"];
                _discipline.sno = Request["txt_sno"];
                if (_disciplineBLL.addDiscipline(_discipline))
                {
                    return Content("ok");
                }
                else return Content("no");
            }
            return View();
        }

        public ActionResult Repair()
        {
            DataTable dt = new DataTable();
            int pageSize = 5;
            int totalCount;
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");//当前页

            string s_time = Request["txt_s_date"] ?? "";
            string e_time = Request["txt_e_date"] ?? "";
            string bid = Request["txt_id"] ?? "";
            string category = Request["txt_category"] ?? "";
            string handle = Request["handle"] ?? "";

            string starIndex = ((pageIndex - 1) * pageSize).ToString();
            string endIndex = (pageIndex * pageSize).ToString();


            totalCount = _repairBLL.GetDataCount(s_time,e_time,bid,category,handle);
            dt = _repairBLL.GetDataTable(starIndex,endIndex,s_time,e_time,bid,category,handle);

            fdata.sDate = s_time;
            fdata.eDate = e_time;
            fdata.Bid = bid;
            fdata.cateGory = category;
            fdata.handle = handle;

            stime = s_time;
            etime = e_time;
            sbid = bid;
            scate = category;
            sha = handle;

            ViewData["dt"] = dt;
            ViewData["pageIndex"] = pageIndex;
            ViewData["totalCount"] = totalCount;
            ViewData["dtCate"] = _repairBLL.Category();
            return View(fdata);
        }

        public ActionResult delectRepair(string id)
        { 
            if(_repairBLL.deleteRepair(id))
                return Content("true");
            else
                return Content("false");
        }

        public ActionResult modifyRepair()
        {
            if (Request["txt_id"] != null)
            {
                _repair.id = Request["txt_id"];
               _repair.rname= Request["txt_rname"];
               _repair.dorm= Request["txt_dorm"];
               _repair.category= Request["txt_category"];
               _repair.handle= int.Parse( Request["handle"]);
               _repair.s_time= Request["txt_s_time"];
               _repair.e_time= Request["txt_e_time"];
               _repair.wname= Request["txt_wname"];
               _repair.remark= Request["txt_remark"];
               _repair.sno = Request["txt_sno"];
               if (_repairBLL.modifyRepair(_repair))
               {
                   return Content("ok");
               }
               else
                   return Content("on");
            }
            return View();
        }

        public ActionResult addRepair()
        {
            DataTable dt = _repairBLL.Category();
            ViewData["dt"] = dt;
            if (Request["txt_rname"] != null)
            {
               // _repair.id = Request["txt_id"];
                _repair.rname = Request["txt_rname"];
                _repair.dorm = Request["txt_dorm"];
                _repair.category = Request["txt_category"];
                _repair.handle = int.Parse(Request["handle"]);
                _repair.s_time = Request["txt_s_time"];
                _repair.e_time = Request["txt_e_time"];
                _repair.wname = Request["txt_wname"];
                _repair.remark = Request["txt_remark"];
                _repair.sno = Request["txt_sno"];
                if (_repairBLL.addRepair(_repair))
                {
                    return Content("ok");
                }
                else
                    return Content("on");
            }
            return View();
        }

        public ActionResult School()
        {
            DataTable dt = new DataTable();
            int pageSize = 5;
            int totalCount;
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");//当前页
            string schyear = Request["txt_s_date"] ?? "";
            string vacation = Request["txt_e_date"] ?? "";

            string starIndex = ((pageIndex - 1) * pageSize).ToString();
            string endIndex = (pageIndex * pageSize).ToString();

            totalCount = _schoolBLL.GetDataCount(schyear,vacation);
            dt = _schoolBLL.GetDataTable(starIndex,endIndex,schyear,vacation);

            ViewData["dt"] = dt;
            ViewData["pageIndex"] = pageIndex;
            ViewData["totalCount"] = totalCount;
            ViewData["year"] = _schoolBLL.schYear();

            fdata.sDate = schyear;
            fdata.eDate = vacation;

            scate = schyear;
            sha = vacation;

            return View(fdata);
        }

        public ActionResult deleteSchool(string id)
        {
            if (_schoolBLL.deleteShool(id))
            {
                return Content("true");
            }
            else
            {
                return Content("false");
            }
        }

        public ActionResult modifySchool()
        {
            if (Request["txt_id"] != null)
            {
                _school.id = Request["txt_id"];
                _school.name = Request["txt_name"];
                _school.sno = Request["txt_no"];
                _school.s_time = Request["txt_s_time"];
                _school.e_time = Request["txt_e_time"];
                _school.pdorm = Request["txt_pdorm"];
                _school.odorm = Request["txt_odorm"];
                _school.leader = Request["txt_leader"];
                _school.tel = Request["txt_tel"];
                _school.remark = Request["txt_remark"];
                if (_schoolBLL.modifySchool(_school))
                {
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
            return View();
        }

        public ActionResult addSchool()
        {
            if (Request["txt_name"] != null)
            {
                _school.name = Request["txt_name"];
                _school.sno = Request["txt_no"];
                _school.s_time = Request["txt_s_time"];
                _school.e_time = Request["txt_e_time"];
                _school.pdorm = Request["txt_pdorm"];
                _school.odorm = Request["txt_odorm"];
                _school.leader = Request["txt_leader"];
                _school.tel = Request["txt_tel"];
                _school.remark = Request["txt_remark"];
                if (_schoolBLL.addSchool(_school))
                    return Content("ok");
                else return Content("no");
                
            }
            return View();
        }

        #endregion

        #region 安排住宿 和 学生退宿
        public ActionResult Plan()
        {
            DataTable sdept = new DataTable();
            sdept = _planBLL.sdeptTable();
            ViewData["sdept"] = sdept;//用于系别的下拉框

            DataTable grade = new DataTable();
            grade = _planBLL.selectGrage();
            ViewData["grade"] = grade;


            return View();
        }

        public ActionResult selectNoPlanStu(string sdept,string sex,string grade)
        {
            DataTable dt = _planBLL.selectNoPlanStu(sdept, sex,grade);
            return Content(JsonConvert.SerializeObject(dt).ToString());
        }

        public ActionResult selectBuild()
        {
            DataTable build = new DataTable();
            build = _planBLL.buildTable();
            ViewData["build"] = build;//用于 楼号的下拉框

            return View();
        }
        public ActionResult selectDorm(string bid)
        {
            DataTable dt = _planBLL.selectBuild(bid);
            return Content(JsonConvert.SerializeObject(dt).ToString());
        }
        public void updateStu(List<stuModel> stuList)
        {
            _stu = stuList;
           // return Content("ok");
        }

        public void updateDorm(List<dormMode> dormList)
        {
            _dorm = dormList;
           // return Content("ok");
        }

        public ActionResult sureAction(string bid)
        {
            int n = _stu.Count;//要安排的学生数
            //判断选择的宿舍床位 够不够 安排
            string dormdt = "";
            DataTable dt = new DataTable();
            int sum = 0;
            Dictionary<string, int> _d = new Dictionary<string, int>();
            foreach (dormMode dorm in _dorm)
            {
                dormdt = dorm.dno;
                dt = _planBLL.selectDormCount(dormdt);
                sum+=int.Parse(dt.Rows[0]["ct"].ToString());//选择的宿舍 能安排的人数
                _planBLL.dormAndcount(dormdt);
            }
            _d = PlanBLL.dictionary;//宿舍和对应人数的 集合 键值对
            int count = 0;
            string dor, son;
            if (sum >= n)//能安排下
            {
                int j = _stu.Count-1;//<list>集合移除元素时，索引值会发生改变 要想按照想要的结果进行，则可以倒着来处理
                foreach(var item in _d)
                {
                    count = item.Value;//一个宿舍能安排的人数
                    dor = item.Key;//宿舍的门号
                    for (int i = 0; i < count && j>=0; i++,j--)//一个一个学生的处理
                    {
                            son = _stu[j].sno;
                            if (_planBLL.updateDorm(bid, dor, son))//处理完一个学生，就把他从集合中移除
                            {
                                _stu.RemoveAt(j);
                            }
                        
                    }
                }
                return Content("ok");
            }
            else//不能安排下，要反馈给用户,先把能安排的安排上，再把没有安排的反馈
            {
                int surplus = n - sum;//剩余不能安排的学生

                int j =sum - 1;
                foreach (var item in _d)
                {
                    count = item.Value;
                    dor = item.Key;
                    for (int i = 0; i < count && j >= 0; i++, j--)
                    {
                        son = _stu[j].sno;
                        if (_planBLL.updateDorm(bid, dor, son))//处理完一个学生，就把他从集合中移除
                        {
                            _stu.RemoveAt(j);
                        }
                    }
                }
                string str = "不能完全安排学生住宿，还剩 "+surplus+" 个学生";
                return Content(str);
            }
        }

        public ActionResult backStu(string grade)
        {
            if (_planBLL.backStu(grade))
            {
                return Content("ok");
            }
            else
                return Content("no");
        }
        #endregion

        #region 反馈信息
        public ActionResult FeedBack()
        {

            DataTable dt = new DataTable();
            int pageSize = 5;
            int totalCount;
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");//当前页
            string s_time = Request["txt_s_date"] ?? "";
            string e_time = Request["txt_e_date"] ?? "";

            string starIndex = ((pageIndex - 1) * pageSize).ToString();
            string endIndex = (pageIndex * pageSize).ToString();

            totalCount = _feedBLL.GetDataCount(s_time,e_time);
            dt = _feedBLL.GetDataTable(starIndex,endIndex,s_time,e_time);

            fdata.sDate = s_time;
            fdata.eDate = e_time;

            stime = s_time;
            etime = e_time;

            ViewData["dt"] = dt;
            ViewData["pageIndex"] = pageIndex;
            ViewData["totalCount"] = totalCount;
            return View(fdata);
        }
        public ActionResult deleteFeed(string id)
        {
            if (_feedBLL.deletFeed(id))
            {
                return Content("ok");
            }
            else return Content("no");
        }
        public ActionResult modifyFeed()
        {
            if (Request["txt_remark"] != null)
            {
                _feed.id = Request["txt_id"];
                _feed.time = Request["txt_s_time"];
                _feed.remark = Request["txt_remark"];
                _feed.remark2 = Request["txt_remark2"];
                _feed.remark3 = Request["txt_e_time"];
                if (_feedBLL.modifyFeed(_feed))
                    return Content("ok");
                else return Content("no");
            }

            return View();
        }

        #endregion

        public ActionResult TryPlan(int?page)
        {
            DataTable sdept = new DataTable();
            sdept = _planBLL.sdeptTable();
            ViewData["sdept"] = sdept;//用于系别的下拉框

            DataTable grade = new DataTable();
            grade = _planBLL.selectGrage();
            ViewData["grade"] = grade;
          
            string sd = Request["sdept"]??"";
            string se = Request["sex"]??"";

            string pgrad = Request["pgrade"] ?? "";//添加上 年级 <安排寝室>

            DataTable table = new DataTable();
            table = _planBLL.selectNoPlanStu(sd, se,pgrad);//根据系院 和 性别 找到全部符合条件的数据 Student表的全部字段
            Pager pager = new Pager(table.Rows.Count, page);

            var viewModel = new IndexViewModel
            {
                itemTable = table.AsEnumerable().Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager,
                Sdept=sd,
                Sex=se
            };

            
            return View(viewModel);

        }

        #region 导出数据
        public ActionResult Leave_ExportData()
        {
            //Thread InvokeThread = new Thread();
           // SaveFileDialog save = new SaveFileDialog();
           // save.Filter = "Excel表格|*.xls";
           // if (save.ShowDialog() == DialogResult.OK)
          //  {
              //  save.RestoreDirectory = true;
               // string fileName = save.FileName;
            string fileName = Server.MapPath("~//file//excelForm.xls");
            DataTable dt = _leaveBLL.DownLoadTable("", "", stime, etime, sbid);
                NOIP.SaveToFile(NOIP.RenderToExcel(dt, "外来人员"), fileName);
                stime = "";
                etime = "";
                sbid = "";
                return Content("ok");
          //  }
           // else return Content("no");
        }
        public ActionResult Discipline_ExportData()
        {
            string fileName = Server.MapPath("~//file//DisciplineExcel.xls");
            DataTable dt = _disciplineBLL.DisciplineDownLoad("", "", stime, etime, sbid);
            NOIP.SaveToFile(NOIP.RenderToExcel(dt,"违纪情况"),fileName);
            stime = "";
            etime = "";
            sbid = "";
            return Content("ok");
        }
        public ActionResult Repair_ExportData()
        {
            string fileName = Server.MapPath("~//file//RepairExcel.xls");
            DataTable dt = _repairBLL.RepairDownLoad("", "", stime, etime, sbid, scate, sha);
            NOIP.SaveToFile(NOIP.RenderToExcel(dt,"维修情况"),fileName);
            stime = "";
            etime = "";
            sbid = "";
            scate = "";
            sha = "";
            return Content("ok");
        }
        public ActionResult School_ExportData()
        {
            string fileName = Server.MapPath("~//file//SchoolExcel.xls");
            DataTable dt = _schoolBLL.SchoolDownLoad("", "", scate, sha);
            NOIP.SaveToFile(NOIP.RenderToExcel(dt,"留校信息"),fileName);

            scate = ""; sha = "";
            return Content("ok");
        }
        public ActionResult Feed_ExportData()
        {
            string fileName = Server.MapPath("~//file//FeedBackExcel.xls");
            DataTable dt = _feedBLL.FeedDownLoad("", "", stime, etime);
            NOIP.SaveToFile(NOIP.RenderToExcel(dt,"反馈信息"),fileName);

            stime = "";
            etime = "";
            return Content("ok");
        }
        
        #endregion
        #region 用户
        public ActionResult User()
        {
            DataTable dt = new DataTable();
            int pageSize = 5;
            int totalCount;
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");//当前页
            string role = Request["txt_s_date"] ?? "";
            string sno = Request["txt_e_date"] ?? "";

            string starIndex = ((pageIndex - 1) * pageSize).ToString();
            string endIndex = (pageIndex * pageSize).ToString();

            totalCount = _loginBLL.userDataCount(role, sno);
            dt = _loginBLL.userDataTable(starIndex, endIndex, role, sno);

            fdata.sDate = role;
            fdata.eDate = sno;

            

            DataTable dd = new DataTable();
            dd = _loginBLL.selectRoel();
            ViewData["dd"] = dd;
            ViewData["dt"] = dt;
            ViewData["pageIndex"] = pageIndex;
            ViewData["totalCount"] = totalCount;
            return View(fdata);
        }

        public ActionResult addUser()
        {
            if (Request["txt_name"] != null)
            {
                if (_loginBLL.selectSno(Request["txt_sno"], Request["role"]))//编号存在
                {
                    login.name = Request["txt_name"];
                    login.pwd = Request["txt_pwd"];
                    login.remark1 = Request["txt_tel"];
                    login.remark2 = Request["txt_sno"];
                    login.remark3 = Request["txt_remark"];
                    login.role = Request["role"];
                    if (_loginBLL.insertUser(login))//插入成功
                        return Content("ok");
                    else return Content("no");//插入失败
                }
                else return Content("noexis");
            }
            return View();
        }
        public ActionResult modifyUser()
        {
            if (Request["txt_sno"] != null)
            {
                login.name = Request["txt_lname"];
                login.remark2 = Request["txt_sno"];
                login.role = Request["role"];
                login.remark1 = Request["txt_tel"];
                login.remark3 = Request["txt_remark"];
                if (_loginBLL.modifyUser(login))
                    return Content("ok");
                else return Content("no");
            }
            return View();
        }
        public ActionResult deleteUser(string id)
        {
            if (_loginBLL.deleteUser(id))
            {
                return Content("oK");
            }
            else
                return Content("no");
        }

        public ActionResult User_Export(string role)
        {
            ToWord tow = new ToWord();
            DataTable dt = _loginBLL.userDownData(role);
            string path = Server.MapPath("~//file//UserDoc.doc");
            string title = role + " "  + "信息";
            tow.CourseTable(dt,path,title);
            return Content("ok");
        }
        #endregion
    }
}
