
using System;
using System.Linq;

namespace Project.Service.Models
{
    public class PageModel<T>
    {
        public IQueryable<T> QueryResult { get; set; }

        public int CurrentPageIndex { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPageSize { get; set; } = 5;
        public int CurrentRowCount { get; set; }

        public bool ReturnPaged { get; set; } = true;
    }
}
