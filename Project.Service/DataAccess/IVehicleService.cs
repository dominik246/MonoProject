﻿using Project.Service.Models;

using System.Threading.Tasks;

namespace Project.Service.DataAccess
{
    public interface IVehicleService<TModel> where TModel : class, IModel
    {
        Task CreateAsync(TModel entity);
        Task DeleteAsync(int id);
        Task<PagedResult<TModel>> FindAsync(string searchString, string sortBy, int page = 1, int pageLength = 10);
        Task<TModel> GetByIdAsync(int id);
        Task UpdateAsync(TModel entity);
    }
}