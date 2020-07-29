using Microsoft.EntityFrameworkCore;

using Project.Service.Models;

using System.Diagnostics.CodeAnalysis;
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
        DbSet<TEntity> Set<TEntity>([NotNullAttribute] string name) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}