using Microsoft.EntityFrameworkCore;

using Project.Service.Models;

namespace Project.Service.DataAccess
{
    public class ServiceDBContext : DbContext, IServiceDBContext
    {
        public ServiceDBContext(DbContextOptions options) : base(options) { }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleModel>()
                .HasOne(s => s.VehicleMake)
                .WithMany(s => s.VehicleModels)
                .HasForeignKey(s => s.MakeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
