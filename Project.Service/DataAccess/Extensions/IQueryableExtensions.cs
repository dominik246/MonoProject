using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Project.Service.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.DataAccess
{
    public static class IQueryableExtensions
    {
        public static PageModel<T> GetPaged<T>(this IQueryable<T> query, PageModel<T> page)
        {
            var pageCount = (double)page.CurrentRowCount / page.CurrentPageSize;
            page.TotalPageCount = pageCount == 0 ? 1 : (int)Math.Ceiling(pageCount);

            var skip = (page.CurrentPageIndex - 1) * page.CurrentPageSize;
            page.QueryResult = query.Skip(skip).Take(page.CurrentPageSize);
            return page;
        }

        public static IQueryable<T> GetSorted<T>(this IQueryable<T> query, SortModel sort) where T : class, IVehicle
        {
            if (sort == null)
                return query;

            return sort.SortBy switch
            {
                "Name" => query.OrderBy(s => s.Name),
                "Id" => query.OrderBy(s => s.Id),
                "Abrv" => query.OrderBy(s => s.Abrv),
                "Id_desc" => query.OrderBy(s => s.Id),
                "Name_desc" => query.OrderByDescending(s => s.Name),
                "Abrv_desc" => query.OrderByDescending(s => s.Abrv),
                "Make" => query.OrderBy(s => s.SelectedVehicleMake.Name),
                "Make_desc" => query.OrderByDescending(s => s.SelectedVehicleMake.Name),
                _ => query.OrderBy(s => s.Name),
            };
        }

        public static IQueryable<T> GetFiltered<T>(this IQueryable<T> query, FilterModel filter) where T : class, IVehicle
        {
            if (string.IsNullOrEmpty(filter?.FilterString))
                return query;
            if (typeof(T).GetProperty("SelectedVehicleMake") != null)
            {
                return query.Where(q => q.Abrv.Contains(filter.FilterString) || q.Name.Contains(filter.FilterString) || q.SelectedVehicleMake.Name.Contains(filter.FilterString));
            }
            else
            {
                return query.Where(q => q.Abrv.Contains(filter.FilterString) || q.Name.Contains(filter.FilterString));
            }
        }

        /// <summary>
        /// Includes all Navigation Properties from the given type.
        /// </summary>
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> query, IServiceDBContext context) where T : class
        {
            List<string> includeList = new List<string>();

            IEnumerable<INavigation> navigationProperties = context.Model.FindEntityType(typeof(T)).GetNavigations();
            if (navigationProperties != null)
            {
                foreach (INavigation navigationProperty in navigationProperties)
                {
                    if (includeList.Contains(navigationProperty.Name))
                        continue;

                    includeList.Add(navigationProperty.Name);
                    query = query.Include(navigationProperty.Name);
                }
            }
            return query;
        }
    }
}
