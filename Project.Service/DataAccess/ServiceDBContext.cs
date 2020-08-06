using Microsoft.EntityFrameworkCore;

using Project.Service.Models;

using System.Threading.Tasks;

namespace Project.Service.DataAccess
{
    public class ServiceDBContext : DbContext, IServiceDBContext
    {
        public ServiceDBContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }
}
