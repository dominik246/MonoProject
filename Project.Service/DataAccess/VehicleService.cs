using Microsoft.EntityFrameworkCore;

using Project.Service.Models;

using System.Linq;
using System.Threading.Tasks;
namespace Project.Service.DataAccess
{
    public class VehicleService<TModel> : IVehicleService<TModel> where TModel : class, IVehicle
    {
        private readonly IServiceDBContext _db;
        public VehicleService(IServiceDBContext db)
        {
            _db = db;
        }

        public async Task<PageModel<TModel>> FindAsync(FilterModel filter, PageModel<TModel> page, SortModel sort)
        {
            page ??= new PageModel<TModel>() { ReturnPaged = false };
            page.QueryResult = await Task.FromResult(_db.Set<TModel>().IncludeAll(_db)
                .GetSorted(sort).GetFiltered(filter).AsNoTracking());

            if (page.ReturnPaged)
            {
                page.CurrentRowCount = page.QueryResult.Count();
                page.QueryResult = page.QueryResult.GetPaged(page).QueryResult;
                page.TotalPageCount = page.TotalPageCount;
            }

            return page;
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
