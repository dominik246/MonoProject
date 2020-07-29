using Project.Service.Models;

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Service.DataAccess
{
    public interface IVehicleService<TModel> where TModel : class, IModel
    {
        Task CreateAsync(TModel entity);
        Task DeleteAsync(int id);
        Task<PagedResult<TModel>> GetAsync(string sortBy = "Id", int page = 1, int pageLength = 10);
        Task<PagedResult<TModel>> GetFilteredAsync(Expression<Func<TModel, bool>> filter, string sortBy = "Id", int page = 1, int pageLength = 10);
        Task<TModel> GetByIdAsync(int id);
        Task UpdateAsync(TModel entity);
    }
}