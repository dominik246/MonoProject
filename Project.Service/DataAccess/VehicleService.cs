using Microsoft.EntityFrameworkCore;

using Project.Service.Models;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Project.Service.DataAccess
{
    public class VehicleService<TEntity> : IVehicleService<TEntity> where TEntity : class, IModel
    {
        private readonly IServiceDBContext _db;
        public VehicleService(IServiceDBContext db)
        {
            _db = db;
        }

        public async Task<PagedResult<TEntity>> GetAsync(string sortBy = "Id", int page = 1, int pageLength = 10)
        {

            return sortBy switch
            {
                "Name" => await Task.FromResult(_db.Set<TEntity>().OrderBy(s => s.Name).AsNoTracking().GetPaged(page, pageLength)),
                "Id" => await Task.FromResult(_db.Set<TEntity>().OrderBy(s => s.Id).AsNoTracking().GetPaged(page, pageLength)),
                "Abrv" => await Task.FromResult(_db.Set<TEntity>().OrderBy(s => s.Abrv).AsNoTracking().GetPaged(page, pageLength)),
                _ => await Task.FromResult(_db.Set<TEntity>().AsNoTracking().GetPaged(page, pageLength))
            };
        }

        public async Task<PagedResult<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter, string sortBy = "Id", int page = 1, int pageLength = 10)
        {
            return sortBy switch
            {
                "Name" => await Task.FromResult(_db.Set<TEntity>().Where(filter).OrderBy(s => s.Name).AsNoTracking().GetPaged(page, pageLength)),
                "Id" => await Task.FromResult(_db.Set<TEntity>().Where(filter).OrderBy(s => s.Id).AsNoTracking().GetPaged(page, pageLength)),
                "Abrv" => await Task.FromResult(_db.Set<TEntity>().Where(filter).OrderBy(s => s.Abrv).AsNoTracking().GetPaged(page, pageLength)),
                _ => await Task.FromResult(_db.Set<TEntity>().Where(filter).AsNoTracking().GetPaged(page, pageLength))
            };
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _db.Set<TEntity>().Update(entity));
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                await Task.Run(() => _db.Set<TEntity>().Remove(entity));
                await _db.SaveChangesAsync();
            }
        }
    }
}
