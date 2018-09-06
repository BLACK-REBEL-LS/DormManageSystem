using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.Models
{
    public class FormData
    {
        public string sDate { get; set; }//开始时间
        public string eDate { get; set; }//结束时间
        public string Bid { get; set; }//楼号

        public string cateGory { get; set; }//类别
        public string handle { get; set; }//是否处理
    }
}