using Microsoft.EntityFrameworkCore;

using Project.Service.Models;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Project.Service.DataAccess
{
    public class VehicleService<TModel> : IVehicleService<TModel> where TModel : class, IModel
    {
        private readonly IServiceDBContext _db;
        public VehicleService(IServiceDBContext db)
        {
            _db = db;
        }

        public async Task<PagedResult<TModel>> GetAsync(string sortBy = "Id", int page = 1, int pageLength = 10)
        {

            return sortBy switch
            {
                "Name" => await Task.FromResult(_db.Set<TModel>().OrderBy(s => s.Name).AsNoTracking().GetPaged(page, pageLength)),
                "Id" => await Task.FromResult(_db.Set<TModel>().OrderBy(s => s.Id).AsNoTracking().GetPaged(page, pageLength)),
                "Abrv" => await Task.FromResult(_db.Set<TModel>().OrderBy(s => s.Abrv).AsNoTracking().GetPaged(page, pageLength)),
                _ => await Task.FromResult(_db.Set<TModel>().AsNoTracking().GetPaged(page, pageLength))
            };
        }

        public async Task<PagedResult<TModel>> GetFilteredAsync(Expression<Func<TModel, bool>> filter, string sortBy = "Id", int page = 1, int pageLength = 10)
        {
            return sortBy switch
            {
                "Name" => await Task.FromResult(_db.Set<TModel>().Where(filter).OrderBy(s => s.Name).AsNoTracking().GetPaged(page, pageLength)),
                "Id" => await Task.FromResult(_db.Set<TModel>().Where(filter).OrderBy(s => s.Id).AsNoTracking().GetPaged(page, pageLength)),
                "Abrv" => await Task.FromResult(_db.Set<TModel>().Where(filter).OrderBy(s => s.Abrv).AsNoTracking().GetPaged(page, pageLength)),
                _ => await Task.FromResult(_db.Set<TModel>().Where(filter).AsNoTracking().GetPaged(page, pageLength))
            };
        }

        public async Task<TModel> GetByIdAsync(int id)
        {
            return await _db.Set<TModel>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(TModel entity)
        {
            await _db.Set<TModel>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TModel entity)
        {
            await Task.Run(() => _db.Set<TModel>().Update(entity));
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Set<TModel>().FindAsync(id);

            if (entity != null)
            {
                await Task.Run(() => _db.Set<TModel>().Remove(entity));
                await _db.SaveChangesAsync();
            }
        }
    }
}
