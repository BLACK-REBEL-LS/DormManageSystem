using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FoodPage
    {
        public static string ShowPageFood(int currentpage,int totalcout)
        {
            double pagesize = 5.0;//页面大小
            string href = "";
            var output = new StringBuilder();
            
            var totalPage = Convert.ToInt32(Math.Ceiling(totalcout / pagesize));//总页数

            //上判断
            if ((currentpage - 1) * pagesize >= totalcout) {
                currentpage = totalPage;
            }//下判断
            else if (currentpage <= 0) {
                currentpage = 1;
            }

            output.AppendFormat("<a class='pg comm' href='{0}?pageIndex=1'>首页</a>", href);

            if (totalPage > 1) {
                //if (currentpage != 1) { 
                //处理首页链接
                //    output.AppendFormat("<a class='cp' href='{0}?pageIndex=1'>首页</a>",href);
               // }

                if (currentpage > 1) { 
                //处理上一页的链接
                    output.AppendFormat("<a class='pg comm' href='{0}?pageIndex={1}'><上一页</a>",href,currentpage-1);
                }
                output.Append(" ");

                for (int i = 1; i <= 3; i++) {
                    if (currentpage == i)
                    {
                        //处理当前页
                        output.AppendFormat("<a href='{0}?pageIndx={1}' class='ccp comm'>{2}</a>", href, currentpage, currentpage);
                    }
                    else { 
                    //处理一般页
                        output.AppendFormat("<a class='cp comm' href='{0}?pageIndex={1}'>{2}</a>",href,i,i);
                    }
                }
                output.AppendFormat("<a class='dp comm' href='{0}'>{1}</a>",href,"...");
                output.AppendFormat("<a class='cp comm' href='{0}?pageIndex={1}'>{2}</a>",href,totalPage,totalPage);
                output.Append(" ");

                if (currentpage < totalPage) {
                    //处理下一页
                    output.AppendFormat("<a class='pg comm' href='{0}?pageIndex={1}'>下一页></a>",href,currentpage+1);
                }
                output.Append(" ");
            }
            output.AppendFormat("第{0}页/共{1}页",currentpage,totalPage);
            return output.ToString();
           
        }

        public static string ShowPageFood(int currentpage, int totalcout, string txt_s_date, string txt_e_date, string txt_id,string category,string handle)
        {
            double pagesize = 5.0;//页面大小
            string href = "";
            var output = new StringBuilder();

            var totalPage = Convert.ToInt32(Math.Ceiling(totalcout / pagesize));//总页数

            //上判断
            if ((currentpage - 1) * pagesize >= totalcout)
            {
                currentpage = totalPage;
            }//下判断
            else if (currentpage <= 0)
            {
                currentpage = 1;
            }

            output.AppendFormat("<a class='pg comm' href='{0}?pageIndex=1 & txt_s_date={1} & txt_e_date={2}&txt_id={3}&txt_category={4}&handle={5}'>首页</a>", href,txt_s_date,txt_e_date,txt_id,category,handle);

            if (totalPage > 1)
            {
                //if (currentpage != 1) { 
                //处理首页链接
                //    output.AppendFormat("<a class='cp' href='{0}?pageIndex=1'>首页</a>",href);
                // }

                if (currentpage > 1)
                {
                    //处理上一页的链接
                    output.AppendFormat("<a class='pg comm' href='{0}?pageIndex={1}&txt_s_date={2}&txt_e_date={3}&txt_id={4}&txt_category={5}&handle={6}'><上一页</a>", href, currentpage - 1,txt_s_date,txt_e_date,txt_id,category,handle);
                }
                output.Append(" ");

                for (int i = 1; i <= 3; i++)
                {
                    if (currentpage == i)
                    {
                        //处理当前页
                        output.AppendFormat("<a href='{0}?pageIndx={1}&txt_s_date={2}&txt_e_date={3}&txt_id={4}&txt_category={5}&handle={6}' class='ccp comm'>{7}</a>", href, currentpage,txt_s_date,txt_e_date,txt_id,category,handle, currentpage);
                    }
                    else
                    {
                        //处理一般页
                        output.AppendFormat("<a class='cp comm' href='{0}?pageIndex={1}&txt_s_date={2}&txt_e_date={3}&txt_id={4}&txt_category={5}&handle={6}'>{7}</a>", href, i,txt_s_date,txt_e_date,txt_id,category,handle, i);
                    }
                }
                output.AppendFormat("<a class='dp comm' href='{0}'>{1}</a>", href, "...");
                output.AppendFormat("<a class='cp comm' href='{0}?pageIndex={1}&txt_s_date={2}&txt_e_date={3}&txt_id={4}&txt_category={5}&handle={6}'>{7}</a>", href, totalPage,txt_s_date,txt_e_date,txt_id,category ,handle,totalPage);
                output.Append(" ");

                if (currentpage < totalPage)
                {
                    //处理下一页
                    output.AppendFormat("<a class='pg comm' href='{0}?pageIndex={1}&txt_s_date={2}&txt_e_date={3}&txt_id={4}&txt_category={5}&handle={6}'>下一页></a>", href, currentpage + 1,txt_s_date,txt_e_date,txt_id,category,handle);
                }
                output.Append(" ");
            }
            output.AppendFormat("第{0}页/共{1}页", currentpage, totalPage);
            return output.ToString();
        }
    }
}
