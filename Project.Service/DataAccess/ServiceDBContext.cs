using Microsoft.EntityFrameworkCore;

using Project.Service.Models;

namespace Project.Service.DataAccess
{
    public class ServiceDBContext : DbContext, IServiceDBContext
    {
        public ServiceDBContext(DbContextOptions options) : base(options) { }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }
}
