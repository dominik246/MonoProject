using Microsoft.EntityFrameworkCore;

using Project.Service.Models;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Service.DataAccess
{
    public static class IQueryableExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };


            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize);

            return result;
        }

        public static IQueryable<T> GetSorted<T>(this IQueryable<T> query, string sortBy) where T : class, IModel
        {
            return sortBy switch
            {
                "Name" => query.OrderBy(s => s.Name),
                "Id" => query.OrderBy(s => s.Id),
                "Abrv" => query.OrderBy(s => s.Abrv),
                _ => query.OrderBy(s => s.Name),
            };
        }

        public static IQueryable<T> GetFiltered<T>(this IQueryable<T> query, string searchString) where T : class, IModel
        {
            if(!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Abrv.Contains(searchString) || s.Name.Contains(searchString));
            }
            return query;
        }
    }
}
