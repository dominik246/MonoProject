using Project.Service.Models;

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Service.DataAccess
{
    public interface IVehicleService<TEntity> where TEntity : class, IModel
    {
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task<PagedResult<TEntity>> GetAsync(string sortBy = "Id", int page = 1, int pageLength = 10);
        Task<PagedResult<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter, string sortBy = "Id", int page = 1, int pageLength = 10);
        Task<TEntity> GetByIdAsync(int id);
        Task UpdateAsync(TEntity entity);
    }
}