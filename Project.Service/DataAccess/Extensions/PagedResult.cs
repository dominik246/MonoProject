using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.DataAccess
{
    public class PagedResult<T> where T : class
    {
        public IQueryable<T> Results { get; set; }

        public int CurrentPageIndex { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPageSize { get; set; }
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

        private List<T> _listResults;
        public List<T> ListResults
        {
            get
            {
                return _listResults ??= Results.ToList();
            }
            set
            {
                _listResults = value;
            }
        }
    }
}
