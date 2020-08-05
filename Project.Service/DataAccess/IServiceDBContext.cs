using Microsoft.EntityFrameworkCore;

using Project.Service.Models;

using System.Threading;
using System.Threading.Tasks;

namespace Project.Service.DataAccess
{
    public interface IServiceDBContext
    {
        DbSet<VehicleMake> VehicleMakes { get; set; }
        DbSet<VehicleModel> VehicleModels { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        DbSet<TModel> Set<TModel>() where TModel : class;
        Microsoft.EntityFrameworkCore.Metadata.IModel Model { get; }
    }
}