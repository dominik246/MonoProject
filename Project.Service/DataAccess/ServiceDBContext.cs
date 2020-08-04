using Microsoft.EntityFrameworkCore;

using Project.Service.Models;

using System.Diagnostics.CodeAnalysis;

namespace Project.Service.DataAccess
{
    public class ServiceDBContext : DbContext, IServiceDBContext
    {
        public ServiceDBContext(DbContextOptions options) : base(options) { }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }
}
