using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcApplication2.Models
{
   
       
        public class IndexViewModel
        {
            //数据的集合
            //public IEnumerable<string> Items { get; set; }
            public IEnumerable<DataRow> itemTable { get; set; }
            //public DataTable itemTable { get; set; }
            //分页
            public Pager Pager { get; set; }

            public string Sdept { get; set; }
            public string Sex { get; set; }

            public string Sgrade { get; set; }
        }
    }
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 2)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }
            TotalPage = totalPages;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalItems = totalItems;
            StartPage = startPage;
            EndPage = endPage;
        }
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPage { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
}