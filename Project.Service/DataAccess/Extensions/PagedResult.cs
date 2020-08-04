using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.DataAccess
{
    public class PagedResult<T> where T : class
    {
        public IQueryable<T> Results { get; set; }

        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage
        {

            get { return ((CurrentPage - 1) * PageSize) + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }
}
