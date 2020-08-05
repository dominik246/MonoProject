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
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPageIndex = page,
                CurrentPageSize = pageSize,
                CurrentRowCount = query.Count()
            };


            var pageCount = (double)result.CurrentRowCount / pageSize;
            result.TotalPageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize);

            return result;
        }

        public static IQueryable<T> GetSorted<T>(this IQueryable<T> query, string sortBy) where T : class, Models.IModel
        {
            return sortBy switch
            {
                "Name" => query.OrderBy(s => s.Name),
                "Id" => query.OrderBy(s => s.Id),
                "Abrv" => query.OrderBy(s => s.Abrv),
                "Id_desc" => query.OrderBy(s => s.Id),
                "Name_desc" => query.OrderByDescending(s => s.Name),
                "Abrv_desc" => query.OrderByDescending(s => s.Abrv),
                _ => query.OrderBy(s => s.Name),
            };
        }

        public static IQueryable<T> GetFiltered<T>(this IQueryable<T> query, string searchString) where T : class, Models.IModel
        {
            if (typeof(T).IsAssignableFrom(typeof(VehicleModel)))
            {
                return query.Where(q => q.Abrv.Contains(searchString) || q.Name.Contains(searchString) || q.SelectedVehicleMake.Name.Contains(searchString));
            }
            else
            {
                return query.Where(q => q.Abrv.Contains(searchString) || q.Name.Contains(searchString));
            }
        }

        public static IQueryable<T> Include<T>(this IQueryable<T> query, IServiceDBContext context) where T : class
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
