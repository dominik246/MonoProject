using Project.Service.Models;

using System.Threading.Tasks;

namespace Project.Service.DataAccess
{
    public interface IVehicleService<TModel> where TModel : class, IVehicle
    {
        Task CreateAsync(TModel entity);
        Task DeleteAsync(int id);
        Task<PageModel<TModel>> FindAsync(FilterModel filter, PageModel<TModel> page, SortModel sort);
        Task<TModel> GetAsync(int? id);
        Task UpdateAsync(TModel entity);
    }
}