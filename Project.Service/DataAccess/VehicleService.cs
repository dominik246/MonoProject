using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;
namespace Project.Service.DataAccess
{
    public class VehicleService<TModel> : IVehicleService<TModel> where TModel : class, Models.IModel
    {
        private readonly IServiceDBContext _db;

        public VehicleService(IServiceDBContext db)
        {
            _db = db;
        }

        public async Task<PagedResult<TModel>> FindAsync(string searchString, string sortBy, int page, int pageLength, bool paged)
        {
            var pagedResult = new PagedResult<TModel>();

            pagedResult.Results = await Task.FromResult(_db.Set<TModel>().IncludeAll(_db).GetSorted(sortBy).AsNoTracking());

            if (!string.IsNullOrEmpty(searchString))
            {
                pagedResult.Results = pagedResult.Results.GetFiltered(searchString);
            }

            if (paged)
            {
                pagedResult = pagedResult.Results.GetPaged(page, pageLength);
            }

            return pagedResult;
        }

        public async Task<TModel> GetAsync(int? id)
        {
            return await _db.Set<TModel>().IncludeAll(_db).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
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
