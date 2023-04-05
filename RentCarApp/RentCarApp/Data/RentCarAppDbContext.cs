
using Microsoft.EntityFrameworkCore;
using RentCarApp.Entities;

namespace RentCarApp.Data
{
    public class RentCarAppDbContext : DbContext
    {
        public DbSet<CarsForRent> carsForRent => Set<CarsForRent>();
        public DbSet<ReturnedCars> ReturnedCars => Set<ReturnedCars>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        }
    }
}

