using Microsoft.EntityFrameworkCore;

using Project.Service.Models;

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

        public async Task<PagedResult<TModel>> FindAsync(string searchString, string sortBy, int page, int pageLength)
        {
            return await _db.Set<TModel>().GetFiltered(searchString).GetSorted(sortBy).AsNoTracking().GetPagedAsync(page, pageLength);
        }

        public async Task<TModel> GetAsync(int? id)
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
