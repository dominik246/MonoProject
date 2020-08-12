using Project.Service.DataAccess;

using System;
using System.Collections.Generic;
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

        public int FirstRowOnPage
        {

            get { return ((CurrentPageIndex - 1) * CurrentPageSize) + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPageIndex * CurrentPageSize, CurrentRowCount); }
        }

        public bool HasPreviousPage
        {
            get
            {
                return CurrentPageIndex > 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return CurrentPageIndex < TotalPageCount;
            }
        }

        public bool ReturnPaged { get; set; } = true;

        public IEnumerable<T> ListResult
        {
            get
            {
                return QueryResult.ToList();
            }
        }
    }
}
